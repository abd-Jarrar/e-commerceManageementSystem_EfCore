using ECommerce.Business.Entities;
using ECommerce.Business.Interfaces;
using ECommerce.Business.Requests;
using ECommerce.Business.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        private Result<Order> GetOrderForOperation(int id)
        {
            if (id <= 0)
            {
                return Result<Order>.Failure(
                    "Order ID must be greater than zero.");
            }

            var order = _orderRepository.GetById(id);

            if (order is null)
            {
                return Result<Order>.Failure(
                    "Order not found.");
            }

            return Result<Order>.Success(order);
        }

        public OrderService(IProductRepository productRepository,IOrderRepository orderRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public Result<Order> CreateOrder(int customerId,List<OrderItemRequest> items)
        {
            if (customerId <= 0)
            {
                return Result<Order>.Failure(
                    "Customer ID must be greater than zero.");
            }

            if (items is null || items.Count == 0)
            {
                return Result<Order>.Failure(
                    "Order must contain at least one item.");
            }

            var customer = _customerRepository.GetById(customerId);

            if (customer is null)
            {
                return Result<Order>.Failure(
                    "Customer not found.");
            }

            _unitOfWork.BeginTransaction();

            try
            {
                var order = new Order
                {
                    CustomerId = customerId,
                    CreatedDate = DateTime.Now,
                    Status = OrderStatus.Pending
                };

                foreach (var item in items)
                {
                    if (item.ProductId <= 0)
                    {
                        _unitOfWork.RollbackTransaction();

                        return Result<Order>.Failure(
                            "Product ID must be greater than zero.");
                    }

                    if (item.Quantity <= 0)
                    {
                        _unitOfWork.RollbackTransaction();

                        return Result<Order>.Failure(
                            "Product quantity must be greater than zero.");
                    }

                    var product = _productRepository.GetById(item.ProductId);

                    if (product is null)
                    {
                        _unitOfWork.RollbackTransaction();

                        return Result<Order>.Failure(
                            $"Product with ID {item.ProductId} was not found.");
                    }

                    var stockDecreased =
                        _productRepository.TryDecreaseStock(
                            item.ProductId,
                            item.Quantity);

                    if (!stockDecreased)
                    {
                        _unitOfWork.RollbackTransaction();

                        return Result<Order>.Failure(
                            $"Not enough stock for product {product.Name}.");
                    }

                    order.Items.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        Price = product.Price
                    });
                }

                _orderRepository.Add(order);

                _unitOfWork.SaveChanges();

                _unitOfWork.CommitTransaction();

                return Result<Order>.Success(order);
            }
            catch
            {
                _unitOfWork.RollbackTransaction();

                throw;
            }
        }

        public Result<Order> GetOrderById(int id)
        {
            var result = GetOrderForOperation(id);
            if (!result.IsSuccess)
            {
                return result;
            }
            return Result<Order>.Success(result.Data!);
        }

        public Result<List<Order>> GetAllOrders()
        {
            var orders = _orderRepository.GetAll();

            return Result<List<Order>>.Success(orders);
        }

        public Result<List<Order>> GetOrders(Expression<Func<Order, bool>> condition)
        {
            if (condition is null)
            {
                return Result<List<Order>>.Failure(
                    "Order condition cannot be null.");
            }

            var orders = _orderRepository.GetOrders(condition);

            return Result<List<Order>>.Success(orders);
        }
        public Result<Order> MarkOrderAsPaid(int id)
        {
            var result = GetOrderForOperation(id);

            if (!result.IsSuccess)
            {
                return result;
            }

            var order = result.Data!;

            if (order.Status != OrderStatus.Pending)
            {
                return Result<Order>.Failure(
                    "Only pending orders can be marked as paid.");
            }

            order.Status = OrderStatus.Paid;

            _orderRepository.Update(order);
            _unitOfWork.SaveChanges();

            return Result<Order>.Success(order);
        }
        public Result<Order> MarkOrderAsShipped(int id)
        {
            var result = GetOrderForOperation(id);

            if (!result.IsSuccess)
            {
                return result;
            }

            var order = result.Data!;

            if (order.Status != OrderStatus.Paid)
            {
                return Result<Order>.Failure(
                    "Only paid orders can be shipped.");
            }

            order.Status = OrderStatus.Shipped;

            _orderRepository.Update(order);
            _unitOfWork.SaveChanges();

            return Result<Order>.Success(order);
        }

        public Result<Order> CancelOrder(int id)
        {
            var result = GetOrderForOperation(id);

            if (!result.IsSuccess)
            {
                return result;
            }

            var order = result.Data!;

            if (order.Status == OrderStatus.Shipped)
            {
                return Result<Order>.Failure(
                    "Shipped orders cannot be cancelled.");
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                return Result<Order>.Failure(
                    "Order is already cancelled.");
            }

            foreach (var item in order.Items)
            {
                var product = _productRepository.GetById(item.ProductId);

                if (product is null)
                {
                    return Result<Order>.Failure(
                        $"Product with ID {item.ProductId} was not found.");
                }

                product.StockQuantity += item.Quantity;
            }

            order.Status = OrderStatus.Cancelled;

            _orderRepository.Update(order);
            _unitOfWork.SaveChanges();

            return Result<Order>.Success(order);
        }
    }
}

using ProductOrderApi.Interfaces;
using ProductOrderApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductOrderApi.Services
{
    /// <summary>
    /// Provides operations for managing orders.
    /// </summary>
    public class OrderService
    {
        private readonly IOrderRepository _repo;

        /// <summary>
        /// Constructor injecting order repository.
        /// </summary>
        public OrderService(IOrderRepository repo) => _repo = repo;

        /// <summary>
        /// Retrieve all orders from the repository.
        /// </summary>
        public Task<IEnumerable<Order>> GetAllAsync() => _repo.GetAllAsync();

        /// <summary>
        /// Retrieve an order by its ID.
        /// </summary>
        public Task<Order?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        /// <summary>
        /// Add a new order to the repository.
        /// </summary>
        public Task AddAsync(Order order) => _repo.AddAsync(order);

        /// <summary>
        /// Update an existing order.
        /// </summary>
        public Task UpdateAsync(Order order) => _repo.UpdateAsync(order);

        /// <summary>
        /// Delete an order by its ID.
        /// </summary>
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}

using ProductOrderApi.Interfaces;
using ProductOrderApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductOrderApi.Services
{
    /// <summary>
    /// Provides operations for managing customers.
    /// </summary>
    public class CustomerService
    {
        private readonly ICustomerRepository _repo;

        /// <summary>
        /// Constructor injecting customer repository.
        /// </summary>
        public CustomerService(ICustomerRepository repo) => _repo = repo;

        /// <summary>
        /// Retrieve all customers from the repository.
        /// </summary>
        public Task<IEnumerable<Customer>> GetAllAsync() => _repo.GetAllAsync();

        /// <summary>
        /// Retrieve a customer by their ID.
        /// </summary>
        public Task<Customer?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        /// <summary>
        /// Add a new customer to the repository.
        /// </summary>
        public Task AddAsync(Customer customer) => _repo.AddAsync(customer);

        /// <summary>
        /// Update an existing customer.
        /// </summary>
        public Task UpdateAsync(Customer customer) => _repo.UpdateAsync(customer);

        /// <summary>
        /// Delete a customer by their ID.
        /// </summary>
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}

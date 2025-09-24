using ProductOrderApi.Interfaces;
using ProductOrderApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductOrderApi.Services
{
    /// <summary>
    /// Defines product service interface with product operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get all products asynchronously.
        /// </summary>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Get a product by its ID asynchronously.
        /// </summary>
        Task<Product?> GetByIdAsync(int id);
    }

    /// <summary>
    /// Implements product service with repository integration.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        /// <summary>
        /// Constructor injecting product repository.
        /// </summary>
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Retrieve all products from the repository.
        /// </summary>
        public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();

        /// <summary>
        /// Retrieve a single product by its ID.
        /// </summary>
        public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    }
}

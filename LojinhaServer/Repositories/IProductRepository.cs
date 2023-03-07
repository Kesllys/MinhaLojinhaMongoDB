using LojinhaServer.Collections;
namespace LojinhaServer.Repositories;

public interface IProductRepository 
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(string id);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(string id);
}
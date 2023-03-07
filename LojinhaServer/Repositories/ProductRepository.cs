using LojinhaServer.Collections;
using MongoDB.Driver;

namespace LojinhaServer.Repositories;

public class ProductRepository : IProductRepository 
{
    private readonly IMongoCollection<Product> _colletion;
    public ProductRepository(IMongoDatabase mongoDatabase) {
        _colletion = mongoDatabase.GetCollection<Product>("products");
    }

    public async Task<List<Product>> GetAllAsync() =>
    await _colletion.Find(_ => true).ToListAsync();

    public async Task<Product> GetByIdAsync(string id) => 
    await _colletion.Find(_ => _.Id == id) .FirstOrDefaultAsync();

    public async Task CreateAsync(Product product) =>
    await _colletion.InsertOneAsync(product);

    public async Task UpdateAsync(Product product) =>
    await _colletion.ReplaceOneAsync(x => x.Id == product.Id, product);

    public async Task DeleteAsync(string id) =>
        await _colletion.DeleteOneAsync(x => x.Id == id);
}
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;

        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var collection = _database.GetCollection<Product>("products");
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var collection = _database.GetCollection<Product>("products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var collection = _database.GetCollection<Product>("products");
            await collection.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var collection = _database.GetCollection<Product>("products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var update = Builders<Product>.Update
                .Set(p => p.Name, product.Name)
                .Set(p => p.Category, product.Category)
                .Set(p => p.Summary, product.Summary)
                .Set(p => p.Description, product.Description)
                .Set(p => p.ImageFile, product.ImageFile)
                .Set(p => p.Price, product.Price);
            var result = await collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var collection = _database.GetCollection<Product>("products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}

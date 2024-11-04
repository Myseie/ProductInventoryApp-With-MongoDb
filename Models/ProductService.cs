using MongoDB.Bson;
using MongoDB.Driver;
using ProductInventoryApp.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ProductInventoryApp.Data;

namespace ProductInventoryApp.Models
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<MongoDBSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _products = database.GetCollection<Product>(mongoDbSettings.Value.CollectionName);
        }

        public List<Product> GetProducts() => _products.Find(product => true).ToList();

        public Product GetProduct(string id) => _products.Find<Product>(product => product.Id == ObjectId.Parse(id)).FirstOrDefault();

        public void Create(Product product) => _products.InsertOne(product);

        public void Update(string id, Product updatedProduct) =>
            _products.ReplaceOne(product => product.Id == ObjectId.Parse(id), updatedProduct);

        public void DeleteProduct(string id)
        {
            _products.DeleteOne(product => product.Id == ObjectId.Parse(id));
        }
    }
}

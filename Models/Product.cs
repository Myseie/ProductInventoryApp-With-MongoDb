using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ProductInventoryApp.Models
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required]
        public string Name { get; set; } 
        
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}

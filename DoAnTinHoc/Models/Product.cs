using System;

namespace DoAnTinHoc.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal PriceRate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? CategoryId { get; set; }
        public Product() { }

        public Product(int id, string name, string description, decimal price, int quantity, decimal priceRate)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            PriceRate = priceRate;
            CreatedDate = DateTime.Now;
        }
    }
}

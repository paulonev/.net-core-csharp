namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public void ChangeData(string name, string description, decimal price, string category)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }
    }
}
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void CanAddProduct()
        {
            //Arrange
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};
            
            Cart target = new Cart();
            
            //Act
            target.AddItem(p1, 2);
            target.AddItem(p2, 3);
            CartLine[] results = target.Lines.ToArray();
            
            //Assert
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(2, results.Length);
        }

        [Fact]
        public void CanAddQuantityForExistingProduct()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};
            
            Cart target = new Cart();
            
            //Act
            target.AddItem(p1, 2);
            target.AddItem(p2, 4);
            target.AddItem(p1,1);
            CartLine[] results = target.Lines
                .OrderBy(l=>l.Product.ProductID).ToArray();
            
            //Assert
            Assert.Equal(3, results[0].Quantity);
            Assert.Equal(4,results[1].Quantity);
        }

        [Fact]
        public void CanRemoveProduct()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};
            
            Cart target = new Cart();
            
            //Act
            target.AddItem(p1, 2);
            target.AddItem(p2, 4);
            target.RemoveItem(p1);
            
            //Assert
            Assert.Equal(0, target.Lines.Count(l=>l.Product == p1));
        }

        [Fact]
        public void CalculateCartTotalValue()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1", Price = 10m};
            Product p2 = new Product {ProductID = 2, Name = "P2", Price = 15m};
            
            Cart target = new Cart();
            
            //Act
            target.AddItem(p1, 2);
            target.AddItem(p2, 4);
            decimal result = target.ComputeTotalValue();
            
            Assert.True(result == 80m);
        }

        [Fact]
        public void CanClearContents()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};
            
            Cart target = new Cart();
            
            //Act
            target.AddItem(p1, 2);
            target.AddItem(p2, 4);

            target.Clear();
            
            Assert.Empty(target.Lines);
        }
    } 
}
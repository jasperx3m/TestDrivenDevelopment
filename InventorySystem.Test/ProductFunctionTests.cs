using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem;
using Models;
using Xunit;

namespace InventorySystem.Test
{
    public class ProductFunctionTest
    {
        [Fact]
        public void ViewProducts_ShouldWork()
        {
            Product product1 = new Product { ProductID = 1001, ProductName = "Chair", Quantity = 22 };
            Product product2 = new Product { ProductID = 1002, ProductName = "Tabler", Quantity = 15 };
            List<Product> productlist = new List<Product>();
            ProductFunction.AddProduct(productlist, product1);
            ProductFunction.AddProduct(productlist, product2);
            ProductFunction.ViewProducts(productlist);
            Assert.True(productlist.Count == 2);
        }
        [Fact]
        public void AddProduct_ShouldWork()
        {
            Product product = new Product { ProductID = 1001, ProductName = "Chair", Quantity=22 };
            List<Product> productlist = new List<Product>();

            ProductFunction.AddProduct(productlist, product);
            Assert.True(productlist.Count == 1);
            Assert.Contains<Product>(product, productlist);

        }

        [Theory]
        [InlineData(null,"Chair",22,"Product ID")]//null id
        [InlineData(int.MaxValue,"Chair",null,"Product Quantity")]//Negative quantity
        [InlineData(1001,"",22,"Product Name")]//null productname
        [InlineData(-100,"Chair",15,"Product ID")]//Negatuve ID
        [InlineData(10002, "Chair",-123,"Product Quantity")]//Negative Quantity
        [InlineData(12, "ChairChairChairChairChairChairChairChairChairChairChairChairChairChairChairChairChairChair",15,"ProductName")]//More than 20 characters
        [InlineData(12, "Ch", 15, "Product Name")]
        public void AddProduct_ShouldFail(int productid,string productname,int quantity,string param)
        {
            Product product = new Product { ProductID = productid, ProductName =productname, Quantity =quantity };
            List<Product> productlist = new List<Product>();
            Assert.Throws<ArgumentException>(param, () => ProductFunction.AddProduct(productlist, product));
        }
        [Fact]
        public void AddProductDuplicateProductID_ShouldFail()
        {
            Product product1 = new Product { ProductID = 1001, ProductName = "Chair", Quantity = 22 };
            Product product2 = new Product { ProductID = 1001, ProductName = "Table", Quantity = 15 };
            List<Product> productlist = new List<Product>();

            ProductFunction.AddProduct(productlist, product1);
            Assert.Throws<ArgumentException>(() => ProductFunction.AddProduct(productlist, product2));
            Assert.True(productlist.Count == 1);
        }
        [Fact]
        public void RemoveProduct_ShouldWork()
        {
            Product product1 = new Product { ProductID = 1001, ProductName = "Chair", Quantity = 22 };
            Product product2 =new Product { ProductID = 1002, ProductName = "Tabler", Quantity = 15 };
            List<Product> productlist = new List<Product>();
            ProductFunction.AddProduct(productlist, product1);
            ProductFunction.AddProduct(productlist, product2);
            ProductFunction.RemoveProduct(productlist, product1.ProductID);
            Assert.True(productlist.Count == 1);
            Assert.DoesNotContain(product1, productlist);
        }
        [Fact]
        public void RemoveProduct_ShouldFail()
        {
            Product product1 = new Product { ProductID = 1001, ProductName = "Chair", Quantity = 22 };
            Product product2 = new Product { ProductID = 1002, ProductName = "Tabler", Quantity = 15 };
            List<Product> productlist = new List<Product>();
            ProductFunction.AddProduct(productlist, product1);
            ProductFunction.AddProduct(productlist, product2);
            Assert.Throws<ArgumentException>(() => ProductFunction.RemoveProduct(productlist, 10003));

        }
        [Fact]
        public void EditProductByID_ShouldWork()
        {
            Product product1 = new Product { ProductID = 1001, ProductName = "Chair", Quantity = 22 };
            Product product2 = new Product { ProductID = 1002, ProductName = "Table", Quantity = 15 };
            List<Product> productlist = new List<Product>();
            ProductFunction.AddProduct(productlist, product1);
            ProductFunction.AddProduct(productlist, product2);
            Product product_edit = new Product { ProductID = 1001, ProductName = "Sofa", Quantity = 25 };
            Assert.True(ProductFunction.EditProductByID(productlist, 1001, product_edit));
        }
        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1001,1002, "Sofa", int.MaxValue },//duplicate id
            new object[] {99,int.MaxValue,"Chair",-23},// id does not exist
            new object[] {null,1000,"Chair",15},//null reference id
            new object[] {1001,1000,"",15},//null productname
            new object[] {1001,null,"Sofa",15},//null Product ID
            new object[] {1001,1000,"Chair",null},//null Quantity
            new object[] {1001,int.MinValue,"Chair",23},//negative id
            new object[] {1001,int.MaxValue,"Chair",-23},//negative quantity
            new object[] {1001,1001,"Ch",15},//Name less than 3 characters
            new object[] {1001,1001, "ChairSofaChairSofaChairSofaChairSofaChairSofaChairSofaChairSofa", 75},//Name is greater than 20 characters

        };
        [Theory]
        [MemberData(nameof(Data))]
        public void EditProductByID_ShouldFail(int productid,int newproductid,string productname, int quantity)
        {
            Product product1 = new Product { ProductID = 1001, ProductName = "Chair", Quantity = 22 };
            Product product2 = new Product { ProductID = 1002, ProductName = "Table", Quantity = 15 };
            List<Product> productlist = new List<Product>();
            ProductFunction.AddProduct(productlist, product1);
            ProductFunction.AddProduct(productlist, product2);
            Product product_edit = new Product { ProductID = newproductid, ProductName = productname, Quantity = quantity };
            Assert.Throws<ArgumentException>(() => ProductFunction.EditProductByID(productlist,productid, product_edit));


        }




    }
}

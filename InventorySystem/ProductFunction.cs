using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace InventorySystem
{
    public class ProductFunction
    {
        #region ViewProducts
        public static List<Product> ViewProducts(List<Product> productlist)
        {
            return productlist;
        } 
        #endregion
        #region AddProduct
        public static void AddProduct(List<Product> productlist, Product product)
        {
            bool uniqueID = false;
            if (productlist.Count == 0)
            {
                uniqueID = true;
            }
            foreach (var checkID in productlist)
            {
                if (checkID.ProductID != product.ProductID)
                {
                    uniqueID = true;
                }
            }
            if (uniqueID == true)
            {
                if (string.IsNullOrWhiteSpace(product.ProductName))
                {
                    throw new ArgumentException("You passed in an invalid parameter", "Product Name");
                }
                if (product.ProductID == 0)
                {
                    throw new ArgumentException("You passed a null value", "Product ID");
                }
                if (product.Quantity == 0)
                {
                    throw new ArgumentException("You passed a null value", "Product Quantity");
                }
                if (product.ProductID < 0)
                {
                    throw new ArgumentException("You passed a negative value", "Product ID");
                }
                if (product.Quantity < 0)
                {
                    throw new ArgumentException("You passed a negative value", "Product Quantity");
                }
                if (product.ProductName.Length < 3)
                {
                    throw new ArgumentException("Product Name was too short", "Product Name");
                }
                if (product.ProductName.Length > 20)
                {
                    throw new ArgumentException("Product Name was too long", "ProductName");
                }
                productlist.Add(product);
            }
            else
            {
                throw new ArgumentException("You passed an already used Product ID");
            }

        }
        #endregion
        #region RemoveProduct
        public static void RemoveProduct(List<Product> productlist, int productID)
        {
            bool containsID = false;
            foreach (var product in productlist)
            {
                if (product.ProductID == productID)
                {
                    containsID = true;
                }
            }
            if (containsID == true)
            {
                var toDelete = from product in productlist
                               where product.ProductID == productID
                               select product;
                productlist.RemoveAll(toDelete.Contains);
            }
            else
            {
                throw new ArgumentException("There is no such Product ID", "Product ID");
            }

        }
        #endregion
        #region EditProduct
        public static bool EditProductByID(List<Product> productlist, int id, Product product)
        {
            bool isAvailable = false;
            bool isUnique = true;
            foreach (var i in productlist)
            {

                if (i.ProductID == id)
                {
                    isAvailable = true;
                }
            }
            foreach (var i in productlist.Where(p => p.ProductID != id))
            {
                if (product.ProductID == i.ProductID)
                {
                    isUnique = false;
                }
            }
            if (isUnique == false)
            {
                throw new ArgumentException("You passed an an already used ID");
            }

            if (isAvailable == true && isUnique == true)
            {
                if (string.IsNullOrWhiteSpace(product.ProductName))
                {
                    throw new ArgumentException("You passed in an invalid parameter");
                }
                if (product.ProductID == 0)
                {
                    throw new ArgumentException("You passed a null value");
                }
                if (product.Quantity == 0)
                {
                    throw new ArgumentException("You passed a null value");
                }
                if (product.ProductID < 0)
                {
                    throw new ArgumentException("You passed a negative value");
                }
                if (product.Quantity < 0)
                {
                    throw new ArgumentException("You passed a negative value");
                }
                if (product.ProductName.Length < 3)
                {
                    throw new ArgumentException("Product Name was too short");
                }
                if (product.ProductName.Length > 20)
                {
                    throw new ArgumentException("Product Name was too long");
                }
                foreach (var i in productlist.Where(p => p.ProductID == id))
                {
                    i.ProductID = product.ProductID;
                    i.ProductName = product.ProductName;
                    i.Quantity = product.Quantity;
                }
                return true;
            }

            else
            {
                throw new ArgumentException("Product ID is not found");
            }
        } 
        #endregion
    }
}

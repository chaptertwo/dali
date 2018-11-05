using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using System.IO;

namespace Flooring.Data
{
    public class ProductRepository : IProductRepository
    {
        private string filePath = @"C:\Data\Flooring\Products.txt";



        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine(); //skip header
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    //Product newProduct = new Product();
                    Product product = new Product();

                    string[] columns = line.Split(',');

                    product.ID = columns[0];
                    product.ProductType = columns[1];
                    product.CostPerSQFoot = decimal.Parse(columns[2]);
                    product.LaborCostPerSQFoot = decimal.Parse(columns[3]);
                    products.Add(product);
                }
                return products;
            }
        }

        
        public Product GetProductByID(string id)
        {
            
            var specificProduct = GetProducts().FirstOrDefault(p => p.ID == id);
            return specificProduct;

        }

        public Product GetProductByType(string productType)
        {
            var specificProduct = GetProducts().FirstOrDefault(p => p.ProductType == productType);
            return specificProduct;

        }
    }
}

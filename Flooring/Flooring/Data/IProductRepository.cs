using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flooring.Models;
using System.Threading.Tasks;

namespace Flooring.Data
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductByID(string ID);
        Product GetProductByType(string productType);
    }
}

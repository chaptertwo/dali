using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.UI;
using Flooring.Data;
using Flooring.Domain;

namespace Flooring
{
    class Program
    {
        static void Main(string[] args) 
        {
            //const string filePath = @"C:\Data\Flooring\TestOrders\Orders_06012013.txt";
            //IOrderRepository orderRepo = new OrderTestRepository(filePath);
            //ITaxRepository taxRepo = new TaxRepository();
            //IProductRepository productRepo = new ProductRepository();

            Service service = ServiceFactory.Create();

            Controller controller = new Controller(service);
            controller.Run();
        }
    }
}

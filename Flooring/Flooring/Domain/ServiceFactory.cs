using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Flooring.Data;
using Flooring.Domain;

namespace Flooring.Domain
{
    public static class ServiceFactory
    {
        public static Service Create()
        {    //we specify a "key" called mode in the .config file
            string mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Test":
                    return new Service(new OrderTestRepository(), new TaxRepository(), new ProductRepository());
                case "Production":
                    return new Service(new OrderRepository(), new TaxRepository(), new ProductRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}

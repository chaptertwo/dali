using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public interface IOrderRepository
    {
        int FindNewID(DateTime day); //needs to be in interface or I can't call from service.

        List<Order> FindByOrderDate(DateTime day); //IEnumerable<Order> FindByDate(DateTime dt);
        Order Create(Order order, DateTime day); //Order Create(Order order);
        void Update(Order order);

        void Delete(List<Order> orders, Order orderToRemove, DateTime day); //bool Delete(DateTime dt, int id); // true order exists and it is deleted.
        

    }
}

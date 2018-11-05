using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using System.IO;

namespace Flooring.Data
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> GetFromFile(DateTime day)  //needed to add date as a parameter, otherwise it's always referencing a single day
        {

            List<Order> orders = new List<Order>();

            using (StreamReader sr = new StreamReader(FilePathCreator(day)))
            {
                sr.ReadLine(); //skip header
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Order newOrder = new Order();
                    string[] columns = line.Split(',');

                    newOrder.OrderNumber = int.Parse(columns[0]);
                    newOrder.CustomerName = columns[1];
                    newOrder.State = columns[2];
                    newOrder.TaxRate = decimal.Parse(columns[3]);
                    newOrder.ProductType = columns[4];
                    newOrder.Area = decimal.Parse(columns[5]);
                    newOrder.CostPerSQFt = decimal.Parse(columns[6]);
                    newOrder.LaborCostPerSQFt = decimal.Parse(columns[7]);
                    newOrder.MaterialCost = decimal.Parse(columns[8]); //If we set the props to read only, we wouldn't have to actually grab the cost from the file.
                    newOrder.LaborCost = decimal.Parse(columns[9]); // as they could be calculated from the data we do retrieve.
                    newOrder.Tax = decimal.Parse(columns[10]);
                    newOrder.Total = decimal.Parse(columns[11]);
                    newOrder.Date = DateTime.Parse(columns[12]);
                    orders.Add(newOrder);
                }
            }
            return orders;
        }

        public List<Order> FindByOrderDate(DateTime day)
        {

            string backToString = string.Format("{0:MMddyyyy}", day);
            string directoryStart = @"C:\Data\Flooring\Orders\";
            if (File.Exists(directoryStart + "Orders_" + backToString + ".txt"))
            {
                var orders = GetFromFile(day);
                return orders;
            }
            else
                return null;
        }

        public string FilePathCreator(DateTime day)
        {
            string backToString = string.Format("{0:MMddyyyy}", day);
            string directoryStart = @"C:\Data\Flooring\Orders\";
            return directoryStart + "Orders_" + backToString + ".txt";
        }

        public Order Create(Order order, DateTime day)
        {
            if (FindByOrderDate(day) == null) //if it cannot find the day we are looking for, create a new file
            {
                var newOrderList = new List<Order>();

                newOrderList.Add(order);

                List<string> lines = new List<string>();
                lines.Add($"OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total,Day");
                foreach (Order o in newOrderList)
                {
                    lines.Add($"{o.OrderNumber},{o.CustomerName},{o.State},{o.TaxRate},{o.ProductType},{o.Area}," +
                        $"{o.CostPerSQFt},{o.LaborCostPerSQFt},{o.MaterialCost},{o.LaborCost},{o.Tax},{o.Total},{o.Date}");
                }
                File.WriteAllLines(FilePathCreator(day), lines.ToArray());
                return order;
            }
            else //if it finds the day, we append.
            {
                var orders = FindByOrderDate(day);

                orders.Add(order);
                List<string> lines = new List<string>();
                lines.Add($"OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total,Day");
                foreach (Order o in orders)
                {
                    lines.Add($"{o.OrderNumber},{o.CustomerName},{o.State},{o.TaxRate},{o.ProductType},{o.Area}," +
                        $"{o.CostPerSQFt},{o.LaborCostPerSQFt},{o.MaterialCost},{o.LaborCost},{o.Tax},{o.Total},{o.Date}");
                }
                File.WriteAllLines(FilePathCreator(day), lines.ToArray());
                return order;
            }
        }

        public int FindNewID(DateTime day)
        {
            var orders = FindByOrderDate(day);
            if (orders == null)
            {
                return 1;
            }
            var nextOrderNum = 0;
            if (orders.Count >= 1)
            {
                var lastOrder = orders.Max(o => o.OrderNumber);
                nextOrderNum = lastOrder + 1;
            }
            else
            {
                nextOrderNum = 1;
            }

            return nextOrderNum;
        }

        public void Delete(List<Order> orders, Order orderToRemove, DateTime day)
        {
            Remove(orders, orderToRemove, day);
        }

        public void Remove(List<Order> orders, Order orderToRemove, DateTime day) //just need the date and ID.
        {
            var newOrderList = orders;

            newOrderList.Remove(orderToRemove);

            List<string> lines = new List<string>();
            lines.Add($"OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total,Day");
            foreach (Order o in newOrderList)
            {
                lines.Add($"{o.OrderNumber},{o.CustomerName},{o.State},{o.TaxRate},{o.ProductType},{o.Area}," +
                    $"{o.CostPerSQFt},{o.LaborCostPerSQFt},{o.MaterialCost},{o.LaborCost},{o.Tax},{o.Total},{o.Date}");
            }
            File.WriteAllLines(FilePathCreator(day), lines.ToArray());
        }

        public void Update(Order thisOrder) //SHOULD ONLY TAKE AN ORDER.
        {


            var thisOrderDate = thisOrder.Date;
            var orders = FindByOrderDate(thisOrderDate);
            for(int i=0; i <orders.Count; i++)
            {
                if(orders[i].OrderNumber == thisOrder.OrderNumber)
                {
                    orders[i] = thisOrder;
                }
            }

            List<string> lines = new List<string>();
            lines.Add($"OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total,Day");
            foreach (Order o in orders)
            {
                lines.Add($"{o.OrderNumber},{o.CustomerName},{o.State},{o.TaxRate},{o.ProductType},{o.Area}," +
                    $"{o.CostPerSQFt},{o.LaborCostPerSQFt},{o.MaterialCost},{o.LaborCost},{o.Tax},{o.Total},{o.Date}");
            }
            File.WriteAllLines(FilePathCreator(thisOrderDate), lines.ToArray());
        }
    }
}

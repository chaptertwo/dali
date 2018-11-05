using Flooring.Data;
using Flooring.Models;
using System;
using System.Linq;

namespace Flooring.Domain
{
    public class Service
    {
        public IOrderRepository orderRepo;
        public ITaxRepository taxRepo;
        public IProductRepository productRepo;


        public Service(IOrderRepository orderRepo, ITaxRepository taxRepo, IProductRepository productRepo) //can't operate without a repository
        {
            this.orderRepo = orderRepo; //whenever a service is instantiated, it will ask for the repositories, whatevery they provide will be saved in the service property
            this.taxRepo = taxRepo;
            this.productRepo = productRepo;
        }


        public OrderLookupResponse LookupOrders(string day)
        {
            var dateParsed = DateTime.Parse(day);

            OrderLookupResponse response = new OrderLookupResponse();

            response.OrderList = orderRepo.FindByOrderDate(dateParsed);

            if (response.OrderList == null)
            {
                response.Success = false;
                response.Message = $"\n!!{day} does not exist in our database. Please try again.";
            }
            else
            {
                response.Success = true;
                response.Message = "\nHere are the orders for that day."; //gotta test, convert to basic string and then format in controller.
            }
            return response;
        }

        public AddOrderResponse CreateOrder(string orderDate, string customerName, string abbr, string productID, decimal area)
        {
            AddOrderResponse addOrderResponse = new AddOrderResponse();
            Order newOrder = new Order();
            var dateOfOrder = DateTime.Parse(orderDate);
            if (dateOfOrder < DateTime.Now)
            {
                addOrderResponse.Message = "You can only use future dates for orders. If you need to edit older dates, use the edit option.";
                addOrderResponse.Success = false;
                return addOrderResponse;
            }

            var products = productRepo.GetProducts(); //-------------------------check product input
            Product product = null;
            var productsTrue = products.Any(p => p.ID == productID);
            if (productsTrue == true)
            {
                product = productRepo.GetProductByID(productID);
            }
            else
            {
                addOrderResponse.Success = false;
                addOrderResponse.Message = "The product ID you entered does not exist. Please try again.";
                return addOrderResponse;
            }

            var taxes = taxRepo.ReadTaxInfo(); //--------------------------------check tax input
            Tax tax = null;
            var taxesTrue = taxes.Any(t => t.Abbreviation == abbr);
            if (taxesTrue == true)
            {
                tax = taxRepo.GetTaxesByAbbr(abbr);
            }
            else
            {
                addOrderResponse.Success = false;
                addOrderResponse.Message = "We do not sell to this state currently. If you mistyped, please try again.";
                return addOrderResponse;
            }

            newOrder.CustomerName = customerName;
            newOrder.Date = dateOfOrder;
            newOrder.OrderNumber = orderRepo.FindNewID(dateOfOrder);
            newOrder.Area = area;

            var newCalculatedOrder = CalculateCosts(newOrder, product, tax);

            addOrderResponse.order = orderRepo.Create(newCalculatedOrder, newCalculatedOrder.Date);
            if (addOrderResponse.order == null)
            {
                addOrderResponse.Success = false;
                addOrderResponse.Message = $"We couldn't complete your order as described. Please try again.";

            }
            else
            {
                addOrderResponse.Success = true;
                addOrderResponse.Message = "\nHere is your new order";
                addOrderResponse.order = newCalculatedOrder;
            }
            return addOrderResponse;
        }


        public Order CalculateCosts(Order order, Product product, Tax state)//, decimal area, decimal costPerSQFoot, decimal laborCostPerSQFoot, DateTime date, int orderNumber) 
        {
            Order currentOrder = order;
            decimal area = currentOrder.Area; //may need to pass area in

            decimal materialCost = area * product.CostPerSQFoot;
            decimal laborCost = area * product.LaborCostPerSQFoot;
            decimal taxRate = state.TaxRate;
            decimal tax = (materialCost + laborCost) * (taxRate / 100);

            currentOrder.State = state.Abbreviation;
            currentOrder.TaxRate = taxRate;
            currentOrder.ProductType = product.ProductType;
            currentOrder.Area = order.Area;
            currentOrder.CostPerSQFt = product.CostPerSQFoot;
            currentOrder.LaborCostPerSQFt = product.LaborCostPerSQFoot;
            currentOrder.MaterialCost = product.CostPerSQFoot * area;
            currentOrder.LaborCost = product.LaborCostPerSQFoot * area;
            currentOrder.Tax = (materialCost + laborCost) * (taxRate / 100);
            currentOrder.Total = (materialCost + laborCost + tax);

            return currentOrder;
        }

        public EditOrderResponse EditOrder(DateTime date, int orderNumber, string customerName, string abbr, string productName, string area) //what part are they editing? probably params for each
        {
            EditOrderResponse response = new EditOrderResponse();
            var orders = orderRepo.FindByOrderDate(date);
            if(orders == null)
            {
                response.Success = false;
                response.Message = "This order does not exist. Please try again.";
                return response;
            }
            var doesOrderExist = orders.Any(o => o.OrderNumber == orderNumber);
            if (doesOrderExist == true)
            {        //thisOrder = o
                var o = orders.FirstOrDefault(od => od.OrderNumber == orderNumber);
                if (o.OrderNumber == orderNumber)
                {
                    Order specific = orders.SingleOrDefault(x => x.OrderNumber == orderNumber);
                    if (productName != "")
                    {
                        var isExistingProduct = productRepo.GetProducts().Any(p => p.ID == productName);
                        if (isExistingProduct != true)
                        {
                            response.Success = false;
                            response.Message = "We don't currently carry a product by that ID, please try again.";
                            return response;
                        }
                    }
                    var actualProduct = productRepo.GetProducts().FirstOrDefault(p => p.ProductType == specific.ProductType);


                    if (customerName != "")
                    {
                        specific.CustomerName = customerName;
                    }
                    if (area != "")
                    {
                        specific.Area = decimal.Parse(area);

                        if(decimal.Parse(area) < 0)
                        {
                            response.Success = false;
                            response.Message = "Area values must be a positive integer.";
                            return response;
                        }
                        else if (decimal.Parse(area) == 0)
                        {
                            response.Success = false;
                            response.Message = "0 is the same as no changes";
                            return response;
                        }
                    }
                    
                    if (abbr != "")
                    {
                        specific.State = abbr;
                    }
                    var actualTax = taxRepo.ReadTaxInfo().FirstOrDefault(t => t.Abbreviation == specific.State);
                    //orderRepo.Delete(orders, specific, date); //================================REMOVE AT SOME POINT after next checks.
                    specific = CalculateCosts(specific, actualProduct, actualTax);//update method would be after this.
                    orderRepo.Update(specific);

                    //response.Order = orderRepo.Create(specific, specific.Date);

                    response.Success = true;
                    response.Message = $"Order Number:{o.OrderNumber} has been updated.";
                    response.Order = specific;
                    return response;
                }
            }
            else

                response.Success = false;
            response.Message = "This order does not exist. Please try again.";
            return response;
        }

        public RemoveOrderResponse RemoveOrder(DateTime date, int orderNumber) //don't have to return a list or order, they can choose to view it again from the other options.
        {
            RemoveOrderResponse response = new RemoveOrderResponse();
            var orders = orderRepo.FindByOrderDate(date);
            foreach (Order o in orders)
            {
                if (o.OrderNumber == orderNumber)
                {
                    Order specific = orders.SingleOrDefault(x => x.OrderNumber == orderNumber);
                    orderRepo.Delete(orders, specific, date);
                    response.Success = true;
                    response.Message = $"Order Number:{o.OrderNumber} has been removed.";
                    return response;
                }
                else
                {
                    continue;
                }
            }
            response.Success = false;
            response.Message = "This order does not exist. Please try again.";
            return response;
        }


    }
}

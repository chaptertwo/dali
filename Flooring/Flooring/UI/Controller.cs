using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Domain;

namespace Flooring.UI
{
    public class Controller
    {
        private Service service;

        public Controller(Service service)
        {
            this.service = service;
        }
        DisplayAndFormats display = new DisplayAndFormats();

        internal void Run()
        {
            DisplayMenu();
        }

        private void DisplayMenu()
        {
            display.MainMenu();
            
            int selection = GetMenuSelection();
            do
            {
                switch (selection)
                {
                    case 1:
                        DisplayOrders();
                        break;
                    case 2:
                        AddAnOrder();
                        break;
                    case 3:
                        EditAnOrder();
                        break;
                    case 4:
                        RemoveAnOrder();
                        break;
                    case 5:
                        Quit();
                        break;

                }
            } while (selection > 1 && selection < 5);
        }

        private void Quit() //could have the while loop go to only 4 and then it would exit.
        {
            Console.WriteLine("Have a productive day!");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private void RemoveAnOrder()
        {
            Console.WriteLine("\nBe prepared to have the order of the date and the order number.");
            DateTime date = CheckDateInputFormatDateTime("\nPlease enter the date of your order.");
            int orderNumber = ReadInt("Please enter the order number.");
            string dateString = date.ToShortDateString();
            bool shouldRemove = false;
            var response = service.LookupOrders(dateString); 
            if (response.Success == false)
            {
                MakeRED("A record for that date doesn't exist. Please try again.");
                Console.ReadKey();
                DisplayMenu();
            }
            var orders = response.OrderList;
            foreach(var o in orders)
            {
                if(o.OrderNumber == orderNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    PrintOrder(o);
                    MakeRED("Delete this order? True/False");
                    shouldRemove = bool.Parse(Console.ReadLine());
                }
            }
            if(shouldRemove == true)
            {
                var successResponse = service.RemoveOrder(date, orderNumber);
                if (successResponse.Success == true)
                {
                    string message = successResponse.Message;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(message);
                    Console.ResetColor();
                    Console.ReadKey();
                    DisplayMenu();
                }
            }
            else
            {
                MakeRED("That order doesn't exist. Please try again.");
            }
        }

        private void EditAnOrder()
        {
            var date = CheckDateInputFormat("Please Enter the date of the order you would like to edit.");
            var orderNumber = ReadInt("Please enter the order number.");
            var dateFromString = DateTime.Parse(date);

            var check = service.LookupOrders(date);
            if (check.Success == true)
            {
                var returnedList = check.OrderList;
                var doesOrderExist = returnedList.Any(o => o.OrderNumber == orderNumber);
                if(doesOrderExist == true)
                {
                    PrintOrder(returnedList.FirstOrDefault(o => o.OrderNumber == orderNumber));
                    var thisOrder = returnedList.FirstOrDefault(o => o.OrderNumber == orderNumber);
                    Console.WriteLine();
                    AvailableProducts();
                    var productName = ReadRequiredStringProductListEdit("Would you like to edit the product type? Please enter the ID of the product you would like, or press enter to skip.");
                    if(productName != "")
                    {
                        var isExistingProduct = service.productRepo.GetProducts().Any(p => p.ID == productName);
                        if (isExistingProduct != true)
                        {
                            MakeRED("We don't currently stock that material. Please try again.");
                            Console.ReadKey();
                            DisplayMenu();
                        }
                    }
                    var actualProduct = service.productRepo.GetProducts().FirstOrDefault(p => p.ID == productName);
                    var customerName = ReadRequiredStringEditCompany("\nWould you like to edit the customer name? Please enter the new value or press enter to skip.");
                    if(customerName == "")
                    {
                        customerName = thisOrder.CustomerName;
                    }
                    MakeRED("\nBe warned: changes to this value will readjust your order price.");
                    var area = ReadRequiredStringEditCompany("Would you like to change the area value? Enter the new value or press enter to skip.");
                    
                    if(area == "")
                    {
                        area = thisOrder.Area.ToString();
                    }
                    MakeRED("\nBe warned: changes to this value will readjust your order price."); 
                    var abbr = ReadRequiredStringAbbrEdit("Would you like the change the state associated with this customer? Enter thet new value or press enter to skip.");
                    if(abbr != "")
                    {
                        var doesStateExist = service.taxRepo.ReadTaxInfo().Any(t => t.Abbreviation == abbr);
                        if (doesStateExist != true)
                        {
                            MakeRED("We currently don't supply to that area. Please try again.");
                            Console.ReadKey();
                            DisplayMenu();
                        } 
                    }
                    abbr = thisOrder.State;
                    if (customerName == "" && abbr == "" && productName == "" && area == "")
                    {
                        MakeRED("You haven't made any changes... operation aborted.");
                        Console.ReadKey();
                        DisplayMenu();
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nYour fields will be updated to this: {customerName} | {abbr} | {productName} | {area}");
                    Console.ResetColor();
                    var doProceed = ReadBool("Would you like to proceed? (True/False)");
                    if(doProceed == false)
                    {
                        MakeRED("Operation cancelled per your request.");
                        Console.ReadKey();
                        DisplayMenu();
                    }
                    var editResponse = service.EditOrder(dateFromString, orderNumber, customerName, abbr, productName, area.ToString());
                    if (editResponse.Success == true)
                    {
                        var message = editResponse.Message;
                        Console.WriteLine(message);
                        PrintOrder(editResponse.Order);
                        Console.ReadKey();
                        DisplayMenu();
                    }
                    else
                    {
                        MakeRED("An error has occured. Please try again.");
                        Console.ReadKey();
                        DisplayMenu();
                    }
                }
                else
                {
                    MakeRED("That order doesn't exist. Please try again.");
                }
                
            }
            else
            {
                MakeRED("That date/order combination doesn't exist");
                Console.ReadKey();
            }
        }

        private void AddAnOrder() 
        {
            Console.WriteLine("Please have your information prepared."); 
            var date = CheckDateInputFormatNoPast("Please enter the date for your order: "); 
            var customerName = ReadRequiredStringCompany("Please enter the name of the customer or company: "); 
            var state = ReadRequiredStringAbbr("Please enter the state abbreviation for this customer's location: "); 
            AvailableProducts();
            var productType = ReadRequiredString("\nPlease enter a product ID: ");
            var area = ReadAndFormatDecimal("Please enter the area for your order: ");
            MakeGreen($"{date}  {customerName}  {state}  {productType}  {Math.Round(area, 3)}");
            bool summary = ReadBool($"Verify this information is correct: (true/false) ");
            
            if (summary == true)
            {
                var addResponse = service.CreateOrder(date, customerName, state, productType, area);
                if (addResponse.Success == true)
                {
                    var message = addResponse.Message;

                    MakeGreen(message);
                    PrintOrder(addResponse.order);
                    Console.ReadKey();
                    DisplayMenu();
                }
                else
                {
                    var message = addResponse.Message;
                    MakeRED(message);
                    Console.ReadKey();
                    DisplayMenu();
                }
            }
            else if(summary == false)
            {
                MakeRED("Your order entry has been cancelled per your request.");
                Console.ReadKey();
                DisplayMenu();
            }

        }

        private void DisplayOrders()
        {
            var date = CheckDateInputFormat("\nWhat is the Date you're searching for? ");
            var response = service.LookupOrders(date);
            string message = response.Message;
            var orders = response.OrderList;
            
            if (response.Success == false)
            {

                MakeRED(message);
                Console.ReadKey();
                DisplayMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (var order in orders) 
                {
                    PrintOrder(order);
                }
                Console.ResetColor();
            }
            Console.ReadKey();
            DisplayMenu();
        }

        private int GetMenuSelection()
        {
            int selection = ReadIntInRangeMenu("\nWhat would you like to do?  ");
            return selection;
        }


//----------------------------------------------------------------Check Methods

        public string ReadRequiredString(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(result))
                {
                    Console.Clear();
                    MakeRED("\nPlease enter a correct input.");
                }
            } while (string.IsNullOrWhiteSpace(result));
            result = result.Trim();
            return result;
        }

        public string ReadRequiredStringProductList(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
                var productList = service.productRepo.GetProducts();
                var doesListContain = productList.Where(p => p.ID == result);
                if (string.IsNullOrWhiteSpace(result))
                {
                    Console.Clear();
                    MakeRED("\nPlease enter a correct input.");
                }
            } while (string.IsNullOrWhiteSpace(result));
            result = result.Trim();
            return result;
        }

        public string ReadRequiredStringProductListEdit(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
                var productList = service.productRepo.GetProducts();
                var doesListContain = productList.Where(p => p.ID == result);
                if (string.IsNullOrWhiteSpace(result))
                {
                    break;
                }
            } while (string.IsNullOrWhiteSpace(result));
            result = result.Trim();
            return result;
        }

        public string ReadRequiredStringCompany(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(result) || result.Contains(','))
                {
                    Console.Clear();
                    MakeRED("\nPlease enter a correct input.");
                }
            } while (string.IsNullOrWhiteSpace(result) || result.Contains(','));
            result = result.Trim();
            return result;
        }

        public string ReadRequiredStringEditCompany(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();

                if (result.Contains(','))
                {
                    Console.Clear();
                    MakeRED("\nPlease enter a correct input.");
                }
            } while (result.Contains(','));
            result = result.Trim();
            return result;
        }

        public string ReadRequiredStringAbbr(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(result) || result.Length != 2)
                {
                    Console.Clear();
                    MakeRED("\nPlease enter a correct input.");
                }
            } while (string.IsNullOrWhiteSpace(result) || result.Length != 2);
            result = result.Trim();
            return result;
        }

        public string ReadRequiredStringAbbrEdit(string prompt)
        {
            bool necessary = false;
            string result = "sure";
            while(necessary == false)
            {
                Console.Write(prompt);
                result = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(result))
                {
                    necessary = true;
                    break;
                }
                if (result.Length != 2)
                {
                    MakeRED("That is not a valid state abbreviation, try again.");
                    Console.ReadKey();
                    continue;
                }
                if (result.Length == 2)
                {
                    necessary = true;
                    return result;
                }
            } 
            return result;
        }

        public int ReadIntInRangeMenu(string prompt)
        {
            int result = 0;
            int validNumber = 0;
            do
            {
                string input = ReadRequiredString(prompt);
                if (!int.TryParse(input, out validNumber) || validNumber < 1 || validNumber > 5)
                {
                    MakeRED("\nPlease enter a number between 1-5.");
                }
                else
                {
                    result = validNumber;
                }
            } while (validNumber < 1 || validNumber > 5 || validNumber == 0);
            return result;
        }

        public string CheckDateInputFormat(string prompt)
        {
            string result = "";
            DateTime validDatetime = new DateTime(1989, 12, 09);
            var currentDate = DateTime.Now;
            bool necessary = false;
            do
            {
                string input = ReadRequiredString(prompt);
                if (!DateTime.TryParse(input, out validDatetime))
                {
                    MakeRED("Please use a propper date format. ex: 12/31/1999");
                }
                else
                {
                    result = validDatetime.ToShortDateString();
                    necessary = true;
                }
            } while (necessary == false);
            return result;
        }

        public string CheckDateInputFormatNoPast(string prompt)
        {
            string result = "";
            DateTime validDatetime = new DateTime(1989, 12, 09);
            var currentDate = DateTime.Now;
            bool necessary = false;
            do
            {
                string input = ReadRequiredString(prompt);
                if (!DateTime.TryParse(input, out validDatetime))
                {
                    MakeRED("Please use a propper date format. ex: 12/31/1999");
                }
                else if (validDatetime < currentDate)
                {
                    MakeRED("You can only add orders for future dates. If you have beef with an old order, choose the edit option from the menu.");
                }
                else
                {
                    result = validDatetime.ToShortDateString();
                    necessary = true;
                }
            } while (necessary == false);
            return result;
        }

        public DateTime CheckDateInputFormatDateTime(string prompt)
        {
            DateTime result = new DateTime(1989, 12, 09);
            DateTime validDatetime = new DateTime(1989, 12, 09);
            bool necessary = false;
            do
            {
                string input = ReadRequiredString(prompt);
                if (!DateTime.TryParse(input, out validDatetime))
                {
                    MakeRED("Please user a propper date format. ex: 12/31/1999");
                }
                else
                {
                    result = validDatetime;
                    necessary = true;
                }
            } while (necessary == false);
            return result;
        }

        public int ReadInt(string prompt)
        {
            int result = 0;
            int validNumber = 0;
            bool necessary = false;
            do
            {
                string input = ReadRequiredString(prompt);
                if (!int.TryParse(input, out validNumber))
                {
                    MakeRED("\nPlease enter a number a valid number.");
                }
                else
                {
                    result = validNumber;
                    necessary = true;
                }
            } while (necessary == false);
            return result;
        }

        public decimal ReadAndFormatDecimal(string prompt)
        {
            bool necessary = false;
            decimal result = 0;
            decimal validNumber = 0;
            do
            {
                string input = ReadRequiredString(prompt);
                if (!decimal.TryParse(input, out validNumber))
                {
                    MakeRED("\nPlease enter a valid number");
                }
                else if(validNumber < 0)
                {
                    MakeRED("\nYour number must be positive.");
                }
                else
                {
                    result = validNumber;
                    necessary = true;
                }
            } while (necessary == false);
            return result;
        }



        public decimal ReadAndFormatDecimalEdit(string prompt)
        {
            bool necessary = false;
            decimal result = 0;
            decimal validNumber = 0;
            while (necessary == false)
            {
                string input = ReadRequiredStringEditCompany(prompt);
                if(string.IsNullOrWhiteSpace(input))
                {
                    necessary = true;
                    break;
                }
                else if (!decimal.TryParse(input, out validNumber))
                {
                    MakeRED("\nPlease enter a valid number");
                }
                else if (validNumber < 0)
                {
                    MakeRED("\nYour number muts be positive.");
                }
                else
                {
                    result = validNumber;
                    necessary = true;
                }
            } 
            return result;
        }
        public bool ReadBool(string prompt)
        {
            bool necessary = false;
            bool result = false;
            while (necessary == false)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (input == "true" || input == "True" || input == "yes" || input == "YES")
                {
                    result = true;
                    necessary = true;
                    return result;
                }
                else
                {
                    result = false;
                    necessary = true;
                    return result;
                }
            } return result;
        }

        public void MakeRED(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(prompt);
            Console.ResetColor();
        }

        public void MakeGreen(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(prompt);
            Console.ResetColor();
        }

        public void AvailableProducts()
        {
            var products = service.productRepo.GetProducts();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nID \t Material \t CostPerSquareFoot \t LaborCostPerSquareFoot  ");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.ID} \t {p.ProductType} \t\t {p.CostPerSQFoot} \t\t {p.LaborCostPerSQFoot}");
            }
            Console.ResetColor();
        }

        public void PrintOrder(Order order)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($@"****************************************************************** 
[{order.OrderNumber}] | [{order.Date.ToShortDateString()}] 
[{order.CustomerName}] 
[{order.State}] 
Product : [{order.ProductType}] 
Materials: [{Math.Round(order.MaterialCost, 3)}] 
Labor: [{Math.Round(order.LaborCost, 3)}] 
Tax: [{Math.Round(order.Tax, 3)}] 
Total: [{Math.Round(order.Total, 3)}] 
*******************************************************************");
            Console.ResetColor();
        }
    }
}

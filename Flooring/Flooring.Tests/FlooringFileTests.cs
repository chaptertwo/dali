using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models;
using Flooring.Domain;
using Flooring.UI;
using NUnit.Framework;
using System.IO;

namespace Flooring.Tests
{
    [TestFixture]
    public class FlooringFileTests
    {

        //====================================================MUST BE IN TEST MODE TO RUN ALL TESTS SUCCESSFULLY=====================================
        Service service = ServiceFactory.Create();
        public IOrderRepository orderRepo;
        public ITaxRepository taxRepo;
        public IProductRepository productRepo;


        private const string filePath = @"C:\Data\Flooring\TestOrders\Orders_09122018.txt";
        private const string originalFile = @"C:\Data\Flooring\TestOrders\Orders_09122018Seed.txt";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.Copy(originalFile, filePath);
        }

        [Test]
        public void CanFindOrder()
        {
            Service service = ServiceFactory.Create(); //will look at app settings and returns a new servicemanager (taking in all the repos)

            OrderLookupResponse response = service.LookupOrders("06/01/2013");

            Assert.IsNotNull(response.OrderList);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void CanAddOrder()
        {
            Service service = ServiceFactory.Create();

            AddOrderResponse response = service.CreateOrder("09/12/2018", "WWT", "OH", "2", 3.75M);
            OrderLookupResponse dateResponse = service.LookupOrders("09/12/2018");

            Assert.IsTrue(dateResponse.Success); //debugged this forever, changing code frequently. green light = best feeling ever.
        }

        [Test]
        public void CanMakeID()
        {
            Service service = ServiceFactory.Create();
            DateTime test = new DateTime(2013, 06, 01);
            var newID = service.orderRepo.FindNewID(test);

            Assert.IsTrue(newID == 2);
            Assert.IsFalse(newID == 1);
        }

        [Test]
        public void CanSelectProduct()
        {
            Service service = ServiceFactory.Create();

            var correctProduct = service.productRepo.GetProductByID("2");

            Assert.AreEqual(correctProduct.ProductType, "Laminate");
            Assert.AreEqual(correctProduct.CostPerSQFoot, 1.75m);
            Assert.AreNotEqual(correctProduct.ProductType, "Stuff");
        }

        [Test]
        public void CanRemoveOrder()
        {
            Service service = ServiceFactory.Create();
            RemoveOrderResponse removeResponse = new RemoveOrderResponse();
            DateTime day = new DateTime(2018, 9, 12);
            removeResponse = service.RemoveOrder(day, 2);

            var orders = service.orderRepo.FindByOrderDate(day);

            Assert.IsTrue(removeResponse.Success);
            Assert.IsTrue(orders.Count == 3); //seed file will ensure that it starts at 4 each time.
        }

        [Test]
        public void CanUpdate() 
        {
            Service service = ServiceFactory.Create();
            EditOrderResponse editResponse = new EditOrderResponse();
            DateTime day = new DateTime(2018, 9, 12);
            editResponse = service.EditOrder(day, 4, "WWTC2", "OH", "3", "5.52");
            var orders = service.orderRepo.FindByOrderDate(day);
            var specific = orders.SingleOrDefault(x => x.OrderNumber == 4);

            Assert.IsTrue(editResponse.Success);
            Assert.IsTrue(specific.State == "OH");
        }


        [TestCase("09/12/18", 3, "WWTC", "OH", "1", "670", true)]
        [TestCase("09/12/18", 3, "", "", "", "-1", false)]//area must be positive
        [TestCase("09/12/18", 3, "", "", "6", "", false)] //this isn't a valid product atm
        [TestCase("09/12/18", 3, "WWTC", "", "", "", true)]
        [TestCase("09/12/18", 3, "", "OH", "", "", true)]
        [TestCase("09/12/18", 3, "", "", "1", "", true)]
        [TestCase("09/12/18", 3, "", "", "", "670", true)]
        [TestCase("09/13/18", 3, "WWTC", "OH", "1", "670", false)] //this file shouldn't exist.
        public void ShootingBlanksEdit(string date, int orderNumber, string customerName, string abbr, string productName, string area, bool expected)
        {

            DateTime realDate = DateTime.Parse(date);
            var actual = service.EditOrder(realDate, orderNumber, customerName, abbr, productName, area);

            Assert.AreEqual(expected, actual.Success);
        }

        [TestCase("09/14/19", "WWTC3", "OH", "2", 839, true)]
        [TestCase("09/14/17", "WWTC3", "OH", "2", 839, false)]
        public void CantAddOldDates(string orderDate, string customerName, string abbr, string productID, decimal area, bool expected)
        {

            var actual = service.CreateOrder(orderDate, customerName, abbr, productID, area);
            Assert.AreEqual(expected, actual.Success);

        }



    }

}

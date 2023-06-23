using AspNetCore.Reporting;
using BookDetails.Models;
using BookDetails.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookDetails.Controllers
{
    public class ReportsController : Controller
    {
        private PublisherDbContext db;
        private IWebHostEnvironment env;
       
        public ReportsController(PublisherDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); 
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllOrders()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this.env.WebRootPath}\\Reports\\Report3.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("rp1", "ASP.NET CORE RDLC Report");
            //get products from product table 
            var data = db.Orders
               .Select(c =>
              new OrderViewModel
              {
                  OrderId = c.OrderId,
                  CustomerName = c.CustomerName,
                  OrderDate = c.OrderDate,
                  Price = c.Price,
                  DiscountRate = c.Discount,
                  Quantity = c.Quantity,
                  TotalPrice = Math.Floor(c.Price * c.Quantity),
                  DiscountAmount = Math.Floor(c.Price * c.Quantity) * (c.Discount / 100),
                  NetPay = Math.Floor((c.Price * c.Quantity) - (c.Price * c.Quantity) * (c.Discount / 100)),
                  BookTitle = c.Book == null ? "" : c.Book.Title

              })
               .ToList();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", data);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }
        public IActionResult SingleOrder(int id )
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this.env.WebRootPath}\\Reports\\Report1.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("id", "ASP.NET CORE RDLC Report");
            //get products from product table 
            var data = db.Orders.FirstOrDefault(o=>o.OrderId == id);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet2", data);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
         }
    }
}

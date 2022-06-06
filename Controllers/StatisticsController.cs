using GestionDeStockMagasin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionDeStockMagasin.Data;
using GestionDeStockMagasin.Models;


namespace GestionDeStockMagasin.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly AppDbContext _db;
        private DateTime date_to_look_for1;
        private DateTime date_to_look_for2;
        

        public StatisticsController(AppDbContext db)
        {
            _db = db;
        }
         [NonAction]
        public static DateTime ChangeTimee( DateTime dateTime, int hours, int minutes, int seconds = default, int milliseconds = default)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hours, minutes, seconds, milliseconds, dateTime.Kind);
        }

        public IActionResult Index(DateTime date_parameter)
        {
            //pour verifier si c'est l'utilisateur posede le droit d'entrer avec son role 
            RoleController1 verificateur = new RoleController1();
            if (verificateur.role("Statistics", HttpContext.Session.GetString("Role")) != null)
            {
                TempData["error"] = "you are not allowed";
                return verificateur.role("Statistics", HttpContext.Session.GetString("Role"));
            }



            //there is a default date is passed when there is no date 
            string dateString = "1 / 1 / 0001 12:00:00 AM";
            DateTime date_default = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);

            if (date_parameter == date_default)
            {
                 date_to_look_for1 = DateTime.Now.Date;
            }
            else
            {
                 date_to_look_for1 = date_parameter.Date;
            }

            date_to_look_for2= ChangeTimee(date_to_look_for1,11, 59, 59);

            //this to tak all the product in this day 
             IEnumerable<Facture> all_proucts = _db.Facture.Where(x => x.DateStock >= date_to_look_for1 && x.DateStock <= date_to_look_for2);//all the products 
           // IEnumerable<Product> list_product = _db.Products.Where(x => x.ProductId ==all_proucts.all_proucts.ElementAt(i).ProductId);//all the products 

            //now I have to count for each product the + of all its occurance 

           // IList<Int32> list_ids = new List<Int32>();
            IList<Product> list_product = new List<Product>();
            IList<Statistic> list_final = new List<Statistic>();
            for (int i = 0; i < all_proucts.Count(); i++)
            {
                 //list_ids.Add(all_proucts.ElementAt(i).ProductId);
                // list_product.Add((Product)_db.Products.Where(x => x.ProductId == all_proucts.ElementAt(i).ProductId));

                list_product.Add((Product)_db.Products.Find(all_proucts.ElementAt(i).ProductId));//this will have all the product names in the same order in the product of to day 

                list_final.Add(new Statistic(list_product.ElementAt(i).Nom, all_proucts.ElementAt(i).Qte));

            }

         





            return View(list_final);
        }
       
    }
}

using Microsoft.AspNetCore.Mvc;
using GestionDeStockMagasin.Data;
using GestionDeStockMagasin.Models;

namespace GestionDeStockMagasin.Controllers
{
    public class RoleController1 : Controller
    {

       

      
        public IActionResult role(string page,string role)
        {
            //so now we want to know wish role  we have 
           

            if (role == "Admin")
            {
                //il peut acceder tout les pages   EXP  { "Personnel_Create" ,"Category_Create"};
                return null;
            }
            else if (role == "Cashier")
            {
                //this is the pages that he is not allowed to enter  : la forme {"controllername}
                string[] pages = {"Personnels","Category", "Statistics","Alerts" };
                bool exists = Array.Exists(pages, element => element == page);//to verify if this controller is allowed or not 
                if (exists)
                {
                    //he is not allowed than he should be redireced 
                   
                    return RedirectToAction("Index", "Home");//we want to redirect from  this function 
                }
            }
            else if(role == "Delivery Man")
            {
                string[] pages = { "Personnels", "Category","Statistics" , "Alerts" };
                bool exists = Array.Exists(pages, element => element == page);//to verify if this controller is allowed or not 
                if (exists)
                {
                    //he is not allowed than he should be redireced 
                
                    return RedirectToAction("Index", "Home");
                }
            }

            return null; 


        }
    }
}

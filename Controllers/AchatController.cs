using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestionDeStockMagasin.Data;
using GestionDeStockMagasin.Models;
using GestionDeStockMagasin.ViewModels;
using static Microsoft.Extensions.Primitives.StringValues;

namespace GestionDeStockMagasin.Controllers
{
    public class AchatController : Controller
    {
        private readonly AppDbContext _db;

       static List<Facture> Objects = new List<Facture>();
        static int id_command = 1 ;
        

        public AchatController(AppDbContext db)
        {
            _db = db ;
        }
        

    
       //Get
        public IActionResult Index()
        {
            AchatIndexViewModel viewModel = new AchatIndexViewModel()
            {
                facturesTemp =  Objects,
            };
            
            return View(viewModel);
        }
        //get 
        public IActionResult Create()
        {
            AchatIndexViewModel viewModel = new AchatIndexViewModel()
            {
                clients = _db.Client.ToList(),
                products = _db.Products.ToList()
            };

            return View(viewModel);
        }
        //post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AchatIndexViewModel obj)
        {
            if(obj.ClientId == 0 || obj.ProductId==0 || obj.Qte == 0 ){
                TempData["error"] = "Veuillez remplir tous les cases ";
                RedirectToAction("Index");
            }else{
                int prix_unite = _db.Products.Where(p => p.ProductId == obj.ProductId).Select(p => p.prixUnite).SingleOrDefault();
                var facture = new Facture();
                facture.ClientId = obj.ClientId;
                facture.ProductId = obj.ProductId;
                facture.Qte = obj.Qte;
                facture.Prix_total = obj.Qte * prix_unite ;
                facture.livrer = false;

                // get the last command id 
                //int commandId = _db.Facture.OrderByDescending(p => p.CommandId)
                //          .Select(p => p.CommandId ).FirstOrDefault();
                facture.CommandId = id_command;
                _db.Facture.Add(facture);
                
                Objects.Add(facture);

                
                
                
                
                /*
                _db.Facture.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Product added successfully ";
                return RedirectToAction("Index");
                */
                
                
            }
            return RedirectToAction("Index");
            
        }
        

        public IActionResult Generate(){
            // decrease the quantity of products 
            
            foreach(var item in Objects ){
                var ProductFromDb = _db.Products.Where(p => p.ProductId == item.ProductId).Select(p => p).SingleOrDefault();
                ProductFromDb.QteStock -= item.Qte;
                if(ProductFromDb.QteStock<0){
                    TempData["error"] = "Il faut remplir le stock ";
                    RedirectToAction("Create");
                }
                 _db.Products.Update(ProductFromDb);
                 
            }
            _db.Facture.AddRange(Objects);
            Objects.Clear();
            id_command++;
            _db.SaveChanges();
            // Save Objects in Facture 
            

            // Generate pdf file for facture 
          return RedirectToAction("Create");

            
        }
    }
}
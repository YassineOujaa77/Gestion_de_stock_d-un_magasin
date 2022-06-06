using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionDeStockMagasin.Data;
using GestionDeStockMagasin.Models;

namespace GestionDeStockMagasin.Controllers
{
    public class ClientController : Controller
    {
        private readonly AppDbContext _db;

       

        public ClientController(AppDbContext db)
        {
            _db = db ;
        }

        public IActionResult Index()
        {
            
            IEnumerable<Client> objClientList = _db.Client;
            return View(objClientList);
        }

        public IActionResult Create()
        {
            //IEnumerable<Category> objCategoryList = _db.Categories ;
            return View();
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Create(Client obj)
        {
            //if(ModelState.IsValid){
                _db.Client.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Client added successfully ";
                return RedirectToAction("Index");
           //}
            
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

            var ProductFromDb = _db.Client.Find(id);

            if(ProductFromDb == null){
                return NotFound();
            }

            return View(ProductFromDb);
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Edit(Client obj)
        {
            
            //if(ModelState.IsValid){
                _db.Client.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Client updated successfully ";
                return RedirectToAction("Index");
            //}
            //return View(obj);
        }


        // Get 
        public IActionResult Delete(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

            var clientFromDb = _db.Client.Find(id);

            if(clientFromDb == null){
                return NotFound();
            }

            return View(clientFromDb);
        }
        
        //Post 
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
         public IActionResult DeletePost(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

            var clientFromDb = _db.Client.Find(id);

            if(clientFromDb == null){
                return NotFound();
            }
            
            _db.Client.Remove(clientFromDb);
            _db.SaveChanges();
            TempData["success"] = "Client Deleted successfully ";
            return RedirectToAction("Index");
            
            
        }
    }
}
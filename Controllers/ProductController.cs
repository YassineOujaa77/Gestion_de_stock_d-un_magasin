using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionDeStockMagasin.Data;
using GestionDeStockMagasin.Models;

namespace GestionDeStockMagasin.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db ;
        }

        
        public IActionResult Index()
        {
            Product model = new Product();
            IEnumerable<Product> objProductList = _db.Products;
            IEnumerable<Product> objProductListAlert = _db.Products.Where(p => p.QteStock <= 2  ).ToList();
            if(objProductListAlert.Count()!=0){
                 TempData["error"] = "Il faut remplir le stock pour des produits ";
            }
            return View(objProductList);
        }

        // GET
        public IActionResult Create()
        {
            //IEnumerable<Category> objCategoryList = _db.Categories ;
            return View();
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Create(Product obj)
        {
            //if(ModelState.IsValid){
                var results = _db.Products.Where(p => p.Nom.ToLower() == obj.Nom.ToLower() ).Select(p => p.Nom).SingleOrDefault();
                if(results!=null){
                    TempData["error"] = "Ce Produit existe déjà ";
                    return View(obj);
                }else{
                    _db.Products.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Product added successfully ";
                    return RedirectToAction("Index");
                }
                
           // }
            
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

            var ProductFromDb = _db.Products.Find(id);

            if(ProductFromDb == null){
                return NotFound();
            }

            return View(ProductFromDb);
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Edit(Product obj)
        {
            
            //if(ModelState.IsValid){
                
                _db.Products.Update(obj);
                
                _db.SaveChanges();
                TempData["success"] = "Product updated successfully ";
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

            var ProductFromDb = _db.Products.Find(id);

            if(ProductFromDb == null){
                return NotFound();
            }

            return View(ProductFromDb);
        }
        
        //Post 
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
         public IActionResult DeletePost(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

            var ProductFromDb = _db.Products.Find(id);

            if(ProductFromDb == null){
                return NotFound();
            }
            
            _db.Products.Remove(ProductFromDb);
            _db.SaveChanges();
            TempData["success"] = "Product Deleted successfully ";
            return RedirectToAction("Index");
            
            
        }

        //GET 
        public IActionResult Alert(){
            //pour verifier si c'est l'utilisateur posede le droit d'entrer avec son role 
            RoleController1 verificateur = new RoleController1();
            if (verificateur.role("Alerts", HttpContext.Session.GetString("Role")) != null)
            {
                TempData["error"] = "you are not allowed";
                return verificateur.role("Alerts", HttpContext.Session.GetString("Role"));
            }

            IEnumerable<Product> objProductList = _db.Products.Where(p => p.QteStock <= 2  ).ToList();
            if(objProductList.Count()!=0)
            {
                TempData["error"] = "Il faut remplir le stock ";
            }
            return View(objProductList);
        }




    }
}
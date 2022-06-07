using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using GestionDeStockMagasin.Data;
using GestionDeStockMagasin.Models;
using GestionDeStockMagasin.ViewModels;

namespace GestionDeStockMagasin.Controllers
{
    public class FactureController : Controller
    {
        private readonly AppDbContext context;

        public FactureController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: FactureController
        public ActionResult Index()
        {
            List<Facture> factures = new List<Facture>();
            factures = context.Facture.Where(f => f.livrer == false).ToList();
            List<Client> clients = new List<Client>();
            List<Product> products = new List<Product>();
            List<int> idFacturesList = new List<int>();
                foreach (var item in factures)
            {
                var client = new Client();
                client = context.Client.Where(c=> c.ClientId == item.ClientId).Select(p => p).SingleOrDefault();
                clients.Add(client);

                var product = new Product();
                product = context.Products.Where(c=> c.ProductId == item.ProductId).Select(p => p).SingleOrDefault();
                products.Add(product); 

                idFacturesList.Add(item.IdFacture);


            }

            LivraisonIndexViewModel viewModel = new LivraisonIndexViewModel()
            {
                idFactureList = idFacturesList,
                Clients = clients,
                Products = products,
                Personnels = context.personnels.Where(x=> x.Role == "Delivery Man").ToList(),
            };

            return View(viewModel);
        }

        




        // GET: FactureController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FactureController/Create
        public ActionResult Create()
        {
            //liste of agents
            return View();
        }

        // POST: FactureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LivraisonIndexViewModel livraisonIndexViewModel )
        {
            Facture facture = new Facture();
            facture = context.Facture.Find(livraisonIndexViewModel.idFact);
            facture.livrer = true;
            context.Facture.Update(facture);
            context.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: FactureController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FactureController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FactureController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FactureController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

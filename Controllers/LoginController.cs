
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using GestionDeStockMagasin.Data;
using GestionDeStockMagasin.Models;
using GestionDeStockMagasin.ViewModels;


namespace GestionDeStockMagasin.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;
       



        public LoginController(AppDbContext db)
        {
            _db= db;
        }

        [ActionName("login")]
        public IActionResult Login()
        {
           
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("login")]
        public IActionResult Login(Personnel user)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)); 


            var Row=_db.personnels.Where(u => u.Email == user.Email || u.Password == hashed);
            if (Row.Count() == 0)
            {
                TempData["error"] = "Incorrect Email or password";
                return RedirectToAction("login");
            }
            HttpContext.Session.SetInt32("Id", Row.First().Id);
            HttpContext.Session.SetString("name", Row.First().Nom);
            HttpContext.Session.SetString("Role", Row.First().Role);
            TempData["success"] = "Welcome "+ Row.First().Nom;
            return RedirectToAction("Index","Home");
        }


        
        [ActionName("logout")]
        public IActionResult logout()
        {
            
            HttpContext.Session.Clear();
            TempData["success"] = "Disconnected ";
            return RedirectToAction("Index", "Home");
        }
    }
}

using la_mia_pizzeria_static.DataBase;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("Index", pizzas);
            }
        }
            public IActionResult Details(int id)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    Pizza? pizzaDetail = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                    if (pizzaDetail == null)
                    {
                        return NotFound($"La pizza che hai cercato non è stata trovata...");
                    }
                    else
                    {
                        return View("Details", pizzaDetail);
                    }
                }
            }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }


            using(PizzaContext db = new PizzaContext())
            {

                db.Pizzas.Add(newPizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }
    }

}

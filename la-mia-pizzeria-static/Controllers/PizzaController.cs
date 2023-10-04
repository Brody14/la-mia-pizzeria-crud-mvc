using la_mia_pizzeria_static.CustomLoggers;
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
        private ICustomLogger _myLogger;
        private PizzaContext _myDatabase;

        public PizzaController(ICustomLogger logger, PizzaContext db )
        {
            _myLogger = logger;
            _myDatabase = db;
        }
        public IActionResult Index()
        {
            _myLogger.WriteLog("Utente in pizza index");

            List<Pizza> pizzas = _myDatabase.Pizzas.ToList<Pizza>();

            return View("Index", pizzas);
            
        }
            public IActionResult Details(int id)
            {
                Pizza? pizzaDetail = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDetail == null)
                {
                    return NotFound("La pizza che hai cercato non è stata trovata...");
                }
                else
                {
                    return View("Details", pizzaDetail);
                }
               
            }

        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _myDatabase.Categories.ToList();

            PizzaFormModel model = new PizzaFormModel { Pizza = new Pizza(), Categories = categories };

            return View("Create", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;

                return View("Create", data);
            }

            _myDatabase.Pizzas.Add(data.Pizza);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if(pizzaToEdit == null)
            {
                return NotFound("La pizza non è stata trovata...");
            }else
            {
                List<Category> categories = _myDatabase.Categories.ToList();

                PizzaFormModel model = new PizzaFormModel { Pizza = pizzaToEdit, Categories = categories };

                return View("Edit", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;

                return View("Edit", data);
            }

            Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if(pizzaToEdit != null)
            {
                pizzaToEdit.Name = data.Pizza.Name;
                pizzaToEdit.Description = data.Pizza.Description;
                pizzaToEdit.Price = data.Pizza.Price;
                pizzaToEdit.Image = data.Pizza.Image;
                pizzaToEdit.CategoryId = data.Pizza.CategoryId;

                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }else
            {
                return NotFound("La pizza non è stata trovata...");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza? pizzaToDelete = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToDelete != null)
            {
                _myDatabase.Pizzas.Remove(pizzaToDelete);

                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("La pizza non è stata trovata...");
            }
           
        }
    }

}

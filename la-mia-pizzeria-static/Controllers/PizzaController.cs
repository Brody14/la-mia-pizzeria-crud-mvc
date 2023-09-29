using la_mia_pizzeria_static.DataBase;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
    }

}

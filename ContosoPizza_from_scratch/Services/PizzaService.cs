using ContosoPizza.Data;
using ContosoPizza.Models;

using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;

    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public void CreateSauce(Sauce sauce) {
        _context.Sauces.Add(sauce);
        _context.SaveChanges();
    }

    public void CreateTopping(Topping topping) {
        _context.Toppings.Add(topping);
        _context.SaveChanges();
    }

    public IEnumerable<Pizza> GetAll() {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .ToList();
    }
}
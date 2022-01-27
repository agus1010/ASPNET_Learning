using ContosoPizza.Models;
using ContosoPizza.Data;

using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{

    private readonly PizzaContext _context;

    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        _context.Add(newPizza);
        _context.SaveChanges();
        
        return newPizza;
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
        var toppingToAdd = _context.Toppings.Find(ToppingId);

        if (pizzaToUpdate is null || toppingToAdd is null) {
            throw new NullReferenceException("Pizza or topping does not exist.");
        }

        if (pizzaToUpdate.Toppings is null) {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Append(toppingToAdd);

        // WHY?
        _context.Pizzas.Update(pizzaToUpdate);

        _context.SaveChanges();
    }

    // Cambia la salsa de una pizza (el nombre es muy malo)
    public void UpdateSauce(int PizzaId, int SauceId)
    {

        var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
        var sauceToUpdate = _context.Sauces.Find(SauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null) {
            throw new NullReferenceException("Pizza or sauce does not exist.");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;

        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);

        //El ejemplo viene tirando excepciones si no encuentra una entidad, excepto aca. WHY?
        /*if (pizzaToDelete is null) {
            throw new NullReferenceException("Pizza does not exist.");
        }*/

        if (pizzaToDelete is null) return;

        _context.Pizzas.Remove(pizzaToDelete);
        _context.SaveChanges();
    }
}
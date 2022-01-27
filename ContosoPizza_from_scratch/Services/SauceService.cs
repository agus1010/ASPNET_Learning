using ContosoPizza.Data;
using ContosoPizza.Models;

using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class SauceService
{
    private readonly PizzaContext _context;

    public SauceService(PizzaContext context)
    {
        _context = context;
    }

    public Sauce? Create(Sauce newSauce) {
        _context.Sauces.Add(newSauce);
        _context.SaveChanges();
        return newSauce;
    }

    public IEnumerable<Sauce> GetAll() {
        return _context
            .Sauces
            .AsNoTracking()
            .ToList();
    }

    public Sauce? GetById(int id) {
        return _context
            .Sauces
            .AsNoTracking()
            .SingleOrDefault(s => s.Id == id);
    }

    public Sauce? Update(int sauceId, Sauce updatedSauce) {
        if(sauceId != updatedSauce.Id)
            throw new NullReferenceException("The sauce does not exist.");
        
        var sauceToUpdate = _context.Sauces.Find(sauceId);
        if(sauceToUpdate is null)


    }

    public IEnumerable<Pizza> GetAll() {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .ToList();
    }
}
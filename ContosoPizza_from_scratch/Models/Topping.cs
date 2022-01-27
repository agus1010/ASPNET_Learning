using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models;

public class Topping {
    
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
    
    public bool IsVegan { get; set; }
}
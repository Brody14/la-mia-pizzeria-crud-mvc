using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(500)]
        public string Image {  get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public double Price { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        public Pizza() { }

        public Pizza(string name, string description, string image, double price, int rating)
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
            this.Price = price;
            this.Rating = rating;
        }
    }
}

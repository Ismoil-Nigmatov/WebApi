using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project.Entity
{
    public class Course : IEntity
    {
        public int Id { get; set; } 
        public string ImageUrl { get; set; } 
        public string Description { get; set; } 
        public double Price { get; set; }

    }
}

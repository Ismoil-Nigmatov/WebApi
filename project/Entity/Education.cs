using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project.Entity
{
    public class Education
    {
        public int Id { get; set; }
        public string Title { get; set; }
            
        public string End { get; set; }
        public string Description { get; set; }

        public Course? Course { get; set; }
    }
}


using project.Data;
using project.Entity;
using project.Generics;

namespace project.repository
{
    public class CourseRepository : GenericService<Course , AppDbContext>
    {
        public CourseRepository(AppDbContext _context) : base(_context) { }
    }
}

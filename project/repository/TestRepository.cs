
using project.Data;
using project.Entity;
using project.Generics;

namespace project.repository
{
    public class TestRepository :  GenericService<Test, AppDbContext>
    {
        public TestRepository(AppDbContext context) : base(context)
        {

        }
    }
}


using project.Data;
using project.Entity;
using project.Generics;

namespace project.repository
{
    public class ContactRepository : GenericService<Contact , AppDbContext>
    {
        public ContactRepository(AppDbContext context) : base(context)
        {

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : AppDbController<Contact, ContactRepository>
    {
        public ContactController(ContactRepository repository) : base(repository) { }
    }
}

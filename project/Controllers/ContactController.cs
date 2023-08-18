using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetContacts()
        {
            var contacts = await _contactRepository.GetAllContactAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetContactById(int id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);

            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult> AddContact(ContactDTO contactDto)
        {
            await _contactRepository.AddContactAsync(contactDto);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            await _contactRepository.DeleteContactAsync(id);
            return Ok();
        }
    }
}

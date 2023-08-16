using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class ContactRepository :IContactRepository
    {

        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllContactAsync()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _context.Contact.FirstOrDefaultAsync(contact => contact.Id == id) ?? throw new BadHttpRequestException("Not Found");
        }

        public async Task AddContactAsync(ContactDTO contactDto)
        {
           Contact contact = new Contact();
           contact.Name = contactDto.Name;
           contact.PhoneNumber = contactDto.PhoneNumber;
           contact.DateTime = DateTime.UtcNow;
           _context.Contact.Add(contact);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }
    }
}

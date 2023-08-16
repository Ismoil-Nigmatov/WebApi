using project.Dto;
using project.Entity;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContactAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task AddContactAsync(ContactDTO contactDto);
        Task DeleteContactAsync(int id);
    }
}

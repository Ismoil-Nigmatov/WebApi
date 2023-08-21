using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dto;
using project.Entity;
using project.Generics;
using Task = System.Threading.Tasks.Task;

namespace project.repository
{
    public class TeacherRepository : GenericService<Teacher , AppDbContext>
    {
        public TeacherRepository(AppDbContext context) : base(context) { }

    }
}

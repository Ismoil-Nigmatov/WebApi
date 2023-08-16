using project.Entity.ENUMS;
using project.Entity;

namespace project.Dto
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public int LessonId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public EProcess Process { get; set; }
    }
}

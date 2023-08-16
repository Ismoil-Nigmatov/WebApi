using project.Entity;

namespace project.Dto
{
    public class EducationDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string End { get; set; }
        public string Description { get; set; }

        public int CourseId { get; set; }
    }
}

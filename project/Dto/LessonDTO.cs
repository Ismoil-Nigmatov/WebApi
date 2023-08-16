namespace project.Dto
{
    public class LessonDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string VideoUrl { get; set; }

        public string Information { get; set; }
        public int CourseId { get; set; }
    }
}

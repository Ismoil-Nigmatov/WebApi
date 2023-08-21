using project.Entity;

namespace project.Dto
{
    public class HomeworkDTO : IEntity
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int TaskId { get; set; }
    }
}

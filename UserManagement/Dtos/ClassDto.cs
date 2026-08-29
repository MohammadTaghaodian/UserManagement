using UserManagement.entities;

namespace UserManagement.Dtos
{
    public class ClassCreateDto
    {
        public required string Title { get; set; }

        public required string Subject { get; set; }

        public Guid SchoolId { get; set; }

    }

    public class ClassResponse
    {
        public required Guid id { get; set; }
        public required string Title { get; set; }
        public required string Subject { get; set; }
        public SchoolEntity? School { get; set; }
    }
}
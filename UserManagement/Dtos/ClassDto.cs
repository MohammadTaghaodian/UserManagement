using UserManagement.entities;

namespace UserManagement.Dtos
{
    public class ClassCreateDto
    {
        public required string Title { get; set; }

        public required string Subject { get; set; }

        public Guid? SchoolId { get; set; }

        public IEnumerable<Guid>? Users { get; set; }

    }

    public class ClassUpdateDto
    {
        public required Guid id { get; set; }
        public string? Title { get; set; }

        public string? Subject { get; set; }

        public Guid? SchoolId { get; set; }

        public IEnumerable<Guid>? Users { get; set; }

    }

    public class ClassResponse
    {
        public required Guid id { get; set; }
        public required string Title { get; set; }
        public required string Subject { get; set; }
        public Guid? SchoolId { get; set; }
        public SchoolResponse? School { get; set; }
        public IEnumerable<UserResponse>? Users { get; set; }
    }
}
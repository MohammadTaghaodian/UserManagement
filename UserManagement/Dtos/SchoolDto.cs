using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace UserManagement.Dtos
{
    public class SchoolCreateDto
    {
        [Required]
        public required string Title { get; set; }

        public Guid ClassId { get; set; }
    }

    public class SchoolUpdateDto
    {
        [Required]
        public required Guid id { get; set; }

        public string? Title { get; set; }

        public Guid ClassId { get; set; }
    }

    public class SchoolResponse
    {
        public required Guid id { get; set; }
        public required string Title { get; set; }
        public Guid Class { get; set; }
    }
}

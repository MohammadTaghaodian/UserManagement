using System.ComponentModel.DataAnnotations;

namespace UserManagement.Dtos
{
    public class UserCreateDto
    {
        [Required, MinLength(2), MaxLength(50)]
        public required string FullName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, MinLength(10), MaxLength(12)]
        public required string PhoneNumber { get; set; }

        public DateTime? Birthdate { get; set; }

        public Boolean IsMarried { get; set; } = false;

    }

    public class UserUpdateDto
    {
        public required Guid id { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? Birthdate { get; set; }

        public Boolean? IsMarried { get; set; }

    }

    public class UserResponse
    {
        public required Guid id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean IsMarried { get; set; }
        public int? Age { get; set; }
    }
}

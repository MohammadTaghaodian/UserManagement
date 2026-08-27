using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.entities
{
    [Table("Users")]
    public class UserEntities
    {
        [Key]
        public Guid id { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public required string FullName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, MinLength(10), MaxLength(12)]
        public required string PhoneNumber { get; set; }

        public DateTime? Birthdate { get; set; }

        public Boolean IsMarried { get; set; } = false;

        public IEnumerable<ClassEntity> Classes { get; set; }

    }
}

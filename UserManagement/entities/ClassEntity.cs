using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.entities
{
    [Table("Classes")]
    public class ClassEntity
    {
        [Key]
        public required Guid id { get; set; }

        public required string Title { get; set; }

        public required string Subject{ get; set; }

        public Guid? SchoolId { get; set; }
        public SchoolEntity? School { get; set; }

        public IEnumerable<UserEntities> Users { get; set; }
    }
}

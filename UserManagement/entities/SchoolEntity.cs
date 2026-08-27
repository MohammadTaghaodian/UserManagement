using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.entities
{
    [Table("Schools")]
    public class SchoolEntity
    {
        [Key]
        public required Guid id { get; set; }

        public required string Title { get; set; }

        public IEnumerable<ClassEntity> Classes { get; set; }
    }
}

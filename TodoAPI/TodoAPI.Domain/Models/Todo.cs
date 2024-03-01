using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoAPI.Domain.Interfaces;

namespace TodoAPI.Domain.Models
{
    public class Todo : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomUserId { get; set; }
        [NotMapped]
        public virtual User User { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Todos.Models
{
    public class Todo 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsCompleted { get; set;}

    }

    
}

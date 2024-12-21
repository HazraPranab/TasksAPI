using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TaskService
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public string? Status { get; set; }

    }
}

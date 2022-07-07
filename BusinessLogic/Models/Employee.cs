using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models
{
    public class Employee
    {
        [Key]

        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "name should not exceed 50 characters")]

        public string? Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc2025RoutingAssignment.Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3,ErrorMessage = "Blog Name must be between 3 and 50 characters.")]
        [Display(Name = "Blog Name")]
        public string? BlogName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2,ErrorMessage = "Author Name must be between 2 and 40 characters.")]
        [Display(Name = "Author's Name")]
        public string? AuthorName { get; set; }


        public virtual ICollection<Post>? Posts { get; set; }
    }
}

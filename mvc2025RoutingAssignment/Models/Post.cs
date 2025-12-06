using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc2025RoutingAssignment.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }   

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; }


        [DisplayName("Tags")]
        public string? Tags { get; set; }


        [Required]
        public int BlogID { get; set; }
        public virtual Blog? Blog { get; set; }
    }
}

using System.ComponentModel;

namespace mvc2025RoutingAssignment.Models.ViewModels
{
    public class BlogsViewModel
    {
        public int BlogID { get; set; }

        [DisplayName("Blog")]
        public string? BlogName { get; set; }

        [DisplayName("Author's Name")]
        public string? AuthorName { get; set; }

        [DisplayName("Posts")]
        public int PostCount { get; set; }
    }
}

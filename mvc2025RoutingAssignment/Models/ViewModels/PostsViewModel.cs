using System.ComponentModel;

namespace mvc2025RoutingAssignment.Models.ViewModels
{
    public class PostsViewModel
    {
        public int PostID { get; set; }

        [DisplayName("Blog")]
        public string? BlogName { get; set; }

        [DisplayName("Title")]
        public string? Title { get; set; }

        [DisplayName("Date Posted")]
        public DateTime DatePosted { get; set; }

        [DisplayName("Tags")]
        public int TagCount { get; set; }
    }
}

using System.ComponentModel;

namespace mvc2025RoutingAssignment.Models.ViewModels
{
    public class TagSearchPostItem
    {
        public int PostID { get; set; }

        [DisplayName("Title")]
        public string? Title { get; set; }

        [DisplayName("Date Posted")]
        public DateTime DatePosted { get; set; }
    }

    public class TagSearchViewModel
    {
        public int TagID { get; set; }

        [DisplayName("TagName")]
        public string? TagName { get; set; }

        public List<TagSearchPostItem> Posts { get; set; } = new List<TagSearchPostItem>();
    }
}

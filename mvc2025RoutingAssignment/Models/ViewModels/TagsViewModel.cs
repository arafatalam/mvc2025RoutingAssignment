using System.ComponentModel;

namespace mvc2025RoutingAssignment.Models.ViewModels
{
    public class TagsViewModel
    {
        public int TagID { get; set; }

        [DisplayName("TagName")]
        public string? TagName { get; set; }

        [DisplayName("Total Posts")]
        public int PostCount { get; set; }
    }
}

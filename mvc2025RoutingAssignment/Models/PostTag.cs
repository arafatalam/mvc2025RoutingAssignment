using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc2025RoutingAssignment.Models
{
    public class PostTag
    {
        
        public int PostID { get; set; }
        public virtual Post? Post { get; set; }

        
        public int TagID { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}

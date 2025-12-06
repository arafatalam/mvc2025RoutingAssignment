using Microsoft.AspNetCore.Mvc;
using mvc2025RoutingAssignment.Data;

namespace mvc2025RoutingAssignment.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ArchiveController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, HttpPost]
        
        public IActionResult Entry(DateTime? entryDate)
        {
            if (entryDate == null)
            {
                return RedirectToAction("Index", "Posts");
            }

            DateTime date = entryDate.Value;

            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            var post = _context.Posts
               .Where(p =>
                   p.DatePosted.Year == year &&
                   p.DatePosted.Month == month &&
                   p.DatePosted.Day == day)
               .OrderByDescending(p => p.DatePosted)
               .FirstOrDefault();


            if (post == null)
            {
                return RedirectToAction("Index", "Posts");
            }

            return RedirectToAction("Details", "Posts", new { id = post.PostID });
        }
       
    }
}

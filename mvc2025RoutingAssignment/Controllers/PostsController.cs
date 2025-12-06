using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc2025RoutingAssignment.Models;
using mvc2025RoutingAssignment.Models.ViewModels;

namespace mvc2025RoutingAssignment.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                .Include(p => p.Blog)
                .OrderByDescending(p => p.DatePosted)
                .ToListAsync();
            
            var data = posts.Select(p => new PostsViewModel
            {
                PostID = p.PostID,
                BlogName = p.Blog.BlogName,
                Title = p.Title,
                DatePosted = p.DatePosted,

                // Count tags from comma-separated string
                TagCount = string.IsNullOrWhiteSpace(p.Tags)
                    ? 0
                    : p.Tags
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Count(tag => !string.IsNullOrWhiteSpace(tag.Trim()))
            })
            .ToList();

            return View(data);

            //var applicationDbContext = _context.Posts.Include(p => p.Blog);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Blog)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["BlogID"] = new SelectList(_context.Blogs, "BlogID", "AuthorName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,Title,Body,DatePosted,Tags,BlogID")] Post post)
        {


            if (ModelState.IsValid)
            {
                _context.Add(post);

                var tagNames = (post.Tags ?? string.Empty)
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim())
                        .Where(t => !string.IsNullOrWhiteSpace(t))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();
                foreach (var tagName in tagNames)
                {
                    
                    var tag = await _context.Tags
                        .FirstOrDefaultAsync(t => t.TagName!.ToLower() == tagName.ToLower());

                    
                    if (tag == null)
                    {
                        tag = new Tag { TagName = tagName };
                        _context.Tags.Add(tag);
                    }

                    
                    _context.PostTags.Add(new PostTag
                    {
                        Post = post,
                        Tag = tag
                    });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BlogID"] = new SelectList(_context.Blogs, "BlogID", "AuthorName", post.BlogID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["BlogID"] = new SelectList(_context.Blogs, "BlogID", "AuthorName", post.BlogID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostID,Title,Body,DatePosted,Tags,BlogID")] Post post)
        {
            if (id != post.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var existingPost = await _context.Posts
                        .Include(p => p.PostTags)
                        .ThenInclude(pt => pt.Tag)
                        .FirstOrDefaultAsync(p => p.PostID == id);
                    if (existingPost == null)
                        return NotFound();

                    existingPost.Title = post.Title;
                    existingPost.Body = post.Body;
                    existingPost.DatePosted = post.DatePosted;
                    existingPost.BlogID = post.BlogID;
                    existingPost.Tags = post.Tags;

                    _context.PostTags.RemoveRange(existingPost.PostTags);

                    var tagNames = (post.Tags ?? string.Empty)
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim())
                        .Where(t => !string.IsNullOrWhiteSpace(t))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();
                    foreach (var tagName in tagNames)
                    {
                        
                        var tag = await _context.Tags
                            .FirstOrDefaultAsync(t => t.TagName!.ToLower() == tagName.ToLower());

                        
                        if (tag == null)
                        {
                            tag = new Tag { TagName = tagName };
                            _context.Tags.Add(tag);
                        }

                        
                        _context.PostTags.Add(new PostTag
                        {
                            Post = existingPost,
                            Tag = tag
                        });
                    }

                    await _context.SaveChangesAsync();


                    //_context.Update(post);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["BlogID"] = new SelectList(_context.Blogs, "BlogID", "BlogName", post.BlogID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Blog)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }


        
       

    }
}

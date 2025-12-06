using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc2025RoutingAssignment.Data;
using mvc2025RoutingAssignment.Models;
using mvc2025RoutingAssignment.Models.ViewModels;

namespace mvc2025RoutingAssignment.Controllers
{
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {

            var tags = await _context.Tags
        .OrderBy(t => t.TagName)
        .ToListAsync();

            var data = tags.Select(t => new TagsViewModel
            {
                TagID = t.TagID,
                TagName = t.TagName,
                PostCount = t.PostTags.Count  // Many-to-many
            })
            .ToList();

            return View(data);
            
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.TagID == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagID,TagName")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TagID,TagName")] Tag tag)
        {
            if (id != tag.TagID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.TagID))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.TagID == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? nameOfTag)
        {
            
            if (string.IsNullOrWhiteSpace(nameOfTag))
            {
                return RedirectToAction("Index");
            }

            
            var tag = await _context.Tags
                .Include(t => t.PostTags)
                    .ThenInclude(pt => pt.Post)
                .FirstOrDefaultAsync(t =>
                    t.TagName != null &&
                    t.TagName.ToLower() == nameOfTag.ToLower());

            
            if (tag == null)
            {
                return RedirectToAction("Index");
            }

            
            var model = new TagSearchViewModel
            {
                TagID = tag.TagID,
                TagName = tag.TagName
            };

            model.Posts = tag.PostTags
                .Where(pt => pt.Post != null)
                .Select(pt => new TagSearchPostItem
                {
                    PostID = pt.Post!.PostID,
                    Title = pt.Post.Title ?? "",
                    DatePosted = pt.Post.DatePosted
                })
                .OrderByDescending(p => p.DatePosted)
                .ToList();

            // Return Search view
            return View("TagSearch", model);
        }


        
        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.TagID == id);
        }
    }
}

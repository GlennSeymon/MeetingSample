using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetingSample.WebAPI.Models;

namespace MeetingSample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingCategoriesController : ControllerBase
    {
        private readonly MeetingSampleWebAPIContext _context;

        public MeetingCategoriesController(MeetingSampleWebAPIContext context)
        {
            _context = context;
        }

        // GET: api/MeetingCategories
        [HttpGet]
        public IEnumerable<MeetingCategory> GetMeetingCategories()
        {
            return _context.MeetingCategories;
        }

        // GET: api/MeetingCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meetingCategory = await _context.MeetingCategories.FindAsync(id);

            if (meetingCategory == null)
            {
                return NotFound();
            }

            return Ok(meetingCategory);
        }

        // PUT: api/MeetingCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetingCategory([FromRoute] int id, [FromBody] MeetingCategory meetingCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meetingCategory.MeetingCategoryCode)
            {
                return BadRequest();
            }

            _context.Entry(meetingCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MeetingCategories
        [HttpPost]
        public async Task<IActionResult> PostMeetingCategory([FromBody] MeetingCategory meetingCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MeetingCategories.Add(meetingCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeetingCategory", new { id = meetingCategory.MeetingCategoryCode }, meetingCategory);
        }

        // DELETE: api/MeetingCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetingCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meetingCategory = await _context.MeetingCategories.FindAsync(id);
            if (meetingCategory == null)
            {
                return NotFound();
            }

            _context.MeetingCategories.Remove(meetingCategory);
            await _context.SaveChangesAsync();

            return Ok(meetingCategory);
        }

        private bool MeetingCategoryExists(int id)
        {
            return _context.MeetingCategories.Any(e => e.MeetingCategoryCode == id);
        }
    }
}
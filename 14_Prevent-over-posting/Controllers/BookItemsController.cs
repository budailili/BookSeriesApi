using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSeriesApi.Models;

namespace BookSeriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemsController : ControllerBase
    {
        private readonly BookContext _context;

        public BookItemsController(BookContext context)
        {
            _context = context;
        }

        // GET: api/BookItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookItemDTO>>> GetBookItems()
        {
            return await _context.BookItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/BookItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookItemDTO>> GetBookItem(long id)
        {
            var bookItem = await _context.BookItems.FindAsync(id);

            if (bookItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(bookItem);
        }

        // PUT: api/BookItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookItem(long id, BookItemDTO bookDTO)
        {
            if (id != bookDTO.Id)
            {
                return BadRequest();
            }

            var bookItem = await _context.BookItems.FindAsync(id);
            if (bookItem == null)
            {
                return NotFound();
            }

            bookItem.Title = bookDTO.Title;
            bookItem.Author = bookDTO.Author;
            bookItem.SeriesName = bookDTO.SeriesName;
            bookItem.MainCouple = bookDTO.MainCouple;
            bookItem.IsRead = bookDTO.IsRead;
            bookItem.SpiceRating = bookDTO.SpiceRating;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BookItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/BookItems
        [HttpPost]
        public async Task<ActionResult<BookItemDTO>> PostBookItem(BookItemDTO bookDTO)
        {
            var bookItem = new BookItem
            {
                Title = bookDTO.Title,
                Author = bookDTO.Author,
                SeriesName = bookDTO.SeriesName,
                MainCouple = bookDTO.MainCouple,
                IsRead = bookDTO.IsRead,
                SpiceRating = bookDTO.SpiceRating
            };

            _context.BookItems.Add(bookItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBookItem),
                new { id = bookItem.Id },
                ItemToDTO(bookItem));
        }

        // DELETE: api/BookItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookItem(long id)
        {
            var bookItem = await _context.BookItems.FindAsync(id);
            if (bookItem == null)
            {
                return NotFound();
            }

            _context.BookItems.Remove(bookItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookItemExists(long id)
        {
            return _context.BookItems.Any(e => e.Id == id);
        }

        private static BookItemDTO ItemToDTO(BookItem bookItem) =>
            new BookItemDTO
            {
                Id = bookItem.Id,
                Title = bookItem.Title,
                Author = bookItem.Author,
                SeriesName = bookItem.SeriesName,
                MainCouple = bookItem.MainCouple,
                IsRead = bookItem.IsRead,
                SpiceRating = bookItem.SpiceRating
            };
    }
}
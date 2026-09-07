using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public CategoriesController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return Ok(categories);
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategorie(int id)
    {
        var categorie = await _context.Categories.FindAsync(id);

        if (categorie == null)
            return NotFound();

        return Ok(categorie);
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<IActionResult> CreateCategorie([FromBody] Categorie categorie)
    {
        _context.Categories.Add(categorie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCategorie),
            new { id = categorie.IdCategorie },
            categorie
        );
    }

    // PUT: api/Categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategorie(
        int id,
        [FromBody] Categorie categorie)
    {
        if (id != categorie.IdCategorie)
            return BadRequest("L'identifiant ne correspond pas.");

        _context.Entry(categorie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Categories.AnyAsync(c => c.IdCategorie == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    // DELETE: api/Categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategorie(int id)
    {
        var categorie = await _context.Categories.FindAsync(id);

        if (categorie == null)
            return NotFound();

        _context.Categories.Remove(categorie);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDs.Context;
using APIDs.Entities;

namespace APIDs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpellController : Controller
{
    private readonly ApplicationDbContext _context;

    public SpellController(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    // Get Spell

    [HttpGet]
    [Route("getOneSpell")]
    async public Task<ActionResult<List<Spell>>> getSpell(string Name)
    {
        Spell selectedSpell = _context.Spells.Where(x => x.Name == Name).FirstOrDefault();
        selectedSpell = _context.Spells.Include(x => x.Colors).FirstOrDefault();

        if (selectedSpell != null)
        {
            return Ok(selectedSpell);
        }
        else
        {
            return BadRequest("No Spell with this Name !");
        }

    }

    // Post Spell

    [HttpPost]
    [Route("addSpell")]
    public ActionResult addSpell(string Name, string Description, int Cost, string ColorName)
    {
        var Color = _context.CardColors.Where(x => x.Name == ColorName).FirstOrDefault();

        if (Color != null)
        {
            _context.Spells.Add(new Spell()
            {
                Name = Name,
                Description = Description,
                Cost = Cost,
                Colors = Color,

            });

            _context.SaveChanges();

            var createdSpell = _context.Spells.FirstOrDefault(x => x.Name == Name);

            return Ok(createdSpell);
        }
        else
        {
            return BadRequest("No Color Card Found !");
        }


    }


    // PATCH Spell

    [HttpPatch]
    [Route("modifySpell")]
    public ActionResult modifySpell(int id, string Name, string Description, int Cost, string ColorName)
    {
        var Color = _context.CardColors.Where(x => x.Name == ColorName).FirstOrDefault();

        _context.Spells.Select(x => new Spell()
        {
            id = x.id,
            Name = x.Name
        });

        if (ColorName != null)
        {

            Name = Name;
            Description = Description;
            Cost = Cost;
            Color = Color;

            _context.SaveChanges();

            var modifySpell = _context.Spells.FirstOrDefault(x => x.Name == Name);

            return Ok(modifySpell);

        }
        else
        {
            return BadRequest("No Color Card Found or No Ground Found !");
        }

    }

    // Delete Spell

    [HttpDelete]
    [Route("deleteSpell")]
    public ActionResult deleteSpell(string Name)
    {
        var Spell = _context.Spells.Where(x => x.Name == Name).FirstOrDefault();
        if (Spell == null)
        {
            return BadRequest("No Spell Found !");
        }
        else
        {
            _context.Spells.Remove(Spell);

            _context.SaveChanges();

            return Ok(_context.Spells);
        }


    }
}
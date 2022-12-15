using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDs.Context;
using APIDs.Entities;

namespace APIDs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreaturesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CreaturesController(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    // Get AllCreatures

    [HttpGet]
    [Route("getAllCreatures")]
    public ActionResult getCreatures()
    {
        return Ok(_context.Creatures);
    }

    // Get One Creature

    [HttpGet]
    [Route("getOneCreature")]
    public ActionResult getOneCreature(string Name)
    {
        var NameOfCreature = _context.Creatures.Where(x => x.Name == Name).FirstOrDefault();

        if(NameOfCreature != null)
        {

            return Ok(NameOfCreature);
        }
        else
        {
            return BadRequest("No Creatures with this Name !");
        }
    }

    // Post Creatures

    [HttpPost]
    [Route("addCreatures")]
    public ActionResult addCreatures(string Name, string Description, int Cost, int Attack, int Defense, string ColorName)
    {

        var Color = _context.CardColors.Where(x => x.Name == ColorName).FirstOrDefault();

        if (Color != null)
        {
            _context.Creatures.Add(new Creatures()
            {
                Name = Name,
                Description = Description,
                Cost = Cost,
                Attack = Attack,
                Defense = Defense,
                Color = Color,

            });

            _context.SaveChanges();

            var createdCreature = _context.Creatures.FirstOrDefault(x => x.Name == Name);

            return Ok(createdCreature);
        }
        else
        {
            return BadRequest("No Color Card Found or No Creatures already exist !");
        }


    }


    // PATCH Creatures

    [HttpPatch]
    [Route("modifyCreatures")]
    public ActionResult modifyCreatures(int id, string Name ,string Description, int Cost, int Attack, int Defense, string ColorName)
    {
        var Creatures = _context.Creatures.Where(x => x.id == id).FirstOrDefault();

        var Color = _context.CardColors.Where(x => x.Name == ColorName).FirstOrDefault();

        _context.Creatures.Select(x => new Creatures()
        {
            id = x.id,
            Name = x.Name
        });

        if (ColorName != null && Creatures != null)
        {

            Creatures.Name = Name;
            Creatures.Description = Description;
            Creatures.Cost = Cost;
            Creatures.Attack = Attack;
            Creatures.Defense = Defense;
            Color = Color;

            _context.SaveChanges();

            var modifyCreature = _context.Creatures.FirstOrDefault(x => x.Name == Name);

            return Ok(modifyCreature);

        }
        else
        {
            return BadRequest("No Color Card Found or No Creatures Found !");
        }
              


    }

    // Delete Creatures

    [HttpDelete]
    [Route("deleteCreatures")]
    public ActionResult deleteCreatures(string Name)
    {
        var Creatures = _context.Creatures.Where(x => x.Name == Name).FirstOrDefault();
        if (Creatures == null)
        {
            return BadRequest("No Creatures Found !");
        }
        else
        {

            _context.Creatures.Remove(Creatures);

            _context.SaveChanges();

            return Ok(_context.Creatures);
        }

    }
}

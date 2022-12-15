using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDs.Context;
using APIDs.Entities;

namespace APIDs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardColorController : Controller
{

    private readonly ApplicationDbContext _context;

    public CardColorController(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }


    // Get CardColor

    [HttpGet]
    [Route("getAllCardColor")]
    public ActionResult getCardColor()
    {
        return Ok(_context.CardColors);
    }

    // Post CardColor

    [HttpPost]
    [Route("addCardColor")]
    public ActionResult addCardColor(string ColorName)
    {
        var Color = _context.CardColors.Where(x => x.Name == ColorName).FirstOrDefault();

        if (Color != null)
        {
            return BadRequest("Color already exist !");

        }
        else
        {
            _context.CardColors.Add(new CardColor()
            {
                Name = ColorName,
            });

            _context.SaveChanges();

            var createdColor = _context.CardColors.FirstOrDefault(x => x.Name == ColorName);

            return Ok(createdColor);
        }

    }


    // PATCH CardColor

    [HttpPatch]
    [Route("modifyCardColor")]
    public ActionResult modifyCardColor(int id, string Name)
    {
        var cardColor = _context.CardColors.Where(x => x.id == id).FirstOrDefault();
        if (cardColor == null)
        {
            return BadRequest("No Card Color Found !");
        }
        else
        {

            _context.CardColors.Select(x => new CardColor()
            {
                id = x.id,
                Name = x.Name
            });

            cardColor.id = id;
            cardColor.Name = Name;

            _context.SaveChanges();
            var modifyColor = _context.CardColors.FirstOrDefault(x => x.Name == Name);

            return Ok(modifyColor);
        }
    }

    // Delete CardColor

    [HttpDelete]
    [Route("deleteCardColor")]
    public ActionResult deleteCardColor(string Name)
    {
        var cardColor = _context.CardColors.Where(x => x.Name == Name).FirstOrDefault();
        if (cardColor == null)
        {
            return BadRequest("No Card Color Found !");
        }
        else
        {
            _context.CardColors.Remove(cardColor);

            _context.SaveChanges();

            return Ok("Success");
        }

    }

}

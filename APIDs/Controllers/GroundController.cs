using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDs.Context;
using APIDs.Entities;

namespace APIDs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroundController : Controller
{
    private readonly ApplicationDbContext _context;

    public GroundController(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    // Get Ground

    [HttpGet]
    [Route("getAllGround")]
    public ActionResult getGround()
    {
        return Ok(_context.Grounds);
    }

    // Get One Ground

    [HttpGet]
    [Route("getOneGround")]
    public ActionResult getOneGround(string Name)
    {
        var NameOfGround = _context.Grounds.Where(x => x.Name == Name).FirstOrDefault();

        if(NameOfGround != null)
        {
            return Ok(NameOfGround);

        }
        else
        {
            return BadRequest("No Ground with this Name !");

        }
    }


    // Post Ground

    [HttpPost]
    [Route("addGround")]
    public ActionResult addGround(string Name, string ColorName)
    {

        var Color = _context.CardColors.Where(x => x.Name == ColorName).FirstOrDefault();

        if (Color != null)
        {
            _context.Grounds.Add(new Ground()
            {
                Name = Name,
                Color = Color,

            });

            _context.SaveChanges();

            var createdGround = _context.Grounds.FirstOrDefault(x => x.Name == Name);

            return Ok(createdGround);
        }
        else
        {
            return BadRequest("No Color Card Found !");
        }


    }


    // PATCH Ground

    [HttpPatch]
    [Route("modifyGround")]
    public ActionResult modifyGround(int id, string Name, string ColorName)
    {
        var Ground = _context.Grounds.Where(x => x.id == id).FirstOrDefault();

        var Color = _context.CardColors.Where(x => x.Name == ColorName).FirstOrDefault();

        _context.Grounds.Select(x => new Ground()
        {
            id = x.id,
            Name = x.Name
        });

        if (ColorName != null && Ground != null)
        {

            Ground.Name = Name;
            Color = Color;

            _context.SaveChanges();

            var modifyGround = _context.Grounds.FirstOrDefault(x => x.Name == Name);

            return Ok(modifyGround);

        }
        else
        {
            return BadRequest("No Color Card Found or No Ground Found !");
        }



    }

    // Delete Ground

    [HttpDelete]
    [Route("deleteGround")]
    public ActionResult deleteGround(string Name)
    {
        var Ground = _context.Grounds.Where(x => x.Name == Name).FirstOrDefault();
        if (Ground == null)
        {
            return BadRequest("No Ground Found !");
        }
        else
        {
            _context.Grounds.Remove(Ground);

            _context.SaveChanges();

            return Ok("Success");
        }


    }

}

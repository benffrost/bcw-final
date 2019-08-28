using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsRepository _keepRepo;
    private readonly UserRepository _userRepo;
    public KeepsController(KeepsRepository keepRepo, UserRepository userRepo)
    {
      _keepRepo = keepRepo;
      _userRepo = userRepo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Keep>> GetPublic()
    {
      return Ok(_keepRepo.GetPublicKeeps());
    }

    [Authorize]
    [HttpGet("user")]
    public ActionResult<IEnumerable<Keep>> GetUserKeeps()
    {
      var id = HttpContext.User.FindFirstValue("Id");
      var user = _userRepo.GetUserById(id);

      if (user != null)
      {
        return Ok(_keepRepo.GetKeepsByUser(id));
      }

      return BadRequest();
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> GetOne(int id)
    {
      return Ok(_keepRepo.GetKeepById(id));
    }

    //POSTS
    [Authorize]
    [HttpPost]
    public ActionResult<Keep> CreateKeep([FromBody]Keep newKeep)
    {
      Keep intermediate = newKeep;

      var id = HttpContext.User.FindFirstValue("Id");
      var user = _userRepo.GetUserById(id);

      if (user != null)
      {
        intermediate.userId = id;

        return Ok(_keepRepo.CreateKeep(intermediate));
      }

      return BadRequest();
    }

    //PUTS
    [Authorize]
    [HttpPut("{id}")]
    public ActionResult<Keep> EditKeep([FromBody] Keep keep)
    {
      //dangerous; we should use the repo(?) to also verify that the keep being editted is actually the user's keep?  from here they could "steal" a keep, but not give it to someone else

      var id = HttpContext.User.FindFirstValue("Id");
      var user = _userRepo.GetUserById(id);

      if (user != null && keep.userId == id)
      {
        return Ok(_keepRepo.EditKeep(keep));
      }
      return BadRequest();
    }


    //deletes
    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<string> DeleteKeep(int id)
    {
      //completely unsafe at this level; userid not checked?
      //TODO needs a little love to make sure there's not too much mess up
      if (_keepRepo.DeleteKeep(id))
      {
        return Ok();
      }
      return BadRequest();
    }
  }
}
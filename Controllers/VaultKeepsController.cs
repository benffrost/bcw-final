using System.Collections.Generic;
using System.Security.Claims;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{

  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsRepository _vkRepo;
    private readonly UserRepository _userRepo;


    public VaultKeepsController(VaultKeepsRepository vkRepo, UserRepository userRepo)
    {
      _vkRepo = vkRepo;
      _userRepo = userRepo;
    }

    //gets
    [HttpGet("{vaultId}")]
    public ActionResult<IEnumerable<Keep>> GetVaultKeeps(int vaultId)
    {
      var userId = HttpContext.User.FindFirstValue("Id");
      var user = _userRepo.GetUserById(userId);

      if (user != null)
      {
        return Ok(_vkRepo.GetVaultKeeps(vaultId, userId));
      }

      return BadRequest();
    }

    //posts
    [HttpPost]
    public ActionResult<VaultKeep> CreateVaultKeep([FromBody]VaultKeep newVK)
    {
      VaultKeep intermediate = newVK;

      var id = HttpContext.User.FindFirstValue("Id");
      var user = _userRepo.GetUserById(id);

      if (user != null)
      {
        intermediate.UserId = id;

        return Ok(_vkRepo.CreateVaultKeep(intermediate));
      }

      return BadRequest();
    }

    //Puts
    [HttpPut]
    public ActionResult<string> DeleteVaultKeep(VaultKeep vk)
    {
      VaultKeep intermediate = vk;
      if (vk.UserId == null)
      {
        intermediate.UserId = HttpContext.User.FindFirstValue("Id");
      }

      //completely unsafe at this level; userid not checked?
      //TODO needs a little love to make sure there's not too much mess up
      if (_vkRepo.DeleteVaultKeep(intermediate))
      {
        return Ok(vk);
      }
      return BadRequest();
    }

  }

}


//create vault keep /vaultkeeps
//get vault keeps /vaultkeeps/:vaultId
//delete vault keeps  /vaultkeeps

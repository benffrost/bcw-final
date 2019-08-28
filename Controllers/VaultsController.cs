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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsRepository _vaultRepo;
    private readonly UserRepository _userRepo;


    public VaultsController(VaultsRepository vaultRepo, UserRepository userRepo)
    {
      _vaultRepo = vaultRepo;
      _userRepo = userRepo;
    }

    //gets
    [HttpGet]
    public ActionResult<IEnumerable<Vault>> GetUserVaults()
    {
      var id = HttpContext.User.FindFirstValue("Id");
      var user = _userRepo.GetUserById(id);

      if (user != null)
      {
        return Ok(_vaultRepo.GetVaultsByUser(id));
      }

      return BadRequest();
    }

    [HttpGet("{id}")]
    public ActionResult<Vault> GetOne(int id)
    {
      return Ok(_vaultRepo.GetVaultById(id));
    }

    //posts
    [HttpPost]
    public ActionResult<Vault> CreateVault([FromBody]Vault newVault)
    {
      Vault intermediate = newVault;

      var id = HttpContext.User.FindFirstValue("Id");
      var user = _userRepo.GetUserById(id);

      if (user != null)
      {
        intermediate.userId = id;

        return Ok(_vaultRepo.CreateVault(intermediate));
      }

      return BadRequest();
    }

    //Puts (none of these)

    //Deletes
    [HttpDelete("{id}")]
    public ActionResult<string> DeleteVault(int id)
    {
      //completely unsafe at this level; userid not checked?
      //TODO needs a little love to make sure there's not too much mess up
      if (_vaultRepo.DeleteVault(id))
      {
        return Ok();
      }
      return BadRequest();
    }

  }

}


//create vault keep /vaultkeeps
//get vault keeps /vaultkeeps/:vaultId
//delete vault keeps  /vaultkeeps

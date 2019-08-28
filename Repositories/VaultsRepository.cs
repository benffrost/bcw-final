using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db) { _db = db; }

    //gets
    public IEnumerable<Vault> GetVaultsByUser(string Id)
    {
      return _db.Query<Vault>("SELECT * FROM vaults WHERE userId = @Id;", new { Id });
    }

    public Vault GetVaultById(int Id)
    {
      return _db.QueryFirst<Vault>("SELECT * FROM vaults WHERE id = @Id;", new { Id });
    }

    //posts

    public Vault CreateVault(Vault vault)
    {
      int id = _db.ExecuteScalar<int>("INSERT INTO vaults (name, description, userId)"
      + "VALUES (@Name, @Description, @UserId); SELECT LAST_INSERT_ID()", vault);
      vault.Id = id;
      return vault;
    }

    //Puts (we don't need any of these?)

    //Deletes
    public bool DeleteVault(int vaultId)
    {
      int success = _db.Execute("DELETE FROM vaults WHERE id = @vaultId", new { vaultId });
      return success > 0;
    }

  }
}

//create vault keep /vaultkeeps
//get vault keeps /vaultkeeps/:vaultId
//delete vault keeps  /vaultkeeps

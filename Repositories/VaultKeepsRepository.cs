using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db) { _db = db; }


    public IEnumerable<Keep> GetVaultKeeps(int vaultId, string userId)
    {
      return _db.Query<Keep>(@"
        SELECT * FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId
        WHERE(vaultId = @vaultId AND vk.userId = @userId)", new { vaultId, userId });
    }

    public VaultKeep CreateVaultKeep(VaultKeep vk)
    {
      int id = _db.ExecuteScalar<int>("INSERT INTO vaultkeeps (vaultid, keepid, userid)"
      + "VALUES (@VaultId, @KeepId, @UserId); SELECT LAST_INSERT_ID()", vk);
      vk.Id = id;
      return vk;
    }

    public bool DeleteVaultKeep(VaultKeep vk)
    {
      int success = _db.Execute("DELETE FROM vaultkeeps WHERE vaultid = @VaultId AND keepid = @KeepId AND userid = @UserID;", vk);
      return success > 0;
    }

  }
}
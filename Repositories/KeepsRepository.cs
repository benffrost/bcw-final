using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db) { _db = db; }

    //gets
    public IEnumerable<Keep> GetPublicKeeps()
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE isPrivate = false;");
    }

    public IEnumerable<Keep> GetKeepsByUser(string Id)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE userId = @Id;", new { Id });
    }

    public Keep GetKeepById(int Id)
    {
      return _db.QueryFirst<Keep>("SELECT * FROM keeps WHERE id = @Id;", new { Id });
    }

    //posts 

    public Keep CreateKeep(Keep keep)
    {
      int id = _db.ExecuteScalar<int>("INSERT INTO keeps (name, description, userId, img, isPrivate)"
      + "VALUES (@Name, @Description, @UserId, @Img, @IsPrivate); SELECT LAST_INSERT_ID()", keep);
      keep.Id = id;
      return keep;
    }

    //puts
    public Keep EditKeep(Keep keep)
    {
      //WILL FAIL ON BAD ID
      string query = @"
                UPDATE keeps SET
                    name = @Name,
                    description = @Description,
                    userId = @UserId,
                    img = @Img,
                    isPrivate = @IsPrivate
                WHERE id = @Id;
                SELECT * FROM keeps WHERE id = @Id;
            ";
      return _db.QueryFirstOrDefault<Keep>(query, keep);
    }

    public bool DeleteKeep(int keepId)
    {
      int success = _db.Execute("DELETE FROM keeps WHERE id = @keepId", new { keepId });
      return success > 0;
    }
  }
}
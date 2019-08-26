using System.Data;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db) { _db = db; }

    //db access goes here
  }
}
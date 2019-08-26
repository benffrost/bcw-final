using System.Data;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db) { _db = db; }

    //db access goes here
  }
}
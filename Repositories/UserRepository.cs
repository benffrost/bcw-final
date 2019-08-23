using System;
using System.Data;
using System.Linq;
using BCrypt.Net;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class UserRepository
    {

        IDbConnection _db;

        //REGISTER
        public User Register(UserRegistration creds)
        {
            //generate the user id
            //HASH THE PASSWORD
            string id = Guid.NewGuid().ToString();
            string hash = BCrypt.Net.BCrypt.HashPassword(creds.Password);
            int success = _db.Execute(@"
        INSERT INTO users (id, username, email, hash)
        VALUES (@id, @username, @email, @hash);
      ", new
            {
                id,
                username = creds.Username,
                email = creds.Email,
                hash
            });

            if (success != 1) { throw new Exception("Invalid Credentials"); }

            return new User()
            {
                Username = creds.Username,
                    Email = creds.Email,
                    Hash = null,
                    Id = id
            };
        }

        //LOGIN 
        public User Login(UserLogin creds)
        {
            User user = _db.Query<User>(@"
                SELECT * FROM users WHERE email = @Email
                ", creds).FirstOrDefault();
            if (user == null) { throw new Exception("Invalid Credentials"); }
            bool validPass = BCrypt.Net.BCrypt.Verify(creds.Password, user.Hash);
            if (!validPass) { throw new Exception("Invalid Credentials"); }
            user.Hash = null;
            return user;
        }

        internal User GetUserById(string id)
        {
            var user = _db.Query<User>(@"
      SELECT * FROM users WHERE id = @id
      ", new { id }).FirstOrDefault();
            if (user == null) { throw new Exception("Invalid UserId"); }
            user.Hash = null;
            return user;
        }

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

    }
}
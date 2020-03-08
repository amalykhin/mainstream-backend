using SteamingService.Helpers;
using SteamingService.Models;
using System;
using System.Linq;

namespace SteamingService.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Credentials can't be null.");
            }

            var candidate = _context.Users.SingleOrDefault(user => user.Username == username);
            if (candidate == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, candidate.PasswordHash, candidate.HashSalt))
            {
                return null;
            }
            candidate.State = User.UserState.Active;
            _context.SaveChanges();

            return candidate;
        }

        public User GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public User Register(User user, string password)
        {
            if (user == null) {
                throw new ArgumentNullException("User can't be null.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password can't be empty or whitespace.");
            }

            var (passwordHash, hashSalt) = CreatePasswordHash(password);
            user.PasswordHash = passwordHash;
            user.HashSalt = hashSalt;
            user.StreamerKey = CreateStreamerKey();

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        private string CreateStreamerKey()
        {
            return $"stream_{Guid.NewGuid()}";
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) 
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) 
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) 
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        private static Tuple<byte[], byte[]> CreatePasswordHash(string password)
        {
            if (password == null) 
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return new Tuple<byte[], byte[]>(
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    hmac.Key);
            }
        }
    }
}

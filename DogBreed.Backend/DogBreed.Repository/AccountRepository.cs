using AutoMapper;
using DogBreed.DAL;
using DogBreed.DAL.Entities;
using DogBreed.Model.Common;
using DogBreed.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DogBreed.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;

        public AccountRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IUser> RegisterAsync(string email, string password)
        {
            bool exist = await CheckIfUserExist(email);
            UserEntity result = null;
            if (!exist)
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);

                using (var ctx = new DogBreedContext())
                {
                    result = new UserEntity()
                    {
                        Id = Guid.NewGuid(),
                        Email = email,
                        Password = savedPasswordHash,
                        ConfirmPassword = savedPasswordHash,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    };
                    await ctx.AddAsync(result);
                    await ctx.SaveChangesAsync();
                }
            }     
            return _mapper.Map<IUser>(result);
        }

        public async Task<IUser> LoginAsync(string email, string password)
        {
            bool loginSuccessful = await CheckIfUserExist(email);

            var existUser = await GetUser(email);

            if (loginSuccessful)
            {
                bool correctPassword = ComparePassword(existUser.Password, password);
                if (correctPassword)
                {
                    return _mapper.Map<IUser>(existUser);
                }
            }
            return null;
        }

        public async Task<bool> CheckIfUserExist(string email)
        {
            List<UserEntity> users;
            bool userExist;
            using (var ctx = new DogBreedContext())
            {
                users = await ctx.Users.ToListAsync();
                userExist = users.Any(u => u.Email == email);
            }
            return userExist;
        }

        public async Task<UserEntity> GetUser(string email)
        {
            UserEntity userExist = null;
            using (var ctx = new DogBreedContext())
            {
                userExist = await ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            return userExist;
        }

        public bool ComparePassword(string savedPassword, string inputPassword)
        {
            bool result;
            byte[] hashBytes = Convert.FromBase64String(savedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }
}

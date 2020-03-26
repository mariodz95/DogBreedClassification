using AutoMapper;
using DogBreed.DAL;
using DogBreed.DAL.Entities;
using DogBreed.Model.Common;
using DogBreed.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                using (var ctx = new DogBreedContext())
                {
                    result = new UserEntity()
                    {
                        Id = Guid.NewGuid(),
                        Email = email,
                        Password = password,
                        ConfirmPassword = password,
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
            UserEntity user = null;
            if (loginSuccessful)
            {
                using (var ctx = new DogBreedContext())
                {
                    user = await ctx.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                    return _mapper.Map<IUser>(user);
                }
            }
            return _mapper.Map<IUser>(user);
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
    }
}

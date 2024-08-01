using ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerLibrary.Helpers;
using BaseLibrar.Responses;
using ServerLibrary.Data;
using BaseLibrar.DTOs;
using BaseLibrar.Entities;


namespace ServerLibrary.Repositories.Implementations
{
    internal class UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUserAccount
    {
         public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null) return new GeneralResponse(false, "Model is empty");

            var checkUser = await FindUserByEmail(user.Email);
            if (checkUser != null) return new GeneralResponse(false, "User registered already");

            // save user
            var applicationUser = await AddToDatabase(new ApplicationUser 
            { 
                Fullname = user.FullName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
                
            });
        }

        public Task<LoginResponse> SignInAsync(Login user) 
        { 
            throw new NotImplementedException(); 
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) => 
            await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower() ) );

        private async Task<T> AddToDatabase <T> (T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }
    }
}

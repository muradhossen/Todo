using Domain.Entities.Users;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace Infrastructure.Persistance
{
    public class Seed
    {
        public static async Task SeedUsersAsync(ApplicationDbContext context)
        {
            // Ensure DB exists
            await context.Database.MigrateAsync();

            // Seed Admin user if not exists
            if (!await context.Users.AnyAsync())
            {

                var users = new List<User>();


                using var hmac = new HMACSHA512();
 

                var adminUser = new User
                {
                    FullName = "Admin",
                    Email = "admin@demo.com",
                    Role = (int)RoleEnum.Admin,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Admin123!")),
                    PasswordSalt = hmac.Key
                }; 
            

                users.Add(adminUser); 


                await context.Users.AddRangeAsync(users);

                await context.SaveChangesAsync();
            }

        }

    }
}

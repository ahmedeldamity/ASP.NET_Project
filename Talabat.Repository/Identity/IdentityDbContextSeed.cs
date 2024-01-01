using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;

namespace Talabat.Repository.Identity
{
    public class IdentityDbContextSeed
    {
        public async static Task SeedUsersAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var usersData = File.ReadAllText("../Talabat.Repository/Identity/DataSeeding/users.json");
                var users = JsonSerializer.Deserialize<List<AppUser>>(usersData);

                if (users?.Count() > 0)
                {
                    foreach (var user in users)
                    {
                        await _userManager.CreateAsync(user, "Pa$$w0rd");
                    }
                }
            }
        }
    }
}

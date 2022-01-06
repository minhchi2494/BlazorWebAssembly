using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Api.Entities;
using TodoList.Models.Enums;
using Task = System.Threading.Tasks.Task;

namespace TodoList.Api.Data
{
    public class ToDoListDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(TodoListDbContext context, ILogger<ToDoListDbContextSeed> logger)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mr",
                    LastName = "A",
                    Email = "a@gmail.com",
                    PhoneNumber = "023256525",
                    UserName = "admin"
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin123@123$");
                context.Add(user);
            }

            if (!context.Tasks.Any())
            {
                context.Tasks.Add(new Entities.Task()
                {
                    Id = Guid.NewGuid(),
                    Name = "Same task 1",
                    CreatedDate = DateTime.Now,
                    Priotiry = Priotiry.High,
                    Status = Status.Done
                });
            }

            await context.SaveChangesAsync();
        }
    }
}

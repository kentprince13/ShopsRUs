using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Services.UserService
{
    public class UsersService : IUsersService
    {
        private readonly ShopsRUsContext _context;
        private readonly ILogger<UsersService> _logger;

        public UsersService(ShopsRUsContext context, ILogger<UsersService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<List<User>> GetUsers()
        {
            _logger.LogInformation("Fetching all Users");
            var users = await _context.Users.Where(c=>c.IsActive).ToListAsync();
            return users;   
        }

        public async Task<User> GetUserById(long id)
        {
            _logger.LogInformation($"Fetching User by Id: {id}");
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
            return user;
        }   
        
        public async Task<User> GetUserByName(string name)
        {
            _logger.LogInformation($"Fetching User by Name: {name}");
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Name == name && c.IsActive);
            return user;
        }
        
        public async Task<User> GetUserByNamAndPhone(string name, string phoneNumber)
        {
            _logger.LogInformation($"Fetching User by Name: {name} and PhoneNumber: {phoneNumber}");
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Name == name  && c.PhoneNumber == phoneNumber && c.IsActive);
            return user;
        }
        public async Task CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New User Created with Name: {user.Name}");
        }

        public async Task DeActivateUserById(long id)
        {
            _logger.LogInformation($"Fetching User by Id: {id}");
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
            user.IsActive = false;
            user.UpdatedOn = DateTime.Now;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User Id: {id} Deactivated");

        }
    }
}
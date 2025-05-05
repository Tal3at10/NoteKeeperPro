using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Application.Dtos.Users;
using NoteKeeperPro.Domain.Entities.Users;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Users;

namespace NoteKeeperPro.Application.Services.Users
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserToReturnDto> GetAllUsers() // Why I Qurable ? - Use IQueryable to allow efficient query composition before executing in the database
        {
            var users = _userRepository.GetAllQuarable()
                .AsNoTracking()
                .Select(user => new UserToReturnDto
                {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    UserName = user.UserName,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber
                })
                .ToList();

            return users;
        }

        public UserDetailsToReturnDto? GetUserById(int id)
        {
            var user = _userRepository.GetById(id);

            if (user != null)
            {
                return new UserDetailsToReturnDto
                {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    UserName = user.UserName,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber
                };
            }

            return null;
        }

        public int CreateUser(UserToCreateDto user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            return _userRepository.AddUser(newUser);
        }

        public int UpdateUser(UserToUpdateDto user)
        {
            var existingUser = _userRepository.GetById(user.Id);

            if (existingUser == null)
                return 0;

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.PhoneNumber = user.PhoneNumber;

            return _userRepository.UpdateUser(existingUser);
        }

        public bool DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);

            if (user != null)
            {
                return _userRepository.DeleteUser(user) > 0;
            }

            return false;
        }
    }
}

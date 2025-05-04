using System;
using System.Collections.Generic;
using NoteKeeperPro.Application.Dtos.Users;
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

        public int CreateUser(UserToCreateDto user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserToReturnDto> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserDetailsToReturnDto? GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateUser(UserToUpdateDto user)
        {
            throw new NotImplementedException();
        }
    }
}

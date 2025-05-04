using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Application.Dtos.Users;

namespace NoteKeeperPro.Application.Services.Users
{
    internal interface IUserService
    {
        IEnumerable<UserToReturnDto> GetAllUsers();
        UserDetailsToReturnDto? GetUserById(int id);
        int CreateUser(UserToCreateDto user);
        int UpdateUser(UserToUpdateDto user);
        bool DeleteUser(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Domain.Entities.Users;

namespace NoteKeeperPro.Infrastructure.Presistance.Repositories.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll(bool AsNoTracking = true);
        User GetById(int id);
        int AddUser(User user);
        int UpdaetUser(User user);
        int DeleteUser(User user);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordManagerApp
{
    public interface ICryptoPersistUserService
    {
        ICollection<EncryptedPasswordManagerUser> GetUsers();
        void SaveUsers(IEnumerable<PasswordManagerUser> users);
    }
}

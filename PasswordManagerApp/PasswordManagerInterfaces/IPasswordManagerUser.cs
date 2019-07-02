using System;
using System.Collections.Generic;

namespace PasswordManagerInterfaces
{
    public interface IPasswordManagerUser
    {
        string Name { get; }
        ICollection<IPasswordProperties> Passwords { get; }
        bool CheckPassword(string password);
    }
}

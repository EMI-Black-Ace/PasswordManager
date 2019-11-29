using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerApp
{
    public interface IUserProvider
    {
        string UserName { get; }
        string MasterPassword { get; }
    }
}

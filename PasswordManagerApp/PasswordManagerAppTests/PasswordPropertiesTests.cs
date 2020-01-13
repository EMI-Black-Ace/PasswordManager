using Moq;
using PasswordManagerApp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerAppTests
{
    public class PasswordPropertiesTests
    {
        private readonly PasswordManagerUser user;
        private readonly PasswordProperties password;
        
        public PasswordPropertiesTests()
        {
            user = PasswordManagerUser.Factory.CreateNewUser("mock user", "mock password");
            password = new PasswordProperties(user);
        }


    }
}

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
            Mock<IUserProvider> provider = new Mock<IUserProvider>();
            provider.Setup(m => m.UserName).Returns("mock user");
            provider.Setup(m => m.MasterPassword).Returns("mock password");

            Mock<HashAlgorithm> hashAlgorithm = new Mock<HashAlgorithm>();
            hashAlgorithm.Setup(m => m.ComputeHash(It.IsAny<byte[]>())).Returns<byte[]>(b => b); //skip actually hashing stuff for better visibility

            user = new PasswordManagerUser(provider.Object, hashAlgorithm.Object);
            password = new PasswordProperties(user);
        }


    }
}

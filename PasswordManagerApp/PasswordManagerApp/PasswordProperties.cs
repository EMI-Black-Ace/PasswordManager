using PasswordManagerInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerApp
{
    public class PasswordProperties : IPasswordProperties
    {
        public string Name { get; set; }
        public uint Length { get; set; }
        public bool MustHaveCaps { get; set; }
        public bool MustHaveLower { get; set; }
        public bool MustHaveSpc { get; set; }
        public bool MustNotHaveSpc { get; set; }

        private readonly IPasswordManagerUser passwordManagerUser;
        private Guid passwordSeed = Guid.NewGuid();

        public void ChangeThisPassword()
        {
            passwordSeed = Guid.NewGuid();
        }

        public string GeneratePassword(string masterPassword)
        {
            throw new NotImplementedException();
        }

        
    }
}

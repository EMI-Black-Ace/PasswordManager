using Newtonsoft.Json;
using PasswordManagerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Changes the stored randomizer seed involved in creating the password.  Not reversible.
        /// </summary>
        public void ChangeThisPassword()
        {
            passwordSeed = Guid.NewGuid();
        }

        /// <summary>
        /// Generates a password mingling the user's master password, stored hash, internal randomizer and object properties
        /// </summary>
        /// <param name="masterPassword">The user's master password</param>
        /// <returns>a generated password</returns>
        public string GeneratePassword(string masterPassword)
        {
            if(!passwordManagerUser.CheckPassword(masterPassword))
            {
                throw new ArgumentException("Password did not match", nameof(masterPassword));
            }

            string overallComponents = $"{masterPassword}" +
                $"{passwordManagerUser.Name}" +
                $"{new string(passwordManagerUser.PasswordHash.Cast<char>().ToArray())}" +
                $"{passwordSeed}" +
                $"{Length}" +
                $"{MustHaveCaps}" +
                $"{MustHaveLower}" +
                $"{MustHaveSpc}" +
                $"{MustNotHaveSpc}";

            byte[] passwordBytes = passwordManagerUser.HashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(overallComponents));

            throw new NotImplementedException();
        }

        public override string ToString() => $"{passwordManagerUser.Name}: {Name}";

        public PasswordProperties(IPasswordManagerUser passwordManagerUser) => this.passwordManagerUser = passwordManagerUser ?? throw new ArgumentNullException(nameof(passwordManagerUser));
    }
}

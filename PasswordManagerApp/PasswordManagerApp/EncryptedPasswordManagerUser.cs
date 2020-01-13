using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerApp
{
    public class EncryptedPasswordManagerUser
    {
        public string UserName { get; internal set; }
        public byte[] CryptoRepresentation { get; internal set; }

        /// <summary>
        /// Retrieves a PasswordManagerUser object from the encrypted representation.
        /// Only succeeds if the master password matches that of the encrypted representation.
        /// </summary>
        /// <param name="masterPassword"></param>
        /// <returns></returns>
        public PasswordManagerUser RetrieveUserData(string masterPassword)
        {
            throw new NotImplementedException();
        }
    }
}

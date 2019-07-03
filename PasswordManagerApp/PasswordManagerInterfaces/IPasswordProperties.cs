using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerInterfaces
{
    public interface IPasswordProperties
    {
        /// <summary>
        /// The name of this generated password, usually something that reminds the user of where the password will be used.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Length the generated password must be
        /// </summary>
        uint Length { get; set; }

        /// <summary>
        /// True if the password must have an uppercase letter
        /// </summary>
        bool MustHaveCaps { get; set; }

        /// <summary>
        /// True if the password must have a lowercase letter
        /// </summary>
        bool MustHaveLower { get; set; }

        /// <summary>
        /// True if the password must have a special character (~!@#$%^...)
        /// </summary>
        bool MustHaveSpc { get; set; }

        /// <summary>
        /// True if the password must not have a special character
        /// </summary>
        bool MustNotHaveSpc { get; set; }

        /// <summary>
        /// Generates a password given the input master password and the given password properties
        /// </summary>
        /// <param name="masterPassword">The user's master password.  Must be correct or it will not generate the correct password.</param>
        /// <returns>A procedurally-generated password.  For a given user, master password, parameters and iteration it will always generate the same password.</returns>
        string GeneratePassword(string masterPassword);

        /// <summary>
        /// Changes an internal condition so the generated password is different from before.
        /// </summary>
        void ChangeThisPassword();
    }
}

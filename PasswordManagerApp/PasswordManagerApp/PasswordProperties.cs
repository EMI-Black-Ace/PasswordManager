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
        public int Length { get => _length; set => _length = value > 0 ? value : throw new ArgumentException("must be greater than 0"); }
        public bool MustHaveCaps { get; set; }
        public bool MustHaveLower { get; set; }
        public bool MustHaveSpc { get; set; }
        public bool MustNotHaveSpc { get; set; }

        private readonly IPasswordManagerUser passwordManagerUser;
        private Guid passwordSeed = Guid.NewGuid();
        private int _length = 8;
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
            if (!passwordManagerUser.CheckPassword(masterPassword))
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

            passwordBytes = passwordBytes.ReduceOrExpand(Length, (x, y) => (byte)((x + y) % byte.MaxValue));

            //set up functions to select characters for password
            Func<byte, char>[] forcingFunctions;
            if (MustNotHaveSpc)
            {
                forcingFunctions = Enumerable.Repeat<Func<byte, char>>(ForceToNonSpecial, Length).ToArray();
            }
            else
            {
                forcingFunctions = Enumerable.Repeat<Func<byte, char>>(ForceToPrintable, Length).ToArray();
            }

            //TODO finish picking forcing functions

            throw new NotImplementedException();
        }

        private char ForceBetween(char arg, char min, char max)
        {
            return (char)(arg % (max - min) + min);
        }

        private char ForceToPrintable(byte characterSeed)
        {
            return ForceBetween((char)characterSeed, '!', '~');
        }

        private char ForceToUpper(byte characterSeed)
        {
            return ForceBetween((char)characterSeed, 'A', 'Z');
        }

        private char ForceToLower(byte characterSeed)
        {
            return ForceBetween((char)characterSeed, 'a', 'z');
        }

        private char ForceToNumeric(byte characterSeed)
        {
            return ForceBetween((char)characterSeed, '0', '9');
        }

        private char ForceToNonSpecial(byte characterSeed)
        {
            if (characterSeed >= (byte.MaxValue * 2) / 3)
                return ForceToLower(characterSeed);
            else if (characterSeed >= byte.MaxValue / 3)
                return ForceToUpper(characterSeed);
            else
                return ForceToNumeric(characterSeed);
        }

        private char ForceToSpecial(byte characterSeed)
        {
            if (characterSeed >= 'a')
                return ForceBetween((char)characterSeed, '{', '~');
            else if (characterSeed >= 'A')
                return ForceBetween((char)characterSeed, '[', '`');
            else if (characterSeed >= '0')
                return ForceBetween((char)characterSeed, ':', '@');
            else
                return ForceBetween((char)characterSeed, '!', '/');
        }

        public override string ToString() => $"{passwordManagerUser.Name}: {Name}";

        public PasswordProperties(IPasswordManagerUser passwordManagerUser) => this.passwordManagerUser = passwordManagerUser ?? throw new ArgumentNullException(nameof(passwordManagerUser));
    }
}

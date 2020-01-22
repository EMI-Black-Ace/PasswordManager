﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordManagerApp
{
    public class PasswordProperties
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public bool MustHaveCaps { get; set; }
        public bool MustHaveLower { get; set; }
        public bool MustHaveNumber { get; set; }
        private bool mustHaveSpc = false;
        public bool MustHaveSpc
        {
            get => mustHaveSpc;
            set
            {
                if(value && mustNotHaveSpc)
                {
                    MustNotHaveSpc = false;
                }
                mustHaveSpc = value;
            }
        }
        private bool mustNotHaveSpc = false;
        public bool MustNotHaveSpc
        {
            get => mustNotHaveSpc;
            set
            {
                if(value && mustHaveSpc)
                {
                    MustHaveSpc = false;
                }
                mustNotHaveSpc = value;
            }
        }

        private readonly PasswordManagerUser passwordManagerUser;
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
                $"{Encoding.ASCII.GetChars(passwordManagerUser.PasswordHash)}" +
                $"{passwordSeed}" +
                $"{Length}" +
                $"{MustHaveCaps}" +
                $"{MustHaveLower}" +
                $"{MustHaveNumber}" + 
                $"{MustHaveSpc}" +
                $"{MustNotHaveSpc}";

            byte[] passwordBytes = passwordManagerUser.HashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(overallComponents))
                .ReduceOrExpand(Length, (x,y) => (byte)(x + y));

            return new string(passwordBytes.Zip(ConvertByteToCharFuncs(), (b, func) => func(b)).ToArray());
        }

        public override string ToString() => $"{passwordManagerUser.Name}: {Name}";

        internal PasswordProperties(PasswordManagerUser passwordManagerUser) => this.passwordManagerUser = passwordManagerUser ?? throw new ArgumentNullException(nameof(passwordManagerUser));

        private IEnumerable<Func<byte, char>> ConvertByteToCharFuncs()
        {
            List<Func<byte, char>> funcs = new List<Func<byte, char>>();
            int orderSelector = passwordSeed.ToByteArray()[0] % 4;
            switch (orderSelector)
            {
                case 0:
                    yield return SelectMethodOrDefault(ByteToCapital, MustHaveCaps);
                    yield return SelectMethodOrDefault(ByteToLowercase, MustHaveLower);
                    yield return SelectMethodOrDefault(ByteToNumber, MustHaveNumber);
                    yield return SelectMethodOrDefault(ByteToSpecial, MustHaveSpc);
                    break;
                case 1:
                    yield return SelectMethodOrDefault(ByteToLowercase, MustHaveLower);
                    yield return SelectMethodOrDefault(ByteToNumber, MustHaveNumber);
                    yield return SelectMethodOrDefault(ByteToSpecial, MustHaveSpc);
                    yield return SelectMethodOrDefault(ByteToCapital, MustHaveCaps);
                    break;
                case 2:
                    yield return SelectMethodOrDefault(ByteToNumber, MustHaveNumber);
                    yield return SelectMethodOrDefault(ByteToSpecial, MustHaveSpc);
                    yield return SelectMethodOrDefault(ByteToCapital, MustHaveCaps);
                    yield return SelectMethodOrDefault(ByteToLowercase, MustHaveLower);
                    break;
                case 3:
                    yield return SelectMethodOrDefault(ByteToSpecial, MustHaveSpc);
                    yield return SelectMethodOrDefault(ByteToCapital, MustHaveCaps);
                    yield return SelectMethodOrDefault(ByteToLowercase, MustHaveLower);
                    yield return SelectMethodOrDefault(ByteToNumber, MustHaveNumber);
                    break;
            }
            while (true)
                yield return SelectMethodOrDefault(null, false);
        }

        private char ByteToPrintable(byte x)
        {
            return CharacterTables.printableTable.WrapSelectChar(x);
        }

        private char ByteToCapital(byte x)
        {
            return CharacterTables.upperTable.WrapSelectChar(x);
        }

        private char ByteToLowercase(byte x)
        {
            return CharacterTables.lowerTable.WrapSelectChar(x);
        }

        private char ByteToNumber(byte x)
        {
            return CharacterTables.numberTable.WrapSelectChar(x);
        }

        private char ByteToSpecial(byte x)
        {
            return CharacterTables.spcTable.WrapSelectChar(x);
        }

        private char ByteToNonSpecial(byte x)
        {
            return CharacterTables.nonSpcTable.WrapSelectChar(x);
        }

        private Func<byte, char> SelectMethodOrDefault(Func<byte, char> method, bool selector)
        {
            if (selector)
                return method;
            else if (MustNotHaveSpc)
                return ByteToNonSpecial;
            else
                return ByteToPrintable;
        }
    }
}

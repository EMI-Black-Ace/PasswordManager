using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerApp
{
    public static class CharacterTables
    {
        public const string spcTable = "`-=[]\\;',./~!@#$%^&*()_+{}|:\"<>? ";
        public const string nonSpcTable = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        public const string lowerTable = "abcdefghijklmnopqrstuvwxyz";
        public const string upperTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string numberTable = "0123456789";
        public const string printableTable = "`-=[]\\;',./~!@#$%^&*()_+{}|:\"<>? 1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        public static char WrapSelectChar(this string source, int selectedChar) => source[selectedChar % source.Length];
    }
}

using System;

namespace ExtensionClasses
{
    public static class TempExtension
    {
        public static void AddChar(this Temp temp, char c)
        {
            temp.StringTemp += c;
        }
    }
}

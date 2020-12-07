// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Linq;

namespace TDDKata
{
    internal class StringCalc
    {
        private const int ERROR = -1;
        internal int Sum(string input)
        {
            if (input == null)
                return ERROR;
            if (input.Equals(string.Empty))
                return 0;
            string[] arguments = input.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string argument in arguments)
            {
                if (!int.TryParse(argument, out int intArgument) || intArgument < 0)
                    return ERROR;
            }
            var intArguments = arguments.Select(a => int.Parse(a));
            return intArguments.Sum();
        }
    }
}
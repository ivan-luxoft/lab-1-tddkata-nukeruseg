// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDKata
{
    internal class StringCalc
    {
        private const int ERROR = -1;
        internal int Sum(string input)
        {
            var argumentsForParse = input;
            string[] delimeters = new string[] { ",", "\n" };

            if (argumentsForParse == null)
                return ERROR;

            if (argumentsForParse.Equals(string.Empty))
                return 0;

            argumentsForParse = ExtractCustomDelimeter(argumentsForParse, ref delimeters);

            var intArguments = ConvertArgumentStringToIntArray(argumentsForParse, delimeters);

            if(intArguments == null)
                return ERROR;

            return intArguments.Sum();
        }

        /// <summary>
        /// Разделяет строку по переданным разделителям и возвращает список чисел
        /// </summary>
        /// <param name="argumentsForParse">Строка для разделения</param>
        /// <param name="delimeters">Массив разделителей</param>
        /// <returns>Список чисел (полученных из строки) или NULL в случае ошибки</returns>
        private static List<int> ConvertArgumentStringToIntArray(string argumentsForParse, string[] delimeters)
        {
            var result = new List<int>();
            string[] arguments = argumentsForParse.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

            foreach (string argument in arguments)
            {
                if (int.TryParse(argument, out int intArgument) && intArgument >= 0)
                    result.Add(intArgument);
                else
                    return null;
            }

            return result;
        }

        /// <summary>
        /// Выделяет настраиваемый разделитель и возвращает строку содержающую только аргументы
        /// </summary>
        /// <param name="argumentsForParse">Строка с настраиваемым разделителем и аргументами</param>
        /// <param name="delimeters">Массив разделителей, в него будет записан настраиваемый, если он есть в строке</param>
        /// <returns>Входная строка argumentsForParse очищенная от настроек разделителя, только аргументы</returns>
        private static string ExtractCustomDelimeter(string argumentsForParse, ref string[] delimeters)
        {
            var result = argumentsForParse;
            if (result.StartsWith("//") && result.Contains("\n"))
            {
                int newLineIdx = result.IndexOf("\n");
                string delimeterFormatPart = result.Substring(0, newLineIdx);
                string delimeter = delimeterFormatPart.Substring(2);

                delimeters = new string[] { delimeter };

                var copyStringFrom = newLineIdx + 1;
                var copyStringLength = argumentsForParse.Length - newLineIdx - 1;
                return result.Substring(copyStringFrom, copyStringLength);
            }

            return result;
        }
    }
}
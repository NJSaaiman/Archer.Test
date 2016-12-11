using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Archer.Test.Helpers
{
    public static class Validations
    {
        public static bool IsCellNUmberValid(string cellNumber)
        {
            if (cellNumber.Length != 10) { return false; }

            return IsNumeric(cellNumber);
        }

        public static bool IsNumeric(string cellNumber)
        {
            return Regex.IsMatch(cellNumber, "^[0-9]*$", RegexOptions.CultureInvariant);
        }
    }
}

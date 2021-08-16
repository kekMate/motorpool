using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace motorpool
{
    class Validators
    {
        static public bool IsValidPath(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("\nA fájlt megtaláltam.");
                return true;
            }
            else
            {
                Console.WriteLine("\nA fájl nem található.");
                return false;
            }
        }

        static public bool LPlateIsValid(string inLPlate)
        {
            inLPlate = inLPlate.ToUpper();
            const string lPlatePattern = @"[A-Z]{3}-\d{3}";
            Regex lPlateRegex = new Regex(lPlatePattern);
            if (lPlateRegex.IsMatch(inLPlate))
            { return true; }
            else
            { return false; }
        }

    }
}

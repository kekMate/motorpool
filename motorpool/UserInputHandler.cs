using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motorpool
{
    class UserInputHandler
    {
        public string UserPathInput()
        {
            string csvPath;
            Validators inputValidator = new Validators();
            do
            {
                Console.WriteLine("Adja meg a nyilvántartás elérési útját (.csv):");
                csvPath = Console.ReadLine();
            }
            while (!(Validators.IsValidPath(csvPath)));
            return csvPath;
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motorpool
{
    class Program
    {
        static void Main(string[] args)
        {
            #region TEST float tryparse
            /*
             * string floatInString = "2";
            float floatOutput;
            if (!float.TryParse(floatInString, out floatOutput)) 
            {
                Console.WriteLine("TryParse elbukott");
                
            }
            else { Console.WriteLine($"Így vette át: {floatOutput}"); }
            */
            #endregion

            #region TEST datetime.now
            //Console.WriteLine($"{DateTime.Now.ToString()}");
            #endregion

            #region TEST printcar
            /*
            Car myCar = new Car("asd-123", "NG", "Ford Fiesta", 2.2f, 2.3f, 2.4f, 2.5f);
            myCar.PrintCar();
            Console.ReadLine();
            */
            #endregion

            UserInputHandler userInputHandler = new UserInputHandler();

            CSVprocesser csvProcesser = new CSVprocesser(userInputHandler.UserPathInput());
            csvProcesser.ProcessCSV();

            PressureEqualizer pressureEqualizer = new PressureEqualizer();
            pressureEqualizer.SetPressures(csvProcesser.carList);

            Console.ReadLine();
        }
        
    }
}

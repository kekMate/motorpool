using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace motorpool
{
    class CSVprocesser
    {
        string[] rawLines;
        string path;
        string truePath;
        List<int> rowsWithError = new List<int>();
        // itt is...
        public List<Car> carList = new List<Car>();

        public CSVprocesser(string filePath)
        {
            path = filePath;
            truePath = Path.GetFullPath(path);
            rawLines = File.ReadAllLines(path);
        }

        public void ProcessCSV()
        {
            StreamWriter CSVerrorLogger = new StreamWriter(
                $"CSV_error_log_{DateTime.Now.ToString().Replace(":", String.Empty).Replace(".", String.Empty)}.txt");
            CSVerrorLogger.WriteLine($"Errors while processing \n{truePath}\non\n{DateTime.Now.ToString()}:\n\n");

            #region Index legend:
            /* [0]: LicensePlate 
             * [1]: Owner
             * [2]: MakeModel
             * [3]: FRONT Left Press.
             * [4]: FRONT Right Press.
             * [5]: REAR Left Press.
             * [6]: REAR Right Press.           */
            #endregion

            int i;
            void closeErrorLineInLogFile()
            {
                CSVerrorLogger.WriteLine($"*** Line #{i+1} has been omitted! ***\n");
                rowsWithError.Add(i);
            }
            bool myFloatParser(string strToParse, out float parsedFloat)
            {
                return float.TryParse(strToParse, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out parsedFloat);
            }
            for (i=0; i<rawLines.Length; i++)
            {
                float fLEFT, fRIGHT, rLEFT, rRIGHT;

                bool errorInLine = false;
                string[] splitLineArr = rawLines[i].Split(';');

                #region CSV error checklist
                if (splitLineArr.Length!=7)  //No. of ";" in line check
                {
                    CSVerrorLogger.WriteLine($"Line #{i+1}: No. of fields incorrect;");
                    closeErrorLineInLogFile();
                    continue;
                }
                if (!Validators.LPlateIsValid(splitLineArr[0].Trim())) //License plate check
                {
                    CSVerrorLogger.WriteLine($"Line #{i+1}: License plate format is invalid: \"{ splitLineArr[0]}\";");
                    errorInLine = true;
                }
                if (splitLineArr[1].Trim().Length<3 || String.IsNullOrWhiteSpace(splitLineArr[1])) //Owner check
                {
                    CSVerrorLogger.WriteLine($"Line #{i+1}: Owner is invalid: \"{splitLineArr[1]}\";");
                    errorInLine = true;
                }
                if (splitLineArr[2].Trim().Length < 3 || String.IsNullOrWhiteSpace(splitLineArr[2])) //Make/model check
                {
                    CSVerrorLogger.WriteLine($"Line #{i+1}: Make/model is invalid: \"{splitLineArr[2]}\";");
                    errorInLine = true;
                }
                if (!myFloatParser(splitLineArr[3], out fLEFT)) //FRONT Left Press. check
                {
                    CSVerrorLogger.WriteLine($"Line #{i+1}: Front left pressure invalid: \"{splitLineArr[3]}\";");
                    errorInLine = true;
                }
                if (!myFloatParser(splitLineArr[4], out fRIGHT)) //FRONT Right Press. check
                {
                    CSVerrorLogger.WriteLine($"Line #{i+1}: Front right pressure invalid: \"{splitLineArr[4]}\";");
                    errorInLine = true;
                }
                if (!myFloatParser(splitLineArr[5], out rLEFT)) //REAR Left Press. check
                {
                    CSVerrorLogger.WriteLine($"Line #{i+1}: Rear left pressure invalid: \"{splitLineArr[5]}\";");
                    errorInLine = true;
                }
                if (!myFloatParser(splitLineArr[6], out rRIGHT)) //REAR Right Press. check
                {
                    CSVerrorLogger.WriteLine($"Line #{i+1}: Rear right pressure invalid: \"{splitLineArr[6]}\";");
                    errorInLine = true;
                }
                #endregion
                if (errorInLine)
                {
                    closeErrorLineInLogFile();
                    continue;
                }
                
                else
                {
                    carList.Add(new Car(
                        splitLineArr[0], splitLineArr[1], splitLineArr[2], fLEFT, fRIGHT, rLEFT, rRIGHT));
                }
            }
            CSVerrorLogger.Close();
        }
                

    }
}

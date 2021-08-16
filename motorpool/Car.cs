using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motorpool
{
    class Car
    {
        //itt is azért public, mert balfék vagyok
        public LicensePlate lPlate;
        private string owner;
        private string makeModel;
        /*
        public PressuresOnAxle FrontAxlePress
        { public get; private set; }
        Ezt nézzük át kérlek, mert a PressureEqualizer classban nem tudtam
        máshogy nyomást lekérni/változtatni
        */
        public PressuresOnAxle frontAxlePress;
        public PressuresOnAxle rearAxlePress;

        public Car(string inLPlate, string inOwner, string inMakeModel,
            float inFLPress, float inFRPress, float inRLPress, float inRRPress)
        {
            lPlate = new LicensePlate(inLPlate);
            owner = inOwner;
            makeModel = inMakeModel;
            frontAxlePress = new PressuresOnAxle(inFLPress, inFRPress);
            rearAxlePress = new PressuresOnAxle(inRLPress, inRRPress);
        }

        public void PrintCar()
        {
            Console.WriteLine($"\nRendszám: {this.lPlate.LPlateValue}");
            Console.WriteLine($"Tulaj: {this.owner}");
            Console.WriteLine($"Gyártmány és típus: {this.makeModel}");
            Console.WriteLine($"Keréknyomások az első tengelyen [bar]: " +
                $"Bal: {this.frontAxlePress.LeftPressOnAxle} Jobb: {this.frontAxlePress.RightPressOnAxle}");
            Console.WriteLine($"Keréknyomások a hátsó tengelyen [bar]: " +
                $"Bal: {this.rearAxlePress.LeftPressOnAxle} Jobb: {this.rearAxlePress.RightPressOnAxle}\n");
        }

    }
}

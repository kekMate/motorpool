using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace motorpool
{
    class PressureEqualizer
    {
        const float pressureDifferenceThreshold = 0.05f;
        public enum PressState { under, over, equal, actionRequired};
        public PressState GetPressState(PressuresOnAxle pressuresOnAxle)
        {
            if (pressuresOnAxle.GetAveragePressOnAxle() <1.5f)
            {
                return PressState.under;
            }
            else if (pressuresOnAxle.GetAveragePressOnAxle() > 3.0f)
            {
                return PressState.over;
            }
            else if (Math.Abs(
                pressuresOnAxle.GetAveragePressOnAxle() -
                pressuresOnAxle.LeftPressOnAxle)
                <pressureDifferenceThreshold / 2)
            {
                return PressState.equal;
            }
            else
            {
                return PressState.actionRequired;
            }
        }

        public float GetPressTarget(PressuresOnAxle pressuresOnAxle)
        {
            PressState pState = GetPressState(pressuresOnAxle);
            switch (pState)
            {
                case PressState.under:
                    return 1.5f;
                case PressState.over:
                    return 3.0f;
                case PressState.equal:
                    return 0f;
                case PressState.actionRequired:
                    return pressuresOnAxle.GetAveragePressOnAxle();
                default:
                    return 100f;
            }
        }
        
        public void SetPressures(List<Car> carList)
        {
            StreamWriter serviceLogWriter = new StreamWriter(
            $"service_log_{DateTime.Now.ToString().Replace(":", String.Empty).Replace(".", String.Empty)}.txt");
            serviceLogWriter.WriteLine($"Service log of \n{ DateTime.Now.ToString()}:\n\n");

            foreach (Car car in carList)
            {
                serviceLogWriter.WriteLine($"\nRendszám: {car.lPlate.LPlateValue}");
                #region Front axle
                if (GetPressState(car.frontAxlePress)==PressState.equal)
                {
                    serviceLogWriter.WriteLine($"Az első tengelyen a nyomások rendben.");
                }
                else
                {
                    serviceLogWriter.WriteLine($"Bal első Δp= " +
                        $"{GetPressTarget(car.frontAxlePress) - car.frontAxlePress.LeftPressOnAxle}");
                    serviceLogWriter.WriteLine($"Jobb első Δp= " +
                        $"{GetPressTarget(car.frontAxlePress) - car.frontAxlePress.RightPressOnAxle}");
                    
                    //car.frontAxlePress.LeftPressOnAxle = GetPressTarget(car.frontAxlePress);
                    //car.frontAxlePress.RightPressOnAxle = GetPressTarget(car.frontAxlePress);
                }
                #endregion

                #region Rear Axle
                if (GetPressState(car.rearAxlePress) == PressState.equal)
                {
                    serviceLogWriter.WriteLine($"A hátsó tengelyen a nyomások rendben.");
                }
                else
                {
                    serviceLogWriter.WriteLine($"Bal hátsó Δp= " +
                        $"{GetPressTarget(car.rearAxlePress) - car.rearAxlePress.LeftPressOnAxle}");
                    serviceLogWriter.WriteLine($"Jobb hátsó Δp= " +
                        $"{GetPressTarget(car.rearAxlePress) - car.rearAxlePress.RightPressOnAxle}");

                    //car.rearAxlePress.LeftPressOnAxle = GetPressTarget(car.rearAxlePress);
                    //car.rearAxlePress.RightPressOnAxle = GetPressTarget(car.rearAxlePress);
                }
                #endregion
            }
        }
    }
}

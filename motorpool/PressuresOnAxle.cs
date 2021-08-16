using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motorpool
{
    class PressuresOnAxle
    {
        public float LeftPressOnAxle
        { get; set; }

        public float RightPressOnAxle
        { get; set; }

        public float GetAveragePressOnAxle()
        {
            return
                (LeftPressOnAxle + RightPressOnAxle) / 2;
        }

        public PressuresOnAxle(float leftP, float rightP)
        {
            LeftPressOnAxle = leftP;
            RightPressOnAxle = rightP;
        }

    }
}

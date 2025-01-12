using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_A3
{
    /// <summary>
    /// Holds enums for "Gender" and "Activity Level"
    /// </summary>
    public static class Enums
    {
        public enum Gender
        {
            Male,
            Female,
            Other,
            Unknown
        }

        public enum ActivityLevel
        {
            High,
            Mid,
            Low,
            Unknown
        }
    }
}

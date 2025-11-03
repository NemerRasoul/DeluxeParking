using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    internal class Car : Vehicle
    {
       internal bool IsElectric { get; set; }
       internal override double RequiredSpaces => 1;

        public Car(string color, bool isElectric) : base (color)
        {
            IsElectric = isElectric;
        }
        
        internal override string GetVehicleInfo() 
        {
            string CarType = IsElectric ? "Elbil" : "Bensinbil";
            return $"{CarType}, {RegistrationNumber}, {Color}";
        }
    }
}

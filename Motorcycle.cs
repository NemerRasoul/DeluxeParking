using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    internal class Motorcycle : Vehicle
    {
        internal string Brand {  get; set; }
        internal override double RequiredSpaces => 0.5;
        public Motorcycle(string color, string brand) : base (color)
        {
            Brand = brand;
        }

        internal override string GetVehicleInfo()
        {
            return $"Motorcykel,  {RegistrationNumber}, {Color}, {Brand}";
        }
    }
}

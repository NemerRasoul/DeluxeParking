using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    internal class Bus : Vehicle
    {
        internal int PassengerCount { get; set; }
        internal override double RequiredSpaces => 2;

        public Bus(string color, int passengerCount) : base (color)
        {
            PassengerCount = passengerCount;
        }

        internal override string GetVehicleInfo()
        {
            return $"Buss, {Color}, {RegistrationNumber}, {PassengerCount} passagerare";
        }
    }
}

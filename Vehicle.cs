using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    abstract class Vehicle
    {
       internal string RegistrationNumber { get; set; }
       internal string Color { get; set; }
       internal DateTime ParkedAt { get; set; }
       internal abstract double RequiredSpaces { get; }
       internal abstract string GetVehicleInfo();

        protected Vehicle(string color)
        {
            RegistrationNumber = LicensePlateGenerator.GenerateRegistrationNumber();
            Color = color;
            ParkedAt = DateTime.Now;
        }
    }
}

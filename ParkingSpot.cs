using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    internal class ParkingSpot 
    {
        internal int SpotNumber { get; set; }
        internal List<Vehicle> Vehicles { get; set; }
        public ParkingSpot(int spotNumber)
        {
            SpotNumber = spotNumber;
            Vehicles = new List<Vehicle>();
        }

        internal double SpaceUsed 
        {
            get { double total = 0;
                foreach (var vehicle in Vehicles) 
                {
                    total += vehicle.RequiredSpaces;
                }
                return total;
            }
        }

        // Kollar om det finns plats för fordonet
        // Denna metod fungerar inte för bussar eftersom Buss kräver två platser
        internal bool HasSpaceFor(Vehicle vehicle) 
        {
            return (SpaceUsed + vehicle.RequiredSpaces) <= 1.0;
        }

        internal bool IsFull 
        {
            get { return SpaceUsed >= 1.0; }
        }

        // Parkerar fordonet på denna spot
        // Denna metod är för Car och Motorcycle, inte för Buss
        // Bussar kräver två platser och hanteras i ParkingLot.cs
        internal void ParkVehicle (Vehicle vehicle) 
        {
            if (HasSpaceFor(vehicle)) 
            {
                Vehicles.Add(vehicle);
                Console.WriteLine($"Fordon {vehicle.RegistrationNumber} har parkerats på plats {SpotNumber}");
            }
            else 
            {
                Console.WriteLine($"Ingen plats på {SpotNumber}");
            }
        }

        internal void RemoveVehicle(string RegNumber) 
        {
            Vehicle VehicleToRemove = Vehicles.Find(vehicle => vehicle.RegistrationNumber == RegNumber);

            if (VehicleToRemove != null)
            {
                Vehicles.Remove(VehicleToRemove);
                Console.WriteLine($"Fordon {RegNumber} har lämnat plats {SpotNumber}");
            }
        }
    }
}

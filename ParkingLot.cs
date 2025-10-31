using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    internal class ParkingLot
    {
        internal List<ParkingSpot> ParkingSpots { get; private set; }

        internal double PricePerMinute { get; set; } = 1.5;

        public ParkingLot(int numberOfSpots = 15)
        {
            ParkingSpots = new List<ParkingSpot>();
            for (int i = 1; i <= numberOfSpots; i++)
            {
                ParkingSpots.Add(new ParkingSpot(i));
            }
        }

        internal bool AddVehicleToSpot(Vehicle vehicle) 
        {
            if (vehicle == null) return false;

            switch (vehicle) 
            {
                case Bus bus:
                    for (int i = 0; i < ParkingSpots.Count; i++) 
                    {
                        var firstSpot = ParkingSpots[i];
                        var secondSpot = ParkingSpots[i + 1];

                        // Buss behöver två tomma platser i rad
                        if (firstSpot.SpaceUsed == 0 && secondSpot.SpaceUsed == 0) 
                        {
                            // Läggs direkt i listan för att undvika ParkVehicle metoden
                            firstSpot.Vehicles.Add(bus);
                            secondSpot.Vehicles.Add(bus);

                            Console.WriteLine($"Buss {bus.RegistrationNumber} parkerad på platser {firstSpot.SpotNumber}-{secondSpot.SpotNumber}");
                            return true;
                        }
                    }
                    Console.WriteLine("Ingen plats för buss");
                    return false;

                    case Car car:
                    foreach (var spot in ParkingSpots) 
                    {
                        if (spot.HasSpaceFor(car)) 
                        {
                            spot.ParkVehicle(car);
                            return true;
                        }
                    }
                    return false;

                    case Motorcycle motorcycle:
                    foreach (var spot in ParkingSpots) 
                    {
                        if (spot.HasSpaceFor(motorcycle)) 
                        {
                            spot.ParkVehicle(motorcycle);
                            return true;
                        }
                    }
                    return false;

                    default:
                    return false;
            }
        }
    }
}

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
                    for (int i = 0; i < ParkingSpots.Count - 1; i++)
                    {
                        var firstSpot = ParkingSpots[i];
                        var secondSpot = ParkingSpots[i + 1];

                        // Buss behöver två tomma platser i rad
                        if (firstSpot.SpaceUsed == 0 && secondSpot.SpaceUsed == 0)
                        {
                            // Läggs direkt i listan för att undvika ParkVehicle metoden
                            firstSpot.Vehicles.Add(bus);
                            secondSpot.Vehicles.Add(bus);

                            Console.WriteLine($"Fordon: {bus.RegistrationNumber} parkerad på platser {firstSpot.SpotNumber}-{secondSpot.SpotNumber}");
                            return true;
                        }
                    }
                    return false;

                case Car car:
                    foreach (var spot in ParkingSpots)
                    {
                        if (spot.SpaceUsed == 0 && spot.HasSpaceFor(car))
                        {
                            spot.ParkVehicle(car);
                            return true;
                        }
                    }
                    return false;

                case Motorcycle motorcycle:
                    foreach (var spot in ParkingSpots)
                    {
                        // Försök dela en plats med en annan motorcykel
                        if (spot.SpaceUsed >= 0.01 && spot.SpaceUsed <= 0.5 && spot.HasSpaceFor(motorcycle))
                        {
                            spot.ParkVehicle(motorcycle);
                            return true;
                        }
                    }

                    // Om ingen delad plats hittades, leta efter en tom plats
                    foreach (var spot in ParkingSpots)
                    {
                        if (spot.SpaceUsed == 0 && spot.HasSpaceFor(motorcycle))
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

        internal bool RemoveVehicle(string regNumber)
        {
            Vehicle foundVehicle = null;

            foreach (var spot in ParkingSpots)
            {
                foundVehicle = spot.Vehicles.Find(vehicle => vehicle.RegistrationNumber == regNumber);

                if (foundVehicle != null)
                    break;
            }

            if (foundVehicle == null)
            {
                Console.WriteLine($"Fordon med registreringsnummer {regNumber} hittades inte");
                return false;
            }

            TimeSpan timeParked = DateTime.Now - foundVehicle.ParkedAt;
            double price = timeParked.TotalMinutes * PricePerMinute;

            // Ta bort fordonet från alla platser den kan vara på
            foreach (var spot in ParkingSpots)
            {
                if (spot.ContainsVehicle(regNumber))
                {
                    spot.RemoveVehicle(regNumber);
                }
            }

            Console.WriteLine($"\nFordon med registreringsnummer {regNumber} har checkat ut");
            Console.WriteLine($"Tid parkerad: {timeParked.TotalMinutes} minuter");
            Console.WriteLine($"Pris: {price} kr");

            return true;
        }

        internal void PrintStatus()
        {
            Console.WriteLine("\nParkerings Status \n");

            foreach (var spot in ParkingSpots)
            {
                if (spot.Vehicles.Count == 0)
                {
                    Console.WriteLine($"Plats {spot.SpotNumber}: Tom");
                }
                else
                {
                    foreach (var vehicle in spot.Vehicles.Distinct())
                    {
                        string vehicleInfo = GenericHelpers.DescribeVehicle(vehicle);
                        Console.WriteLine($"Plats {spot.SpotNumber}: {vehicleInfo}");
                    }
                }
            }
        }
    }
}

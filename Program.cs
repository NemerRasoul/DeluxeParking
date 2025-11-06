

namespace DeluxeParking1
{
    internal class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            ParkingLot parkingLot = new ParkingLot();
            bool running = true;

            while (running) 
            {
                Console.WriteLine("\n-- Parkeringssystem --");
                Console.WriteLine("1. Parkera fordon");
                Console.WriteLine("2. Checka ut fordon");
                Console.WriteLine("3. Visa parkeringsstatus");
                Console.WriteLine("4. Sök fordon");
                Console.WriteLine("5. Avsluta\n");

                string input = Console.ReadLine();

                switch (input) 
                {
                    case "1":
                        ParkVehicle(parkingLot);
                        break;

                    case "2":
                        CheckOutVehicle(parkingLot);
                        break;

                    case "3":
                        parkingLot.PrintStatus();
                        break;

                        case "4":
                            SearchVehicle(parkingLot);
                            break;

                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen");
                        break;
                        
                }
            }
        }

        static void ParkVehicle(ParkingLot parkingLot) 
        {
            string[] colors = { "Svart", "Vit", "Blå", "Gul", "Grön", "Röd" };
            string[] mcBrands = { "Harley", "Honda", "Yamaha", "Kawasaki", "KTM" };

            Console.WriteLine("Välj fordon: 1 = Bil, 2 = MC, 3 = Buss (Enter för random)");

            string vehicleChoice = Console.ReadLine();
            int vehicleType;

            if (vehicleChoice == string.Empty)
            {
                vehicleType = random.Next(1, 4);
            }

            else if (!int.TryParse(vehicleChoice, out vehicleType) || vehicleType < 1 || vehicleType > 3)
            {
                Console.WriteLine("Ogiltigt val.");
                return;
            }

            string color;
            Console.Write($"Färg (Enter för random) - tillgängliga färger: {string.Join(", ", colors)}: ");
            string colorInput = Console.ReadLine();

            if (colorInput == string.Empty)
            {
                color = colors[random.Next(colors.Length)];
            }

            else
            {
                string userColor = (colorInput ?? "").ToLower();
                string matchedColor = Array.Find(colors, colorOption => colorOption.ToLower() == userColor);

                if (matchedColor != null)
                {
                    color = matchedColor;
                }

                else
                {
                    Console.WriteLine("Ogiltig färg, väljer random.");
                    color = colors[random.Next(colors.Length)];
                }
            }
            Vehicle vehicle = null;

           switch (vehicleType) 
           {
                case 1:
                    bool isElectric;
                    Console.Write("Elbil? (Y/N) Enter för random: ");
                    string electricInput = Console.ReadLine();

                    string elInput = (electricInput ?? "").ToLower();

                    if (electricInput == string.Empty)
                    {
                        isElectric = random.Next(0, 2) == 0;
                    }

                    else if (elInput == "y" || elInput == "yes")
                    {
                        isElectric = true;
                    }

                    else if (elInput == "n" || elInput == "no")
                    {
                        isElectric = false;
                    }

                    else
                    {
                        Console.WriteLine("Ogiltigt val, väljer random.");
                        isElectric = random.Next(0, 2) == 0;
                    }

                    vehicle = new Car(color, isElectric);
                    break;

                case 2:
                    string brand;
                    Console.Write($"Märke (Enter för random) - tillgängliga Märken: {string.Join(", ", mcBrands)}:\n");
                    string brandInput = Console.ReadLine();

                    if (brandInput == string.Empty)
                    {
                        brand = mcBrands[random.Next(mcBrands.Length)];
                    }

                    else
                    {
                        string userBrand = (brandInput ?? "").ToLower();
                        string matchedBrand = Array.Find(mcBrands, brandOption => brandOption.ToLower() == userBrand);

                        if (matchedBrand != null)
                        {
                            brand = matchedBrand;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt märke, väljer random.");
                            brand = mcBrands[random.Next(mcBrands.Length)];
                        }
                    }

                    vehicle = new Motorcycle(color, brand);
                    break;

                case 3:
                    int passengers;
                    Console.Write("Antal passagerare (Enter för random): ");
                    string passengerInput = Console.ReadLine();

                    if (passengerInput == string.Empty)
                    {
                        passengers = random.Next(1, 50);
                    }

                    else if (!int.TryParse(passengerInput, out passengers) || passengers < 0 || passengers > 50)
                    {
                        Console.WriteLine("Felaktig antal ange antal passagerare mellan 0 och 50");
                        return;
                    }

                    vehicle = new Bus(color, passengers);
                    break;
           }

            if (parkingLot.AddVehicleToSpot(vehicle))
            {
                Console.WriteLine($"Fordon parkerat: {GenericHelpers.DescribeVehicle(vehicle)}");
            }

            else
            {
                Console.WriteLine("\nIngen plats");
            }
        }

        static void CheckOutVehicle(ParkingLot parkingLot) 
        {
            Console.Write("Ange registreringsnummer: ");
            string regNumber = Console.ReadLine();

            if (!parkingLot.RemoveVehicle(regNumber)) 
            {
                Console.WriteLine("Fordonet kunde inte checkas ut");
            }
        }

        static void SearchVehicle(ParkingLot parkingLot) 
        {
            Console.WriteLine("\n -- Sök Fordon --");
            Console.WriteLine("Ange registreringsnummer att söka efter: ");

            string regNumberToSearch = Console.ReadLine();

            Vehicle foundVehicle = parkingLot.FindVehicle(regNumberToSearch);

            if (foundVehicle != null)
            {
                string spotInfo = parkingLot.GetSpotInfo(foundVehicle);

                string vehicleDetails = GenericHelpers.DescribeVehicle(foundVehicle);

                TimeSpan timeParked = parkingLot.CalculateTimeParked(foundVehicle);
                string timeInfo = $"(Parkerad i {timeParked.TotalMinutes:F2} minuter)";

                Console.WriteLine("\n-- Sökresultat --");
                Console.WriteLine($"{spotInfo}: {vehicleDetails}, {timeInfo}");
            }
            else
            {
                Console.WriteLine($"Fordon med registreringsnummer: {regNumberToSearch} hittades inte");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    internal class LicensePlateGenerator
    {
        private static Random random = new Random();

        internal static string GenerateRegistrationNumber() 
        {
            char[] letters = new char[3];
            for (int i = 0; i < 3; i++) 
            {
                letters[i] = (char)random.Next('A', 'Z' + 1);
            }

            char[] numbers = new char[3];
            for (int i = 0; i < 3; i++)
            {
                numbers[i] = (char)('0' + random.Next(0, 10));
            }

            return $"{new string(letters)}-{new string(numbers)}";
        }
    }
}

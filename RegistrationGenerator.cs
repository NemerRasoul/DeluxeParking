using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    internal class RegistrationGenerator
    {
        private static Random random = new Random();

        internal static string GenerateRegistrationNumber() 
        {
            char letter1 = (char)random.Next('A', 'Ö' + 1);
            char letter2 = (char)random.Next('A', 'Ö' + 1);
            char letter3 = (char)random.Next('A', 'Ö' + 1);

            int number1 = random.Next(0, 10);
            int number2 = random.Next(0, 10);
            int number3 = random.Next(0, 10);

            string RegistrationNumber = $"{letter1}{letter2}{letter3}-{number1}{number2}{number3}";

            return RegistrationNumber;
        }
    }
}

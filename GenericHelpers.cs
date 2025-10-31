using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking1
{
    internal class GenericHelpers
    {
        internal static string DescribeVehicle<T>(T obj) where T : Vehicle 
        {
            if (obj == null) return "Fordon saknas";
            return obj.GetVehicleInfo();
        }
    }
}

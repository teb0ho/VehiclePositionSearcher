using System.Diagnostics;
using System.Text;
using VehiclePositionSearcher.Helpers;
using VehiclePositionSearcher.Models;

namespace VehiclePositions
{
    public class Program
    {
        public static void Main()
        {
             var vehiclePositions = DataFileConverter.ImportVehiclePositions();
             Stopwatch sw = Stopwatch.StartNew();

             var closestVehicles = GPSLocationHelper.GetNearestLocation(new LookupLocations().LocationsToLookup, vehiclePositions);
             Console.WriteLine(sw.Elapsed);
             Console.WriteLine("....");


             foreach (var answer in closestVehicles)
             {
                 if (answer != null)
                 {
                     Console.WriteLine(answer.VehicleRegistration);
                 }
             }


             sw.Stop();

            

            


            Console.WriteLine("Done...");
        }

        
    }
}

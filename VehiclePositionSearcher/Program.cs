using VehiclePositionSearcher.Helpers;
using VehiclePositionSearcher.Models;

namespace VehiclePositions
{
    public class Program
    {
        public static void Main()
        {
            var vehiclePositions = DataFileConverter.ImportVehiclePositions(@"Data\VehiclePositions.dat");

            var closestVehicles = GPSLocationHelper.GetNearestLocation(new LookupLocations().LocationsToLookup, vehiclePositions);

            Console.WriteLine("License registrations are: ");

            foreach (var answer in closestVehicles)
            {
                Console.WriteLine(answer.VehicleRegistration);
            }
        }


    }
}

using VehiclePositionSearcher.Helpers;
using VehiclePositionSearcher.Models;

namespace VehiclePositionSearcher
{
    public class Program
    {
        public static void Main()
        {
            /* call ImportVehiclePosition with your data file location like so:  DataFileConverter.ImportVehiclePositions(@"C:\Users\..........");
             * It uses my file location by default if no argument is passed.
             * I will not embed the file with the project as the file is too large
             */
            var vehiclePositions = DataFileConverter.ImportVehiclePositions();

            var closestVehicles = GPSLocationHelper.GetNearestLocation(new LookupLocations().LocationsToLookup, vehiclePositions);

            Console.WriteLine("License registrations are: ");

            foreach (var answer in closestVehicles)
            {
                Console.WriteLine(answer.VehicleRegistration);
            }
        }


    }
}

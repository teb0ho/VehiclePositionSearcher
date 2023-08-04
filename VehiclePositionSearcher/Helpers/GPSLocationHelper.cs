using System.Drawing;
using System.Xml.Linq;
using VehiclePositionSearcher.Models;

namespace VehiclePositionSearcher.Helpers
{
    public class GPSLocationHelper
    {

        private const double MaxValue = double.MaxValue;

        public static List<VehiclePosition> GetNearestLocation(List<Coordinate> lookupLocations, List<VehiclePosition> vehicleDataset)
        {
            VehiclePosition closestVehicle = null;
            List<VehiclePosition> closestVehicles = new List<VehiclePosition>();

            closestVehicles.OrderBy(v => v.Latitude).ThenBy(v => v.Longitude).ToList();

            double minDistance = MaxValue;


            int median = vehicleDataset.Count / 2;


            foreach (var location in lookupLocations)
            {
                if (location.Latitude < vehicleDataset[median].Latitude && location.Longitude < vehicleDataset[median].Longitude)
                {
                    for (int i = median; i > 0; i--)
                    {
                        double distance = CalculateHaversineDistance(location.Latitude, location.Longitude, vehicleDataset[i].Latitude, vehicleDataset[i].Longitude);

                        GetClosetVehicle(vehicleDataset, ref closestVehicle, ref minDistance, i, distance);

                        // break out of loop if the next element is does not have a smaller distance
                        if (minDistance != MaxValue && distance < minDistance)
                            break;
                    }
                    minDistance = AddClosestVehicleAndResetMinDistance(closestVehicle, closestVehicles);
                }
                else
                {
                    for (int i = median + 1; i < vehicleDataset.Count; i++)
                    {
                        double distance = CalculateHaversineDistance(location.Latitude, location.Longitude, vehicleDataset[i].Latitude, vehicleDataset[i].Longitude);

                        // break out of loop if the next element is does not have a smaller distance
                        GetClosetVehicle(vehicleDataset, ref closestVehicle, ref minDistance, i, distance);

                        if (minDistance != MaxValue && distance < minDistance)
                            break;
                    }
                    minDistance = AddClosestVehicleAndResetMinDistance(closestVehicle, closestVehicles);
                }
            }

            return closestVehicles;
        }

        private static double AddClosestVehicleAndResetMinDistance(VehiclePosition closestVehicle, List<VehiclePosition> closestVehicles)
        {
            closestVehicles.Add(closestVehicle);
            return MaxValue;
        }

        private static void GetClosetVehicle(List<VehiclePosition> vehicleDataset, ref VehiclePosition closestVehicle, ref double minDistance, int i, double distance)
        {
            if (distance < minDistance)
            {
                minDistance = distance;
                closestVehicle = vehicleDataset[i];
            }
        }

        static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        static double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double earthRadius = 6371.0; // Radius of Earth in kilometers

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = earthRadius * c;
            return distance;
        }
    }
}

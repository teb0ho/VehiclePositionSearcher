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

            var vehiclesOrdered = vehicleDataset.OrderBy(v => v.Latitude).ThenBy(v => v.Longitude).ToList();

            double minDistance = MaxValue;
            
            int median = vehicleDataset.Count / 2;


            foreach (var location in lookupLocations)
            {
                if (location.Latitude < vehiclesOrdered[median].Latitude && location.Longitude < vehiclesOrdered[median].Longitude)
                {
                    for (int i = median; i > 0; i--)
                    {
                        double distance = CalculateHaversineDistance(location.Latitude, location.Longitude, vehiclesOrdered[i].Latitude, vehiclesOrdered[i].Longitude);

                        GetClosetVehicle(vehiclesOrdered, ref closestVehicle, ref minDistance, i, distance);
                    }
                    minDistance = AddClosestVehicleAndResetMinDistance(closestVehicle, closestVehicles);
                }
                else
                {
                    for (int i = median + 1; i < vehiclesOrdered.Count; i++)
                    {
                        double distance = CalculateHaversineDistance(location.Latitude, location.Longitude, vehiclesOrdered[i].Latitude, vehiclesOrdered[i].Longitude);

                        GetClosetVehicle(vehiclesOrdered, ref closestVehicle, ref minDistance, i, distance);
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
            double earthRadius = 6372.8;

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);
            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Asin(Math.Sqrt(a));
            
            return earthRadius * c;
        }
    }
}

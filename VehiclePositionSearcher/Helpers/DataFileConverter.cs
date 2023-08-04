using System.Diagnostics;
using System.Text;
using VehiclePositionSearcher.Models;

namespace VehiclePositionSearcher.Helpers
{
    public static class DataFileConverter
    {
        public static List<VehiclePosition> ImportVehiclePositions(string fileLocation =
            @"C:\Users\teboh\Downloads\MiX\VehiclePositions_DataFile\VehiclePositions.dat")
        {
            List<VehiclePosition> vehiclePositions = new List<VehiclePosition>();
            using (var stream = File.Open(fileLocation, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    try
                    {
                        while (true)
                        {
                            var newVehicle = new VehiclePosition
                            {
                                VehicleId = reader.ReadInt32(),
                                VehicleRegistration = ReadAscii(reader),
                                Longitude = reader.ReadSingle(),
                                Latitude = reader.ReadSingle(),
                                RecordedTimeUTC = reader.ReadInt64()
                            };

                            vehiclePositions.Add(newVehicle);
                        }
                    }
                    catch (EndOfStreamException)
                    {
                        Console.WriteLine("Import finished");
                    }
                }
            }

            return vehiclePositions;
        }

        public static string ReadAscii(BinaryReader input)
        {
            List<byte> strBytes = new List<byte>();
            int b;
            while ((b = input.ReadByte()) != 0x00)
                strBytes.Add((byte)b);
            return Encoding.ASCII.GetString(strBytes.ToArray());
        }
    }
}

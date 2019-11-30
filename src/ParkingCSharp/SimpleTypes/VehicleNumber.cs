using System;

namespace ParkingCSharp.SimpleTypes
{
    public class VehicleNumber
    {
        public string Value { get; private set; }

        public VehicleNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 8)
            {
                throw new Exception("Vehicle number is invalid.");
            }

            Value = value;
        }
    }
}

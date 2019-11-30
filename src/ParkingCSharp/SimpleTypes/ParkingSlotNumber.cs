using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCSharp.SimpleTypes
{
    public class ParkingSlotNumber
    {
        public int Value { get; private set; }

        public ParkingSlotNumber(int value)
        {
            if (value < 1 || value > 9999)
            {
                throw new Exception("Parking slot number is invalid.");
            }

            Value = value;
        }
    }
}

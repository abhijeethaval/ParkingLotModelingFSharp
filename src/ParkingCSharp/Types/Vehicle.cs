using ParkingCSharp.SimpleTypes;

namespace ParkingCSharp.Types
{
    public class Vehicle
    {
        public Vehicle(VehicleNumber number) => Number = number;
        public VehicleNumber Number { get; }
    }

    public class Truck : Vehicle
    {
        public Truck(VehicleNumber number) : base(number) { }
    }

    public class Car : Vehicle
    {
        public Car(VehicleNumber number) : base(number) { }
    }
    public class Motorcycle : Vehicle
    {
        public Motorcycle(VehicleNumber number) : base(number) { }
    }

}

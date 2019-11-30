using ParkingCSharp.SimpleTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCSharp.Types
{

    public class LargeParkingSlotState 
    {
        public static LargeParkingSlotState Create(Truck truck) => new OccupiedByTruck(truck);

        public static LargeParkingSlotState Create() => new Empty();
    }
    public class Empty  : LargeParkingSlotState{ }
    public class OccupiedByTruck : LargeParkingSlotState
    {
        public OccupiedByTruck(Truck value) => Value = value;
        public Truck Value { get; }
    }
    public class OccupiedByCar : LargeParkingSlotState
    {
        public OccupiedByCar(Car value) => Value = value;
        public Car Value { get; }
    }
    public class OccupiedByTwoCars : LargeParkingSlotState
    {
        public OccupiedByTwoCars((Car, Car) value) => Value = value;
        public (Car, Car) Value { get; }
    }
    public class OccupiedByMotorcycle : LargeParkingSlotState
    {
        public OccupiedByMotorcycle(Motorcycle value) => Value = value;
        public Motorcycle Value { get; }
    }
    public class OccupiedByTwoMotorcycles : LargeParkingSlotState
    {
        public OccupiedByTwoMotorcycles((Motorcycle, Motorcycle) value) => Value = value;
        public (Motorcycle, Motorcycle) Value { get; }
    }
    public class OccupiedByThreeMotorcycles : LargeParkingSlotState
    {
        public OccupiedByThreeMotorcycles((Motorcycle, Motorcycle, Motorcycle) value) => Value = value;
        public (Motorcycle, Motorcycle, Motorcycle) Value { get; }
    }
    public class OccupiedByFourMotorcycles : LargeParkingSlotState
    {
        public OccupiedByFourMotorcycles((Motorcycle, Motorcycle, Motorcycle, Motorcycle) value) => Value = value;
        public (Motorcycle, Motorcycle, Motorcycle, Motorcycle) Value { get; }
    }
    public class OccupiedByMotorcycleAndCar : LargeParkingSlotState
    {
        public OccupiedByMotorcycleAndCar((Motorcycle, Car) value) => Value = value;
        public (Motorcycle, Car) Value { get; }
    }
    public class OccupiedByTwoMotorcyclesAndCar : LargeParkingSlotState
    {
        public OccupiedByTwoMotorcyclesAndCar((Motorcycle, Motorcycle, Car) value) => Value = value;
        public (Motorcycle, Motorcycle, Car) Value { get; }
    }
}

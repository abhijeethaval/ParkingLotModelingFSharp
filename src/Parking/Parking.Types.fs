namespace Parking

open System
open Payment
      
type Truck = Truck of VehicleNumber

type Car = Car of VehicleNumber

type Motorcycle = Motorcycle of VehicleNumber
    
type Vehicle = 
    | Truck of Truck
    | Car of Car
    | Motorcycle of Motorcycle 

type LargeParkingSlotState = 
    | Empty
    | OccupiedByTruck of Truck
    | OccupiedByCar of Car
    | OccupiedByTwoCars of Car * Car
    | OccupiedByMotorcycle of Motorcycle
    | OccupiedByTwoMotorcycles of Motorcycle * Motorcycle
    | OccupiedByThreeMotorcycles of Motorcycle * Motorcycle * Motorcycle
    | OccupiedByFourMotorcycles of Motorcycle * Motorcycle * Motorcycle * Motorcycle
    | OccupiedByMotorcycleAndCar of Motorcycle * Car
    | OccupiedByTwoMotorcyclesAndCar of Motorcycle * Motorcycle * Car

type CompactParkingSlotState = 
    | Empty
    | OccupiedByCar of Car
    | OccupiedByMotorcycle of Motorcycle
    | OccupiedByTwoMotorcycles of Motorcycle * Motorcycle

type MotorcycleParkingSlotState = 
    | Empty
    | OccupiedByMotorcycle of Motorcycle

type ParkingSlotState = 
    | Large of LargeParkingSlotState
    | Compact of CompactParkingSlotState
    | Motorcycle of MotorcycleParkingSlotState

type ParkingSlot = {
    ParkingSlotNumber: ParkingSlotNumber
    State: ParkingSlotState
}

type ParkingFloor = {
    Slots : ParkingSlot list
    FloorNumber: FloorNumber
}

type ParkingLot = {
    Floors : ParkingFloor list
}

type UnpaidParkingTicket = {
    Vehicle : Vehicle
    ParkingSlot : ParkingSlot
    InTime: DateTime
}

type PaidParkingTicket = {
    ParkingTicket : UnpaidParkingTicket
    OutTime: DateTime
    Payment : Payment
}


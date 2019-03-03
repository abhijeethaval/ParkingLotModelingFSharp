module Parking.Implementation

open Parking
open Parking.TruckParking
open Parking.CarParking
open Parking.MotorcycleParking

let ParkVehicleToSlotState (slotState: ParkingSlotState) (vehicle: Vehicle) =
    match vehicle with
    | Vehicle.Truck truck -> ParkTruck slotState truck
    | Vehicle.Car car -> ParkCar slotState car
    | Vehicle.Motorcycle motorcycle -> ParkMotorcycle slotState motorcycle

let ParkVehicle (slot: ParkingSlot) (vehicle: Vehicle) =
    let newSlotStateResult = ParkVehicleToSlotState slot.State vehicle
    match newSlotStateResult with
    | Ok state -> {ParkingSlotNumber = slot.ParkingSlotNumber; State = state} |> Ok
    | Error e -> Error e

let CanParkVehicle(slot: ParkingSlot) (vehicle: Vehicle) =
    match vehicle with
    | Vehicle.Truck _ -> CanParkTruck slot.State
    | Vehicle.Car _ -> CanParkCar slot.State
    | Vehicle.Motorcycle _ -> CanParkMotorcycle slot.State
    


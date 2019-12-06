module Parking.Implementation

open Parking
open Parking.TruckParking
open Parking.CarParking
open Parking.MotorcycleParking

let ParkVehicle (slot) (vehicle) =
    match vehicle with
    | Vehicle.Truck truck -> ParkTruck slot truck
    | Vehicle.Car car -> ParkCar slot car
    | Vehicle.Motorcycle motorcycle -> ParkMotorcycle slot motorcycle

let CanParkVehicle(slot) (vehicle) =
    match vehicle with
    | Vehicle.Truck _ -> CanParkTruck slot
    | Vehicle.Car _ -> CanParkCar slot
    | Vehicle.Motorcycle _ -> CanParkMotorcycle slot
    


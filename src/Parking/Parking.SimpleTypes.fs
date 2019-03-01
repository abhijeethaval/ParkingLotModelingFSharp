namespace Parking

open System

type ParkingSlotNumber = private ParkingSlotNumber of int

module ParkingSlotNumber = 
    let create i = 
        if i < 1 || i > 9999 then Result.Error("Parking slot number is invalid.") 
        else Result.Ok(i |> ParkingSlotNumber)       

type VehicleNumber = private VehicleNumber of string

module VehicleNumber =
    let create s = 
        if s|> String.IsNullOrWhiteSpace || s.Length < 8 then Result.Error("Vehicle number is invalid.") 
        else Result.Ok(s|> VehicleNumber)
 
type FloorNumber = FloorNumber of int

 
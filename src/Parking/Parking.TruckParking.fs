module Parking.TruckParking

open Parking

let CanParkTruckInLargeSlot (slotState) =
    match slotState with
    | LargeParkingSlotState.Empty -> true
    | LargeParkingSlotState.OccupiedByCar _ -> false
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> false
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> false
    | LargeParkingSlotState.OccupiedByTruck _ -> false
    | LargeParkingSlotState.OccupiedByTwoCars _ -> false
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> false
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> false
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> false
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> false

let ParkTruckInLargeSlot (slotState) (truck) =
    match slotState with
    | LargeParkingSlotState.Empty -> truck|> LargeParkingSlotState.OccupiedByTruck |> Ok
    | LargeParkingSlotState.OccupiedByCar _ -> "Occupied by car" |> Error
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> "Occupied by motorcycle" |> Error
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> "Occupied by two motorcycle" |> Error
    | LargeParkingSlotState.OccupiedByTruck _ -> "Occupied by truck" |> Error
    | LargeParkingSlotState.OccupiedByTwoCars _ -> "Occupied by two cars" |> Error
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> "Occupied by motorcycles and a car" |> Error
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> "Occupied by two motorcycles and a car" |> Error
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> "Occupied by three motorcycles" |> Error
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> "Occupied by four motorcycles" |> Error

let CanParkTruck(parkingSlot) =
    match parkingSlot with
    | Large (state, _) -> CanParkTruckInLargeSlot state
    | Compact _ -> false
    | Motorcycle _ -> false

let ParkTruck(parkingSlot) (truck) =
    match parkingSlot with
    | Large (state, number) -> 
        let result = ParkTruckInLargeSlot state truck
        match result with
        | Ok largeSlotState -> ParkingSlot.Large (largeSlotState, number) |> Ok
        | Error e -> Error e
    | Compact _ -> Error "Cannot park truck in Compact slot"
    | Motorcycle _ -> Error "Cannot park truck in motocycle slot"


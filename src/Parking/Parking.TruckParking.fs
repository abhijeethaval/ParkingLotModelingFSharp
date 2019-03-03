module Parking.TruckParking

open Parking

let CanParkTruckInLargeSlot (slotState: LargeParkingSlotState) =
    match slotState with
    | LargeParkingSlotState.Empty -> true
    | LargeParkingSlotState.OccupiedByCar _ -> false
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> false
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> false
    | LargeParkingSlotState.Occupied _ -> false
    | LargeParkingSlotState.OccupiedByTwoCars _ -> false
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> false
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> false
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> false
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> false

let ParkTruckInLargeSlot (slotState: LargeParkingSlotState) (truck : Truck) =
    match slotState with
    | LargeParkingSlotState.Empty -> truck|> LargeParkingSlotState.Occupied |> Ok
    | LargeParkingSlotState.OccupiedByCar _ -> "Occupied by car" |> Error
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> "Occupied by motorcycle" |> Error
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> "Occupied by two motorcycle" |> Error
    | LargeParkingSlotState.Occupied _ -> "Occupied by truck" |> Error
    | LargeParkingSlotState.OccupiedByTwoCars _ -> "Occupied by two cars" |> Error
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> "Occupied by motorcycles and a car" |> Error
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> "Occupied by two motorcycles and a car" |> Error
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> "Occupied by three motorcycles" |> Error
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> "Occupied by four motorcycles" |> Error

let CanParkTruck(slotState: ParkingSlotState) =
    match slotState with
    | ParkingSlotState.Large state -> CanParkTruckInLargeSlot state
    | ParkingSlotState.Compact _ -> false
    | ParkingSlotState.Motorcycle _ -> false

let ParkTruck(slotState: ParkingSlotState) (truck: Truck) =
    match slotState with
    | Large largeSlotState -> 
        let result = truck |> ParkTruckInLargeSlot largeSlotState
        match result with
        | Ok largeSlotState -> largeSlotState |> Large |> Ok
        | Error e -> Error e
    | Compact _ -> Error "Cannot park truck in Compact slot"
    | Motorcycle _ -> Error "Cannot park truck in motocycle slot"


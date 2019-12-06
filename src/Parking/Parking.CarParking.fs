module Parking.CarParking

open Parking

let CanParkCarInLargeSlot (slotState) =
    match slotState with
    | LargeParkingSlotState.Empty -> true
    | LargeParkingSlotState.OccupiedByCar _ -> true
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> true
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> true
    | LargeParkingSlotState.OccupiedByTruck _ -> false
    | LargeParkingSlotState.OccupiedByTwoCars _ -> false
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> false
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> false
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> false
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> false

let ParkCarInLargeSlot (slotState) (car) =
    match slotState with
    | LargeParkingSlotState.Empty -> LargeParkingSlotState.OccupiedByCar(car) |> Ok
    | LargeParkingSlotState.OccupiedByCar parkedCar -> LargeParkingSlotState.OccupiedByTwoCars(car, parkedCar) |> Ok
    | LargeParkingSlotState.OccupiedByMotorcycle motorcycle -> LargeParkingSlotState.OccupiedByMotorcycleAndCar(motorcycle, car) |> Ok
    | LargeParkingSlotState.OccupiedByTwoMotorcycles (motorcycle1, motorcycle2) ->LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar(motorcycle1, motorcycle2, car) |> Ok
    | LargeParkingSlotState.OccupiedByTruck _ -> "Occupied by truck" |> Error
    | LargeParkingSlotState.OccupiedByTwoCars _ -> "Occupied by two cars" |> Error
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> "Occupied by motorcycles and a car" |> Error
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> "Occupied by two motorcycles and a car" |> Error
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> "Occupied by three motorcycles" |> Error
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> "Occupied by four motorcycles" |> Error

let CanParkCarInCompactSlot (slotState) =
    match slotState with 
    | CompactParkingSlotState.Empty -> true
    | CompactParkingSlotState.OccupiedByCar _ -> false
    | CompactParkingSlotState.OccupiedByMotorcycle _ -> false
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ -> false

let ParkCarInCompactSlot (slotState) (car) =
    match slotState with 
    | CompactParkingSlotState.Empty -> car |> CompactParkingSlotState.OccupiedByCar |> Ok
    | CompactParkingSlotState.OccupiedByCar _ ->  "Occupied by car" |> Error
    | CompactParkingSlotState.OccupiedByMotorcycle _ ->  "Occupied by motorcycle" |> Error
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ ->  "Occupied by two motorcycles" |> Error

let CanParkCar(parkingSlot) = 
    match parkingSlot with
    | Large (state, _ ) -> state |> CanParkCarInLargeSlot 
    | Compact (state, _ ) -> state |> CanParkCarInCompactSlot 
    | Motorcycle _ -> false

let ParkCar(parkingSlot) (car) =
    match parkingSlot with
    | Large (state, number) -> 
        let result = car |> ParkCarInLargeSlot state 
        match result with
        | Ok largeSlotState ->  Large(largeSlotState, number) |> Ok
        | Error e -> Error e
    | Compact (state, number) -> 
        let result = car |> ParkCarInCompactSlot state 
        match result with
        | Ok compactSlotState -> Compact(compactSlotState, number) |> Ok
        | Error e -> Error e
    | Motorcycle _ -> Error "Cannot park car in motocycle slot"
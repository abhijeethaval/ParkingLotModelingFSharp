module Parking.CarParking

open Parking

let CanParkCarInLargeSlot (slotState: LargeParkingSlotState) =
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

let ParkCarInLargeSlot (slotState: LargeParkingSlotState) (car : Car) =
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

let CanParkCarInCompactSlot (slotState: CompactParkingSlotState) =
    match slotState with 
    | CompactParkingSlotState.Empty -> true
    | CompactParkingSlotState.OccupiedByCar _ -> false
    | CompactParkingSlotState.OccupiedByMotorcycle _ -> false
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ -> false

let ParkCarInCompactSlot (slotState: CompactParkingSlotState) (car: Car) =
    match slotState with 
    | CompactParkingSlotState.Empty -> car |> CompactParkingSlotState.OccupiedByCar |> Ok
    | CompactParkingSlotState.OccupiedByCar _ ->  "Occupied by car" |> Error
    | CompactParkingSlotState.OccupiedByMotorcycle _ ->  "Occupied by motorcycle" |> Error
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ ->  "Occupied by two motorcycles" |> Error

let CanParkCar(slotState: ParkingSlotState) = 
    match slotState with
    | Large largeSlotState -> largeSlotState |> CanParkCarInLargeSlot 
    | Compact compactSlotState -> compactSlotState |> CanParkCarInCompactSlot 
    | Motorcycle _ -> false

let ParkCar(slotState: ParkingSlotState) (car: Car) =
    match slotState with
    | Large largeSlot -> 
        let result = car |> ParkCarInLargeSlot largeSlot 
        match result with
        | Ok largeSlotState -> largeSlotState |> Large |> Ok
        | Error e -> Error e
    | Compact compactSlotState -> 
        let result = car |> ParkCarInCompactSlot compactSlotState 
        match result with
        | Ok compactSlotState -> compactSlotState |> Compact |> Ok
        | Error e -> Error e
    | Motorcycle _ -> Error "Cannot park car in motocycle slot"
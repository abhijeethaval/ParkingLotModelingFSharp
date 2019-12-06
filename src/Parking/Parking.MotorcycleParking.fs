module Parking.MotorcycleParking

open Parking

let CanParkMotorcycleInLargeSlot (slotState) =
    match slotState with
    | LargeParkingSlotState.Empty -> true
    | LargeParkingSlotState.OccupiedByCar _ -> true
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> true
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> true
    | LargeParkingSlotState.OccupiedByTruck _ -> false
    | LargeParkingSlotState.OccupiedByTwoCars _ -> false
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> true
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> false
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> true
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> false

let ParkMotorcycleInLargeSlot (slotState) (motorcycle) =
    match slotState with
    | LargeParkingSlotState.Empty -> LargeParkingSlotState.OccupiedByMotorcycle(motorcycle) |> Ok
    | LargeParkingSlotState.OccupiedByCar parkedCar -> LargeParkingSlotState.OccupiedByMotorcycleAndCar(motorcycle, parkedCar) |> Ok
    | LargeParkingSlotState.OccupiedByMotorcycle parkedMotorcycle -> LargeParkingSlotState.OccupiedByTwoMotorcycles(parkedMotorcycle, motorcycle) |> Ok
    | LargeParkingSlotState.OccupiedByTwoMotorcycles (motorcycle1, motorcycle2) -> LargeParkingSlotState.OccupiedByThreeMotorcycles(motorcycle1, motorcycle2, motorcycle) |> Ok
    | LargeParkingSlotState.OccupiedByTruck _ -> "Occupied by truck" |> Error
    | LargeParkingSlotState.OccupiedByTwoCars _ -> "Occupied by two cars" |> Error
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar (parkedMotorcycle, car) -> LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar(parkedMotorcycle, motorcycle, car) |> Ok
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> "Occupied by two motorcycles and a car" |> Error
    | LargeParkingSlotState.OccupiedByThreeMotorcycles (motorcycle1, motorcycle2, motorcycle3) -> LargeParkingSlotState.OccupiedByFourMotorcycles(motorcycle1, motorcycle2, motorcycle3, motorcycle) |> Ok
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> "Occupied by four motorcycles" |> Error


let CanParkMotorcycleInCompactSlot (slotState) =
    match slotState with 
    | CompactParkingSlotState.Empty -> true
    | CompactParkingSlotState.OccupiedByCar _ -> false
    | CompactParkingSlotState.OccupiedByMotorcycle _ -> true
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ -> false

let ParkMotorcycleInCompactSlot (slotState) (motorcycle) =
    match slotState with 
    | CompactParkingSlotState.Empty -> CompactParkingSlotState.OccupiedByMotorcycle(motorcycle) |> Ok
    | CompactParkingSlotState.OccupiedByCar _ -> "Occupied by car" |> Error
    | CompactParkingSlotState.OccupiedByMotorcycle parkedMotorcycle -> CompactParkingSlotState.OccupiedByTwoMotorcycles(parkedMotorcycle, motorcycle) |> Ok
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ -> "Occupied by two motorcycles" |> Error

let CanParkMotorcycleInMotorcycleSlot (slotState) = 
    match slotState with
    | MotorcycleParkingSlotState.Empty -> true
    | MotorcycleParkingSlotState.OccupiedByMotorcycle _ -> false

let ParkMotorcycleInMotorcycleSlot (slotState) (motorcycle) = 
    match slotState with
    | MotorcycleParkingSlotState.Empty -> MotorcycleParkingSlotState.OccupiedByMotorcycle(motorcycle) |> Ok
    | MotorcycleParkingSlotState.OccupiedByMotorcycle _ -> "Occupied by motorcycle" |> Error

let CanParkMotorcycle(slotState) = 
    match slotState with
    | Large (state, _) -> state |> CanParkMotorcycleInLargeSlot
    | Compact (state, _) -> state |> CanParkMotorcycleInCompactSlot
    | Motorcycle (state, _) -> state |> CanParkMotorcycleInMotorcycleSlot

let ParkMotorcycle(parkingSlot) (motorcycle) =
    match parkingSlot with
    | Large (state, number) -> 
        let result = motorcycle |> ParkMotorcycleInLargeSlot state
        match result with
        | Ok largeState -> Large (largeState, number) |> Ok
        | Error e -> Error e
    | Compact (state, number) -> 
        let result = motorcycle |> ParkMotorcycleInCompactSlot state
        match result with
        | Ok compactState -> Compact (compactState, number) |> Ok
        | Error e -> Error e
    | Motorcycle (state, number)  -> 
        let result = motorcycle |> ParkMotorcycleInMotorcycleSlot state 
        match result with
        | Ok newMotorcycleSlotState -> Motorcycle (newMotorcycleSlotState, number) |> Ok
        | Error e -> Error e
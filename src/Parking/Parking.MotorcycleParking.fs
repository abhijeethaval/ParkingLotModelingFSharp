module Parking.MotorcycleParking

open Parking

let CanParkMotorcycleInLargeSlot (slotState: LargeParkingSlotState) =
    match slotState with
    | LargeParkingSlotState.Empty -> true
    | LargeParkingSlotState.OccupiedByCar _ -> true
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> true
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> true
    | LargeParkingSlotState.Occupied _ -> false
    | LargeParkingSlotState.OccupiedByTwoCars _ -> false
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> true
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> false
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> true
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> false

let ParkMotorcycleInLargeSlot (slotState: LargeParkingSlotState) (motorcycle : Motorcycle) =
    match slotState with
    | LargeParkingSlotState.Empty -> LargeParkingSlotState.OccupiedByMotorcycle(motorcycle) |> Ok
    | LargeParkingSlotState.OccupiedByCar parkedCar -> LargeParkingSlotState.OccupiedByMotorcycleAndCar(motorcycle, parkedCar) |> Ok
    | LargeParkingSlotState.OccupiedByMotorcycle parkedMotorcycle -> LargeParkingSlotState.OccupiedByTwoMotorcycles(parkedMotorcycle, motorcycle) |> Ok
    | LargeParkingSlotState.OccupiedByTwoMotorcycles (motorcycle1, motorcycle2) -> LargeParkingSlotState.OccupiedByThreeMotorcycles(motorcycle1, motorcycle2, motorcycle) |> Ok
    | LargeParkingSlotState.Occupied _ -> "Occupied by truck" |> Error
    | LargeParkingSlotState.OccupiedByTwoCars _ -> "Occupied by two cars" |> Error
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar (parkedMotorcycle, car) -> LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar(parkedMotorcycle, motorcycle, car) |> Ok
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> "Occupied by two motorcycles and a car" |> Error
    | LargeParkingSlotState.OccupiedByThreeMotorcycles (motorcycle1, motorcycle2, motorcycle3) -> LargeParkingSlotState.OccupiedByFourMotorcycles(motorcycle1, motorcycle2, motorcycle3, motorcycle) |> Ok
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> "Occupied by four motorcycles" |> Error


let CanParkMotorcycleInCompactSlot (slotState: CompactParkingSlotState) =
    match slotState with 
    | CompactParkingSlotState.Empty -> true
    | CompactParkingSlotState.Occupied _ -> false
    | CompactParkingSlotState.OccupiedByMotorcycle _ -> true
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ -> false

let ParkMotorcycleInCompactSlot (slotState: CompactParkingSlotState) (motorcycle: Motorcycle) =
    match slotState with 
    | CompactParkingSlotState.Empty -> CompactParkingSlotState.OccupiedByMotorcycle(motorcycle) |> Ok
    | CompactParkingSlotState.Occupied _ -> "Occupied by car" |> Error
    | CompactParkingSlotState.OccupiedByMotorcycle parkedMotorcycle -> CompactParkingSlotState.OccupiedByTwoMotorcycles(parkedMotorcycle, motorcycle) |> Ok
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ -> "Occupied by two motorcycles" |> Error

let CanParkMotorcycleInMotorcycleSlot (slotState: MotorcycleParkingSlotState) = 
    match slotState with
    | MotorcycleParkingSlotState.Empty -> true
    | MotorcycleParkingSlotState.Occupied _ -> false

let ParkMotorcycleInMotorcycleSlot (slotState: MotorcycleParkingSlotState) (motorcycle: Motorcycle) = 
    match slotState with
    | MotorcycleParkingSlotState.Empty -> MotorcycleParkingSlotState.Occupied(motorcycle) |> Ok
    | MotorcycleParkingSlotState.Occupied _ -> "Occupied by motorcycle" |> Error

let CanParkMotorcycle(slotState: ParkingSlotState) = 
    match slotState with
    | Large largeSlotState -> largeSlotState |> CanParkMotorcycleInLargeSlot
    | Compact compactSlotState -> compactSlotState |> CanParkMotorcycleInCompactSlot
    | Motorcycle motorcycleSlotState -> motorcycleSlotState |> CanParkMotorcycleInMotorcycleSlot

let ParkMotorcycle(slotState: ParkingSlotState) (motorcycle: Motorcycle) =
    match slotState with
    | Large largeSlotState -> 
        let result = motorcycle |> ParkMotorcycleInLargeSlot largeSlotState
        match result with
        | Ok large -> large |> Large |> Ok
        | Error e -> Error e
    | Compact compactSlotState -> 
        let result = motorcycle |> ParkMotorcycleInCompactSlot compactSlotState 
        match result with
        | Ok compact -> compact |> Compact |> Ok
        | Error e -> Error e
    | Motorcycle motorcycleSlotState -> 
        let result = motorcycle |> ParkMotorcycleInMotorcycleSlot motorcycleSlotState 
        match result with
        | Ok newMotorcycleSlotState -> newMotorcycleSlotState |> Motorcycle |> Ok
        | Error e -> Error e
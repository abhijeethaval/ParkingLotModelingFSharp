module Parking.MotorcycleParking

open Parking


let CanParkMotorcycleInLargeSlot (slot: LargeParkingSlot) =
    match slot.State with
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

let ParkMotorcycleInLargeSlot (slot: LargeParkingSlot, motorcycle : Motorcycle) =
    match slot.State with
    | LargeParkingSlotState.Empty -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = LargeParkingSlotState.OccupiedByMotorcycle(motorcycle)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByCar parkedCar -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = OccupiedByMotorcycleAndCar(motorcycle, parkedCar)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByMotorcycle parkedMotorcycle -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = LargeParkingSlotState.OccupiedByTwoMotorcycles(parkedMotorcycle, motorcycle)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByTwoMotorcycles (motorcycle1, motorcycle2) -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = OccupiedByThreeMotorcycles(motorcycle1, motorcycle2, motorcycle)}: LargeParkingSlot)
    | LargeParkingSlotState.Occupied _ -> Error "Occupied by truck"
    | LargeParkingSlotState.OccupiedByTwoCars _ -> Error "Occupied by two cars"
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar (parkedMotorcycle, car) -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = OccupiedByTwoMotorcyclesAndCar(parkedMotorcycle, motorcycle, car)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> Error "Occupied by two motorcycles and a car"
    | LargeParkingSlotState.OccupiedByThreeMotorcycles (motorcycle1, motorcycle2, motorcycle3) -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = OccupiedByFourMotorcycles(motorcycle1, motorcycle2, motorcycle3, motorcycle)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> Error "Occupied by four motorcycles"


let CanParkMotorcycleInCompactSlot (slot: CompactParkingSlot) =
    match slot.State with 
    | CompactParkingSlotState.Empty -> true
    | CompactParkingSlotState.Occupied _ -> false
    | CompactParkingSlotState.OccupiedByMotorcycle _ -> true
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ -> false

let ParkMotorcycleInCompactSlot (slot: CompactParkingSlot, motorcycle: Motorcycle) =
    match slot.State with 
    | CompactParkingSlotState.Empty -> Ok({ParkingSlotNumber = slot.ParkingSlotNumber; State = CompactParkingSlotState.OccupiedByMotorcycle(motorcycle)}: CompactParkingSlot)
    | CompactParkingSlotState.Occupied _ ->  Error "Occupied by car"
    | CompactParkingSlotState.OccupiedByMotorcycle parkedMotorcycle -> Ok({ParkingSlotNumber = slot.ParkingSlotNumber; State = CompactParkingSlotState.OccupiedByTwoMotorcycles(parkedMotorcycle, motorcycle)}: CompactParkingSlot)
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ ->  Error "Occupied by two motorcycles"

let CanParkMotorcycleInMotorcycleSlot (slot: MotorcycleParkingSlot) = 
    match slot.State with
    | MotorcycleParkingSlotState.Empty -> true
    | MotorcycleParkingSlotState.Occupied _ -> false

let ParkMotorcycleInMotorcycleSlot (slot: MotorcycleParkingSlot, motorcycle: Motorcycle) = 
    match slot.State with
    | MotorcycleParkingSlotState.Empty -> Ok({ParkingSlotNumber = slot.ParkingSlotNumber; State = MotorcycleParkingSlotState.Occupied(motorcycle)}: MotorcycleParkingSlot)
    | MotorcycleParkingSlotState.Occupied _ -> Error "Occupied by motorcycle"

let CanParkMotorcycle(slot: ParkingSlot) = 
    match slot with
    | Large largeSlot -> CanParkMotorcycleInLargeSlot largeSlot
    | Compact compactSlot -> CanParkMotorcycleInCompactSlot compactSlot
    | Motorcycle motorcycleSlot -> CanParkMotorcycleInMotorcycleSlot motorcycleSlot

let ParkMotorrcycle(slot: ParkingSlot, motorcycle: Motorcycle) =
    match slot with
    | Large largeSlot -> 
        let result = ParkMotorcycleInLargeSlot(largeSlot, motorcycle) 
        match result with
        | Ok large -> Ok(ParkingSlot.Large(large))
        | Error e -> Error e
    | Compact compactSlot -> 
        let result = ParkMotorcycleInCompactSlot(compactSlot, motorcycle) 
        match result with
        | Ok compact -> Ok(ParkingSlot.Compact(compact))
        | Error e -> Error e
    | Motorcycle motorcycleSlot -> 
        let result = ParkMotorcycleInMotorcycleSlot(motorcycleSlot, motorcycle) 
        match result with
        | Ok motorcycleSlot -> Ok(ParkingSlot.Motorcycle(motorcycleSlot))
        | Error e -> Error e
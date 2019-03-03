module Parking.CarParking

open Parking

let CanParkCarInLargeSlot (slot: LargeParkingSlot) =
    match slot.State with
    | LargeParkingSlotState.Empty -> true
    | LargeParkingSlotState.OccupiedByCar _ -> true
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> true
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> true
    | LargeParkingSlotState.Occupied _ -> false
    | LargeParkingSlotState.OccupiedByTwoCars _ -> false
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> false
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> false
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> false
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> false

let ParkCarInLargeSlot (slot: LargeParkingSlot, car : Car) =
    match slot.State with
    | LargeParkingSlotState.Empty -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = LargeParkingSlotState.OccupiedByCar(car)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByCar parkedCar -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = OccupiedByTwoCars(car, parkedCar)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByMotorcycle motorcycle -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = OccupiedByMotorcycleAndCar(motorcycle, car)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByTwoMotorcycles (motorcycle1, motorcycle2) -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = OccupiedByTwoMotorcyclesAndCar(motorcycle1, motorcycle2, car)}: LargeParkingSlot)
    | LargeParkingSlotState.Occupied _ -> Error "Occupied by truck"
    | LargeParkingSlotState.OccupiedByTwoCars _ -> Error "Occupied by two cars"
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> Error "Occupied by motorcycles and a car"
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> Error "Occupied by two motorcycles and a car"
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> Error "Occupied by three motorcycles"
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> Error "Occupied by four motorcycles"

let CanParkCarInCompactSlot (slot: CompactParkingSlot) =
    match slot.State with 
    | CompactParkingSlotState.Empty -> true
    | CompactParkingSlotState.Occupied _ -> false
    | CompactParkingSlotState.OccupiedByMotorcycle _ -> false
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ -> false

let ParkCarInCompactSlot (slot: CompactParkingSlot, car: Car) =
    match slot.State with 
    | CompactParkingSlotState.Empty -> Ok({ParkingSlotNumber = slot.ParkingSlotNumber; State = CompactParkingSlotState.Occupied(car)}: CompactParkingSlot)
    | CompactParkingSlotState.Occupied _ ->  Error "Occupied by car"
    | CompactParkingSlotState.OccupiedByMotorcycle _ ->  Error "Occupied by motorcycle"
    | CompactParkingSlotState.OccupiedByTwoMotorcycles _ ->  Error "Occupied by two motorcycles"

let CanParkCar(slot: ParkingSlot) = 
    match slot with
    | Large largeSlot -> CanParkCarInLargeSlot largeSlot
    | Compact compactSlot -> CanParkCarInCompactSlot compactSlot
    | Motorcycle _ -> false

let ParkCar(slot: ParkingSlot, car: Car) =
    match slot with
    | Large largeSlot -> 
        let result = ParkCarInLargeSlot(largeSlot, car) 
        match result with
        | Ok large -> Ok(ParkingSlot.Large(large))
        | Error e -> Error e
    | Compact compactSlot -> 
        let result = ParkCarInCompactSlot(compactSlot, car) 
        match result with
        | Ok compact -> Ok(ParkingSlot.Compact(compact))
        | Error e -> Error e
    | Motorcycle _ -> Error "Cannot park car in motocycle slot"
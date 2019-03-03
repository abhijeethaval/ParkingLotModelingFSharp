module Parking.TruckParking

open Parking

let CanParkTruckInLargeSlot (slot: LargeParkingSlot) =
    match slot.State with
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

let ParkTruckInLargeSlot (slot: LargeParkingSlot, truck : Truck) =
    match slot.State with
    | LargeParkingSlotState.Empty -> Ok ({ParkingSlotNumber = slot.ParkingSlotNumber; State = LargeParkingSlotState.Occupied(truck)}: LargeParkingSlot)
    | LargeParkingSlotState.OccupiedByCar _ -> Error "Occupied by car"
    | LargeParkingSlotState.OccupiedByMotorcycle _ -> Error "Occupied by motorcycle"
    | LargeParkingSlotState.OccupiedByTwoMotorcycles _ -> Error "Occupied by two motorcycle"
    | LargeParkingSlotState.Occupied _ -> Error "Occupied by truck"
    | LargeParkingSlotState.OccupiedByTwoCars _ -> Error "Occupied by two cars"
    | LargeParkingSlotState.OccupiedByMotorcycleAndCar _ -> Error "Occupied by motorcycles and a car"
    | LargeParkingSlotState.OccupiedByTwoMotorcyclesAndCar _ -> Error "Occupied by two motorcycles and a car"
    | LargeParkingSlotState.OccupiedByThreeMotorcycles _ -> Error "Occupied by three motorcycles"
    | LargeParkingSlotState.OccupiedByFourMotorcycles _ -> Error "Occupied by four motorcycles"

let CanParkTruck(slot: ParkingSlot) =
    match slot with
    | Large large -> CanParkTruckInLargeSlot large
    | Compact _ -> false
    | Motorcycle _ -> false

let ParkTruck(slot: ParkingSlot, truck: Truck) =
    match slot with
    | Large largeSlot -> 
        let result = ParkTruckInLargeSlot(largeSlot, truck) 
        match result with
        | Ok large -> Ok(ParkingSlot.Large(large))
        | Error e -> Error e
    | Compact _ -> Error "Cannot park truck in Compact slot"
    | Motorcycle _ -> Error "Cannot park truck in motocycle slot"


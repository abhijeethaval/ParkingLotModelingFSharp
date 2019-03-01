namespace Parking

open Payment
open System

type ParkingOutput = {
    Ticket: UnpaidParkingTicket
    ParkingLot: ParkingLot
}

type UnparkingOutput = {
    Ticket: PaidParkingTicket
    ParkingLot: ParkingLot
}

type ParkVehicle = 
    ParkingLot -> Vehicle -> Result<ParkingOutput, string>

type UnparkVehicle = 
    PaymentMethod //dependency
        -> UnpaidParkingTicket 
        -> TimeSpan 
        -> Result<UnparkingOutput, string>


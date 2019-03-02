# ParkingLotModelingFSharp

This is F# implementation of Car Parking domain modeling. It is inspired from the book "Domain Modeling Made Functional" by Scott Wlaschin and "Types + Properties = Software" blog series by Mark Seemann.

F# is functional first multi paradigm proggramming language with following features:
1. Static typing
2. Algebraic type system 
3. Strong type inference 

These features alongwith support on mainstream programming platform (.NET Framework and .NET Core) and visual studio tooling makes F# ideal for domain modeling for any simple to complex domain.

The parking system problem solved with this solution is described as below:

System should help manage multi floor parking lot. Each floor can have multiple parking slots with different sizes. Parking system manages parking on different vehicle sizes. The parking slots and vehicle types allowed is as follows:

1. Large Parking Slot: It can accomodate a truck or two cars or one car and two motorcycles or four motorcycles. 
2. Compact Parking Slot: It can accomodate one car or two motorcycles.
3. Motorcycle Parking Slot: As name suggests it can accomodate only one motorcycle.

The Parking slots can be empty, partially filled or completely filled.
Motorcycles will be given parking slot as per avaialability in following preference:
1. Motorcycle Parking Slot
2. Compact Parking Slot
3. Large Parking Slot

Cars will be given parking slot as per availability in following preference:
1. Compact Parking Slot
2. Large Parking Slot

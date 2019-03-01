module Payment

type CheckNumber = CheckNumber of int

type UpiId = UpiId of string

type CreditCardNumber = CreditCardNumber of int

type CardType = VISA | MASTER

type CreditCardInfo = {
    Number: CreditCardNumber
    Type: CardType
}

type PaymentMethod = 
    | Cash
    | Check of CheckNumber
    | UPI of UpiId
    | CreditCard of CreditCardInfo

type Currency = INR | USD

type PaymentAmount = PaymentAmount of decimal

type Payment = {
    Amount : PaymentAmount
    Currency : Currency
    Method : PaymentMethod
}
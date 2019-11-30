namespace ParkingCSharp.Payment
{
    public enum CardType { VISA, MASTER }

    public abstract class PaymentMethod { }

    public class CashPaymentMethod : PaymentMethod { }
    public class CheckPaymentMethod : PaymentMethod { public int CheckNumber { get; set; } }
    public class UPIPaymentMethod : PaymentMethod { public string Upi { get; set; } }
    public class CreditCardPaymentMethod : PaymentMethod
    {
        public int CreditCardNumber { get; set; }
        public CardType Type { get; set; }
    }

    public enum CurrencyType { INR, USD }


    public class Payment
    {
        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public PaymentMethod Method { get; set; }
    }
}

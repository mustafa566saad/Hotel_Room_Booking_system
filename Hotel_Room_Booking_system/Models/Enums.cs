namespace Hotel_Room_Booking_system.Models
{

    public enum RoomStatus
    {
        Available,
        Unavailable
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public enum PaymentStatus
    {
        Unpaid,
        Paid,
        Refunded,
        Failed
    }

    public enum CapacityRoom
    {
        Single = 1,
        Double = 2,
        Triple = 3,
        Quadruple = 4,
        Family = 5
    }

    public enum paymentMethod
    {
        CreditCard,
        DebitCard,
        PayPal,
        BankTransfer,
        Cash
    }
}

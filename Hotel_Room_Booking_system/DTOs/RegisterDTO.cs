namespace Hotel_Room_Booking_system.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

public class FitnessMember : User
{
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
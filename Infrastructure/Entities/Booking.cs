public class Booking:BaseEntity
{
    public int FitnessMemberId { get; set; }
    public FitnessMember FitnessMember { get; set; } = new FitnessMember();
    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = new Schedule();
    public DateTime BookingDate { get; set; }
}

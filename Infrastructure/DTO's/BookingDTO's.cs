public class NewBookingDto
{
    public int FitnessMemberId { get; set; }
    public int ScheduleId { get; set; }
    public DateTime BookingDate { get; set; }
}

public class BookingInfoDto
{
    public int Id { get; set; }
    public int FitnessMemberId { get; set; }
    public int ScheduleId { get; set; }
    public DateTime BookingDate { get; set; }
}

public class ModifyBookingDto
{
    public int ScheduleId { get; set; }
    public int FitnessMemberId { get; set; }
    public DateTime BookingDate { get; set; }
}

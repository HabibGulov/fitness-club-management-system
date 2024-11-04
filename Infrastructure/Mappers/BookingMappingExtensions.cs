public static class BookingMappingExtensions
{
    public static Booking ToBooking(this NewBookingDto newBooking)
    {
        return new Booking
        {
            FitnessMemberId = newBooking.FitnessMemberId,
            ScheduleId = newBooking.ScheduleId,
            BookingDate = newBooking.BookingDate
        };
    }

    public static BookingInfoDto ToBookingInfoDto(this Booking booking)
    {
        return new BookingInfoDto
        {
            Id = booking.Id,
            FitnessMemberId = booking.FitnessMemberId,
            ScheduleId = booking.ScheduleId,
            BookingDate = booking.BookingDate
        };
    }

    public static Booking UpdatedBooking(this Booking booking, ModifyBookingDto modifyBooking)
    {
        booking.ScheduleId = modifyBooking.ScheduleId;
        booking.FitnessMemberId = modifyBooking.FitnessMemberId;
        booking.BookingDate = modifyBooking.BookingDate;
        booking.UpdatedAt = DateTime.UtcNow; 
        return booking;
    }

    public static Booking DeleteBooking(this Booking booking)
    {
        booking.IsDeleted=true;
        booking.DeletedAt=DateTime.UtcNow;
        booking.UpdatedAt=DateTime.UtcNow;
        booking.Version+=1;
        return booking;
    }
}
public interface IBookingRepository
{
    Task<Result<PagedResponse<IEnumerable<BookingInfoDto>>>> GetBookings(BookingFilter filter);
    Task<BaseResult> CreateBooking(NewBookingDto info);
    Task<BaseResult> UpdateBooking(int id, ModifyBookingDto modifyBookingDto);
    Task<BaseResult> DeleteBooking(int id);
    Task<Result<BookingInfoDto>> GetBookingById(int id);
}
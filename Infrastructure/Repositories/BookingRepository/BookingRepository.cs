
using Microsoft.EntityFrameworkCore;

public class BookingRepository(FitnessDBContext context) : IBookingRepository
{
    public async Task<BaseResult> CreateBooking(NewBookingDto info)
    {
        bool isAlreadyExist = await context.Bookings.AnyAsync(x => x.ScheduleId == info.ScheduleId && x.FitnessMemberId == info.FitnessMemberId && x.IsDeleted == false);
        if (isAlreadyExist)
            return BaseResult.Failure(Error.AlreadyExist());
        await context.Bookings.AddAsync(info.ToBooking());
        int result = await context.SaveChangesAsync();

        return result is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteBooking(int id)
    {
        Booking? booking = await context.Bookings.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (booking is null)
            return BaseResult.Failure(Error.NotFound());

        booking.DeleteBooking();
        int res = await context.SaveChangesAsync();

        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data was not deleted!"))
            : BaseResult.Success();
    }

    public async Task<Result<BookingInfoDto>> GetBookingById(int id)
    {
        Booking? booking = await context.Bookings.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        return booking is null
    ? Result<BookingInfoDto>.Failure(Error.NotFound())
    : Result<BookingInfoDto>.Success(booking.ToBookingInfoDto());
    }

    public async Task<Result<PagedResponse<IEnumerable<BookingInfoDto>>>> GetBookings(BookingFilter filter)
    {
        IQueryable<Booking> bookings = context.Bookings.Where(x => x.IsDeleted == false);

        if (filter.FitnessMemberId is not null)
            bookings = bookings.Where(x => x.FitnessMemberId == filter.FitnessMemberId);

        int count = await bookings.CountAsync();

        IQueryable<BookingInfoDto> result = bookings
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).Select(x => x.ToBookingInfoDto());

        PagedResponse<IEnumerable<BookingInfoDto>> response = PagedResponse<IEnumerable<BookingInfoDto>>
            .Create(filter.PageNumber, filter.PageSize, count, result);

        return Result<PagedResponse<IEnumerable<BookingInfoDto>>>.Success(response);
    }

    public async Task<BaseResult> UpdateBooking(int id, ModifyBookingDto modifyBookingDto)
    {
        Booking? booking = await context.Bookings.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (booking is null)
            return BaseResult.Failure(Error.NotFound());

        bool conflict = await context.Bookings.AnyAsync(x => x.ScheduleId == modifyBookingDto.ScheduleId && x.FitnessMemberId == modifyBookingDto.FitnessMemberId && x.IsDeleted == false);
        if (conflict)
            return BaseResult.Failure(Error.Conflict());

        booking.UpdatedBooking(modifyBookingDto);
        int res = await context.SaveChangesAsync();

        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!"))
            : BaseResult.Success();
    }
}
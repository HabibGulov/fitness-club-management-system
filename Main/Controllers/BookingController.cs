using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/bookings")]
public sealed class BookingController(IBookingRepository bookingRepository) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BookingFilter filter)
        => (await bookingRepository.GetBookings(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => (await bookingRepository.GetBookingById(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewBookingDto entity)
        => (await bookingRepository.CreateBooking(entity)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ModifyBookingDto entity)
        => (await bookingRepository.UpdateBooking(id, entity)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await bookingRepository.DeleteBooking(id)).ToActionResult();
}

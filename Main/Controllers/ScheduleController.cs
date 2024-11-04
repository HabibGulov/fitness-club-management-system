using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/schedules")]
public sealed class ScheduleController(IScheduleRepository scheduleRepository) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ScheduleFilter filter)
        => (await scheduleRepository.GetSchedules(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => (await scheduleRepository.GetScheduleById(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewScheduleDto entity)
        => (await scheduleRepository.CreateSchedule(entity)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ModifyScheduleDto entity)
        => (await scheduleRepository.UpdateSchedule(id, entity)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await scheduleRepository.DeleteSchedule(id)).ToActionResult();
}

using Microsoft.AspNetCore.Mvc;

[Route("/api/workouts")]
public sealed class WorkoutController(IWorkoutRepository workoutRepository) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] WorkoutFilter filter)
        => (await workoutRepository.GetWorkouts(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute]int id)
        => (await workoutRepository.GetWorkoutById(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewWorkoutDto entity)
        => (await workoutRepository.CreateWorkout(entity)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ModifyWorkoutDto entity)
        => (await workoutRepository.UpdateWorkout(id, entity)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
        => (await workoutRepository.DeleteWorkout(id)).ToActionResult();
}
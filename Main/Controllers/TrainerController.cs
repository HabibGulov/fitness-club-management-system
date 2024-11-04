using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/trainers")]
public sealed class TrainerController(ITrainerRepository trainerRepository) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] TrainerFilter filter)
        => (await trainerRepository.GetTrainers(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => (await trainerRepository.GetTrainerById(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewTrainerDto entity)
        => (await trainerRepository.CreateTrainer(entity)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ModifyTrainerDto entity)
        => (await trainerRepository.UpdateTrainer(id, entity)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await trainerRepository.DeleteTrainer(id)).ToActionResult();
}
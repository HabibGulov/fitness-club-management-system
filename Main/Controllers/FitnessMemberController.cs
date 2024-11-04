using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/fitness-members")]
public sealed class FitnessMemberController(IFitnessMemberRepository fitnessMemberRepository) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FitnessMemberFilter filter)
        => (await fitnessMemberRepository.GetFitnessMembers(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => (await fitnessMemberRepository.GetFitnessMemberById(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewFitnessMemberDto entity)
        => (await fitnessMemberRepository.CreateFitnessMember(entity)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ModifyFitnessMemberDto entity)
        => (await fitnessMemberRepository.UpdateFitnessMember(id, entity)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await fitnessMemberRepository.DeleteFitnessMember(id)).ToActionResult();
}
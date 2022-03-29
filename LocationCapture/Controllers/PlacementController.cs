using LocationCapture.Core;
using LocationCapture.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LocationCapture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlacementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(nameof(GetPlacements))]
        public async Task<ActionResult<IList<Placement>>> GetPlacements()
        {
            var placements = await _unitOfWork.Placements.GetAll();
            
            if (placements.Count() == 0) return NotFound();

            return Ok(placements.ToList());
        }

        [HttpGet(nameof(GetPlacementById))]
        public async Task<ActionResult<Placement>> GetPlacementById([FromQuery] int id)
        {
            var placement = await _unitOfWork.Placements.Get(id);

            if (placement == null) return NotFound();

            return Ok(placement);            
        }

        [HttpGet(nameof(GetSpeedOfMovement))]
        public async Task<ActionResult<double>> GetSpeedOfMovement([FromQuery] int id, [FromQuery] int id2)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var speedOfMovement = await _unitOfWork.Placements.CalculateSpeedOfMovement(id, id2);

            if (speedOfMovement == 0.0) return NotFound();

            return Ok(speedOfMovement);
        }

        [HttpPost(nameof(CreatePlacement))]
        public async Task<IActionResult> CreatePlacement(Placement placement)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _unitOfWork.Placements.Add(placement);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}

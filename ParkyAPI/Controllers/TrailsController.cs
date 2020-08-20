using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/Trails/")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository _trailRepo;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository trailRepo, IMapper mapper)
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TrailDto>))]
        public IActionResult GetTrails()
        {
            var trails = _trailRepo.GetTrails();
            var trailDto = new List<TrailDto>();
            foreach (var trail in trails)
            {
                trailDto.Add(_mapper.Map<TrailDto>(trail));
            }
            return Ok(trailDto);
        }

        /// <summary>
        /// Get individual trail
        /// </summary>
        /// <param name="trailId">The Id of the trail</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepo.GetTrail(trailId);
            if (trail == null)
            {
                return NotFound();
            }
            var trailDto = _mapper.Map<TrailDto>(trail);
            return Ok(trailDto);
        }

        /// <summary>
        /// Create new trail
        /// </summary>
        /// <param name="trailDto">New trail object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailDto trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_trailRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail Exists!");
                return StatusCode(404, ModelState);
            }

            var trail = _mapper.Map<Trail>(trailDto);
            if (!_trailRepo.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { trailId = trail.Id }, trail);
        }

        /// <summary>
        /// Update existing trail based upon Id
        /// </summary>
        /// <param name="trailId">Trail Id</param>
        /// <param name="trailDto">New trail to update</param>
        /// <returns></returns>
        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailDto trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (_trailRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail Park Exists!");
                return StatusCode(404, ModelState);
            }

            var trail = _mapper.Map<Trail>(trailDto);
            if (!_trailRepo.UpdateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes trail based upon Id
        /// </summary>
        /// <param name="trailId">Trail Id</param>
        /// <returns></returns>
        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepo.TrailExists(trailId))
            {
                return NotFound();
            }

            var trail = _trailRepo.GetTrail(trailId);
            if (!_trailRepo.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

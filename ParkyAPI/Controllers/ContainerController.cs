using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/Containers")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ContainerController : ControllerBase
    {
        private readonly IContainerRepository _containerRepo;
        private readonly IMapper _mapper;

        public ContainerController(IContainerRepository containerRepo, IMapper mapper)
        {
            _containerRepo = containerRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Container>))]
        public IActionResult GetContainers()
        {
            var containers = _containerRepo.GetContainers();
            var containersDto = new List<ContainerDto>();

            foreach (var c in containers)
            {
                containersDto.Add(_mapper.Map<ContainerDto>(c));
            }
            return Ok(containersDto);
        }

        [HttpGet("{containerId:int}", Name = "GetContainer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Container>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetContainer(int containerId)
        {
            var container = _containerRepo.GetContainer(containerId);
            if (container == null)
            {
                return NotFound();
            }

            var containerDto = _mapper.Map<ContainerDto>(container);
            return Ok(containerDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Container))]
        public IActionResult CreateContainer([FromBody] ContainerDto containerDto)
        {
            if (containerDto == null)
            {
                return BadRequest(ModelState);
            }

            var container = _mapper.Map<Container>(containerDto);
            if (!_containerRepo.CreateContainer(container))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {container.ContainerId} : {container.ContainerName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetContainer", new { containerId = container.ContainerId }, container);
        }
    }
}

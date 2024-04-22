using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nzwalks.Data;
using Nzwalks.Modals.Domain;
using Nzwalks.Modals.DTO;

namespace Nzwalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            //Get Data From Database Domain
            var regionsDomain = dbContext.Regions.ToList();

            //map domain  models to DTOS
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });                    
            }


            //Return DTOs back to client
            return Ok(regionsDto);

        }

        //get regions by id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
           // var region = dbContext.Regions.Find(id);

            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);


            if (regionDomain == null)
            {
                return NotFound();
            }

            //map region Domain Model to region Dto
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };
            return Ok(regionDto);
        }


        //Post method post to create a new region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            //Convert DTO to domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            //use domain model to create region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //map domain model back to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id=regionDto.Id}, regionDto);
        }
    }
}

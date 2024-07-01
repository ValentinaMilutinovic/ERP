using AutoMapper;
using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodjacController : Controller
    {
        private readonly IProizvodjacRepository proizvodjacRepository;
        public readonly IMapper mapper;



        public ProizvodjacController(IProizvodjacRepository proizvodjacRepository, IMapper mapper)

        {
            this.proizvodjacRepository = proizvodjacRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetManufacturersAsync()
        {
            var proizvodjacDomain = await proizvodjacRepository.GetAllAsync();


            var proizvodjacDTO = mapper.Map<List<Models.DTO.Proizvodjac>>(proizvodjacDomain);


            return Ok(proizvodjacDTO);
        }
        [HttpGet]
        [Route("{Idproizvodjaca:int}")]
        [ActionName("GeProizvodjacAsync")]
        public async Task<IActionResult> GetManufacturerAsync(int Idproizvodjaca)
        {
            var manufacturer = await proizvodjacRepository.GetAsync(Idproizvodjaca);

            if (manufacturer == null)
            {

                return NotFound("There is not Proizvodjac with taht ID");
            }


            var manufacturerDTO = mapper.Map<Models.DTO.Proizvodjac>(manufacturer);

            return Ok(manufacturerDTO);
        }
        //, Authorize(Roles = "Admin")
        [HttpPost]
        public async Task<IActionResult> AddManufacturerAsync(AddProizvodjacRequest addProizvodjacRequest)
        {
            var manufacturer = new Db.TblProizvodjac()
            {

                //     DocumentId = Guid.NewGuid(),
                NazivProizvodjaca = addProizvodjacRequest.NazivProizvodjaca,
                ZemljaPorekla = addProizvodjacRequest.ZemljaPorekla
            };

            manufacturer = await proizvodjacRepository.AddAsync(manufacturer);

            var manufacturerDTO = mapper.Map<Proizvodjac>(manufacturer);


            return Ok(manufacturerDTO);
        }
        //, Authorize(Roles = "Admin")

        [HttpDelete]
        [Route("{Idproizvodjaca:int}")]
        public async Task<IActionResult> DeletdManufacturerAsync(int Idproizvodjaca)
        {

            //get doc from database

            var manufacturer = await proizvodjacRepository.DeleteAsync(Idproizvodjaca);

            if (manufacturer == null)
            {
                return NotFound("There is not Proizvodjac with taht ID");
            }

            var manufacturerDTO = mapper.Map<Proizvodjac>(manufacturer);


            return Ok(manufacturerDTO);

        }

        //, Authorize(Roles = "Admin")
        [HttpPut]
        [Route("{Idproizvodjaca:int}")]
        public async Task<IActionResult> UpdateManufacturerAsync([FromRoute] int Idproizvodjaca, [FromBody] UpdateProizvodjacRequest updateProizvodjacRequest)
        {
            var manufacturer = new Db.TblProizvodjac()
            {

                NazivProizvodjaca = updateProizvodjacRequest.NazivProizvodjaca,
                ZemljaPorekla = updateProizvodjacRequest.ZemljaPorekla
            };

            manufacturer = await proizvodjacRepository.UpdateAsync(Idproizvodjaca, manufacturer);

            if (manufacturer == null)
            {
                return NotFound("There is not Proizvodjac with taht ID");
            }
            var manufacturerDTO = mapper.Map<Proizvodjac>(manufacturer);




            return Ok(manufacturerDTO);

        }
    }
}

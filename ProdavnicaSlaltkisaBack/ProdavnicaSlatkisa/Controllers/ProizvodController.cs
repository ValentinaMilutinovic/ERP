using AutoMapper;
using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodController : Controller
    {
        private readonly IProizvodRepository proizvodRepository;
        private readonly IMapper mapper;

        public ProizvodController(IProizvodRepository proizvodRepository, IMapper mapper)

        {
            this.proizvodRepository = proizvodRepository;
            this.mapper = mapper;
        }
        


        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetProizvodAsync")]
        public async Task<IActionResult> GetProizvodAsync(int id)
        {
            var proizvod = await proizvodRepository.GetAsync(id);

            if (proizvod == null)
            {

                return NotFound("There is not product with taht ID");
            }


            var dto = mapper.Map<Proizvod>(proizvod);

            return Ok(dto);
        }
        
        [HttpGet]
        [Route("update/{id:int}")]
        [ActionName("GetProizvodUpdateAsync")]
        public async Task<IActionResult> GetProizvodUpdateAsync(int id)
        {
            var proizvod = await proizvodRepository.GetAsync(id);

            if (proizvod == null)
            {

                return NotFound("There is not product with taht ID");
            }


            var dto = mapper.Map<Proizvod>(id);

            return Ok(dto);
        }

        //, Authorize(Roles = "Admin")
        [HttpPost]
        public async Task<IActionResult> AddProizvodAsync([FromBody] AddProzivodRequest request)
        {
            var proizvod = new Db.TblProizvod
            {
                Cena = request.Cena,
                Idproizvodjaca = request.Idproizvodjaca,
                TipProizvodaId = request.TipProizvodaId,
                KolicinaNaStanju = request.KolicinaNaStanju,
                Naziv = request.Naziv
            };
            proizvod = await proizvodRepository.AddAsync(proizvod);

            var dto = mapper.Map<Proizvod>(proizvod);


            return Ok(dto);
        }

        //, Authorize(Roles = "Admin")
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProizvodAsync(int id)
        {

            //get doc from database

            var proizvod = await proizvodRepository.DeleteAsync(id);

            if (proizvod == null)
            {
                return NotFound("There is not product with taht ID");
            }

            var dto = mapper.Map<Proizvod>(proizvod);

            return Ok(dto);

        }

        //, Authorize(Roles = "Admin"
        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateProizvodAsync([FromRoute] int id ,[FromBody] UpdateProizvodRequest request)
        {
            var proizvod = new Db.TblProizvod()
            {
                Cena = request.Cena,
                Idproizvodjaca = request.Idproizvodjaca,
                TipProizvodaId = request.TipProizvodaId,
                KolicinaNaStanju = request.KolicinaNaStanju,
                Naziv = request.Naziv
            };

            proizvod = await proizvodRepository.UpdateAsync(id, proizvod);

            if (proizvod == null)
            {
                return NotFound("There is not product with taht ID");
            }
            var dto = mapper.Map<Proizvod>(proizvod);

            return Ok(dto);

        }
        [HttpGet]
        public async Task<IActionResult> GetProizvodAsync()
        {
            var proizvod = await proizvodRepository.GetAllAsync();

            var dto = mapper.Map<List<Proizvod>>(proizvod);

            return Ok(dto);
        }
    }
}

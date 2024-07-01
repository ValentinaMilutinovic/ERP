using AutoMapper;
using ProdavnicaSlatkisa.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipProizvodumController : Controller
    {
        private readonly ITipProizvodumRepository tipProizvodumRepository;
        public readonly IMapper mapper;



        public TipProizvodumController(ITipProizvodumRepository tipProizvodumRepository, IMapper mapper)

        {
            this.tipProizvodumRepository = tipProizvodumRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTypeOfProductsAsync()
        {
            var typeDomain = await tipProizvodumRepository.GetAllAsync();


            var typelDTO = mapper.Map<List<Models.DTO.TipProizvoda>>(typeDomain);


            return Ok(typelDTO);
        }

        [HttpGet]
        [Route("{TipProizvodaId:int}")]
        [ActionName("GTypeOfProductAsync")]
        public async Task<IActionResult> GetTypeOfProductAsync(int TipProizvodaId)
        {
            var type = await tipProizvodumRepository.GetAsync(TipProizvodaId);

            if (type == null)
            {

                return NotFound("There is not Tip proizvoda with taht ID");
            }


            var typeDTO = mapper.Map<Models.DTO.TipProizvoda>(type);

            return Ok(typeDTO);
        }
        //, Authorize(Roles ="Admin")
        [HttpPost]
        public async Task<IActionResult> AddTypeOfProductAsync(Models.DTO.AddTipProizvodumRequest addTipProizvodumRequest)
        {
            var type = new Db.TblTipProizvodum()
            {

                //     DocumentId = Guid.NewGuid(),
                Sastav = addTipProizvodumRequest.Sastav
            };

            type = await tipProizvodumRepository.AddAsync(type);

            var typeDTO = mapper.Map<Models.DTO.TipProizvoda>(type);



            return Ok(typeDTO);
        }
        //, Authorize(Roles = "Admin")

        [HttpDelete]
        [Route("{TipProizvodaId:int}")]
        public async Task<IActionResult> DeleteTypeOfProductAsync(int TipProizvodaId)
        {

            //get doc from database

            var type = await tipProizvodumRepository.DeleteAsync(TipProizvodaId);

            if (type == null)
            {
                return NotFound("There is not Tip proizvoda with taht ID");
            }

            var typeDTO = mapper.Map<Models.DTO.TipProizvoda>(type);


            return Ok(typeDTO);

        }
        //, Authorize(Roles = "Admin")
        [HttpPut]
        [Route("{TipProizvodaId:int}")]
        public async Task<IActionResult> UpdateTypeOfProductAsync([FromRoute] int TipProizvodaId, [FromBody] Models.DTO.UpdateTipProizvodumRequest updateTipProizvodumRequest)
        {
            var type = new Db.TblTipProizvodum()
            {

                Sastav = updateTipProizvodumRequest.Sastav
            };

            type = await tipProizvodumRepository.UpdateAsync(TipProizvodaId, type);

            if (type == null)
            {
                return NotFound("There is not Tip proizvoda with taht ID");
            }
            var typeDTO = mapper.Map<Models.DTO.TipProizvoda>(type);




            return Ok(typeDTO);

        }
    }
}

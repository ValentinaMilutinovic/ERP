using AutoMapper;
using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorpaController : Controller
    {
        private readonly IKorpaRepository korpaRepository;
        private readonly IRacunRepository racunRepository;
        private readonly IMapper mapper;

        public KorpaController(IKorpaRepository korpaRepository, IMapper mapper, IRacunRepository racunRepository)

        {
            this.korpaRepository = korpaRepository;
            this.mapper = mapper;
            this.racunRepository = racunRepository;
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCartsAsync()
        {
            var cart = await korpaRepository.GetAllAsync();



            var cartDTO = mapper.Map<List<Models.DTO.Korpa>>(cart);


            return Ok(cartDTO);
        }
        [HttpGet]
        [Route("KorpaId/{KorpaId:int}")]
        [ActionName("GetCartAsync")]
        public async Task<IActionResult> GetCartAsync(int KorpaId)
        {
            var cart = await korpaRepository.GetAsync(KorpaId);

            if (cart == null)
            {

                return NotFound("Korpa not found");
            }


            var cartDTO = mapper.Map<Models.DTO.Korpa>(cart);

            return Ok(cartDTO);
        }
        

        [HttpPost]
        public async Task<IActionResult> AddCartAsync([FromBody] Models.DTO.AddKorpaRequest addKorpaRequest)
        {
            var cart = new Db.TblKorpa
            {

                //     DocumentId = Guid.NewGuid(),
                KorisnikId = addKorpaRequest.KorisnikId,
                UkupanIznos = 0,
                BrProizvoda = 0
            };
            

            cart = await korpaRepository.AddAsync(cart);

            var cartDTO = mapper.Map<Models.DTO.Korpa>(cart);


            return Ok(cart);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{KorpaId:int}")]
        public async Task<IActionResult> DeleteCartAsync(int KorpaId)
        {

            //get doc from database

            var cart = await korpaRepository.DeleteAsync(KorpaId);

            if (cart == null)
            {
                return NotFound("Korpa not found");
            }

            var cartDTO = mapper.Map<Models.DTO.Korpa>(cart);

            return Ok(cartDTO);

        }
        [HttpPut]
        [Route("{KorpaId:int}")]

        public async Task<IActionResult> UpdateCartAsync([FromRoute] int KorpaId, [FromBody] Models.DTO.UpdateKorpaRequest updateKorpaRequest)
        {
            var cart = new Db.TblKorpa()
            {
                KorisnikId = updateKorpaRequest.KorisnikId,
                UkupanIznos = updateKorpaRequest.UkupanIznos,
                BrProizvoda = updateKorpaRequest.BrProizvoda
            };

            cart = await korpaRepository.UpdateAsync(KorpaId, cart);

            if (cart == null)
            {
                return NotFound("Korpa ot found");
            }
            var cartDTO = mapper.Map<Models.DTO.Korpa>(cart);



            return Ok(cartDTO);

        }
    }
}

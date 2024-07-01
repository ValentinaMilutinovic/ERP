using AutoMapper;
using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProizvodUKorpiController : Controller
    {
        private readonly IProizvodUKorpiRepository proizvodUKorpiRepository;
        private readonly IMapper mapper;
        private readonly IKorpaRepository korpaRepository;

        public ProizvodUKorpiController(IProizvodUKorpiRepository proizvodUKorpiRepository, IMapper mapper, IKorpaRepository korpaRepository)

        {
            this.proizvodUKorpiRepository = proizvodUKorpiRepository;
            this.mapper = mapper;
            this.korpaRepository = korpaRepository;
        }
        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetCartItemsAsync()
        {
            var item = await proizvodUKorpiRepository.GetAllAsync();



            var itemDTO = mapper.Map<List<Models.DTO.ProizvodUKorpi>>(item);


            return Ok(itemDTO);
        }
        [HttpGet]
        [Route("{ProizUkorpiId:int}")]
        [ActionName("GetCatrItemAsync")]
        public async Task<IActionResult> GetCartItemAsync(int ProizUkorpiId)
        {
            try
            {
                var cartItem = await proizvodUKorpiRepository.GetAsync(ProizUkorpiId);

                if (cartItem == null)
                {

                    return NotFound("There is not cart item with taht ID");
                }


                var cartItemDTO = mapper.Map<Models.DTO.ProizvodUKorpi>(cartItem);

                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCartItemAsync([FromBody] Models.DTO.AddProizvodUKorpiRequest addProizvodUKorpiRequest)
        {
            try {
                var cartItem = new Db.TblProizvodUkorpi()
                {
                    BrojKomada = addProizvodUKorpiRequest.BrojKomada,
                    ProizvodId = addProizvodUKorpiRequest.Idproizvoda,
                    KorpaId = addProizvodUKorpiRequest.KorpaId,
                };

                cartItem = await proizvodUKorpiRepository.AddAsync(cartItem);

                bool positive = true;

                await korpaRepository.CalculateTotal(cartItem.KorpaId, cartItem.Iznos, positive, cartItem.BrojKomada);

                var cartItemDTO = mapper.Map<Models.DTO.ProizvodUKorpi>(cartItem);


                return Ok(cartItemDTO);


            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{ProizUkorpiId:int}")]
        /* public async Task<IActionResult> DeleteCartItemAsync(int ProizUkorpiId)
         {

             //get doc from database
             var existingPrizUKor = (TblProizvodUkorpi)await proizvodUKorpiRepository.GetAsync(ProizUkorpiId);
             var amount = 0 - existingPrizUKor.Iznos;
             var cartItem = await proizvodUKorpiRepository.DeleteAsync(ProizUkorpiId);
             bool positive = false;
             await korpaRepository.CalculateTotal(existingPrizUKor.KorpaId, amount, positive);
             if (cartItem == null)
             {
                 return NotFound("There is not cart item with taht ID");
             }

             var cartItemDTO = mapper.Map<Models.DTO.ProizvodUKorpi>(cartItem);

             return Ok(cartItemDTO);

         }*/
        public async Task<IActionResult> DeleteCartItemAsync(int ProizUkorpiId)
        {
            try
            {
                var existingPrizUKor = await proizvodUKorpiRepository.GetAsync(ProizUkorpiId);

                if (existingPrizUKor == null)
                {
                    return NotFound("There is no cart item with that ID");
                }

                var amount = -existingPrizUKor.Iznos;
                bool positive = false;

                var cartItem = await proizvodUKorpiRepository.DeleteAsync(ProizUkorpiId);

                await korpaRepository.CalculateTotal(existingPrizUKor.KorpaId, Math.Abs(amount), positive, existingPrizUKor.BrojKomada);

                if (cartItem == null)
                {
                    return NotFound("There is no cart item with that ID");
                }

                var cartItemDTO = mapper.Map<Models.DTO.ProizvodUKorpi>(cartItem);

                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{ProizUkorpiId:int}")]

        public async Task<IActionResult> UpdateCartItemAsync([FromRoute] int ProizUkorpiId, [FromBody] Models.DTO.UpdateProizvodUKorpiRequest updateProizvodUKorpiRequest)
        {
            try
            {
                var existingPrizUKor = await proizvodUKorpiRepository.GetAsync(ProizUkorpiId);

                if (existingPrizUKor == null)
                {
                    return NotFound("There is no cart item with that ID");
                }

                var originalIznos = existingPrizUKor.Iznos;
                var originalBrKom = existingPrizUKor.BrojKomada;
                existingPrizUKor.BrojKomada = updateProizvodUKorpiRequest.BrojKomada;

                var updatedCartItem = await proizvodUKorpiRepository.UpdateAsync(ProizUkorpiId, existingPrizUKor);

                var diff = updatedCartItem.Iznos - originalIznos;
                bool positive = diff >= 0;
                var brDif = updateProizvodUKorpiRequest.BrojKomada - originalBrKom;

                await korpaRepository.CalculateTotal(existingPrizUKor.KorpaId, Math.Abs(diff), positive, Math.Abs(brDif));

                var cartItemDTO = mapper.Map<Models.DTO.ProizvodUKorpi>(updatedCartItem);

                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("korpa/{korpaId:int}")]
        public async Task<IActionResult> GetProductInShoppingCartByKorpaIdAsync(int korpaId)
        {
            var productInShoppingCartEntities = await proizvodUKorpiRepository.GetByKorpaIdAsync(korpaId);

            if (productInShoppingCartEntities == null || !productInShoppingCartEntities.Any())
            {
                return NotFound("There are no products in the shopping cart with this korpaId.");
            }

            var productInShoppingCartDtos = productInShoppingCartEntities.Select(entity => mapper.Map<Models.DTO.ProizvodUKorpi>(entity));
            return Ok(productInShoppingCartDtos);
        }
    }
}

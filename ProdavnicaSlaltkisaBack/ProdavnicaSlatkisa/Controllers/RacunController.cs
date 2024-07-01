using AutoMapper;
using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RacunController : Controller
    {
        private readonly IRacunRepository racunRepository;
        public readonly IMapper mapper;
        private readonly IKorpaRepository korpaRepository;



        public RacunController(IRacunRepository racunRepository, IMapper mapper, IKorpaRepository korpaRepository)

        {
            this.racunRepository = racunRepository;
            this.mapper = mapper;
            this.korpaRepository = korpaRepository;
        }
        //, Authorize(Roles = "Admin")
        [HttpGet]
        public async Task<IActionResult> GetReceiptsAsync()
        {
            var receipt = await racunRepository.GetAllAsync();


            var receiptDTO = mapper.Map<List<Models.DTO.Racun>>(receipt);


            return Ok(receiptDTO);
        }


        [HttpGet]
        [Route("RacunId/{RacunId:int}")]
        [ActionName("GetReceiptAsync")]
        public async Task<IActionResult> GetReceiptAsync(int RacunId)
        {
            var receipt = await racunRepository.GetAsync(RacunId);

            if (receipt == null)
            {

                return NotFound("There is not Racun with taht ID");
            }


            var receiptDTO = mapper.Map<Models.DTO.Racun>(receipt);

            return Ok(receiptDTO);
        }
        [HttpGet]
        [Route("korpa/{KorpaId:int}")]
        [ActionName("GetKorpaByRacunIDAsync")]
        public async Task<IActionResult> GetRacunByKorpaIDAsync(int KorpaId)
        {
            var cart = await racunRepository.GetRacunByKorpaIDAsync(KorpaId);

            if (cart == null)
            {

                return NotFound("Korpa with givven racunId not found");
            }
            /*if (cart.RacunId != null)
            {
                await racunRepository.AddDiscount(cart.KorisnikId, cart.RacunId, cart.KorpaId);
            }*/

            var cartDTO = mapper.Map<Models.DTO.Korpa>(cart);

            return Ok(cartDTO);

        }
        [HttpPost]
        public async Task<IActionResult> AddReceiptAsync(Models.DTO.AddRacunRequest addRacunRequest)
        {
            
            var receipt = new Db.TblRacun()
            {

                //     DocumentId = Guid.NewGuid(),
                //UkupanIznos =0,
                DatumKupovine = DateTime.Today,
                VremeKupovine = DateTime.Now.TimeOfDay,
                KorpaId=addRacunRequest.KorpaId
               /* Popust = false,
                ProcenatPop = 0,
                IznosSaPopustom = 0*/
            };
            var korpa = (TblKorpa)await korpaRepository.GetAsync(addRacunRequest.KorpaId);
            var korisnik = korpa.KorisnikId;

            

            receipt = await racunRepository.AddAsync(receipt);
            await racunRepository.AddDiscount(korisnik, receipt.RacunId, addRacunRequest.KorpaId);

            var receiptDTO = mapper.Map<Models.DTO.Racun>(receipt);



            return Ok(receiptDTO);
        }
        //, Authorize(Roles = "Admin")
        [HttpDelete]
        [Route("{RacunId:int}")]
        public async Task<IActionResult> DeleteReceiptAsync(int RacunId)
        {

            //get doc from database

            var receipt = await racunRepository.DeleteAsync(RacunId);

            if (receipt == null)
            {
                return NotFound("There is not Racun with taht ID");
            }

            var receiptDTO = mapper.Map<Models.DTO.Racun>(receipt);


            return Ok(receiptDTO);

        }
        //, Authorize(Roles = "Admin")
        [HttpPut]
        [Route("{RacunId:int}")]
        public async Task<IActionResult> UpdateReceiptAsync([FromRoute] int RacunId, [FromBody] Models.DTO.UpdateRacunRequest updateRacunRequest)
        {
            var receipt = new Db.TblRacun()
            {

                UkupanIznos = updateRacunRequest.UkupanIznos,
                DatumKupovine = updateRacunRequest.DatumKupovine,
                VremeKupovine = updateRacunRequest.VremeKupovine,
                Popust = updateRacunRequest.Popust,
                ProcenatPop = updateRacunRequest.ProcenatPop,
                IznosSaPopustom = updateRacunRequest.IznosSaPopustom
            };

            receipt = await racunRepository.UpdateAsync(RacunId, receipt);

            if (receipt == null)
            {
                return NotFound("There is not Racun with taht ID");
            }
            var receiptDTO = mapper.Map<Models.DTO.Racun>(receipt);




            return Ok(receiptDTO);

        }
        [HttpGet]
        [Route("payment-intent/{paymentIntentId}")]
        public async Task<IActionResult> GetBillByPaymentIntentIdAsync(string paymentIntentId)
        {
            var billEntity = await racunRepository.GetByPaymentIntentIdAsync(paymentIntentId);

            if (billEntity == null)
            {
                return NotFound("There is no bill with this PaymentIntentId.");
            }

            var billDto = mapper.Map<Models.DTO.Racun>(billEntity);
            return Ok(billDto);
        }

    }
}

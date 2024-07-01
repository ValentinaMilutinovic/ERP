using AutoMapper;
using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikController : Controller
    {
        private readonly IKorisnikRepository korisnikRepository;
        public readonly IMapper mapper;



        public KorisnikController(IKorisnikRepository korisnikRepository, IMapper mapper)

        {
            this.korisnikRepository = korisnikRepository;
            this.mapper = mapper;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserssAsync()
        {
            var kuser = await korisnikRepository.GetAllAsync();


            var kuserDTO = mapper.Map<List<Models.DTO.Korisnik>>(kuser);


            return Ok(kuserDTO);
        }

        [HttpGet,Authorize(Roles = "Admin,User")]
        [Route("{KorisnikId:int}")]
        [ActionName("GetUserAsync")]
        public async Task<IActionResult> GetUserAsync(int KorisnikId)
        {
            var user = await korisnikRepository.GetAsync(KorisnikId);

            if (user == null)
            {

                return NotFound("Korisnik not found");
            }


            var userDTO = mapper.Map<Models.DTO.Korisnik>(user);

            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(Models.DTO.AddKorisnikRequest addKorisnikRequest)
        {
            var user = new Db.TblKorisnik()
            {

                //     DocumentId = Guid.NewGuid(),
                Ime = "Guest",
                Prezime = "Guest",
                Email = "Guest",
                Kontakt = "Guest",
                Adresa = "Guest",
                Grad = "Guest"
            };

            user = await korisnikRepository.AddAsync(user);

            var userDTO = mapper.Map<Models.DTO.Korisnik>(user);

            return Ok(userDTO);
        }


        [HttpDelete]
        [Route("{KorisnikId:int}")]
        public async Task<IActionResult> DeleteUserAsync(int KorisnikId)
        {

            //get doc from database

            var user = await korisnikRepository.DeleteAsync(KorisnikId);

            if (user == null)
            {
                return NotFound("Korisnik not found");
            }

            var userDTO = mapper.Map<Models.DTO.Korisnik>(user);


            return Ok(userDTO);

        }

        [HttpPut]
        [Route("{KorisnikId:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int KorisnikId, [FromBody] Models.DTO.UpdateKorisnikRequest updateKorisnikRequest)
        {
            var user = new Db.TblKorisnik()
            {
                Ime = updateKorisnikRequest.Ime,
                Prezime = updateKorisnikRequest.Prezime,
                Email = updateKorisnikRequest.Email,
                Kontakt = updateKorisnikRequest.Kontakt,
                Adresa = updateKorisnikRequest.Adresa,
                Grad = updateKorisnikRequest.Grad
            };

            user = await korisnikRepository.UpdateAsync(KorisnikId, user);

            if (user == null)
            {
                return NotFound("Korisnik not found");
            }
            var userDTO = mapper.Map<Models.DTO.Korisnik>(user);




            return Ok(userDTO);

        }
    }
}

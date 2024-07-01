using AutoMapper;
using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KupacController : Controller
    {
        private readonly IKupacRepository kupacRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IKorisnikRepository korisnikRepository;

        public KupacController(IKupacRepository kupacRepository, IMapper mapper, IConfiguration configuration, IKorisnikRepository korisnikRepository)

        {
            this.kupacRepository = kupacRepository;
            this.mapper = mapper;
            this.configuration = configuration;
            this.korisnikRepository= korisnikRepository;
        }
        //,Authorize(Roles = "Admin")
        [HttpGet]
        public async Task<IActionResult> GetBuyersAsync()
        {
            var buyer = await kupacRepository.GetAllAsync();



            var buyerDTO = mapper.Map<List<Models.DTO.Kupac>>(buyer);


            return Ok(buyerDTO);
        }
        //,Authorize(Roles = "Admin,User")
        [HttpGet]
        [Route("{KupacId:int}")]
        [ActionName("GetBuyerAsync")]
        public async Task<IActionResult> GetBuyerAsync(int KupacId)
        {
            var buyer = await kupacRepository.GetAsync(KupacId);

            if (buyer == null)
            {

                return NotFound("User not found");
            }


            var buyerDTO = mapper.Map<Models.DTO.Kupac>(buyer);

            return Ok(buyerDTO);
        }

        [HttpGet]
        [Route("UsernameRk/{UsernameRk}")]
        [ActionName("GetByUernameAsync")]
        public async Task<IActionResult> GetByUernameAsync(string UsernameRk)
        {
            var buyer = await kupacRepository.GetByUsernameAsync(UsernameRk);

            if (buyer == null)
            {

                return NotFound("User not found");
            }


            var buyerDTO = mapper.Map<Models.DTO.Kupac>(buyer);

            return Ok(buyerDTO);
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddBuyerAsync([FromBody] Models.DTO.AddKupacRequest addKupacRequest)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(addKupacRequest.LozinkaRk);
            TblKorisnik korisnik = await korisnikRepository.GetKorisnikByDetails(addKupacRequest);


            var buyer = new Db.TblKupac
            {

                //     DocumentId = Guid.NewGuid(),
                // KupacId =addKupacRequest.KupacId,
                UsernameRk = addKupacRequest.UsernameRk,
                LozinkaRk = passwordHash,
                BrojKupovina = 0,
                Registrovan = addKupacRequest.Registrovan,
                Korisnik = korisnik
            };
            buyer = await kupacRepository.AddAsync(buyer);

            var buyerDTO = mapper.Map<Models.DTO.Kupac>(buyer);


            return Ok(buyerDTO);
        }
        //,Authorize(Roles = "Admin,User")
        [HttpDelete]
        [Route("{KupacId:int}")]
        public async Task<IActionResult> DeleteBuyerAsync(int KupacId)
        {

            //get doc from database

            var buyer = await kupacRepository.DeleteAsync(KupacId);

            if (buyer == null)
            {
                return NotFound("User not found");
            }

            var buyerDTO = mapper.Map<Models.DTO.Kupac>(buyer);

            return Ok(buyerDTO);

        }
        //,Authorize(Roles = "Admin,User")
        [HttpPut]
        [Route("{KupacId:int}")]

        public async Task<IActionResult> UpdateBuyerAsync([FromRoute] int KupacId, [FromBody] Models.DTO.UpdateKupacRequest updateKupacRequest)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(updateKupacRequest.LozinkaRk);
            var buyer = new Db.TblKupac()
            {
                UsernameRk = updateKupacRequest.UsernameRk,
                LozinkaRk = passwordHash,
                BrojKupovina = updateKupacRequest.BrojKupovina,
                Registrovan = updateKupacRequest.Registrovan
            };

            buyer = await kupacRepository.UpdateAsync(KupacId, buyer);

            if (buyer == null)
            {
                return NotFound();
            }
            var buyerDTO = mapper.Map<Models.DTO.Kupac>(buyer);



            return Ok(buyerDTO);

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginBuyerAsync(Models.DTO.LoginBuyerRequest loginBuyerRequest)
        {
            var buyerEntity = await kupacRepository.GetByUsernameAsync(loginBuyerRequest.UsernameRk);


            if (buyerEntity == null)
            {
                return NotFound("User not found");
            }

            // Verify if the password is correct
            if (!BCrypt.Net.BCrypt.Verify(loginBuyerRequest.LozinkaRk, buyerEntity.LozinkaRk))
            {
                return Unauthorized("Invalid password");
            }

            string token = CreateToken(buyerEntity);
            return Ok(token);
        }
        private string CreateToken(TblKupac kupac)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name,kupac.UsernameRk),
                new Claim(ClaimTypes.Role, "User")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

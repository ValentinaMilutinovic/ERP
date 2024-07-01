using AutoMapper;
using ProdavnicaSlatkisa.API.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProdavnicaSlatkisa.API.Contracts;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorController : Controller
    {
        private readonly IAdministratorRepository administratorRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AdministratorController(IAdministratorRepository administratorRepository, IMapper mapper, IConfiguration configuration)

        {
            this.administratorRepository = administratorRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminsAsync()
        {
            var admin = await administratorRepository.GetAllAsync();



            var adminDTO = mapper.Map<List<Models.DTO.Administrator>>(admin);


            return Ok(adminDTO);
        }
        [HttpGet, Authorize(Roles = "Admin")]
        [Route("{AdminId:int}")]
        [ActionName("GetAdminAsync")]
        public async Task<IActionResult> GetAdminAsync(int AdminId)
        {
            var admin = await administratorRepository.GetAsync(AdminId);

            if (admin == null)
            {

                return NotFound("Admin not found");
            }


            var adminDTO = mapper.Map<Models.DTO.Administrator>(admin);

            return Ok(adminDTO);
        }
        //, Authorize(Roles = "Admin")
        [HttpPost("Register")]
        public async Task<IActionResult> AddAdminAsync([FromBody] Models.DTO.AddAdministratorRequest addAdministratorRequest)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(addAdministratorRequest.Lozinka);

            var admin = new Db.TblAdministrator
            {
                Jmbg = addAdministratorRequest.Jmbg,
                Username = addAdministratorRequest.Username,
                Lozinka = passwordHash
            };
            admin = await administratorRepository.AddAsync(admin);

            var adminDTO = mapper.Map<Models.DTO.Administrator>(admin);


            return Ok(adminDTO);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{AdminId:int}")]
        public async Task<IActionResult> DeleteAdminAsync(int AdminId)
        {

            //get doc from database

            var admin = await administratorRepository.DeleteAsync(AdminId);

            if (admin == null)
            {
                return NotFound("Admin not found");
            }

            var adminDTO = mapper.Map<Models.DTO.Administrator>(admin);

            return Ok(adminDTO);

        }
        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{AdminId:int}")]

        public async Task<IActionResult> UpdateAdminAsync([FromRoute] int AdminId, [FromBody] Models.DTO.UpdateAdministratorRequest updateAdministratorRequest)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(updateAdministratorRequest.Lozinka);
            var admin = new Db.TblAdministrator()
            {
                Jmbg = updateAdministratorRequest.Jmbg,
                Username = updateAdministratorRequest.Username,
                Lozinka = passwordHash
            };

            admin = await administratorRepository.UpdateAsync(AdminId, admin);

            if (admin == null)
            {
                return NotFound("Admin not found");
            }
            var adminDTO = mapper.Map<Models.DTO.Administrator>(admin);



            return Ok(adminDTO);

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAdminAsync(Models.DTO.LoginAdminRequerst loginAdminRequerst)
        {
            var adminEntity = await administratorRepository.GetByUsernameAsync(loginAdminRequerst.Username);


            if (adminEntity == null)
            {
                return NotFound("User not found");
            }

            // Verify if the password is correct
            if (!BCrypt.Net.BCrypt.Verify(loginAdminRequerst.Lozinka, adminEntity.Lozinka))
            {
                return Unauthorized("Invalid password");
            }

            string token = CreateToken(adminEntity);
            return Ok(token);
        }
        private string CreateToken(TblAdministrator admin)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name,admin.Username),
                new Claim(ClaimTypes.Role, "Admin")

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

        [HttpGet]
        [Route("Username/{Username}")]
        [ActionName("GetByUernameAsync")]
        public async Task<IActionResult> GetByUernameAsync(string Username)
        {
            var admin = await administratorRepository.GetByUsernameAsync(Username);

            if (admin == null)
            {

                return NotFound("User not found");
            }


            var adminDTO = mapper.Map<Models.DTO.Administrator>(admin);

            return Ok(adminDTO);
        }
    }
}

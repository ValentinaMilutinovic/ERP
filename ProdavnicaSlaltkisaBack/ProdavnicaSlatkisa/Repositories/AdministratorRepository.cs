using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class AdministratorRepository :IAdministratorRepository
    {

        private readonly  CustomDbContext _context;

        public AdministratorRepository(CustomDbContext context)
        {
            this._context = context;
        }

     
        public async Task<IEnumerable<TblAdministrator>> GetAllAsync()
        {
            return await _context.TblAdministrators
                 .Include(x => x.Korisnik)


                 .ToListAsync();
        }
        public async Task<TblAdministrator> GetAsync(int AdminId)
        {

            return await _context.TblAdministrators
            .Include(x => x.Korisnik)
                 .FirstOrDefaultAsync(x => x.AdminId == AdminId);


        }
        public async Task<TblAdministrator> AddAsync(TblAdministrator tblAdministrator)
        {

            await _context.AddAsync(tblAdministrator);
            await _context.SaveChangesAsync();
            return tblAdministrator;

        }
        public async Task<TblAdministrator> DeleteAsync(int AdminId)
        {
            var admin = await _context.TblAdministrators.FirstOrDefaultAsync(x => x.AdminId == AdminId);

            if (admin == null)
            {
                return null;

            }

            _context.TblAdministrators.Remove(admin);
            await _context.SaveChangesAsync();

            return admin;
        }




        public async Task<TblAdministrator> UpdateAsync(int AdminId, TblAdministrator tblAdministrator)
        {
            var admin = await _context.TblAdministrators.FirstOrDefaultAsync(x => x.AdminId == AdminId);

            if (admin == null)
            {
                return null;
            }
            admin.Jmbg = tblAdministrator.Jmbg;
            admin.Username = tblAdministrator.Username;
            admin.Lozinka = tblAdministrator.Lozinka;
           

            await _context.SaveChangesAsync();

            return admin;

        }

        public async Task<TblAdministrator> GetByUsernameAsync(string username)
        {
            return await _context.TblAdministrators
                .Include(x => x.Korisnik)
                .FirstOrDefaultAsync(x => x.Username == username);
        }


    }
}

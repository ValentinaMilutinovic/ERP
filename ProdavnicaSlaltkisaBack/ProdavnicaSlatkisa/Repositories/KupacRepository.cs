using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class KupacRepository : IKupacRepository
    {

        private readonly CustomDbContext _context;

        public KupacRepository(CustomDbContext context)
        {
            this._context = context;
        }


        public async Task<IEnumerable<TblKupac>> GetAllAsync()
        {
            return await _context.TblKupacs.Include(x=>x.Korisnik).ToListAsync();
        }
        public async Task<TblKupac> GetAsync(int KupacId)
        {

            return await _context.TblKupacs
            .Include(x => x.Korisnik)
            
                 .FirstOrDefaultAsync(x => x.KupacId == KupacId);


        }
        public async Task<TblKupac> AddAsync(TblKupac tblKupac)
        {

            await _context.AddAsync(tblKupac);
            await _context.SaveChangesAsync();
            return tblKupac;

        }
        public async Task<TblKupac> DeleteAsync(int KupacId)
        {
            var buyer = await _context.TblKupacs.FirstOrDefaultAsync(x => x.KupacId == KupacId);

            if (buyer == null)
            {
                return null;

            }

            _context.TblKupacs.Remove(buyer);
            await _context.SaveChangesAsync();

            return buyer;
        }




        public async Task<TblKupac> UpdateAsync(int KupacId, TblKupac tblKupac)
        {
            var buyer = await _context.TblKupacs.FirstOrDefaultAsync(x => x.KupacId == KupacId);

            if (buyer == null)
            {
                return null;
            }
            buyer.UsernameRk = tblKupac.UsernameRk;
            buyer.LozinkaRk = tblKupac.LozinkaRk;
            buyer.BrojKupovina = tblKupac.BrojKupovina;
            buyer.Registrovan = tblKupac.Registrovan;


            await _context.SaveChangesAsync();

            return buyer;

        }
        public async Task<TblKupac> GetByUsernameAsync(string username)
        {
            return await _context.TblKupacs
                .Include(x => x.Korisnik)
                .FirstOrDefaultAsync(x => x.UsernameRk == username);
        }
    }
}

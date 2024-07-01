using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class KorpaRepository : IKorpaRepository
    {
        private readonly CustomDbContext _context;

        public KorpaRepository(CustomDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<TblKorpa>> GetAllAsync()
        {
            return await _context.TblKorpas
                 .Include(x => x.Korisnik)
                 .ToListAsync();
        }

        public async Task<TblKorpa> GetAsync(int KorpaId)
        {

            return await _context.TblKorpas
                 .Include(x => x.Korisnik)
                 .FirstOrDefaultAsync(x => x.KorpaId == KorpaId);


        }


        public async Task<TblKorpa> AddAsync(TblKorpa tblKorpa)
        {

            await _context.AddAsync(tblKorpa);
            await _context.SaveChangesAsync();
            return tblKorpa;

        }
        public async Task<TblKorpa> DeleteAsync(int KorpaId)
        {
            var cart = await _context.TblKorpas.FirstOrDefaultAsync(x => x.KorpaId == KorpaId);

            if (cart == null)
            {
                return null;

            }

            _context.TblKorpas.Remove(cart);
            await _context.SaveChangesAsync();

            return cart;
        }




        public async Task<TblKorpa> UpdateAsync(int KorpaId, TblKorpa tblKorpa)
        {
            var cart = await _context.TblKorpas.FirstOrDefaultAsync(x => x.KorpaId == KorpaId);

            if (cart == null)
            {
                return null;
            }
            cart.KorisnikId = tblKorpa.KorisnikId;
            cart.UkupanIznos = tblKorpa.UkupanIznos;
            cart.BrProizvoda = tblKorpa.BrProizvoda;
            

            await _context.SaveChangesAsync();

            return cart;

        }

        public async Task CalculateTotal(int KorpaId, int Iznos, bool positive, int BrojKom)
        {
            var cart= await _context.TblKorpas.FirstOrDefaultAsync(x => x.KorpaId == KorpaId);
            
            
            
            if(cart != null)
            {

                if (positive == true)
                {
                    cart.UkupanIznos += Iznos;
                    cart.BrProizvoda += BrojKom;
                }
                else
                {
                    cart.UkupanIznos = cart.UkupanIznos-Iznos;
                    cart.BrProizvoda = cart.BrProizvoda-BrojKom;
                }
            }
            await _context.SaveChangesAsync();


        }

    }
}

using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using ProdavnicaSlatkisa.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class KorisnikRepository:IKorisnikRepository
    {
        private readonly CustomDbContext _context;

        public KorisnikRepository(CustomDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<TblKorisnik>> GetAllAsync()
        {
            return await _context.TblKorisniks.ToListAsync();
        }
        public async Task<TblKorisnik> AddAsync(TblKorisnik tblKorisnik)
        {
            /*
            using (SqlCommand command = new SqlCommand("SELECT NEXT VALUE FOR IDModela_seq"))
            {
                //Execute the SqlCommand and get the next sequence value
                int nextSequenceValue = (int)command.ExecuteScalar();

                //Use the nextSequenceValue to create your object with a new ID
                tblModel.Idmodela = nextSequenceValue;
            }*/
            await _context.AddAsync(tblKorisnik);
            await _context.SaveChangesAsync();
            return tblKorisnik;
        }
        public async Task<TblKorisnik> GetKorisnikByDetails(AddKupacRequest addKupacRequest)
        {

            var korisnik = await _context.TblKorisniks
            .FirstOrDefaultAsync(x => x.Email == addKupacRequest.Kupac.Email);

            if (korisnik == null)
            {
                korisnik = new TblKorisnik
                {
                    Ime = addKupacRequest.Kupac.Ime,
                    Prezime = addKupacRequest.Kupac.Prezime,
                    Email = addKupacRequest.Kupac.Email,
                    Kontakt = addKupacRequest.Kupac.Kontakt,
                    Adresa = addKupacRequest.Kupac.Adresa,
                    Grad = addKupacRequest.Kupac.Grad
                };


                _context.TblKorisniks.Add(korisnik);
                await _context.SaveChangesAsync();

            }
            return korisnik;
            
        }



        
        
        
        public async Task<TblKorisnik> DeleteAsync(int KorisnikId)
        {
            var user = await _context.TblKorisniks.FirstOrDefaultAsync(x => x.KorisnikId == KorisnikId);

            if (user == null)
            {
                return null;

            }

            _context.TblKorisniks.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<TblKorisnik> GetAsync(int KorisnikId)
        {
            return await _context.TblKorisniks.FirstOrDefaultAsync(x => x.KorisnikId == KorisnikId);
        }

        public async Task<TblKorisnik> UpdateAsync(int KorisnikId, TblKorisnik tblKorisnik)
        {
            var user = await _context.TblKorisniks.FirstOrDefaultAsync(x => x.KorisnikId == KorisnikId);

            if (user == null)
            {
                return null;
            }
            user.Ime = tblKorisnik.Ime;
            user.Prezime = tblKorisnik.Prezime;
            user.Email = tblKorisnik.Email;
            user.Kontakt = tblKorisnik.Kontakt;
            user.Adresa = tblKorisnik.Adresa;
            user.Grad = tblKorisnik.Grad;


            await _context.SaveChangesAsync();

            return user;

        }
    }
}

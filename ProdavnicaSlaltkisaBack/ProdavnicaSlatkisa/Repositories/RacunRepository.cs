using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class RacunRepository : IRacunRepository
    {
        private readonly CustomDbContext _context;
        private readonly IKorpaRepository korpaRepository;

        public RacunRepository(CustomDbContext context,IKorpaRepository korpaRepository)
        {
            this._context = context;
            this.korpaRepository = korpaRepository;
            
        }
        public async Task<TblRacun> AddAsync(TblRacun tblRacun)
        {/*
            using (SqlCommand command = new SqlCommand("SELECT NEXT VALUE FOR IDModela_seq"))
            {
                //Execute the SqlCommand and get the next sequence value
                int nextSequenceValue = (int)command.ExecuteScalar();

                //Use the nextSequenceValue to create your object with a new ID
                tblModel.Idmodela = nextSequenceValue;
            }*/
            var korpa = await _context.TblKorpas.FirstOrDefaultAsync(x=>x.KorpaId==tblRacun.KorpaId);
            int iznos = (int)korpa.UkupanIznos;
            tblRacun.UkupanIznos = iznos;

            await _context.AddAsync(tblRacun);
            await _context.SaveChangesAsync();
            return tblRacun;
        }

        public async Task AddDiscount(int KorisnikId, int? RacunId, int KorpaId)
        {
            var racunEntity = await _context.TblRacuns.FirstOrDefaultAsync(x => x.RacunId == RacunId);
            var kupac = await _context.TblKupacs.FirstOrDefaultAsync(x => x.KupacId == KorisnikId);
            var cart= await _context.TblKorpas.FirstOrDefaultAsync(x => x.KorpaId == KorpaId);
            if (kupac!=null)
            {
                racunEntity.UkupanIznos = (int)cart.UkupanIznos;


                if(kupac.BrojKupovina==null)
                {
                    kupac.BrojKupovina = 1;
                    await _context.SaveChangesAsync();
                }else
                {

                    kupac.BrojKupovina += 1;

                    if(kupac.BrojKupovina>4 && kupac.BrojKupovina<10)
                    {
                       
                        racunEntity.Popust = true;
                        racunEntity.ProcenatPop = 5;
                        int iznosPop = (int)(racunEntity.UkupanIznos * racunEntity.ProcenatPop / 100);
                        racunEntity.IznosSaPopustom = racunEntity.UkupanIznos - iznosPop;
                    }
                    else if(kupac.BrojKupovina>9)
                    {

                        racunEntity.Popust = true;
                        racunEntity.ProcenatPop = 10;

                        int iznosPop = (int)(racunEntity.UkupanIznos * racunEntity.ProcenatPop / 100);
                        racunEntity.IznosSaPopustom = racunEntity.UkupanIznos - iznosPop;
                    }

                    

                    

                    await _context.SaveChangesAsync();

                }

            }


        }

        public async Task<TblRacun> DeleteAsync(int RacunId)
        {
            var racun = await _context.TblRacuns.FirstOrDefaultAsync(x => x.RacunId == RacunId);

            if (racun == null)
            {
                return null;

            }

            _context.TblRacuns.Remove(racun);
            await _context.SaveChangesAsync();

            return racun;
        }

        public async Task<IEnumerable<TblRacun>> GetAllAsync()
        {
            return await _context.TblRacuns.ToListAsync();
        }
        public async Task<TblRacun> GetAsync(int RacunId)
        {
            return await _context.TblRacuns.FirstOrDefaultAsync(x => x.RacunId == RacunId);
        }


        public async Task<TblRacun> GetRacunByKorpaIDAsync(int KorpaId)
        {



            return await _context.TblRacuns.FirstOrDefaultAsync(x => x.KorpaId == KorpaId);


        }

        public async Task<TblRacun> UpdateAsync(int RacunId, TblRacun tblRacun)
        {
            var racun = await _context.TblRacuns.FirstOrDefaultAsync(x => x.RacunId == RacunId);

            if (racun == null)
            {
                return null;
            }
            racun.UkupanIznos = tblRacun.UkupanIznos;
            racun.DatumKupovine = tblRacun.DatumKupovine;
            racun.VremeKupovine = tblRacun.VremeKupovine;
            racun.Popust = tblRacun.Popust;
            racun.ProcenatPop = tblRacun.ProcenatPop;
            racun.IznosSaPopustom = tblRacun.IznosSaPopustom;


            await _context.SaveChangesAsync();

            return racun;

        }
        public async Task<TblRacun> GetByPaymentIntentIdAsync(string paymentIntentId)
        {
            return await _context.TblRacuns.Include(x => x.Korpa).FirstOrDefaultAsync(x => x.PaymentIntentId == paymentIntentId);
        }



    }
}

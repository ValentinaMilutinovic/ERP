using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class TipProizvodumRepository : ITipProizvodumRepository
    {
        private readonly CustomDbContext _context;

        public TipProizvodumRepository(CustomDbContext context)
        {
            this._context = context;
        }

        public async Task<TblTipProizvodum> AddAsync(TblTipProizvodum tblTipProizvodum)
        {/*
            using (SqlCommand command = new SqlCommand("SELECT NEXT VALUE FOR IDModela_seq"))
            {
                //Execute the SqlCommand and get the next sequence value
                int nextSequenceValue = (int)command.ExecuteScalar();

                //Use the nextSequenceValue to create your object with a new ID
                tblModel.Idmodela = nextSequenceValue;
            }*/
            await _context.AddAsync(tblTipProizvodum);
            await _context.SaveChangesAsync();
            return tblTipProizvodum;
        }

        public async Task<TblTipProizvodum> DeleteAsync(int TipProizvodaId)
        {
            var tipP = await _context.TblTipProizvoda.FirstOrDefaultAsync(x => x.TipProizvodaId == TipProizvodaId);

            if (tipP == null)
            {
                return null;

            }

            _context.TblTipProizvoda.Remove(tipP);
            await _context.SaveChangesAsync();

            return tipP;
        }

        public async Task<IEnumerable<TblTipProizvodum>> GetAllAsync()
        {
            return await _context.TblTipProizvoda.ToListAsync();
        }

        public async Task<TblTipProizvodum> GetAsync(int TipProizvodaId)
        {
            return await _context.TblTipProizvoda.FirstOrDefaultAsync(x => x.TipProizvodaId == TipProizvodaId);
        }

        public async Task<TblTipProizvodum> UpdateAsync(int TipProizvodaId, TblTipProizvodum tblTipProizvodum)
        {
            var tipP = await _context.TblTipProizvoda.FirstOrDefaultAsync(x => x.TipProizvodaId == TipProizvodaId);

            if (tipP == null)
            {
                return null;
            }
            tipP.Sastav = tblTipProizvodum.Sastav;


            await _context.SaveChangesAsync();

            return tipP;

        }
    }
}



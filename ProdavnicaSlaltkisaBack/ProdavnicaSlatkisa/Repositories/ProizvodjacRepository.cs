using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class ProizvodjacRepository: IProizvodjacRepository
    {
        private readonly CustomDbContext _context;

        public ProizvodjacRepository(CustomDbContext context)
        {
            this._context = context;
        }


        public async Task<TblProizvodjac> AddAsync(TblProizvodjac tblProizvodjac)
        {/*
            using (SqlCommand command = new SqlCommand("SELECT NEXT VALUE FOR IDModela_seq"))
            {
                //Execute the SqlCommand and get the next sequence value
                int nextSequenceValue = (int)command.ExecuteScalar();

                //Use the nextSequenceValue to create your object with a new ID
                tblModel.Idmodela = nextSequenceValue;
            }*/
            await _context.AddAsync(tblProizvodjac);
            await _context.SaveChangesAsync();
            return tblProizvodjac;
        }

        public async Task<TblProizvodjac> DeleteAsync(int Idproizvodjaca)
        {
            var proizvodjac = await _context.TblProizvodjacs.FirstOrDefaultAsync(x => x.Idproizvodjaca == Idproizvodjaca);

            if (proizvodjac == null)
            {
                return null;

            }

            _context.TblProizvodjacs.Remove(proizvodjac);
            await _context.SaveChangesAsync();

            return proizvodjac;
        }

        public async Task<IEnumerable<TblProizvodjac>> GetAllAsync()
        {
            return await _context.TblProizvodjacs.ToListAsync();
        }

        public async Task<TblProizvodjac> GetAsync(int Idproizvodjaca)
        {
            return await _context.TblProizvodjacs.FirstOrDefaultAsync(x => x.Idproizvodjaca == Idproizvodjaca);
        }

        public async Task<TblProizvodjac> UpdateAsync(int Idproizvodjaca, TblProizvodjac tblProizvodjac)
        {
            var proizvodjac = await _context.TblProizvodjacs.FirstOrDefaultAsync(x => x.Idproizvodjaca == Idproizvodjaca);

            if (proizvodjac == null)
            {
                return null;
            }
            proizvodjac.NazivProizvodjaca = tblProizvodjac.NazivProizvodjaca;
            proizvodjac.ZemljaPorekla = tblProizvodjac.ZemljaPorekla;


            await _context.SaveChangesAsync();

            return proizvodjac;

        }

    }
}

using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using ProdavnicaSlatkisa.API.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class ProizvodRepository:IProizvodRepository
    {
        private readonly CustomDbContext _context;

        public ProizvodRepository(CustomDbContext context)
        {
            this._context = context;
        }
        public async Task<List<TblProizvod>> GetAllAsync()
        {
            return await _context.TblProzvodi
                 .Include("IdproizvodjacaNavigation")
                 .Include("TipProizvoda")
                 .ToListAsync();
        }
        public async Task<TblProizvod> GetAsync(int IdProizvoda)
        {

            return await _context.TblProzvodi
                 .Include(x => x.IdproizvodjacaNavigation)
                 .Include(x => x.TipProizvoda)
                 .FirstOrDefaultAsync(x => x.IdProizvoda == IdProizvoda);


        }

        public async Task<TblProizvod> AddAsync(TblProizvod tblProizvod)
        {
                await _context.TblProzvodi.AddAsync(tblProizvod);
                await _context.SaveChangesAsync();
                return tblProizvod;
        }
        public async Task<TblProizvod> DeleteAsync(int idProizvod)
        {
            var proizvod = await _context.TblProzvodi.FirstOrDefaultAsync(x => x.IdProizvoda == idProizvod);

            if (proizvod == null)
            {
                return null;

            }

            _context.TblProzvodi.Remove(proizvod);
            await _context.SaveChangesAsync();

            return proizvod;
        }

        public async Task<TblProizvod> UpdateAsync(int IdProizvod, TblProizvod tblProizvod)
        {
            var proizvod = await _context.TblProzvodi.FirstOrDefaultAsync(x => x.IdProizvoda == IdProizvod);

            if (proizvod == null)
            {
                return null;
            }
            proizvod.Cena = tblProizvod.Cena;
            proizvod.Idproizvodjaca = tblProizvod.Idproizvodjaca;
            proizvod.TipProizvodaId = tblProizvod.TipProizvodaId;
            proizvod.KolicinaNaStanju = tblProizvod.KolicinaNaStanju;
            proizvod.Naziv = tblProizvod.Naziv;

           await  _context.SaveChangesAsync();

            return proizvod;

        }

    }
}

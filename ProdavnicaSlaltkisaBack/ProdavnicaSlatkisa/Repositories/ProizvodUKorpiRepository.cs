using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class ProizvodUKorpiRepository : IProizvodUKorpiRepository
    {
        private readonly CustomDbContext context;
        private readonly IProizvodRepository proizvodRepository;

        public ProizvodUKorpiRepository(CustomDbContext context, IProizvodRepository proizvodRepository)
        {
            this.context = context;
            this.proizvodRepository = proizvodRepository;
        }
        public async Task<IEnumerable<TblProizvodUkorpi>> GetAllAsync()
        {
            return await context.TblProizvodUkorpis
                 .Include(x => x.ProizvodNavigation)
                 .Include(x => x.Korpa)


                 .ToListAsync();
        }
        public async Task<TblProizvodUkorpi> GetAsync(int ProizUkorpiId)
        {

            return await context.TblProizvodUkorpis
                 .Include(x => x.ProizvodNavigation)
                 .Include(x => x.Korpa)
                 .FirstOrDefaultAsync(x => x.ProizUkorpiId == ProizUkorpiId);


        }
        public async Task<TblProizvodUkorpi> AddAsync(TblProizvodUkorpi tblProizvodUkorpi)
        {
            var proizvod = await proizvodRepository.GetAsync(tblProizvodUkorpi.ProizvodId);
            var cena = proizvod.Cena;

            if (tblProizvodUkorpi.BrojKomada >= proizvod.KolicinaNaStanju)
            {
                throw new Exception("Product is out of stock");
            }
            else
            {
                var iznosVr = tblProizvodUkorpi.BrojKomada * cena;
                tblProizvodUkorpi.Iznos = (int)iznosVr; // Update the value of Iznos
                await context.TblProizvodUkorpis.AddAsync(tblProizvodUkorpi);
                await context.SaveChangesAsync();
                return tblProizvodUkorpi;
            }
        }

        public async Task<TblProizvodUkorpi> DeleteAsync(int ProizUkorpiId)
        {
            var cartItem = await context.TblProizvodUkorpis.FirstOrDefaultAsync(x => x.ProizUkorpiId == ProizUkorpiId);

            if (cartItem == null)
            {
                return null;

            }

            context.TblProizvodUkorpis.Remove(cartItem);
             await context.SaveChangesAsync();

            return cartItem;
        }




        public async Task<TblProizvodUkorpi> UpdateAsync(int ProizUkorpiId, TblProizvodUkorpi tblProizvodUkorpi)
        {
            var cartItem = await context.TblProizvodUkorpis.FirstOrDefaultAsync(x => x.ProizUkorpiId == ProizUkorpiId);
            var proizvod = await proizvodRepository.GetAsync(cartItem.ProizvodId);
            var cena = (int)proizvod.Cena;
            if (tblProizvodUkorpi.BrojKomada >= proizvod.KolicinaNaStanju)
            {
                throw new Exception("Product is out of stock");

            }
            else
            {
                if (cartItem == null)
                {
                    return null;
                }


                cartItem.BrojKomada = tblProizvodUkorpi.BrojKomada;
                cartItem.ProizvodId = cartItem.ProizvodId;
                cartItem.KorpaId = cartItem.KorpaId;
                cartItem.Iznos = tblProizvodUkorpi.BrojKomada * cena;


                await context.SaveChangesAsync();
            }
            return cartItem;

        }


        public async Task<IEnumerable<TblProizvodUkorpi>> GetByKorpaIdAsync(int korpaId)
        {
            return await context.TblProizvodUkorpis
                .Include(x => x.ProizvodNavigation)
                    .ThenInclude(p => p.IdproizvodjacaNavigation)
                .Include(x => x.ProizvodNavigation)
                    .ThenInclude(p => p.TipProizvoda)
                .Where(x => x.KorpaId == korpaId)
                .ToListAsync();
        }
    }
}

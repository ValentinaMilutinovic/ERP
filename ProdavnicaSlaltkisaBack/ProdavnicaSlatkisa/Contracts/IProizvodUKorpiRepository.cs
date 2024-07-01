using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IProizvodUKorpiRepository
    {
        Task<IEnumerable<TblProizvodUkorpi>> GetAllAsync();

        Task<TblProizvodUkorpi> GetAsync(int ProizUkorpiId);


        Task<TblProizvodUkorpi> AddAsync(TblProizvodUkorpi tblProizvodUkorpi);

        Task<TblProizvodUkorpi> DeleteAsync(int ProizUkorpiId);

        Task<TblProizvodUkorpi> UpdateAsync(int ProizUkorpiId, TblProizvodUkorpi tblProizvodUkorpi);

        Task<IEnumerable<TblProizvodUkorpi>> GetByKorpaIdAsync(int korpaId);
    }
}

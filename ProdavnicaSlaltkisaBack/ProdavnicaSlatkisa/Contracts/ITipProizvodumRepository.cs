using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface ITipProizvodumRepository
    {
        Task<IEnumerable<TblTipProizvodum>> GetAllAsync();

        Task<TblTipProizvodum> GetAsync(int TipProizvodaId);


        Task<TblTipProizvodum> AddAsync(TblTipProizvodum tblTipProizvodum);

        Task<TblTipProizvodum> DeleteAsync(int TipProizvodaId);

        Task<TblTipProizvodum> UpdateAsync(int TipProizvodaId, TblTipProizvodum tblTipProizvodum);
    }
}

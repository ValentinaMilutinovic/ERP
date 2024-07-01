using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IProizvodRepository
    {

        Task<List<TblProizvod>> GetAllAsync();

        Task<TblProizvod> GetAsync(int idProizvod);


        Task<TblProizvod> AddAsync(TblProizvod tblProizvod);

        Task<TblProizvod> DeleteAsync(int idProizvod);

        Task<TblProizvod> UpdateAsync(int idProizvod, TblProizvod tblProizvod);
    }
}

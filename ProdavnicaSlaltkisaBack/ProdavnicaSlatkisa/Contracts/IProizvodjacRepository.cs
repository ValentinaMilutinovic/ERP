using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IProizvodjacRepository
    {
        Task<IEnumerable<TblProizvodjac>> GetAllAsync();

        Task<TblProizvodjac> GetAsync(int Idproizvodjaca);


        Task<TblProizvodjac> AddAsync(TblProizvodjac tblProizvodjac);

        Task<TblProizvodjac> DeleteAsync(int Idproizvodjaca);

        Task<TblProizvodjac> UpdateAsync(int Idproizvodjaca, TblProizvodjac tblProizvodjac);
    }
}

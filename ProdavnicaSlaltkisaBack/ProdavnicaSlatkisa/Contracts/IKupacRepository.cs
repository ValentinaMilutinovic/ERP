using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IKupacRepository
    {
        Task<IEnumerable<TblKupac>> GetAllAsync();

        Task<TblKupac> GetAsync(int KupacId);


        Task<TblKupac> AddAsync(TblKupac tblKupac);

        Task<TblKupac> DeleteAsync(int KupacId);

        Task<TblKupac> UpdateAsync(int KupacId, TblKupac tblKupac);
        Task<TblKupac> GetByUsernameAsync(string username);
    }
}

using ProdavnicaSlatkisa.API.Db;
using System.Reflection.Metadata;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IAdministratorRepository
    {
        Task<IEnumerable<TblAdministrator>> GetAllAsync();

        Task<TblAdministrator> GetAsync(int AdminId);


        Task<TblAdministrator> AddAsync(TblAdministrator tblAdministrator);

        Task<TblAdministrator> DeleteAsync(int AdminId);

        Task<TblAdministrator> UpdateAsync(int AdminId, TblAdministrator tblAdministrator);

        Task<TblAdministrator> GetByUsernameAsync(string username);


    }
}

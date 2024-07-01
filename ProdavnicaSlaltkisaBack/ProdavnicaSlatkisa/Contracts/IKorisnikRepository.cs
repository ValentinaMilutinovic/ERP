using ProdavnicaSlatkisa.API.Db;
using ProdavnicaSlatkisa.API.Models.DTO;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IKorisnikRepository
    {
        Task<IEnumerable<TblKorisnik>> GetAllAsync();

        Task<TblKorisnik> GetAsync(int KorisnikId);

        Task<TblKorisnik> GetKorisnikByDetails(AddKupacRequest addKupacRequest);
        Task<TblKorisnik> AddAsync(TblKorisnik tblKorisnik);

        Task<TblKorisnik> DeleteAsync(int KorisnikId);

        Task<TblKorisnik> UpdateAsync(int KorisnikId, TblKorisnik tblKorisnik);
    }
}

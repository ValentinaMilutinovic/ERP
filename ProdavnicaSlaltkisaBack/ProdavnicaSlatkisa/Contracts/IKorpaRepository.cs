using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IKorpaRepository
    {
        Task<IEnumerable<TblKorpa>> GetAllAsync();

        Task<TblKorpa> GetAsync(int KorpaId);


        Task<TblKorpa> AddAsync(TblKorpa tblKorpa);

        Task<TblKorpa> DeleteAsync(int KorpaId);

        Task<TblKorpa> UpdateAsync(int KorpaId, TblKorpa tblKorpa);

        Task CalculateTotal(int KorpaId, int Iznos, bool positive, int BrojKom);
    }
}

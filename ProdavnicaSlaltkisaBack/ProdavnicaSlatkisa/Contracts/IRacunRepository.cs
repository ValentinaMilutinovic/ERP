using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IRacunRepository
    {
        Task<IEnumerable<TblRacun>> GetAllAsync();

        Task<TblRacun> GetAsync(int RacunId);


        Task<TblRacun> AddAsync(TblRacun tblRacun);

        Task<TblRacun> GetRacunByKorpaIDAsync(int KorpaId);

        Task<TblRacun> DeleteAsync(int RacunId);

        Task<TblRacun> UpdateAsync(int RacunId, TblRacun tblRacun);
        Task AddDiscount(int KorisnikId, int? RacunId, int KorpaId);
        Task<TblRacun> GetByPaymentIntentIdAsync(string paymentIntentId);
    }
}

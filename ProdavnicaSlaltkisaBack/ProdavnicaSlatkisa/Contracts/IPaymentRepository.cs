using ProdavnicaSlatkisa.API.Db;

namespace ProdavnicaSlatkisa.API.Contracts
{
    public interface IPaymentRepository
    {
        Task<TblRacun> CreateUpdatePaymentIntent(int racunId);
        Task<TblRacun> UpdateRacunPaymentSucceeded(string paymentIntentId);
        Task<TblRacun> UpdateRacunPaymentFailed(string paymentIntentId);
    }
}
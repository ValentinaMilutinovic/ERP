using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using Stripe;

namespace ProdavnicaSlatkisa.API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IRacunRepository racunRepository;
        private readonly IConfiguration configuration;
        private readonly CustomDbContext _context;

        public PaymentRepository(IRacunRepository racunRepository, IConfiguration configuration, CustomDbContext context)
        {
            this.racunRepository = racunRepository;
            this.configuration = configuration;
            this._context = context;
        }

        public async Task<TblRacun> CreateUpdatePaymentIntent(int racunId)
        {
            StripeConfiguration.ApiKey = configuration.GetSection("StripeSettings:SecretKey").Value;
            var racun = await racunRepository.GetAsync(racunId);

            var sumPrice = racun.UkupanIznos;
            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(racun.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)sumPrice,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                racun.PaymentIntentId = intent.Id;
                racun.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)sumPrice
                };
                intent = await service.UpdateAsync(racun.PaymentIntentId, options);
                racun.ClientSecret = intent.ClientSecret;
            }

            await racunRepository.UpdateAsync(racunId, racun);
            return racun;
        }


        public async Task<TblRacun> UpdateRacunPaymentFailed(string paymentIntentId)
        {
            var racun = await racunRepository.GetByPaymentIntentIdAsync(paymentIntentId);
            racun.Status = "Failed";
            await racunRepository.UpdateAsync(racun.RacunId, racun);
            await _context.SaveChangesAsync();
            return racun;
        }

        public async Task<TblRacun> UpdateRacunPaymentSucceeded(string paymentIntentId)
        {
            var racun = await racunRepository.GetByPaymentIntentIdAsync(paymentIntentId);
            racun.Status = "Succeeded";
            await racunRepository.UpdateAsync(racun.RacunId, racun);
            await _context.SaveChangesAsync();
            return racun;
        }

    }
}
using ProdavnicaSlatkisa.API.Contracts;
using ProdavnicaSlatkisa.API.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ProdavnicaSlatkisa.API.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly ILogger<PaymentController> logger;
        private const string WhSecret = "whsec_4a389544466ff9c50c9995e8ddb01c3d9fb8ec6828601f640aa0190865cf0458";

        public PaymentController(IPaymentRepository paymentRepository, ILogger<PaymentController> logger)
        {
            this.paymentRepository = paymentRepository;
            this.logger = logger;
        }

        //[Authorize]
        [HttpPost("RacunId/{racunId}")]
        public async Task<ActionResult<TblRacun>> CreateUpdatePaymentIntent(int racunId)
        {
            return await paymentRepository.CreateUpdatePaymentIntent(racunId);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret, throwOnApiVersionMismatch: false);

            PaymentIntent intent;
            TblRacun racun;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment succeeded ", intent.Id);
                    //Update racun with new status
                    racun = await paymentRepository.UpdateRacunPaymentSucceeded(intent.Id);
                    logger.LogInformation("Racun status updated to succeeded ", racun.RacunId);
                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment failed ", intent.Id);
                    //Update racun with new status
                    racun = await paymentRepository.UpdateRacunPaymentFailed(intent.Id);
                    logger.LogInformation("Racun status updated to failed ", racun.RacunId);
                    break;
            }
            return new EmptyResult();
        }
    }
}
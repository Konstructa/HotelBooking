using Application.Booking.Dtos;
using Payments.Application.MercadoPago;
using Payments.Application;
using Application.Responses;

namespace Payments.UnitTests
{
    public class MercadoPagoTests
    {
        [Test]
        public void ShouldReturn_MercadoPagoAdapter_Provider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

            Assert.That(typeof(MercadoPagoAdapter), Is.EqualTo(provider.GetType()));
        }

        [Test]
        public async Task Should_FailWhenPaymentIntentionStringIsInvalid()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

            var res = await provider.CapturePayment("");

            Assert.Multiple(() =>
            {
                Assert.That(res.Success, Is.False);
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION));
                Assert.That(res.Message, Is.EqualTo("The selected payment intention is invalid"));
            });
        }
    }
}
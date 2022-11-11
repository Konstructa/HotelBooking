
using Application.Booking.Dtos;
using Application.Responses;
using Payments.Application;
using Payments.Application.MercadoPago;

namespace Payments.UnitTests
{
    public class PaymentProcessorFactoryTests
    {
        [Test]
        public void ShouldReturn_NotImplementedPaymentProvider_WhenAskingForStripeProvider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.Stripe);

            Assert.That(typeof(NotImplementedPaymentProvider), Is.EqualTo(provider.GetType()));
        }

        [Test]
        public void ShouldReturn_MercadoPagoAdapter_Provider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

            Assert.That(typeof(MercadoPagoAdapter), Is.EqualTo(provider.GetType()));
        }

        [Test]
        public async Task ShouldReturnFalse_WhenCapturingPaymentFor_NotImplementedPaymentProvider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.Stripe);

            var res = await provider.CapturePayment("https://myprovider.com/asdf");

            Assert.Multiple(() =>
            {
                Assert.That(res.Success, Is.False);
                Assert.That(res.ErrorCode, Is.EqualTo((ErrorCodes.PAYMENT_PROVIDER_NOT_IMPLEMENTED)));
                Assert.That(res.Message, Is.EqualTo("The selected payment provider is not available at the moment"));
            });
        }
    }
}

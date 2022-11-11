
using Application.Booking.Dtos;
using Application.Payment.Ports;
using Payments.Application.MercadoPago;

namespace Payments.Application
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders selectedPaymentProvider)
        {
            return selectedPaymentProvider switch
            {
                SupportedPaymentProviders.MercadoPago => new MercadoPagoAdapter(),
                _ => new NotImplementedPaymentProvider(),
            };
        }
    }
}

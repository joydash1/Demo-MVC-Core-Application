using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ERP.Utility.Helpers
{
    public static class WhatsAppService
    {
        private static readonly string accountSid = "US8736b7c430801e18006d74f97ad778ec";
        private static readonly string authToken = "5FLWACLUPHWK5KRM4XBCS34A";
        private static readonly string fromWhatsAppNumber = "whatsapp:+14155238886";

        public static void SendWhatsAppMessage(string clientId, string clientPhoneNumber)
        {
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber($"whatsapp:{clientPhoneNumber}");

            var message = MessageResource.Create(
                to: to,
                from: new PhoneNumber(fromWhatsAppNumber),
                body: $"Hello! This is a message for client ID: {clientId}"
            );
        }
    }
}
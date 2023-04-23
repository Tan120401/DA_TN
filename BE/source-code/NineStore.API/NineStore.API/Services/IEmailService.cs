using NineStore.Common.Entities.DTO;

namespace NineStore.API.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}

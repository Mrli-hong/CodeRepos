

using UserMgrDomain.ValueObjects;

namespace Users.WebAPI.Controllers
{
    public record CheckLoginByPhoneAndCodeRequest(PhoneNumber PhoneNumber, string Code);
}

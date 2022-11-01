using Microsoft.AspNetCore.Identity;

namespace Identity
{
    public class MyRole:IdentityRole<long>
    {
        public string? WeixinNumber { get; set; }
    }
}

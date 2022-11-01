using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JWTandVersion
{
    public class JwtSetting
    {
        public string Key { get; set; }
        public int ExpireSeconds { get; set; }
    }
}

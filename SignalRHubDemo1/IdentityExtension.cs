using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace SignalRHubDemo1
{
    public static  class IdentityExtension
    {
        public static async Task CheckAsync(this Task<IdentityResult> IdentityResult)
        {
            var result =await IdentityResult;
            if (!result.Succeeded)
                throw new Exception(JsonSerializer.Serialize(result.Errors));
        }
    }
}

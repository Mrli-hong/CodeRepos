using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace JWTandVersion
{
    public static class MyIdentityExtension
    {
        /// <summary>
        /// 自定义扩展方法检测IdentityResult是否Succeed
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task CheckAsync(this Task<IdentityResult> task)
        {
            var result = await task;
            if (!result.Succeeded)
                throw new Exception(JsonSerializer.Serialize(result.Errors));
        }
    }
}

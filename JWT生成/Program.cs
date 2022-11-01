using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

List<Claim> claims = new List<Claim>();
claims.Add(new Claim("UserName", "LHY"));
claims.Add(new Claim("Password", "123.com"));
claims.Add(new Claim(ClaimTypes.GroupSid, "421"));
claims.Add(new Claim(ClaimTypes.Role, "admin"));
claims.Add(new Claim(ClaimTypes.Role, "user"));

string key = "1233124asdfasdfsq34er)**@&#*asodfhbnoi";
DateTime expire = DateTime.Now.AddMinutes(20);

byte[] secBytes = Encoding.UTF8.GetBytes(key);
var serKey = new SymmetricSecurityKey(secBytes);
var credentials = new SigningCredentials(serKey, SecurityAlgorithms.HmacSha256Signature);
var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: credentials);
string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
Console.WriteLine(jwt);
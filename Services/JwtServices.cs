using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce;

public class JwtServices : Ijwt
{
    private readonly IConfiguration _iconfiguration;

    public JwtServices(IConfiguration configuration)
    {
        _iconfiguration=configuration;
        
    }
    public string GenerateToken(User user)
    {
        var secret_key= _iconfiguration.GetSection("JwtOptions:SecretKey").Value;
        var issuer =_iconfiguration.GetSection("JwtOptions:Issuer").Value;
        var audience=_iconfiguration.GetSection("JwtOptions:Audience").Value;

        var mykey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));

        var cred= new SigningCredentials(mykey, SecurityAlgorithms.HmacSha256);

        //payload
        List<Claim> claims= new List<Claim>();
        claims.Add(new Claim("Roles", user.Role));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Fullname));

        //token
        var tokendescriptor=new SecurityTokenDescriptor(){
            Issuer=issuer,
            Audience=audience,
            Expires= DateTime.UtcNow.AddHours(4),
            Subject= new ClaimsIdentity(claims),
            SigningCredentials=cred
        };

        var token= new JwtSecurityTokenHandler().CreateToken(tokendescriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

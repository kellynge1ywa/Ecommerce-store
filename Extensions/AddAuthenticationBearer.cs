using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce;

public static class AddAuthenticationBearer
{
    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder){
        builder.Services.AddAuthentication("Bearer").AddJwtBearer(options=>{
    options.TokenValidationParameters= new (){
        //Things to be validated
        
        ValidateAudience=true,
        ValidateIssuer=true,
        ValidateIssuerSigningKey=true,

        ValidAudience=builder.Configuration.GetSection("JwtOptions:Audience").Value,
        ValidIssuer= builder.Configuration.GetSection("JwtOptions:Issuer").Value,
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:SecretKey").Value))
    };

});
 return builder;


    }

}

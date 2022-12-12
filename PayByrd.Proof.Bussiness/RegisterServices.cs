using Microsoft.Extensions.DependencyInjection;
using PayByrd.Proof.Bussiness.Interfaces;
using PayByrd.Proof.Bussiness.Service;

namespace PayByrd.Proof.Bussiness;

public static class RegisterServices
{
    public static void RegisterServiceIoC(this IServiceCollection services)
    {

        services.AddScoped<IDiffService, DiffService>();
    }
}
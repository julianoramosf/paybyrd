using Microsoft.Extensions.DependencyInjection;
using Paybyrd.Proof.Bussiness.Interfaces;
using Paybyrd.Proof.Bussiness.Service;

namespace Paybyrd.Proof.Bussiness;

public static class RegisterServices
{
    public static void RegisterServiceIoC(this IServiceCollection services)
    {

        services.AddScoped<IDiffService, DiffService>();
    }
}
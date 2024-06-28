using Microsoft.EntityFrameworkCore;
using VistoriaAPI.Data;

namespace VistoriaAPI.Veiculos
{
    public static class VeiculoRotas
    {
        public static void AddRotasVeiculos(this WebApplication app)
        {
            var rotasVeiculos = app.MapGroup("veiculo");
            rotasVeiculos.MapPost("", async (AddVeiculoRequest request, AppDbContext context) =>
            {
                var getVeiculo = await context.Veiculos.SingleOrDefaultAsync(a => Equals(a.Chassi, request.Chassi));
                var veiculo = request.ConvertToVeiculo();
                if (getVeiculo is null)
                    await context.Veiculos.AddAsync(veiculo);
                else
                    request.ConvertToVeiculo(getVeiculo);

                await context.SaveChangesAsync();
                return Results.Ok(getVeiculo is null ? veiculo : getVeiculo);
            });

            rotasVeiculos.MapGet("byData", (DateTime corte, AppDbContext context) =>
            {
                var veiculos = context.Veiculos.Where(a => a.DataIntegracao >= corte);
                return Results.Ok(veiculos);
            });
        }
    }
}

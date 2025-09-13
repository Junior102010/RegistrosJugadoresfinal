using Microsoft.EntityFrameworkCore;
using RegistrosJugadores.DAL;
using RegistrosJugadores.Models;
using System.Linq.Expressions;

namespace RegistrosJugadores.Services
{
    public class PartidasServices(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Partidas Partida)
        {
            if (!await Existe(Partida.PartidaId))
            {
                return await Insertar(Partida);
            }
            else
            {
                return await Modificar(Partida);
            }
        }

        public async Task<bool> Existe(int id)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Partidas.AnyAsync(j => j.PartidaId == id);
        }

        public async Task<bool> Insertar(Partidas Partida)
        {
            try
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                contexto.Partidas.Add(Partida);
                var cambios = await contexto.SaveChangesAsync();
                Console.WriteLine($">>> Guardado exitoso. Cambios: {cambios}");
                return cambios > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(">>> ERROR al guardar partida <<<");
                Console.WriteLine(ex.ToString());   // muestra stack completo
                Console.WriteLine(ex.InnerException?.Message); // muestra el error SQL real
                throw;
            }
        }

        public async Task<bool> Modificar(Partidas Partida)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(Partida);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Partidas?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.PartidaId == id);
        }


        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            return await contexto.Partidas.Where(j => j.PartidaId == id).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Partidas>> Listar(Expression<Func<Partidas, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas.Where(criterio).AsNoTracking().ToListAsync();

        }

    }
}

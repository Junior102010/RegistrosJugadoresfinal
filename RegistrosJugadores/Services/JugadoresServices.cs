using RegistrosJugadores.DAL;
using Microsoft.EntityFrameworkCore;
using RegistrosJugadores.Models;
using System.Linq.Expressions;

namespace RegistrosJugadores.Services
{
    public class JugadoresServices(IDbContextFactory<Contexto> DbFactory)
    {

        public async Task<bool> Guardar(Jugadores jugador)
        {
            if (!await Existe(jugador.JugadorId))
            {
                return await Insertar(jugador);


            }

            else
            {
                return await Modificar(jugador);
            }
        }

        public async Task<bool> Existe(int id)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Jugadores.AnyAsync(j => j.JugadorId == id);
        }

        public async Task<bool> Insertar(Jugadores jugador)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Jugadores.Add(jugador);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Jugadores jugador)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(jugador);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Jugadores?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.JugadorId == id);
        }


        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            return await contexto.Jugadores.Where(j => j.JugadorId == id).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Jugadores>> Listar(Expression<Func<Jugadores, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores.Where(criterio).AsNoTracking().ToListAsync();

        }
    }
}

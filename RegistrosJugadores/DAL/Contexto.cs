using RegistrosJugadores.Models;
using Microsoft.EntityFrameworkCore;

namespace RegistrosJugadores.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        public DbSet<Jugadores> Jugadores { get; set; }
    }
    
}

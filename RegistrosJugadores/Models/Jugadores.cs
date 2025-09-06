using System.ComponentModel.DataAnnotations;



namespace RegistrosJugadores.Models
{
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public int Partidas { get; set; }
    }
}

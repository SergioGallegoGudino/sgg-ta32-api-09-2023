using System.ComponentModel.DataAnnotations;

namespace TA32_1_sgallego.Models
{
    public class Investigador
    {

        public String Dni { get; set; }

        public String NomApels { get; set; }

        public int FacultadCodigo { get; set; }

        public Facultad Facultad { get; set; }

        public ICollection<Reserva> Reservas{ get; set; } = new List<Reserva>();
    }

    public class Equipo
    {

        public String NumSerie { get; set; }

        public String Nombre { get; set; }

        public int FacultadCodigo { get; set; }

        public Facultad Facultad { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

    public class Reserva
    {

        public String InvestigadorDni{ get; set; }

        public String EquipoNum { get; set; }


        public Investigador Investigador { get; set; }
    
        public Equipo Equipo{ get; set; }

        public DateTime Comienzo { get; set; }

        public DateTime Fin { get; set; }


    }

    public class Facultad
    {

        public int Codigo{ get; set; }

        public String Nombre { get; set; }

        public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
        public ICollection<Investigador> Investigadores{ get; set; } = new List<Investigador>();


    }

    public class InvestigadorDTO
    {

        public String Dni { get; set; }

        public String NomApels { get; set; }

        public int FacultadCodigo { get; set; }


    }

    public class EquipoDTO
    {

        public String NumSerie { get; set; }

        public String Nombre { get; set; }

        public int FacultadCodigo { get; set; }


    }

    public class ReservaDTO
    {

        public String InvestigadorDni { get; set; }

        public String EquipoNum { get; set; }

        public DateTime Comienzo { get; set; }
        
        public DateTime Fin { get; set; }
    }

    public class FacultadDTO
    {

        public int Codigo { get; set; }

        public String Nombre { get; set; }



    }
}

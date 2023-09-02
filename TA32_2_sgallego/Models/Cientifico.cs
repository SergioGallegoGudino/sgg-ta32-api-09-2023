using System.ComponentModel.DataAnnotations;

namespace TA32_1_sgallego.Models
{
    public class Proyecto
    {

        public int Id { get; set; }

        public String Nombre { get; set; }

        public int Horas { get; set; }


        public ICollection<Asignado> Asignados { get; set; } = new List<Asignado>();
    }

    public class Asignado
    {

        public int ProyectoId{ get; set; }

        public String CientificoDni { get; set; }

        public Cientifico Cientifico{ get; set; }
    
        public Proyecto Proyecto{ get; set; }

    }

    public class Cientifico
    {

        public String Dni { get; set; }

        public String NomApels { get; set; }

        public ICollection<Asignado> Asignados { get; set; } = new List<Asignado>();

    }

    public class ProyectoDTO
    {

        public int Id { get; set; }

        public String Nombre { get; set; }

        public int Horas { get; set; }


    }

    public class AsignadoDTO
    {

        public int ProyectoId { get; set; }

        public String CientificoDni { get; set; }

        public Cientifico Cientifico { get; set; }

        public Proyecto Proyecto { get; set; }

    }

    public class CientificoDTO
    {

        public String Dni { get; set; }

        public String NomApels { get; set; }


    }
}

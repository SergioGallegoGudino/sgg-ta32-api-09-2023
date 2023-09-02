using System.ComponentModel.DataAnnotations;

namespace TA32_1_sgallego.Models
{
    public class Pieza
    {

        public int Codigo { get; set; }

        public String Nombre { get; set; }

        public ICollection<Suministra> Suministra { get; set; } = new List<Suministra>();
    }

    public class Suministra
    {

        public int PiezaCodigo { get; set; }

        public String ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }
    
        public Pieza Pieza { get; set; }

        public int Precio { get; set; }


    }

    public class Proveedor
    {

        public String Id { get; set; }

        public String Nombre { get; set; }

        public ICollection<Suministra> Suministra { get; set; } = new List<Suministra>();

    }

    public class PiezaDTO
    {

        public int Codigo { get; set; }

        public String Nombre { get; set; }

    }

    public class SuministraDTO
    {

        public int PiezaCodigo { get; set; }

        public String ProveedorId { get; set; }

        public int Precio { get; set; }


    }

    public class ProveedorDTO
    {

        public String Id { get; set; }

        public String Nombre { get; set; }


    }
}

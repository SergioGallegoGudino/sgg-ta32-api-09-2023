using System.ComponentModel.DataAnnotations;

namespace TA32_1_sgallego.Models
{
    public class Producto
    {

        public int Codigo { get; set; }

        public String Nombre { get; set; }

        public int Precio { get; set; }


        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }

    public class Venta
    {

        public int CajeroCodigo{ get; set; }

        public int ProductoCodigo { get; set; }

        public int MaquinaCodigo { get; set; }

        public Producto Producto{ get; set; }
    
        public Maquina Maquina{ get; set; }

        public Cajero Cajero{ get; set; }


    }

    public class Maquina
    {

        public int Codigo{ get; set; }

        public int Piso { get; set; }

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();

    }

    public class Cajero
    {

        public int Codigo { get; set; }

        public String NomApels{ get; set; }

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();

    }

    public class ProductoDTO
    {

        public int Codigo { get; set; }

        public String Nombre { get; set; }

        public int Precio { get; set; }


    }

    public class VentaDTO
    {

        public int CajeroCodigo { get; set; }

        public int ProductoCodigo { get; set; }

        public int MaquinaCodigo { get; set; }

    }

    public class MaquinaDTO
    {

        public int Codigo { get; set; }

        public int Piso { get; set; }


    }

    public class CajeroDTO
    {

        public int Codigo { get; set; }

        public String NomApels { get; set; }


    }
}

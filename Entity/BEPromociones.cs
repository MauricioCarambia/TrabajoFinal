using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEPromociones
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoPromocion Tipo { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal MontoMinimo { get; set; }
        public bool Activa { get; set; }
        public List<string> MetodosPago { get; set; }

        public enum TipoPromocion
        {
            Porcentaje,
            MontoFijo
        }
    }
}

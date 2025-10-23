using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEPlato
    {
        public int Id { get; set; }                  
        public string Nombre { get; set; }
       
        public decimal PorcentajeGanancia { get; set; }
        public decimal PrecioCosto { get;  set; }
        public decimal PrecioVenta { get;  set; }

        public List<BEPlatoInsumo> ListaInsumos { get; set; } = new List<BEPlatoInsumo>();
        
        public bool Activo { get; set; } = true;
        public CategoriasPlato Categoria { get; set; }

        public enum CategoriasPlato
        {
            Entrada,
            Pastas,
            Pizzas,
            Carnes,
            Postre,
            Bebida,
            Acompanamiento
        }
        public decimal CalcularCosto()
        {
            PrecioCosto = ListaInsumos.Sum(i => i.CostoProporcional);
            return PrecioCosto;
        }


        public decimal CalcularVenta(decimal porcentajeGanancia)
        {
            PrecioVenta = Math.Round(PrecioCosto * (1 + porcentajeGanancia / 100m), 2);
            return PrecioVenta;
        }
    }
}

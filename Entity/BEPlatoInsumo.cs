using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEPlatoInsumo
    {
        public int Id { get; set; }
        public BEInsumo Insumo { get; set; }
        public BEPlato Plato { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }

        public BEPlatoInsumo() { }

        public BEPlatoInsumo(BEInsumo insumo, decimal cantidad)
        {
            Insumo = insumo;
            Cantidad = cantidad;
            CostoUnitario = insumo?.Precio ?? 0m;
        }

        // Costo proporcional del insumo según la cantidad usada
        public decimal CostoProporcional => Math.Round(Cantidad * CostoUnitario, 2);

    }
}


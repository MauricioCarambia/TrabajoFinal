using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPlatoInsumo
    {
        private MPPPlatoInsumo oMPPPlatoInsumo = new MPPPlatoInsumo();
        private BLLInsumo bllInsumo = new BLLInsumo();

        public void Guardar(BEPlatoInsumo oBEPlatoInsumo)
        {
            if (oBEPlatoInsumo == null)
                throw new ArgumentNullException(nameof(oBEPlatoInsumo), "No se proporcionó un insumo válido.");

            if (oBEPlatoInsumo.Plato == null || oBEPlatoInsumo.Plato.Id == 0)
                throw new Exception("El insumo debe estar asociado a un plato válido con Id.");

            oMPPPlatoInsumo.Guardar(oBEPlatoInsumo);
        }

        /// <summary>
        /// Guarda todos los insumos de un plato (lista completa)
        /// </summary>
        /// <param name="oBEPlato"></param>
        public void GuardarLista(BEPlato oBEPlato)
        {
            if (oBEPlato == null)
                throw new ArgumentNullException(nameof(oBEPlato), "No se proporcionó un plato válido.");

            if (oBEPlato.ListaInsumos == null || oBEPlato.ListaInsumos.Count == 0)
                throw new Exception("El plato no tiene insumos para guardar.");

            foreach (var insumo in oBEPlato.ListaInsumos)
            {
                insumo.Plato = oBEPlato; // Asegurarse que cada insumo tenga el plato asociado
                Guardar(insumo);
            }
        }

        public void Eliminar(int idPlato)
        {
            oMPPPlatoInsumo.Eliminar(idPlato); // elimina todos los insumos asociados a un plato
        }

        public List<BEPlatoInsumo> ListarTodo(int idPlato)
        {
            var lista = oMPPPlatoInsumo.ListarPorPlato(idPlato);

            // Completo nombre y unidad desde BLLInsumo
            var insumosDisponibles = bllInsumo.ListarTodo();

            foreach (var pi in lista)
            {
                var insumoBase = insumosDisponibles.FirstOrDefault(i => i.Id == pi.Insumo.Id);
                if (insumoBase != null)
                {
                    pi.Insumo.Nombre = insumoBase.Nombre;
                    pi.Insumo.UnidadMedida = insumoBase.UnidadMedida;
                }
            }

            return lista;
        }
    }
}

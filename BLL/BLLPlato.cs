using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPlato
    {

        private MPPPlato oMPPPlato = new MPPPlato();
        private BLLInsumo oBLLInsumo = new BLLInsumo();

        public bool CrearXML() { throw new NotImplementedException(); }

        public void Eliminar(BEPlato oBEPlato) { oMPPPlato.Eliminar(oBEPlato); }

        public void Guardar(BEPlato oBEPlato) { oMPPPlato.Guardar(oBEPlato); }

        public BEPlato ListarObjeto(BEPlato oBEPlato) { return oMPPPlato.ListarObjeto(oBEPlato); }
        public List<BEPlatoInsumo> ListarInsumosPorPlato(int idPlato)
        {
            List<BEPlatoInsumo> lista = new List<BEPlatoInsumo>();

            // Obtenemos el BEPlato completo
            BEPlato plato = ListarObjetoPorId(new BEPlato { Id = idPlato });

            // Traemos los datos crudos del mapper (idInsumo + cantidad)
            var insumosDatos = oMPPPlato.ListarInsumosPorPlato(idPlato);

            // Por cada insumo declarado en el XML del plato
            foreach (var dato in insumosDatos)
            {
                // Obtenemos el objeto BEInsumo completo
                BEInsumo insumo = oBLLInsumo.ListarObjetoPorId(dato.IdInsumo);

                if (insumo != null)
                {
                    // Creamos la relación Plato–Insumo con su cantidad y costo unitario
                    BEPlatoInsumo pi = new BEPlatoInsumo(insumo, dato.Cantidad)
                    {
                        Plato = plato
                    };

                    lista.Add(pi);
                }
            }

            return lista;
        }

        public BEPlato ListarObjetoPorId(BEPlato oBEPlato)
        {
            var plato = oMPPPlato.ListarObjetoPorId(oBEPlato);
            if (plato == null) return null;

            var listaInsumosDisponibles = new BLLInsumo().ListarTodo(); // Solo en BLL

            foreach (var pi in plato.ListaInsumos)
            {
                var insumoBase = listaInsumosDisponibles.FirstOrDefault(ins => ins.Id == pi.Insumo.Id);
                if (insumoBase != null)
                {
                    pi.Insumo.Nombre = insumoBase.Nombre;
                    pi.Insumo.UnidadMedida = insumoBase.UnidadMedida;
                }
            }

            return plato;
        }

        public List<BEPlato> ListarTodo()
        {
            var listaPlatos = oMPPPlato.ListarTodo(); // Mapper solo da IDs y cantidades

            var listaInsumosDisponibles = new BLLInsumo().ListarTodo(); // Ahora sí, BLL

            // Completar nombre, unidad y precio
            foreach (var plato in listaPlatos)
            {
                foreach (var pi in plato.ListaInsumos)
                {
                    var insumoBase = listaInsumosDisponibles.FirstOrDefault(i => i.Id == pi.Insumo.Id);
                    if (insumoBase != null)
                    {
                        pi.Insumo.Nombre = insumoBase.Nombre;
                        pi.Insumo.UnidadMedida = insumoBase.UnidadMedida;
                    }
                }
            }

            return listaPlatos;
        }

        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEPlato objeto) { throw new NotImplementedException(); }



    }
}

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

        public bool CrearXML() { throw new NotImplementedException(); }

        public void Eliminar(BEPlato oBEPlato) { oMPPPlato.Eliminar(oBEPlato); }

        public void Guardar(BEPlato oBEPlato) { oMPPPlato.Guardar(oBEPlato); }

        public BEPlato ListarObjeto(BEPlato oBEPlato) { return oMPPPlato.ListarObjeto(oBEPlato); }

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

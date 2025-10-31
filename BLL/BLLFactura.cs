using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper;

namespace BLL
{
    public class BLLFactura
    {
        private MPPFactura oMPPFactura = new MPPFactura();
        public bool CrearXML() { throw new NotImplementedException(); }
        public void Guardar(BEFactura factura)
        {
            try
            {
                 oMPPFactura.Guardar(factura);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la factura: " + ex.Message);
            }
        }

        public List<BEFactura> ListarTodo()
        {
            try
            {
                return oMPPFactura.ListarTodo();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar facturas: " + ex.Message);
            }
        }
        public string ObtenerProximoNumeroFactura()
        {
            try
            {
                return oMPPFactura.ObtenerProximoNumeroFactura();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el número de factura: " + ex.Message);
            }
        }

        //public BEFactura ListarObjetoPorId(int id)
        //{
        //    try
        //    {
        //        return oMPPFactura.ListarObjetoPorId(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener factura: " + ex.Message);
        //    }
        //}

        //public int ObtenerUltimoId()
        //{
        //    try
        //    {
        //        return oMPPFactura.ObtenerUltimoId();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener el último ID de factura: " + ex.Message);
        //    }
        //}

        //public bool ExisteFactura(int id)
        //{
        //    try
        //    {
        //        return oMPPFactura.ExisteFactura(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al verificar existencia de factura: " + ex.Message);
        //    }
        //}
    }
}

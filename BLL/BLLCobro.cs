using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper;

namespace BLL
{
    public class BLLCobro
    {
        private MPPCobro oMPPCobro = new MPPCobro();
        public void Guardar(BECobro cobro)
        {
            try
            {
               oMPPCobro.Guardar(cobro);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el cobro: " + ex.Message);
            }
        }

        public List<BECobro> ListarTodo()
        {
            try
            {
                return oMPPCobro.ListarTodo();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cobros: " + ex.Message);
            }
        }

        //public BECobro ListarObjetoPorId(int id)
        //{
        //    try
        //    {
        //        return oMPPCobro.ListarObjetoPorId(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener cobro: " + ex.Message);
        //    }
        //}

        //public int ObtenerUltimoId()
        //{
        //    try
        //    {
        //        return oMPPCobro.ObtenerUltimoId();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener el último ID de cobro: " + ex.Message);
        //    }
        //}

        //public bool ExisteCobro(int id)
        //{
        //    try
        //    {
        //        return oMPPCobro.ExisteCobro(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al verificar existencia de cobro: " + ex.Message);
        //    }
        //}
    }
}

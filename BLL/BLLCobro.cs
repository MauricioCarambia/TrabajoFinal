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
        private MPPFactura oMPPFactura = new MPPFactura();
        private MPPUsuarios oMPPUsuario = new MPPUsuarios();
        private MPPPedido oMPPPedido = new MPPPedido();
        private MPPCliente oMPPCliente = new MPPCliente();
        private BLLFactura oBLLFactura = new BLLFactura();
        private BLLPedido oBLLPedido = new BLLPedido();
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
            // 🔹 Traer todos los cobros directos desde el XML vía Mapper
            List<BECobro> lista = oMPPCobro.ListarTodo();

            foreach (var cobro in lista)
            {
                // 🔹 Traer datos completos de la factura
                if (cobro.Factura != null && cobro.Factura.IdFactura > 0)
                {
                    cobro.Factura = oMPPFactura.ListarObjetoPorId(cobro.Factura.IdFactura);
                }

                // 🔹 Traer datos completos del pedido
                if (cobro.Pedido != null && cobro.Pedido.Id > 0)
                {
                    cobro.Pedido = oMPPPedido.ListarObjetoPorId(cobro.Pedido.Id);
                }

              // 🔹 Traer datos completos del cliente del pedido
                if (cobro.Pedido?.Reserva?.Cliente != null && cobro.Pedido.Reserva.Cliente.Id > 0)
                {
                    cobro.Pedido.Reserva.Cliente = oMPPCliente.ListarObjetoPorIdCliente(new BECliente { Id = cobro.Pedido.Reserva.Cliente.Id });
                }
            }

            return lista;
        }

        public List<int> ListarPedidosCobrados()
        {
            try
            {
                return oMPPCobro.ListarPedidosCobrados();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cobros: " + ex.Message);
            }
        }
        //public BECobro ListarObjetoPorId(int idCobro)
        //{
        //    try
        //    {
        //        BECobro cobro = oMPPCobro.ListarObjetoPorId(idCobro);
        //        if (cobro == null) return null;

        //        // 🔹 Traer los objetos completos
        //        BLLFactura oBLLFactura = new BLLFactura();
        //        BLLPedido oBLLPedido = new BLLPedido();

        //        cobro.Factura = oBLLFactura.ListarObjetoPorId(cobro.Factura.IdFactura);
        //        cobro.Pedido = oBLLPedido.ListarObjetoPorId(cobro.Pedido.Id);

        //        return cobro;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener cobro completo: " + ex.Message);
        //    }
        //}
    }
}

using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPedido
    {
        private MPPPedido oMPPPedido = new MPPPedido();

        public bool CrearXML() { throw new NotImplementedException(); }

        //public void Eliminar(BEPedido oBEPedido) { oMPPPedido.Eliminar(oBEPedido); }

        public void Guardar(BEPedido oBEPedido) { oMPPPedido.Guardar(oBEPedido); }

        //public BEPedido ListarObjeto(BEPedido oBEPedido) { return oMPPPedido.ListarObjeto(oBEPedido); }

        public BEPedido ListarObjetoPorId(BEPedido oBEPedido) { return oMPPPedido.ListarObjetoPorId(oBEPedido); }
        public void DescontarStockInsumos(BEPedido oBEPedido)
        {
            oMPPPedido.DescontarStockInsumos(oBEPedido);
        }
        //public void ReponerStockInsumos(BEPedido oBEPedido)
        //{
        //    oMPPPedido.ReponerStockInsumos(oBEPedido);
        //}
        public List<string> ConfirmarPedido(BEPedido pedidoTemporal, BEPedido pedidoOriginal)
        {
            List<string> errores = new List<string>();

            if (pedidoTemporal == null || pedidoTemporal.ListaPlatos.Count == 0)
            {
                errores.Add("No hay platos pendientes para confirmar.");
                return errores;
            }

            // 1️⃣ Verificar stock en Mapper
            if (!oMPPPedido.VerificarStockPedido(pedidoTemporal, out List<string> erroresStock))
            {
                return erroresStock;
            }

            try
            {
                // 2️⃣ Descontar stock en Mapper
                oMPPPedido.DescontarStockInsumos(pedidoTemporal);

                // 3️⃣ Cambiar estado de los platos en la copia original
                foreach (var plato in pedidoTemporal.ListaPlatos)
                {
                    var original = pedidoOriginal.ListaPlatos.First(p => p.Plato.Id == plato.Plato.Id);
                    original.Estado = BEPedidoPlato.EstadoPlato.Confirmado;
                }

                //// 4️⃣ Cambiar estado general del pedido si todos los platos ya están confirmados
                //if (pedidoOriginal.ListaPlatos.All(p => p.Estado == BEPedidoPlato.EstadoPlato.Confirmado))
                //    pedidoOriginal.Estado = BEPedido.EstadoPedido.Cerrado;

                pedidoOriginal.Fecha = DateTime.Now;

                // 5️⃣ Guardar cambios en Mapper
                oMPPPedido.Guardar(pedidoOriginal);
            }
            catch (Exception ex)
            {
                errores.Add("Error al confirmar el pedido: " + ex.Message);
            }

            return errores; // vacía si todo salió bien
        }
        public BEPedido ListarPorReserva(int reservaId)
        {
            return oMPPPedido.ListarPorReserva(reservaId);
        }

        public List<BEPedido> ListarTodo() { return oMPPPedido.ListarTodo(); }
        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEPedido objeto) { throw new NotImplementedException(); }
        public decimal CalcularTotal(BEPedido pedido)
        {
            return pedido.ListaPlatos.Sum(p => (p.Plato?.PrecioVenta ?? 0) * p.Cantidad);
        }
    }
}

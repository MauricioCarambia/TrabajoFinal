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
        private MPPReserva oMPPReserva = new MPPReserva();
        private BLLMesa oBLLMesa = new BLLMesa();

        public bool CrearXML() { throw new NotImplementedException(); }

        public void EliminarPlato(BEPedido pedido, BEPedidoPlato plato, int cantidadAEliminar)
        {
            if (pedido == null || plato == null || cantidadAEliminar <= 0)
                return;

            // 1️⃣ Reponer stock proporcional
            BEPedidoPlato platoTemporal = new BEPedidoPlato
            {
                Plato = plato.Plato,
                Cantidad = cantidadAEliminar
            };
            MPPPedido mpp = new MPPPedido();
            mpp.ReponerStockInsumos(platoTemporal);

            // 2️⃣ Eliminar plato del XML (total o parcial)
            mpp.EliminarPlato(pedido.Id, plato.Id, cantidadAEliminar);
        }

        public void Guardar(BEPedido oBEPedido) { oMPPPedido.Guardar(oBEPedido); }

        //public BEPedido ListarObjeto(BEPedido oBEPedido) { return oMPPPedido.ListarObjeto(oBEPedido); }

        public BEPedido ListarObjetoPorId(int pedidoId) { return oMPPPedido.ListarObjetoPorId(pedidoId); }
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
        public List<BEPedido> ListarPedidosPorFecha(DateTime fecha)
        {
            var pedidos = oMPPPedido.ListarPedidosPorFecha(fecha); // Obtener todos los pedidos de ese día

            foreach (var pedido in pedidos)
            {
                if (pedido.Reserva != null)
                {
                    // El cliente ya viene de la reserva
                    pedido.Cliente = pedido.Reserva.Cliente;

                    // La mesa también viene de la reserva
                    pedido.Reserva.Mesa = pedido.Reserva.Mesa ?? oBLLMesa.ListarObjetoPorNumeroMesa(
                        new BEMesa { NumeroMesa = pedido.Reserva.Mesa?.NumeroMesa ?? 0 });
                }
            }

            return pedidos;
        }
        public BEPedido ListarPorReserva(int reservaId)
        {
            var pedido = oMPPPedido.ListarPorReserva(reservaId);
            if (pedido == null)
                return null;

            BLLReserva oBLLReserva = new BLLReserva();
            // Traer la reserva completa
            pedido.Reserva = oBLLReserva.ListarObjetoPorId(pedido.Reserva);

            // Traer el cliente completo
            BLLCliente oBLLCliente = new BLLCliente();
            pedido.Cliente = oBLLCliente.ListarObjetoPorId(pedido.Cliente);

            // Traer los platos completos
            foreach (var item in pedido.ListaPlatos)
            {
                BLLPlato oBLLPlato = new BLLPlato();
                item.Plato = oBLLPlato.ListarObjetoPorId(item.Plato);
            }

            return pedido;
        }

        public List<BEPedido> ListarTodo() { return oMPPPedido.ListarTodo(); }
        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEPedido objeto) { throw new NotImplementedException(); }
        public decimal CalcularTotal(BEPedido pedido)
        {
            return pedido.ListaPlatos.Sum(p => (p.Plato?.PrecioVenta ?? 0) * p.Cantidad);
        }
        // Traer platos pendientes o en preparación
        //public List<BEPedidoPlato> ObtenerPlatosPendientes()
        //{
        //    return oMPPPedido.ListarPlatosPorEstado("Pendiente");
        //}

        //// Traer platos terminados
        public List<BEPedidoPlato> ObtenerPlatosTerminados()
        {
            return oMPPPedido.ObtenerPlatosPorEstado("Terminado");
        }

        // Método genérico si querés filtrar por cualquier estado
        public List<BEPedidoPlato> ObtenerPlatosPorEstado(string estado)
        {
            return oMPPPedido.ObtenerPlatosPorEstado(estado);
        }
        public void CambiarEstadoPlato(int idPedido, int idPedidoPlato, BEPedidoPlato.EstadoPlato nuevoEstado)
        {
            if (idPedido <= 0 || idPedidoPlato <= 0)
                throw new ArgumentException("Id de pedido o plato inválido.");

            // Llamada al MPP para que modifique el XML
            oMPPPedido.CambiarEstadoPlato(idPedido, idPedidoPlato, nuevoEstado);
        }
        public void EnviarPlatosACocina(int pedidoId)
        {
            try
            {
                oMPPPedido.EnviarPlatosACocina(pedidoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar platos a cocina: {ex.Message}");
            }
        }
        public void CambiarEstadoPedido(int idPedido, BEPedido.EstadoPedido nuevoEstado)
        {
            if (idPedido <= 0) throw new ArgumentException("Id de pedido inválido.");

            oMPPPedido.CambiarEstadoPedido(idPedido, nuevoEstado);
        }
        public void EnviarPedidoACobranza(int pedidoId)
        {
            if (pedidoId <= 0)
                throw new Exception("Pedido inválido.");

            oMPPPedido.EnviarPedidoACobranza(pedidoId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Mapper;

namespace BLL
{
    public class BLLReserva
    {
        private MPPReserva oMPPReserva = new MPPReserva();
        private MPPPedido oMPPPedido = new MPPPedido();
        private BLLCliente oBLLCliente = new BLLCliente();
        private BLLMesa oBLLMesa = new BLLMesa();

        public bool CrearXML() { throw new NotImplementedException(); }

        public void Eliminar(BEReserva oBEReserva) { oMPPReserva.Eliminar(oBEReserva); }

        public void Guardar(BEReserva oBEReserva) { oMPPReserva.Guardar(oBEReserva); }

        public BEReserva ListarObjeto(BEReserva BEReserva) { return oMPPReserva.ListarObjeto(BEReserva); }

        public BEReserva ListarObjetoPorId(BEReserva BEReserva) { return oMPPReserva.ListarObjetoPorIdReserva(BEReserva); }

        public List<BEReserva> ListarTodo() { return oMPPReserva.ListarTodo(); }

        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEReserva objeto) { throw new NotImplementedException(); }
        public List<BEReserva> ListarPorFecha(DateTime fecha)
        {
            var reservas = oMPPReserva.ListarPorFecha(fecha);

            foreach (var r in reservas)
            {
                // Completa los datos del cliente y la mesa
                r.Cliente = oBLLCliente.ListarObjetoPorId(new BECliente { Id = r.Cliente.Id });
                r.Mesa = oBLLMesa.ListarObjetoPorNumeroMesa(new BEMesa { NumeroMesa = r.Mesa.NumeroMesa });

            }

            return reservas;
        }
        public List<BEPedido> ListarPedidosPorFecha(DateTime fecha)
        {
            var pedidos = oMPPPedido.ListarPedidosPorFecha(fecha);
            var reservas = oMPPReserva.ListarTodo();

            // Filtramos los pedidos según la fecha de su reserva
            List<BEPedido> pedidosFiltrados = new List<BEPedido>();

            foreach (var pedido in pedidos)
            {
                var reserva = reservas.FirstOrDefault(r => r.Id == pedido.Reserva.Id);

                if (reserva != null && reserva.FechaReserva.Date == fecha.Date)
                {
                    pedido.Reserva = reserva;
                    pedido.Cliente = reserva.Cliente;
                    pedidosFiltrados.Add(pedido);
                }
            }

            return pedidosFiltrados;
        }
        //public BEReserva ObtenerReservaCompleta(int idReserva)
        //{
        //    // Llamamos al DAL/Mapper
        //    BEReserva reserva = oMPPReserva.ListarObjetoPorIdReserva(new BEReserva { Id = idReserva });

        //    if (reserva == null)
        //        throw new Exception("No se encontró la reserva.");

        //    // Llenar cliente completo
        //    if (reserva.Cliente?.Id > 0)
        //        reserva.Cliente = oBLLCliente.ListarObjetoPorIdCliente(new BECliente { Id = reserva.Cliente.Id });

        //    // Llenar mesa completa
        //    if (reserva.Mesa?.IdMesa > 0)
        //        reserva.Mesa = oBLLMesa.ListarObjeto(new BEMesa { IdMesa = reserva.Mesa.IdMesa });

        //    return reserva;
        //}
    }
}

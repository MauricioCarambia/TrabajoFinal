using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProveedor
    {
        public MPPProveedor  oMPPProveedor = new MPPProveedor(); 

        public bool CrearXML() { throw new NotImplementedException(); }

        public bool Eliminar(BEProveedor oBEProveedor) { return oMPPProveedor.Eliminar(oBEProveedor); }

        public bool Guardar(BEProveedor oBEProveedor) { return oMPPProveedor.Guardar(oBEProveedor); }

        public BEProveedor ListarObjeto(BEProveedor oBEProveedor) { return oMPPProveedor.ListarObjeto(oBEProveedor); }

        public List<BEProveedor> ListarTodo() { return oMPPProveedor.ListarTodo(); }

        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEProveedor oBEProveedor) { throw new NotImplementedException(); }

        //public bool AsociarProveedorProducto(BEProveedor oBEProveedor, BEProducto oBEProducto, BEStockProductoProveedor oBEStockProductoProveedor, BEPrecioProductoProveedor oBEPrecioProductoProveedor) { return oMPPProveedor.AsociarProveedorProducto(oBEProveedor, oBEProducto, oBEStockProductoProveedor, oBEPrecioProductoProveedor); }

        //public bool DesasociarProveedorProducto(BEProveedor oBEProveedor, BEProducto oBEProducto, BEStockProductoProveedor oBEStockProductoProveedor, BEPrecioProductoProveedor oBEPrecioProductoProveedor) { return oMPPProveedor.DesasociarProveedorProducto(oBEProveedor, oBEProducto, oBEStockProductoProveedor, oBEPrecioProductoProveedor); }

        ////public bool RestarStockProveedorPorEntrega(BEProveedor oBEProovedor, BEProducto oBEProducto) { return oMPPProveedor.RestarStockProveedorPorEntrega(oBEProovedor, oBEProducto); }

        //public bool VerificarProductoEnProveedor(BEProveedor oBEProveedor, BEProducto oBEProducto) { return oMPPProveedor.VerificarProductoEnProveedor(oBEProveedor, oBEProducto); }

        //public List<BEProducto> ListarProductosDelProveedor(BEProveedor oBEProveedor) { return oMPPProveedor.ListarProductosDelProveedor(oBEProveedor); }

        public int ContarTotalProveedores() { return oMPPProveedor.ContarTotalProveedores(); }
    }
}

using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLInsumo
    {
        public MPPInsumo oMPPInsumo = new MPPInsumo();

        public bool CrearXML() { throw new NotImplementedException(); }

        public bool Eliminar(BEInsumo oBEInsumo) { return oMPPInsumo.Eliminar(oBEInsumo); }

        public bool Guardar(BEInsumo oBEInsumo) { return oMPPInsumo.Guardar(oBEInsumo); }

        public BEInsumo ListarObjetoPorId(int idInsumo)
        {
            return oMPPInsumo.ListarObjetoPorId(idInsumo);
        }
        public BEInsumo ListarObjeto(BEInsumo oBEInsumo) { return oMPPInsumo.ListarObjeto(oBEInsumo); }
       

        public List<BEInsumo> ListarTodo() { return oMPPInsumo.ListarTodo(); }

        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEInsumo oBEInsumo) { throw new NotImplementedException(); }
        public void DescontarStock(int idInsumo, decimal cantidadADescontar)
        {
            oMPPInsumo.DescontarStock(idInsumo, cantidadADescontar);
        }

        //public bool AsociarProveedorProducto(BEProveedor oBEProveedor, BEProducto oBEProducto, BEStockProductoProveedor oBEStockProductoProveedor, BEPrecioProductoProveedor oBEPrecioProductoProveedor) { return oMPPProveedor.AsociarProveedorProducto(oBEProveedor, oBEProducto, oBEStockProductoProveedor, oBEPrecioProductoProveedor); }

        //public bool DesasociarProveedorProducto(BEProveedor oBEProveedor, BEProducto oBEProducto, BEStockProductoProveedor oBEStockProductoProveedor, BEPrecioProductoProveedor oBEPrecioProductoProveedor) { return oMPPProveedor.DesasociarProveedorProducto(oBEProveedor, oBEProducto, oBEStockProductoProveedor, oBEPrecioProductoProveedor); }

        ////public bool RestarStockProveedorPorEntrega(BEProveedor oBEProovedor, BEProducto oBEProducto) { return oMPPProveedor.RestarStockProveedorPorEntrega(oBEProovedor, oBEProducto); }

        //public bool VerificarProductoEnProveedor(BEProveedor oBEProveedor, BEProducto oBEProducto) { return oMPPProveedor.VerificarProductoEnProveedor(oBEProveedor, oBEProducto); }

        //public List<BEProducto> ListarProductosDelProveedor(BEProveedor oBEProveedor) { return oMPPProveedor.ListarProductosDelProveedor(oBEProveedor); }

        //public int ContarTotalProveedores() { return oMPPInsumo.ContarTotalProveedores(); }
    }
}

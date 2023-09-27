using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DS_Process.Querys
{
    internal class America
    {
        private static string _yearFrom 
        {
            get
            {
              return  ConfigurationManager.AppSettings["YearFrom"].ToString();
            }
        }
        public static string GetQueryImportarVentas(int year)
        {
                return "select 'VENTA' TipoTrasaccion, " +
                        "convert(char,  fc.fecha_regis,112) Fecha_codi, " +
                        "p.produc, " +
                        "Null Prov_codi, " +
                        "fc.cliente cliente_codi, " +
                        "fc.vende, " +
                        "isnull(sup.super, '000') super, " +
                        "p.agru_5 segmento, " +
                        "p.agru_1 Rubro, " +
                        "p.agru_6 GruposEstacionales, " +
                        "p.agru_4 GruposNegocio, " +
                        "sum((fi.canti_venta - fi.canti_boni) * uc.conver) -sum(isnull(ci.canti_venta, 0) * uc.conver) TotalVentaKg, " +
                        "sum((fi.canti_venta - fi.canti_boni) * ucb.conver) - sum(isnull(ci.canti_venta, 0) * ucb.conver) TotalVentaBU, " +
                        "sum((fi.canti_venta - fi.canti_boni) * ucl.conver) - sum(isnull(ci.canti_venta, 0) * ucl.conver) TotalVentaLTS, " +
                        "SUM((fi.precio_uni_neto * fi.canti_venta) - fi.boni_impor) ImporteTotal, " +
                        "fc.factu_nume " +
                        " " +
                        "from factu_cabe fc " +
                        "inner join factu_items fi on fi.factu_nume = fc.factu_nume and fi.factu_codi = fc.factu_codi and fi.sucur = fc.sucur " +
                        "LEFT join credi_items ci on ci.factu_nume = fi.factu_nume and ci.factu_codi = fi.factu_codi and ci.factu_sucur = fi.sucur and ci.produc = fi.produc and ci.factu_linea_nume = fi.linea_nume " +
                        "left join uni_medi_produc_conver uc on uc.produc = fi.produc and uc.uni1 = fi.uni_medi and uc.uni2 = 'KG' " +
                        "left join uni_medi_produc_conver ucb on ucb.produc = fi.produc and ucb.uni1 = fi.uni_medi and ucb.uni2 = 'BU' " +
                        "left join uni_medi_produc_conver ucl on ucl.produc = fi.produc and ucl.uni1 = fi.uni_medi and ucl.uni2 = 'Li' " +
                        "inner join productos p on p.produc = fi.produc " +
                        "inner join vendedores ven on ven.codi = fc.vende " +
                        "left join supervisores sup on sup.super = ven.super " +
                        "where fi.depo = '01' " +
                        "and year(fc.fecha_regis) = " + year.ToString() + " " +
                        "group by " +
                        "fc.fecha_regis, " +
                        "p.produc, " +
                        "fc.cliente, " +
                        "fc.vende, " +
                        "sup.super, " +
                        "p.agru_5, " +
                        "p.agru_1, " +
                        "p.agru_6, " +
                        "p.agru_4, " +
                        "fc.factu_nume ";
        }

        public static string GetQueryImportarCompras(int year)
        {

                return "select 'COMPRA' TipoTrasaccion, " +
                        "convert(char,  fc.fecha_regis,112) Fecha_codi, " +
                        "pr.produc, " +
                        "fc.proveedor Prov_codi, " +
                        "null cliente_codi, " +
                        "null vende, " +
                        "'000' super, " +
                        "pr.agru_5 segmento, " +
                        "pr.agru_1 Rubro, " +
                        "pr.agru_6 GruposEstacionales, " +
                        "pr.agru_4 GruposNegocio, " +
                        "sum(fi.canti * uc.conver)  TotalCompraKg, " +
                        "sum(fi.canti * ucb.conver) TotalCompraBU, " +
                        "sum(fi.canti * ucl.conver) TotalCompraLTS, " +
                        "0 ImporteTotal, " +
                        "fc.factu_nume " +
                        " from factu_prove_cabe fc " +
                        "    inner join factu_prove_items fi on fi.factu_nume = fc.factu_nume and fi.factu_codi = fc.factu_codi and fi.factu_letra = fc.factu_letra " +
                        "    inner join productos pr on pr.produc = fi.produc " +
                        "    left join uni_medi_produc_conver uc on uc.produc = pr.produc and uc.uni1 = pr.uni_medi_compra and uc.uni2 = 'KG' " +
                        "    left join uni_medi_produc_conver ucb on ucb.produc = pr.produc and ucb.uni1 = pr.uni_medi_compra and ucb.uni2 = 'BU' " +
                        "    left join uni_medi_produc_conver ucl on ucl.produc = pr.produc and ucl.uni1 = pr.uni_medi_compra and ucl.uni2 = 'Li' " +
                        "where year(fc.fecha_regis) = " + year.ToString() + " " +
                        "group by " +
                        "fc.fecha_regis, " +
                        "fc.proveedor, " +
                        "pr.produc, " +
                        "pr.agru_5, " +
                        "pr.agru_1, " +
                        "pr.agru_6, " +
                        "pr.agru_4, " +
                        "fc.factu_nume ";
        }
    }
}

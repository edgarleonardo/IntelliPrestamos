using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class PagoManager : BaseManager<Pagos>
    {
        public PagoManager()
            : base()
        {            
        }

        public void SetExtra(int id_prestamo, decimal monto, int afecta_prestamo)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_prestamo", id_prestamo),
                    new SqlParameter("@monto", monto),
                    new SqlParameter("@Afecta_prestamo", afecta_prestamo)
            };
            var result = Get("SET_pagos_extra  @id_prestamo, @monto, @Afecta_prestamo", parameters);
            if (result != null && result.Count > 0 && result[0].errorMessage.Trim() != "")
            {
                Error_Message = result[0].errorMessage;
            }
            if (Error_Message != null && !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
        }
        public void Set(int id_prestamo, int numero_cuotas, decimal monto, int mora_status)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_prestamo", id_prestamo),
                    new SqlParameter("@numero_cuota", numero_cuotas),
                    new SqlParameter("@monto", monto),
                    new SqlParameter("@mora_status", mora_status)
            };
            var result = Get("SET_pagos  @id_prestamo, @numero_cuota, @monto, @mora_status", parameters);
            if (result != null && result.Count > 0 && result[0].errorMessage.Trim() != "")
            {
                Error_Message = result[0].errorMessage;
            }
            if (Error_Message != null && !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
        }
        public Pagos Get(int prestamos_id, int numero)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_prestamo", prestamos_id),
                    new SqlParameter("@numero_id", numero)
            };
            Pagos pago = new Pagos();
            var result = Get("GET_Pago @id_prestamo,@numero_id", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                if (result != null && result.Count > 0)
                {
                    pago = result[0];
                }
                return pago;
            }
        }
        public Pagos Get(int prestamos_id, int numero, int pago_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_prestamo", prestamos_id),
                    new SqlParameter("@numero_id", numero),
                    new SqlParameter("@pago_id", pago_id)
            };
            Pagos pago = new Pagos();
            var result = Get("GET_Pago_detail @id_prestamo,@numero_id, @pago_id", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                if (result != null && result.Count > 0)
                {
                    pago = result[0];
                }
                return pago;
            }
        }

        public List<Pagos> GetPays(int prestamos_id, int numero, int pago_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_prestamo", prestamos_id),
                    new SqlParameter("@numero_id", numero),
                    new SqlParameter("@pago_id", pago_id)
            };
            var result = Get("GET_Pago_detail @id_prestamo,@numero_id, @pago_id", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
               
                return result;
            }
        }
    }
}

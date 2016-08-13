using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class DocumentManager : BaseManager<Document>
    {
        public DocumentManager()
            : base()
        {
            
        }

        public List<Document> Get()
        {
            var result = Get("get_document");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }
        public List<Document> Get(int Flujo_Id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@flujo_id", Flujo_Id)};
            var result = Get("get_document_by @flujo_id", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                return result;
            }
        }
        public Document GetById(int Id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID", Id)};
            var result = Get("get_document_by_id @ID", parameters);
            if (result == null  || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Document cargo = new Document();
                if (result.Count > 0)
                {
                    cargo=result[0];
                } 
                return cargo;
            }
        }
        public void Set(Document model)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@id", model.Id),
                    new SqlParameter("@flujo_id", model.Flujo_Id), 
                    new SqlParameter("@nombre_documento", model.Nombre_Documento),
                    new SqlParameter("@documento", model.Documento),
                    new SqlParameter("@activo", model.Activo)
            };
            Execute(@"Set_document @id,@flujo_id,@nombre_documento,@documento, @activo", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }             
        }

        public void Del(Document model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID", model.Id)
            };
            Execute(@"del_document_by @ID", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }            
        }
        public string DocumentParseWithVariables(int solicitud_id, int prestamo_id, string cedula, int document_id)
        {
            string resultHtml = "";
            var parameters = new SqlParameter[]{
                new SqlParameter("@solicitud_id", solicitud_id),
                    new SqlParameter("@prestamo_id", prestamo_id), 
                    new SqlParameter("@cedula", cedula),
                    new SqlParameter("@document_id", document_id)
            };
            var result = Get(@"SET_SYSTEM_VARIABLES @solicitud_id,@prestamo_id,@cedula,@document_id", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Document cargo = new Document();
                if (result.Count > 0)
                {
                    cargo = result[0];
                }
                resultHtml = cargo.Documento;
            }
            return resultHtml;
        }
        
    }
}

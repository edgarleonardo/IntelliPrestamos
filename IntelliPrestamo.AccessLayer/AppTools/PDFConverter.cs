using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winnovative;

namespace IntelliPrestamo.AccessLayer.AppTools
{
    public class PDFConverter
    {
        /// <summary>
        /// Generar un pdf a partir de una url para la carta e info de adtrends
        /// </summary>
        /// <param name="url"></param>
        /// <param name="proposalId"></param>
        /// <param name="mkId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GeneratePdfFromString(string html, string NombreArchivo, string LocalRoot, string RemoteRoot)
        {
            string debugger = string.Empty;
            string fileName = NombreArchivo + DateTime.Now.Millisecond.ToString();
            string returnURL = LocalRoot + "PDFs\\" + "\\" + fileName + ".pdf";
            try
            {                
                HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
                // Leave it not set to use the converter in demo mode
                htmlToPdfConverter.LicenseKey = "11lKWElYQEpNWElJVkhYS0lWSUpWQUFBQQ==";

                // Set HTML Viewer width in pixels which is the equivalent in converter of the browser window width
                htmlToPdfConverter.HtmlViewerWidth = 800;
                // Set PDF page size which can be a predefined size like A4 or a custom size in points 
                // Leave it not set to have a default A4 PDF page
                htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;

                // Set PDF page orientation to Portrait or Landscape
                // Leave it not set to have a default Portrait orientation for PDF page
                htmlToPdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;
                htmlToPdfConverter.PdfDocumentOptions.AvoidImageBreak = true;
                htmlToPdfConverter.PdfDocumentOptions.AvoidTextBreak = true;
                
                try { File.Delete(returnURL); }
                catch { }
                string htmlUrl = returnURL.Replace(".pdf",".html");
                try { File.Delete(htmlUrl); }
                catch { }
                File.WriteAllText(htmlUrl, html);
                string lv_url = RemoteRoot+ "/PDFs/" + fileName + ".html";
          
                byte[] downloadBytes = htmlToPdfConverter.ConvertUrl(lv_url);
                FileStream fs = new FileStream(returnURL, FileMode.Create);
                fs.Write(downloadBytes, 0, downloadBytes.Length);


                fs.Flush();
                fs.Close();
                fileName = returnURL.Replace(".html", ".pdf");
            }
            catch (Exception e)
            {
            }

            return fileName;
        }
        /// <summary>
        /// Generar un pdf a partir de una url para la carta e info de adtrends
        /// </summary>
        /// <param name="url"></param>
        /// <param name="proposalId"></param>
        /// <param name="mkId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GeneratePdfFromUrl( string NombreArchivo, string LocalRoot, string Url, string remoteUrl)
        {
            string debugger = string.Empty;
            string fileName = NombreArchivo + DateTime.Now.Millisecond.ToString();
            string returnURL = LocalRoot + "PDFs\\" + "\\" + fileName + ".pdf";
            try
            {
                HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
                // Leave it not set to use the converter in demo mode
                htmlToPdfConverter.LicenseKey = "11lKWElYQEpNWElJVkhYS0lWSUpWQUFBQQ==";

                // Set HTML Viewer width in pixels which is the equivalent in converter of the browser window width
                htmlToPdfConverter.HtmlViewerWidth = 800;
                // Set PDF page size which can be a predefined size like A4 or a custom size in points 
                // Leave it not set to have a default A4 PDF page
                htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;

                // Set PDF page orientation to Portrait or Landscape
                // Leave it not set to have a default Portrait orientation for PDF page
                htmlToPdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;
                htmlToPdfConverter.PdfDocumentOptions.AvoidImageBreak = true;
                htmlToPdfConverter.PdfDocumentOptions.AvoidTextBreak = true;

                byte[] downloadBytes = htmlToPdfConverter.ConvertUrl(remoteUrl + Url);
                FileStream fs = new FileStream(returnURL, FileMode.Create);
                fs.Write(downloadBytes, 0, downloadBytes.Length);


                fs.Flush();
                fs.Close();
                fileName = returnURL.Replace(".html", ".pdf");
            }
            catch (Exception e)
            {
            }

            return fileName;
        }
    }
}

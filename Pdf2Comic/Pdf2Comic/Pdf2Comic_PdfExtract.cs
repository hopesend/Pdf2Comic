using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Pdf2Comic
{
    public class Pdf2Comic_PdfExtract
    {
        public objetoPDF miPDF;
        public Pdf2Comic_PdfExtract(string rutaPDF)
        {
            miPDF = new objetoPDF(rutaPDF);
            miPDF.Insertar_Imagenes(Extraer_Imagenes());
        }

        public Dictionary<System.Drawing.Image, string> Extraer_Imagenes()
        {
            Dictionary<System.Drawing.Image, string> listaImagenes = new Dictionary<System.Drawing.Image, string>();

            if(miPDF != null)
            {
                PdfReaderContentParser chekeadorPDF = new PdfReaderContentParser(miPDF.PdfLeido);
                ImageRenderListener validadorImagenes = null;

                for (byte i = 1; i <= miPDF.PdfLeido.NumberOfPages; i++)
                {
                    chekeadorPDF.ProcessContent(i, (validadorImagenes = new ImageRenderListener()));

                    if (validadorImagenes.Imagenes.Count > 0)
                    {
                        foreach (var imagenSeleccionada in validadorImagenes.Imagenes)
                        {
                            listaImagenes.Add(imagenSeleccionada.Key, imagenSeleccionada.Value);
                        }
                    }
                }

                return listaImagenes;
            }

            return null;
        }
    }

    public class ImageRenderListener : IRenderListener
    {
        private Dictionary<System.Drawing.Image, string> imagenes = new Dictionary<System.Drawing.Image, string>();

        public Dictionary<System.Drawing.Image, string> Imagenes
        {
            get { return imagenes; }
        }

        public void BeginTextBlock() { }
        public void EndTextBlock() { }
        public void RenderImage(ImageRenderInfo renderInfo)
        {
            PdfImageObject imagen = renderInfo.GetImage();
            PdfName filtro = (PdfName)imagen.Get(PdfName.FILTER);

            if (filtro != null)
            {
                System.Drawing.Image drawingImage = imagen.GetDrawingImage();
                string extension = ".";

                if (filtro == PdfName.DCTDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.JPG.FileExtension;
                }
                else if (filtro == PdfName.JPXDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.JP2.FileExtension;
                }
                else if (filtro == PdfName.FLATEDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.PNG.FileExtension;
                }
                else if (filtro == PdfName.LZWDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.CCITT.FileExtension;
                }

                this.Imagenes.Add(drawingImage, extension);
            }
        }
        public void RenderText(TextRenderInfo renderInfo) { }
    }

    public class objetoPDF
    {
        private PdfReader pdfLeido;
        public PdfReader PdfLeido
        {
            get { return pdfLeido; }
            set { pdfLeido = value; }
        }

        private Dictionary<System.Drawing.Image, string> listaImagenes = new Dictionary<System.Drawing.Image, string>();
        public Dictionary<System.Drawing.Image, string> ListaImagenes
        {
            get { return listaImagenes; }
            set { listaImagenes = value; }
        }

        private string nombrePDF;
        public string NombrePDF
        {
            get { return nombrePDF; }
            set { nombrePDF = value; }
        }

        public objetoPDF(string ruta)
        {
            pdfLeido = new PdfReader(ruta);
            //NombrePDF = pdfLeido.
        }

        public void Insertar_Imagenes(Dictionary<System.Drawing.Image, string> lista)
        {
            ListaImagenes = lista;
        }

        public List<System.Drawing.Image> Devolver_Imagenes()
        {
            if (pdfLeido != null)
            {
                List<System.Drawing.Image> listaAux = new List<System.Drawing.Image>();
                foreach (var imagen in ListaImagenes)
                {
                    listaAux.Add(imagen.Key);
                }

                return listaAux;
            }

            return null;
        }

        public Dictionary<System.Drawing.Image, string> Devolver_Diccionario_Imagenes()
        {
            if (pdfLeido != null)
            {
                return ListaImagenes;
            }

            return null;
        }
    }
}

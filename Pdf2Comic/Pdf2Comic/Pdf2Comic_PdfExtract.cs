using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Forms;

namespace Pdf2Comic
{
    public class Pdf2Comic_PdfExtract
    {
        #region Global Propierties

        public objetoPDF miPDF;

        #endregion

        #region DECLARACION EVENTOS

        /// <summary>
        /// Datos del evento StartReadEvent.
        /// </summary>
        public class StartReadEventArgs : EventArgs
        {
            /// <summary>
            /// Numero de imagenes contenidas en el pdf
            /// </summary>
            /// <value>
            /// numero total de imagenes contenidas en el pdf leido
            /// </value>
            public int totalImages { get; set; }

            /// <summary>
            /// Inicializa una instancia a la clase <see cref="StarReadEventArgs"/>.
            /// </summary>
            /// <param name="totalImages">number total of images in pdf</param>
            public StartReadEventArgs(int totalImages)
            {
                this.totalImages = totalImages;
            }
        }
        /// <summary>
        /// Delegado del evento StartRead
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">La instancia <see cref="StartReadEventArgs"/> contiene los datos del evento</param>
        public delegate void StartReadHandler(object sender, StartReadEventArgs e);
        /// <summary>
        /// Evento que se produce al comenzar a leer el pdf
        /// </summary>
        public event StartReadHandler StartReadPdf;

        /// <summary>
        /// Datos del evento StopReadEvent.
        /// </summary>
        public class StopReadEventArgs : EventArgs
        {
            /// <summary>
            /// Inicializa una instancia a la clase <see cref="StopReadEventArgs"/>.
            /// </summary>
            public StopReadEventArgs()
            {
            }
        }
        /// <summary>
        /// Delegado del evento SopRead
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">La instancia <see cref="StoptReadEventArgs"/> contiene los datos del evento</param>
        public delegate void StopReadHandler(object sender, StopReadEventArgs e);
        /// <summary>
        /// Evento que se produce al finalizar la lectura del pdf
        /// </summary>
        public event StopReadHandler StopReadPdf;

        /// <summary>
        /// Datos del evento GetImagesPdf
        /// </summary>
        public class GetImagesPdfEventArgs : EventArgs
        {
            /// <summary>
            /// Numero de Imagen en el ciclo
            /// </summary>
            /// <value>
            /// Numero de indice de la imagen el el total de Imagenes
            /// </value>
            public int newImage { get; set; }

            /// <summary>
            /// Inicializa una instancia a la clase <see cref="GetImagesPdfEventArgs"/>.
            /// </summary>
            /// <param name="newImage">index of new image in totalImages</param>
            public GetImagesPdfEventArgs(int newImage)
            {
                this.newImage = newImage;
            }
        }
        /// <summary>
        /// Delegado del evento GetImagesPdf
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">La instancia <see cref="GetImagesPdfEventArgs"/> contiene los datos del evento</param>
        public delegate void GetImagesPdfHandler(object sender, GetImagesPdfEventArgs e);
        /// <summary>
        /// Ocurre cuando una nueva imagen es leida en el pdf
        /// </summary>
        public event GetImagesPdfHandler NewImageRead;

        #endregion

        public Pdf2Comic_PdfExtract()
        {

        }

        public void LanzarExtraccion(string rutaPDF)
        {
            miPDF = new objetoPDF(rutaPDF);

            //Lanzamos el Evento de Comienzo de lectura de Pdf
            StartReadEventArgs StartEvento = new StartReadEventArgs(miPDF.PdfLeido.NumberOfPages);
            StartReadPdf(this, StartEvento);

            miPDF.Insertar_Imagenes(Extraer_Imagenes());

            //Lanzamos el Evento de Finalizacion de lectura de Pdf
            StopReadEventArgs StopEvento = new StopReadEventArgs();
            StopReadPdf(this, StopEvento);
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
                            
                            //Lanzamos el Evento de Nueva Imagen Leida
                            GetImagesPdfEventArgs NewImageEvent = new GetImagesPdfEventArgs(i);
                            NewImageRead(this, NewImageEvent);
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

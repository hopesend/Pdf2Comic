using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SharpCompress.Writer;
using SharpCompress.Common;

namespace Pdf2Comic
{
    class Pdf2Comic_ImageCompress
    {
        #region VARIABLES

        private List<string> listaPath;
        public List<string> ListaPath
        {
            get { return listaPath; }
            set { listaPath = value; }
        }

        private string nombreArchivoComprimido;
        public string NombreArchivoComprimido
        {
            get { return nombreArchivoComprimido; }
            set { nombreArchivoComprimido = value; }
        }

        # endregion

        #region EVENTOS

        /// <summary>
        /// Datos del evento StartReadEvent.
        /// </summary>
        public class StartCompressEventArgs : EventArgs
        {
            /// <summary>
            /// Numero de elementos a Comprimir
            /// </summary>
            /// <value>
            /// numero total de elementos a comprimir
            /// </value>
            public int totalElements { get; set; }

            /// <summary>
            /// Inicializa una instancia a la clase <see cref="StarCompressEventArgs"/>.
            /// </summary>
            /// <param name="totalElements">number total of elements to compress</param>
            public StartCompressEventArgs(int totalElements)
            {
                this.totalElements = totalElements;
            }
        }
        /// <summary>
        /// Delegado del evento StartCompress
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">La instancia <see cref="StartCompressEventArgs"/> contiene los datos del evento</param>
        public delegate void StartCompressHandler(object sender, StartCompressEventArgs e);
        /// <summary>
        /// Evento que se produce al comenzar comprimir una cantidad de elementos
        /// </summary>
        public event StartCompressHandler StartCompressElements;

        /// <summary>
        /// Datos del evento StopCompressEvent.
        /// </summary>
        public class StopCompressEventArgs : EventArgs
        {
            /// <summary>
            /// Inicializa una instancia a la clase <see cref="StopCompressEventArgs"/>.
            /// </summary>
            public StopCompressEventArgs()
            {
            }
        }
        /// <summary>
        /// Delegado del evento StopCompress
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">La instancia <see cref="StoptCompressEventArgs"/> contiene los datos del evento</param>
        public delegate void StopCompressHandler(object sender, StopCompressEventArgs e);
        /// <summary>
        /// Evento que se produce al finalizar la compresion de los elementos
        /// </summary>
        public event StopCompressHandler StopCompressElements;

        /// <summary>
        /// Datos del evento CompressElement
        /// </summary>
        public class CompressElementEventArgs : EventArgs
        {
            /// <summary>
            /// number of element
            /// </summary>
            /// <value>
            /// index number into total Elements
            /// </value>
            public int newElement { get; set; }

            /// <summary>
            /// Inicializa una instancia a la clase <see cref="CompressElementEventArgs"/>.
            /// </summary>
            /// <param name="newElement">index of new element in totalElements</param>
            public CompressElementEventArgs(int newElement)
            {
                this.newElement = newElement;
            }
        }
        /// <summary>
        /// Delegado del evento GetImagesPdf
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">La instancia <see cref="GetImagesPdfEventArgs"/> contiene los datos del evento</param>
        public delegate void CompressElementHandler(object sender, CompressElementEventArgs e);
        /// <summary>
        /// Ocurrs than a new element is compressed
        /// </summary>
        public event CompressElementHandler NewElementCompress;

        #endregion

        #region CONSTRUCTORES

        public Pdf2Comic_ImageCompress()
        {
            listaPath = new List<string>();
        }

        public Pdf2Comic_ImageCompress(List<string> paths, string nombre)
        {
            listaPath = new List<string>();

            listaPath = paths;
            NombreArchivoComprimido = nombre;
        }

        #endregion

        #region METODOS VARIOS

        public void Comprimir(string pathDestino)
        {
            //Lanzamos el Evento de Comienzo Compresion
            StartCompressEventArgs StartCompressEvent = new StartCompressEventArgs(listaPath.Count);
            StartCompressElements(this, StartCompressEvent);

            Stream stream = File.OpenWrite(pathDestino);
            var zipWriter = WriterFactory.Open(stream, ArchiveType.Zip, CompressionType.Deflate);

            int cont = 1;
            foreach (string ruta in ListaPath)
            {
                zipWriter.Write(Path.GetFileName(ruta), ruta);

                //Lanzamos el Evento de Nuevo Elemento Comprimido
                CompressElementEventArgs CompressElementEvent = new CompressElementEventArgs(cont);
                NewElementCompress(this, CompressElementEvent);

                cont++;
            }

            zipWriter.Dispose();
            stream.Close();

            //Lanzamos el Evento de Finalizacion de Compresion
            StopCompressEventArgs StopCompressEvent = new StopCompressEventArgs();
            StopCompressElements(this, StopCompressEvent);
        }

        #endregion
    }
}

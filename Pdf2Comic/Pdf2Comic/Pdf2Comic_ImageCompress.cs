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

        public void Comprimir(string pathDestino)
        {
            Stream stream = File.OpenWrite(pathDestino);
            var zipWriter = WriterFactory.Open(stream, ArchiveType.Zip,CompressionType.Deflate);
          
            foreach (string ruta in ListaPath)
            {
                zipWriter.Write(Path.GetFileName(ruta), ruta);
            }

            zipWriter.Dispose();
            stream.Close();
        }
    }
}

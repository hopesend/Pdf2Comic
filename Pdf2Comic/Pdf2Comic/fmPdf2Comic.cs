using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pdf2Comic
{
    public partial class fmPdf2Comic : Form
    {
        #region PROPIEDADES PUBLICAS

        public Pdf2Comic_Program datosPrograma;
        public Pdf2Comic_PdfExtract objetoPDF;
        public List<Image> ListaImagenes;
        public string nombreArchivoPDF;

        public List<string> listaPathImagenes = new List<string>();

        #endregion

        #region CONSTRUCTORES

        public fmPdf2Comic()
        {
            InitializeComponent();
        }

        #endregion

        #region EVENTOS FORMULARIO

        private void fmPdf2Comic_Load_1(object sender, EventArgs e)
        {
            datosPrograma = new Pdf2Comic_Program();
            ListaImagenes = new List<Image>();

            lvImageList.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            rbToComic.Checked = true;
        }


        private void btAbrirPDF_Click(object sender, EventArgs e)
        {
            ofdAbrirPDF.Filter = "PDF Files|*.pdf";
            ofdAbrirPDF.Title = "Select a PDF File";
            ofdAbrirPDF.FileName = "";
            ofdAbrirPDF.InitialDirectory = datosPrograma.UltimoPath;

            if (ofdAbrirPDF.ShowDialog() == DialogResult.OK)
            {
                lbFile.Text = ofdAbrirPDF.FileName;
                datosPrograma.UltimoPath = Path.GetDirectoryName(ofdAbrirPDF.FileName);
                nombreArchivoPDF = Path.GetFileName(ofdAbrirPDF.FileName);

                Lanzar_Carga(ofdAbrirPDF.FileName);
            }
        }

        private void lvImageList_ItemActivate(object sender, EventArgs e)
        {
            pbLoadImage.Image = (Image)lvImageList.SelectedItems[0].Tag;
        }
        
        #endregion

        #region METODOS PROPIOS

        private void Lanzar_Carga(string rutaArchivo)
        {
            objetoPDF = new Pdf2Comic_PdfExtract(rutaArchivo);
            lbNumberPages.Text = objetoPDF.miPDF.PdfLeido.NumberOfPages.ToString();

            //Llenamos el ListView
            ListaImagenes = objetoPDF.miPDF.Devolver_Imagenes();

            for (int cont = 0; cont < ListaImagenes.Count - 1; cont++ )
                //foreach (var imagen in ListaImagenes)
            {
                ListViewItem item = new ListViewItem();
                item.Text = Path.GetFileNameWithoutExtension(nombreArchivoPDF) + "_" + cont;
                item.Tag = ListaImagenes[cont];
                lvImageList.Items.Add(item);
            }

            lvImageList.Refresh();

        }

        private void Guardar_Imagenes()
        {
            int i = 1;
            foreach (var imagen in objetoPDF.miPDF.Devolver_Diccionario_Imagenes())
            {
                string nombreImagen = Path.GetFileNameWithoutExtension(nombreArchivoPDF) + i + imagen.Value;
                string rutaImagen = Path.Combine(new string[] { datosPrograma.UltimoPath, nombreImagen });

                System.Drawing.Imaging.ImageFormat formatoImagen = null;
                if (imagen.Value.Contains("jpg"))
                    formatoImagen = System.Drawing.Imaging.ImageFormat.Jpeg;

                imagen.Key.Save(rutaImagen, formatoImagen);
                i++;

                listaPathImagenes.Add(rutaImagen);
            }
        }

        private void Comprimir_Imagenes()
        {
            Pdf2Comic_ImageCompress compresion = new Pdf2Comic_ImageCompress(listaPathImagenes, Path.GetFileNameWithoutExtension(nombreArchivoPDF));
            compresion.Comprimir(Path.Combine(new string[] { datosPrograma.UltimoPath, Path.GetFileNameWithoutExtension(nombreArchivoPDF) + ".zip" }));
        }

        #endregion
        

        private void btImagen_Click(object sender, EventArgs e)
        {
            Guardar_Imagenes();
        }

        private void btCBZ_Click(object sender, EventArgs e)
        {
            Guardar_Imagenes();
            Comprimir_Imagenes();
            //Borrar_Imagenes();
        }


    }
}

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

        public Pdf2Comic_PdfExtract objetoPDF;
        public string pathArchivo;
        public List<Image> ListaImagenes;
        public string nombreArchivoPDF;

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
            ListaImagenes = new List<Image>();

            lvImageList.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            rbToComic.Checked = true;
        }


        private void btAbrirPDF_Click(object sender, EventArgs e)
        {
            ofdAbrirPDF.Filter = "PDF Files|*.pdf";
            ofdAbrirPDF.Title = "Select a PDF File";
            ofdAbrirPDF.FileName = "";

            if (ofdAbrirPDF.ShowDialog() == DialogResult.OK)
            {
                lbFile.Text = ofdAbrirPDF.FileName;
                pathArchivo = Path.GetDirectoryName(ofdAbrirPDF.FileName);
                nombreArchivoPDF = Path.GetFileName(ofdAbrirPDF.FileName);

                Lanzar_Carga(ofdAbrirPDF.FileName);
            }
        }

        private void lvImageList_ItemActivate(object sender, EventArgs e)
        {
            pbLoadImage.Image = (Image)lvImageList.SelectedItems[0].Tag;
        }

        private void btConvert_Click(object sender, EventArgs e)
        {
            if(rbToImage.Checked)
            {
                ConvertToImage();
                return;
            }

            if(rbToComic.Checked)
            {
                ConvertToComic();
                return;
            }
        }

        private void lvImageList_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvImageList.SelectedItems.Count > 0)
                if (e.KeyCode == Keys.Delete)
                    Borrar_Items(lvImageList.SelectedItems);
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvImageList.SelectedItems.Count > 0)
                Borrar_Items(lvImageList.SelectedItems);
        }
        
        #endregion

        #region METODOS PROPIOS

        private void Lanzar_Carga(string rutaArchivo)
        {
            objetoPDF = new Pdf2Comic_PdfExtract(rutaArchivo);
            lbNumberPages.Text = objetoPDF.miPDF.PdfLeido.NumberOfPages.ToString();
            fmInformativo fmInfo = new fmInformativo("Leyendo PDF", "Cargando las Imagenes del PDF");
            fmInfo.Show();

            //Llenamos el ListView
            ListaImagenes = objetoPDF.miPDF.Devolver_Imagenes();

            for (int cont = 0; cont < ListaImagenes.Count - 1; cont++ )
            {
                ListViewItem item = new ListViewItem();
                item.Text = Path.GetFileNameWithoutExtension(nombreArchivoPDF) + "_" + cont;
                item.Tag = ListaImagenes[cont];
                lvImageList.Items.Add(item);
            }

            lvImageList.Refresh();

            ListaImagenes.Clear();

            fmInfo.Close();
        }

        private void Guardar_Imagenes()
        {
            fmInformativo fmInfo = new fmInformativo("Extranyendo Imagenes", "Extrayendo las Imagenes del PDF");
            fmInfo.Show();

            foreach(ListViewItem item in lvImageList.Items)
            {
                Image imagen = (Image)item.Tag;
                ImageFormat formatoImagen = imagen.RawFormat;
                string nombreImagen = item.Text + Devolver_Extension(formatoImagen);
                string rutaImagen = Path.Combine(new string[] { pathArchivo, nombreImagen });
                item.Tag = rutaImagen;

                imagen.Save(rutaImagen, formatoImagen);
                imagen.Dispose();
            }

            fmInfo.Close();
        }

        private void Comprimir_Imagenes()
        {
            fmInformativo fmInfo = new fmInformativo("Construyendo Comic", "Comprimiendo las Imagenes y Construyendo el Comic");
            fmInfo.Show();

            List<string> paths = new List<string>();
            foreach(ListViewItem item in lvImageList.Items)
            {
                paths.Add(item.Tag.ToString());
            }
            
            //Comprimimos las imagenes
            Pdf2Comic_ImageCompress compresion = new Pdf2Comic_ImageCompress(paths, Path.GetFileNameWithoutExtension(nombreArchivoPDF));
            compresion.Comprimir(Path.Combine(new string[] { pathArchivo, Path.GetFileNameWithoutExtension(nombreArchivoPDF) + ".zip" }));
            
            //Renombramos el archivo zip
            string antiguoPath = Path.Combine(new string[] { pathArchivo, Path.GetFileNameWithoutExtension(nombreArchivoPDF) + ".zip" });
            string nuevoPath = Path.Combine(new string[] { pathArchivo, Path.GetFileNameWithoutExtension(nombreArchivoPDF) + ".cbz" });
            File.Move(antiguoPath, nuevoPath);

            //Borramos las images del disco duro
            foreach(string path in paths)
            {
                File.Delete(path);
            }

            fmInfo.Close();
        }

        private string Devolver_Extension(ImageFormat formato)
        {
            if (formato.Equals(ImageFormat.Jpeg))
                return ".jpg";

            if (formato.Equals(ImageFormat.Bmp))
                return ".bmp";

            if (formato.Equals(ImageFormat.Gif))
                return ".gif";

            if (formato.Equals(ImageFormat.Png))
                return ".png";

            return null;
        }

        private void ConvertToImage()
        {
            Guardar_Imagenes();
        }

        private void ConvertToComic()
        {
            Guardar_Imagenes();
            Comprimir_Imagenes();
            //Borrar_Imagenes();
        }

        private void Borrar_Items(ListView.SelectedListViewItemCollection itemsBorrar)
        {
            foreach (ListViewItem item in itemsBorrar)
            {
                lvImageList.Items.Remove(item);
            }
        }

        #endregion
    }
}

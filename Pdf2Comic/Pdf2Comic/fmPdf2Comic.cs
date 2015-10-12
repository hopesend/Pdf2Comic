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

        void objetoPDF_StopReadPdf(object sender, Pdf2Comic_PdfExtract.StopReadEventArgs e)
        {
            pbBarraProgreso.Visible = false;
        }

        void objetoPDF_StartReadPdf(object sender, Pdf2Comic_PdfExtract.StartReadEventArgs e)
        {
            pbBarraProgreso.Visible = true;
            pbBarraProgreso.Maximum = e.totalImages;
            pbBarraProgreso.Step = 1;
            pbBarraProgreso.Minimum = 1;
        }

        void objetoPDF_NewImageRead(object sender, Pdf2Comic_PdfExtract.GetImagesPdfEventArgs e)
        {
            pbBarraProgreso.PerformStep();
        }

        void compresion_NewElementCompress(object sender, Pdf2Comic_ImageCompress.CompressElementEventArgs e)
        {
            pbBarraProgreso.PerformStep();
        }

        void compresion_StopCompressElements(object sender, Pdf2Comic_ImageCompress.StopCompressEventArgs e)
        {
            pbBarraProgreso.Visible = false;
            lbStep.Visible = false;
        }

        void compresion_StartCompressElements(object sender, Pdf2Comic_ImageCompress.StartCompressEventArgs e)
        {
            pbBarraProgreso.Visible = true;
            pbBarraProgreso.Maximum = e.totalElements;
            pbBarraProgreso.Step = 1;
            pbBarraProgreso.Minimum = 1;

            lbStep.Text = "Compress Images...";
            lbStep.Visible = true;
        }

        private void btAbrirPDF_Click(object sender, EventArgs e)
        {
            ofdAbrirPDF.Filter = "PDF Files|*.pdf";
            ofdAbrirPDF.Title = "Select a PDF File";
            ofdAbrirPDF.FileName = "";

            if (ofdAbrirPDF.ShowDialog() == DialogResult.OK)
            {
                if (pbLoadImage.Image != null)
                    pbLoadImage.Image = pbLoadImage.InitialImage;

                lvImageList.Items.Clear();
                lvImageList.Refresh();

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
            objetoPDF = new Pdf2Comic_PdfExtract();
            
            objetoPDF.StartReadPdf += objetoPDF_StartReadPdf;
            objetoPDF.StopReadPdf += objetoPDF_StopReadPdf;
            objetoPDF.NewImageRead += objetoPDF_NewImageRead;

            objetoPDF.LanzarExtraccion(rutaArchivo);
            lbNumberPages.Text = objetoPDF.miPDF.PdfLeido.NumberOfPages.ToString();

            //Llenamos el ListView
            ListaImagenes = objetoPDF.miPDF.Devolver_Imagenes();

            for (int cont = 0; cont < ListaImagenes.Count; cont++ )
            {
                ListViewItem item = new ListViewItem();
                item.Text = Path.GetFileNameWithoutExtension(nombreArchivoPDF) + "_" + (cont+1).ToString("0#");
                item.Tag = ListaImagenes[cont];
                lvImageList.Items.Add(item);
            }

            lvImageList.Refresh();

            ListaImagenes.Clear();
        }

        private void Guardar_Imagenes()
        {
            pbBarraProgreso.Visible = true;
            pbBarraProgreso.Minimum = 1;
            pbBarraProgreso.Step = 1;
            pbBarraProgreso.Maximum = lvImageList.Items.Count;

            lbStep.Text = "Saving Images...";
            lbStep.Visible = true;

            foreach(ListViewItem item in lvImageList.Items)
            {
                Image imagen = (Image)item.Tag;
                ImageFormat formatoImagen = imagen.RawFormat;
                string nombreImagen = item.Text + Devolver_Extension(formatoImagen);
                string rutaImagen = Path.Combine(new string[] { pathArchivo, nombreImagen });
                item.Tag = rutaImagen;

                imagen.Save(rutaImagen, formatoImagen);
                imagen.Dispose();

                pbBarraProgreso.PerformStep();
            }

            lbStep.Visible = false;
            pbBarraProgreso.Visible = false;
        }

        private void Comprimir_Imagenes()
        {
            List<string> paths = new List<string>();
            foreach(ListViewItem item in lvImageList.Items)
            {
                paths.Add(item.Tag.ToString());
            }
            
            //Comprimimos las imagenes
            Pdf2Comic_ImageCompress compresion = new Pdf2Comic_ImageCompress(paths, Path.GetFileNameWithoutExtension(nombreArchivoPDF));
            compresion.StartCompressElements += compresion_StartCompressElements;
            compresion.StopCompressElements += compresion_StopCompressElements;
            compresion.NewElementCompress += compresion_NewElementCompress;

            compresion.Comprimir(Path.Combine(new string[] { pathArchivo, Path.GetFileNameWithoutExtension(nombreArchivoPDF) + ".zip" }));
            
            //Renombramos el archivo zip
            string antiguoPath = Path.Combine(new string[] { pathArchivo, Path.GetFileNameWithoutExtension(nombreArchivoPDF) + ".zip" });
            string nuevoPath = Path.Combine(new string[] { pathArchivo, Path.GetFileNameWithoutExtension(nombreArchivoPDF) + ".cbz" });
            
            if(File.Exists(nuevoPath))
            {
                if(MessageBox.Show("The File Exist, ¿Overwrite?", "Attention!!!",  MessageBoxButtons.YesNo) == DialogResult.OK)
                {
                    File.Delete(nuevoPath);
                    File.Move(antiguoPath, nuevoPath);
                }
            }
            else
                File.Move(antiguoPath, nuevoPath);

            //Borramos las images del disco duro
            foreach(string path in paths)
            {
                File.Delete(path);
            }
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

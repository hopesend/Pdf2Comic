using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pdf2Comic
{
    public partial class fmInformativo : Form
    {
        public fmInformativo()
        {
            InitializeComponent();
        }

        public fmInformativo(string CabeceraFormulario, string Cabecera)
        {
            InitializeComponent();

            this.Text = CabeceraFormulario;
            this.lbTitulo.Text = Cabecera;
        }
    }
}

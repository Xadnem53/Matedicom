using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Matedicom
{
    public partial class DemostracionFormulaAngulo : Form
    {
        public DemostracionFormulaAngulo()
        {
            InitializeComponent();
            this.Location = new Point(580, 0);
        }



        private void btCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
            

    }
}

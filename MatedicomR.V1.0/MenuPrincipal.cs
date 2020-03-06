using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Matedicom
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void AlgebraLineal_Click(object sender, EventArgs e)
        {   
            // Llamar a la solucion Algebra lineal y cerrar el menu principal
            string ruta = Directory.GetCurrentDirectory();
            ruta = ruta.Substring(0,ruta.LastIndexOf("Matedicom"));
            System.Diagnostics.Process.Start(ruta + "AlgebraLineal\\bin\\Release\\AlgebraLineal.exe");
            this.Close();
        }

        private void Vectores_Click(object sender, EventArgs e)
        {
            // Llamar a la solucion Vectores y cerrar el menu principal
            string ruta = Directory.GetCurrentDirectory();
            ruta = ruta.Substring(0, ruta.LastIndexOf("Matedicom"));
            System.Diagnostics.Process.Start(ruta + "Vectores\\bin\\Release\\Vectores.exe");
            //System.Diagnostics.Process.Start(@"C:\Users\Jose Antonio\Documents\Visual Studio 2013\Proyecto\MatedicomR.V1.0\Vectores\bin\Debug\Vectores.exe");
            this.Close();
        }

        private void Algebra_Click(object sender, EventArgs e)
        {
            // Llamar a la solucion Algebra y cerrar el menu principal
            string ruta = Directory.GetCurrentDirectory();
            ruta = ruta.Substring(0, ruta.LastIndexOf("Matedicom"));
            System.Diagnostics.Process.Start(ruta + "Algebra\\bin\\Release\\Algebra.exe");
            //System.Diagnostics.Process.Start(@"C:\Users\Jose Antonio\Documents\Visual Studio 2013\Proyecto\MatedicomR.V1.0\Vectores\bin\Debug\Vectores.exe");
            this.Close();
        }


        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.Environment.Exit(0);
        }


	private void Maximizado(object sender, EventArgs e)
	{
	   foreach(Control c in this.Controls)
	    if(c.Name == "btSalir")
		c.Location = new Point(this.ClientSize.Width-200 , c.Location.Y);
	     else if(c.Name == "Titulo")
		c.Location = new Point((this.ClientSize.Width-150)/2 , c.Location.Y);
	    else
		c.Location = new Point((this.ClientSize.Width-568)/2 , c.Location.Y);
	}


    }
}

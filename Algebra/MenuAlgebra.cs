using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matedicom;

namespace Algebra
{
    public partial class MenuAlgebra : Form
    {
        public MenuAlgebra()
        {
            InitializeComponent();
        }

        private void btAtras_Click(object sender, EventArgs e)
        {
            // Llamar al menu principal
            string ruta = Directory.GetCurrentDirectory();
            ruta = ruta.Substring(0, ruta.LastIndexOf("Matedicom"));
            System.Diagnostics.Process.Start(@ruta + "\\MatedicomR.V1.0\\bin\\Debug\\MatedicomR.V1.0.exe");
            this.Close();
        }
        private void btCerrar_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Dispose();
            else
                this.Dispose();
            System.Environment.Exit(0);
        }

        private void btSumaResta_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btSumaResta.BackColor = Color.Aquamarine;
            btAceptar.Click += AceptarSumaResta_Click;
            btAceptar.Click -= AceptarFactorizacion_Click;
            btAceptar.Click -= AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarDecimalesAFraccion_Click;
            btAceptar.Click -= AceptarImaginarios_Click;
             btAceptar.Click -= AceptarOperacionesImaginarios_Click;
		 btAceptar.Click -= AceptarModuloArgumento_Click;
		 btAceptar.Click -= AceptarCombinatoria_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarSumaResta_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            SumaRestaPolinomios sumaresta = new SumaRestaPolinomios(directo);
            sumaresta.Size = new Size(1392, 703);
            sumaresta.Show();
            sumaresta.Owner = this.Owner;
            this.Hide();
        }

        private void btMultiplicacionDivision_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btMultiplicacionDivision.BackColor = Color.Aquamarine;
            btAceptar.Click += AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarPotencia_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarDecimalesAFraccion_Click;
            btAceptar.Click -= AceptarImaginarios_Click;
            btAceptar.Click -= AceptarOperacionesImaginarios_Click;
		 btAceptar.Click -= AceptarModuloArgumento_Click;
		 btAceptar.Click -= AceptarCombinatoria_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarMultiplicacionDivision_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            MultiplicacionDivision multiplicaciondivision = new MultiplicacionDivision(directo);
            multiplicaciondivision.Size = new Size(1392, 703);
            multiplicaciondivision.Show();
            multiplicaciondivision.Owner = this.Owner;
            this.Hide();
        }

        private void btPotencia_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btPotencia.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarOperacionesImaginarios_Click;
            btAceptar.Click -= AceptarImaginarios_Click;
            btAceptar.Click += AceptarPotencia_Click;
            btAceptar.Click -= AceptarDecimalesAFraccion_Click;
            btAceptar.Click -= AceptarFactorizacion_Click;
		 btAceptar.Click -= AceptarModuloArgumento_Click;
		 btAceptar.Click -= AceptarCombinatoria_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarPotencia_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            Potencia potencia = new Potencia(directo);
            potencia.Size = new Size(1392, 703);
            potencia.Show();
            potencia.Owner = this.Owner;
            this.Hide();
        }

        private void btFactorizacion_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btFactorizacion.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarPotencia_Click;
            btAceptar.Click -= AceptarDecimalesAFraccion_Click;
            btAceptar.Click -= AceptarImaginarios_Click;
            btAceptar.Click -= AceptarOperacionesImaginarios_Click;
		 btAceptar.Click -= AceptarModuloArgumento_Click;
		 btAceptar.Click -= AceptarCombinatoria_Click;
            btAceptar.Click += AceptarFactorizacion_Click;

            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarFactorizacion_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            Factorizacion factorizacion = new Factorizacion(directo);
            factorizacion.Size = new Size(1392, 703);
            factorizacion.Show();
            factorizacion.Owner = this.Owner;
            this.Hide();
        }

        private void btDecimalesAFraccion_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btDecimalesAFraccion.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarPotencia_Click;
            btAceptar.Click -= AceptarFactorizacion_Click;
            btAceptar.Click -= AceptarImaginarios_Click;
            btAceptar.Click -= AceptarOperacionesImaginarios_Click;
		 btAceptar.Click -= AceptarModuloArgumento_Click;
		 btAceptar.Click -= AceptarCombinatoria_Click;
            btAceptar.Click += AceptarDecimalesAFraccion_Click;

            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarDecimalesAFraccion_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            DecimalesAFraccion decimalesafraccion = new DecimalesAFraccion(directo);
            decimalesafraccion.Size = new Size(1392, 703);
            decimalesafraccion.Show();
            decimalesafraccion.Owner = this.Owner;
            this.Hide();
        }

        private void btImaginarios_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btImaginarios.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarPotencia_Click;
            btAceptar.Click -= AceptarFactorizacion_Click;
            btAceptar.Click -= AceptarDecimalesAFraccion_Click;
            btAceptar.Click -= AceptarOperacionesImaginarios_Click;
	     btAceptar.Click -= AceptarModuloArgumento_Click;
		 btAceptar.Click -= AceptarCombinatoria_Click;
            btAceptar.Click += AceptarImaginarios_Click;

            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarImaginarios_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            Imaginarios imaginarios = new Imaginarios(directo);
            imaginarios.Size = new Size(1392, 703);
            imaginarios.Show();
            imaginarios.Owner = this.Owner;
            this.Hide();
        }

          private void btOperacionesImaginarios_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btOperacionesImaginarios.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarPotencia_Click;
            btAceptar.Click -= AceptarFactorizacion_Click;
            btAceptar.Click -= AceptarDecimalesAFraccion_Click;
            btAceptar.Click -= AceptarOperacionesImaginarios_Click;
	     btAceptar.Click -= AceptarModuloArgumento_Click;
		 btAceptar.Click -= AceptarCombinatoria_Click;
            btAceptar.Click += AceptarOperacionesImaginarios_Click;

            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarOperacionesImaginarios_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            OperacionesImaginarios operacionesimaginarios = new OperacionesImaginarios(directo);
            operacionesimaginarios.Size = new Size(1392, 703);
            operacionesimaginarios.Show();
            operacionesimaginarios.Owner = this.Owner;
            this.Hide();
        }


	
	private void btModuloArgumento_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btModuloArgumento.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarPotencia_Click;
            btAceptar.Click -= AceptarFactorizacion_Click;
            btAceptar.Click -= AceptarDecimalesAFraccion_Click;
            btAceptar.Click -= AceptarImaginarios_Click;
            btAceptar.Click -= AceptarOperacionesImaginarios_Click;
	     btAceptar.Click -= AceptarCombinatoria_Click;
	    btAceptar.Click += AceptarModuloArgumento_Click;

            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarModuloArgumento_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
             ModuloArgumento moduloargumento = new ModuloArgumento(directo);
            moduloargumento.Size = new Size(1392, 703);
            moduloargumento.Show();
            moduloargumento.Owner = this.Owner;
            this.Hide();
        }

	private void btCombinatoria_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.OliveDrab;
                }
            }
            btCombinatoria.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarMultiplicacionDivision_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarPotencia_Click;
            btAceptar.Click -= AceptarFactorizacion_Click;
            btAceptar.Click -= AceptarDecimalesAFraccion_Click;
            btAceptar.Click -= AceptarImaginarios_Click;
            btAceptar.Click -= AceptarOperacionesImaginarios_Click;
	    btAceptar.Click -= AceptarModuloArgumento_Click;
	    btAceptar.Click += AceptarCombinatoria_Click;

            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarCombinatoria_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
             Combinatoria combinatoria = new Combinatoria(directo);
            combinatoria.Size = new Size(1392, 703);
            combinatoria.Show();
            combinatoria.Owner = this.Owner;
            this.Hide();
        }

    }
}

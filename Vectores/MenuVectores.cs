using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matedicom
{
    public partial class MenuVectores : Form
    {
        public MenuVectores()
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


        private void btDistancia_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btDistancia.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= btCircunferencia_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;

            btAceptar.Click += AceptarDistancia_Click;

            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarDistancia_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            Distancia distancia = new Distancia(directo);
            distancia.Size = new Size(1392, 703);
            distancia.Show();
            distancia.Owner = this.Owner;
            this.Hide();
        }

        private void btPuntoMedio_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btPuntoMedio.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= btCircunferencia_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarPuntoMedio_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarPuntoMedio_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            PuntoMedio puntomedio = new PuntoMedio(directo);
            puntomedio.Size = new Size(1392, 703);
            puntomedio.Show();
            puntomedio.Owner = this.Owner;
            this.Hide();
        }

        private void btCircunferencia_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btCircunferencia.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarCircunferencia_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarCircunferencia_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
           
            Circunferencia circunferencia = new Circunferencia(directo);
            circunferencia.Size = new Size(1392, 703);
            circunferencia.Show();
            circunferencia.Owner = this.Owner;
            this.Hide();
            
        }

        private void btVectorUnitario_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btUnitario.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarUnitario_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarUnitario_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            VectorUnitario unitario = new VectorUnitario(directo);
            unitario.Size = new Size(1392, 703);
            unitario.Show();
            unitario.Owner = this.Owner;
            this.Hide();
        }


        private void btSumaResta_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btSumaResta.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarSumaResta_Click;
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
            SumaYResta sumaresta = new SumaYResta(directo);
            sumaresta.Size = new Size(1392, 703);
            sumaresta.Show();
            sumaresta.Owner = this.Owner;
            this.Hide();
        }


        private void btProductoPunto_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btProductoPunto.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarProductoPunto_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarProductoPunto_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            ProductoPunto productopunto = new ProductoPunto(directo);
            productopunto.Size = new Size(1392, 703);
            productopunto.Show();
            productopunto.Owner = this.Owner;
            this.Hide();
        }

        private void btAngulo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btAngulo.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarAngulo_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarAngulo_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            Angulo angulo = new Angulo(directo);
            angulo.Size = new Size(1392, 703);
            angulo.Show();
            angulo.Owner = this.Owner;
            this.Hide();
        }

        private void btProyeccion_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btProyeccion.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarProyeccion_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarProyeccion_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            Proyeccion proyeccion = new Proyeccion(directo);
            proyeccion.Size = new Size(1392, 703);
            proyeccion.Show();
            proyeccion.Owner = this.Owner;
            this.Hide();
        }


        private void btProductoCruz_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btProductoCruz.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarProductoCruz_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarProductoCruz_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            ProductoCruz productocruz = new ProductoCruz(directo);
            productocruz.Size = new Size(1392, 703);
            productocruz.Show();
            productocruz.Owner = this.Owner;
            this.Hide();
        }

        private void btVolumen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btVolumen.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click += AceptarVolumen_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarVolumen_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            Volumen volumen = new Volumen(directo);
            volumen.Size = new Size(1392, 703);
            volumen.Show();
            volumen.Owner = this.Owner;
            this.Hide();
        }

        private void btRecta_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btRecta.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click += AceptarRecta_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarRecta_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            
            this.Hide();
            RectaForm recta = new RectaForm(directo);
            recta.Size = new Size(1392, 703);
            recta.Owner = this.Owner;
            recta.Show();
             
        }

        private void btPuntoSobreLaRecta_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btPuntoSobreLaRecta.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click += AceptarPuntoSobreRecta_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarPuntoSobreRecta_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
           
            PuntoSobreRecta puntosobrerecta = new PuntoSobreRecta(directo);
            puntosobrerecta.Size = new Size(1392, 703);
            puntosobrerecta.Show();
            puntosobrerecta.Owner = this.Owner;
            this.Hide();
            
        }


        private void btInterseccionRectas_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btInterseccionRectas.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click += AceptarInterseccionRectas_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarInterseccionRectas_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            
            InterseccionRectas interseccionrectas = new InterseccionRectas(directo);
            interseccionrectas.Size = new Size(1392, 703);
            interseccionrectas.Show();
            interseccionrectas.Owner = this.Owner;
            this.Hide();
           
        }


        private void btPlano_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btPlano.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click += AceptarPlano_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarPlano_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            
            EcuacionPlano plano = new EcuacionPlano(directo);
            plano.Size = new Size(1392, 703);
            plano.Show();
            plano.Owner = this.Owner;
            this.Hide();
             
        }


        private void btPlanoDosRectas_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btPlanoDosRectas.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click += AceptarPlanoDosRectas_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarPlanoDosRectas_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            
            PlanoDosRectas planodosrectas = new PlanoDosRectas(directo);
            planodosrectas.Size = new Size(1392, 703);
            planodosrectas.Show();
            planodosrectas.Owner = this.Owner;
            this.Hide();
             
        }


        private void btDistanciaPuntoRecta_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btDistanciaPuntoRecta.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click += AceptarDistanciaPuntoRecta_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarDistanciaPuntoRecta_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            
            DistanciaPuntoRecta distanciapuntorecta = new DistanciaPuntoRecta(directo);
            distanciapuntorecta.Size = new Size(1392, 703);
            distanciapuntorecta.Show();
            distanciapuntorecta.Owner = this.Owner;
            this.Hide();
             
        }


        private void btInterseccionRectaPlano_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btInterseccionRectaPlano.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click += AceptarInterseccionRectaPlano_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarInterseccionRectaPlano_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            
            InterseccionRectaPlano interseccionrectaplano = new InterseccionRectaPlano(directo);
            interseccionrectaplano.Size = new Size(1392, 703);
            interseccionrectaplano.Show();
            interseccionrectaplano.Owner = this.Owner;
            this.Hide();
             
        }


        private void btInterseccionPlanos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btInterseccionPlanos.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarDistanciaPuntoPlano_Click;
            btAceptar.Click += AceptarInterseccionPlanos_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarInterseccionPlanos_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            
            InterseccionPlanos interseccionplanos = new InterseccionPlanos(directo);
            interseccionplanos.Size = new Size(1392, 703);
            interseccionplanos.Show();
            interseccionplanos.Owner = this.Owner;
            this.Hide();
             
        }


        private void btDistanciaPuntoPlano_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Button && this.Controls[i].Name != "btAtras" && this.Controls[i].Name != "btCerrar" && this.Controls[i].Name != "btAceptar")
                {
                    this.Controls[i].BackColor = Color.DarkGoldenrod;
                }
            }
            btDistanciaPuntoPlano.BackColor = Color.Aquamarine;
            btAceptar.Click -= AceptarDistancia_Click;
            btAceptar.Click -= AceptarPuntoMedio_Click;
            btAceptar.Click -= AceptarCircunferencia_Click;
            btAceptar.Click -= AceptarUnitario_Click;
            btAceptar.Click -= AceptarSumaResta_Click;
            btAceptar.Click -= AceptarProductoPunto_Click;
            btAceptar.Click -= AceptarAngulo_Click;
            btAceptar.Click -= AceptarProyeccion_Click;
            btAceptar.Click -= AceptarProductoCruz_Click;
            btAceptar.Click -= AceptarVolumen_Click;
            btAceptar.Click -= AceptarRecta_Click;
            btAceptar.Click -= AceptarPuntoSobreRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectas_Click;
            btAceptar.Click -= AceptarPlano_Click;
            btAceptar.Click -= AceptarPlanoDosRectas_Click;
            btAceptar.Click -= AceptarDistanciaPuntoRecta_Click;
            btAceptar.Click -= AceptarInterseccionRectaPlano_Click;
            btAceptar.Click -= AceptarInterseccionPlanos_Click;
            btAceptar.Click += AceptarDistanciaPuntoPlano_Click;
            btAceptar.Show();
            rbDirecta.Show();
            rbPasoAPaso.Show();
        }

        private void AceptarDistanciaPuntoPlano_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Hide();

            bool directo = rbDirecta.Checked;
            if (!rbDirecta.Checked && !rbPasoAPaso.Checked)
                return;
            
            DistanciaPuntoPlano distanciapuntoplano = new DistanciaPuntoPlano(directo);
            distanciapuntoplano.Size = new Size(1392, 703);
            distanciapuntoplano.Show();
            distanciapuntoplano.Owner = this.Owner;
            this.Hide();
             
        }
    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Punto_y_Vector;
using Recta_y_plano;
using Matematicas;


namespace Matedicom
{
    public partial class Angulo : FormularioBase
    {
      new  bool directa; // Tipo de resolucion
        EspacioIsometrico ventanagrafica; // Ventana grafica
        Punto punto1; // Punto final del primer vector
        Punto punto2; // Punto final del segundo vector
        Vector vector1; // Primer vector
        Vector vector2; // Segundo vector 
        new bool defecto = false; // Es true si se pulsa el boton de valores por defecto
        Button btDemostracion; // Para mostrar la demostracion de la formula del angulo entre vectores

        public Angulo(bool direct)
        {
            directa = direct;
        }


        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = "Ángulo entre vectores.";
            ventanagrafica = new EspacioIsometrico(830, 640, new Point(5, 80));
            Controls.Add(ventanagrafica.Ventana);
            lbExplicacion.Show();
            lbExplicacion.Text = " Introducir las componentes de los vectores ( enteros o racionales ) .\n\n( O pulse el botón [E] para ejemplo con valores por omisión.";
            lbExplicacion.Width = btSalir.Location.X;
            tbPunto1X.Focus();
            pnDatos.Show();
            pnDatos.Location = new Point(ventanagrafica.Ventana.Location.X + ventanagrafica.Ventana.Width, ventanagrafica.Ventana.Location.Y);
            lbRecta1.Hide();
            lbRecta2.Hide();
            lbPuntoDePaso.Hide();
            lbVectorNormal.Hide();
            tbPunto3X.Hide();
            tbPunto3Y.Hide();
            tbPunto3Z.Hide();
            tbPunto4X.Hide();
            tbPunto4Y.Hide();
            tbPunto4Z.Hide();
            label35.Text = "i";
            label36.Text = "j";
            label37.Text = "k";
            lbRotuloA.Text = "Vector 1:";
            lbRotuloA.AutoSize = true;
            lbRotuloA.BackColor = Color.DarkOrange;
            lbRotuloA.Location = new Point(lbRotuloA.Location.X - 5, lbRotuloA.Location.Y);
            lbRotuloB.Text = "Vector 2:";
            lbRotuloB.AutoSize = true;
            lbRotuloB.Location = new Point(lbRotuloB.Location.X - 5, lbRotuloB.Location.Y);
            lbRotuloB.BackColor = Color.SlateBlue;
            btDefecto.Location = new Point(pnDatos.Location.X + pnDatos.Width, pnDatos.Location.Y);
            labeli2.Hide();
            labelj2.Hide();
            labelk2.Hide();
            pnDatos.Height = tbPunto2X.Location.Y + tbPunto2X.Height + 15;
            tbPunto1X.Focus();
            tbPunto1X.KeyPress += Cajas_KeyPress;
            tbPunto1Y.KeyPress += Cajas_KeyPress;
            tbPunto1Z.KeyPress += Cajas_KeyPress;
            tbPunto2X.KeyPress += Cajas_KeyPress;
            tbPunto2Y.KeyPress += Cajas_KeyPress;
            tbPunto2Z.KeyPress += Cajas_KeyPress;
            lbTituloAnguloX.Location = new Point(ventanagrafica.Ventana.Width + 10, pnDatos.Location.Y + pnDatos.Height + 5);
            lbAnguloX.Location = new Point(lbTituloAnguloX.Location.X + lbTituloAnguloX.Width + 10, lbTituloAnguloX.Location.Y);
            sbAngulox.Location = new Point(lbTituloAnguloX.Location.X, lbAnguloX.Location.Y + lbAnguloX.Height + 5);
            lbTituloAnguloY.Location = new Point(ventanagrafica.Ventana.Width + 10, sbAngulox.Location.Y + sbAngulox.Height + 5);
            lbAnguloY.Location = new Point(lbTituloAnguloY.Location.X + lbTituloAnguloY.Width + 10, lbTituloAnguloY.Location.Y);
            sbAnguloY.Location = new Point(lbTituloAnguloY.Location.X, lbAnguloY.Location.Y + lbAnguloY.Height + 5);
            lbTituloAnguloZ.Location = new Point(ventanagrafica.Ventana.Width + 10, sbAnguloY.Location.Y + sbAnguloY.Height + 5);
            lbAnguloZ.Location = new Point(lbTituloAnguloZ.Location.X + lbTituloAnguloZ.Width + 5, lbTituloAnguloZ.Location.Y);
            sbAnguloZ.Location = new Point(lbTituloAnguloZ.Location.X, lbAnguloZ.Location.Y + lbAnguloZ.Height + 5);
            btContinuar.Click += btContinuar_Click;
            pnZoom.Location = new Point(sbAnguloZ.Location.X, sbAnguloZ.Location.Y + sbAnguloZ.Height + 5);
            btIsometrica.Location = new Point(lbTituloAnguloX.Location.X + sbAngulox.Width + 10, lbTituloAnguloX.Location.Y);
            btAlzado.Location = new Point(btIsometrica.Location.X, btIsometrica.Location.Y + btIsometrica.Height + 3);
            btPerfil.Location = new Point(btAlzado.Location.X, btAlzado.Location.Y + btAlzado.Height + 3);
            btPlanta.Location = new Point(btPerfil.Location.X, btPerfil.Location.Y + btPerfil.Height + 3);
            sbAngulox.ValueChanged += RotarX;
            sbAnguloY.ValueChanged += RotarY;
            sbAnguloZ.ValueChanged += RotarZ;
            btZoomMas.Click += ZoomMas;
            btZoomMenos.Click += ZoomMenos;
            btArriba.Click += arriba;
            btAbajo.Click += abajo;
            btDerecha.Click += derecha;
            btIzquierda.Click += Izquierda;
            btCentrar.Click += centrar;
            btIsometrica.Click += VistaIsometrica;
            btAlzado.Click += VistaAlzado;
            btPerfil.Click += VistaPerfil;
            btPlanta.Click += VistaPlanta;
            btAjustar.Click += AjustarZoom;
            btContinuar.Location = new Point(pnZoom.Location.X, pnZoom.Location.Y + pnZoom.Height + 5);

            // Controles exclusivos de esta clase
            btDemostracion = new Button();
            Controls.Add(btDemostracion);
            btDemostracion.BackColor = Color.Gray;
            btDemostracion.Text = "Demostración";
            btDemostracion.Font = btContinuar.Font;
            btDemostracion.AutoSize = true;
            btDemostracion.Visible = false;
         //   btDemostracion.Location = new Point(btContinuar.Location.X + btContinuar.Width + 50, btContinuar.Location.Y);
            btDemostracion.Location = new Point(695, 55);
            btDemostracion.Click += btDemostracion_Click;
        }
        /// <summary>
        ///  
        ///SALE DEL PROGRAMA Y LIBERA TODOS LOS RECURSOS
        /// 
        /// </summary>
        private void btCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.Environment.Exit(0);
        }

        /// <summary>
        ///  
        /// CIERRA EL FORMULARIO ACTUAL Y ABRE UN NUEVO MENU DE ALGEBRA LINEAL
        /// 
        /// </summary>

        public override void btSalir_Click(object sender, EventArgs e)
        {
            MenuVectores menuvectores = new MenuVectores();
            menuvectores.Show();
            this.Dispose();
        }
        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            Angulo nuevo = new Angulo(directa);
            nuevo.Show();
            this.Close();
        }
        /// <summary>
        ///   
        ///  CONTROLES DE VISUALIZACION
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void RotarX(object sender, EventArgs e)
        {
            float angulogirado = sbAngulox.Value - ventanagrafica.angulogirox;
            ventanagrafica.angulogirox = sbAngulox.Value;
            angulogirado /= 57.295779513082320876798154814105F;
            ventanagrafica.GirarX(angulogirado);
            ventanagrafica.Ventana.Invalidate();
            lbAnguloX.Text = ventanagrafica.angulogirox.ToString();
        }


        private void RotarY(object sender, EventArgs e)
        {
            float angulogirado = sbAnguloY.Value - ventanagrafica.angulogiroy;
            ventanagrafica.angulogiroy = sbAnguloY.Value;
            angulogirado /= 57.295779513082320876798154814105F;
            ventanagrafica.GirarY(angulogirado);
            ventanagrafica.Ventana.Invalidate();
            lbAnguloY.Text = ventanagrafica.angulogiroy.ToString();
        }
        private void RotarZ(object sender, EventArgs e)
        {
            float angulogirado = sbAnguloZ.Value - ventanagrafica.angulogiroz;
            ventanagrafica.angulogiroz = sbAnguloZ.Value;
            angulogirado /= 57.295779513082320876798154814105F;
            ventanagrafica.GirarZ(angulogirado);
            ventanagrafica.Ventana.Invalidate();
            lbAnguloZ.Text = ventanagrafica.angulogiroz.ToString();
        }

        private void VistaIsometrica(object sender, EventArgs e)
        {
            ventanagrafica.VistaIsometrica();
            lbAnguloX.Text = ventanagrafica.angulogirox.ToString();
            sbAngulox.Value = (int)ventanagrafica.angulogirox;
            lbAnguloY.Text = ventanagrafica.angulogiroy.ToString();
            sbAnguloY.Value = (int)ventanagrafica.angulogiroy;
            lbAnguloZ.Text = ventanagrafica.angulogiroz.ToString();
            sbAnguloZ.Value = (int)ventanagrafica.angulogiroz;
        }
        private void VistaAlzado(object sender, EventArgs e)
        {
            ventanagrafica.VistaAlzado();
            lbAnguloX.Text = ventanagrafica.angulogirox.ToString();
            sbAngulox.Value = (int)ventanagrafica.angulogirox;
            lbAnguloY.Text = ventanagrafica.angulogiroy.ToString();
            sbAnguloY.Value = (int)ventanagrafica.angulogiroy;
            lbAnguloZ.Text = ventanagrafica.angulogiroz.ToString();
            sbAnguloZ.Value = (int)ventanagrafica.angulogiroz;
        }
        private void VistaPerfil(object sender, EventArgs e)
        {
            ventanagrafica.VistaPerfil();
            lbAnguloX.Text = ventanagrafica.angulogirox.ToString();
            sbAngulox.Value = (int)ventanagrafica.angulogirox;
            lbAnguloY.Text = ventanagrafica.angulogiroy.ToString();
            sbAnguloY.Value = (int)ventanagrafica.angulogiroy;
            lbAnguloZ.Text = ventanagrafica.angulogiroz.ToString();
            sbAnguloZ.Value = (int)ventanagrafica.angulogiroz;
        }
        private void VistaPlanta(object sender, EventArgs e)
        {
            ventanagrafica.VistaPlanta();
            lbAnguloX.Text = ventanagrafica.angulogirox.ToString();
            sbAngulox.Value = (int)ventanagrafica.angulogirox;
            lbAnguloY.Text = ventanagrafica.angulogiroy.ToString();
            sbAnguloY.Value = (int)ventanagrafica.angulogiroy;
            lbAnguloZ.Text = ventanagrafica.angulogiroz.ToString();
            sbAnguloZ.Value = (int)ventanagrafica.angulogiroz;
        }
        private void AjustarZoom(object sender, EventArgs e)
        {
            ventanagrafica.EstablecerEscala();
        }
        private void ZoomMas(object sender, EventArgs e)
        {
            ventanagrafica.ZoomMas();
        }
        private void ZoomMenos(object sender, EventArgs e)
        {
            ventanagrafica.ZoomMenos();
        }
        private void Izquierda(object sender, EventArgs e)
        {
            ventanagrafica.DesplazamientoIzquierda();
        }
        private void derecha(object sender, EventArgs e)
        {
            ventanagrafica.DesplazamientoDerecha();
        }
        private void arriba(object sender, EventArgs e)
        {
            ventanagrafica.DesplazamientoArriba();
        }
        private void abajo(object sender, EventArgs e)
        {
            ventanagrafica.DesplazamientoAbajo();
        }
        private void centrar(object sender, EventArgs e)
        {
            ventanagrafica.Centrar();
        }


    

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN VALORES RACIONAL O ENTEROS EN LAS CAJAS DE TEXTO DE LAS
        /// COORDENADAS DE LOS PUNTOS
        /// 
        /// </summary>
        /// 
        private void Cajas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (e.KeyChar == (char)'-')
            {
                if (caja.Text.Length == 0)
                {
                    e.Handled = false;
                }
                else
                    e.Handled = true;
            }

            else if (e.KeyChar == (char)8)
                e.Handled = false;

            else if (e.KeyChar == (char)'/')
            {
                if (caja.Text.Length == 0)
                {
                    e.Handled = true;
                    caja.Focus();
                }
                else
                {
                    if (caja.Text.IndexOf('/') > 0)
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
            }
            else if (e.KeyChar == (char)13)
            {
                if (caja.Text.Length == 0 || caja.Text == "-")
                {
                    e.Handled = true;
                    caja.Focus();
                }
                else
                {
                    if (caja == this.tbPunto1X)
                    {
                        e.Handled = true;
                        this.tbPunto1Y.Focus();
                    }
                    else if (caja == this.tbPunto1Y)
                    {
                        e.Handled = true;
                        this.tbPunto1Z.Focus();
                    }
                    else if (caja == this.tbPunto1Z)
                    {
                        e.Handled = true;
                        this.tbPunto2X.Focus();
                    }
                    else if (caja == this.tbPunto2X)
                    {
                        e.Handled = true;
                        this.tbPunto2Y.Focus();
                    }
                    else if (caja == this.tbPunto2Y)
                    {
                        e.Handled = true;
                        this.tbPunto2Z.Focus();
                    }
                    else if (caja == this.tbPunto2Z)
                    {
                        e.Handled = true;
                        this.Focus();
                        IniciarResolucion();
                        this.AcceptButton = this.btContinuar;
                    }
                }
            }
            else if (e.KeyChar == '0' || e.KeyChar == '1' || e.KeyChar == '2' || e.KeyChar == '3' || e.KeyChar == '4' || e.KeyChar == '5' ||
                        e.KeyChar == '6' || e.KeyChar == '7' || e.KeyChar == '8' || e.KeyChar == '9')
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }


        /// <summary>
        /// 
        ///  PONE EL ATRIBUTO defecto A TRUE PARA QUE SE INICIE LA RESOLUCION CON LOS VALORES POR
        ///  DEFECTO
        /// 
        /// </summary>
        /// 
        public override void btDefecto_Click(object sender, EventArgs e)
        {
            defecto = true;
            btContinuar.Show();
            IniciarResolucion();
            btDefecto.Hide();
        }

     

        /// <summary>
        /// 
        /// LLAMA AL METODO ADECUADO SEGUN EN QUE PASO SE ENCUENTRE LA RESOLUCION PASO A PASO
        /// 
        /// </summary>
        /// 
        private void btContinuar_Click(object sender, EventArgs e)
        {
                Finalizar();
        }

        /// <summary>
        /// 
        /// CONSTRUYE LOS DOS VECTORES A SUMAR O RESTAR SEGUN LOS DATOS INTRODUCIDOS EN LAS CAJAS
        /// DE LAS COMPONENTES Y LOS REPRESENTA EN PANTALLA
        /// 
        /// </summary>
        /// 
        private void IniciarResolucion()
        {
            // Mostrar los controles
            lbTituloAnguloX.Visible = true; btIsometrica.Visible = true;
            lbAnguloX.Visible = true; btAlzado.Visible = true;
            sbAngulox.Visible = true; btPerfil.Visible = true;
            lbTituloAnguloY.Visible = true; btPlanta.Visible = true;
            lbAnguloY.Visible = true;
            sbAnguloY.Visible = true;
            lbTituloAnguloZ.Visible = true;
            lbAnguloZ.Visible = true;
            sbAnguloZ.Visible = true;
            btZoomMas.Visible = true;
            btZoomMenos.Visible = true;
            lbZoomtitulo.Visible = true;
            lbAjustar.Visible = true;
            btAjustar.Visible = true;
            pnZoom.Visible = true;
            btAbajo.Visible = true;
            btArriba.Visible = true;
            btDerecha.Visible = true;
            btIzquierda.Visible = true;
            btCentrar.Visible = true;
            btContinuar.Visible = true;

            if(defecto)
            {
                punto1 = new Punto("26 10 25");
                tbPunto1X.Text = Racional.AString(punto1.X);
                tbPunto1Y.Text = Racional.AString(punto1.Y);
                tbPunto1Z.Text = Racional.AString(punto1.Z);
                punto2 = new Punto("14 30 5");
                tbPunto2X.Text = Racional.AString(punto2.X);
                tbPunto2Y.Text = Racional.AString(punto2.Y);
                tbPunto2Z.Text = Racional.AString(punto2.Z);
                ventanagrafica.Escala = 1.3F;
            }

            vector1 = new Vector(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text);
            vector2 = new Vector(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);
            punto1 = new Punto(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text);
            punto2 = new Punto(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);
            // Pintar los vectores
            ventanagrafica.PintarVector(new Punto("0 0 0"), new Punto(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text), Color.DarkOrange,7,true);
            ventanagrafica.PintarVector(new Punto("0 0 0"), new Punto(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text), Color.SlateBlue,7,true);
            btIsometrica.PerformClick();
            if(!defecto)
            btAjustar.PerformClick();
            if (!directa)
                lbExplicacion.Text = " El ángulo entre dos vectores θ, se mide sobre el plano que los contiene a ambos.\n La fórmula para calcularlo es: cos θ = ( U * V ) / ( |U| * |V| ).\n Esta fórmula se extrae de la ley de los cosenos: | W |² = | U |² + |V|² - 2 |U| * |V| * cos θ.  Pulse el botón [ Demostración ] para ver como se extrae. ";
            // Pintar el plano entre los vectores
            Punto final1 = new Punto(new Racional[] { vector1.Componentes[0] / 2, vector1.Componentes[1] / 2, vector1.Componentes[2] / 2 });
            Punto final2 = new Punto(new Racional[] { vector2.Componentes[0] / 2, vector2.Componentes[1] / 2, vector2.Componentes[2] / 2 });
            ventanagrafica.PintarSuperficie(new Punto[] { final1, final2, new Punto("0 0 0") }, Color.Chartreuse,false);
            //Pintar las letras que identifican cada vector
            Punto situacionU = Punto.PuntoMedio(new Punto("0 0 0"), new Punto(vector1.Componentes));
            PointF finalv1 = ventanagrafica.PuntoAPoint(new Punto(vector1.Componentes));
            double pendiente = finalv1.Y / finalv1.X;
            situacionU.Coordenadas[1] -= 1;
            situacionU.Coordenadas[2] -= 1;
            Punto situacionV = Punto.PuntoMedio(new Punto("0 0 0"), new Punto(vector2.Componentes));
            situacionV.Coordenadas[1] -= 1;
            situacionV.Coordenadas[2] -= 1;
            ventanagrafica.PintarString(situacionU, Color.Orange, 14, "U");
            ventanagrafica.PintarString(situacionV, Color.SlateBlue, 14, "V");
            // Mover las etiquetas para dejar sitio a los signos de multiplicar y el desarrollo de las operaciones
            label35.Location = new Point(label35.Location.X, label35.Location.Y - 15);
            label36.Location = new Point(label36.Location.X, label36.Location.Y - 15);
            label37.Location = new Point(label37.Location.X, label37.Location.Y - 15);
            lbRotuloA.Location = new Point(lbRotuloA.Location.X, lbRotuloA.Location.Y - 15);
            tbPunto1X.Location = new Point(tbPunto1X.Location.X, tbPunto1X.Location.Y - 15);
            tbPunto1Y.Location = new Point(tbPunto1Y.Location.X, tbPunto1Y.Location.Y - 15);
            tbPunto1Z.Location = new Point(tbPunto1Z.Location.X, tbPunto1Z.Location.Y - 15);
            // Mostrar las etiquetas con los signos
            label1.Show();
            label1.Text = "*";
            label1.BackColor = Color.Transparent;
            label1.Font = new Font(label1.Font.FontFamily, 15, FontStyle.Bold);
            pnDatos.Controls.Add(label1);
            label1.Location = new Point(tbPunto1X.Location.X + (tbPunto1X.Width / 2) - 5, (tbPunto1X.Location.Y + tbPunto2X.Location.Y) / 2 + 2);

            label2.Show();
            label2.Text = "*";
            label2.BackColor = Color.Transparent;
            label2.Font = new Font(label1.Font.FontFamily, 15, FontStyle.Bold);
            pnDatos.Controls.Add(label2);
            label2.Location = new Point(tbPunto1Y.Location.X + (tbPunto1Y.Width / 2) - 5, label1.Location.Y);

            label3.Show();
            label3.Text = "*";
            label3.BackColor = Color.Transparent;
            label3.Font = new Font(label1.Font.FontFamily, 15, FontStyle.Bold);
            pnDatos.Controls.Add(label3);
            label3.Location = new Point(tbPunto1Z.Location.X + (tbPunto1Z.Width / 2) - 5, label1.Location.Y);

            label4.Show();
            label4.Text = Racional.AString(vector1.Componentes[0] * vector2.Componentes[0]);
            label4.Font = new Font(label1.Font.FontFamily, 10, FontStyle.Bold);
            pnDatos.Controls.Add(label4);
            label4.Location = new Point(tbPunto1X.Location.X, tbPunto2X.Location.Y + tbPunto2X.Height);
            label4.BackColor = Color.Transparent;

            label5.Show();
            label5.BackColor = Color.Transparent;
            label5.Text = " + ";
            label5.Font = new Font(label1.Font.FontFamily, 10, FontStyle.Bold);
            pnDatos.Controls.Add(label5);
            label5.Location = new Point(tbPunto1X.Location.X + tbPunto2X.Width, label4.Location.Y);
            label4.BackColor = Color.Transparent;

            label6.Show();
            label6.Text = Racional.AString(vector1.Componentes[1] * vector2.Componentes[1]);
            label6.Font = new Font(label1.Font.FontFamily, 10, FontStyle.Bold);
            pnDatos.Controls.Add(label6);
            label6.Location = new Point(tbPunto1Y.Location.X, label5.Location.Y);
            label6.BackColor = Color.Transparent;

            label7.Show();
            label7.BackColor = Color.Transparent;
            label7.Text = " + ";
            label7.Font = new Font(label1.Font.FontFamily, 10, FontStyle.Bold);
            pnDatos.Controls.Add(label7);
            label7.Location = new Point(tbPunto1Y.Location.X + tbPunto2X.Width, label6.Location.Y);
            label7.BackColor = Color.Transparent;

            label8.Show();
            label8.Text = Racional.AString(vector1.Componentes[2] * vector2.Componentes[2]);
            label8.Font = new Font(label1.Font.FontFamily, 10, FontStyle.Bold);
            pnDatos.Controls.Add(label8);
            label8.Location = new Point(tbPunto1Z.Location.X, label5.Location.Y);
            label8.BackColor = Color.Transparent;

            label9.Show();
            label9.BackColor = Color.Chartreuse;
            label9.Text = " = "+ Racional.AString(vector1 * vector2);
            label9.Font = new Font(label1.Font.FontFamily, 13, FontStyle.Bold);
            label9.Location = new Point(pnDatos.Location.X + pnDatos.Width, pnDatos.Location.Y + pnDatos.Height - 21);

            label10.Show();
            label10.Font = new Font(label8.Font.FontFamily, 9, FontStyle.Underline);
            label10.BackColor = Color.Transparent;
            label10.Text = "Producto punto (U*V): ";
            pnDatos.Controls.Add(label10);
            label10.Location = new Point(label35.Location.X-130, label8.Location.Y);

            if (!directa)
                btDemostracion.Show();
            // Mostrar los modulos de los vectores
            label11.Show();
            label11.Font = new Font (label10.Font.FontFamily,10, FontStyle.Bold);
            label11.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + btContinuar.Height + 5);
            label11.BackColor = Color.Transparent;
            label11.Text = "Módulo del vector 1 ( | U | ): ";

            label12.Show();
            label12.Font = new Font(label11.Font.FontFamily, 13, FontStyle.Bold);
            label12.Location = new Point(label11.Location.X + label11.Width, label11.Location.Y);
            label12.BackColor = Color.DarkOrange;
            label12.Text = Math.Round(vector1.ModuloDecimal(), 2).ToString();

            label13.Show();
            label13.Font = new Font(label10.Font.FontFamily, 10, FontStyle.Bold);
            label13.Location = new Point(btContinuar.Location.X, label11.Location.Y + label11.Height + 10);
            label13.BackColor = Color.Transparent;
            label13.Text = "Módulo del vector 2 ( | V | ): ";

            label14.Show();
            label14.Font = new Font(label11.Font.FontFamily, 13, FontStyle.Bold);
            label14.Location = new Point(label13.Location.X + label13.Width, label13.Location.Y);
            label14.BackColor = Color.SlateBlue;
            label14.Text = Math.Round(vector2.ModuloDecimal(), 2).ToString();

            // Mostrar la formula del angulo entre vectores
            label15.Show();
            label15.Font = label14.Font;
            label15.Location = new Point(btContinuar.Location.X, label14.Location.Y + label14.Height+5);
            label15.BackColor = Color.LightYellow;
            label15.Text = "           U * V " + "\ncos Φ = ----------" + "\n             |U| * |V|";
           
            if (directa)
            {
                paso++;
                lbExplicacion.Hide();
              Finalizar();
            }
             
        }
        
         /// <summary>
        /// 
       /// REALIZA LA DEMOSTRACION DE LA FORMULA DEL ANGULO ENTRE VECTORES QUE SE EXTRAE DE LA LEY
       /// DE LOS COSENOS 
        /// 
        /// </summary>
        /// 
        private void btDemostracion_Click( object sender, EventArgs e)
        {
            ventanagrafica.PintarVector(new Punto(vector2.Componentes), new Punto(vector1.Componentes), Color.Red,4,false);
                Punto situacionW = Punto.PuntoMedio(new Punto(vector1.Componentes), new Punto(vector2.Componentes));
            ventanagrafica.PintarString(situacionW, Color.Red, 14, "W");
            DemostracionFormulaAngulo demostracion = new DemostracionFormulaAngulo();
            ventanagrafica.Ventana.Invalidate();
            demostracion.ShowDialog();
        }

           /// <summary>
        /// 
       /// FINALIZA LA RESOLUCION PASO A PASO
        /// 
        /// </summary>
        /// 
        private void Finalizar()
        {
          //  double cosenoangulo = ((vector1 * vector2).Numerador / (vector1 * vector2).Denominador) / (vector1.ModuloDecimal() * vector2.ModuloDecimal());
            double cosenoangulo = Math.Cos(Vector.Angulo(vector1, vector2));
            // Mostrar el resultado
            label20.Show();
            label20.Font = label15.Font;
            label20.BackColor = label15.BackColor;
            label20.Location = new Point(label15.Location.X + label15.Width, label15.Location.Y + 15);
            label20.Text = " = " + Math.Round(cosenoangulo, 3).ToString();
            label16.Show();
            label16.Font = new Font(label15.Font.FontFamily,15,FontStyle.Bold);
            label16.BackColor = Color.Chartreuse;
            label16.Location = new Point(label15.Location.X, label15.Location.Y + label15.Height+5);
         //   label16.Text =  Math.Round(Math.Acos(cosenoangulo), 3).ToString() + " radianes.";
            label16.Text = Math.Round(Vector.Angulo(vector1,vector2), 3).ToString() + " radianes.";
label17.Show();
            label17.BackColor = label16.BackColor;
            label17.Font = label16.Font;
            label17.Location = new Point ( label15.Location.X, label16.Location.Y+label16.Height+5);
            label17.Text =  Math.Round(Math.Acos(cosenoangulo) * (180/Math.PI), 3).ToString() + " grados.";

            btContinuar.Hide();
          
          //  ventanagrafica.Ventana.Invalidate();
            
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Matematicas;
using Punto_y_Vector;
using Recta_y_plano;



namespace Matedicom
{
    public partial class InterseccionRectas : FormularioBase
    {
       new bool directa; // Tipo de resolucion
        new bool defecto = false; // Será true si se pulsa el boton DEF para usar valores por defecto
        EspacioIsometrico ventanagrafica; // Ventana grafica

        Punto punto1; // Punto inicial del segmento de recta
        Punto punto2; // Punto final del segmento de recta
        Punto punto3;// Punto inicial del segmento de la segunda recta
            Punto punto4; // Punto final del segmento de la segunda recta

        new int paso = 0; // Paso en el que se encuentra la resolucion

        Recta linea1; // Objeto linea que se construirá con los dos primeros puntos introducidos
        Recta linea2; // Objeto linea que se construirá con los dos segundos puntos introducidos
        //Vector vector1; // Vector paralelo a la recta 1
       // Vector vector2; // Vector paralelo a la recta 2
        Ecuacion[] ecuaciones1; // Sistema con las ecuaciones parametricas de la recta 1
        Ecuacion[] ecuaciones2; // Sistema con las ecuaciones parametricas de la recta 2
        ControlesFlotantes flotante; // Se usará para mostrar las operaciones a realizar para la resolución
        public InterseccionRectas (bool resolucion)
        {
            directa = resolucion;
        }


        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = "Punto de intersección entre dos rectas.";
            labeli2.Hide();
            labelj2.Hide();
            labelk2.Hide();
            ventanagrafica = new EspacioIsometrico(830, 640, new Point(5, 80));
            Controls.Add(ventanagrafica.Ventana);
            lbExplicacion.Show();
            lbExplicacion.Text = " Introducir las coodenadas de los puntos incial y final de un segmento de cada recta ( enteros o racionales ) .\n\n( O pulse el botón [E] para ejemplo con valores por omisión.";
            lbExplicacion.Width = btSalir.Location.X;
            tbPunto1X.Focus();
            pnDatos.Show();
            pnDatos.Location = new Point(ventanagrafica.Ventana.Location.X + ventanagrafica.Ventana.Width, ventanagrafica.Ventana.Location.Y);
            lbRecta1.Text = "Recta 1:";
            lbPuntoDePaso.Text = "Punto c:";
            lbPuntoDePaso.Location = new Point(lbRotuloA.Location.X, lbPuntoDePaso.Location.Y);
            lbVectorNormal.Text = "Punto d:";
            lbVectorNormal.Location = new Point(lbPuntoDePaso.Location.X, lbPuntoDePaso.Location.Y + lbPuntoDePaso.Height + 10);
            label35.Text = "x";
            label36.Text = "y";
            label37.Text = "z";
            lbRotuloA.Text = "Punto a:";
            lbRotuloA.AutoSize = true;
            lbRotuloA.BackColor = Color.Transparent;
            lbRotuloA.Location = new Point(lbRotuloA.Location.X - 5, lbRotuloA.Location.Y);
            lbRotuloB.Text = "Punto b:";
            lbRotuloB.AutoSize = true;
            lbRotuloB.Location = new Point(lbRotuloB.Location.X - 5, lbRotuloB.Location.Y);
            lbRotuloB.BackColor = Color.Transparent;
            btDefecto.Location = new Point(pnDatos.Location.X + pnDatos.Width, pnDatos.Location.Y);
            lbRecta2.Text = "Recta 2:";
            lbRecta2.BackColor = Color.SlateBlue;
            pnDatos.Height = tbPunto4X.Location.Y + tbPunto2X.Height + 15;
            tbPunto1X.Focus();
            tbPunto1X.KeyPress += Cajas_KeyPress;
            tbPunto1Y.KeyPress += Cajas_KeyPress;
            tbPunto1Z.KeyPress += Cajas_KeyPress;
            tbPunto2X.KeyPress += Cajas_KeyPress;
            tbPunto2Y.KeyPress += Cajas_KeyPress;
            tbPunto2Z.KeyPress += Cajas_KeyPress;
            tbPunto3X.KeyPress += Cajas_KeyPress;
            tbPunto3Y.KeyPress += Cajas_KeyPress;
            tbPunto3Z.KeyPress += Cajas_KeyPress;
            tbPunto4X.KeyPress += Cajas_KeyPress;
            tbPunto4Y.KeyPress += Cajas_KeyPress;
            tbPunto4Z.KeyPress += Cajas_KeyPress;
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
            if (directa)
                btDefecto.Hide();
            tbPunto3X.Location = new Point(tbPunto3X.Location.X, lbPuntoDePaso.Location.Y);
            tbPunto3Y.Location = new Point(tbPunto3Y.Location.X, lbPuntoDePaso.Location.Y);
            tbPunto3Z.Location = new Point(tbPunto3Z.Location.X, lbPuntoDePaso.Location.Y);
            tbPunto4X.Location = new Point(tbPunto4X.Location.X, lbVectorNormal.Location.Y);
            tbPunto4Y.Location = new Point(tbPunto4Y.Location.X, lbVectorNormal.Location.Y);
            tbPunto4Z.Location = new Point(tbPunto4Z.Location.X, lbVectorNormal.Location.Y);
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
	  /*
            MenuVectores menuvectores = new MenuVectores();
            menuvectores.Show();
            if (flotante != null)
                flotante.Dispose();
            this.Dispose();
	  */
        }

        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            InterseccionRectas nuevo = new InterseccionRectas(directa);
            nuevo.Show();
            this.Close();
            if (flotante != null)
                flotante.Close();
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
        ///  PONE EL ATRIBUTO defecto A TRUE PARA QUE SE INICIE LA RESOLUCION CON LOS VALORES POR
        ///  DEFECTO
        /// 
        /// </summary>
        /// 
        public override void btDefecto_Click(object sender, EventArgs e)
        {
            defecto = true;
            IniciarResolucion();
            btDefecto.Hide();
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
                        this.tbPunto3X.Focus();
                    }
                    else if (caja == this.tbPunto3X)
                    {
                        e.Handled = true;
                        this.tbPunto3Y.Focus();
                    }
                    else if (caja == this.tbPunto3Y)
                    {
                        e.Handled = true;
                        this.tbPunto3Z.Focus();
                    }
                    else if (caja == this.tbPunto3Z)
                    {
                        e.Handled = true;
                        this.tbPunto4X.Focus();
                    }
                    else if (caja == this.tbPunto4X)
                    {
                        e.Handled = true;
                        this.tbPunto4Y.Focus();
                    }
                    else if (caja == this.tbPunto4Y)
                    {
                        e.Handled = true;
                        this.tbPunto4Z.Focus();
                    }
                    else if (caja == this.tbPunto4Z)
                    {
                        e.Handled = true;
                        this.Focus();
                        if (!directa)
                            btContinuar.Visible = true;
                        if (!directa)
                        {
                            this.AcceptButton = this.btContinuar;
                            IniciarResolucion();
                            paso = 1;
                        }
                        else if (directa)
                            IniciarResolucion();
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
        /// LLAMA AL METODO ADECUADO SEGUN EN QUE PASO SE ENCUENTRE LA RESOLUCION PASO A PASO
        /// 
        /// </summary>
        /// 
        private void btContinuar_Click(object sender, EventArgs e)
        {
             if (paso == 1)
                ContinuarResolucion();
            else if (paso > 1)
                FinalizarResolucion();
            paso++;
        }

        /// <summary>
        /// 
        /// DIBUJA LOS PUNTOS, EL SEGMENTO DE LINEA Y EL VECTOR E INICIA LA EXPLICACION
        /// 
        /// </summary>
        /// 
        private void IniciarResolucion()
        {
            btIsometrica.PerformClick();
            if (defecto)
            {
                this.Focus();
                btContinuar.Visible = true;
                this.AcceptButton = this.btContinuar;
                paso++;
                tbPunto1X.Text = "-27";
                tbPunto1Y.Text = "-61";
                tbPunto1Z.Text = "17";
                tbPunto2X.Text = "23";
                tbPunto2Y.Text = "39";
                tbPunto2Z.Text = "-8";
                 tbPunto3X.Text = "50";
                tbPunto3Y.Text = "-30";
                tbPunto3Z.Text = "20";
                 tbPunto4X.Text = "79/2";
                tbPunto4Y.Text = "267/2";
                tbPunto4Z.Text = "-37";
                sbAnguloZ.Value = 70;
                sbAnguloY.Value = -30;
                sbAngulox.Value = 30;
            }

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
            btDefecto.Hide();

            punto1 = new Punto(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text);
            punto2 = new Punto(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);
            punto3 = new Punto(tbPunto3X.Text + " " + tbPunto3Y.Text + " " + tbPunto3Z.Text);
             punto4 = new Punto(tbPunto4X.Text + " " + tbPunto4Y.Text + " " + tbPunto4Z.Text);

            lbExplicacion.Text = " Primero construimos las ecuaciones paramétricas de las rectas que pasan por los puntos introducidos. ( Ecuación de la recta en el menu anterior. )";
            if (directa)
                lbExplicacion.Hide();

            // Dibujar los segmentos de las lineas entre los puntos introducidos
            ventanagrafica.PintarVector(punto1,punto2,Color.DarkOrange,3F,true);
            ventanagrafica.PintarVector(punto3,punto4,Color.SlateBlue,3F,true);
         if(defecto)
             btAjustar.PerformClick();

            // Mostrar las ecuaciones paramétricas de la rectas en un recuadro flotante
            linea1 = new Recta(punto1, punto2);
            linea2 = new Recta(punto3, punto4);
            if(!directa)
            {
                flotante = new ControlesFlotantes();
                flotante.Text = " Desarrollo de la resolución";
                flotante.pnParametro.Show();
                flotante.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + 20);
                flotante.pnParametro.Controls.Add(label1);
                flotante.Show();
                flotante.Opacity = 1;
                flotante.lbTituloParametro.Text = "Ecuaciones paramétricas de la recta 1:";
                flotante.sbValor.Hide();
                flotante.tbParametro.Hide();
                flotante.StartPosition = FormStartPosition.Manual;
                flotante.Location = new Point(pnDatos.Location.X, pnDatos.Location.Y + pnDatos.Height + 5);
                foreach (Ecuacion e in linea1.EcuacionesParametricas())
                {
                    //flotante.lbTituloParametro.Text += "\n" + e.ObtenerTerminoIzquierda(0).ToString() + " = " + e.ObtenerTerminoDerecha(0).ToString();
 		    double coef1 = Math.Round(e.ObtenerTerminoIzquierda(0).Coeficiente.ToDouble(),4);
		    double coef2 = Math.Round(e.ObtenerTerminoDerecha(0).Coeficiente.ToDouble(),4);
		    String ecu1 = "\n" + e.ObtenerTerminoIzquierda(0).Variables[0] + " = " + coef2.ToString() +" " + e.ObtenerTerminoDerecha(0).Variables[0];
		  //  flotante.lbTituloParametro.Text += "\n" + e.ObtenerTerminoIzquierda(0).Variables[0] + " = " + coef2.ToString() +" " + e.ObtenerTerminoDerecha(0).Variables[0];
		
                    if (e.ObtenerLadoDerecho.Largo > 1 && e.ObtenerTerminoDerecha(1).Coeficiente.Numerador > 0)
                       // flotante.lbTituloParametro.Text += " + ";
			  ecu1 += " + ";
		    else
			//flotante.lbTituloParametro.Text += " ";
			ecu1 += " ";
                    if( e.ObtenerLadoDerecho.Largo > 1)
                    //flotante.lbTituloParametro.Text += Math.Round(e.ObtenerTerminoDerecha(1).Coeficiente.ToDouble(), 4).ToString() +" "+ e.ObtenerTerminoDerecha(1).Variables[0];
		      ecu1 += Math.Round(e.ObtenerTerminoDerecha(1).Coeficiente.ToDouble(), 4).ToString() +" "+ e.ObtenerTerminoDerecha(1).Variables[0];
			ecu1 = ecu1.Replace('P','t');
		   flotante.lbTituloParametro.Text += ecu1;
                }
                   // flotante.lbTituloParametro.Text += "\n" + e.ToString();
                flotante.lbTituloParametro.Text += "\nEcuaciones paramétricas de la recta 2:";

                foreach (Ecuacion e in linea2.EcuacionesParametricas())
                {
			double coef3 = Math.Round(e.ObtenerTerminoIzquierda(0).Coeficiente.ToDouble(),4);
			double coef4 = Math.Round(e.ObtenerTerminoDerecha(0).Coeficiente.ToDouble(),4);
			string ecu2 = "\n" + e.ObtenerTerminoIzquierda(0).Variables[0] + " = " + coef4.ToString() + " " + e.ObtenerTerminoDerecha(0).Variables[0];
                   // flotante.lbTituloParametro.Text += "\n" + e.ObtenerTerminoIzquierda(0).ToString() + " = " + e.ObtenerTerminoDerecha(0).ToString();
                    if (e.ObtenerLadoDerecho.Largo > 1 && e.ObtenerTerminoDerecha(1).Coeficiente.Numerador > 0)
                        //flotante.lbTituloParametro.Text += " + ";
			ecu2 += " + ";
			else 
			 //flotante.lbTituloParametro.Text += " ";
			ecu2 += " ";
                    if(e.ObtenerLadoDerecho.Largo > 1)
                   // flotante.lbTituloParametro.Text += Math.Round(e.ObtenerTerminoDerecha(1).Coeficiente.ToDouble(), 4).ToString() + e.ObtenerTerminoDerecha(1).Variables[0];
		   ecu2 += Math.Round(e.ObtenerTerminoDerecha(1).Coeficiente.ToDouble(), 4).ToString() + e.ObtenerTerminoDerecha(1).Variables[0];
		 ecu2 = ecu2.Replace('P','s');
		   flotante.lbTituloParametro.Text += ecu2;
                }
                flotante.lbTituloParametro.Anchor = AnchorStyles.Top;
                flotante.lbTituloParametro.TextAlign = ContentAlignment.MiddleLeft;
                flotante.lbTituloParametro.Location = new Point(flotante.lbTituloParametro.Location.X - 100, flotante.lbTituloParametro.Location.Y);
                flotante.Size = new Size(450, 200);
                flotante.pnParametro.Size = new Size(430, 180);
            }
            if (!directa)
                btIsometrica.PerformClick();
            if (directa)
                ContinuarResolucion();
           
        }


        /// <summary>
        /// 
        /// CONTRUYE EL SISTEMA IGUALANDO LAS DOS PRIMERAS ECUACIONES PARAMETRICAS DE CADA
        /// RECTA Y CONTINUA LA EXPLICACION
        /// 
        /// </summary>
        /// 
        private void ContinuarResolucion()
        {
            lbExplicacion.Text = " El punto de intersección en caso de que exista, será un punto que estará en las dos rectas, por lo tanto cumplirá las ecuaciones paramétricas de ambas.\n Si llamamos t al parametro de la recta 1 y s al parametro de la recta 2, e igualamos las dos primeras ecuaciones de cada recta, podemos calcular el valor del parametro que hay que aplicar en la ecuación de cada recta para determinar las coordenadas del punto de intersección";

            ecuaciones1 = linea1.EcuacionesParametricas();
             ecuaciones2 = linea2.EcuacionesParametricas();
            Ecuacion ecuacion1 = new Ecuacion(new List<Termino>(), new List<Termino>());
            Ecuacion ecuacion2 = new Ecuacion(new List<Termino>(), new List<Termino>());
            ecuacion1.ObtenerLadoDerecho.EliminarCeros();
            ecuacion1.ObtenerLadoIzquierdo.EliminarCeros();
            ecuacion2.ObtenerLadoDerecho.EliminarCeros();
            ecuacion2.ObtenerLadoIzquierdo.EliminarCeros();
            foreach (Termino t in ecuaciones1[0].ObtenerLadoDerecho.Terminos)
            {
                if (t.Variables[0] != ' ')
                {
                    ecuacion1.AñadirTerminoIzquierda(new Termino(t.Coeficiente, 't', t.Exponentes[0]));
                }
                else
                    ecuacion1.AñadirTerminoIzquierda(new Termino(t.Coeficiente, t.Variables[0], t.Exponentes[0]));
            }
            foreach (Termino t in ecuaciones2[0].ObtenerLadoDerecho.Terminos)
            {
                if (t.Variables[0] != ' ')
                {
                    ecuacion1.AñadirTerminoDerecha(new Termino(t.Coeficiente, 's', t.Exponentes[0]));
                }
                else
                    ecuacion1.AñadirTerminoDerecha(new Termino(t.Coeficiente, t.Variables[0], t.Exponentes[0]));
            }
            foreach (Termino t in ecuaciones1[1].ObtenerLadoDerecho.Terminos)
            {
                if (t.Variables[0] != ' ')
                {
                    ecuacion2.AñadirTerminoIzquierda(new Termino(t.Coeficiente, 't', t.Exponentes[0]));
                }
                else
                    ecuacion2.AñadirTerminoIzquierda(new Termino(t.Coeficiente, t.Variables[0], t.Exponentes[0]));
            }
            foreach (Termino t in ecuaciones2[1].ObtenerLadoDerecho.Terminos)
            {
                if (t.Variables[0] != ' ')
                {
                    ecuacion2.AñadirTerminoDerecha(new Termino(t.Coeficiente, 's', t.Exponentes[0]));
                }
                else
                    ecuacion2.AñadirTerminoDerecha(new Termino(t.Coeficiente, t.Variables[0], t.Exponentes[0]));
            }
            if (!directa)
            {
                flotante.Size = new Size(flotante.Width, flotante.Height + 100);
                flotante.pnParametro.Size = new Size(flotante.pnParametro.Width, flotante.pnParametro.Height + 100);
               
                flotante.lbTituloParametro.Text += "\n\nSistema igualando las dos primeras ecuaciones:\n";
                double aux1i = ecuacion1.ObtenerTerminoIzquierda(0).Coeficiente.ToDouble();
                double aux2i = 0;
                if (ecuacion1.ObtenerLadoIzquierdo.Largo > 1)
                    aux2i = ecuacion1.ObtenerTerminoIzquierda(1).Coeficiente.ToDouble();
                double aux1d = ecuacion1.ObtenerTerminoDerecha(0).Coeficiente.ToDouble();
                double aux2d = 0;
                if (ecuacion1.ObtenerLadoDerecho.Largo > 1)
                    aux2d = ecuacion1.ObtenerTerminoDerecha(1).Coeficiente.ToDouble();

                flotante.lbTituloParametro.Text += Math.Round(aux1i, 4).ToString() + " t ";
                if (aux2i > 0)
                    flotante.lbTituloParametro.Text += " + ";
                flotante.lbTituloParametro.Text += Math.Round(aux2i, 4).ToString() + ecuacion1.ObtenerTerminoIzquierda(1).Variables[0] + " = ";

                flotante.lbTituloParametro.Text += Math.Round(aux1d, 4).ToString() + " s ";
                if (aux2d > 0)
                    flotante.lbTituloParametro.Text += " + ";
                if (ecuacion1.ObtenerLadoDerecho.Largo > 1)
                    flotante.lbTituloParametro.Text += Math.Round(aux2d, 4).ToString() + ecuacion1.ObtenerTerminoDerecha(1).Variables[0];


                aux1i = ecuacion2.ObtenerTerminoIzquierda(0).Coeficiente.ToDouble();
                if (ecuacion2.ObtenerLadoIzquierdo.Largo > 1)
                    aux2i = ecuacion2.ObtenerTerminoIzquierda(1).Coeficiente.ToDouble();
                aux1d = ecuacion2.ObtenerTerminoDerecha(0).Coeficiente.ToDouble();
                if (ecuacion2.ObtenerLadoDerecho.Largo > 1)
                    aux2d = ecuacion2.ObtenerTerminoDerecha(1).Coeficiente.ToDouble();
                flotante.lbTituloParametro.Text += "\n" + Math.Round(aux1i, 4).ToString() + " t ";
                if (aux2i > 0)
                    flotante.lbTituloParametro.Text += " + ";
                if (ecuacion2.ObtenerLadoIzquierdo.Largo > 1)
                    flotante.lbTituloParametro.Text += Math.Round(aux2i, 4).ToString() + ecuacion2.ObtenerTerminoIzquierda(1).Variables[0] + " = ";
                else
                    flotante.lbTituloParametro.Text += " = ";

                flotante.lbTituloParametro.Text += Math.Round(aux1d, 4).ToString() + " s ";
                if (aux2d > 0)
                    flotante.lbTituloParametro.Text += " + ";
                if (ecuacion2.ObtenerLadoDerecho.Largo > 1)
                    flotante.lbTituloParametro.Text += Math.Round(aux2d, 4).ToString() + ecuacion2.ObtenerTerminoDerecha(1).Variables[0];
            }
                if (directa)
                    FinalizarResolucion();
            
        }

        /// <summary>
        ///
        /// VISUALIZA PARAMETRO PARA CADA RECTA, EL PUNTO DE INTERSECCION , EXTIENDE LAS RECTAS HASTA
        /// EL MISMO Y FINALIZA LA RESOLUCION
        /// 
        /// 
        /// </summary>
        /// 
        private void FinalizarResolucion()
        {
            
            btContinuar.Hide();
            lbExplicacion.Text = " Resolviendo el sistema, se obtienen los parametros t y s que se han de aplicar a las ecuaciones parametricas de cada recta para obtener el punto de intersección.";
  
            Punto interseccion = linea1.Interseccion(linea2);
           
            if( linea1.EsParalela(linea2))
            {
                if (directa)
                {
                    lbResultado.Show();
                    lbResultado.Text = "Rectas paralelas.";
                }
                lbExplicacion.Text += "\nComo las lineas introducidas son paralelas, el punto de intersección entre ambas no existe2.";
            }
            else if( interseccion == null)
            {
                if (directa)
                {
                    label10.Show();
                    label10.Text = "Las lineas se cruzan sin cortarse.";
                    label10.BackColor = Color.Chartreuse;
                    label10.Location = btContinuar.Location;
                }
                lbExplicacion.Text += "\n Podemos observar que aplicando el parámetro correspondiente a las ecuaciones de cada recta, el resultado es distinto. Esto indica que las rectas no se cortan, sino que se cruzan sin llegar a coincidir en ningún punto.";
                 
                // Alargar las dos rectas 100 unidades por los dos extremos
                Punto extension1 = linea1.SituacionPunto(100);
              
              //  ventanagrafica.PintarPunto(extension1, 10, false, Color.Pink);
                
                Punto extension2 = linea1.SituacionPunto(-100);
                Punto extension3 = linea2.SituacionPunto(100);
                Punto extension4 = linea2.SituacionPunto(-100);
                ventanagrafica.PintarLinea(extension1, extension2, Color.DarkOrange,1);
                ventanagrafica.PintarLinea(extension3, extension4, Color.SlateBlue,1);
            }
            else
            {
                lbExplicacion.Text += "\n Podemos observar, que aplicando el parámetro correspondiente a las ecuaciones de cada recta, el resultado es el mismo. Si el resultado fuese distinto, las rectas no se cortarian, pero en este caso se cortan en el punto indicado como se vé en el grafico.";
              // Puntos de paso de cada recta
                Punto a = linea1.PuntoDePaso;
                Punto c = linea2.PuntoDePaso;
                // Reducir la precision del punto de interseccion a tres decimales
                double intx = Math.Round(interseccion.X.ToDouble(), 3);
                double inty = Math.Round(interseccion.Y.ToDouble(), 3);
                double intz = Math.Round(interseccion.Z.ToDouble(), 3);
                Punto intersecc = new Punto(new Racional[] { intx, inty, intz });
                // Calcular parametro que será la distancia entre el punto de paso de cada recta y el punto de interseccion
                Vector vector1 = new Vector(a, intersecc);
                double parametro1 = Math.Round(vector1.Modulo().ARacional().ToDouble(), 4);
                Vector vector2 = new Vector(c, intersecc);
                double parametro2 = Math.Round(vector2.Modulo().ARacional().ToDouble(),4);
              // Mostrar los parametros en el control flotante
                if(!directa)
                flotante.lbTituloParametro.Text += "\n\nt = " + parametro1 + "        s = " + parametro2;
                lbResultado.Show();

                label10.Show();
                label10.Location = btContinuar.Location;
                label10.BackColor = Color.Chartreuse;
                label10.Text = "Punto de intersección de las rectas:\n ( x:" + Math.Round(interseccion.X.ToDouble(), 4).ToString() + " y: " + Math.Round(interseccion.Y.ToDouble(), 4).ToString() + " z: " + Math.Round(interseccion.Z.ToDouble(), 4).ToString();

                ventanagrafica.PintarPunto(interseccion, 20, true, Color.Chartreuse);
                ventanagrafica.PintarLinea(punto2, interseccion, Color.DarkOrange, 1);
                ventanagrafica.PintarLinea(punto4, interseccion, Color.SlateBlue, 1);
            }
            btCentrar.PerformClick();
            if (directa)
            {
                btIsometrica.PerformClick();
                btAjustar.PerformClick();
            }
        }
        

    }
}
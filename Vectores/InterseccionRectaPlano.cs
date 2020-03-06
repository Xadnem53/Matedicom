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
    public partial class InterseccionRectaPlano : FormularioBase
    {
       new bool directa; // Tipo de resolucion
       new bool defecto = false; // Será true si se pulsa el boton DEF para usar valores por defecto
        EspacioIsometrico ventanagrafica; // Ventana grafica

        Punto punto1; // Punto inicial del segmento de recta
        Punto punto2; // Punto final del segmento de recta
        Punto punto3;// Vector perpendicular al plano
        Punto punto4; // Punto de paso del plano

        new int paso = 0; // Paso en el que se encuentra la resolucion

        Recta linea1; // Objeto linea que se construirá con los dos primeros puntos introducidos
        Plano plano; // Plano que se construirá con el vector perpendicular y el punto de paso

        double n1 = 0;
        double p1 = 0;
        double n2 = 0;
        double p2 = 0;
        double n3 = 0;
        double p3 = 0;
        double independiente = 0;


        double parametro; // Será el parametro a aplicar en las ecuaciones de la recta para llegar al punto de interseccion con el plano
        ControlesFlotantes flotante; // Para mostrar el desarrollo de la resolucion

        public InterseccionRectaPlano(bool direct)
        {
            directa = direct;
        }

        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = "Punto de intersección entre una recta y un plano.";
            ventanagrafica = new EspacioIsometrico(830, 640, new Point(5, 80));
            Controls.Add(ventanagrafica.Ventana);
            lbExplicacion.Show();
            lbExplicacion.Text = " Introducir las coodenadas de dos puntos para definir la recta y un punto de paso y un vector perpendicular para definir el plano ( enteros o racionales ) .\n\n( O pulse el botón [E] para ejemplo con valores por omisión.";
            lbExplicacion.Width = btSalir.Location.X;
            tbPunto1X.Focus();
            pnDatos.Show();
            pnDatos.Location = new Point(ventanagrafica.Ventana.Location.X + ventanagrafica.Ventana.Width, ventanagrafica.Ventana.Location.Y);
            lbRecta1.Text = "Recta:";
            lbPuntoDePaso.Text = "Punto de paso:";
            lbPuntoDePaso.Location = new Point(lbRotuloA.Location.X-55, lbPuntoDePaso.Location.Y);
            lbVectorNormal.Text = "Vector normal:";
            lbVectorNormal.Location = new Point(lbPuntoDePaso.Location.X, lbPuntoDePaso.Location.Y + lbPuntoDePaso.Height + 25);
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
            lbRecta2.Text = "Plano:";
            lbRecta2.BackColor = Color.LightGreen;
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
            MenuVectores menuvectores = new MenuVectores();
            menuvectores.Show();
            if (flotante != null)
                flotante.Dispose();
            this.Dispose();
        }

        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            InterseccionRectaPlano nuevo = new InterseccionRectaPlano(directa);
            nuevo.Show();
            if (flotante != null)
                flotante.Close();
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
                            this.AcceptButton = this.btContinuar;
                            btContinuar.Focus();
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
        /// DIBUJA LA RECTA, EL PLANO  E INICIA LA EXPLICACION
        /// 
        /// </summary>
        /// 
        private void IniciarResolucion()
        {

            btDefecto.Hide();

            // Valores por defecto
            if (defecto)
            {
                this.Focus();
                btContinuar.Visible = true;
                this.AcceptButton = this.btContinuar;
                paso++;
                tbPunto1X.Text = "33";
                tbPunto1Y.Text = "12";
                tbPunto1Z.Text = "8";
                tbPunto2X.Text = "3";
                tbPunto2Y.Text = "5";
                tbPunto2Z.Text = "10";
                tbPunto3X.Text = "4";
                tbPunto3Y.Text = "13";
                tbPunto3Z.Text = "-9";
                tbPunto4X.Text = "2";
                tbPunto4Y.Text = "-1";
                tbPunto4Z.Text = "3";
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

            punto1 = new Punto(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text);// Punto inicial de la recta
            punto2 = new Punto(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);// Punto final de la recta
            punto3 = new Punto(tbPunto3X.Text + " " + tbPunto3Y.Text + " " + tbPunto3Z.Text);// Punto de paso del plano
            punto4 = new Punto(tbPunto4X.Text + " " + tbPunto4Y.Text + " " + tbPunto4Z.Text);// Vector perpendicular al plano

            lbExplicacion.Text = " El punto de intersección, será un punto que estará a la vez en el plano y en la recta. Por lo tanto, el punto buscado debe cumplir tanto la ecuación de la recta, como la ecuación del plano.";
            if (directa)
                lbExplicacion.Hide();

         //   // Crear el objeto linea.
            linea1 = new Recta(punto1, punto2);
          
            // Dibujar el segmento de linea entre los puntos introducidos.
            ventanagrafica.PintarLinea(punto1, punto2, Color.DarkOrange, 3F);
          
            // Crear el objeto plano
            plano = new Plano(new Vector(punto4.Coordenadas), punto3);
           
            // Dibujar el plano segun el vector y punto de paso introducidos.
            Racional factor = plano.Interseccion(linea1).CoordenadaMaxima; // Para hacer el plano mas o menos grande segun donde esté el punto de interseccion
          
            ventanagrafica.PintarPlano(new Vector(punto4.Coordenadas), punto3, 50, Color.Chartreuse);
           
            // Mostrar la ecuacion paramétrica de la rectas
            if (!directa)
                label1.Show();
            label1.Location = new Point(btContinuar.Location.X + btContinuar.Width + 5, btContinuar.Location.Y);// + btContinuar.Height + 10);
            label1.BackColor = Color.Transparent;
            label1.Font = new Font(label1.Font.FontFamily, 10);
            label1.Text = "Ecuaciones paramétricas de la recta:\n";

            foreach (Ecuacion e in linea1.EcuacionesParametricas())
            {
               // label1.Text += e.ToString() + "\n";
                label1.Text += e.ObtenerLadoIzquierdo.ToString() + " = " + Math.Round(e.ObtenerTerminoDerecha(0).Coeficiente.ToDouble(), 4).ToString();
                if( e.ObtenerTerminoDerecha(1).Coeficiente.Numerador > 0)
                    label1.Text += " + ";
                label1.Text += Math.Round(e.ObtenerTerminoDerecha(1).Coeficiente.ToDouble(), 4).ToString() + "p\n";

            }

            // Mostrar la ecuacion del plano
            if (!directa)
                label2.Show();
            label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 10);
            label2.Font = label1.Font;
            label2.BackColor = Color.Transparent;
            label2.Text = "Ecuación del plano: \n";
            label2.Text += plano.ToString();

            btIsometrica.PerformClick();
            if (directa)
                ContinuarResolucion();

            paso = 1;
        }


        /// <summary>
        /// 
       /// SUSTITUYE LOS VALORES X, Y, Z EN LA ECUACION DEL PLANO, POR LA ECUACION PARAMETRICA DE LA
       /// RECTA CORRESPONDIENTE Y REALIZA LAS OPERACIONES PASO A PASO PARA DESPEJAR EL VALOR DEL
       /// PARAMETRO P
        /// 
        /// </summary>
        /// 
        private void ContinuarResolucion()
        {
            lbExplicacion.Text = " Para encontrar el punto que está en la recta y en el plano seguimos los siguientes pasos:\n Sustituimos los valores x,y,z de la ecuación del plano por la correspondiente ecuación paramétrica de la recta, eliminamos parentesis, simplificamos y despejamos P.";
            if (!directa)
            {
                flotante = new ControlesFlotantes();
                flotante.Show();

                label3.Show();
                flotante.Controls.Add(label3);
                label3.Location = new Point(5, 5);
                label3.BackColor = Color.Transparent;
                label3.Font = new Font(label3.Font.FontFamily, 12);
                label3.Text = "Sustituir: \n";
                flotante.AutoSize = true;
                flotante.StartPosition = FormStartPosition.Manual;
                flotante.Location = new Point(pnDatos.Location.X - flotante.Width - 5, pnDatos.Location.Y + 20);
                flotante.Opacity = 1;
                // ESCRIBIR EL DESARROLLO DE LAS SUSTITUCIONES EN LA ETIQUETA
                // Coeficientes de las incognitas en la ecuacion del plano en formato double
                double coeficientex = plano.EcuacionDelPlano().ObtenerTerminoIzquierda(0).Coeficiente.ToDouble();
                double coeficientey = plano.EcuacionDelPlano().ObtenerTerminoIzquierda(1).Coeficiente.ToDouble();
                double coeficientez = plano.EcuacionDelPlano().ObtenerTerminoIzquierda(2).Coeficiente.ToDouble();
                // Coeficiente del termino independiente de la ecuacion del plano en formato double
                independiente = plano.EcuacionDelPlano().ObtenerTerminoDerecha(0).Coeficiente.ToDouble();
                // Coordenadas del punto de paso de la recta en formato double
                double ax = punto1.X.ToDouble();
                double ay = punto1.Y.ToDouble();
                double az = punto1.Z.ToDouble();
                // Componentes del vector unitario paralelo a la recta en formato double
                double vpi = linea1.VectorParalelo.Componentes[0].ToDouble();
                double vpj = linea1.VectorParalelo.Componentes[1].ToDouble();
                double vpk = linea1.VectorParalelo.Componentes[2].ToDouble();

                label3.Text += Math.Round(coeficientex, 4).ToString() + " * (" + Math.Round(ax, 4).ToString();
                if (vpi > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(vpi, 4).ToString() + "P )";
                if (coeficientey > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(coeficientey, 4).ToString() + " * ( " + Math.Round(ay, 4).ToString();
                if (vpj > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(vpj, 4).ToString() + "P )";
                if (coeficientez > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(coeficientez, 4).ToString() + " * (" + Math.Round(az, 4).ToString();
                if (vpk > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(vpk, 4).ToString() + "P ) = " + Math.Round(independiente, 4).ToString();


                label3.Text += "\n\n Eliminar parentesis: \n";
                n1 = coeficientex * ax;
                p1 = coeficientex * vpi;
                n2 = coeficientey * ay;
                p2 = coeficientey * vpj;
                n3 = coeficientez * az;
                p3 = coeficientez * vpk;
                label3.Text += Math.Round(n1, 4).ToString();
                if (p1 > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(p1, 4).ToString() + "P";
                if (n2 > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(n2, 4).ToString();
                if (p2 > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(p2, 4).ToString() + "P";
                if (n3 > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(n3, 4).ToString();
                if (p3 > 0)
                    label3.Text += " + ";
                label3.Text += Math.Round(p3, 4).ToString() + "P = " + Math.Round(independiente, 4).ToString();

                // Determinar el parametro
                double sumap = p1 + p2 + p3;
                double suman = independiente + n1 * -1 + n2 * -1 + n3 * -1;
                parametro = suman / sumap;

                label3.Text += "\n\n Simplificar y despejar\n";
                label3.Text += "P = " + Math.Round(parametro, 4).ToString();
            }
            if (directa)
                FinalizarResolucion();

        }
        
        /// <summary>
        ///
        /// MUESTRA LAS COORDENADAS DEL PUNTO DE INTERSECCION APLICANDO EL PARAMETRO A CADA UNA
        /// DE LAS ECUACIONES DE LA RECTA Y DIBUJA EL PUNTO DE INTERSECCION Y EXTIENDE LA RECTA HASTA
        /// LLEGAR AL PUNTO
        /// 
        /// </summary>
        /// 
        private void FinalizarResolucion()
        {
            
            btContinuar.Hide();
            lbExplicacion.Text = " Por último, basta con aplicar el parámetro obtenido a las ecuaciones paramétricas de la recta, para obtener las coordenadas x, y , z del punto de intersección buscado.\n(Mostrado en el gráfico en color rojo.)";
            
            //  Dibujar el punto de interseccion de la recta con el plano
            ventanagrafica.PintarPunto(plano.Interseccion(linea1), 10, false, Color.Red);

            // Dibujar la linea que va desde el punto inicial al punto de interseccion de la recta con el plano si el punto esta fuera del segmento de recta
            if (new Vector(punto1, punto2).ModuloDecimal() < new Vector(punto1, plano.Interseccion(linea1)).ModuloDecimal() || new Vector(punto1, punto2).ModuloDecimal() < new Vector(punto2, plano.Interseccion(linea1)).ModuloDecimal())
            {
                if (parametro < 0)
                    ventanagrafica.PintarLinea(punto2, plano.Interseccion(linea1), Color.DarkOrange,3);
                else
                    ventanagrafica.PintarLinea(punto1, plano.Interseccion(linea1), Color.DarkOrange,3);
            }
            btCentrar.PerformClick();

            lbResultado.Show();
            lbResultado.Location = new Point(10, lbExplicacion.Height + 10);
       
                    lbResultado.Text = "Punto de intersección = " + Math.Round(plano.Interseccion(linea1).X.ToDouble(), 6).ToString() + "x ;" + Math.Round(plano.Interseccion(linea1).Y.ToDouble(), 6).ToString() + "y ;" + Math.Round(plano.Interseccion(linea1).Z.ToDouble(), 6).ToString() + "z ";

            ventanagrafica.PintarPunto(plano.Interseccion(linea1), 10, true, Color.Red);
            //Reducir la precision de las coordenadas del punto de interseccion para evitar overflow
            Punto interseccion = plano.Interseccion(linea1);
            Racional[] coords = interseccion.Coordenadas;
            Racional[] reducida = new Racional[coords.Length];
            int cont = 0;
            foreach(Racional r in coords)
            {
                double s = Math.Round(r.ToDouble(), 10);
                reducida[cont] = s;
                cont++;
            }
            interseccion = new Punto(reducida);           
            ventanagrafica.PintarLinea(punto2, interseccion, Color.Chartreuse, 3);
            ventanagrafica.PintarPlano(new Vector(punto4.Coordenadas), plano.Interseccion(linea1), 10, Color.Chartreuse);
            btCentrar.PerformClick();
        }
        
        

    }
}

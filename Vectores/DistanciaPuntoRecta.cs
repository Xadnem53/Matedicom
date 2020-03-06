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
    public partial class DistanciaPuntoRecta : FormularioBase
    {
       new bool directa; // Tipo de resolucion
       new bool defecto = false; // Será true si se pulsa el boton DEF para usar valores por defecto
        EspacioIsometrico ventanagrafica; // Ventana grafica

        Punto punto1; // Punto inicial del segmento de recta
        Punto punto2; // Punto final del segmento de recta
        Punto punto3;// Punto inicial del segmento de la segunda recta

        new int paso = 0; // Paso en el que se encuentra la resolucion

        Recta linea1; // Objeto linea que se construirá con los dos primeros puntos introducidos

        Plano plano; // Será el plano perpendicular a la recta que pasa por el punto;
        Punto interseccion; // Sera el punto de interseccion de la recta con el plano perpendicular a la misma que pasa por el punto

        public DistanciaPuntoRecta(bool direct)
        {
            directa = direct;
        }

        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = "Distancia entre un punto y una recta.";
            if (directa)
                btDefecto.Hide();
            ventanagrafica = new EspacioIsometrico(830, 640, new Point(5, 80));
            Controls.Add(ventanagrafica.Ventana);
            lbExplicacion.Show();
            lbExplicacion.Text = " Introducir las coordenadas de dos puntos de la recta y el punto del que se quiere conocer su distancia a la recta.( enteros o racionales ).\n\n( O pulse el botón [E] para ejemplo con valores por omisión. )";
            lbExplicacion.Width = btSalir.Location.X;
            tbPunto1X.Focus();
            pnDatos.Show();
            pnDatos.Location = new Point(ventanagrafica.Ventana.Location.X + ventanagrafica.Ventana.Width, ventanagrafica.Ventana.Location.Y);
            lbRecta2.Text = "Punto:";
            lbRecta2.Location = new Point(lbRecta2.Location.X, tbPunto2X.Location.Y + tbPunto2X.Height + 5);
           // lbPuntoDePaso.Location = new Point(lbRotuloB.Location.X, lbRotuloB.Location.Y + lbRotuloB.Height + 5);
            //lbPuntoDePaso.Text = "Punto c:";
            //lbPuntoDePaso.Location = new Point(lbPuntoDePaso.Location.X, tbPunto2X.Location.Y + tbPunto2X.Height + 5);
            lbPuntoDePaso.Hide();
            tbPunto3X.Location = new Point(tbPunto3X.Location.X, lbRecta2.Location.Y + lbRecta2.Height + 5);
            tbPunto3Y.Location = new Point(tbPunto3Y.Location.X, tbPunto3X.Location.Y);
            tbPunto3Z.Location = new Point(tbPunto3Z.Location.X, tbPunto3X.Location.Y);
            tbPunto3X.BackColor = tbPunto2X.BackColor;
            tbPunto3Y.BackColor = tbPunto2X.BackColor;
            tbPunto3Z.BackColor = tbPunto2X.BackColor;
            lbVectorNormal.Hide();
            tbPunto4X.Hide();
            tbPunto4Y.Hide();
            tbPunto4Z.Hide();
            btDefecto.Location = new Point(pnDatos.Location.X + pnDatos.Width, pnDatos.Location.Y);
            labeli2.Hide();
            labelj2.Hide();
            labelk2.Hide();
            pnDatos.Height = tbPunto3X.Location.Y + tbPunto3X.Height + 15;
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
        }
        /// <summary>
        ///  
        ///SALE DEL PROGRAMA Y LIBERA TODOS LOS RECURSOS
        /// 
        /// </summary>
        /// 
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
        /// 
        public override void btSalir_Click(object sender, EventArgs e)
        {
            MenuVectores menuvectores = new MenuVectores();
            menuvectores.Show();
            this.Dispose();
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
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>
        /// 
        public override void btNuevo_Click(object sender, EventArgs e)
        {
            DistanciaPuntoRecta nuevo = new DistanciaPuntoRecta(directa);
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
                        this.Focus();
                        if (!directa)
                            btContinuar.Visible = true;
                            this.AcceptButton = this.btContinuar;
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
            else if (paso == 2)
                SeguirResolucion();
            else
                FinalizarResolucion();
            paso++;
        }

        /// <summary>
        /// 
        /// DIBUJA LA RECTA, EL PUNTO , EL PLANO PERPENDICULAR A LA RECTA QUE PASA POR EL PUNTO E INICIA
        /// LA EXPLICACION
        /// 
        /// </summary>
        /// 
        private void IniciarResolucion()
        {
            btDefecto.Hide();

            if (defecto)
            {
                this.Focus();
                btContinuar.Visible = true;
                this.AcceptButton = this.btContinuar;
                paso++;
                tbPunto1X.Text = "2";
                tbPunto1Y.Text = "6";
                tbPunto1Z.Text = "7";
                tbPunto2X.Text = "10";
                tbPunto2Y.Text = "-5";
                tbPunto2Z.Text = "15";
                tbPunto3X.Text = "10";
                tbPunto3Y.Text = "3";
                tbPunto3Z.Text = "2";
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
            punto1 = new Punto(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text);
            punto2 = new Punto(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);
            punto3 = new Punto(tbPunto3X.Text + " " + tbPunto3Y.Text + " " + tbPunto3Z.Text);

            if (directa)
                lbExplicacion.Hide();

            lbExplicacion.Text = " La distancia entre el punto y la recta, es el módulo del vector perpendicular a la recta que vá desde el punto a la recta.\n El punto inicial de ese vector, es el punto del que se quiere encontrar la distancia a la recta. El punto final de ese vector, será el punto de intersección de la recta con el plano perpendicular a la misma, y que pasa por el punto del que se quiere encontrar la distancia a la recta.";
           // Crear la recta
            linea1 = new Recta(punto1, punto2);
            
            // Pintar la linea
            ventanagrafica.PintarLinea(punto1, punto2, Color.DarkOrange,5);
            // Pintar los puntos inicial y final del segmento de recta
            ventanagrafica.PintarPunto(punto1, 5, true, Color.DarkOrange);
            ventanagrafica.PintarPunto(punto2, 5, true, Color.DarkOrange);

            // Pintar el punto
            ventanagrafica.PintarPunto(punto3,10,false, Color.Yellow);

            btIsometrica.PerformClick();
            btAjustar.PerformClick();

            if (directa)
                ContinuarResolucion();
            paso = 1;
        }


        /// <summary>
        /// 
        /// DIBUJA EL PLANO PERPENDICULAR A LA RECTA Y QUE PASA POR EL PUNTO , MUESTRA LA ECUACION DEL
        /// PLANO Y CONTINUA LA EXPLICACION
        /// 
        /// </summary>
        /// 
        private void ContinuarResolucion()
        {
            
            lbExplicacion.Text = "Construimos la ecuación del plano, con el punto del cual queremos conocer la distancia a la recta como punto de paso del plano, y el vector paralelo a la recta como vector perpendicular al plano.";

            Vector perpendicular = new Vector(punto1, punto2);
        
            // Construir el plano
           
            plano = new Plano(perpendicular, punto3);
            if (!directa)
            {
                // Mostrar la ecuacion del plano
                label1.Show();
                label1.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + btContinuar.Height + 5);
                label1.BackColor = Color.Transparent;
                label1.Font = new Font(label1.Font.FontFamily, 10);

                label1.Text = "Ecuación del plano:\n";
                foreach (Termino t in plano.EcuacionDelPlano().ObtenerLadoIzquierdo.Terminos)
                {
                    if (t.Coeficiente.Numerador > 0)
                        label1.Text += " + ";
                    label1.Text += Math.Round(t.Coeficiente.ToDouble(), 4).ToString() + t.Variables[0];
                }
                label1.Text += " = " + Math.Round(plano.EcuacionDelPlano().ObtenerTerminoDerecha(0).Coeficiente.ToDouble(), 4).ToString();
            }

            // Dibujar el plano
            long tamaño = (long)perpendicular.ModuloDecimal()*2; // Para determinar el tamaño en funcion del largo de la recta
           ventanagrafica.PintarPlano(perpendicular, punto3, tamaño, Color.Chartreuse);
           btCentrar.PerformClick();

            if (directa)
                SeguirResolucion();
         
        }


        /// <summary>
        ///
        ///DIBUJA EL PUNTO DE INTERSECCION DE LA RECTA CON EL PLANO, LO MUESTRA EN LA ETIQUETA Y SIGUE
        ///LA EXPLICACION
        /// 
        /// 
        /// </summary>
        /// 
        private void SeguirResolucion()
        {
            
            lbExplicacion.Text = " Con la ecuación del plano y las ecuaciones parámetricas de la recta, calculamos el punto de intersección de la recta con el plano. ( De color rojo en el gráfico.)";
            if (!directa)
            {
                label1.Text += "\n\nPunto de intersección:\n";
              label1.Text += "X: " + Math.Round(plano.Interseccion(linea1).X.ToDouble(),4).ToString();
              label1.Text += "\nY: " + Math.Round(plano.Interseccion(linea1).Y.ToDouble(), 4).ToString();
              label1.Text += "\nZ: " + Math.Round(plano.Interseccion(linea1).Z.ToDouble(), 4).ToString();
            }
            interseccion = plano.Interseccion(linea1); 
            ventanagrafica.PintarPunto(interseccion, 12, false, Color.Red);
            ventanagrafica.EliminarLinea(0);
            ventanagrafica.PintarLinea(punto1, interseccion,Color.DarkOrange,5);
            ventanagrafica.PintarLinea(interseccion, punto2, Color.FromArgb(10, Color.DarkOrange), 2);
            btCentrar.PerformClick();

            if (directa)
                FinalizarResolucion();
             

        }
        /// <summary>
        ///
        ///DIBUJA EL VECTOR ENTRE EL PUNTO DEL QUE SE QUIERE ENCONTRAR LA DISTANCIA A LA RECTA
        ///Y EL PUNTO DE INTERSECCION DE LA RECTA CON EL PLANO Y FINALIZA LA EXPLICACION
        /// 
        /// 
        /// </summary>
        /// 
        private void FinalizarResolucion()
        {
            label1.Location = btContinuar.Location;
            btContinuar.Hide();
            lbExplicacion.Text = "Por último, construimos el vector entre el punto del que queremos saber la distancia a la recta , y el punto de intersección de la recta con el plano.\nLa distancia buscada, es el módulo de este vector.";

            // Construir el vector entre los puntos
            Vector vector = new Vector(punto3, interseccion);
            if (!directa)
            {
               // lbVector.Show();
              //  lbVector.Text = "Vector entre los puntos:\n " + vector.ToString();
                label1.Text += "\nVector entre los puntos:\n " + Math.Round(vector.Componentes[0].ToDouble(),4).ToString() + "i";
                if (vector.Componentes[1].Numerador > 0)
                    label1.Text += " + ";
                label1.Text += Math.Round(vector.Componentes[1].ToDouble(), 4).ToString() + "j";
                if (vector.Componentes[2].Numerador > 0)
                    label1.Text += " + ";
                label1.Text += Math.Round(vector.Componentes[2].ToDouble(), 4).ToString() + "k";
            }
            // Dibujar el vector 
            ventanagrafica.PintarVector(punto3, interseccion, Color.Chartreuse, 5, false);

            //ventanagrafica.AñadirPunto(plano.Interseccion(linea1), 10, false, Color.Red);

            // Mostrar el resultado
            double modulo = Math.Sqrt((Math.Pow(vector.Componentes[0].ToDouble(), 2) + Math.Pow(vector.Componentes[1].ToDouble(), 2) + Math.Pow(vector.Componentes[2].ToDouble(), 2)));
           
            lbResultado.Show();
            lbResultado.Text = "Distancia entre el punto y la recta: \n" + modulo.ToString();
            lbResultado.Location = new Point(5, lbExplicacion.Height + 5);
            btCentrar.PerformClick();
            
        }
        

    }
}




    
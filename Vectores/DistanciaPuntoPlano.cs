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
    public partial class DistanciaPuntoPlano : FormularioBase
    {
       new bool directa; // Tipo de resolucion
        new bool defecto = false; // Será true si se pulsa el boton DEF para usar valores por defecto
        EspacioIsometrico ventanagrafica; // Ventana grafica

        Punto puntopasoplano; // Punto de paso del plano
        Punto punto; // Punto del que se quiere encontrar la distancia al plano
        Punto puntointerseccion;// Será el punto donde la recta que perpendicular al plano que pasa por el punto, corta al plano

        Vector perpendicular; // Vector perpendicular al plano

        new int paso = 0; // Paso en el que se encuentra la resolucion

        Recta recta; //Será la linea perpendicular al plano que pasa por el punto

        Plano plano; // Será el plano del que se quiere encontrar la distancia al punto

        public DistanciaPuntoPlano(bool resolucion)
        {
            directa = resolucion;
        }

        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = "Distancia entre un punto y un plano.";
            if (directa)
                btDefecto.Hide();
            ventanagrafica = new EspacioIsometrico(830, 640, new Point(5, 80));
            Controls.Add(ventanagrafica.Ventana);
            lbExplicacion.Show();
            if(!directa)
            lbExplicacion.Text = " Introducir el vector perpendicular y el punto de paso del plano, y el punto del que se quiere conocer su distancia al plano ( enteros o racionales ).\n\n( O pulse el botón [E] para ejemplo con valores por omisión. )";
            else
                lbExplicacion.Text = " Introducir el vector perpendicular y el punto de paso del plano, y el punto del que se quiere conocer su distancia al plano ( enteros o racionales ).";
            lbExplicacion.Width = btSalir.Location.X;
            tbPunto1X.Focus();
            pnDatos.Show();
            pnDatos.Location = new Point(ventanagrafica.Ventana.Location.X + ventanagrafica.Ventana.Width, ventanagrafica.Ventana.Location.Y);
            lbRecta1.Text = "Plano:";
            lbRotuloA.Text = "Vector perpendicular:";
            lbRotuloA.Location = new Point(lbRotuloA.Location.X-45, lbRotuloA.Location.Y);
            lbRotuloA.Font = new Font(lbRotuloA.Font.FontFamily, 9);
            lbRotuloA.AutoSize = true;
            label35.Text = "i";
            label36.Text = "j";
            label37.Text = "k";
            lbRotuloB.Text = "Punto de paso:";
            lbRotuloB.Location = new Point(lbRotuloB.Location.X - 15, lbRotuloB.Location.Y + 20);
            lbRotuloB.Font = lbRotuloA.Font;
            lbRotuloB.AutoSize = true;
            pnDatos.Controls.Add(label20);
            pnDatos.Controls.Add(label21);
            pnDatos.Controls.Add(label22);
            label20.Show();
            label21.Show();
            label22.Show();
            label20.Location = new Point(label35.Location.X, tbPunto1X.Location.Y + tbPunto1X.Height + 5);
            label21.Location = new Point(label36.Location.X, label20.Location.Y);
            label22.Location = new Point(label37.Location.X, label20.Location.Y);
            label20.Font = label35.Font;
            label21.Font = label20.Font;
            label22.Font = label20.Font;
            label20.BackColor = Color.Transparent;
            label21.BackColor = Color.Transparent;
            label22.BackColor = Color.Transparent;
            label20.Text = "X";
            label21.Text = "Y";
            label22.Text = "Z";
            tbPunto2X.Location = new Point(tbPunto2X.Location.X, label20.Location.Y + label20.Height + 5);
            tbPunto2Y.Location = new Point(tbPunto2Y.Location.X, tbPunto2X.Location.Y);
            tbPunto2Z.Location = new Point(tbPunto2Z.Location.X, tbPunto2X.Location.Y);
            lbRecta2.Text = "Punto:";
            lbRecta2.Location = new Point(lbRecta2.Location.X, tbPunto2X.Location.Y + tbPunto2X.Height + 5);
            lbPuntoDePaso.Hide();
            tbPunto3X.Location = new Point(tbPunto3X.Location.X, lbRecta2.Location.Y + lbRecta2.Height + 5);
            tbPunto3Y.Location = new Point(tbPunto3Y.Location.X, tbPunto3X.Location.Y);
            tbPunto3Z.Location = new Point(tbPunto3Z.Location.X, tbPunto3X.Location.Y);
            tbPunto3X.BackColor = tbPunto2X.BackColor;
            tbPunto3Y.BackColor = tbPunto2X.BackColor;
            tbPunto3Z.BackColor = tbPunto2X.BackColor;
            pnDatos.Controls.Add(label10);
            pnDatos.Controls.Add(label11);
            pnDatos.Controls.Add(label12);
            label10.Show();
            label11.Show();
            label12.Show();
            label10.Font = label20.Font;
            label11.Font = label10.Font;
            label12.Font = label11.Font;
            label10.BackColor = Color.Transparent;
            label11.BackColor = Color.Transparent;
            label12.BackColor = Color.Transparent;
            label10.Location = new Point(label20.Location.X, tbPunto3X.Location.Y - 20);
            label11.Location = new Point(label21.Location.X, label10.Location.Y);
            label12.Location = new Point(label22.Location.X, label10.Location.Y);
            label10.Text = "X";
            label11.Text = "Y";
            label12.Text = "Z";
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
            DistanciaPuntoPlano nuevo = new DistanciaPuntoPlano(directa);
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
            else if (paso == 2)
                SeguirResolucion();
            else
                FinalizarResolucion();
            paso++;
        }

        /// <summary>
        /// 
        /// DIBUJA EL PLANO, EL PUNTO  Y INICIA/ LA EXPLICACION
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
                //Vector perpendicular
                tbPunto1X.Text = "2";
                tbPunto1Y.Text = "6";
                tbPunto1Z.Text = "3";
                //Punto de paso
                tbPunto2X.Text = "15";
                tbPunto2Y.Text = "26";
                tbPunto2Z.Text = "10";
                // Punto del que se quiere encontrar la distancia al plano
                tbPunto3X.Text = "50";
                tbPunto3Y.Text = "60";
                tbPunto3Z.Text = "45";
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


            // Crear el vector perpendicular al plano
            perpendicular = new Vector(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text);
            // Crear el punto de paso del plano
           puntopasoplano = new Punto (tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);
            // Crear el punto del que se quiere encontrar la distancia al plano
            punto = new Punto(tbPunto3X.Text + " " + tbPunto3Y.Text + " " + tbPunto3Z.Text);
            // Crear el plano
             plano = new Plano(perpendicular,puntopasoplano);
             // Crear la recta perpendicular al plano y que pasa por el punto
             recta = new Recta(punto, plano.VectorPerpendicular);
            // Obtener el punto de interseccion de la recta con el plano
             puntointerseccion = plano.Interseccion(recta);

            // Punto oculto para establecer la escala
             Punto ficticio = plano.ADistanciaXY(35, 35);
             if (ficticio != null)
                 ventanagrafica.PintarVector(ficticio, ficticio, Color.Black, 1, false);
             else
             {
                 ficticio = new Punto("35 35 35");
                 ventanagrafica.PintarVector(ficticio, ficticio, Color.Black, 1, false);
             }
          
            if (directa)
                lbExplicacion.Hide();

            lbExplicacion.Text = " La distancia entre el punto y el plano, es el módulo del vector que vá desde el punto inicial, al punto donde una recta perpendicular al plano y que pasa por el punto inicial, corta al plano.";

            // Calcular el tamaño del plano para que pase por el punto de interseccion con la recta perpendicular

            Racional dist = plano.PuntoDePaso.Distancia(puntointerseccion);

            int tamaño = (int)dist.ToDouble();
            if (tamaño == 0)
                tamaño = 5;

            // Pintar el plano
            ventanagrafica.PintarPlano(plano.VectorPerpendicular, puntointerseccion, 50 + tamaño, Color.Chartreuse);

            // Pintar el punto
           ventanagrafica.PintarPunto(punto,10,true, Color.Coral);

           if (!directa)
               btAjustar.PerformClick();
               btIsometrica.PerformClick();

            if (directa)
                ContinuarResolucion();
            paso = 1;
        }


        /// <summary>
        /// 
        /// DIBUJA LA RECTA PERPENDICULAR AL PLANO QUE PASA POR EL PUNTO, MUESTRA LA ECUACION DE LA
        /// RECTA Y CONTINUA LA EXPLICACION
        /// 
        /// </summary>
        /// 
        private void ContinuarResolucion()
        {
            lbExplicacion.Text = "Usando el punto del que se quiere encontrar la distancia al plano como punto de paso, y el vector perpendicular al plano como vector paralelo, contruimos la ecuación de la recta que pasa por el punto y es perpendicular al plano.";

            // Mostrar la ecuacion de la recta en la etiqueta
            label1.Show();
            label1.Location = new Point(btContinuar.Location.X + btContinuar.Width + 5, btContinuar.Location.Y);
            label1.Font = new Font (label1.Font.FontFamily,12);
            label1.BackColor = Color.Transparent;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Text = "Ecuaciones paramétricas de la recta:\n";
            foreach (Ecuacion e in recta.EcuacionesParametricas() )
                label1.Text += e.ToString() + "\n";

            Racional distancia = plano.Distancia(punto);

            // Dibujar la recta
           Punto a = recta.SituacionPunto(distancia);
           Punto b = recta.SituacionPunto(-distancia);
            ventanagrafica.PintarLinea(a, b, Color.Red, 2);
            if (directa)
                SeguirResolucion();
            if (!directa)
                ventanagrafica.Ventana.Invalidate();
            
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
            
            lbExplicacion.Text = " Una vez obtenida la ecuación de la recta, hay que calcular el punto de intersección de la recta con el plano. ( Ver como calcular el punto de intersección entre recta y plano en el menú anterior. )";
            
            // Definir el punto de interseccion
            puntointerseccion = plano.Interseccion(recta);
      
            // Pintar el punto de interseccion 
            ventanagrafica.PintarPunto(puntointerseccion, 10, false, Color.Yellow);

            // Mostrar el punto de interseccion en la etiqueta
            label1.Text += "\n Punto de Intersección entre recta y plano: \n" + Math.Round(puntointerseccion.X.ToDouble(),4).ToString() + " X ; " + Math.Round(puntointerseccion.Y.ToDouble(),4).ToString() + " Y ; " + Math.Round(puntointerseccion.Z.ToDouble(),4).ToString() + " Z";


            if (!directa)
            {
                btZoomMas.PerformClick();
                btZoomMenos.PerformClick();
            }

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
          
            btContinuar.Hide();
            lbExplicacion.Text = "Por último, construimos el vector entre el punto del que queremos saber la distancia al plano , y el punto de intersección de la recta con el plano.\nLa distancia buscada, es el módulo de este vector.";

            // Construir el vector entre los puntos
            /*
            puntointerseccion.Coordenadas[0] = new Racional(Math.Round(puntointerseccion.X.ToDouble(), 5));
            puntointerseccion.Coordenadas[1] = new Racional(Math.Round(puntointerseccion.Y.ToDouble(), 5));
            puntointerseccion.Coordenadas[2] = new Racional(Math.Round(puntointerseccion.Z.ToDouble(), 5));
            Vector distancia = new Vector(punto, puntointerseccion);
        
            RacionalRadical dist = distancia.Modulo();
        */
 
            // Dibujar el vector 
            try
            {
                ventanagrafica.PintarVector(punto, puntointerseccion, Color.Chartreuse, 8, false);
            }
            catch (OverflowException)
            {
  
            }
          
            // Mostrar el resultado
            lbResultado.Show();
            lbResultado.Text = "Distancia entre el punto y el plano: \n" + Math.Round(plano.Distancia(punto).ToDouble(),6).ToString();
            lbResultado.Location = new Point(5, lbExplicacion.Height + 5);
            ventanagrafica.Ventana.Invalidate();
        }


    }
}




    

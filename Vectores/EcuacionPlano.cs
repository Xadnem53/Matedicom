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
    public partial class EcuacionPlano : FormularioBase
    {
         new  bool directa; // Tipo de resolucion
       new bool defecto = false; // Será true si se pulsa el boton DEF para usar valores por defecto
        EspacioIsometrico ventanagrafica; // Ventana grafica
        Punto punto1; // Punto de paso del plano
        Vector vector; // Vector perpendicular al plano
        Plano plano; // Plano del que se va a construir la ecuacion
        new int paso = 0; // Paso en el que se encuentra la resolucion
        Ecuacion ecuacion; // Será la ecuacion del plano

        public EcuacionPlano(bool resolucion)
        {
            directa = resolucion;
        }


        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = "Ecuación de un plano.";
            if (directa)
                btDefecto.Hide();
            ventanagrafica = new EspacioIsometrico(830, 640, new Point(5, 80));
            Controls.Add(ventanagrafica.Ventana);
            lbExplicacion.Show();
            lbExplicacion.Text = " Introducir las coordenadas del punto de paso del plano, y el vector perpendicular al mismo ( enteros o racionales ).\n\n( O pulse el botón [E] para ejemplo con valores por omisión. )";
            lbRotuloA.Text = "Punto de Paso:";
            lbRotuloA.BackColor = Color.DarkOrange;
            lbRotuloA.MaximumSize = new Size(130,150);
            lbRotuloA.AutoSize = true;
            lbRotuloA.Location = new Point(lbRotuloA.Location.X - 45, lbRotuloA.Location.Y);
            lbRotuloB.Location = new Point(lbRotuloA.Location.X, lbRotuloA.Location.Y + lbRotuloA.Height + 15);
            lbRotuloB.BackColor = Color.SlateBlue;
            lbRotuloB.MaximumSize = new Size(130, 150);
            lbRotuloB.AutoSize = true;
            lbRotuloB.Text = "Vector perpendicular:";
            tbPunto2X.Location = new Point(tbPunto2X.Location.X, lbRotuloB.Location.Y);
            tbPunto2Y.Location = new Point(tbPunto2Y.Location.X, tbPunto2X.Location.Y);
            tbPunto2Z.Location = new Point(tbPunto2Z.Location.X, tbPunto2X.Location.Y);
            labeli2.Location = new Point(labeli2.Location.X, tbPunto2X.Location.Y - 20);
            labelj2.Location = new Point(labelj2.Location.X, tbPunto2Y.Location.Y - 20);
            labelk2.Location = new Point(labelk2.Location.X, tbPunto2Z.Location.Y - 20);
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
            btDefecto.Location = new Point(pnDatos.Location.X + pnDatos.Width, pnDatos.Location.Y);
            pnDatos.Height = tbPunto2X.Location.Y + tbPunto2X.Height + 30;
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

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            EcuacionPlano nuevo = new EcuacionPlano(directa);
            nuevo.Show();
            this.Close();
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
                        if (!directa)
                            btContinuar.Visible = true;
                        IniciarResolucion();
                        btContinuar.Focus();
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
        /// LLAMA AL METODO ADECUADO, SEGUN EL PASO EN EL QUE SE ENCUENTRE LA RESOLUCION
        /// 
        /// </summary>
        /// 
        private void btContinuar_Click(object sender, EventArgs e)
        {
            if (paso == 1)
                ContinuarResolucion();
            else if (paso == 2)
                FinalizarResolucion();
            paso++;
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
        /// DIBUJA EL PUNTO DE PASO,  EL VECTOR PERPENDICULAR, EL PLANO E INICIA LA EXPLICACION
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
             // Establecer los valores por defecto
            if(defecto)
            {
                tbPunto1X.Text = "15"; tbPunto1Y.Text = "25"; tbPunto1Z.Text = "6";
                tbPunto2X.Text = "13"; tbPunto2Y.Text = "8"; tbPunto2Z.Text = "30";
            }
            punto1 = new Punto(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text);
            vector = new Vector(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);
            plano = new Plano(vector,punto1);

            lbExplicacion.Text = " Un plano en el espacio, se define por un punto de paso del plano, y un vector perpendicular al mismo.\n En la imagen puede verse el punto introducido en color naranja, el vector perpendicular en color azul y una porción del plano en color verde.";
 
            if (directa)
                lbExplicacion.Hide();

          // Dibujar el vector perpendicular al plano
            ventanagrafica.PintarVector(new Punto("0 0 0"), new Punto(vector.Componentes), Color.SlateBlue,5,true);
            //Dibujar el punto de paso del plano
            ventanagrafica.PintarPunto(punto1, 10, true, Color.DarkOrange);
         
            // Dibujar la porcion de plano
            ventanagrafica.PintarPlano(vector,punto1,50,Color.Chartreuse);

            if (directa)
                ContinuarResolucion();
            btIsometrica.PerformClick();
            btAjustar.PerformClick();
            if (defecto)
            {
                btArriba.PerformClick();
                btArriba.PerformClick();
            }
            paso = 1;
        }


          /// <summary>
        /// 
        /// CONTINUA LA EXPLICACION Y PLANTEA LA ECUACION DEL PLANO
        /// 
        /// </summary>
        /// 
        private void ContinuarResolucion()
        {
            
            lbExplicacion.Text = " El vector entre el punto de paso del plano, y cualquier punto contenido en el mismo, será perpendicular al vector perpendicular del plano.\n Por lo tanto el producto punto entre el vector perpendicular al plano y el vector entre el punto de paso del plano y cualquier punto contenido en el mimo, será igual a cero.\n Con esto, podemos plantear la ecuación del plano: ";
            label1.Show();
            label1.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + btContinuar.Height + 10);
            label1.Text = "ECUACIÓN DE ESTE PLANO";
            label1.BackColor = Color.SeaGreen;
            label1.Font = new Font(label1.Font.FontFamily, 10);
            if (!directa)
            label1.Text += "\n\n" + Racional.AString(vector.Componentes[0]) + " * (" + Racional.AString(punto1.Coordenadas[0]) + " - X ) + " + Racional.AString(vector.Componentes[1]) + " * ( " + Racional.AString(punto1.Coordenadas[1]) + " - Y ) + " + Racional.AString(vector.Componentes[2]) + " * ( " + Racional.AString(punto1.Coordenadas[2]) + " - Z ) = 0";
            if (directa)
                FinalizarResolucion();
             
        }

        /// <summary>
        ///
        /// REALIZA LAS OPERACIONES PARA LLEGAR A LA ECUACION DEL PLANO SIMPLIFICADA Y FINALIZA LA 
        /// EXPLICACION
        /// 
        /// 
        /// </summary>
        /// 
        private void FinalizarResolucion()
        {

            lbExplicacion.Text = " Una vez planteada la ecuación, podemos continuar realizando los productos y sumas y despejar el termino independiente. \n Con esto obtenemos la ecuación simplificada de este plano.";
            if (!directa)
                label1.Text += "\n" + Racional.AString(vector.Componentes[0] * punto1.Coordenadas[0]) + " - " + Racional.AString(vector.Componentes[0]) + "X +" + Racional.AString(vector.Componentes[1] * punto1.Coordenadas[1]) + " - " + Racional.AString(vector.Componentes[1]) + "Y +" + Racional.AString(vector.Componentes[2] * punto1.Coordenadas[2]) + " - " + Racional.AString(vector.Componentes[2]) + "Z = 0";
            label2.Show();
            label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 10);
            label2.BackColor = Color.Chartreuse;
            Plano plano = new Plano(vector, punto1);
            ecuacion = plano.EcuacionDelPlano();
            label2.Text = ecuacion.ToString();
            int altoletra = 400 / label2.Text.Length;
            label2.Font = new Font("Dejavu Sans", altoletra);
            btContinuar.Hide();
            btCentrar.PerformClick();
            if (defecto)
            {
                btArriba.PerformClick();
                btArriba.PerformClick();
            }

        }
   





    }
}

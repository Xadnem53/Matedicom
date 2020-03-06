using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Punto_y_Vector;
using Matematicas;

namespace Matedicom
{
    public partial class Distancia : FormularioBase
    {
        // Atributos
        new bool directa = false; // Tipo de resolucion firecta (true ) o paso a paso ( false )
         Punto punto1;
         Punto punto2;
         EspacioIsometrico ventanagrafica;
        new bool defecto = false; // Es true si se pulsa el boton de valores por defecto

         public override void Cargar(object sender, EventArgs e)
         {
             this.Text = "Distancia entre dos puntos.";
             ventanagrafica = new EspacioIsometrico(830, 640, new Point(5, 80));
             Controls.Add(ventanagrafica.Ventana);
             lbExplicacion.Show();
             lbExplicacion.Text = " Introducir las coordenadas de los puntos ( enteros o racionales ).\n\n( O pulse el botón [E] para ejemplo con valores por omisión. )";
             lbExplicacion.Width = btSalir.Location.X;
             tbPunto1X.Focus();
             pnDatos.Show();
             pnDatos.Location = new Point(ventanagrafica.Ventana.Location.X+ventanagrafica.Ventana.Width, ventanagrafica.Ventana.Location.Y);
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
             lbTituloAnguloX.Location = new Point(ventanagrafica.Ventana.Width + 10, pnDatos.Location.Y + pnDatos.Height+5);
             lbAnguloX.Location = new Point(lbTituloAnguloX.Location.X + lbTituloAnguloX.Width + 10, lbTituloAnguloX.Location.Y); 
             sbAngulox.Location = new Point(lbTituloAnguloX.Location.X, lbAnguloX.Location.Y + lbAnguloX.Height + 5);
             lbTituloAnguloY.Location = new Point(ventanagrafica.Ventana.Width + 10, sbAngulox.Location.Y + sbAngulox.Height + 5);
             lbAnguloY.Location = new Point(lbTituloAnguloY.Location.X + lbTituloAnguloY.Width +10, lbTituloAnguloY.Location.Y);
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

        public Distancia(bool resolucion)
        {
           directa = resolucion;
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
            Distancia nuevo = new Distancia(directa);
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
                if (caja.Text.Length == 0)
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
                        this.AcceptButton = btContinuar;
                        // Siguiente metodo
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
        /// CONTRUYE LOS PUNTOS CON LOS VALORES INTRODUCIDOS EN LAS CAJAS DE TEXTO, CAMBIA EL TEXTO
        /// DEL BOTON ACEPTAR POR CONTINUAR , LO CAMBIA A COLOR VERDE OSCURO Y DIBUJA LOS PUNTOS EN
        /// EL AREA GRAFICA
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
            
            if (!defecto) // Si no se ha pulsado el boton de resolucion con valores por defecto
            {
                punto1 = new Punto(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + tbPunto1Z.Text);
                punto2 = new Punto(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);
                btDefecto.Hide();
            }
            else
            {
                punto1 = new Punto("10 26 25");
                tbPunto1X.Text = Racional.AString(punto1.X);
                tbPunto1Y.Text = Racional.AString(punto1.Y);
                tbPunto1Z.Text = Racional.AString(punto1.Z);
                punto2 = new Punto("25 -30 -40");
                tbPunto2X.Text = Racional.AString(punto2.X);
                tbPunto2Y.Text = Racional.AString(punto2.Y);
                tbPunto2Z.Text = Racional.AString(punto2.Z);
                ventanagrafica.Escala = 0.85F;
                btIsometrica.PerformClick();
               // sbAngulox.Value = 30;
              //  sbAnguloY.Value = -25;
              //  sbAnguloZ.Value = 65;
            }

            
            if (!directa)
            {
                ventanagrafica.PintarPunto(punto1, 10, true, Color.Red);
                ventanagrafica.PintarPunto(punto2,10,true, Color.GreenYellow);
                lbExplicacion.Text = " La distancia entre los dos puntos, es igual a la raiz cuadrada de la suma de los cuadrados de las diferencias de sus coordenadas respectivas. \nEs decir: Distancia = √ ( " + tbPunto1X.Text + " - " + tbPunto2X.Text + ")^2 + ( " + tbPunto1Y.Text + " - " + tbPunto2Y.Text + " )^2 + ( " + tbPunto1Z.Text + " - " + tbPunto2Z.Text + " )^2";
            }
            if (directa)
            {
                lbExplicacion.Text = " La distancia entre los puntos es: " + new Vector(punto1, punto2).ModuloDecimal().ToString();
                ventanagrafica.PintarVector(punto1, punto2, Color.CadetBlue, 5, true);
                btContinuar.Hide();
            }
           
            if (!defecto)
            {
                btAjustar.PerformClick();
                btIsometrica.PerformClick();
            }
        }
  
        /// <summary>
        /// 
        /// CONTINUA CON EL SEGUNDO PASO DE LA EXPLICACION EN EL METODO PASO A PASO
        /// 
        /// </summary>
        /// 
        private void btContinuar_Click(object sender, EventArgs e)
        {
            lbExplicacion.Text += " \n Esto es así, porque la distancia entre los dos puntos es la hipotenusa del triangulo rectangulo formado por el vector entre los puntos y sus proyecciones sobre los planos ortogonales.";
            // Pintar el vector 
            ventanagrafica.PintarVector(punto1,punto2,Color.Chartreuse,6,true);
            string pto1X;

            if (punto1.X.ToString() != "")
                pto1X = punto1.X.ToString();
            else
                pto1X = "0";
           
            string pto1Y;
            if (punto1.Y.ToString() != "")
                pto1Y = punto1.Y.ToString();
            else
                pto1Y = "0";
            
            string pto1Z;
            if (punto1.Z.ToString() != "")
                pto1Z = punto1.Z.ToString();
            else
                pto1Z = "0";

            string pto2X;
            if (punto2.X.ToString() != "")
                pto2X = punto2.X.ToString();
            else
                pto2X = "0";

            string pto2Y;
            if (punto2.Y.ToString() != "")
                pto2Y = punto2.Y.ToString();
            else
                pto2Y = "0";

            string pto2Z;
            if (punto2.Z.ToString() != "")
                pto2Z = punto2.Z.ToString();
            else
                pto2Z = "0";
           
            // Pintar las lineas que forman los catetos del triangulo rectangulo
            if (punto2.Z > punto1.Z)
            {
                ventanagrafica.PintarLinea(punto1,new Punto(pto2X + " " + pto2Y + " " + pto1Z),Color.Coral,0.5F);
                ventanagrafica.PintarLinea(new Punto(pto2X + " " + pto2Y + " " + (pto1Z)), punto2, Color.Aquamarine,0.5f);
                ventanagrafica.PintarString(new Punto(new Racional[] { (punto1.X + punto2.X) / 2, (punto1.Y + punto2.Y) / 2, punto1.Z }), Color.Coral, 13, "√ ( (" + Racional.AString(punto1.X) + " - " + Racional.AString(punto2.X) + " )^2 + ( " + Racional.AString(punto1.Y) + " - " + Racional.AString(punto2.Y) + " )^2 )");
                ventanagrafica.PintarString(new Punto(new Racional[] { punto2.X, punto2.Y, (punto1.Z + punto2.Z) / 2 }), Color.Aquamarine, 13, Racional.AString(punto1.Z) + " - " + Racional.AString(punto2.Z));
            }
            else
            {
                ventanagrafica.PintarLinea(punto2, new Punto(pto1X + " " + pto1Y + " " + pto2Z), Color.Coral, 0.5f);
                ventanagrafica.PintarLinea(new Punto(pto1X + " " + pto1Y + " " + pto2Z), punto1, Color.Aquamarine, 0.5f);
                ventanagrafica.PintarString(new Punto(new Racional[]{ ( punto1.X+punto2.X)/2, (punto1.Y+punto2.Y)/2,punto2.Z}), Color.Coral, 13, "√ ( (" + Racional.AString(punto1.X) + " - " + Racional.AString(punto2.X) + " )^2 + ( " + Racional.AString(punto1.Y) + " - " + Racional.AString(punto2.Y) + " )^2 )");
                ventanagrafica.PintarString(new Punto(new Racional[] { punto1.X, punto1.Y, (punto1.Z + punto2.Z) / 2 }), Color.Aquamarine, 13, Racional.AString(punto1.Z) + " - " + Racional.AString(punto2.Z));
            }
        
            btContinuar.Click -= btContinuar_Click;
            btContinuar.Click += btContinuar2_Click;
            for (int i = 0; i < 3; i++)
                btArriba.PerformClick();
            ventanagrafica.Ventana.Invalidate();
             
        }
        
        /// <summary>
        /// 
        /// REALIZA EL ULTIMO PASO EN LA RESOLUCION PASO A PASO
        /// 
        /// </summary>
        /// 
        private void btContinuar2_Click(object sender, EventArgs e)
        {
            lbResultado.Location = new Point(sbAnguloZ.Location.X, pnZoom.Location.Y + pnZoom.Height + 5);
            lbResultado.Visible = true;
            lbResultado.Font = new Font(lbResultado.Font.FontFamily, 12);
            lbResultado.Text = " Por lo tanto en este caso: ";
            lbResultado.Text += "\n √ [  ( √ ( (" + Racional.AString(punto1.X) + " - " + Racional.AString(punto2.X) + " )^2 + ( " + Racional.AString(punto1.Y) + " - " + Racional.AString(punto2.Y) + " )^2 )^2 + (" + Racional.AString(punto1.Z) + " - " + Racional.AString(punto2.Z) + " )^2  ]";
            lbResultado.Text += "\n Lo que es igual a: ";
            lbResultado.Text += "\n √ ( (" + Racional.AString(punto1.X) + " - " + Racional.AString(punto2.X) + " )^2 + ( " + Racional.AString(punto1.Y) + " - " + Racional.AString(punto2.Y) + " )^2 )  + ( " + Racional.AString(punto1.Z) + " - " + Racional.AString(punto2.Z) + " )^2 )";
            Vector resul = new Vector(punto1, punto2);
            lbResultado.Text += "\n La distancia es: " + resul.ModuloDecimal().ToString();
            btContinuar.Hide();
             
        }

    
    }
}

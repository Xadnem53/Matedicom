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
    public partial class InterseccionPlanos : FormularioBase
    {
        new bool directa; // Tipo de resolucion
        new bool defecto = false; // Será true si se pulsa el boton DEF para usar valores por defecto
        EspacioIsometrico ventanagrafica; // Ventana grafica

        Punto punto1; // Punto de paso del primer plano
        Punto punto2; // Punto de paso del segundo plano
        Punto oculto; // Punto de oculto para ajustar la escala
        new int paso = 0; // Paso en el que se encuentra la resolucion

        Vector vector1; // Vector perpendicular al plano1
        Vector vector2; // Vector perpendicular al plano2

        Plano plano1;
        Plano plano2;

        Vector paralelo; // Será el vector paralelo a la linea de interseccion entre los planos
         Recta interseccion; // Será la linea de interseccion entre los planos
         ControlesFlotantes flotante; // Para mostrar el desarrollo de la resolucion.

        public InterseccionPlanos(bool direc)
        {
            directa = direc;
        }

        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = "Recta de intersección entre dos planos.";
            ventanagrafica = new EspacioIsometrico(830, 640, new Point(5, 80));
            Controls.Add(ventanagrafica.Ventana);
            lbExplicacion.Show();
            if(!directa)
            lbExplicacion.Text = " Introducir las coodenadas del punto de paso y el vector perpendicular de cada plano. ( enteros o racionales ) .\n\n( O pulse el botón [E] para ejemplo con valores por omisión.";
            if( directa)
                lbExplicacion.Text = " Introducir las coodenadas del punto de paso y el vector perpendicular de cada plano. ( enteros o racionales ) .";
            lbExplicacion.Width = btSalir.Location.X;
            tbPunto1X.Focus();
            pnDatos.Show();
            pnDatos.Location = new Point(ventanagrafica.Ventana.Location.X + ventanagrafica.Ventana.Width, ventanagrafica.Ventana.Location.Y);
            btDefecto.Location = new Point(pnDatos.Location.X + pnDatos.Width, pnDatos.Location.Y);
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

            lbTituloAnguloX.Location = new Point(ventanagrafica.Ventana.Width + 10, pnDatos.Location.Y + pnDatos.Height + 30);
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
            tbPunto2X.Location = new Point(tbPunto1X.Location.X, labeli2.Location.Y + labeli2.Height + 5);
            tbPunto2Y.Location = new Point(tbPunto1Y.Location.X, tbPunto2X.Location.Y);
            tbPunto2Z.Location = new Point(tbPunto1Z.Location.X, tbPunto2X.Location.Y);

            lbRecta1.Text = "Plano 1";
            label35.Text = "x";
            label36.Text = "y";
            label37.Text = "z";

            lbRotuloA.Text = "Punto de paso:";
            lbRotuloA.AutoSize = true;
            lbRotuloA.BackColor = Color.Transparent;
            lbRotuloA.Location = new Point(lbRotuloA.Location.X - 50, lbRotuloA.Location.Y);

            labeli2.Location = new Point(label35.Location.X, tbPunto1X.Location.Y + tbPunto1X.Height + 5);
            labelj2.Location = new Point(label36.Location.X, labeli2.Location.Y);
            labelk2.Location = new Point(label37.Location.X, labeli2.Location.Y);

            tbPunto2X.Location = new Point(tbPunto1X.Location.X, labeli2.Location.Y + labeli2.Height + 5);
            tbPunto2Y.Location = new Point(tbPunto1Y.Location.X, tbPunto2X.Location.Y);
            tbPunto2Z.Location = new Point(tbPunto1Z.Location.X, tbPunto2X.Location.Y);

            lbRotuloB.Text = "Vector normal:";
            lbRotuloB.AutoSize = true;
            lbRotuloB.Location = new Point(lbRotuloA.Location.X, tbPunto2X.Location.Y);
            lbRotuloB.BackColor = Color.Transparent;

            lbRecta2.Text = "Plano 2:";
            lbRecta2.BackColor = Color.SlateBlue;
            lbRecta2.Location = new Point(lbRecta1.Location.X, tbPunto2X.Location.Y + tbPunto2X.Height + 5);

            label20.Show();
            label20.BackColor = Color.Transparent;
            pnDatos.Controls.Add(label20);
            label20.Font = label35.Font;
            label20.Text = "x";
            label20.Location = new Point(label35.Location.X, lbRecta2.Location.Y + 5);
            label21.Show();
            label21.BackColor = Color.Transparent;
            pnDatos.Controls.Add(label21);
            label21.Font = label20.Font;
            label21.Location = new Point(label36.Location.X, label20.Location.Y);
            label21.Text = "y";
            label22.Show();
            label22.BackColor = Color.Transparent;
            pnDatos.Controls.Add(label22);
            label22.Font = label21.Font;
            label22.Location = new Point(label37.Location.X, label21.Location.Y);
            label22.Text = "z";

            tbPunto3X.Location = new Point(tbPunto3X.Location.X, label22.Location.Y + label22.Height + 5);
            tbPunto3Y.Location = new Point(tbPunto3Y.Location.X, tbPunto3X.Location.Y);
            tbPunto3Z.Location = new Point(tbPunto3Z.Location.X, tbPunto3X.Location.Y);

            lbPuntoDePaso.Text = "Punto de paso:";
            lbPuntoDePaso.Location = new Point(lbRotuloA.Location.X, tbPunto3X.Location.Y);

            label23.Show();
            label23.BackColor = Color.Transparent;
            pnDatos.Controls.Add(label23);
            label23.Font = label35.Font;
            label23.Text = "i";
            label23.Location = new Point(label35.Location.X, tbPunto3X.Location.Y + tbPunto3X.Height + 5);
            label24.Show();
            label24.BackColor = Color.Transparent;
            pnDatos.Controls.Add(label24);
            label24.Font = label20.Font;
            label24.Location = new Point(label36.Location.X, label23.Location.Y);
            label24.Text = "j";
            label25.Show();
            label25.BackColor = Color.Transparent;
            pnDatos.Controls.Add(label25);
            label25.Font = label21.Font;
            label25.Location = new Point(label37.Location.X, label24.Location.Y);
            label25.Text = "k";

            tbPunto4X.Location = new Point(tbPunto4X.Location.X, label25.Location.Y + label25.Height + 5);
            tbPunto4Y.Location = new Point(tbPunto4Y.Location.X, tbPunto4X.Location.Y);
            tbPunto4Z.Location = new Point(tbPunto4Z.Location.X, tbPunto4X.Location.Y);

            lbVectorNormal.Text = "Vector normal:";
            lbVectorNormal.Location = new Point(lbPuntoDePaso.Location.X, tbPunto4X.Location.Y);

            pnDatos.Height = tbPunto4X.Location.Y + tbPunto2X.Height + 15;
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
            if (flotante != null)
                flotante.Close();
        }

        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            InterseccionPlanos nuevo = new InterseccionPlanos(directa);
            if (flotante != null)
                flotante.Close();
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
                          IniciarResolucion();
                            this.AcceptButton = this.btContinuar;
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
        /// LLAMA AL METODO ADECUADO SEGUN EN QUE PASO SE ENCUENTRE LA RESOLUCION PASO A PASO
        /// 
        /// </summary>
        /// 
        private void btContinuar_Click(object sender, EventArgs e)
        {
             if (paso == 1)
                ContinuarResolucion();
            else if (paso == 2)
                PrepararResultado();
            else if (paso == 3)
                FinalizarResolucion();
            paso++;
        }

        /// <summary>
        /// 
        /// DIBUJA LOS PLANOS
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
                //Punto de paso 1
                tbPunto1X.Text = "0";
                tbPunto1Y.Text = "10";
                tbPunto1Z.Text = "10";
                //Vector perpendicular 1
                tbPunto2X.Text = "0";
                tbPunto2Y.Text = "10";
                tbPunto2Z.Text = "0";
                //Punto de paso 2
                tbPunto3X.Text = "0";
                tbPunto3Y.Text = "0";
                tbPunto3Z.Text = "20";
                //Vector perpendicular 2
                tbPunto4X.Text = "0";
                tbPunto4Y.Text = "0";
                tbPunto4Z.Text = "10";

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
            punto2 = new Punto(tbPunto3X.Text + " " + tbPunto3Y.Text + " " + tbPunto3Z.Text);
            vector1 = new Vector(tbPunto2X.Text + " " + tbPunto2Y.Text + " " + tbPunto2Z.Text);
             vector2 = new Vector(tbPunto4X.Text + " " + tbPunto4Y.Text + " " + tbPunto4Z.Text);

             lbExplicacion.Text = " Todo punto de la línea de intersección entre los dos planos, pertenecerá a ambos. Por lo tanto,todo punto de la recta de intersección cumplirá las ecuaciones de los dos planos.";
            if (directa)
                lbExplicacion.Hide();
           // Construir los planos
            plano1 = new Plano(vector1, punto1);
            plano2 = new Plano(vector2, punto2);
                     
            // Mostrar las ecuaciones de los planos
            label1.Show();
            label1.Location = new Point(btContinuar.Location.X + btContinuar.Width + 5, btContinuar.Location.Y);
            label1.Font = new Font(label1.Font.FontFamily, 10);
            label1.BackColor = Color.Transparent;
            label1.Text = "Ecuación del plano1: \n" + plano1.EcuacionDelPlano().ToString() + " \n\n Ecuación del plano2: \n" + plano2.EcuacionDelPlano().ToString();
       
            
            // Dibujar los planos
            Vector escalado1 = new Vector(vector1);
            escalado1.MultiplicarPorEscalar(10);
            ventanagrafica.PintarPlano(escalado1, punto1,50,Color.DarkOrange);
            ventanagrafica.PintarPlano(vector2, punto2, 50, Color.SlateBlue);

            // Punto oculto para que se ajuste la escala al tamaño de los planos
            Vector posicion1 = new Vector(plano1.PuntoDePaso.Coordenadas); // Vector de posicion del punto de paso del 1er plano
            Vector posicion2 = new Vector(plano2.PuntoDePaso.Coordenadas); // Vector de posicion del punto de paso del 2º plano
            if (posicion1.ModuloDecimal() > posicion2.ModuloDecimal())
            {
                Vector ampliado = new Vector(posicion1);
                ampliado.MultiplicarPorEscalar(10);
                oculto = new Punto(ampliado.Componentes);
            }
            else
            {
                Vector ampliado = new Vector(posicion1);
                ampliado.MultiplicarPorEscalar(2);
                oculto = new Punto(ampliado.Componentes);
            }
            ventanagrafica.PintarPunto(oculto, 1, false, Color.Black);

           btIsometrica.PerformClick();
           btAjustar.PerformClick();

            if (directa)
                ContinuarResolucion();
            paso = 1;
        }


        /// <summary>
        /// 
        /// CONTRUYE EL SISTEMA IGUALANDO LAS ECUACIONES DE CADA
        /// PLANO, PINTA EL PUNTO DE PASO DE LA RECTA DE INTERSECCION  Y CONTINUA LA EXPLICACION
        /// 
        /// </summary>
        /// 
        private void ContinuarResolucion()
        {
            // Construir la recta de interseccion
            interseccion = plano1.Interseccion(plano2);

            // Determinar la variable que se consideró nula
            int contador = 0;
            char variablenula ='\0';
            foreach (Racional r in interseccion.PuntoDePaso.Coordenadas)
            {
                if (r.Numerador == 0)
                    variablenula = (char)((int)'X' + contador);
                contador++;
            }

            lbExplicacion.Text = " Obtenemos un punto de paso de la línea de intersección, construyendo un sistema con las ecuaciones de cada plano y dando valor cero a una de las variables. En este caso, le damos valor cero a la coordenada " + variablenula + ".";

            if (!directa)
            {
                // Construir la ventana flotante
                flotante = new ControlesFlotantes();
                flotante.Show();
                flotante.Text = " ";
                flotante.StartPosition = FormStartPosition.Manual;
                flotante.Location = pnDatos.Location;
                flotante.Opacity = 1;

                // Mostrar el sistema de ecuaciones
                int nula = variablenula - 'X';
                label2.Visible = true;
                label2.AutoSize = true;
                label2.Font = new Font(label2.Font.FontFamily, 10);
                label2.BackColor = Color.Transparent;
                label2.Text = "Sistema con las ecuaciones de cada plano con : " + variablenula + " = 0.";
                label2.Text += "\n";
                for (int i = 0; i < plano1.EcuacionDelPlano().CantidadDeTerminosIzquierda; i++)
                {
                    if (plano1.EcuacionDelPlano().ObtenerTerminoIzquierda(i).Variables[0] != variablenula)
                        label2.Text += plano1.EcuacionDelPlano().ObtenerTerminoIzquierda(i).ToString();
                }
                label2.Text += " = " + Racional.AString(plano1.EcuacionDelPlano().ObtenerTerminoDerecha(0).Coeficiente);
                label2.Text += "\n";
                for (int i = 0; i < plano2.EcuacionDelPlano().CantidadDeTerminosIzquierda; i++)
                {
                    if (plano2.EcuacionDelPlano().ObtenerTerminoIzquierda(i).Variables[0] != variablenula)
                        label2.Text += plano2.EcuacionDelPlano().ObtenerTerminoIzquierda(i).ToString();
                }
                label2.Text += " = " + Racional.AString(plano2.EcuacionDelPlano().ObtenerTerminoDerecha(0).Coeficiente);
                flotante.Controls.Add(label2);
                label2.Location = new Point(5, 5);


                // Mostrar el punto de paso
                label2.Text += "\n\n X: " + Racional.AString(interseccion.PuntoDePaso.X) + "\n Y: " + Racional.AString(interseccion.PuntoDePaso.Y) + "\n Z: " + Racional.AString(interseccion.PuntoDePaso.Z);

                // Pintar el punto de paso de la recta de interseccion
                ventanagrafica.PintarPunto(interseccion.PuntoDePaso, 5, false, Color.Red);
                flotante.Size = new Size(flotante.Width, label2.Height + 65);
            }

            ventanagrafica.Ventana.Invalidate();
            if (!directa)
                btContinuar.Show();
            if (directa)
                PrepararResultado();
             
        }

        /// <summary>
        ///
        ///PINTA LOS VECTORES PERPENDICULARES DE LOS PLANOS , EL VECTOR PARALELO A LA LINEA DE
        ///INTERSECCION Y CONTINUA LA EXPLICACION
        /// 
        /// 
        /// </summary>
        /// 
        private void PrepararResultado()
        {
            
            lbExplicacion.Text = " Una vez obtenido un punto de paso de la recta de intersección, falta obtener el vector paralelo a esa recta.\nEl vector paralelo a la recta, ha de ser un vector perpendicular a los vectores perpendiculares de los dos planos. Para obtener este vector, podemos calcular el producto cruz entre los vectores perpendiculares de los planos.";
         
             // Obtener el vector perpendicular a los vectores perpendiculares de los planos
            paralelo = Vector.ProductoCruz(plano1.VectorPerpendicular, plano2.VectorPerpendicular);
            if (!directa)
            {
                label2.Text += "\n\nVector paralelo a la recta de intersección: \n" + paralelo.ToString();
                flotante.Height += 40;
            }
            // Construir la recta de interseccion
            interseccion = plano1.Interseccion(plano2);

            //Vector paralelo a la recta de interseccion
            paralelo = new Vector(new Punto("0 0 0"), new Punto(paralelo.Componentes));
            
            // Pintar los vectores perpendiculares 
            Vector perpendicular1 = new Vector ( new Punto("0 0 0"), new Punto(plano1.VectorPerpendicular.Componentes)); 
            Vector perpendicular2 = new Vector(new Punto("0 0 0"), new Punto(plano2.VectorPerpendicular.Componentes));
            

            ventanagrafica.PintarVector(interseccion.PuntoDePaso, new Punto(new Racional[3]{ perpendicular1.Componentes[0]+interseccion.PuntoDePaso.X, perpendicular1.Componentes[1]+interseccion.PuntoDePaso.Y,perpendicular1.Componentes[2]+interseccion.PuntoDePaso.Z}), Color.DarkOrange, 4, false);
             ventanagrafica.PintarVector(interseccion.PuntoDePaso, new Punto(new Racional[3] { perpendicular2.Componentes[0] + interseccion.PuntoDePaso.X, perpendicular2.Componentes[1] + interseccion.PuntoDePaso.Y, perpendicular2.Componentes[2] + interseccion.PuntoDePaso.Z }), Color.SlateBlue, 4, false);

             ventanagrafica.PintarVector(interseccion.PuntoDePaso, new Punto(new Racional[3] { paralelo.Componentes[0]/2 + interseccion.PuntoDePaso.X, paralelo.Componentes[1]/2 + interseccion.PuntoDePaso.Y, paralelo.Componentes[2]/2 + interseccion.PuntoDePaso.Z }), Color.Red, 4, false);
         
        
            ventanagrafica.Ventana.Invalidate();
              
      

                if (directa)
                    FinalizarResolucion();
             
        }
        


          /// <summary>
        ///
        /// PINTA LA LINEA DE INTERSECCION, MUESTRA LA ECUACION PARAMETRICA DE LA MISMA  Y FINALIZA
        /// LA EXPLICACION
        /// 
        /// 
        /// </summary>
        /// 
        private void FinalizarResolucion()
        {
            
            lbExplicacion.Text = " Con el punto de paso y el vector paralelo, ya tenemos definida la línea de intersección entre los planos.";

            //Pintar la linea de interseccion
            Punto ini = interseccion.SituacionPunto(100);
            Punto fin = interseccion.SituacionPunto(-100);
            ventanagrafica.PintarLinea(ini, fin, Color.Red, 2);

 

            // Mostrar la ecuacion parametrica de la recta de interseccion
            lbResultado.Text = "Ecuaciones paramétricas de la recta de intersección:\n";
            foreach (Ecuacion e in interseccion.EcuacionesParametricas())
            {
                lbResultado.Text += e.ToString() + "\n";
            }
            lbResultado.Show();
            lbResultado.Font = new Font(lbResultado.Font.FontFamily, 12);
            if (!directa)
                lbResultado.Location = new Point(5, lbExplicacion.Height);
            else if (directa)
                lbResultado.Location = lbExplicacion.Location;
            btContinuar.Hide();

            btCentrar.PerformClick();
             
        }


    }
}

  
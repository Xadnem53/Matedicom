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
using Recta_y_plano;

namespace Matedicom
{
    public partial class Circunferencia : FormularioBase
    {
       new bool directa;
        
        Espacio2D ventanagrafica;
         Punto apintar; // Coordenadas del centro de la circunferencia
         Racional radio; // Radio de la circunferencia
        double anguloradio; // Angulo en que se dibujará el radio de la circunferencia
        int origenx;
        int origeny;
       new int paso = 0; // Registra el paso en el que se encuentra la explicacion paso a paso
        double angulo = 0; // Para guardar el angulo de giro de la linea de radio de la circunferencia

        public Circunferencia(bool resolucion)
        {
            directa = resolucion;
        }

        public override void Cargar(object sender, EventArgs e)
        {
            ventanagrafica = new Espacio2D(830, 635, new Point(5, 65));
            Controls.Add(ventanagrafica.Ventana);
            tbPunto1X.Focus();
            lbExplicacion.Text = " Introduzca el centro y radio de la circunferencia ( enteros o racionales )\n\n O Pulse el botón [E] para ejemplo con valores por omisión.";
            btContinuar.Click += btContinuar_Click;
            lbExplicacion.Show();

            pnDatos.Show();
            pnDatos.Location = new Point(ventanagrafica.Ventana.Width + 10, ventanagrafica.Ventana.Location.Y);
            if (!directa)
                btDefecto.Location = new Point(pnDatos.Location.X + pnDatos.Size.Width + 5, pnDatos.Location.Y);
            else
                btDefecto.Hide();

            lbPuntoDePaso.Hide();
            lbVectorNormal.Hide();
            this.lbRecta1.Hide();
            lbRecta2.Hide();
            label37.Hide();
            tbPunto1Z.Hide();
            tbPunto2Y.Hide();
            tbPunto2Z.Hide();
            tbPunto3X.Hide();
            tbPunto3Y.Hide();
            tbPunto3Z.Hide();
            tbPunto4X.Hide();
            tbPunto4Y.Hide();
            tbPunto4Z.Hide();

            tbPunto1X.KeyPress += Cajas_KeyPress;
            tbPunto1Y.KeyPress += Cajas_KeyPress;
            tbPunto2X.KeyPress += Cajas_KeyPress;
            sbValor.Value = 100;
            sbValor.Maximum = (int)(Math.PI * 200);
            sbValor.Minimum = (int)(Math.PI * -200);
            sbValor.LargeChange = 17;
            sbValor.SmallChange = 1;
            tbParametro.Text = "1";
            sbValor.ValueChanged += sbValor_ValueChanged;

            pnDatos.Size = new Size(pnDatos.Width, tbPunto2X.Location.Y + tbPunto2X.Height + 10 );
            
            lbRotuloA.Text = "Centro:";
            lbRotuloB.Text = "Radio";

            btIzquierda.Click += btIzquierda_Click;
            btArriba.Click += btArriba_Click;
            btDerecha.Click += btDerecha_Click;
            btAbajo.Click += btAbajo_Click;
            btCentrar.Click += btCentrar_Click;
            btZoomMas.Click += btZoomMas_Click;
            btZoomMenos.Click += btZoomMenos_Click;
          
            btContinuar.Location = new Point(pnDatos.Location.X, pnDatos.Location.Y + pnDatos.Height + 5);
            tbPunto1X.Focus();
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
            btDefecto.Hide();
            IniciarResolucion();
        }
        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            Circunferencia nuevo = new Circunferencia(directa);
            nuevo.Show();
            this.Close();
        }



        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN VALORES RACIONAL O ENTEROS EN LAS CAJAS DE TEXTO DE LAS
        /// COORDENADAS DEL CENTRO Y RADIO DE LA CIRCUNFERENCIA A DIBUJAR
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
                        this.tbPunto2X.Focus();
                    }
                    else if (caja == this.tbPunto2X)
                    {
                        e.Handled = true;
                        this.Focus();
                        btContinuar.Visible = true;
                        btContinuar.BackColor = Color.Chartreuse;
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
        
        ///<Summary>
        ///
        /// LLAMA AL METODO ADECUADO SEGUN EL PASO EN EL QUE SE ENCUENTRE LA RESOLUCION 
        /// 
        /// </Summary>
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
        /// CONTRUYE EL PUNTO CON LOS VALORES INTRODUCIDOS EN LAS CAJAS DE TEXTO, CAMBIA EL TEXTO
        /// DEL BOTON ACEPTAR POR CONTINUAR , LO CAMBIA A COLOR VERDE OSCURO y DIBUJA LOS PUNTOS EN
        /// EL AREA GRAFICA
        /// 
        /// </summary>
        /// 

        private void IniciarResolucion()
        {
            // Valores por defecto
            if(defecto)
            {
                tbPunto1X.Text = "5";
                tbPunto1Y.Text = "3";
                tbPunto2X.Text = "7";
            }
            // Mostrar los controles
            pnZoom.Show();
            btIzquierda.Show();
            btDerecha.Show();
            btArriba.Show();
            btAbajo.Show();
            btCentrar.Show();
            btZoomMas.Show();
            btZoomMenos.Show();
            lbZoomtitulo.Show();
            lbDesplazamiento.Show();
            pnZoom.Location = btContinuar.Location;
            pnParametro.Show();
            pnParametro.Location = new Point(btContinuar.Location.X , pnZoom.Location.Y + pnZoom.Height + 5);
            btContinuar.Location = new Point(btContinuar.Location.X, pnParametro.Location.Y + pnParametro.Height + 5);
            lbTituloParametro.Text = "Radianes:";
           
            lbPorPanel.Hide();
            lbIgualPanel.Hide();
            tbLargoFinal.Hide();
            lbLargoFinal.Hide();
            tbParametro.Location = new Point(tbParametro.Location.X - 50, tbParametro.Location.Y);

            lbTituloParametro.TextAlign = ContentAlignment.MiddleCenter;
            lbTituloParametro.AutoSize = true;
            lbSegmentoInicial.AutoSize = true;
            lbTituloParametro.Location = new Point(lbTituloParametro.Location.X - 30, lbTituloParametro.Location.Y);
            lbSegmentoInicial.Location = new Point(lbTituloParametro.Location.X + 80,lbTituloParametro.Location.Y);
            tbLargoInicial.Location = new Point(tbLargoInicial.Location.X + 100, tbLargoInicial.Location.Y);
            tbLargoInicial.Text = "57,29";
            lbSegmentoInicial.Text = "Grados: ";
            apintar = new Punto(tbPunto1X.Text + " " + tbPunto1Y.Text + " " + "0");
            radio = Racional.StringToRacional(tbPunto2X.Text);
            if (radio.Numerador < 0) // El radio no puede ser negativo
                radio = new Racional(radio.Numerador * -1, radio.Denominador);
            anguloradio = 1;
            ventanagrafica.PintarCirculo(apintar, radio, Color.Olive, 2,anguloradio);
            btCentrar.PerformClick();
            if (!directa)
            lbExplicacion.Text = " El radio de la circunferencia en un ángulo determinado, es la hipotenusa de un triángulo rectángulo.\n La imagen representa la circunferencia introducida junto con el triángulo rectángulo en el que: el radio es la hipotenusa, y el largo de los catetos cumple el teorema de Pitágoras.\n Se puede observar como cambia el largo de los catetos cambiando el ángulo en el que se situa la linea del radio pulsando los botones.";
            label1.Show();
            label1.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + btContinuar.Height + 5);
            label1.BackColor = Color.Transparent;
            label1.Font = new Font(label1.Font.FontFamily, 12);
            label1.Text = "Teorema de Pitágoras aplicado a esta circunferencia:\n";
            double radiodouble = radio.Numerador / radio.Denominador;
            label1.Text += Racional.AString(radio) + "= Ѵ (" + Math.Round(radiodouble * Math.Sin(1),2).ToString() + "^2 + " + Math.Round(radiodouble * Math.Cos(1),2).ToString() + "^2 )  " ;
            Controls.Add(ventanagrafica.Ventana);
            if (directa)
                ContinuarResolucion();
            paso = 1;
            ventanagrafica.Ventana.Invalidate();
        }


        /// <summary>
        /// 
        ///PINTA EL PUNTO FINAL DE LA LINEA DE RADIO Y LAS LINEAS CORREPONDIENTES Y CONTINUA CON LA
        ///EXPLICACION
        /// 
        /// </summary>
        /// 
        private void ContinuarResolucion()
        {
            ventanagrafica.EliminarCirculo(0);
            ventanagrafica.PintarCirculo(apintar, radio, Color.Olive, 2, anguloradio, true);
            double radiodouble = (double)(radio.Numerador/radio.Denominador);
            double centroxx = (double)apintar.X.Numerador / apintar.X.Denominador;
            double centroyy = (double)apintar.Y.Numerador / apintar.Y.Denominador;
                lbExplicacion.Hide();
            lbExplicacion.Show();
                lbExplicacion.Text = " Las coordenadas X e Y del punto final del radio, se obtienen sumando o restando el largo de los catetos a las coodenadas X o Y respectivas del centro de la circunferencia.\n Se han añadido a la figura en color amarillo, y puede verse como varian cambiando el ángulo de la línea del radio.\n La ecuación del punto final del radio en este ángulo de la linea del radio es:  " + Racional.AString(radio * radio) + " = ( " + Math.Round((radiodouble * Math.Cos(anguloradio) + centroxx), 4).ToString() + " - " + centroxx.ToString() + " )^2 + ( " + Math.Round((radiodouble * Math.Sin(anguloradio) + centroyy), 4).ToString() + " - " + centroyy.ToString() + ")^2";
            ventanagrafica.Ventana.Invalidate();
            if (directa)
                FinalizarResolucion();
             
        }
          /// <summary>
        /// 
        /// MUESTRA LA ECUACION DE LA CIRCUNFERENCIA INTRODUCIDA Y TERMINA LA EXPLICACION
        /// 
        /// </summary>
        /// 
        private void FinalizarResolucion()
        {
            double radiodouble = (double)(radio.Numerador / radio.Denominador);
            double centroxx = (double)apintar.X.Numerador / apintar.X.Denominador;
            double centroyy = (double)apintar.Y.Numerador / apintar.Y.Denominador;
            if (!directa)
            lbExplicacion.Text = " Las coordenadas del centro de la circunferencia, son las mismas esté donde esté el punto final del radio de la circunferencia, si cambiamos las coordenadas del punto final del radio  por las variables X e Y, la ecuación queda: \n " + Racional.AString(radio * radio) + " = ( X - " + centroxx.ToString() + " )^2 + ( Y - " + centroyy.ToString() + ")^2 . Desarrollando los cuadrados de las restas: " + Racional.AString(radio * radio) + " =  X^2 - " + (2 * centroxx).ToString() + "X + " + (centroxx * centroxx).ToString() + " + Y^2  - " + (2 * centroyy).ToString() + "Y + " + (centroyy * centroyy).ToString() + ".  Simplificando: " + ((radiodouble * radiodouble) - (centroxx * centroxx) - (centroyy * centroyy)) + " =  X^2 - " + (2 * centroxx).ToString() + "X + Y^2 - " + (2 * centroyy).ToString() + "Y";
            lbResultado.Visible = true;
            lbResultado.Location = new Point(btContinuar.Location.X, label1.Location.Y + label1.Height + 5);
            lbResultado.Text = "Ecuación de esta circunferencia:\nX^2 - " + (2 * centroxx).ToString() + "X + Y^2 - " + (2 * centroyy).ToString() + "Y = " + ((radiodouble * radiodouble) - (centroxx * centroxx) - (centroyy * centroyy));
            btContinuar.Hide();
            if (directa)
                lbExplicacion.Hide();
        }

        /// <summary>
        /// 
        ///  CIERRA EL ZOOM SOBRE EL AREAGRAFICA
        /// 
        /// </summary>
        /// 
        private void btZoomMas_Click(object sender, EventArgs e)
        {
            try
            {
                if( ventanagrafica.Escala >= 10F)
           
                ventanagrafica.Escala += 10F;
                else
                    ventanagrafica.Escala *= 2F;
            }
            catch (ArgumentOutOfRangeException)
            {
                ventanagrafica.Escala *= 1F;
            }
            ventanagrafica.Ventana.Invalidate();
        }

        /// <summary>
        /// 
        ///  ABRE  EL ZOOM SOBRE EL AREAGRAFICA
        /// 
        /// </summary>
        /// 
        private void btZoomMenos_Click(object sender, EventArgs e)
        {
            try
            {
                if( ventanagrafica.Escala - 10F > 0)
               // ventanagrafica.Escala *= 0.5F;
                ventanagrafica.Escala -= 10F;
                else
                    ventanagrafica.Escala *= 0.5F;
            }
            catch (ArgumentOutOfRangeException)
            {
                ventanagrafica.Escala *= 1;
            }
            ventanagrafica.Ventana.Invalidate();
        }

        /// <summary>
        /// 
        ///  MUEVE EL AREA GRAFICA ARRIBA UN VALOR EN FUNCION DE LA ESCALA
        /// 
        /// </summary>
        /// 
        private void btArriba_Click(object sender, EventArgs e)
        {
            ventanagrafica.DesplazarArriba();
            ventanagrafica.Ventana.Invalidate();
        }
        /// <summary>
        /// 
        ///  MUEVE EL AREA GRAFICA ABAJO UN VALOR EN FUNCION DE LA ESCALA
        /// 
        /// </summary>
        /// 
        private void btAbajo_Click(object sender, EventArgs e)
        {
            ventanagrafica.DesplazarAbajo();
            ventanagrafica.Ventana.Invalidate();
        }
        /// <summary>
        /// 
        ///  MUEVE EL AREA GRAFICA A LA DERECHA UN VALOR EN FUNCION DE LA ESCALA
        /// 
        /// </summary>
        /// 
        private void btDerecha_Click(object sender, EventArgs e)
        {
            ventanagrafica.DesplazarADerecha();
            ventanagrafica.Ventana.Invalidate();
        }
        /// <summary>
        /// 
        ///  MUEVE EL AREA GRAFICA A LA IZQUIERDA UN VALOR EN FUNCION DE LA ESCALA
        /// 
        /// </summary>
        /// 
        private void btIzquierda_Click(object sender, EventArgs e)
        {
            ventanagrafica.DesplazarAIzquierda();
            ventanagrafica.Ventana.Invalidate();
        }

          /// <summary>
        /// 
        ///  CENTRA Y ESCALA LA PANTALLA AUTOMATICAMENTE
        /// 
        /// </summary>
        /// 
        private void btCentrar_Click(object sender, EventArgs e)
        {
            Racional coordenadamaxima = apintar.CoordenadaMaxima; // Coordenada mas grande del centro del circulo
            if (coordenadamaxima.Numerador < 0) // Pasar a valor absoluto
                coordenadamaxima = new Racional(coordenadamaxima.Numerador * -1, coordenadamaxima.Denominador);
            coordenadamaxima += radio;
            float maximafloat = (float)((float)coordenadamaxima.Numerador / (float)coordenadamaxima.Denominador);
            maximafloat = Math.Abs(maximafloat);
            // Escalar
            if ((float)Math.Round((double)(ventanagrafica.Ventana.Height / 10) / maximafloat) > 1) // Coordenada maxima del centro y radio mas pequeños que el area grafica
                ventanagrafica.Escala = (float)Math.Round((double)(ventanagrafica.Ventana.Height / 10) / (double)maximafloat) * 10;
            else // Coordenada maxima o radio, mas grandes que el area grafica
            {
                ventanagrafica.Escala = (float)Math.Round((double)(ventanagrafica.Ventana.Height) / (double)(maximafloat), 0); // Coordenada maxima del centro o radio mas grandes que el area grafica
            }
            
            if (radio > apintar.CoordenadaMaxima)
                ventanagrafica.Escala /= 2;
            if (ventanagrafica.Escala == 0)
                ventanagrafica.Escala = 0.1F;
            
          // Desplazar
            if ((apintar.X.Numerador > 0 && apintar.Y.Numerador > 0) || (apintar.X.Numerador == 0 && apintar.Y.Numerador > 0) || (apintar.X.Numerador > 0 && apintar.Y.Numerador == 0) || (apintar.X.Numerador == 0 && apintar.Y.Numerador == 0)) // Centro en el primer cuadrante
            {
                Racional desplazamientoy = radio - apintar.X;
                if (desplazamientoy < 0)
                    desplazamientoy = 0;
                int desplazamienty = (int)(desplazamientoy.Numerador / desplazamientoy.Denominador);
                desplazamienty *= (int)(ventanagrafica.Escala);
                Racional desplazamientox = radio - apintar.Y;
                if (desplazamientox < 0)
                    desplazamientox = 0;
                int desplazamientx = (int)(desplazamientox.Numerador / desplazamientox.Denominador);
                desplazamientx *= (int)(ventanagrafica.Escala);
                ventanagrafica.DesplazarA(new Point(ventanagrafica.Ventana.Height - desplazamientx-40, desplazamienty+10));
            }
            else if (apintar.X < 0 && apintar.Y > 0)
            {
                Racional desplazamientoy = radio + apintar.X;
                if (desplazamientoy < 0)
                    desplazamientoy = 0;
                int desplazamienty = (int)(desplazamientoy.Numerador / desplazamientoy.Denominador);
                desplazamienty *= (int)(ventanagrafica.Escala);
                Racional desplazamientox = radio - apintar.Y;
                if (desplazamientox < 0)
                    desplazamientox = 0;
                int desplazamientx = (int)(desplazamientox.Numerador / desplazamientox.Denominador);
                desplazamientx *= (int)(ventanagrafica.Escala);
                ventanagrafica.DesplazarA(new Point(ventanagrafica.Ventana.Height - desplazamientx - 10, ventanagrafica.Ventana.Width-desplazamienty - 40));
            }
            else if (apintar.X < 0 && apintar.Y < 0)
            {
                Racional desplazamientoy = radio + apintar.X;
                if (desplazamientoy < 0)
                    desplazamientoy = 0;
                int desplazamienty = (int)(desplazamientoy.Numerador / desplazamientoy.Denominador);
                desplazamienty *= (int)(ventanagrafica.Escala);
                Racional desplazamientox = radio - apintar.Y;
                if (desplazamientox < 0)
                    desplazamientox = 0;
                int desplazamientx = (int)(desplazamientox.Numerador / desplazamientox.Denominador);
                desplazamientx *= (int)(ventanagrafica.Escala);
                ventanagrafica.DesplazarA(new Point(ventanagrafica.Ventana.Height - desplazamientx - 10, ventanagrafica.Ventana.Width - desplazamienty - 40));
            }
            else if (apintar.X > 0 && apintar.Y < 0)
            {
                //ventanagrafica.DesplazarA(new Point(5, 40));
                Racional desplazamientoy = radio - apintar.X;
                if (desplazamientoy < 0)
                    desplazamientoy = 0;
                int desplazamienty = (int)(desplazamientoy.Numerador / desplazamientoy.Denominador);
                desplazamienty *= (int)(ventanagrafica.Escala);
                Racional desplazamientox = radio - apintar.Y;
                if (desplazamientox < 0)
                    desplazamientox = 0;
                int desplazamientx = (int)(desplazamientox.Numerador / desplazamientox.Denominador);
                desplazamientx *= (int)(ventanagrafica.Escala);
                ventanagrafica.DesplazarA(new Point(5 + desplazamientx + 10, 40 + desplazamienty));
            }
            ventanagrafica.Ventana.Invalidate();   
    
        }

        ///<Summary>
        ///
        /// LLAMA AL METODO ADECUADO SEGUN SE PULSE EL DIAL A MAS O MENOS 
        /// 
        /// </Summary>
        /// 
        private void sbValor_ValueChanged(object sender, EventArgs e)
        {
           // if (sbValor.Value < 0)
             //   anguloradio = (double)2 * Math.PI + (double)sbValor.Value;
            //else 
            anguloradio = (double)sbValor.Value / 100;
            
            if (angulo < anguloradio)
                AnguloMas();
            else
                AnguloMenos();

            angulo = anguloradio;
        }

        /// <summary>
        /// 
        ///  REDIBUJA EL CIRCULO CON EL RADIO EN EL ANGULO INCREMENTADO EN 0,1 RADIANES
        ///  Y CAMBIA LA ETIQUETA CON EL TEOREMA DE PITAGORAS lbPitagoras
        /// 
        /// </summary>
        /// 
        private void AnguloMas()
        {
            float escalaactual = ventanagrafica.Escala;
            origenx = ventanagrafica.OrigenX;
            origeny = ventanagrafica.OrigenY;

            try
            {
                ventanagrafica.EliminarCirculo(0);
            }
            catch( ArgumentOutOfRangeException)
            {

            }
            ventanagrafica.Escala = escalaactual;
            ventanagrafica.DesplazarA(new Point(origeny, origenx));
            if( paso == 1)
            ventanagrafica.PintarCirculo(apintar, radio, Color.Olive, 2, anguloradio);
            else
                ventanagrafica.PintarCirculo(apintar, radio, Color.Olive, 2, anguloradio,true);
            tbParametro.Text = anguloradio.ToString();
            double grados = (anguloradio * 57.29577951F);
            tbLargoInicial.Text = grados.ToString();
            double radiodouble = radio.Numerador / radio.Denominador;
            label1.Text = "Teorema de Pitágoras aplicado a esta circunferencia:\nѴ (" + Math.Round(radiodouble * Math.Sin(anguloradio), 2).ToString() + "^2 + " + Math.Round(radiodouble * Math.Cos(anguloradio), 2).ToString() + "^2 )  = " + Racional.AString(radio);
            double centroxx = (double)apintar.X.Numerador / apintar.X.Denominador;
            double centroyy = (double)apintar.Y.Numerador / apintar.Y.Denominador;
            if( paso == 1)
                lbExplicacion.Text = " Las coordenadas X e Y del punto final del radio, se obtienen sumando o restando el largo de los catetos a las coodenadas X o Y respectivas del centro de la circunferencia.\n Se han añadido a la figura en color amarillo, y puede verse como varian cambiando el ángulo de la línea del radio.\n La ecuación del punto final del radio en este ángulo de la linea del radio es:  " + Racional.AString(radio * radio) + " = ( " + Math.Round((radiodouble * Math.Cos(anguloradio) + centroxx), 4).ToString() + " - " + centroxx.ToString() + " )^2 + ( " + Math.Round((radiodouble * Math.Sin(anguloradio) + centroyy), 4).ToString() + " - " + centroyy.ToString() + ")^2";
            ventanagrafica.Ventana.Invalidate();
       
        }

        /// <summary>
        /// 
        ///  REDIBUJA EL CIRCULO CON EL RADIO EN EL ANGULO REDUCIDO EN 0,1 RADIANES
        ///  Y CAMBIA LA ETIQUETA CON EL TEOREMA DE PITAGORAS lbPitagoras
        /// 
        /// </summary>
        /// 
        private void AnguloMenos()
        {
            float escalaactual = ventanagrafica.Escala;
            origenx = ventanagrafica.OrigenX;
            origeny = ventanagrafica.OrigenY;
            ventanagrafica.EliminarCirculo(0);
          
            ventanagrafica.Escala = escalaactual;
            ventanagrafica.DesplazarA(new Point(origeny, origenx));
            if( paso == 1)
            ventanagrafica.PintarCirculo(apintar, radio, Color.Olive, 2, anguloradio);
            else
                ventanagrafica.PintarCirculo(apintar, radio, Color.Olive, 2, anguloradio,true);
            tbParametro.Text = anguloradio.ToString();
            double grados = (anguloradio * 57.29577951F);
            tbLargoInicial.Text = grados.ToString();
            double radiodouble = radio.Numerador / radio.Denominador;
            double centroxx = (double)apintar.X.Numerador / apintar.X.Denominador;
            double centroyy = (double)apintar.Y.Numerador / apintar.Y.Denominador;
            if (paso == 1)
                lbExplicacion.Text = " Las coordenadas X e Y del punto final del radio, se obtienen sumando o restando el largo de los catetos a las coodenadas X o Y respectivas del centro de la circunferencia.\n Se han añadido a la figura en color amarillo, y puede verse como varian cambiando el ángulo de la línea del radio.\n La ecuación del punto final del radio en este ángulo de la linea del radio es:  " + Racional.AString(radio * radio) + " = ( " + Math.Round((radiodouble * Math.Cos(anguloradio) + centroxx), 4).ToString() + " - " + centroxx.ToString() + " )^2 + ( " + Math.Round((radiodouble * Math.Sin(anguloradio) + centroyy), 4).ToString() + " - " + centroyy.ToString() + ")^2";
            label1.Text = "Teorema de Pitágoras aplicado a esta circunferencia:\nѴ (" + Math.Round(radiodouble * Math.Sin(anguloradio), 2).ToString() + "^2 + " + Math.Round(radiodouble * Math.Cos(anguloradio), 2).ToString() + "^2 )  = " + Racional.AString(radio);
            ventanagrafica.Ventana.Invalidate();
             
        }

    }
}

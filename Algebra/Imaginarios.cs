using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matematicas;
using System.Windows.Forms;
using System.Drawing;
using Matedicom;
using Punto_y_Vector;

namespace Algebra
{
    class Imaginarios : FormularioBase
    {
        List<int> indicesrojos; // Para guardar el indice y el largo de los intervalos en rojo en el RichTextBox
        Imaginario imaginario; // Será el imaginario introducido
        Espacio2D grafica; // Será el objeto Espacio2D para representar el imaginario graficamente

        public Imaginarios(bool tiporesolucion)
        {
            directa = tiporesolucion;
        }

        public override void Cargar(object sender, EventArgs e)
        {
            lbExplicacion.Show();
            lbExplicacion.Text = "Los números imaginarios, se usan principalmente, para expresar la raiz de inidice par de un número negativo, como por ejemplo √-3.";
            btDefecto.Hide();
            rtbExplicaciones.Show();
	    if(!directa)
            rtbExplicaciones.Location = new Point(800, 120);
            else if(directa)
	    rtbExplicaciones.Location = new Point(800,200);
	    rtbExplicaciones.BackColor = Color.SeaGreen;
            rtbExplicaciones.BorderStyle = BorderStyle.None;
            rtbExplicaciones.Size = new Size(400, 200);
            rtbExplicaciones.Font = new Font("Dejavu Sans", 12);
            btContinuar.Show();
	
            btContinuar.Location = new Point(rtbExplicaciones.Location.X, rtbExplicaciones.Location.Y + rtbExplicaciones.Height + 5);
           
	    btContinuar.Click += btContinuar_Click;
	    radioButton1.Click += RadioButton1_Click;
	    radioButton2.Click += RadioButton2_Click;
	    if(directa)
	    {
	     btContinuar.PerformClick();
	     btContinuar.PerformClick();
	     btContinuar.PerformClick();
	    }
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
        /// CIERRA EL FORMULARIO ACTUAL Y ABRE UN NUEVO MENU DE ALGEBRA 
        /// 
        /// </summary>
        /// 
        public override void btSalir_Click(object sender, EventArgs e)
        {
            MenuAlgebra menualgebra = new MenuAlgebra();
            menualgebra.Show();
            this.Dispose();
        }

        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>
        /// 
        public override void btNuevo_Click(object sender, EventArgs e)
        {
            Imaginarios nuevo = new Imaginarios(directa);
            nuevo.Show();
            this.Close();
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN VALORES  RACIONALES O ENTEROS O SIGNOS CORRECTOS EN LAS CAJAS DE TEXTO DE LOS
        /// POLINOMIOS
        /// 
        /// </summary>
        /// 
        private void Cajas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (e.KeyChar == (char)'-') // Signo -
            {
                 if(caja.Text.Length > 0)
                    e.Handled = true;
                else if( caja.Name == "tbcolumnas")
                {
                    label2.Text = "-";
                    e.Handled = true;
                }
            }

            else if (e.KeyChar == (char)'+') // Signo +
            {
                if(caja.Text.Length > 0)
                    e.Handled = true;
                else if( caja.Name == "tbcolumnas")
                {
                    label2.Text = "+";
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == '/') // Para racionales
            {
                if (caja.Text.Contains('/'))
                    e.Handled = true;
                else if (caja.Text.Length == 0)
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            else if ((e.KeyChar >= (char)65 && e.KeyChar <= (char)90) || (e.KeyChar >= (char)97 && e.KeyChar <= (char)122)) // letras de la A a la Z mayusculas o minusculas
            {
                if (e.KeyChar > 90) // Pasar a mayuscula
                {
                    tbFilas.Text += char.ToUpper(e.KeyChar);
                    tbFilas.SelectionStart = tbFilas.Text.Length;
                    e.Handled = true;
                }
                else
                    e.Handled = false;
            }

            else if (e.KeyChar == (char)8) // Retroceso
                e.Handled = false;


            else if (e.KeyChar == (char)13) // Intro
            {
                if (caja.Text.Length == 0)
                    e.Handled = true;
                else
                {
                    if (caja.Name == "tbFilas")
                      {
			  tbcolumnas.Focus();
			if(paso > 0)
			tbcolumnas.SelectAll();
		      }
                    else
                    {
                        LeerImaginario();
                        if (imaginario.ParteImaginaria.Numerador > 0 && label2.Text != "-")
                            label2.Text = "+";
			
                    }

                }
            }
            // Digitos
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void btContinuar_Click(object sender, EventArgs e)
        {
            if (paso == 0)
            {
                indicesrojos = new List<int>();
                if(!directa)
	        lbExplicacion.Text = "El número imaginario unidad es √-1 y se indica como 'i'. Un número imaginario de coeficiente distinto a la unidad, se indica 'ci' donde c es un número real que es el coeficiente. Tambien se puede expresar en combinación con un número real con la forma: a + ci , donde 'a' y 'c' son números reales, en estos casos se denominan: numeros complejos.\nEl cuadrado del imaginario unidad, es -1.";
                else if(directa)
		lbExplicacion.Text += "\nEl número imaginario unidad es √-1 y se indica como 'i'. Un número imaginario de coeficiente distinto a la unidad, se indica 'ci' donde c es un número real que es el coeficiente. Tambien se puede expresar en combinación con un número real con la forma: a + ci , donde 'a' y 'c' son números reales, en estos casos se denominan: numeros complejos.\nEl cuadrado del imaginario unidad, es -1.";
		rtbExplicaciones.Text = "i: Número imaginario unidad igual a la raiz de -1 ";
                indicesrojos.Add(0);
                indicesrojos.Add(1);
                indicesrojos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "\n\n2i:  √(-4) = √(4 * -1) = 2√(-1) = 2i Imaginario puro";
                indicesrojos.Add(4);
                indicesrojos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "\n\n15+2i: Combinacion de un número real (15) más un imaginario (2i). Número complejo.";
                indicesrojos.Add(7);
                indicesrojos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "\n\ni^2 = -1";
                indicesrojos.Add(5);
                for (int i = 0; i < indicesrojos.Count; i += 2)
                {
                    rtbExplicaciones.Select(indicesrojos[i], indicesrojos[i + 1]);
                    rtbExplicaciones.SelectionColor = Color.Red;
                    rtbExplicaciones.SelectionFont = new Font("Dejavu Sans", 14, FontStyle.Bold);
                }
                paso++;
            }
            else if (paso == 1)
            {
		if(!directa)
                lbExplicacion.Text = "Un número complejo se puede representar gráficamente, considerando la parte real como el valor de las abcisas ( eje X ), y el coeficiente imaginario como valor de las ordenadas ( eje Y ).\nEn el ejemplo, se puede ver la representación gráfica del complejo: 5 + 9i ";
                else if(directa)
		 lbExplicacion.Text += "\nUn número complejo se puede representar gráficamente, considerando la parte real como el valor de las abcisas ( eje X ), y el coeficiente imaginario como valor de las ordenadas ( eje Y ).\nEn el ejemplo, se puede ver la representación gráfica del complejo: 5 + 9i ";
		EtiquetaFilas.Show();
                EtiquetaFilas.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + btContinuar.Height + 5);
                EtiquetaFilas.Text = "Parte real:";
                EtiquetaFilas.AutoSize = true;
                tbFilas.Show();
                tbFilas.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 5);
                tbFilas.Width = EtiquetaFilas.Width;
                tbFilas.Text = "5";
                tbFilas.KeyPress += Cajas_KeyPress;
                tbFilas.TextAlign = HorizontalAlignment.Center;
                label2.Show();
                label2.BackColor = Color.Transparent;
                label2.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
                label2.Font = EtiquetaFilas.Font;
                label2.Text = "+";
                label2.Width = 3;
                label2.TextAlign = ContentAlignment.MiddleCenter;
                EtiquetaColumnas.Show();
                EtiquetaColumnas.Location = new Point(label2.Location.X + label2.Width + 5, EtiquetaFilas.Location.Y);
                EtiquetaColumnas.Text = "Parte imaginaria";
                EtiquetaColumnas.AutoSize = true;
                tbcolumnas.Show();
                tbcolumnas.Location = new Point(EtiquetaColumnas.Location.X, tbFilas.Location.Y);
                tbcolumnas.Width = EtiquetaColumnas.Width;
                tbcolumnas.Text = "9";
                tbcolumnas.TextAlign = HorizontalAlignment.Center;
                tbcolumnas.KeyPress += Cajas_KeyPress;
                imaginario = new Imaginario(5, 9);
                label1.Show();
                label1.BackColor = Color.Transparent;
                label1.Font = EtiquetaFilas.Font;
                label1.Location = new Point(tbcolumnas.Location.X + tbcolumnas.Width + 5,tbcolumnas.Location.Y);
                label1.Text = "i";
		if(!directa)
                grafica = new Espacio2D(750, 600, new Point(10, lbExplicacion.Height + 5));
		else if(directa)
		grafica = new Espacio2D(750, 600, new Point(10, lbExplicacion.Height + 10));
		Controls.Add(grafica.Ventana);
                pnZoom.Show();
                pnZoom.Location = new Point(tbFilas.Location.X, tbFilas.Location.Y + tbFilas.Height + 5);
                btAbajo.Show();
                btAbajo.Click += MoverAbajo;
                btArriba.Show();
                btArriba.Click += MoverArriba;
                btDerecha.Show();
                btDerecha.Click += MoverDerecha;
                btIzquierda.Show();
                btIzquierda.Click += MoverIzquierda;
                btCentrar.Show();
                btCentrar.Click += Centrar;
                btZoomMas.Show();
                btZoomMas.Click += ZoomMas;
                btZoomMenos.Show();
                btZoomMenos.Click += ZoomMenos;
                lbDesplazamiento.Show();
                lbZoomtitulo.Show();
		radioButton1.Show();
		radioButton1.Location = new Point(rtbExplicaciones.Location.X,rtbExplicaciones.Location.Y - 30);
		radioButton2.Show();
		radioButton2.Text = "Escala Decimal.";
		radioButton1.Text = "Escala Racional";
		radioButton2.Location = new Point(radioButton1.Location.X + radioButton1.Width + 5, radioButton1.Location.Y);
                if(!directa)
		grafica.PintarLinea(new Punto(new Racional[] {new Racional(0,1),new Racional(0,1)}),new Punto(new Racional[]{imaginario.ParteReal,imaginario.ParteImaginaria}),Color.Red,3,true);
		paso++;
            }
	   else if( !directa && paso == 2)
	   {
		lbExplicacion.Text = "Introduzca los valores de cualquier imaginario, con coeficientes enteros o racionales, para ver su representacion grafica.";
		tbFilas.Focus(); 
		tbFilas.SelectAll();
		grafica.PintarLinea(new Punto(new Racional[] {new Racional(0,1),new Racional(0,1)}),new Punto(new Racional[]{imaginario.ParteReal,imaginario.ParteImaginaria}),Color.Red,3,true);
		grafica.Ventana.Invalidate();
		btContinuar.BackColor = Color.SeaGreen;
		btContinuar.Text = "";
		btContinuar.Size = new Size(0,0);
	   }
	   else if( directa && paso >= 2)
	   {
		if(paso > 2)
		{
		 grafica.PintarLinea(new Punto(new Racional[] {new Racional(0,1),new Racional(0,1)}),new Punto(new Racional[]{imaginario.ParteReal,imaginario.ParteImaginaria}),Color.Red,3,true);
		 grafica.Ventana.Invalidate();
		}
		else if(paso == 2 )
		lbExplicacion.Text += "\nIntroduzca los valores de cualquier imaginario, con coeficientes enteros o racionales, para ver su representacion grafica.";
		  tbFilas.ResetText();
		  tbcolumnas.ResetText();
		  label2.Text ="";
		  tbFilas.Focus();
		paso++;
	   }
        }

        /// <summary>
        /// 
        /// CONSTRUYE EL NUMERO IMAGINARIO CON LOS DATOS INTRODUCIDOS
        /// 
        /// </summary>
        /// 
        private void LeerImaginario()
        {
            Racional real = Racional.StringToRacional(tbFilas.Text);
            Racional imaginaria = Racional.StringToRacional(tbcolumnas.Text);
	    if(label2.Text =="-")
		imaginaria = new Racional(imaginaria.Numerador*-1,imaginaria.Denominador);
            imaginario = new Imaginario(real, imaginaria);
	   if(paso > 0)
		btContinuar.PerformClick();
        }


        private void MoverDerecha(object sender, EventArgs e)
        {
            grafica.DesplazarADerecha();
            btZoomMas.PerformClick();
            btZoomMenos.PerformClick();
        }
        private void MoverIzquierda(object sender, EventArgs e)
        {
            grafica.DesplazarAIzquierda();
            btZoomMas.PerformClick();
            btZoomMenos.PerformClick();
        }
        private void MoverArriba(object sender, EventArgs e)
        {
            grafica.DesplazarArriba();
            btZoomMas.PerformClick();
            btZoomMenos.PerformClick();
        }
        private void MoverAbajo(object sender, EventArgs e)
        {
            grafica.DesplazarAbajo();
            btZoomMas.PerformClick();
            btZoomMenos.PerformClick();
        }
        private void ZoomMas(object sender,EventArgs e)
        {
            grafica.Escala += 2;
            grafica.Ventana.Invalidate();
        }
        private void ZoomMenos(object sender, EventArgs e)
        {
            grafica.Escala -= 2;
            grafica.Ventana.Invalidate();
        }
        private void Centrar(object sender, EventArgs e)
        {
            grafica.DesplazarA(new Point( ((grafica.Ventana.Height) / 2), (grafica.Ventana.Width) / 2));
            grafica.Escala = 10;
            btZoomMas.PerformClick();
            btZoomMenos.PerformClick();
        }

	private void RadioButton1_Click(object sender,EventArgs e)
	{
		grafica.ValoresRacionales = true;
		btZoomMas.PerformClick();
		btZoomMenos.PerformClick();
	}
	private void RadioButton2_Click(object sender,EventArgs e)
	{
		grafica.ValoresRacionales = false;
		btZoomMas.PerformClick();
		btZoomMenos.PerformClick();
	}

      
    }
}

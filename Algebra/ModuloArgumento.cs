using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matematicas;
using Matedicom;
using System.Windows.Forms;
using System.Drawing;
using Punto_y_Vector;

namespace Algebra
{
    class ModuloArgumento:FormularioBase
    {

     // !!!!!!!!!!!!!!!!!!!!!!!!!
     // !! 
     // !! INICIO DE LA CLASE EL DIA 26/1/2016
     // !!
     // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


	Espacio2D grafica; // Area grafica donde se representaran los complejos
	Imaginario imaginario; // Será el imaginario introducido 
	
       
        public ModuloArgumento( bool resoluciondirecta )
        {
            directa = resoluciondirecta;
        }


        public override void Cargar(object sender, EventArgs e)
        {
	    if(directa)
	     btDefecto.Hide();

	    this.Text = "Módulo y argumento de números complejos";

	    lbExplicacion.Show();
	    lbExplicacion.Text ="Introduzca la parte real e imaginaria del número complejo.\n\nO pulse el botón [E] para ejemplo con valores por defecto.";

	    grafica = new Espacio2D(750,600,new Point(10,lbExplicacion.Height + 15));
	    Controls.Add(grafica.Ventana);
	    grafica.Ventana.Hide();
	  
	    EtiquetaFilas.Show();
	    EtiquetaFilas.Text = "Parte real:";
	    EtiquetaFilas.Location = new Point(grafica.Ventana.Location.X + grafica.Ventana.Width + 5, grafica.Ventana.Location.Y);
	    EtiquetaFilas.AutoSize = true;

	    EtiquetaColumnas.Show();
	    EtiquetaColumnas.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 5);
	    EtiquetaColumnas.Text ="Parte imaginaria:";
	    EtiquetaColumnas.AutoSize = true;

	    tbcolumnas.Show();
	    tbcolumnas.Location = new Point(EtiquetaColumnas.Location.X + EtiquetaColumnas.Width + 5, EtiquetaColumnas.Location.Y);
	    tbcolumnas.KeyPress += Cajas_KeyPress;
	    
	    tbFilas.Show();
	    tbFilas.Location = new Point(tbcolumnas.Location.X, EtiquetaFilas.Location.Y);
	    tbFilas.KeyPress += Cajas_KeyPress;

	    btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 10, tbFilas.Location.Y-2);
        }


        /// <summary>
        ///  
        /// SALE DEL PROGRAMA Y LIBERA TODOS LOS RECURSOS
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
        ///  PONE EL ATRIBUTO defecto A TRUE PARA QUE SE INICIE LA RESOLUCION CON LOS VALORES POR
        ///  DEFECTO
        /// 
        /// </summary>
        /// 
        public override void btDefecto_Click(object sender, EventArgs e)
        {
	  tbFilas.Text = "37/5";
	  tbcolumnas.Text = "8";
	  btDefecto.Hide();
	  IniciarResolucion();	
        }

        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>
        /// 
        public override void btNuevo_Click(object sender, EventArgs e)
        {
            ModuloArgumento nuevo = new ModuloArgumento(directa);
            nuevo.Show();
            this.Close();
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN VALORES  RACIONALES O ENTEROS O SIGNOS CORRECTOS EN LAS CAJAS DE TEXTO DEL COMPLEJO
        /// 
        /// 
        /// </summary>
        /// 
        private void Cajas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (e.KeyChar == (char)'-') // Signo -
            {
		if( caja.Text.Length == 0)
		{
		    e.Handled = false;
		}
		else
		e.Handled = true;
            }

            else if (e.KeyChar == (char)'+') // Signo +
		if(caja.Text.Length == 0)
		{
                 e.Handled = false;
		}
		else
		e.Handled = true;

           
            else if (e.KeyChar == (char)8) // Retroceso
                e.Handled = false;

            else if (e.KeyChar == (char)'/')// Signo dividir
            {
                if (caja.Text.Length == 0 || caja.Text.Contains('/'))
                {
                    e.Handled = true;
                    caja.Focus();
                }
                else
                {
                    e.Handled = false;
                }
            }
                
          
            else if (e.KeyChar == (char)13) // Intro
            {
                if (caja.Text.Length == 0)
                    e.Handled = true;
                else
                {
                    if(caja.Name == "tbFilas")
			tbcolumnas.Focus();
		    else if(caja.Name == "tbcolumnas")
			IniciarResolucion();
                }

            }
            // Digitos
         
            else if(char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

	

	
        private void btContinuar_Click(object sender, EventArgs e)
        {
		paso++;
		IniciarResolucion();
        }


        /// <summary>
        /// 
        /// REALIZA LA RESOLUCION PASO A PASO 
        /// 
        /// </summary>
        /// 
        private void IniciarResolucion()
        {
	   if(paso == 0)
	       {
		// Mostrar panel de controles Zoom y desplazamiento
		this.pnZoom.Show();
		pnZoom.Location = new Point( EtiquetaFilas.Location.X, 400);
		btZoomMas.Show();
		btZoomMas.Click += btZoomMas_Click;
		btZoomMenos.Show();
		btZoomMenos.Click += btZoomMenos_Click;
		lbZoomtitulo.Show();
		btCentrar.Show();
		btCentrar.Click += btCentrar_Click;
		lbAjustar.Show();
		btDerecha.Show();
		btDerecha.Click += btDerecha_Click;
		btIzquierda.Show();
		btIzquierda.Click += btIzquierda_Click;
		btArriba.Show();
		btArriba.Click += btArriba_Click;
		btAbajo.Show();
		btAbajo.Click += btAbajo_Click;
		btAjustar.Show();
		btAjustar.Click += btAjustar_Click;
		lbAjustar.Show();
		lbDesplazamiento.Show();
		// Mostrar el area de la grafica
		grafica.Ventana.Show();
		// Mostrar los radioButton para elegir escala decimal o racional
		radioButton1.Show();
		radioButton1.Location = new Point(pnZoom.Location.X, pnZoom.Location.Y - radioButton1.Height - 5);
		radioButton1.Text = "Escala racional";
		radioButton1.Checked = true;
		radioButton1.Click += radioButton1_Click;
		radioButton2.Show();
		radioButton2.Location = new Point(radioButton1.Location.X + radioButton1.Width+5, radioButton1.Location.Y);
		radioButton2.Text = "Escala decimal";
		radioButton2.Click += radioButton2_Click;
		// Construir el complejo y mostrarlo en la etiqueta
		  imaginario = new Imaginario( Racional.StringToRacional(tbFilas.Text), Racional.StringToRacional(tbcolumnas.Text) );
		label1.Show();
		label1.BackColor = Color.SeaGreen;
		label1.Font = EtiquetaFilas.Font;
		label1.Location = new Point( EtiquetaFilas.Location.X, tbcolumnas.Location.Y + tbcolumnas.Height + 5);
		label1.Text = "Número complejo introducido: ";
		label2.Show();
		label2.BackColor = Color.SeaGreen;
		label2.Font = label1.Font;
		label2.Location = new Point(label1.Location.X + label1.Width + 5, label1.Location.Y);
		label2.Text = imaginario.ToString();
		// Mostrar la representacion grafica del complejo
		grafica.PintarLinea(new Punto(new Racional[] {0,0}), new Punto(new Racional[] { imaginario.ParteReal,imaginario.ParteImaginaria}),Color.Chartreuse,3,true);
		// Ocultar el boton defecto y mostrar el boton continuar
		btDefecto.Hide();
		btContinuar.Show();
		btContinuar.Location = new Point(EtiquetaFilas.Location.X, pnZoom.Location.Y + pnZoom.Height + 5);
		lbExplicacion.Text = "En la gráfica se muestra la representación gráfica del complejo introducido. Esta representación se corresponde con la de un vector que vá del punto origen de coordenadas, al punto de coordenada 'X' igual a la parte real del complejo y coordenada 'Y' igual a la parte imaginaria del mismo.";
 		lbExplicacion.Focus();
		btContinuar.Click += btContinuar_Click;
		if(directa)
		  btContinuar.PerformClick();
	       }
	    else if( paso == 1)
	      {
		lbExplicacion.Text = "El módulo de un número complejo, es igual al módulo del vector representado: ";
		label3.Show();
		label3.Font = label2.Font;
		label3.BackColor = Color.Chartreuse;
		label3.Location = new Point( label1.Location.X, label1.Location.Y + label1.Height + 5);
		if(!directa)
		label3.Text = "Módulo = √(" + imaginario.ParteReal.ToString() + "^2 + " + imaginario.ParteImaginaria.ToString() + "^2 ) = " + (imaginario.Modulo).ToDouble().ToString();
		if(directa)
		{
		label3.Text = "Módulo = " + (imaginario.Modulo).ToDouble().ToString();
		btContinuar.PerformClick();
		}
	      }
 	    else if( paso == 2)
	      {
		lbExplicacion.Text = "El llamado 'Argumento' de un número complejo, corresponde al ángulo que la representación gráfica del mismo forma con el eje de abcisas:";
		Racional radio = imaginario.Modulo/2;
		grafica.PintarArco(new Punto(new Racional[]{0,0}),radio,-0.05236D,imaginario.Argumento,Color.Red);
		grafica.Ventana.Invalidate();
		label4.Show();
		label4.Font = label3.Font;
		label4.BackColor = Color.Chartreuse;
		label4.Location = new Point( label3.Location.X, label3.Location.Y + label3.Height + 5);
		if(!directa)
		label4.Text = "Argumento = Arcotangente de ( " + imaginario.ParteImaginaria.ToString() + " / " + imaginario.ParteReal.ToString() + " ) = " + imaginario.Argumento.ToString() + " rads";
	      	if(directa)
		{
		label4.Text = "Argumento = " + imaginario.Argumento.ToString() + " rads";
		btContinuar.PerformClick();
		}
	      }
	     else if( paso == 3)
	      {
		lbExplicacion.Text = "Con el módulo y el argumento, se puede definir tambien un número complejo. En esta definición llamada Polar, se pone primero el módulo del complejo seguido de su argumento ( normalmente en grados sexagesimales ).";
		label5.Show();
		double sexagesimal = imaginario.Argumento * ( 180D / Math.PI);
		label5.Show();
		label5.Location = new Point(label4.Location.X, label4.Location.Y + label4.Height + 5);
		label5.Text = "Forma polar del imaginario: " + Math.Round(imaginario.Modulo.ToDouble(),2).ToString();
		label5.BackColor = Color.Transparent;
		label6.Show();
		label6.Location = new Point(label5.Location.X + label5.Width,label5.Location.Y + label5.Height /2 - 6);
		label6.Text = Math.Round(sexagesimal,2).ToString() + "º";
		label6.Font = new Font("Dejavu Sans", 10);
		label6.BackColor = Color.Transparent;
		if(directa)
		lbExplicacion.Hide();
		btContinuar.Hide();
	      }
        }

     


	///<Summary>
	///
	/// CIERRA EL ZOOM SOBRE EL AREA GRAFICA CADA VEZ QUE SE PULSA EL BOTON ZOOM MAS
	///
	///</Summary>
	///
	private void btZoomMas_Click(object sender, EventArgs e)
	{
		grafica.Escala += 2;
		grafica.Ventana.Invalidate();
	}


	///<Summary>
	///
	/// ABRE EL ZOOM SOBRE EL AREA GRAFICA CADA VEZ QUE SE PULSA EL BOTON ZOOM MAS
	///
	///</Summary>
	///
	private void btZoomMenos_Click(object sender, EventArgs e)
	{
		grafica.Escala -= 2;
		grafica.Ventana.Invalidate();
	}

	
	///<Summary>
	///
	/// REINICIA EL ZOOM A LOS VALORES INICIALES ( ESCALA 1 )
	///
	///</Summary>
	///
	private void btAjustar_Click(object sender, EventArgs e)
	{
		grafica.Escala = 10;
		grafica.Ventana.Invalidate();
	}


	///<Summary>
	///
	/// MUEVE EL GRAFICO A LA DERECHA CADA VEZ QUE SE PULSA EL BOTON
	///
	///</Summary>
	///
	private void btDerecha_Click(object sender, EventArgs e)
	{
		grafica.DesplazarADerecha();
		grafica.Ventana.Invalidate();
	}


	///<Summary>
	///
	/// MUEVE EL GRAFICO A LA IQUIERDA CADA VEZ QUE SE PULSA EL BOTON
	///
	///</Summary>
	///
	private void btIzquierda_Click(object sender, EventArgs e)
	{
		grafica.DesplazarAIzquierda();
		grafica.Ventana.Invalidate();
	}


	///<Summary>
	///
	/// MUEVE EL GRAFICO ARRIBA CADA VEZ QUE SE PULSA EL BOTON
	///
	///</Summary>
	///
	private void btArriba_Click(object sender, EventArgs e)
	{
		grafica.DesplazarArriba();
		grafica.Ventana.Invalidate();
	}


	///<Summary>
	///
	/// MUEVE EL GRAFICO ABAJO CADA VEZ QUE SE PULSA EL BOTON
	///
	///</Summary>
	///
	private void btAbajo_Click(object sender, EventArgs e)
	{
		grafica.DesplazarAbajo();
		grafica.Ventana.Invalidate();
	}


	///<Summary>
	///
	/// MUEVE EL GRAFICO AL CENTRO CADA VEZ QUE SE PULSA EL BOTON
	///
	///</Summary>
	///
	private void btCentrar_Click(object sender, EventArgs e)
	{
		grafica.DesplazarA(new Point(grafica.Ventana.Height/2,grafica.Ventana.Width/2));
		grafica.Ventana.Invalidate();
	}

	///<Summary>
	///
	/// CAMBIA LA ESCALA DEL GRAFICO A RACIONAL
	///
	///</Summary>
	///
	private void radioButton1_Click(object sender, EventArgs e)
	{
		grafica.ValoresRacionales = true;
		btZoomMas.PerformClick();
		btZoomMenos.PerformClick();
	}

	///<Summary>
	///
	/// CAMBIA LA ESCALA DEL GRAFICO A DECIMAL
	///
	///</Summary>
	///
	private void radioButton2_Click(object sender, EventArgs e)
	{
		grafica.ValoresRacionales = false;
		btZoomMas.PerformClick();
		btZoomMenos.PerformClick();
	}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matematicas;
using System.Windows.Forms;
using System.Drawing;
using Punto_y_Vector;
using Matedicom;

namespace Algebra
{
    class Combinatoria : FormularioBase
    {

        // !!!!!!!!!!!!!!!!!!!!!!!!!
        // !! 
        // !! INICIO DE LA CLASE EL DIA 1/2/2016
        // !!
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        bool variaciones = false;
        bool permutaciones = false;
        bool combinaciones = false;
        bool repeticion = false;
        int poblacion = 0;
        int muestra = 0;
        int contabutton = 0; // Para llevar la cuenta de las veces que se clica sobre el radiobutton1
        List <long> repetidos; // Para almacenar la cantidad de veces que se repiten algunos elementos en las permutaciones con repeticion
        char repetido; // Será la denominacion de cada elemento repetido en las permutaciones con repeticion.
        List<TextBox> cajas; // Para almacenar las cajas de los elementos repetidos en las permutaciones con repeticion
        List<Label> etiquetas; // Para alamacenar las etiquetas de los elementos repetidos en las permutaciones con repeticion
        List<int> repeticiones; // Para almacenar las veces que se repiten los elementos repetidos en las permutaciones
        int sumarepetidos; // Para llevar la cuenta de la suma de los elementos repetidos en las permutaciones


        public Combinatoria(bool resoluciondirecta)
        {
            directa = resoluciondirecta;
        }


        public override void Cargar(object sender, EventArgs e)
        {
            btDefecto.Hide();
            lbExplicacion.Show();
            lbExplicacion.Text = "Elija el tipo de operación combinatoria que desea realizar.";
            label1.Show();
            radioButton1.Show();
            radioButton1.Location = new Point(lbExplicacion.Location.X + 20, lbExplicacion.Location.Y + lbExplicacion.Height + 5);
            radioButton1.Text = "Variaciones";
            radioButton1.Font = new Font(radioButton1.Font.FontFamily, 10, FontStyle.Bold);
            radioButton1.Click += radioButton1_Click;
            label1.Location = new Point(radioButton1.Location.X - 15, radioButton1.Location.Y + radioButton1.Height + 5);
            label1.Font = new Font("Dejavu Sans", 10);
            label1.BackColor = Color.SeaGreen;
            label1.Text = "Con una cantidad de m elementos,\ncantidad de conjuntos de n elementos ( n <= m ), \ncon o sin repetición de los mismos,\nen los que el orden de los elementos es diferente.";
            label1.TextAlign = ContentAlignment.MiddleLeft;

            radioButton2.Show();
            radioButton2.Location = new Point(radioButton1.Location.X + label1.Width + 25, radioButton1.Location.Y);
            radioButton2.Text = "Permutaciones";
            radioButton2.Font = new Font(radioButton1.Font.FontFamily, 10, FontStyle.Bold);
            radioButton2.Click += radioButton2_Click;
            label2.Show();
            label2.Font = label1.Font;
            label2.BackColor = Color.SeaGreen;
            label2.TextAlign = ContentAlignment.MiddleLeft;
            label2.Text = "Con una cantidad de m elementos,\ncantidad de conjuntos de m elementos,\nsin repetición de los mismos,\nen los que el orden de los elementos es diferente.";
            label2.Location = new Point(label1.Location.X + label1.Width + 10, label1.Location.Y);

            radioButton3.Show();
            radioButton3.Location = new Point(radioButton2.Location.X + label2.Width + 25 + 5, radioButton2.Location.Y);
            radioButton3.Text = "Combinaciones";
            radioButton3.Font = new Font(radioButton1.Font.FontFamily, 10, FontStyle.Bold);
            radioButton3.Click += radioButton3_Click;
            label3.Show();
            label3.Font = label2.Font;
            label3.BackColor = Color.SeaGreen;
            label3.TextAlign = ContentAlignment.MiddleLeft;
            label3.Text = "Con una cantidad de m elementos, \ncantidad de cojuntos de n elementos ( n <= m),\ncon o sin repetición de los mismos, en los que los \nelementos son diferentes.\nNo se considera el orden.";
            label3.Location = new Point(label2.Location.X + label2.Width + 10, label2.Location.Y);

            tbFilas.KeyPress += Cajas_KeyPress;
            tbcolumnas.KeyPress += Cajas_KeyPress;
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
	   tbFilas.Text = "10";
	   tbcolumnas.Text = "3";
	   btContinuar.Show();
	   btContinuar.Click += btContinuar_Click;
	   btContinuar.PerformClick();
	   defecto = true;
	   //radioButton1.Hide();
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
            Combinatoria nuevo = new Combinatoria(directa);
            nuevo.Show();
            this.Close();
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN VALORES  RACIONALES O ENTEROS O SIGNOS CORRECTOS EN LAS CAJAS DE TEXTO 
        /// 
        /// 
        /// </summary>
        /// 
        private void Cajas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (e.KeyChar == (char)'-') // Signo -
            {
                if (caja.Text.Length == 0)
                {
                    e.Handled = false;
                }
                else
                    e.Handled = true;
            }

            else if (e.KeyChar == (char)'+') // Signo +
                if (caja.Text.Length == 0)
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
                btContinuar.Location = new Point(radioButton1.Location.X, radioButton1.Location.Y + radioButton1.Height + 10);

                if (caja.Text.Length == 0)
                    e.Handled = true;
                else
                {
                    if (variaciones || combinaciones)
                    {
                        if (caja.Name == "tbFilas")
                        {
                            tbcolumnas.Focus();
                        }
                        else if (caja.Name == "tbcolumnas")
                        {
                            btContinuar.Show();
                            btContinuar.Click += btContinuar_Click;
			    btDefecto.Hide();
                        }
                        e.Handled = true;
                    }
                    else if (permutaciones)
                    {
                        btContinuar.Show();
                        btContinuar.Click += btContinuar_Click;
                        e.Handled = true;
			btDefecto.Hide();
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

        ///<Summary>
        ///
        /// PONE EL ATRIBUTO VARIACIONES EN TRUE Y MUESTRA LAS CAJAS Y ETIQUETAS PARA LA TOMA DE CANTIDAD TOTAL DE ELEMENTOS, CANTIDAD DE ELEMENTOS DE CADA CONJUNTO Y 
        /// EL RADIOBUTTON PARA LA OPCION DE CON O SIN REPETICION
        ///
        ///</Summary>
        ///
        private void radioButton1_Click(object sender, EventArgs e)
        {
            variaciones = true;
            radioButton2.Hide();
            radioButton3.Hide();
            EtiquetaFilas.Show();
            EtiquetaFilas.Text = "Cantidad total de elementos ( m ):";
            EtiquetaFilas.AutoSize = true;
            EtiquetaFilas.Location = new Point(30, 100);
            tbFilas.Show();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            EtiquetaColumnas.Show();
            EtiquetaColumnas.Text = "Cantidad de elementos de cada conjunto ( n ):";
            EtiquetaColumnas.AutoSize = true;
            EtiquetaColumnas.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 10);
            tbcolumnas.Show();
            tbcolumnas.Location = new Point(EtiquetaColumnas.Location.X + EtiquetaColumnas.Width + 5, EtiquetaColumnas.Location.Y);
            tbFilas.Location = new Point(tbcolumnas.Location.X, EtiquetaFilas.Location.Y);
            radioButton1.Location = new Point(EtiquetaColumnas.Location.X, EtiquetaColumnas.Location.Y + EtiquetaColumnas.Height + 10);
            radioButton1.Text = "Con repetición de elementos.";
            radioButton1.Click -= radioButton1_Click;
            radioButton1.Click += radioButton1_Click2;
            radioButton1.Checked = false;
            lbExplicacion.Text = "Introduzca la cantidad total de elementos 'm'( población ) y la cantidad de elementos de cada conjunto 'n'( muestra ).\nChequear el botón de selección para el calculo de variaciones con repetición de elementos.\nO pulse el botón [E] para resolución de ejemplo con valores por defecto.";
            tbFilas.Focus();
	if(!directa)
	{
	    btDefecto.Show();
	    btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
	    this.Text = "Matedicom Cálculo de Variaciones";
	}
	   
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            permutaciones = true;
            radioButton2.Hide();
            radioButton3.Hide();
            EtiquetaFilas.Show();
            EtiquetaFilas.Text = "Cantidad total de elementos ( m ):";
            EtiquetaFilas.AutoSize = true;
            EtiquetaFilas.Location = new Point(30, 100);
            tbFilas.Show();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Width + 5, EtiquetaFilas.Location.Y);
            radioButton1.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 10);
            radioButton1.Text = "Con repetición de elementos.";
            radioButton1.Click -= radioButton1_Click;
            radioButton1.Click += radioButton1_Click2;
            radioButton1.Checked = false;
            lbExplicacion.Text = "Introduzca la cantidad total de elementos ( población ).\nChequear el botón de selección para el calculo de permutaciones con repetición de elementos.";
            tbFilas.Focus();
            this.AcceptButton = null;
	 if(!directa)
	 {
	     btDefecto.Show();
	    btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
		this.Text ="Matedicom Cálculo de Permutaciones.";
	 }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            combinaciones = true;
            radioButton3.Hide();
            radioButton2.Hide();
            EtiquetaFilas.Show();
            EtiquetaFilas.Text = "Cantidad total de elementos ( m ):";
            EtiquetaFilas.AutoSize = true;
            EtiquetaFilas.Location = new Point(30, 100);
            tbFilas.Show();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            EtiquetaColumnas.Show();
            EtiquetaColumnas.Text = "Cantidad de elementos de cada conjunto ( n ):";
            EtiquetaColumnas.AutoSize = true;
            EtiquetaColumnas.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 10);
            tbcolumnas.Show();
            tbcolumnas.Location = new Point(EtiquetaColumnas.Location.X + EtiquetaColumnas.Width + 5, EtiquetaColumnas.Location.Y);
            tbFilas.Location = new Point(tbcolumnas.Location.X, EtiquetaFilas.Location.Y);
            radioButton1.Location = new Point(EtiquetaColumnas.Location.X, EtiquetaColumnas.Location.Y + EtiquetaColumnas.Height + 10);
            radioButton1.Text = "Con repetición de elementos.";
            radioButton1.Click -= radioButton1_Click;
            radioButton1.Click += radioButton1_Click2;
            radioButton1.Checked = false;
            lbExplicacion.Text = "Introduzca la cantidad total de elementos ( población ) y la cantidad de elementos de cada conjunto ( muestra ).\nChequear el botón de selección para el calculo de combinaciones con repetición de elementos.";
            tbFilas.Focus();
	if(!directa)
	  {
	     btDefecto.Show();
	    btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
		this.Text = "Matedicom Cálculo de Combinaciones.";
	  }
        }

        private void radioButton1_Click2(object sender, EventArgs e)
        {
            contabutton++;
            if (contabutton % 2 == 0)
                radioButton1.Checked = false;
        }

        private void btContinuar_Click(object sender, EventArgs e)
        {
            if (paso == 0)
            {
                if (variaciones || combinaciones)
                {
                    poblacion = Int32.Parse(tbFilas.Text);
                    muestra = Int32.Parse(tbcolumnas.Text);

                    if (muestra > poblacion)
                    {
                        MessageBox.Show("La cantidad de elementos de los conjuntos ( n ), ha de ser menor o igual a la cantidad total de elementos ( m )");
                        tbFilas.ResetText();
                        tbcolumnas.ResetText();
                        tbFilas.Focus();
                        btContinuar.Hide();
                        radioButton1.Checked = false;
                        return;
                    }
                }
                else if (permutaciones)
                {
                    poblacion = Int32.Parse(tbFilas.Text);
                }
            }
            IniciarResolucion();
            paso++;
        }


        /// <summary>
        /// 
        /// REALIZA LA RESOLUCION PASO A PASO 
        /// 
        /// </summary>
        /// 
        private void IniciarResolucion()
        {
            if (paso == 0)
            {
                btContinuar.Location = new Point(EtiquetaColumnas.Location.X, EtiquetaColumnas.Location.Y + EtiquetaColumnas.Height + 50);
                label25.Show();
                label25.Font = radioButton1.Font;
                label25.BackColor = Color.SeaGreen;
                label25.Location = radioButton1.Location;
                radioButton1.Hide();
                if (directa)
                    lbExplicacion.Hide();
                if (radioButton1.Checked)
                {
                    repeticion = true;
                    label25.Text = "Con repetición de elementos.";
                }
                else
                    label25.Text = "Sin repetición de elementos.";
                if (variaciones)
                {
                    if (!repeticion)
                        lbExplicacion.Text = "Las variaciones sin repetición son la cantidad de conjuntos distintos que se pueden formar con una cantidad igual o menor de elementos que la cantidad total de los mismos. Se considera un conjunto distinto de otro si el orden, o alguno de sus elementos, es diferente al del otro.\nLa notación de las variaciones, se lee: ' Variaciones de m elementos tomados de n en n '.Donde m, es la cantidad total de elementos o población, y n es la cantidad de elementos que contiene cada conjunto o muestra.  ";

                    else if (repeticion)
                        lbExplicacion.Text = "Las variaciones con repetición son la cantidad de conjuntos distintos que se pueden formar con una cantidad igual o menor de elementos que la cantidad total de los mismos.En las variaciones con repeticion, se considera un conjunto diferente de otro, únicamente si los elementos están distribuidos en un orden distinto al del otro.\nLa notación de las variaciones, se lee: ' Variaciones de m elementos tomados de n en n '.Donde m, es la cantidad total de elementos o población, y n es la cantidad de elementos que contiene cada uno de los conjuntos a formar.  ";
                    label1.Show();
                    label1.Location = new Point(btContinuar.Location.X + 10, btContinuar.Location.Y + btContinuar.Height + 50);
                    label1.Text = "V  → V =";
                    label1.Font = new Font(label1.Font.FontFamily, 14, FontStyle.Bold);
                    label2.Show();
                    label2.TextAlign = ContentAlignment.MiddleLeft;
                    label2.Font = new Font(label2.Font.FontFamily, 8);
                    label2.Location = new Point(label1.Location.X + 20, label1.Location.Y - 11);
                    label2.Text = "m";
                    label3.Show();
                    label3.Font = label2.Font;
                    label3.Location = new Point(label2.Location.X, label1.Location.Y + 22);
                    label3.Text = "n";
                    label20.Show();
                    label20.BackColor = Color.SeaGreen;
                    label20.Font = label2.Font;
                    label20.Location = new Point(label1.Location.X + label1.Width - 35, label2.Location.Y);
                    label20.Text = poblacion.ToString();
                    label21.Show();
                    label21.BackColor = Color.SeaGreen;
                    label21.Font = label20.Font;
                    label21.Location = new Point(label20.Location.X, label3.Location.Y);
                    label21.Text = muestra.ToString();

                }
                else if (permutaciones)
                {
                    if (!repeticion)
                    {
                        btContinuar.Location = new Point(EtiquetaFilas.Location.X, 200);
                        lbExplicacion.Text = "Las permutaciones sin repetición, son la cantidad de conjuntos distintos que se pueden formar con una cantidad de elementos o población ( m ). \nSe considera un conjunto distinto de otro, únicamente si el orden en el que están dispuestos sus elementos es distinto al del otro.\nLa notación de las permutaciones sin repetición se lee: ' Permutaciones de m elementos '.";
                        label1.Show();
                        label1.Location = new Point(btContinuar.Location.X + 10, btContinuar.Location.Y + btContinuar.Height + 10);
                        label1.Text = "P  → P =";
                        label1.Font = new Font(label1.Font.FontFamily, 14, FontStyle.Bold);
                        label2.Show();
                        label2.TextAlign = ContentAlignment.MiddleLeft;
                        label2.Font = new Font(label2.Font.FontFamily, 8);
                        label2.Location = new Point(label1.Location.X + 10, label1.Location.Y + 22);
                        label2.Text = "m         " + poblacion.ToString();
                        label2.BackColor = Color.SeaGreen;
                        if (directa)
                        {
                            paso++;
                            IniciarResolucion();
                        }
                    }
                    else if (repeticion)
                    {
                        btContinuar.Hide();
                        if (repeticion)
                            radioButton1.Checked = true;
                        lbExplicacion.Text = "Las permutaciones con repeticion, donde un elemento se repite ' a ' veces, otro ' b ' veces etc., son la cantidad de conjuntos distintos que se pueden formar con una cantidad de elementos o población ( m ). \nSe considera un conjunto distinto de otro, únicamente si el orden en el que están dispuestos sus elementos es distinto al del otro.\nIntroduzca la cantidad de veces que se repiten los elementos repetidos";
                        label1.Show();
                        label1.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
                        label1.Font = EtiquetaFilas.Font;
                        label1.BackColor = Color.SeaGreen;
                        repetido = 'a';
                        label1.Text = "Cantidad de repeticiones:";
                        textBox1.Show();
                        textBox1.Location = new Point(label1.Location.X + label1.Width + 5, label1.Location.Y);
                        textBox1.Focus();
                        textBox1.BackColor = Color.White;
                        textBox1.KeyPress += Repeticiones_KeyPress;
                        label2.Show();
                        label2.Location = new Point(textBox1.Location.X, textBox1.Location.Y - textBox1.Height);
                        label2.BackColor = Color.SeaGreen;
                        label2.Font = new Font("Dejavu Sans", 12, FontStyle.Underline);
                        label2.Text = "" + repetido;
                        cajas = new List<TextBox>();
                        etiquetas = new List<Label>();
                        etiquetas.Add(label2);
                        repeticiones = new List<int>();
                        cajas.Add(textBox1);
                    }

                }
                else if (combinaciones)
                {
                    if (!repeticion)
                        lbExplicacion.Text = "Las combinaciones sin repetición son la cantidad de conjuntos distintos que se pueden formar con una cantidad igual o menor de elementos que la cantidad total de los mismos. Se considera un conjunto distinto de otro, si alguno de sus elementos es diferente al del otro, sin importar el orden y sin que se repitan elementos.\nLa notación de las combinaciones se lee: ' Combinaciones de m elementos tomados de n en n '.Donde m, es la cantidad total de elementos o población, y n es la cantidad de elementos que contiene cada conjunto o muestra.  ";

                    else if (repeticion)
                        lbExplicacion.Text = "Las combinaciones con repetición son la cantidad de conjuntos distintos que se pueden formar con una cantidad igual o menor de elementos que la cantidad total de los mismos.En las combinaciones con repeticion, se considera un conjunto diferente de otro, si los elementos de un conjunto son distintos al del otro, pudiendose repetir los elementos y sin importar el orden.\nLa notación de las combinaciones se lee: ' Combinaciones de m elementos tomados de n en n '.Donde m, es la cantidad total de elementos o población, y n es la cantidad de elementos que contiene cada uno de los conjuntos a formar.  ";

                    label1.Show();
                    label1.Location = new Point(btContinuar.Location.X + 10, btContinuar.Location.Y + btContinuar.Height + 10);
                    if (!repeticion)
                        label1.Text = "C  → C =";
                    else if (repeticion)
                        label1.Text = "CR  → CR =";
                    label1.Font = new Font(label1.Font.FontFamily, 14, FontStyle.Bold);
                    label2.Show();
                    label2.TextAlign = ContentAlignment.MiddleLeft;
                    label2.Font = new Font(label2.Font.FontFamily, 8);
                    if (!repeticion)
                        label2.Location = new Point(label1.Location.X + 20, label1.Location.Y - 11);
                    else
                        label2.Location = new Point(label1.Location.X + 25, label1.Location.Y - 11);
                    label2.Text = "m";
                    label3.Show();
                    label3.Font = label2.Font;
                    label3.Location = new Point(label2.Location.X, label1.Location.Y + 22);
                    label3.Text = "n";
                    label20.Show();
                    label20.BackColor = Color.SeaGreen;
                    label20.Font = label2.Font;
                    label20.Location = new Point(label1.Location.X + label1.Width - 35, label2.Location.Y);
                    label20.Text = poblacion.ToString();
                    label21.Show();
                    label21.BackColor = Color.SeaGreen;
                    label21.Font = label20.Font;
                    label21.Location = new Point(label20.Location.X, label3.Location.Y);
                    label21.Text = muestra.ToString();
                }
                if (directa)
                {
                    if (!permutaciones)
                    {
                        paso++;
                        IniciarResolucion();
                    }
                }
            }
            else if (paso == 1)
            {
                if (variaciones)
                {
                    label4.Show();
                    label4.BackColor = Color.SeaGreen;
                    label5.Show();
                    label5.BackColor = Color.SeaGreen;
                    label5.Font = new Font(label1.Font.FontFamily, label1.Font.Size, FontStyle.Regular);
                    if (!repeticion)
                    {
                        lbExplicacion.Text = "Las variaciones sin repetición, son el cociente entre el factorial de la cantidad total de elementos m!, y el factorial de la cantidad total de elementos menos la cantidad de elementos de cada cojunto ( m-n )!";
                        label4.Location = new Point(label1.Location.X + label1.Width + 5, label2.Location.Y);
                        label4.Text = "    m!    ";
                        label4.TextAlign = ContentAlignment.MiddleCenter;
                        label4.Font = new Font(label1.Font.FontFamily, label1.Font.Size, FontStyle.Underline);
                        label5.Location = new Point(label4.Location.X, label4.Location.Y + label4.Height);
                        label5.TextAlign = ContentAlignment.MiddleCenter;
                        label5.Text = "( m-n )!";
                        label4.Width = label5.Width;
                    }
                    else if (repeticion)
                    {
                        lbExplicacion.Text = "Las variaciones con repetición, son la cantidad de elementos o población ( m ), elevada a la cantidad de elementos de cada conjunto o muestra ( n ).";
                        label4.Location = new Point(label1.Location.X + label1.Width + 5, label1.Location.Y);
                        label4.Text = "m";
                        label4.Font = label1.Font;
                        label5.Location = new Point(label4.Location.X + label4.Width - 5, label2.Location.Y);
                        label5.Text = "n";
                    }
                    if (directa)
                    {
                        paso++;
                        IniciarResolucion();
                    }
                }
                else if (permutaciones)
                {
                    if (!repeticion)
                    {
                        lbExplicacion.Text = "Las permutaciones sin repetición son el factorial de la cantidad de elementos o población.";
                        label3.Show();
                        label3.Font = label1.Font;
                        label3.BackColor = Color.SeaGreen;
                        label3.Location = new Point(label1.Location.X + label1.Width + 5, label1.Location.Y);
                        label3.Text = poblacion.ToString() + "! = ";
                    }
                    else if (repeticion)
                    {
                        lbExplicacion.Text = "La notación de las permutaciones con repetición se lee: Permutaciones de m elementos con repeticion de 'a' veces , 'b' veces , etc.";
                        label1.Show();
                        label1.Location = new Point(btContinuar.Location.X + 10, btContinuar.Location.Y + btContinuar.Height + 10);
                        label1.Text = "PR  → PR =";
                        label1.Font = new Font(label1.Font.FontFamily, 14, FontStyle.Bold);
                        label2.Show();
                        label2.TextAlign = ContentAlignment.MiddleLeft;
                        label2.Font = new Font(label2.Font.FontFamily, 8);
                        label2.Location = new Point(label1.Location.X + 20, label1.Location.Y - 11);
                        string aux = "";
                        repetido--;
                        while (repetido >= 'a')
                        {
                            aux = aux.Insert(0, repetido + ",");
                            repetido--;
                        }
                        label2.Text = aux;
                        label3.Show();
                        label3.Font = label2.Font;
                        label3.Location = new Point(label2.Location.X, label1.Location.Y + 22);
                        label3.Text = "m";
                        label4.Show();
                        label4.Location = new Point(label1.Location.X + label1.Width - 30, label2.Location.Y);
                        label4.ResetText();
                        foreach (int i in repeticiones)
                            label4.Text += i.ToString() + " , ";
                        label4.Font = label3.Font;
                        label4.BackColor = Color.SeaGreen;
                        label5.Show();
                        label5.Font = label3.Font;
                        label5.BackColor = Color.SeaGreen;
                        label5.Location = new Point(label4.Location.X, label3.Location.Y);
                        label5.Text = poblacion.ToString();
                        btContinuar.Location = new Point(btContinuar.Location.X, label5.Location.Y + label5.Height + 10);
                        btContinuar.Show();
                        if (repeticion)
                        {
                            paso++;
                            IniciarResolucion();
                        }

                    }
                    if (directa)
                    {
                        paso++;
                        IniciarResolucion();
                    }
                }
                else if (combinaciones)
                {
                    label4.Show();
                    label4.BackColor = Color.SeaGreen;
                    label5.Show();
                    label5.BackColor = Color.SeaGreen;
                    label5.Font = new Font(label1.Font.FontFamily, label1.Font.Size, FontStyle.Regular);
                    if (!repeticion)
                    {
                        lbExplicacion.Text = "Las combinaciones sin repetición, son el cociente entre las variaciones y las permutaciones, por lo tanto el factorial de la cantidad total de elementos m!, y el factorial de la cantidad total de elementos menos la cantidad de elementos de cada cojunto ( m-n )! multiplicado por el factorial de la cantidad de elementos de cada conjunto o población.";
                        label4.Location = new Point(label1.Location.X + label1.Width + 5, label2.Location.Y);
                        label4.Text = "       m!       ";
                        label4.TextAlign = ContentAlignment.MiddleCenter;
                        label4.Font = new Font(label1.Font.FontFamily, label1.Font.Size, FontStyle.Underline);
                        label5.Location = new Point(label4.Location.X, label4.Location.Y + label4.Height);
                        label5.TextAlign = ContentAlignment.MiddleCenter;
                        label5.Text = "n! * ( m-n )!";
                        label4.Width = label5.Width;
                    }
                    else if (repeticion)
                    {
                        lbExplicacion.Text = "Las combinaciones con repetición, son el cociente entre el factorial de la suma de la cantidad total de elementos con la cantidad de elementos de cada conjunto menos uno ( m+n-1)!, y el producto entre el factorial de la cantidad de elementos de cada conjunto y el factorial de la cantidad total de elementos menos uno n! * ( m -1 )!.";
                        label4.Location = new Point(label1.Location.X + label1.Width + 5, label2.Location.Y);
                        label4.Text = "( m + n - 1 )!";
                        label4.TextAlign = ContentAlignment.MiddleCenter;
                        label4.Font = new Font(label1.Font.FontFamily, label1.Font.Size, FontStyle.Underline);
                        label5.Location = new Point(label4.Location.X, label4.Location.Y + label4.Height);
                        label5.TextAlign = ContentAlignment.MiddleCenter;
                        label5.Text = "n! * ( m - 1 )!";
                        label4.Width = label5.Width;
                    }
                    if (directa)
                    {
                        paso++;
                        IniciarResolucion();
                    }

                }

            }
            else if (paso == 2)
            {
                if (variaciones)
                {
                    if (!repeticion)
                    {
                        lbExplicacion.Text = "Calculando los factoriales y el cociente entre ambos, se obtiene el número de variaciones.";
                        label7.Show();
                        label6.Show();
                        label6.Font = label1.Font;
                        label6.Location = new Point(label5.Location.X + label5.Width, label1.Location.Y);
                        label6.Text = " = ";
                        label6.BackColor = Color.SeaGreen;
                        label7.Font = new Font(label4.Font.FontFamily, label4.Font.Size, FontStyle.Underline);
                        label7.BackColor = Color.SeaGreen;
                        label7.Location = new Point(label6.Location.X + label6.Width, label4.Location.Y);
                        label7.Text = poblacion.ToString() + "!";
                        label8.Show();
                        label8.Font = label5.Font;
                        label8.BackColor = Color.SeaGreen;
                        label8.Location = new Point(label7.Location.X, label5.Location.Y);
                        label8.Text = "( " + poblacion.ToString() + " - " + muestra.ToString() + " )!";
                        label7.TextAlign = ContentAlignment.MiddleCenter;
                        label7.Height += 5;
                        label7.Width = label8.Width;
                        if (label7.Text.Length < label8.Text.Length)
                        {
                            int diferencia = (label8.Text.Length - label7.Text.Length);
                            for (int i = 0; i < diferencia; i++)
                            {
                                if (i % 2 == 0)
                                    label7.Text = label7.Text.Insert(0, " ");
                                else
                                    label7.Text = label7.Text.Insert(label7.Text.Length, " ");
                            }
                        }
                        label9.Show();
                        label9.Location = new Point(label8.Location.X + label8.Width + 5, label6.Location.Y);
                        label9.BackColor = Color.SeaGreen;
                        label9.Text = " = ";
                        label10.Show();
                        label10.Location = new Point(label9.Location.X + label9.Width + 5, label6.Location.Y);
                        label10.Font = label8.Font;
                        label10.BackColor = Color.Chartreuse;
                        label10.Text = Matematicas.Combinatoria.Variaciones(poblacion, muestra, false).ToString();
                        btContinuar.Hide();
                        lbExplicacion.Focus();
                    }
                    else if (repeticion)
                    {
                        lbExplicacion.Text = "Elevando la cantidad total de elementos ( m ), a la cantidad de elementos de cada conjunto ( n ), obtenemos la cantidad de variaciones con repetición de " + poblacion.ToString() + " elementos, tomados de " + muestra.ToString() + " en " + muestra.ToString();
                        label6.Show();
                        label6.BackColor = Color.SeaGreen;
                        label6.Font = label4.Font;
                        label6.Text = " = ";
                        label6.Location = new Point(label5.Location.X + label5.Width + 2, label4.Location.Y);
                        label7.Show();
                        label7.Font = label4.Font;
                        label7.Location = new Point(label6.Location.X + label6.Width + 3, label4.Location.Y);
                        label7.BackColor = Color.SeaGreen;
                        label7.Text = poblacion.ToString();
                        label8.Show();
                        label8.Font = label5.Font;
                        label8.BackColor = Color.SeaGreen;
                        label8.Location = new Point(label7.Location.X + label7.Width, label5.Location.Y);
                        label8.Text = muestra.ToString();
                        label9.Show();
                        label9.Location = new Point(label8.Location.X + label8.Width + 2, label6.Location.Y);
                        label9.BackColor = Color.SeaGreen;
                        label9.Font = label6.Font;
                        label9.Text = " = ";
                        label10.Show();
                        label10.Location = new Point(label9.Location.X + label9.Width + 2, label9.Location.Y);
                        label10.Font = label9.Font;
                        label10.BackColor = Color.Chartreuse;
                        label10.Text = Matematicas.Combinatoria.Variaciones(poblacion, muestra, true).ToString();
                        btContinuar.Hide();
                        lbExplicacion.Focus();
                    }
                }
                else if (permutaciones)
                {
                    if (!repeticion)
                    {
                        lbExplicacion.Text = "Calculando el factorial, obtenemos la cantidad de permutaciones sin repetición de la cantidad de elementos m.";
                        label4.Show();
                        label4.Location = new Point(label3.Location.X + label3.Width + 5, label3.Location.Y);
                        label4.BackColor = Color.Chartreuse;
                        label4.Font = label3.Font;
                        label4.Text = Matematicas.Combinatoria.Permutaciones(poblacion).ToString();
                        btContinuar.Hide();

                    }
                    else
                    {
                        lbExplicacion.Text = "Las permutaciones con repetición son el cociente entre el factorial de la cantidad total de elementos, y el producto de los factoriales de las repeticiones de cada uno de los elementos repetidos.";
                        label6.Show();
                        label6.AutoSize = true;
                        label6.Font = new Font(label3.Font.FontFamily, 12, FontStyle.Underline);
                        label6.BackColor = Color.SeaGreen;
                        label6.Text = poblacion.ToString() + "!";
                        label6.TextAlign = ContentAlignment.MiddleCenter;
                        label6.Location = new Point(label4.Location.X + label4.Width + 5, label1.Location.Y - 10);
                        label7.Show();
                        label7.AutoSize = true;
                        label7.Font = new Font(label6.Font.FontFamily, 12);
                        label7.Location = new Point(label6.Location.X, label6.Location.Y + 20);
                        label7.BackColor = Color.SeaGreen;
                        label7.ResetText();
                        foreach (int i in repeticiones)
                            label7.Text += i + "! * ";
                        string aux = label7.Text;
                        aux = aux.Substring(0, aux.Length - 2);
                        label7.Text = aux;
                        if (label6.Width < label7.Width)
                        {
                            string lect = label6.Text;
                            int dif = label7.Text.Length - label6.Text.Length;
                            for (int i = 0; i < dif; i++)
                                if (i % 2 == 0)
                                    lect += " ";
                                else
                                    lect = lect.Insert(0, " ");
                            label6.Text = lect;
                        }
                        else
                        {
                            int dif = label6.Text.Length - label7.Text.Length;
                            for (int i = 0; i < dif; i++)
                                if (i % 2 == 0)
                                    label7.Text += " ";
                                else
                                    label7.Text.Insert(0, " ");
                        }
                        if (directa && !repeticion)
                            btContinuar.PerformClick();
                        else
                        {
                            paso++;
                            IniciarResolucion();
                        }
                    }
                }
                else if (combinaciones)
                {
                    if (!repeticion)
                    {
                        lbExplicacion.Text = "Calculando los factoriales y el cociente entre ambos, se obtiene el número de combinaciones.";
                        label7.Show();
                        label6.Show();
                        label6.Font = label1.Font;
                        label6.Location = new Point(label5.Location.X + label5.Width, label1.Location.Y);
                        label6.Text = " = ";
                        label6.BackColor = Color.SeaGreen;
                        label7.Font = label4.Font;
                        label7.BackColor = Color.SeaGreen;
                        label7.Location = new Point(label6.Location.X + label6.Width, label4.Location.Y);
                        label7.Text = poblacion.ToString() + "!";
                        label8.Show();
                        label8.Font = label5.Font;
                        label8.BackColor = Color.SeaGreen;
                        label8.Location = new Point(label7.Location.X, label5.Location.Y);
                        label8.Text = muestra.ToString() + "! * ( " + poblacion.ToString() + " - " + muestra.ToString() + " )!";
                        label7.TextAlign = ContentAlignment.MiddleCenter;
                        // label7.AutoSize = false;
                        label7.Height += 5;
                        label7.Width = label8.Width;
                        if (label7.Text.Length < label8.Text.Length)
                        {
                            int diferencia = (label8.Text.Length - label7.Text.Length);
                            for (int i = 0; i < diferencia; i++)
                            {
                                if (i % 2 == 0)
                                    label7.Text = label7.Text.Insert(0, " ");
                                else
                                    label7.Text = label7.Text.Insert(label7.Text.Length, " ");
                            }
                        }
                        label9.Show();
                        label9.Location = new Point(label8.Location.X + label8.Width + 5, label6.Location.Y);
                        label9.BackColor = Color.SeaGreen;
                        label9.Text = " = ";
                        label10.Show();
                        label10.Location = new Point(label9.Location.X + label9.Width + 5, label6.Location.Y);
                        label10.Font = label8.Font;
                        label10.BackColor = Color.Chartreuse;
                        label10.Text = Matematicas.Combinatoria.Combinaciones(poblacion, muestra, false).ToString();
                        btContinuar.Hide();
                    }
                    else if (repeticion)
                    {
                        lbExplicacion.Text = "Calculando los factoriales y el cociente entre ambos, se obtiene el número de combinaciones.";
                        label7.Show();
                        label6.Show();
                        label6.Font = label1.Font;
                        label6.Location = new Point(label5.Location.X + label5.Width, label1.Location.Y);
                        label6.Text = " = ";
                        label6.BackColor = Color.SeaGreen;
                        label7.Font = label4.Font;
                        label7.BackColor = Color.SeaGreen;
                        label7.Location = new Point(label6.Location.X + label6.Width, label4.Location.Y);
                        label7.Text = "( " + poblacion.ToString() + " + " + muestra.ToString() + " - 1 )!";
                        label8.Show();
                        label8.Font = label5.Font;
                        label8.BackColor = Color.SeaGreen;
                        label8.Location = new Point(label7.Location.X, label5.Location.Y);
                        label8.Text = muestra.ToString() + "! * ( " + poblacion.ToString() + " - 1 )!";
                        label7.TextAlign = ContentAlignment.MiddleCenter;
                        label7.AutoSize = true;
                        label7.Height += 5;
                        //label7.Width = label8.Width;
                        if (label7.Text.Length < label8.Text.Length)
                        {
                            int diferencia = (label8.Text.Length - label7.Text.Length);
                            for (int i = 0; i < diferencia; i++)
                            {
                                if (i % 2 == 0)
                                    label7.Text = label7.Text.Insert(0, " ");
                                else
                                    label7.Text = label7.Text.Insert(label7.Text.Length, " ");
                            }
                        }
                        label9.Show();
                        label9.Location = new Point(label8.Location.X + label8.Width + 5, label6.Location.Y);
                        label9.BackColor = Color.SeaGreen;
                        label9.Text = " = ";
                        label10.Show();
                        label10.Location = new Point(label9.Location.X + label9.Width + 5, label6.Location.Y);
                        label10.Font = label8.Font;
                        label10.BackColor = Color.Chartreuse;
                        label10.Text = Matematicas.Combinatoria.Combinaciones(poblacion, muestra, true).ToString();
                        btContinuar.Hide();
                    }
                    if (directa)
                    {
                        paso++;
                        IniciarResolucion();
                    }
                }
                lbExplicacion.Focus();
            }
            else if (paso == 3)
            {
                if (permutaciones) // Solo para permutaciones con repeticion
                {
                    lbExplicacion.Text = "Realizando los productos y el cociente anteriores, obtenemos la cantidad de permutaciones con repetición según los datos iniciales.";
                    label8.Show();
                    int largoanterior = 0;
                    if (label6.Width > label7.Width)
                        largoanterior = label6.Location.X + label6.Width;
                    else
                        largoanterior = label7.Location.X + label7.Width;
                    label9.Show();
                    label9.Location = new Point(largoanterior, label1.Location.Y);
                    label9.BackColor = Color.SeaGreen;
                    label9.Text = " = ";
                    label8.Location = new Point(label9.Location.X + label9.Width + 5, label1.Location.Y);
                    label8.BackColor = Color.Chartreuse;
                    label8.Font = label1.Font;
                    long[] reps = new long[repeticiones.Count];
                    for (int i = 0; i < repeticiones.Count; i++)
                        reps[i] = repeticiones[i];
                    label8.Text = Matematicas.Combinatoria.Permutaciones(poblacion, reps).ToString();
                    btContinuar.Hide();
                    lbExplicacion.Focus();
                }
            }

        }
           
        /// <summary>
        /// 
        ///  CONTROLA QUE SOLO SE PUEDAN INTRODUCIR ENTEROS NO NEGATIVOS Y AÑADE UN NUEVA CAJA CUANDO SE PULSA
        ///  INTRO, SI LA CAJA ESTÁ VACIA, NO AÑADE MAS CAJAS Y CONTINUA LA RESOLUCION DE LAS PERMUTACIONES
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

            private void Repeticiones_KeyPress(object sender, KeyPressEventArgs e)
            {
                TextBox caja = (TextBox)sender;
                if (char.IsDigit(e.KeyChar))
                    e.Handled = false;
                else if (e.KeyChar == (char)8) // Retroceso
                    e.Handled = false;
                else if(e.KeyChar == (char)13)//Intro
                {
                    if ((caja.Text.Length > 0))
                    {
                        repeticiones.Add(Int32.Parse(cajas[cajas.Count - 1].Text));
                        sumarepetidos += repeticiones[repeticiones.Count - 1];
                        if(sumarepetidos > poblacion)
                        {
                            MessageBox.Show("La suma de los elementos repetidos, no puede ser mayor que la cantidad total de elementos.");
                            e.Handled = true;
                            caja.ResetText();
                            caja.Focus();
                            return;
                        }
                        cajas.Add(new TextBox());
                        Controls.Add(cajas[cajas.Count - 1]);
                        cajas[cajas.Count - 1].BackColor = cajas[cajas.Count - 2].BackColor;
                        cajas[cajas.Count - 1].Font = cajas[cajas.Count - 2].Font;
                        cajas[cajas.Count - 1].Size = cajas[cajas.Count - 2].Size;
                        cajas[cajas.Count - 1].Location = new Point(cajas[cajas.Count - 2].Location.X + cajas[cajas.Count - 2].Width + 5, cajas[cajas.Count - 2].Location.Y);
                        cajas[cajas.Count - 1].KeyPress += Repeticiones_KeyPress;
                        cajas[cajas.Count - 1].Focus();
                        cajas[cajas.Count - 1].Show();
                        etiquetas.Add(new Label());
                        Controls.Add(etiquetas[etiquetas.Count - 1]);
                        etiquetas[etiquetas.Count - 1].BackColor = label2.BackColor;
                        etiquetas[etiquetas.Count - 1].Font = label2.Font;
                        etiquetas[etiquetas.Count - 1].Width = label2.Width;
                        etiquetas[etiquetas.Count - 1].Location = new Point(etiquetas[etiquetas.Count - 2].Location.X + cajas[0].Width + 5 , label2.Location.Y);
                        etiquetas[etiquetas.Count-1].Text = ""+(char)((int)repetido + 1);
                        etiquetas[etiquetas.Count - 1].Show();
                        repetido++;
                        e.Handled = true;
                    }
                    else
                    {
                        Controls.RemoveAt(Controls.Count-1);
                        Controls.RemoveAt(Controls.Count-1);
                        paso = 1;
                        e.Handled = true;
                        foreach (TextBox t in cajas)
                            t.Hide();
                        foreach (Label l in etiquetas)
                            l.Hide();
                        IniciarResolucion();
                    }
                }
                else
                    e.Handled = true;
            }





    }
}

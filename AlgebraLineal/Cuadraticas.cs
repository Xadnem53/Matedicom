using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matematicas;
using System.Drawing;
using System.Windows.Forms;

namespace AlgebraLineal
{
    class Cuadraticas:FormularioBase
    {
        new bool directa; // Tipo de resolucion
        bool submenu = true; // Se usará para distinguir cuando se ha de salir al submenu o al menu de Algebra Lineal
        long[,] simetricaejemplo = new long[,] { { 2, 3, 1 }, { 3, -5, 7 }, { 1, 7, 8 } }; // Matriz simetrica que será usada como ejemplo
        int avance = 0; // Para controlar en que paso del proceso se está
        Label [,] simetrica; // Para mostrar la matriz simetrica

        int contador2 = 0; // Contador auxiliar;
        TextBox[,] simetric; // Matriz de cajas de texto que será la matriz simétrica
        List<Label> rotulosh; // Serie horizontal de rotulos con las variables a,b,c ... etc correspondientes a la columnas 
        List<Label> rotulosv; // Serie vertical de rotulos con las variables a,b,c ... etc correspondientes a la filas 
        long[,] matrizlong; // Será la matriz simetrica en formato long.
        Racional[,] matrizdiagonal; // Será la matriz diagonal en formato racional.
        Racional[,] modalracional; // Será la matriz modal en formato racional.
        Racional[,] modaltraspuesta; // Sera la traspuesta de la matriz modal en formato racional
        long[,] matrizmodal; // Será la matriz modal en la resolucion por el metodo de la matriz ortogonal
        Label[,] diagonal; // Para mostrar la matriz diagonal en la resolucion por el metodo de la matriz ortogonal
        Label[,] modal; // Para mostrar la matriz modal en la resolucion por el metodo de la matriz ortogonal
        Polinomio forma; // Matriz simetrica en forma polinomica
        List<Polinomio> vectores; // Para guardar los vectores que se construyen con la matriz modal
        List<List<object>> vectoresnormalizados; // Para guardar los vectores normalizados
        List<string> tipos; // Para guardar la secuencia de tipos Racional o RacionalRadical de cada vector en vectoresnormalizados
       
        List<Polinomio> cambiodevariables; // Para guardar los polinomios equivalentes en la resolucion por el metodo de Lagrange
        List<Termino> cuadraticas; // Lista de terminos que representaran a cada una de las formas cuadraticas en el lado izquierdo de la igualdad
        List<Polinomio> formas;  // lista donde se guardaran las formas equivalentes en cada iteracion (polinomio formasiguiente)
        Termino forma1; // Será el termino que representa a la primera forma cuadratica
        int iteraciones = 0; // Para contar la cantidad de veces que se aplica el metodo de sustitucion de una nueva variable por  el cuadrado de una suma.
        Racional coeficienteelegido; // Para sacar factor comun en la resolucion por el metodo de Lagrange
        bool cuadradoperfecto = false; // Para registrar si el coeficiente del termino elegido es un cuadrado perfecto o no lo es.
        Polinomio formainicial; // Copia de la forma anterior que se usa para operar sin alterar la forma anterior
        Termino factorcomun; // Termino que se usara para sacar factor comun en el metodo de Lagrange
        Polinomio comunes; // Para guardar los terminos con el factor comun en el metodo de Lagrange
        FactorComun factorcomun1; // Objeto que encapsulara el factor y el polinomio con los terminos comunes
        Polinomio memoforma;
        Polinomio p;
        Polinomio P; // Polinomio que será el cuadrado de la suma en el metodo de Lagrange
        char nuevavariable; //Variable que representará cada variable del cambio de variables en el metodo de Lagrange.
        List<int> indicesamarillos; // Lista para guardar los indices de inicio y final de los parrafos en color de letra verde amarillento
        List<int> indicespequeña; // Lista para guardar los indices de inicio y final de los parrafos en letra pequeña
       Polinomio formasiguiente; // Polinomio que es la forma en cada iteracion.
                List <Polinomio> formas2; // Lista donde se guardaran las formas de cada iteracion usando las nuevas variables
                Sistema sistema; // Será el sistema de ecuaciones usado en el metodo de lagrange para obtener la matriz modal
                Racional[,] comprobacionracional; // Sera la matriz que se usara para hacer la prueba en la resolucion Lagrange
                Label[,] comprobacion; // Para mostrar la matriz que se usara para hacer la prueba en la resolucion Lagrange

                Racional[,] copia; // Matriz copia de la original para no alterar la original en la resolucion por el metodo de Gauss
                List<Racional> cocientes; // Para guardar los cocientes en el metodo de Gauss
        bool diagonalizada; // Para registrar cuando la matriz está diagonalizada en el metodo de Gauss
        Timer timer4; // Para realizar la operacion de reduccion de un elemento por debajo de la diagonal principal en cero cada vez
        Timer timer1; // Igual que el anterior pero actua sobre la matriz unidad
        Timer timer2; // Para realizar las restas sobre la matriz unidad
        Timer timer3; // Para realizar las restas de las columnas sobre la matriz diagonal
        bool cocientecero; // Para registrar cuando el cociente sea cero.
        Racional[,] matrizmemo; // Para guardar el estado anterior de la matriz transformada
        int filaactualunidad; // Para contar la fila sobre la que se está actuando de la matriz unidad en el metodo de Gauss.
        bool despejada; // Para registrar cuando se ha despejado el sistema en terminos de a,b,c etc en el metodo de Gauss.
        List <string> resultados;
	 bool coeficientenegativo = false; // Para registrar cuando el termino elegido en el metodo de Lagrange, tiene coeficiente negativo
	


        ///<Summary>
        ///
        /// CONSTRUCTOR
        /// 
        ///</Summary>
        ///
        public Cuadraticas(bool tiporesolucion)
        {
            directa = tiporesolucion;
        }

        ///<Summary>
        ///
        ///VALORES INICIALES
        /// 
        ///</Summary>
        ///
        public override void Cargar(object sender, EventArgs e)
        {
	    this.lbExplicacion.Anchor = AnchorStyles.Top|AnchorStyles.Left;
	    this.btContinuar.Anchor = AnchorStyles.Top|AnchorStyles.Left;
            this.Text = "Formas cuadráticas.";
	    this.MinimumSize = new Size(1300,800);
            tbFilas.KeyPress += tbFilas_KeyPress;
            btContinuar.Click += btContinuar_Click;
            if (!directa) // Resoluciones paso a paso
            {
                timer1 = new Timer();
                timer1.Tick += Timer1_Tick;
                timer1.Interval = 750;
                timer2 = new Timer();
                timer2.Tick += Timer2_Tick;
                timer2.Interval = 750;
                timer3 = new Timer();
                timer3.Tick += Timer3_Tick;
                timer3.Interval = 750;
                timer4 = new Timer();
                timer4.Tick += Timer4_Tick;
                timer4.Interval = 750;
                btDefecto.Hide();
                radioButton1.Show();
                radioButton1.Location = new Point(200, 150);
                radioButton1.Font = new Font("Dejavu Sans", 14);
                radioButton1.Text = " Definición de forma cuadrática.";
                radioButton1.Click += RadioButton1_Click;
                radioButton1.Checked = false;
                radioButton2.Show();
                radioButton2.Location = new Point(radioButton1.Location.X + 50, radioButton1.Location.Y + 100);
                radioButton2.Font = radioButton1.Font;
                radioButton2.Text = " Diagonalización por el método de la matriz ortogonal.";
                radioButton2.Click += RadioButton2_Click;
                radioButton3.Show();
                radioButton3.Location = new Point(radioButton2.Location.X + 50, radioButton2.Location.Y + 100);
                radioButton3.Font = radioButton1.Font;
                radioButton3.Text = " Diagonalización por el método de Lagrange.";
                radioButton3.Click += RadioButton3_Click;
                radioButton4.Show();
                radioButton4.Location = new Point(radioButton3.Location.X + 50, radioButton3.Location.Y + 100);
                radioButton4.Font = radioButton1.Font;
                radioButton4.Text = " Diagonalización por el método de Gauss.";
                radioButton4.Click += RadioButton4_Click;
            }
            else if( directa) // Resolucion directa;
            {
                lbExplicacion.Show();
                ResolucionGauss();
                btDefecto.Hide();
            }
                
        }

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            lbExplicacion.ForeColor = Color.Black;
            lbExplicacion.Text = "";
            this.AcceptButton = null;
            avance = 0;
            foreach (Control c in Controls)
                if (c is TextBox)
                    c.Hide();
                else if (c is Label & c.Name != "lbExplicacion")
                    c.Hide();
            btContinuar.Hide();
            rtbExplicaciones.ResetText();
            rtbExplicaciones.Hide();
            tbFilas.ResetText();
            if (radioButton1.Checked)
                RadioButton1_Click(new object(), new EventArgs());
            else if (radioButton2.Checked)
                RadioButton2_Click(new object(), new EventArgs()); // Metodo de la matriz ortogonal
            else if (radioButton3.Checked)
                RadioButton3_Click(new object(), new EventArgs());// Metodo de Lagrange
            else
            {
                diagonalizada = false;
                despejada = false;
                contador = 0;
                contador2 = 0;
                RadioButton4_Click(new object(), new EventArgs());//Metodo de Gauss
            }
        }


        /// <summary>
        ///  
        /// CIERRA EL FORMULARIO ACTUAL Y ABRE UN NUEVO MENU DE ALGEBRA LINEAL
        /// 
        /// </summary>

        public override void btSalir_Click(object sender, EventArgs e)
        {
            if (submenu) // Si se está en el submenú de Formas Cuadráticas
            {
                MenuMatrices menumatrices = new MenuMatrices();
                menumatrices.Show();
                this.Dispose();
            }
            else
            {
                MenuMatrices menumatrices = new MenuMatrices();
                menumatrices.Show();
                menumatrices.btCuadraticas.PerformClick();
                if (directa)
                    menumatrices.directa.Checked = true;
                else if (!directa)
                    menumatrices.pasoapaso.Checked = true;
                menumatrices.aceptar.PerformClick();
                this.Dispose();
            }
        }

        /// <Summary>
        /// 
        /// INICIA LA DEFINICION DE UNA FORMA CUADRATICA
        /// 
        /// </Summary>
        /// 
        private void RadioButton1_Click(object sender, EventArgs e)
        {
	    this.MinimumSize=new Size(1200,700);
            submenu = false;
            contador = 0;
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            radioButton4.Hide();
            lbExplicacion.Show();
	    lbExplicacion.Text = "Una matriz simetrica, es una matriz igual a su traspuesta ( Fila n =  Columna n ). Como por ejemplo la matriz:";
            // Construccion de la matriz de etiquetas para la matriz simetrica
            orden = 3;
            Point origen = new Point(50, 200);
            simetrica = new Label[orden, orden];
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    simetrica[i, j] = new Label();
                    simetrica[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    simetrica[i, j].Font = new System.Drawing.Font("Dejavu-Sans", 10);
                    simetrica[i, j].Size = new System.Drawing.Size(70, 20);
                    simetrica[i, j].Location = new Point(origen.X + (contador * 90), origen.Y);
                    simetrica[i, j].BackColor = Color.White;
                    simetrica[i, j].Text = simetricaejemplo[i, j].ToString();
                    Controls.Add(simetrica[i, j]);
                    simetrica[i, j].Show();
                    contador++;
                }
                contador = 0;
                origen.Y += 50;
            }
            btContinuar.Show();
            btContinuar.Location = new Point(simetrica[2, 0].Location.X+50, simetrica[2, 0].Location.Y + 100);
            btContinuar.Click += DefinicionCuadratica;
        }


        ///<summary>
        ///
        /// CONTINUA LA DEFINICION DE UNA FORMA CUADRATICA
        /// 
        ///</summary>
        ///
        private void DefinicionCuadratica(object sender, EventArgs e)
        {
            if (avance == 0) // Primer paso de la explicacion
            {
                lbExplicacion.Text = "Como los vectores de las filas y las columnas homologas son iguales, el coeficiente del elemento [1,2] es igual al del elemento [2,1], el del [1,3] igual al del [3,1] y el del [2,3] igual al del [3,2].\n\nEs decir, todo elemento fuera de la diagonal principal tiene un elemento equivalente.";
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        if ((i == 0 && j == 1) || (i == 1 && j == 0))
                        {
                            Controls.Remove(simetrica[i, j]);
                            simetrica[i, j].Text = simetricaejemplo[i, j].ToString();
                            simetrica[i, j].BackColor = Color.GreenYellow;
                            Controls.Add(simetrica[i, j]);
                        }
                        else if ((i == 0 && j == 2) || (i == 2 && j == 0))
                        {
                            Controls.Remove(simetrica[i, j]);
                            simetrica[i, j].Text = simetricaejemplo[i, j].ToString();
                            simetrica[i, j].BackColor = Color.Coral;
                            Controls.Add(simetrica[i, j]);
                        }
                        else if ((i == 1 && j == 2) || (i == 2 && j == 1))
                        {
                            Controls.Remove(simetrica[i, j]);
                            simetrica[i, j].Text = simetricaejemplo[i, j].ToString();
                            simetrica[i, j].BackColor = Color.Aquamarine;
                            Controls.Add(simetrica[i, j]);
                        }
                        else
                        {
                            Controls.Remove(simetrica[i, j]);
                            simetrica[i, j].Text = simetricaejemplo[i, j].ToString();
                            Controls.Add(simetrica[i, j]);
                        }
                    }
                }
                avance++;
            }
            else if (avance == 1) // Segundo paso de la explicacion
            {
                lbExplicacion.Text = "Si añadimos las variables ' Xn , Yn ' como indices de la fila y columna de cada elemento de la matriz:";
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        simetrica[i, j].Text = simetrica[i, j].Text = simetricaejemplo[i, j].ToString() + "X" + (i + 1).ToString() + " Y" + (j + 1).ToString();
                    }
                }
                avance++;
            }
            else if (avance == 2) //Tercer paso de la explicacion
            {
                lbExplicacion.Text = "Al ser una matriz simetrica, Xn = Yn, por lo tanto podemos cambiar la variable Y por la variable X.\nPodemos observar como el indice de la variable X, se repite en los elementos de la diagonal principal. ";
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        simetrica[i, j].Text = simetricaejemplo[i, j].ToString() + "X" + (i + 1).ToString() + " X" + (j + 1).ToString();
                    }
                }
                avance++;
            }
            else if (avance == 3)
            {
                lbExplicacion.Text = "La matriz en forma polinómica, se forma de la siguiente manera:\n-Los elementos cuadraticos (elevados al cuadrado ) del polinomio son los elementos de la diagonal principal \n  con su variable al cuadrado.\n- Los elementos fuera de la diagonal principal, son los terminos rectangulares ( elevados a la unidad)\n   con dos variables. Como X1X2 es igual a X2X1, X1X3 igual a X3X1 etc, son terminos semejantes que se pueden sumar.";
                Point origen = new Point(100, 600);
                label1.Font = new Font("Dejavu-Sans", 16);
                label1.Text = simetricaejemplo[0, 0].ToString() + "X1² ";
                label1.ForeColor = Color.White;
                label1.Size = new Size(65, 30);
                label1.BackColor = Color.Transparent;
                label1.Show();
                label1.Location = origen;
                origen.X += 60;
                label2.Font = new Font("Dejavu-Sans", 16);
                label2.Text = simetricaejemplo[1, 1].ToString() + "X2² ";
                label2.ForeColor = Color.White;
                label2.Size = new Size(65, 30);
                label2.BackColor = Color.Transparent;
                label2.Show();
                label2.Location = origen;
                origen.X += 60;
                label3.Font = new Font("Dejavu-Sans", 16);
                label3.Text = simetricaejemplo[1, 1].ToString() + "X3² ";
                label3.ForeColor = Color.White;
                label3.Size = new Size(65, 30);
                label3.Show();
                label3.BackColor = Color.Transparent;
                label3.Location = origen;
                origen.X += 60;
                label4.Font = new Font("Dejavu-Sans", 16);
                label4.Text = "+" + (simetricaejemplo[0, 1] * 2).ToString() + "X1X2";
                label4.ForeColor = Color.GreenYellow;
                label4.Size = new Size(100, 30);
                label4.BackColor = Color.Transparent;
                label4.Show();
                label4.Location = origen;
                origen.X += 95;
                label5.Font = new Font("Dejavu-Sans", 16);
                label5.Text = "+" + (simetricaejemplo[0, 2] * 2).ToString() + "X1X3";
                label5.ForeColor = Color.Coral;
                label5.Size = new Size(100, 30);
                label5.BackColor = Color.Transparent;
                label5.Show();
                label5.Location = origen;
                origen.X += 95;
                label6.Font = new Font("Dejavu-Sans", 16);
                label6.Text = "+" + (simetricaejemplo[1, 2] * 2).ToString() + "X2X3";
                label6.ForeColor = Color.Aquamarine;
                label6.Size = new Size(120, 30);
                label6.BackColor = Color.Transparent;
                label6.Show();
                label6.Location = origen;
                btContinuar.Hide();
            }
         
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUZCA UN NUMERO ENTERO DISTINTO DE CERO
        /// 
        /// </summary>


        private void tbFilas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox aux = (TextBox)sender;
            if (e.KeyChar == Convert.ToChar(13)) // Si se pulsa la tecla intro
            {
                if (aux.Text.Length > 0)
                {
                    e.Handled = true;
                    orden = Int32.Parse(tbFilas.Text);
                    ConstruirMatriz();
                }
                else
                {
                    e.Handled = true;
                    aux.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(8)) // Si se pulsa la tecla BackSpace
                e.Handled = false;

            else if (e.KeyChar < '0' || e.KeyChar > '9') // Asegurar que se introduzcan digitos y no otro tipo de caracteres
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// CONSTRUYE LA MATRIZ SIMETRICA CON LAS FILAS Y COLUMNAS INTRODUCIDAS
        /// 
        ///</summary>
        ///
        private void ConstruirMatriz()
        {
            EtiquetaFilas.Hide();
            tbFilas.Hide();
            btDefecto.Hide();
            simetric = new TextBox[orden, orden];
            Point origen = new Point(0,0);
            if(radioButton3.Checked)
             origen = new Point(100, 100);
              if (radioButton1.Checked || radioButton2.Checked)
                  origen = new Point(100, 200);
              else if (radioButton4.Checked)
                  origen = new Point(100, 150);
              else
                  origen = new Point(100, 150);
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    simetric[i, j] = new TextBox();
                    simetric[i, j].TabIndex = (0 + contador2);
                    simetric[i, j].Location = new Point(origen.X + 60 * contador, origen.Y);
                    simetric[i, j].Size = new Size(50, 20);
                    simetric[i, j].Font = new Font("Dejavu-Sans", 10);
                    simetric[i, j].KeyPress += new KeyPressEventHandler(Matriz_KeyPress);
                    simetric[i, j].BackColor = Color.White;
                    Controls.Add(simetric[i, j]);
                    simetric[i, j].Show();
                    contador++;
                    contador2++;
                }
                origen.Y += 40;
                contador = 0;
            }

            if(radioButton3.Checked)
            origen = new Point(100, 100);
            else if (radioButton1.Checked || radioButton2.Checked)
                origen = new Point(100, 200);
          //  else if (radioButton4.Checked)
            //    origen = new Point(100, 150);

            if(radioButton2.Checked)
            lbExplicacion.Text = "Introduzca la matriz simétrica (Solo enteros).";
            else
                lbExplicacion.Text = "Introduzca la matriz simétrica (Enteros o racionales).";
            label1.Hide();
            simetric[0, 0].Focus();
        }

        /// <summary>
        ///  
        ///  COMPRUEBA QUE LOS DATOS INTRODUCIDO EN CADA TextBox EN matriz SEAN
        ///  ADECUADOS PARA PASARLOS A Racional MAS TARDE
        ///  Y CREA LA MATRIZ EN FORMATO Racional Y LA RELLENA CON DATOS DE matriz
        ///  PASADOS A Racional
        /// </summary>

        private void Matriz_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Restablecer los indices de cada control TextBox de la matriz para asegurar de que estan seguidos
            int cont = 0;
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    simetric[i, j].TabIndex = cont;
                    cont++;
                }
            }
            bool signohuerfano = false;
            matrizracional = new Racional[orden, orden];
            TextBox aux = (TextBox)sender;
            if (e.KeyChar == Convert.ToChar(13)) // Si se pulsa la tecla intro
            {
                if (aux.Text.Length == 1)
                {
                    if (aux.Text[0] == '+' || aux.Text[0] == '-' && aux.Text.Length == 1)
                    {
                        signohuerfano = true;
                    }
                }
                if (aux.Text.Length > 0 && !signohuerfano) // Si la caja de texto no está vacia o no tiene solo un signo.
                {
                    filaactual = (aux.TabIndex) / simetric.GetLength(0); // primer indice de la caja actual
                    columnaactual = (aux.TabIndex) - (simetric.GetLength(0) * filaactual); // segundo indice de la caja actual
                    if (columnaactual == simetric.GetLength(1) - 1) // Si es la ultima caja de la fila
                    {
                        if (aux.TabIndex < ((simetric.GetLength(0)) * (simetric.GetLength(0)))) // Si no es la ultima caja de la matriz.
                        {
                            columnaactual = 0;
                            filaactual++;
                        }
                        else // Si es la ultima caja de la matriz rellenar la matriz en formato Racional
                        {
                            e.Handled = true;
                            for (int i = 0; i < simetric.GetLength(0); i++)
                            {
                                for (int j = 0; j < simetric.GetLength(0); j++)
                                {
                                    matrizracional[i, j] = Racional.StringToRacional(simetric[i, j].Text);
                                }
                            }
                            // SIGUIENTE METODO
                            if (!directa)
                            {
                                lbExplicacion.Focus();
                                avance++;
                                if (radioButton2.Checked)
                                {
                                    ResolucionOrtogonal();
                                }
                                else if (radioButton3.Checked)
                                    ResolucionLagrange();
                                else if (radioButton4.Checked)
                                    ResolucionGauss();
                            }
                            else
                            {
                                avance = 1;
                                ResolucionGauss();
                            }
                        }
                    }
                    else // Si no es la ultima caja de la fila
                    {
                        columnaactual++;
                    }
                    try
                    {
                        e.Handled = true;
                        simetric[filaactual, columnaactual].Focus();
                    }
                    catch (IndexOutOfRangeException) // Rellenar la matriz Racional 
                    {
                        e.Handled = true;
                        for (int i = 0; i < simetric.GetLength(0); i++)
                        {
                            for (int j = 0; j < simetric.GetLength(1); j++)
                            {
                                matrizracional[i, j] = Racional.StringToRacional(simetric[i, j].Text);
                            }
                        }
                        // SIGUIENTE METODO
                        if (!directa)
                        {
                            lbExplicacion.Focus();
                            avance++;
                            if (radioButton2.Checked)
                            {
                                ResolucionOrtogonal();
                            }
                            else if (radioButton3.Checked)
                                ResolucionLagrange();
                            else if (radioButton4.Checked)
                                ResolucionGauss();
                        }
                        else
                        {
                            avance = 1;
                            ResolucionGauss();
                        }
                    }
                }
                else // Si la caja está vacia
                {
                    aux.Focus();
                }

            }

            else if (e.KeyChar == Convert.ToChar(8)) // Si se pulsa la tecla BackSpace
                e.Handled = false;

            else if (e.KeyChar == '+' || e.KeyChar == '-') // Asegurar que los signos + o - esten en la primera posicion.
            {
                if (aux.SelectionLength == aux.Text.Length) // Si todo el texto de la caja está seleccionado, es decir estamos en el primer caracter
                    e.Handled = false;
                else
                    e.Handled = true;
            }

            else if (( radioButton3.Checked || radioButton4.Checked) && e.KeyChar == '/') // Solo puede haber un caracter 
            {
                if (aux.Text.Length > 1 && aux.Text.IndexOf('/') != -1)
                    e.Handled = true;
                else
                {
                    if (aux.Text.Length > 0)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }

            }

            else if (e.KeyChar < '0' || e.KeyChar > '9') // Asegurar que se introduzcan digitos y no otro tipo de caracteres
                e.Handled = true;

        }

        /// <summary>
        /// 
        /// CONSTRUYE LA MATRIZ SIMETRICA POR DEFECTO
        /// 
        /// </summary>
        /// 
        public override void btDefecto_Click(object sender, EventArgs e)
        {
            orden = 3;
                contador = 0;
                contador2 = 0;
            ConstruirMatriz();
            matrizracional = new Racional[3, 3];
            btDefecto.Hide();
            simetric[0, 0].Text = "5"; matrizracional[0, 0] = 5; simetric[0, 1].Text = "2"; matrizracional[0, 1] = 2; simetric[0, 2].Text = "0"; matrizracional[0, 2] = 0;
            simetric[1, 0].Text = "2"; matrizracional[1, 0] = 2; simetric[1, 1].Text = "4"; matrizracional[1, 1] = 4; simetric[1, 2].Text = "2"; matrizracional[1, 2] = 2;
            simetric[2, 0].Text = "0"; matrizracional[2, 0] = 0; simetric[2, 1].Text = "2"; matrizracional[2, 1] = 2; simetric[2, 2].Text = "3"; matrizracional[2, 2] = 3;
            avance++;
            if (radioButton2.Checked)
                ResolucionOrtogonal();
            else if (radioButton3.Checked)
                ResolucionLagrange();
            else if (radioButton4.Checked)
                ResolucionGauss();
        }

        
        /// <summary>
        /// 
        /// LLAMA AL METODO DE DIAGONALIZACION DE FORMAS CUADRATICAS POR EL METODO DE LA MATRIZ
        /// ORTOGONAL
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void RadioButton2_Click(object sender, EventArgs e)
        {
            submenu = false;
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            radioButton4.Hide();
            lbExplicacion.Show();
            ResolucionOrtogonal();
        }

        /// <summary>
        /// 
        /// LLAMA AL METODO DE DIAGONALIZACION DE FORMAS CUADRATICAS POR EL METODO DE LAGRANGE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void RadioButton3_Click(object sender, EventArgs e)
        {
            submenu = false;
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            radioButton4.Hide();
            lbExplicacion.Show();
            this.AcceptButton = null;
            ResolucionLagrange();
        }

        /// <summary>
        /// 
        /// LLAMA AL METODO DE DIAGONALIZACION DE FORMAS CUADRATICAS POR EL METODO DE Gauss
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void RadioButton4_Click(object sender, EventArgs e)
        {
            submenu = false;
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            radioButton4.Hide();
            lbExplicacion.Show();
            this.AcceptButton = null;
            rtbExplicaciones.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbExplicaciones.MaximumSize = new Size(1000, 230);
            ResolucionGauss();
        }
        /// <summary>
        ///  
        /// AVANZA EN LA DIAGONALIZACION DE LA MATRIZ SIMETRICA SEGUN DEL METODO
        /// 
        ///</summary>
        ///
        private void btContinuar_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked) // Metodo de la matriz ortogonal
            {
                avance++;
                ResolucionOrtogonal();
            }
            else if (radioButton3.Checked)
            {
                ResolucionLagrange();
            }
            else if (radioButton4.Checked)
            {
                ResolucionGauss();
            }
        }
        
         /// <summary>
        ///  
        /// REALIZA LA DIAGONALIZACION DE UNA FORMA CUADRATICA POR EL METODO DE LA MATRIZ
        ///  ORTOGONAL PASO A PASO 
        /// 
        /// </summary>

        private void ResolucionOrtogonal()
        {
            contador = 0;
            contador2 = 0;
            lbExplicacion.Focus();
            if (avance == 0) //Obtener el orden de la matriz simetrica y construir la matriz de cajas de texto correspondiente
            {
                EtiquetaFilas.AutoSize = true;
                EtiquetaFilas.Text = "Cantidad de filas y columnas de la matriz simétrica.";
                EtiquetaFilas.Show();
                lbExplicacion.Text = "Introducir enteros.\n\nBotón [E] para ejemplo con valores por defecto";
                tbFilas.Show();
                tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Width + 10, EtiquetaFilas.Location.Y);
                btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 10, tbFilas.Location.Y);
                btDefecto.Show();
                tbFilas.Focus();
                EtiquetaFilas.Location = new Point(EtiquetaFilas.Location.X, lbExplicacion.Height + 5);
                tbFilas.Location = new Point(tbFilas.Location.X + 10, EtiquetaFilas.Location.Y);
                btDefecto.Location = new Point(btDefecto.Location.X, tbFilas.Location.Y);
                EtiquetaColumnas.Location = new Point(EtiquetaColumnas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 5);
                tbcolumnas.Location = new Point(tbFilas.Location.X, EtiquetaColumnas.Location.Y);
            }
            else if (avance == 1) // Comprobar si la matriz introducida es simetrica y si lo es mostrar la forma polinomica de la misma.
            {
                contador = 0;
                contador2 = 0;
                bool essimetrica = Matematicas.AlgebraLineal.EsSimetrica(matrizracional);
                if (!essimetrica)
                {
                    MessageBox.Show("La matriz introducida no es simétrica.Introduzcala de nuevo.");
                    foreach (TextBox t in simetric)
                        Controls.Remove(t);
                    avance = 0;
                    ConstruirMatriz();
                }
                else
                {
                    btContinuar.Show();
                    this.AcceptButton = btContinuar;
                    btContinuar.Location = new Point(simetric[orden - 1, 0].Location.X, simetric[orden - 1, 0].Location.Y + 50);
                    this.AcceptButton = btContinuar;
                    rotulosh = new List<Label>();
                    for (int i = 0; i < orden; i++)
                    {
                        rotulosh.Add(new Label());
                        Controls.Add(rotulosh[i]);
                        rotulosh[i].Show();
                        rotulosh[i].AutoSize = true;
                        rotulosh[i].BackColor = Color.Transparent;
                        rotulosh[i].Location = new Point(simetric[0, contador].Location.X, (simetric[0, contador].Location.Y - 20));
                        rotulosh[i].Font = new System.Drawing.Font("Dejavu-Sans", 12);
                        rotulosh[i].Text += (char)((int)'a' + contador);
                        contador++;
                    }
                    contador = 0;
                    rotulosv = new List<Label>();
                    for (int i = 0; i < orden; i++)
                    {
                        rotulosv.Add(new Label());
                        Controls.Add(rotulosv[i]);
                        rotulosv[i].Show();
                        rotulosv[i].AutoSize = true;
                        rotulosv[i].BackColor = Color.Transparent;
                        rotulosv[i].Location = new Point(simetric[0, 0].Location.X - 20, (simetric[contador, i].Location.Y));
                        rotulosv[i].Font = new System.Drawing.Font("Dejavu-Sans", 12);
                        rotulosv[i].Text += (char)((int)'a' + contador);
                        contador++;
                    }
                    lbExplicacion.Text = "La forma polinomica de la matriz simetrica introducida es: ";
                    label1.BackColor = Color.SeaGreen;
                    label1.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + 30);
                    label1.Show();
                    label1.AutoSize = true;
                    Polinomio formainicial = Matematicas.AlgebraLineal.SimetricaAForma(matrizracional);
                    formainicial.EliminarCeros();
                    formainicial.Ordenar();
                    label1.Text = "Matriz simetrica en forma polinomica: " + formainicial.ToString();
                }
            }
            else if (avance == 2) // Diagonalizar la matriz si es posible, de lo contrario finalizar.
            {
                lbExplicacion.Text = " Diagonalizar una forma cuadrática, es transformarla en otra forma equivalente en la que no hayan términos rectangulares, es decir, una forma en la que todos los elementos estén elevados al cuadrado.\n\nPara empezar, diagonalizamos la matriz simétrica obteniendo una matriz diagonal y una modal.";
               matrizlong = new long[orden,orden];
               for (int i = 0; i < orden; i++)
               {
                   for (int j = 0; j < orden; j++)
                   {
                       matrizlong[i, j] = matrizracional[i, j].Numerador;
                   }
               }
               matrizdiagonal = new Racional[orden, orden];
               try
               {
                   Racional[,] matrizracio = Matematicas.AlgebraLineal.MatrizLongARacional(matrizlong);
                   Racional[,] modalracional =Matematicas.AlgebraLineal.Diagonalizar(matrizracio, ref matrizdiagonal);
                   matrizmodal = new long[orden,orden];
                   for(int i = 0; i < orden ; i++)
                      for(int j = 0; j < orden ; j++)
                          matrizmodal[i,j] = (long)modalracional[i,j].ToDouble();
               }
               catch (NullReferenceException)
               {
                   matrizmodal = null;
               }
                RacionalRadical[,] modalalternativa = new RacionalRadical[matrizlong.GetLength(0), matrizlong.GetLength(0)];

                if (matrizmodal == null)
                {
                    lbExplicacion.ForeColor = Color.Red;
                    lbExplicacion.Text = " Error, el programa no puede continuar la resolución por este metodo.Probablemente sea posible usando otro método del menu anterior.";
                    btContinuar.Hide();
                    this.AcceptButton = null;
                }
                else
                {
                    contador = 0;
                    diagonal = new Label[orden, orden];
                    Point origen = new Point(simetric[0, orden - 1].Location.X + 80, simetric[0, 0].Location.Y);
                    label2.AutoSize = true;
                    label2.BackColor = Color.SeaGreen;
                    label2.Location = new Point(origen.X + 10, origen.Y - 30);
                    label2.Font = new System.Drawing.Font("Dejavu-Sans", 10, FontStyle.Underline);
                    label2.Text = "Matriz diagonal";
                    label2.Show();
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            diagonal[i, j] = new Label();
                            diagonal[i, j].Text = matrizdiagonal[i, j].ToString();
                            diagonal[i, j].Location = new Point(origen.X + 60 * contador, origen.Y);
                            diagonal[i, j].Size = new Size(50, 20);
                            diagonal[i, j].Font = new Font("Dejavu-Sans", 10);
                            diagonal[i, j].BackColor = Color.GreenYellow;
                            Controls.Add(diagonal[i, j]);
                            diagonal[i, j].Show();
                            contador++;
                        }
                        origen.Y += 40;
                        contador = 0;
                    }
                    contador = 0;
                    modal = new Label[orden, orden];
                    origen = new Point(diagonal[0, orden - 1].Location.X + 80, diagonal[0, 0].Location.Y);
                    label3.AutoSize = true;
                    label3.BackColor = Color.SeaGreen;
                    label3.Location = new Point(origen.X + 10, origen.Y - 30);
                    label3.Font = new System.Drawing.Font("Dejavu-Sans", 10, FontStyle.Underline);
                    label3.Text = "Matriz modal";
                    label3.Show();
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            modal[i, j] = new Label();
                            modal[i, j].Text = matrizmodal[i, j].ToString();
                            modal[i, j].Location = new Point(origen.X + 60 * contador, origen.Y);
                            modal[i, j].Size = new Size(50, 20);
                            modal[i, j].Font = new Font("Dejavu-Sans", 10);
                            modal[i, j].BackColor = Color.Green;
                            Controls.Add(modal[i, j]);
                            modal[i, j].Show();
                            contador++;
                        }
                        origen.Y += 40;
                        contador = 0;
                    }
                   
                }
            }
            else if (avance == 3)//Comprobar que la diagonal este compuesta por enteros y si es así construye la forma equivalente
            {
                lbExplicacion.Text = " Una vez obtenidas las matrices diagonal y modal, construímos la forma equivalente sin términos rectangulares usando cada uno de los elementos de la matriz diagonal como coeficiente de cada termino de la forma, y variables distintas a las anteriores.";
                bool diagonalentera = true;
                    foreach (Racional r in matrizdiagonal)
                        try
                        {
                            if (!r.EsEntero())
                                diagonalentera = false;
                        }
                        catch (NullReferenceException)
                        {
                            diagonalentera = false;
                        }
                
                if (!diagonalentera)
                {
                    lbExplicacion.ForeColor = Color.Red;
                    lbExplicacion.Text = " Error, el programa no puede continuar la resolución por este metodo.Probablemente sea posible usando otro método del menu anterior.";
                    btContinuar.Hide();
                    avance = 20;
                    return;
                }
                 
                    long[,] diagonalong = new long[orden, orden];
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            diagonalong[i, j] = matrizdiagonal[i, j].Numerador;
                        }
                    }
                    forma = Matematicas.AlgebraLineal.DiagonalAForma(diagonalong);
                    Polinomio formasinceros = new Polinomio(forma);
                    formasinceros.EliminarCeros();
                    label1.Text += "\nForma equivalente: " + formasinceros.ToString();
                
            }
            else if (avance == 4)//Construye la lista de polinomios correspondientes a las columnas de la matriz modal
            {
                lbExplicacion.Text = " Despues, usando la matriz modal, construímos un polinomio por cada columna de la matriz. Los elementos de cada columna, seran los coeficientes de  cada uno de los términos en los polinomios.Usamos las mismas variables que en la forma polinómica de la matriz.;";
                vectores = new List<Polinomio>();
            
                if (matrizmodal != null)
                {
                    vectores = Matematicas.AlgebraLineal.MatrizAVectores(matrizmodal);
                    for (int i = 0; i < vectores.Count; i++)
                    {
                        vectores[i].EliminarCeros();
                    }
                }
                else
                    vectores = new List<Polinomio>();

                    foreach (Polinomio p in vectores)
                    {
                        lbExplicacion.Text += "\n " + p.ToString();
                    }
            }
            else if (avance == 5) // Normalizar los vectores extraidos de la matriz modal y mostrarlos
            {
                tipos = new List<string>();
                resultados = new List<string>();
                vectoresnormalizados = Matematicas.AlgebraLineal.NormalizarVectores(vectores, ref tipos);
   
                    lbExplicacion.Text = " Ahora, tenemos que normalizar cada uno de los polinomios anteriores.\nNormalizar un vector, es dividir cada uno de sus componentes por la magnitud del vector. La magnitud de un vector, es la raiz cuadrada de la suma de los cuadrados de sus componentes.";
                    lbExplicacion.Text += "\n";
                    for (int i = 0; i < vectoresnormalizados.Count; i++)
                    {
                        string lectura ="";
                        for (int j = 0; j < vectoresnormalizados[i].Count; j++)
                        {
                            if (tipos[i] == "Termino")
                            {
                                Termino aux = (Termino)vectoresnormalizados[i][j];
                                lbExplicacion.Text += aux.ToString();
                                lectura += aux.ToString();
                            }
                            else if (tipos[i] == "TerminoRadical")
                            {
                                TerminoRadical aux = (TerminoRadical)vectoresnormalizados[i][j];
                                if (aux.Coeficiente.NumeradorEntero > 0)
                                    lbExplicacion.Text += "+";
                                lbExplicacion.Text += aux.Coeficiente.ToString() + aux.Variables[0].ToString();
                           lectura += aux.Coeficiente.ToString() + aux.Variables[0].ToString();
                            }
                        }
                        lbExplicacion.Text += "\n";
			
                       // resultados.Add(lectura);
                    }
		// Añadir los resultados en la etiqueta de las explicaciones a la lista
		   string auxx = lbExplicacion.Text;
		   string [] splitting = auxx.Split();
		   resultados.Clear(); 
		   foreach(string s in splitting)
		    if(s.Length > 0 && (s[0] == '+' || s[0] == '-') )
		    {
			resultados.Add(s);
		       // MessageBox.Show(s);
		    }
		   
            }
            else if (avance == 6) {
                lbExplicacion.Text += "\nCada vector normalizado, es una columna de la matriz modal.";
				contador = 0;
				List <char> variables = new List<char> (); // Para contener las variables en los polinomios
				for (int i = 0; i < orden; i++)
					variables.Add ((char)((int)'a' + i));

		// Obtener cada uno de los termino de cada vector y añadirlo a la matriz modal
		   int columna = 0;
                foreach (string s in resultados)
                {
                    string vector = s;

		  while(vector.Length > 0)
		  {
		    //Obtener el indice del ultimo operando de suma , resta o raiz
			
		    int indicemas = vector.LastIndexOf('+');
		    int indicemenos = vector.LastIndexOf('-');
		    int indice = -1;
		    if(indicemas > 0 || indicemenos > 0)
		    {
			if(indicemas > indicemenos)
			 indice = indicemas;
			else
			 indice = indicemenos;
		    }
		     //else if( vector.Contains((char)8730) )
		      //indice = vector.LastIndexOf( (char)8730 );
		  //  MessageBox.Show("Linea 1021: " + indice.ToString());
		   // Obtener el ultimo termino
		     string termino = "";
		     if(indice > 0)
		     {
			termino = vector.Substring(indice);
			vector = vector.Substring(0,indice);
		     }
		     else
		     {
			termino = vector;
			vector = "";
		     }
		    // MessageBox.Show("Linea 1034   Termino: " + termino);
		     // Obtener la variable del termino
			char variable = ' ';
			for(int i = 0; i < termino.Length;i++)
			{
			  char aux = termino[i];
			  if( aux != '+' && aux != '-' && aux != '/' && !char.IsDigit(aux) && aux != (int)8730 )
			  {
				variable = aux;
				break;
			  }
			}
		     // Obtener la fila y columna donde se situara el termino en la matriz modal
			int fila = (int)variable - (int)'a';
		     //Eliminar la variable antes de poner el termino en la matriz modal
			int contadore = 0;
			foreach(char c in termino)
			{
			  if(char.IsLetter(c))
			  termino = termino.Substring(0,contadore)+termino.Substring(contadore+1);
			  contadore++;
			}
			modal[fila,columna].Text = termino;
		    // MessageBox.Show("Fila 1032         Vector:  " + vector + "  Termino:  " + termino + "  Variable: " + ""+variable);
		  }
		  columna++;		
		}
		
            }
            else if (avance == 7) 
            {
                lbExplicacion.Text = "Al ser la matriz modal una matriz ortogonal ( las columnas son vectores normalizados ), su inversa es igual a su traspuesta.";
                Label[,] modalinversa = new Label[orden, orden];
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        modalinversa[i, j] = new Label();
                        Controls.Add(modalinversa[i, j]);
                        modalinversa[i, j].Size = modal[i, j].Size;
                        modalinversa[i, j].Font = modal[i, j].Font;
                        modalinversa[i, j].Text = modal[j, i].Text;
                        modalinversa[i, j].Location = new Point(modal[i, j].Location.X + (modal[i, j].Width * orden) + (15 * orden), modal[i, j].Location.Y);
                        modalinversa[i, j].Visible = true;
                        modalinversa[i, j].BackColor = Color.Aquamarine;
                        modalinversa[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    }
                }
                Label lbrotuloinversa = new Label();
                lbrotuloinversa.Font = label3.Font;
                lbrotuloinversa.Location = new Point(modalinversa[0,0].Location.X + 30, label3.Location.Y);
                lbrotuloinversa.Text = "Matriz modal inversa";
                lbrotuloinversa.AutoSize = true;
                lbrotuloinversa.Visible = true;
                Controls.Add(lbrotuloinversa);
            }
            else if (avance == 8)
            {
                lbExplicacion.Text = "Es fácil comporbar que: A = M * D * M inversa.\n Donde 'A' es la matriz simétrica original, 'M' es la matriz modal, 'D' es la matriz diagonal y 'M inversa ' es la matriz modal traspuesta.\nLa matriz introducida ha sido diagonalizada.";
                btContinuar.Hide();
            }
        }


           /// <summary>
        ///  
        /// REALIZA LA DIAGONALIZACION DE UNA FORMA CUADRATICA POR EL METODO DE LAGRANGE
        /// 
        /// </summary>

        private void ResolucionLagrange()
        {
            contador = 0;
            contador2 = 0;

            if (avance == 0) // Mostrar la caja de texto para el orden de la matriz, y construirla
            {
                EtiquetaFilas.Show();
                EtiquetaFilas.Text = "Cantidad de filas y columnas de la matriz:";
                EtiquetaFilas.AutoSize = true;
                lbExplicacion.Text = "Introducir enteros.\n\nBotón [E] para resolución de ejemplo con valores por defecto.";
                tbFilas.Show();
                tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Width + 5, tbFilas.Location.Y);
                tbFilas.Focus();
                btDefecto.Show();
                btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
                indicesamarillos = new List<int>();
                indicespequeña = new List<int>();
                EtiquetaFilas.Location = new Point(EtiquetaFilas.Location.X, lbExplicacion.Height + 5);
                tbFilas.Location = new Point(tbFilas.Location.X + 10, EtiquetaFilas.Location.Y);
                btDefecto.Location = new Point(btDefecto.Location.X, tbFilas.Location.Y);
                EtiquetaColumnas.Location = new Point(EtiquetaColumnas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 5);
                tbcolumnas.Location = new Point(tbFilas.Location.X, EtiquetaColumnas.Location.Y);
            }
            else if (avance == 1) // Comprobar si la matriz introducida es simetrica y si se puede diagonalizar por este metodo.Si lo es
            // muestra la forma polinomica de la matriz introducida
            {
                /*
                if (simetric[0, 0].Location.X != 600 && simetric[0,0].Location.Y != 150)
                {
                    MessageBox.Show("ok");
                    ConstruirMatriz();
                }
                 */
                contador = 0;
                contador2 = 0;
                bool essimetrica = Matematicas.AlgebraLineal.EsSimetrica(matrizracional);
                if (!essimetrica)
                {
                    lbExplicacion.Text = "La matriz introducida no es simétrica. Vuelva al paso anterior para intentarlo de nuevo.";
                    ConstruirMatriz();
                }
                else
                {
                    rotulosh = new List<Label>();
                    for (int i = 0; i < orden; i++)
                    {
                        rotulosh.Add(new Label());
                        Controls.Add(rotulosh[i]);
                        rotulosh[i].Show();
                        rotulosh[i].AutoSize = true;
                        rotulosh[i].BackColor = Color.Transparent;
                        rotulosh[i].Location = new Point(simetric[0, contador].Location.X, (simetric[0, contador].Location.Y - 20));
                        rotulosh[i].Font = new System.Drawing.Font("Dejavu-Sans", 12);
                        rotulosh[i].Text += (char)((int)'a' + contador);
                        contador++;
                    }
                    contador = 0;
                    rotulosv = new List<Label>();
                    for (int i = 0; i < orden; i++)
                    {
                        rotulosv.Add(new Label());
                        Controls.Add(rotulosv[i]);
                        rotulosv[i].Show();
                        rotulosv[i].AutoSize = true;
                        rotulosv[i].BackColor = Color.Transparent;
                        rotulosv[i].Location = new Point(simetric[0, 0].Location.X - 20, (simetric[contador, i].Location.Y));
                        rotulosv[i].Font = new System.Drawing.Font("Dejavu-Sans", 12);
                        rotulosv[i].Text += (char)((int)'a' + contador);
                        contador++;
                    }
                    btContinuar.Show();
                    this.AcceptButton = btContinuar;
                    btContinuar.Location = new Point(simetric[orden - 1, orden - 1].Location.X, simetric[orden - 1, orden - 1].Location.Y + 50);
                  
                    rtbExplicaciones.Show();
                    rtbExplicaciones.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + btContinuar.Height + 15);
                    rtbExplicaciones.Size = new Size(1200, this.Size.Height - (orden * 50));
                    rtbExplicaciones.BackColor = Color.SeaGreen;
                    rtbExplicaciones.Font = new Font("Dejavu Sans", 16);
                    rtbExplicaciones.BorderStyle = BorderStyle.None;

                    forma = Matematicas.AlgebraLineal.SimetricaAForma(matrizracional);
                    forma.EliminarCeros();
                    forma.Ordenar();
                    rtbExplicaciones.Text = "Matriz simetrica en forma polinomica:\nC1 = " + forma.ToString();

                    // En caso de que el coeficiente del primer termino cuadratico sea cero, moverlo al final del polinomio
                    // hasta que los terminos dejen de ser cuadraticos. En caso de que todos los coeficientes de todos los 
                    // terminos cuadraticos sean nulos, no se puede continuar.

                    bool coeficientecero = ((forma.ObtenerTermino(0).Coeficiente).Numerador == 0);
                    int cuenta = matrizracional.GetLength(0);
                    while (coeficientecero && cuenta > 0)
                    {
                        Termino aux = new Termino(forma.ObtenerTermino(0));
                        forma.Terminos.Remove(forma.ObtenerTermino(0));
                        forma.AñadirTermino(aux);
                        cuenta--;
                        coeficientecero = ((forma.ObtenerTermino(0).Coeficiente).Numerador == 0);
                    }

                    // comprobar si todos los terminos cuadraticos son nulos
                    coeficientecero = true;
                    foreach (Termino t in forma.Terminos)
                    {
                        if ((t.Coeficiente).Numerador != 0 && (t.Exponentes[0]).Numerador == 2)
                        {
                            coeficientecero = false;
                        }
                    }

                    // si todos los teminos cuadraticos son nulos salir
                    if (coeficientecero)
                    {
                        lbExplicacion.ForeColor = Color.Red;
                        lbExplicacion.Text = " Todos los terminos cuadraticos tienen coeficiente nulo. No se puede diagonalizar la forma por este metodo.";
                        btContinuar.Hide();
                    }
                    lbExplicacion.Text = "Diagonalizar una forma cuadratica, es transformarla en otra forma equivalente en la que no hayan terminos rectangulares, es decir, una forma en la que todos los elementos esten elevados al cuadrado.";
                    lbExplicacion.Text += "\nLa forma polinomica de la matriz simetrica introducida es: ";
                    // Lista de Polinomios que elevados al cuadrado ( cuadrado de una suma ), representaran a la nuevas variables 
                    cambiodevariables = new List<Polinomio>();
                    // Lista de terminos que representan a cada una de las formas cuadraticas en el lado izquierdo de la igualdad
                    cuadraticas = new List<Termino>();
                    // Llista donde se guardaran las formas equivalentes en cada iteracion (polinomio formasiguiente)
                    formas = new List<Polinomio>();
                    // Lista donde se guardaran las formas de cada iteracion usando las nuevas variables
                    formas2 = new List<Polinomio>();
                    formas.Add(new Polinomio(forma));
                    forma1 = new Termino(1, new List<char>() { 'C', (contador + 1).ToString()[0] }, new List<Racional>() { 1, 1 }); //termino que representa a la forma cuadratica
                    forma1.Variables.Reverse(); // para que se vea primero la letra y despues el numero
                }
                avance++;
            }
            else if( cuadraticas.Count < matrizracional.GetLength(0))// Mientras no se hayan extraido todos los polinomios equivalentes a las nuevas variables cuadraticas
            {
                if (avance < 11) // Extraer la variable equivalente
                {
                    ExtraerCuadratica(formas[formas.Count - 1]);
                }
                else if (avance == 11 ) // Extraer la forma siguiente si la hay
                {
                    if (formas[formas.Count - 1].Largo > 0)
                    {
                        lbExplicacion.Text = "Si quitamos la nueva variable de la forma actual, obtenemos una nueva forma C" + (cuadraticas.Count + 1).ToString();
                        indicespequeña.Add(rtbExplicaciones.Text.Length);
                        rtbExplicaciones.Text += "\nExtraer la forma siguiente.";
                        indicespequeña.Add(rtbExplicaciones.Text.Length - indicespequeña[indicespequeña.Count - 1]);
                        // Escribir el termino que representa la forma siguiente
                        rtbExplicaciones.Text += "\nC" + (cuadraticas.Count + 1).ToString() + " = " + formas[formas.Count - 1].ToString();
                        for (int i = 0; i < indicespequeña.Count - 1; i += 2)
                        {
                            rtbExplicaciones.Select(indicespequeña[i], indicespequeña[i + 1]);
                            rtbExplicaciones.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                        }
                        // Cambiar los terminos pertenecientes al cuadrado de la suma en color verde amarillento
                        for (int i = 0; i < indicesamarillos.Count - 1; i += 2)
                        {
                            rtbExplicaciones.Select(indicesamarillos[i], indicesamarillos[i + 1] - indicesamarillos[i]);
                            rtbExplicaciones.SelectionColor = Color.GreenYellow;
                        }
                    }
                    else
                    {
                        lbExplicacion.Text = "Al simplificar se eliminan todos los terminos fuera del cuadrado de la suma.";
                    }
                    avance = 12;
                }
                else if (avance == 12) // Sustituir la forma siguiente por el termino que la representa
                {
                    if (formas[formas.Count - 1].Largo > 0)
                        lbExplicacion.Text = "Si sustituimos en la forma anterior la forma siguiente por la nueva variable C" + (cuadraticas.Count + 1).ToString();
                    else
                        lbExplicacion.Text = "La forma queda:";

                    if (cuadraticas.Count == 1) // Solo la primera vez
                    {
                        label3.Show();
                        label3.ResetText();
                        label3.Location = new Point(label2.Location.X - 300, label2.Location.Y);
                        label3.BackColor = Color.SeaGreen;
                        label3.Font = label2.Font;
                        label3.ForeColor = Color.Beige;
                        label3.TextAlign = ContentAlignment.MiddleLeft;
                    }
              // Añadir a la lista el siguiente polinomio que es la forma siguiente usando las nuevas variables
                   Polinomio forma2 = new Polinomio(new List <Termino>());
                    forma2.AñadirTermino(new Termino(1,(char)((int)'P' + cuadraticas.Count - 1),2));
                    if (formas[formas.Count - 1].Largo > 0)
                    {
                        Termino siguienteforma = (new Termino(1, new List<char>() { (cuadraticas.Count + 1).ToString()[0], 'C' }, new List<Racional> { 1, 1 }));
                        forma2.AñadirTermino(siguienteforma);
                    }
                    forma2.EliminarCeros();
                    formas2.Add(forma2);
                    formas2[formas2.Count - 1].ObtenerTermino(formas2[formas2.Count-1].Largo - 1).Variables.Reverse();
                    // Escribir la forma siguiente con las nuevas variables en la etiqueta de la parte superior
                          if (cuadraticas.Count > 1)
                        label3.Text += "\n";
                    label3.Text += cuadraticas[cuadraticas.Count - 1].ToString() + " = " + formas2[formas2.Count-1].ToString();
                    avance = 2;
                }
            }
            else if (cuadraticas.Count == matrizracional.GetLength(0)) // Si ya se han obtenido todas las nuevas variables equivalentes
            {
                if (avance < 20)
                {
                    // Añadir la ultima forma a la lista
                    formas2.Add(formasiguiente);

                    lbExplicacion.Text = " Si reducimos a la unidad el coeficiente del termino que equivale a cada forma:";
                    rtbExplicaciones.Text = "";
                    for (int i = 0; i < formas2.Count; i++)
                    {
                        if (formas2[i].Largo > 0)
                        {
                            formas2[i] = formas2[i] / cuadraticas[i].Coeficiente;
                            formas2[i].Terminos[formas2[i].Largo - 1].Variables.Reverse();
                            cuadraticas[i].Coeficiente = 1;
                            rtbExplicaciones.Text += cuadraticas[i].ToString() + " = " + formas2[i].ToString() + "\n";
                        }
                    }
                    avance = 20;
                }
                else if (avance == 20)
                {
                    lbExplicacion.Text = "Si sustituimos cada forma en la anterior hasta llegar a la primera, la forma equivalente sin terminos rectangulares queda:";
                    for (int i = formas2.Count - 1; i > 0; i--)
                    {
                        if (formas2[i].Largo > 0)
                        {
                            Racional coef = new Racional(formas2[i - 1].ObtenerTermino(formas2[i - 1].Largo - 1).Coeficiente);
                            formas2[i - 1].Terminos.RemoveAt(formas2[i - 1].Largo - 1);
                            foreach (Termino t in formas2[i].Terminos)
                            {
                                formas2[i - 1].AñadirTermino(new Termino(t.Coeficiente * coef, t.Variables, t.Exponentes));
                            }
                        }
                    }
                    formas2[0].EliminarCeros();
                    rtbExplicaciones.Text += "\n" + cuadraticas[0].ToString() + " = " + formas2[0].ToString();
                    avance++;
                }
                else if (avance == 21)
                {
                    lbExplicacion.Text = "Se puede observar que sustituyendo las variables de la forma sin terminos rectangulares, se obtiene la forma inicial: ";
                    rtbExplicaciones.Text =  "\n" + cuadraticas[0].ToString() + " = " + formas2[0].ToString() + " = " + forma.ToString();
                  //  rtbExplicaciones.Location = new Point(rtbExplicaciones.Location.X, rtbExplicaciones.Location.Y + 200);
                    label3.Hide();
                    avance++;
                }
                else if (avance == 22) // Mostrar la matriz diagonal
                {
                    lbExplicacion.Text = "La matriz diagonal se construye con los coeficientes de los terminos de la forma sin terminos rectangulares:";
                    diagonal = new Label[orden, orden];
                    matrizdiagonal = new Racional[orden, orden];
                    Point origen = new Point(simetric[0, orden - 1].Location.X + simetric[0,0].Width + 15, simetric[0, 0].Location.Y);
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            diagonal[i, j] = new Label();
                            Controls.Add(diagonal[i, j]);
                            diagonal[i, j].Show();
                            diagonal[i, j].Size = simetric[0, 0].Size;
                            diagonal[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            diagonal[i, j].BackColor = Color.LightGreen;
                            diagonal[i, j].Location = origen;
                            if (i == j)
                            {
                                try
                                {
                                    diagonal[i, j].Text = formas2[0].ObtenerTermino(i).Coeficiente.ToString();
                                    matrizdiagonal[i, j] = new Racional(formas2[0].ObtenerTermino(i).Coeficiente);
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    diagonal[i, j].Text = "0";
                                    matrizdiagonal[i, j] = 0;
                                }
                            }
                            else
                            {
                                diagonal[i, j].Text = "0";
                                matrizdiagonal[i, j] = 0;
                            }
                            origen.X += simetric[0, 0].Width + 10;
                        }
                        origen = new Point(simetric[0, orden - 1].Location.X + simetric[0,0].Width +15, origen.Y + simetric[0, 0].Height + 17);
                    }
                    // Mostrar el titulo de la matriz diagonal
                    label10.Show();
                    label10.BackColor = Color.SeaGreen;
                    label10.Font = new Font (rotulosh[0].Font.FontFamily,10,FontStyle.Underline);
                    label10.AutoSize = false;
                    label10.Size = new Size(diagonal[0, orden - 1].Location.X - diagonal[0, 0].Location.X + diagonal[0, 0].Width, 20);
                    label10.Location = new Point(diagonal[0,0].Location.X, rotulosh[0].Location.Y-10);
                    label10.TextAlign = ContentAlignment.MiddleCenter;
                    label10.Text = "Matriz diagonal";
                    avance++;
                }
                else if (avance == 23)
                {
                    lbExplicacion.Text = "Para construir la matriz modal, tenemos que despejar las variables: ";
                    for (int i = 0; i < orden; i++)
                    {
                        lbExplicacion.Text += (char)((int)'a' + i) + " , ";
                    }
                    lbExplicacion.Text += " del valor de cada nueva variable.\nPara ello, construimos un sistema en que cada ecuación es una de las igualdades.";
                    // Construir el sistema
                    sistema = new Sistema(new List<Ecuacion>());
                    for (int i = 0; i < orden; i++)
                    {
                        Ecuacion aux = new Ecuacion(new List<Termino>(), new List<Termino>()); // Ecuacion que se añadira al sistema
                        aux.AñadirTerminoIzquierda(new Termino(1, (char)((int)'P' + i), 1));// Añadir la nueva variable a la izquierda
                        foreach (Termino t in cambiodevariables[i].Terminos)
                            aux.AñadirTerminoDerecha(new Termino(t));
                        aux.ObtenerLadoDerecho.EliminarCeros();
                        aux.ObtenerLadoIzquierdo.EliminarCeros();
                        sistema.AñadirEcuacion(new Ecuacion(aux));
                    }
                    sistema.ObtenerEcuacion(orden-1).BorrarLadoDerecho();

                    if (cambiodevariables[cambiodevariables.Count - 1].Terminos[0].Coeficiente.Numerador != 0)
                        sistema.ObtenerEcuacion(orden - 1).AñadirTerminoDerecha(new Termino(1, cambiodevariables[orden - 1].ObtenerTermino(0).Variables[0], 1));
                    else
                        sistema.ObtenerEcuacion(orden - 1).AñadirTerminoDerecha(new Termino(0, cambiodevariables[orden - 1].ObtenerTermino(0).Variables[0], 1));

                    // Si hay alguna ecuacion sin terminos en el lado derecho, añadir un cero en el lado derecho
                    for (int i = 0; i < orden; i++)
                    {
                        if (sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Largo == 0)// && sistema.ObtenerEcuacion(i).ObtenerTerminoDerecha(0).Variables[0] == ' ')
                        {
                            sistema.ObtenerEcuacion(i).BorrarLadoDerecho();
                          //  sistema.ObtenerEcuacion(i).AñadirTerminoDerecha(new Termino(0, vari, 1));
                            sistema.ObtenerEcuacion(i).AñadirTerminoDerecha(new Termino(0, ' ', 1));
                        }
                    }
                    rtbExplicaciones.Text += "\n\n" + sistema.ToString();
                    avance++;
                }
                else if (avance == 24)
                {
                    lbExplicacion.Text = "Despejamos una de las variables en cada ecuación del sistema:";
                    List<char> variables = sistema.VariablesDelSistema; // Para llevar el control de las variables despejadas
                    // Dejar solo las variables minusculas en la lista de variables
                    List<char> aux = new List<char>();
                    foreach (char c in variables)
                        if (char.IsLower(c))
                            aux.Add(c);
                    variables = aux;
                    // Si alguna ecuacion tiene un cero en el lado derecho, añadir al termino de la derecha una variable minuscula, en
                    // funcion de la variable que haya en el lado izquierdo
                    for (int i = 0; i < orden - 1; i++)
                    {
                        if (sistema.ObtenerEcuacion(i).ObtenerTerminoDerecha(0).Coeficiente.Numerador == 0)
                        {
                            int diferencia = sistema.ObtenerEcuacion(i).ObtenerTerminoIzquierda(0).Variables[0] - 'P';
                            char vari = (char)((int)'a' + diferencia);
                            sistema.ObtenerEcuacion(i).ObtenerTerminoDerecha(0).Variables[0] = vari;
                        }
                    }

                    // Elegir una variable en cada ecuacion y despejarla
                    for (int i = orden-1; i >= 0; i--)
                    {
                        char variableadespejar = ' ';
                        int index = 0; // indice del termino del que se va a coger la variable
                        while (index < sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Largo && variableadespejar == ' ')
                        {
                            if ( variables.Contains(sistema.ObtenerEcuacion(i).ObtenerTerminoDerecha(index).Variables[0]))
                            {
                                variableadespejar = sistema.ObtenerEcuacion(i).ObtenerTerminoDerecha(index).Variables[0];
                                variables.Remove(variableadespejar);
                            }

                            else
                                index++;
                        }
                        sistema.ObtenerEcuacion(i).DespejarVariable(variableadespejar);
                        sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.EliminarCeros();
                    }
                    for( int i = 0; i < orden;i++)
                    sistema.BorrarEcuacionesNulas();

                    // Si hay ecuaciones en el sistema con un cero en el lado izquierdo, eliminar esas ecuaciones y añadir una nueva
                    // con cada variable que quede en la lista en el lado izquierdo y cero en el lado derecho
                    for (int i = 0; i < sistema.CantidadDeEcuaciones; i++)
                    {
                        if (sistema.ObtenerEcuacion(i).ObtenerTerminoIzquierda(0).Coeficiente.Numerador == 0)
                            sistema.EliminarEcuacion(i);
                    }
            
                    for (int i = 0; i < variables.Count; i++)
                    {
                        sistema.AñadirEcuacion(new Ecuacion(new List<Termino>(), new List<Termino>()));
                        sistema.ObtenerEcuacion(sistema.CantidadDeEcuaciones - 1).AñadirTerminoIzquierda(new Termino(1, variables[i], 1));
                        sistema.ObtenerEcuacion(sistema.CantidadDeEcuaciones - 1).ObtenerLadoIzquierdo.EliminarCeros();
                    }
                    rtbExplicaciones.Text += "\n" + sistema.ToString();
                    avance++;
                }
                else if (avance == 25)
                {
                    lbExplicacion.Text = "Sustituimos en todas las ecuaciones el valor de la variable despejada en la ecuación anterior:";
                    bool sustituidas = false; // Para controlar cuando se han sustituido todas las variables del lado izquierdo
                    int asustituir = orden - 1; // Ecuacion que se sustituirá en las demas.
                    while (!sustituidas)
                    {
                        sustituidas = true;
                        for (int i = 0; i < asustituir; i++)
                        {
                            sistema.ObtenerEcuacion(i).Sustituir(sistema.ObtenerEcuacion(asustituir));
                            sistema.ObtenerEcuacion(i).DespejarVariable(sistema.ObtenerEcuacion(i).ObtenerTerminoDerecha(sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Largo - 1).Variables[0]);
                            sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.EliminarCeros();
                        }
                        // Comprobar si se han sustituido todas las variables
                        for (int i = 0; i < sistema.CantidadDeEcuaciones - 1; i++)
                        {
                            foreach (Termino t in sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Terminos)
                            {
                                if (char.IsLower(t.Variables[0]))
                                    sustituidas = false;
                            }
                        }
                        asustituir--;
                    }

                    rtbExplicaciones.Text = cuadraticas[0].ToString() + " = " + formas2[0].ToString() + " = " + forma.ToString();
                    rtbExplicaciones.Text += "\n\n" + sistema.ToString();
                    avance++;
                }
                else if (avance == 26)
                {
                    lbExplicacion.Text = "Cada fila de la matriz modal, corresponde a la variable de la izquierda de una ecuacion del sistema ( variable 'a' primera fila, variable 'b' segunda fila, etc).";
                    // Construir la matriz modal
                    modalracional = new Racional[matrizracional.GetLength(0), matrizracional.GetLength(0)];
                    // Asignar ceros a todos los elementos de la matriz modal como valor por defecto.
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            modalracional[i, j] = 0;
                        }
                    }
                    // Asignar los coeficientes de las ecuaciones del sistema en funcion de la variable de la izquierda de cada ecuacion
                    // a las filas correspondientes de la matriz modal
                    for (int i = 0; i < sistema.CantidadDeEcuaciones; i++)
                    {
                        int filamodal = (int)(sistema.ObtenerEcuacion(i).ObtenerTerminoIzquierda(0).Variables[0]) - (int)'a';
                        foreach (Termino t in sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Terminos)
                        {
                            int columnamodal = (int)(t.Variables[0]) - (int)'P';
                            try
                            {
                                modalracional[filamodal, columnamodal] = t.Coeficiente;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                continue;
                            }
                        }
                    }
                    // Contruir la matriz de etiquetas para mostrar la matriz modal
                    modal = new Label[orden, orden];
                    Point origen = new Point(diagonal[0, orden - 1].Location.X + 60, diagonal[0, orden - 1].Location.Y);
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            modal[i, j] = new Label();
                            modal[i, j].Location = new Point(origen.X + 60 * contador, origen.Y);
                            modal[i, j].Size = diagonal[0, 0].Size;
                            modal[i, j].Font = new Font("Dejavu-Sans", 10);
                            modal[i, j].BackColor = Color.LawnGreen;
                            Controls.Add(modal[i, j]);
                            modal[i, j].Show();
                            modal[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            contador++;
                        }
                        origen.Y += 40;
                        contador = 0;
                    }
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            modal[i, j].Text = Racional.AString(modalracional[i, j]);
                        }
                    }
                    // Añadir el rotulo de la matriz modal
                    label11.Show();
                    label11.Location = new Point(modal[0, 0].Location.X, label10.Location.Y);
                    label11.BackColor = Color.SeaGreen;
                    label11.Font = label10.Font;
                    label11.Text = "Matriz Modal";
                    label11.AutoSize = false;
                    label11.Size = label10.Size;

                    // Mover la etiqueta con la equivalencia de las nuevas variables en funcion del orden 
                    label2.Location = new Point(modal[0, orden - 1].Location.X + 100, 100);

                    avance++;
                }
                else if (avance == 27)
                {
                    lbExplicacion.Text = " Podemos probar que el resultado es correcto, comprobando que se cumpla la siguiente igualdad:\n[ Matriz diagonal ]  = [ Traspuesta de la matriz modal ] x [ Matriz original ] x [ Matriz modal ].\nComprobémoslo:  ";
                    // Mostrar el rotulo de la matriz comprobacion
                    label12.Show();
                    label12.BackColor = Color.SeaGreen;
                    label12.Font = label10.Font;
                    label12.AutoSize = false;
                    label12.Size = new Size(300, 20);
                    label12.Text = "Matriz original x Matriz modal";
                    comprobacionracional = Matematicas.AlgebraLineal.MultiplicarMatrices(matrizracional, modalracional);
                    modaltraspuesta = Matematicas.AlgebraLineal.Traspuesta(modalracional);
                    comprobacion = new Label[orden, orden];
                    //Point origen = new Point(diagonal[orden - 1, 0].Location.X, diagonal[orden - 1, 0].Location.Y + 60);
                    Point origen = new Point(diagonal[orden - 1, 0].Location.X, btContinuar.Location.Y + 100);
                    contador = 0;
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            comprobacion[i, j] = new Label();
                            comprobacion[i, j].Location = new Point(origen.X + 60 * contador, origen.Y);
                            comprobacion[i, j].Size = new Size(50, 20);
                            comprobacion[i, j].Font = new Font("Dejavu-Sans", 10);
                            comprobacion[i, j].BackColor = Color.Orange;
                            Controls.Add(comprobacion[i, j]);
                            comprobacion[i, j].Show();
                            comprobacion[i, j].Text = Racional.AString(comprobacionracional[i, j]);
                            contador++;
                        }
                        origen.Y += 40;
                        contador = 0;
                    }
                   // label12.Location = new Point(label10.Location.X, comprobacion[0, 0].Location.Y-35);
                    label12.Location = new Point(label10.Location.X - ((300-label10.Width)/2), comprobacion[0, 0].Location.Y - 35);
                    // Ocultar el richTextBox
                    rtbExplicaciones.Location = new Point(rtbExplicaciones.Location.X, comprobacion[orden - 1, 0].Location.Y+100);
                    rtbExplicaciones.Height = 50;
                    avance++;
                }
                else if (avance == 28)
                {
                    label12.Text = "Modal traspuesta * Matriz simetrica * Modal";
                    lbExplicacion.Text = "Obtenemos la matriz diagonal, por lo tanto, el resultado es correcto.";
                    comprobacionracional = (Matematicas.AlgebraLineal.MultiplicarMatrices(modaltraspuesta, comprobacionracional));
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            comprobacion[i, j].BackColor = Color.PaleGreen;

                            comprobacion[i, j].Text = Racional.AString(comprobacionracional[i, j]);
                        }
                    }
                    avance++;
                    btContinuar.Hide();
                    lbExplicacion.Focus();
                }

            }
        }


        /// <summary>
        /// 
        /// REALIZA EL CAMBIO  DE VARIABLE POR EL METODO DE LAGRANGE Y METE LA FORMA SIGUIENTE CON MENOS
        /// TERMINOS RECTANGULARES EN LA LISTA DE FORMAS
        /// 
        /// </summary>
        /// <param name="forma"></param>
        /// 
        private void ExtraerCuadratica(Polinomio forma)
        {
            if (avance == 2 ) // Mostrar el termino cuadratico elegido para construir el cuadrado de una suma
            {
                bool sinrectangulares = true; // Para comprobar si la forma pasada como argumento, no tiene terminos rectangulares
                foreach (Termino t in forma.Terminos)
                {
                    if (t.Exponentes[0].Numerador != 2)
                        sinrectangulares = false;
                }
                bool sincuadraticos = true; // Para comprobar si en la forma quedan terminos cuadraticos, de lo contrario no se puede seguir
                foreach (Termino t in forma.Terminos)
                {
                    if (t.Exponentes[0].Numerador == 2)
                        sincuadraticos = false;
                }
                if (sincuadraticos && forma.Terminos.Count > 0)
                {
                    lbExplicacion.Text = "No quedan terminos cuadráticos en la forma siguiente. No se puede diagonalizar por este método.";
                    btContinuar.Hide();
                    return;
                }
                else if( sincuadraticos && forma.Terminos.Count == 0)
                    sinrectangulares = true;

                if (sinrectangulares) // La forma ya esta diagonalizada
                {
                    if (formas[formas.Count - 1].Largo > 0)
                    {
                        lbExplicacion.Text = "Como ya solo quedan terminos cuadraticos, podemos asignar estos terminos a una nueva variable.";
                        forma1 = new Termino(1, new List<char>() { 'C', (cuadraticas.Count + 1).ToString()[0] }, new List<Racional>() { 1, 1 }); //termino que representa a la forma cuadratica
                        forma1.Variables.Reverse();
                        cuadraticas.Add(forma1);
                        // añadir a la lista de Polinomios cambiodevariables las variables que componen la forma
                        Polinomio form = new Polinomio(new List<Termino>());
                        foreach (Termino t in forma.Terminos)
                            form.AñadirTermino(new Termino(t.Coeficiente, t.Variables[0], 1));
                        form.EliminarCeros();
                        cambiodevariables.Add(form);
                        // Añadir la forma siguiente a la lista de formas
                        formas.Add(new Polinomio(forma));
                        if (forma.Terminos.Count > 0)
                            label2.Text += "\n" + (char)((int)'P' + cuadraticas.Count - 1) + " = " + forma.ObtenerTermino(0).Variables[0].ToString();
                        label3.Text += "\n+C" + (cuadraticas.Count).ToString() + " = " + forma.ToString();
                    }
                    else
                        lbExplicacion.Text = "Como no quedan teminos, asignamos cero al resto de las nuevas variables";

                    // Si quedan variables sin asignar, asignarles valor cero
                    while (cuadraticas.Count < orden)// && forma1.Variables[0] != (char)((int)'P' + orden - 1))
                    {
                        cuadraticas.Add(new Termino(1, (char)((int)'P' + cuadraticas.Count), 1));
                        cambiodevariables.Add(new Polinomio(new List<Termino>()));
                        label2.Text += "\n" + (char)((int)'P' + (cuadraticas.Count - 1)) + " = " + cambiodevariables[cambiodevariables.Count - 1].ToString();
                    }
                    return;
                }

                lbExplicacion.Text = "Para reducir los terminos rectangulares, elegimos uno cualquiera de los terminos elevados al cuadrado.\nPor ejemplo, en este caso, elegimos el primer termino cuadratico: " + forma.ObtenerTermino(0).ToString();
                if (cuadraticas.Count == 0) // Solo la primera iteracion
                    lbExplicacion.Text += "\nEl procedimiento para eliminar los terminos rectangulares, es obtener el cuadrado de una suma y sustituir el mismo por una nueva variable ( cambio de variable).";
                else
                    lbExplicacion.Text += "\nAplicamos el mismo procedimiento anterior para reducir los terminos rectangulares: Obtener el cuadrado de una suma para realizar el siguiente cambio de variable.";
                avance++;
                return;
            }
            else if (avance == 3 ) // Comprobar si el coeficiente del termino elegido es un cuadrado perfercto y multiplicar los dos lados
                                               // de la igualdad por el coeficiente si no es cuadrado perfecto
            {
                if (cuadraticas.Count > 0)
                {
                    rtbExplicaciones.Text = "";
                }
                indicesamarillos.Clear();
                indicespequeña.Clear();
                forma1 = new Termino(1, new List<char>() { 'C', (cuadraticas.Count+1).ToString()[0] }, new List<Racional>() { 1, 1 }); //termino que representa a la forma cuadratica
                forma1.Variables.Reverse();
                coeficienteelegido = forma.ObtenerTermino(0).Coeficiente; //  Coeficiente del termino cuadratico que se usara para sacar factor comun
	        cuadradoperfecto = coeficienteelegido.EsCuadradoPerfecto();
                formainicial = new Polinomio(forma); // Hacer copia de la forma inicial para operar
                if (cuadraticas.Count > 0)
                {
                    rtbExplicaciones.Text = "Forma: ";
                    rtbExplicaciones.Text += "\n" +forma1.ToString()  + " = " + formas[formas.Count - 1].ToString();
                }
                if (cuadradoperfecto)
                {
                    lbExplicacion.Text = "Comenzamos a realizar operaciones para obtener el cuadrado de una suma. \nComo el coeficiente del termino elegido " + Racional.AString(coeficienteelegido) + " es un cuadrado perfecto (su raiz es un entero),no tenemos que hacer nada mas.";
                }
                else if (!cuadradoperfecto)
                {
                    lbExplicacion.Text = " Como el coeficiente del termino elegido " + Racional.AString(coeficienteelegido) + " no es un cuadrado perfecto ( su raiz no es un entero ), multiplicamos los dos lados de la igualdad por el coeficiente del termino elegido. ";
                    forma1 *= coeficienteelegido;
                    forma1.Variables.Reverse();
                    formainicial *= coeficienteelegido;
                    formainicial.EliminarCeros();
                    indicespequeña.Add(rtbExplicaciones.Text.Length);
                    rtbExplicaciones.Text += "\nMultiplicar los dos lados de la ecuacion por: " + Racional.AString(coeficienteelegido);
                    indicespequeña.Add(rtbExplicaciones.Text.Length - indicespequeña[indicespequeña.Count-1]);
                    rtbExplicaciones.Text += "\n" + forma1.ToString() + " = ";
                    rtbExplicaciones.Text += formainicial.ToString();
                }
                for (int i = 0; i < indicespequeña.Count - 1; i++)
                {
                    rtbExplicaciones.Select(indicespequeña[i], indicespequeña[i + 1]);
                    rtbExplicaciones.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                }
                avance++;
                return;
            }
            else if (avance == 4 ) //Sacar factor comun de la raiz del termino elegido multiplicado por dos ( doble del primero )
            {
                long coeficienteelegidolong = (long)(coeficienteelegido.Numerador / coeficienteelegido.Denominador);
                Racional raiz = new Racional(formainicial.ObtenerTermino(0).Coeficiente);
		//Comprobar si se trata del cuadrado de una suma o de una resta
		 
		 if(raiz.Numerador < 0)
		 {
		    raiz = raiz*-1;
		    coeficientenegativo= true;
		    forma1 = new Termino(-1, new List<char>() { 'C', (cuadraticas.Count+1).ToString()[0] }, new List<Racional>() { 1, 1 }); //termino que representa a la forma cuadratica
		    forma1.Variables.Reverse();
		    formainicial *= -1;
		    formainicial.EliminarCeros();
		    rtbExplicaciones.Text += "\n" + forma1.ToString() + " = ";
                    rtbExplicaciones.Text += formainicial.ToString();
		 }
                raiz.Raiz();
		//if(cuadradoresta)
		//factorcomun = new Termino(-2 * raiz, formainicial.ObtenerTermino(0).Variables, new List<Racional>() { new Racional(1, 1) });
		//else
                 factorcomun = new Termino(2 * raiz, formainicial.ObtenerTermino(0).Variables, new List<Racional>() { new Racional(1, 1) });
               
	        lbExplicacion.Text = " En este caso, la raiz de " + formainicial.ObtenerTermino(0).ToString() + " multiplicada por dos es:" + factorcomun.ToString() + "\nSacamos factor comun de la raiz del termino elegido multiplicado por dos ( doble del primer termino del cuadrado de una suma ), en todos los terminos donde aparezca la variable del termino elegido en grado de unidad. ";
                if(coeficientenegativo)
		 lbExplicacion.Text += "\nComo el coeficiente del término elegido es negativo, multiplicamos los dos lados de la igualdad por -1 con objeto de operar más facilmente.";
		comunes = formainicial.SacarFactorComun(factorcomun);
                comunes.EliminarCeros();
                factorcomun1 = new FactorComun(factorcomun, comunes);
                indicespequeña.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text +="\nSacar factor común de la raiz del coeficiente del término cuadrático elegido multiplicada por dos ( doble del primero por el segundo).";
                indicespequeña.Add(rtbExplicaciones.Text.Length - indicespequeña[indicespequeña.Count-1]);
                rtbExplicaciones.Text += "\n" + forma1.ToString() + " = " + formainicial.ToString();
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += factorcomun1.Factor.ToString() + "*(";
                foreach (Termino t in factorcomun1.TerminosComunes.Terminos)
                {
                    rtbExplicaciones.Text += t.ToString();
                }
                rtbExplicaciones.Text += ")";
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                for (int i = 0; i < indicesamarillos.Count - 1; i++)
                {
                    rtbExplicaciones.Select(indicesamarillos[i], indicesamarillos[i + 1] - indicesamarillos[i]);
                    rtbExplicaciones.SelectionColor = Color.GreenYellow;
                }
                memoforma = new Polinomio(new List<Termino>());
                foreach (Termino t in formainicial.Terminos)
                {
                    memoforma.AñadirTermino(new Termino(t));
                }
                for (int i = 0; i < indicespequeña.Count - 1; i+=2)
                {
                    rtbExplicaciones.Select(indicespequeña[i], indicespequeña[i + 1]);
                    rtbExplicaciones.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                }
                avance++;
                return;
            }
            else if (avance == 5 ) //
            {
                lbExplicacion.Text = "Para completar el cuadrado de la suma sin alterar la forma cuadratica, sumamos y restamos el cuadrado de los factores comunes que hay dentro del parentesis ( cuadrado del segundo)";
                lbExplicacion.Text += "\nLos terminos en color amarillo son los que componen el cuadrado de una suma.";
                indicespequeña.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "\nSumar y restar el contenido del parentesis al cuadrado ( cuadrado del segundo ).";
                indicespequeña.Add(rtbExplicaciones.Text.Length - indicespequeña[indicespequeña.Count - 1]);
                // Escribir el termino que representa a la forma
                rtbExplicaciones.Text += "\n" + forma1.ToString() + " = ";
                // Escribir el cuadrado del primero
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += formainicial.ObtenerTermino(0).ToString();
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                // Escribir los terminos que quedaran fuera del cuadrado de la suma
                for (int i = 1; i < formainicial.Largo; i++)
                {
                    rtbExplicaciones.Text += formainicial.ObtenerTermino(i).ToString();
                }
                // Escribir el doble del primero por el segundo
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += factorcomun1.Factor.ToString() + "*(";
                foreach (Termino t in factorcomun1.TerminosComunes.Terminos)
                    rtbExplicaciones.Text += t.ToString();
                rtbExplicaciones.Text += ") + (";
                // Escribir la suma del cuadrado del segundo
                foreach (Termino t in factorcomun1.TerminosComunes.Terminos)
                {
                    rtbExplicaciones.Text += t.ToString();
                }
                rtbExplicaciones.Text += " )^2 ";
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                // Escribir la resta del cuadrado del segundo
                rtbExplicaciones.Text += "- ( ";
                foreach (Termino t in factorcomun1.TerminosComunes.Terminos)
                {
                    rtbExplicaciones.Text += t.ToString();
                }
                rtbExplicaciones.Text += " )^2 ";
                // Cambiar los terminos pertenecientes al cuadrado de la suma en color verde amarillento
                for (int i = 0; i < indicesamarillos.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicesamarillos[i], indicesamarillos[i + 1] - indicesamarillos[i]);
                    rtbExplicaciones.SelectionColor = Color.GreenYellow;
                }
                for (int i = 0; i < indicespequeña.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicespequeña[i], indicespequeña[i + 1]);
                    rtbExplicaciones.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                }
                avance++;
                return;
            }
            else if (avance == 6 ) //Escribe el cuadrado de la suma compactado y el resto de terminos fuera de el
            {
                lbExplicacion.Text = " Si compactamos el cuadrado de la suma, la forma queda: ";
                formainicial.Terminos.RemoveAt(0); // eliminar de la forma el primer termino
                formainicial.Ordenar();
                // añadir a la lista de Polinomios cambiodevariables los polinomios que componen el cuadrado de la suma
                cambiodevariables.Add(new Polinomio(new List<Termino>() { factorcomun1.Factor / 2 }) + factorcomun1.TerminosComunes);
                p = cambiodevariables[cambiodevariables.Count - 1];
                p.EliminarCeros();
                indicespequeña.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "\nCompactar el cuadrado de la suma.";
                indicespequeña.Add(rtbExplicaciones.Text.Length - indicespequeña[indicespequeña.Count - 1]);
               
		// Escribir el termino que representa a la forma
                rtbExplicaciones.Text += "\n" + forma1.ToString() + " = ";
                // Escribir el cuadrado de la suma
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "(";
                foreach (Termino t in p.Terminos)
                {
                    rtbExplicaciones.Text += t.ToString();
                }
                rtbExplicaciones.Text += ")^2";
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                // Escribir los demas terminos de la forma
                rtbExplicaciones.Text += formainicial.ToString() + "  - ( ";
                foreach (Termino t in factorcomun1.TerminosComunes.Terminos)
                    rtbExplicaciones.Text += t.ToString();
                rtbExplicaciones.Text += " )^2";
                // Cambiar los terminos pertenecientes al cuadrado de la suma en color verde amarillento
                for (int i = 0; i < indicesamarillos.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicesamarillos[i], indicesamarillos[i + 1] - indicesamarillos[i]);
                    rtbExplicaciones.SelectionColor = Color.GreenYellow;
                }
                for (int i = 0; i < indicespequeña.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicespequeña[i], indicespequeña[i + 1]);
                    rtbExplicaciones.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                }
                avance++;
                return;
            }
            else if (avance == 7 ) //Mostrar el cambio de variable en la parte superior del formulario
            {
                lbExplicacion.Text = " Si sustituimos los sumandos en el cuadrado de la suma anterior por una nueva variable: " + nuevavariable + "  ( cambio de variable )";
                // Crear una nueva variable "P" a la que se le asignara el polinomio que es el cuadrado de la suma desarrollado
                P = new Polinomio(new List<Termino>() { (factorcomun1.Factor / 2) * (factorcomun1.Factor / 2) }); // asignar a P el primer termino al cuadrado
                foreach (Termino t in factorcomun1.TerminosComunes.Terminos) // asignar a P el doble del primero por el segundo 
                {
                    P.AñadirTermino(factorcomun1.Factor * t);
                }
                // sumar a P el cuadrado del segundo
                P += (factorcomun1.TerminosComunes * factorcomun1.TerminosComunes);
                P.Simplificar();
                P.Ordenar();

                nuevavariable = ' ';
                nuevavariable = (char)((int)'P' + (cuadraticas.Count));
                // Escribir la igualdad entre la nueva variable y el cuadrado de la suma desarrollado en una etiqueta en la parte superior del formulario
                if (cuadraticas.Count == 0)
                {
                    label2.ResetText();
                    label2.Location = new Point(800, 100);  
                    label2.ForeColor = Color.Blue;
                    label2.Font = new Font("DejaVu Sans", 14);
                    label2.AutoSize = true;
                    label2.Show();
                    label2.BackColor = Color.SeaGreen;
                    label2.TextAlign = ContentAlignment.MiddleLeft;
                    Point origen = new Point(label11.Location.X, label11.Location.Y + (50 * iteraciones));
                }
                if (cuadraticas.Count > 0)
                    label2.Text += "\n";
                label2.Text += nuevavariable + " = ";
                label2.Text += "(" + cambiodevariables[cambiodevariables.Count-1].ToString() + ")";
                avance++;
                return;
            }
            else if (avance == 8 ) //Mostrar la forma con la nueva variable sustituida
            {
                lbExplicacion.Text += "\nEntonces:";
                indicespequeña.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "\nSustituir el cuadrado de la suma por una nueva variable.";
                indicespequeña.Add(rtbExplicaciones.Text.Length - indicespequeña[indicespequeña.Count - 1]);
                // Escribir el termino que representa a la forma
                rtbExplicaciones.Text += "\n" + forma1.ToString() + " = ";
                // Escribir la nueva variable al cuadrado
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += nuevavariable + "^2";
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                // Escribir el resto de terminos de la forma
                rtbExplicaciones.Text += formainicial.ToString() + " - (";
                foreach (Termino t in factorcomun1.TerminosComunes.Terminos)
                    rtbExplicaciones.Text += t.ToString();
                rtbExplicaciones.Text += " )^2";
                // Cambiar los terminos pertenecientes al cuadrado de la suma en color verde amarillento
                for (int i = 0; i < indicesamarillos.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicesamarillos[i], indicesamarillos[i + 1] - indicesamarillos[i]);
                    rtbExplicaciones.SelectionColor = Color.GreenYellow;
                }
                for (int i = 0; i < indicespequeña.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicespequeña[i], indicespequeña[i + 1]);
                    rtbExplicaciones.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                }
                avance++;
            }
            else if (avance == 9) // Mostrar la forma con la resta realizada
            {
                lbExplicacion.Text = " Eliminando el parentesis en la resta anterior : ";
                indicespequeña.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "\nEliminar el parentesis.";
                indicespequeña.Add(rtbExplicaciones.Text.Length - indicespequeña[indicespequeña.Count - 1]);
                // Escribir el termino que representa a la forma
                rtbExplicaciones.Text += "\n" + forma1.ToString() + " = ";
                // Escribir la nueva variable al cuadrado
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += nuevavariable + "^2";
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                // Crear el polinomio resultado de elevar el polinomio entre parentesis al cuadrado
                Polinomio aux2 = (factorcomun1.TerminosComunes * factorcomun1.TerminosComunes * -1);
                aux2.Simplificar();
                aux2.EliminarCeros();
                aux2.Ordenar();
                // Escribir el resto de terminos de la forma
                rtbExplicaciones.Text += formainicial.ToString();
                //   else if (avance == 20)
                //     aux15.Text = formasiguiente.ToString();
                // Escribir el polinomio creado anteriormente
                int indiceazul = rtbExplicaciones.Text.Length;
                rtbExplicaciones.Text += aux2.ToString();
                int largoazul = rtbExplicaciones.Text.Length - indiceazul;
                // Cambiar los terminos pertenecientes al cuadrado de la suma en color verde amarillento
                for (int i = 0; i < indicesamarillos.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicesamarillos[i], indicesamarillos[i + 1] - indicesamarillos[i]);
                    rtbExplicaciones.SelectionColor = Color.GreenYellow;
                }
                // Cambiar el polinomio creado anteriormente a color azul
                rtbExplicaciones.Select(indiceazul, largoazul);
                rtbExplicaciones.SelectionColor = Color.Cyan;

                for (int i = 0; i < indicespequeña.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicespequeña[i], indicespequeña[i + 1]);
                    rtbExplicaciones.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                }
                avance++;
                return;
            }
            else if (avance == 10) //Mostrar la forma simplificada
            {
                lbExplicacion.Text = " Simplificando: ";

                Polinomio resta = (factorcomun1.TerminosComunes * factorcomun1.TerminosComunes);
                resta.EliminarCeros();
                resta.Simplificar();
                resta.Ordenar();
           
                // Crear la formasiguiente
                formasiguiente = (formainicial - resta);
                formasiguiente.EliminarCeros();

                // Añadir la forma siguiente a la lista de formas
                formas.Add(formasiguiente);
                // Añadir el termino que representa a la forma
                cuadraticas.Add(forma1);

                // Escribir el termino que representa a la forma
                indicespequeña.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += "\nSimplificar.";
                indicespequeña.Add(rtbExplicaciones.Text.Length - indicespequeña[indicespequeña.Count - 1]);
                rtbExplicaciones.Text += "\n" + forma1.ToString() + " = ";
                // Escribir la nueva variable al cuadrado
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                rtbExplicaciones.Text += nuevavariable + "^2 ";
                indicesamarillos.Add(rtbExplicaciones.Text.Length);
                // Escribir la forma siguiente
                if (formasiguiente.Largo > 0)
                    rtbExplicaciones.Text += formasiguiente.ToString();

                // Cambiar los terminos pertenecientes al cuadrado de la suma en color verde amarillento
                for (int i = 0; i < indicesamarillos.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicesamarillos[i], indicesamarillos[i + 1] - indicesamarillos[i]);
                    rtbExplicaciones.SelectionColor = Color.GreenYellow;
                }
                for (int i = 0; i < indicespequeña.Count - 1; i += 2)
                {
                    rtbExplicaciones.Select(indicespequeña[i], indicespequeña[i + 1]);
                    rtbExplicaciones.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                }
                avance = 11;
                return;
            }
            
        }

         /// <summary>
        ///  
        /// REALIZA LA DIAGONALIZACION DE UNA FORMA CUADRATICA POR EL METODO DE GAUSS
        /// 
        /// </summary>

        private void ResolucionGauss()
        {
            if (avance == 0) // Mostrar la caja de texto para el orden de la matriz, y construirla
            {
                EtiquetaFilas.Show();
                EtiquetaFilas.Text = "Cantidad de filas y columnas de la matriz:";
                EtiquetaFilas.AutoSize = true;
                if (!directa) 
                lbExplicacion.Text = "Introducir enteros.\n\nBotón [E] para resolución de ejemplo con valores por defecto.";
                else if (directa)
                     lbExplicacion.Text = "Introducir enteros.";
                tbFilas.Show();
                tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Width + 5, tbFilas.Location.Y);
                tbFilas.Focus();
                if (!directa) 
                btDefecto.Show();
                btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
                EtiquetaFilas.Location = new Point(EtiquetaFilas.Location.X, lbExplicacion.Height + 5);
                tbFilas.Location = new Point(tbFilas.Location.X + 10, EtiquetaFilas.Location.Y);
                btDefecto.Location = new Point(btDefecto.Location.X, tbFilas.Location.Y);
                EtiquetaColumnas.Location = new Point(EtiquetaColumnas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 5);
                tbcolumnas.Location = new Point(tbFilas.Location.X, EtiquetaColumnas.Location.Y);
            }
            else if (avance == 1) // Comprobar si la matriz introducida es simetrica y si se puede diagonalizar por este metodo.Si lo es           
                                             // muestra la forma polinomica de la matriz introducida
            {
                lbExplicacion.Focus();
                contador = 0;
                contador2 = 0;
                bool essimetrica = Matematicas.AlgebraLineal.EsSimetrica(matrizracional);
              
                if (!essimetrica)
                {
                    if (!directa)
                    {
                        lbExplicacion.Text = "La matriz introducida no es simétrica. Vuelva al paso anterior para intentarlo de nuevo.";
                        ConstruirMatriz();
                    }
                    if (directa)
                    {
                        MessageBox.Show("La matriz introducida no es simétrica.");
                        tbFilas.ResetText();
                        avance = 0;
                        foreach (TextBox t in simetric)
                            t.Dispose();
                        simetric = null;
                        ResolucionGauss();
                        return;
                    }
                }
                    rotulosh = new List<Label>();
                    for (int i = 0; i < orden; i++)
                    {
                        rotulosh.Add(new Label());
                        Controls.Add(rotulosh[i]);
                        rotulosh[i].Show();
                        rotulosh[i].AutoSize = true;
                        rotulosh[i].BackColor = Color.Transparent;
                        rotulosh[i].Location = new Point(simetric[0, contador].Location.X, (simetric[0, contador].Location.Y - 20));
                        rotulosh[i].Font = new System.Drawing.Font("Dejavu-Sans", 12);
                        rotulosh[i].Text += (char)((int)'a' + contador);
                        contador++;
                    }
                    contador = 0;
                    rotulosv = new List<Label>();
                    for (int i = 0; i < orden; i++)
                    {
                        rotulosv.Add(new Label());
                        Controls.Add(rotulosv[i]);
                        rotulosv[i].Show();
                        rotulosv[i].AutoSize = true;
                        rotulosv[i].BackColor = Color.Transparent;
                        rotulosv[i].Location = new Point(simetric[0, 0].Location.X - 20, (simetric[contador, i].Location.Y));
                        rotulosv[i].Font = new System.Drawing.Font("Dejavu-Sans", 12);
                        rotulosv[i].Text += (char)((int)'a' + contador);
                        contador++;
                    }

                    if (!directa)
                    {
                        btContinuar.Show();
                        this.AcceptButton = btContinuar;
                        btContinuar.Location = new Point(simetric[orden - 1, orden - 1].Location.X, simetric[orden - 1, orden - 1].Location.Y + 50);
                    }
                        rtbExplicaciones.Show();
                        rtbExplicaciones.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + btContinuar.Height + 15);
                        rtbExplicaciones.Size = new Size(1200, this.Size.Height - (orden * 50));
                        rtbExplicaciones.BackColor = Color.SeaGreen;
                        rtbExplicaciones.Font = new Font("Dejavu Sans", 16);
                        rtbExplicaciones.BorderStyle = BorderStyle.None;

                        forma = Matematicas.AlgebraLineal.SimetricaAForma(matrizracional);
                        forma.EliminarCeros();
                        forma.Ordenar();
                        rtbExplicaciones.Text = "Matriz simetrica en forma polinomica: " + forma.ToString();

                        if (!directa)
                        {
                            lbExplicacion.Text = " El metodo de transformacion de una matriz simetrica en diagonal por medio de operaciones elementales, consiste en convertir todos los elementos por debajo y a la derecha de la diagonal principal en cero. Esto se lleva a cabo por medio de aplicar alguna de las operaciones elementales que pueden aplicarse a una matriz obteniendo una matriz equivalente ( cambiar una fila o columna por otra, sumar a una fila el resultado de multiplicar otra por un número etc.). ";
                            rtbExplicaciones.Focus();
                            rtbExplicaciones.Select(rtbExplicaciones.Text.Length - 1, 0);
                        }
                    avance++;
                    if (directa)
                    {
                        ResolucionGauss();
                        return;
                    }
            }
            else if (avance == 2)
            {
                contador = 0;
            
                // Ocultar la matriz original
                foreach (TextBox t in simetric)
                    t.Hide();
                foreach (Label l in rotulosh)
                    l.Hide();
                foreach (Label l in rotulosv)
                    l.Hide();
              
                // Hacer una copia de la matriz original para conservarla sin modificar para usarla en la prueba
                copia = Matematicas.AlgebraLineal.MatrizCopia(matrizracional);
                // Construir la matriz unidad en formato racional
                modalracional = new Racional[matrizracional.GetLength(0), matrizracional.GetLength(0)];
                for (int i = 0; i < matrizracional.GetLength(0); i++)
                {
                    for (int j = 0; j < matrizracional.GetLength(0); j++)
                    {
                        if (i == j)
                            modalracional[i, j] = 1;

                        else
                            modalracional[i, j] = 0;
                    }
                }
                if (directa)
                    lbExplicacion.Hide();
                // Matriz de etiquetas con la matriz copia de la original
                lbExplicacion.Text = "Para comenzar ponemos al lado de la matriz simétrica introducida, una matriz unidad del mismo orden.";
                Point origen = new Point(simetric[0, orden - 1].Location.X + 100, simetric[0, orden - 1].Location.Y);
                diagonal = new Label[orden, orden];
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        diagonal[i, j] = new Label();
                        diagonal[i, j].Text = simetric[i, j].Text;
                        diagonal[i, j].Location = new Point(origen.X + 60 * contador, origen.Y);
                        diagonal[i, j].Size = new Size(50, 20);
                        diagonal[i, j].Font = new Font("Dejavu-Sans", 10);
                        diagonal[i, j].BackColor = Color.White;
                        Controls.Add(diagonal[i, j]);
                        diagonal[i, j].Show();
                        diagonal[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        contador++;
                    }
                    origen.Y += 40;
                    contador = 0;
                }
                // Rotulo de la matriz copia de la original
                label1.Font = new Font("Dejavu-Sans", 9, FontStyle.Underline);
                label1.AutoSize = false;
                label1.Size = new Size(diagonal[0, orden - 1].Location.X - diagonal[0, 0].Location.X + diagonal[0, 0].Width, 20);
                label1.TextAlign = ContentAlignment.MiddleCenter;
                label1.Show();
                label1.Location = new Point(diagonal[0, 0].Location.X, diagonal[0, 0].Location.Y - 30);
                label1.Text = "Copia de la original";
                label1.BackColor = Color.SeaGreen;

                // Matriz de etiquetas para mostrar la matriz unidad
                origen = new Point(diagonal[0, orden - 1].Location.X + 100, diagonal[0, orden - 1].Location.Y);
                modal = new Label[orden, orden];
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        modal[i, j] = new Label();
                        modal[i, j].Text = Racional.AString(modalracional[i, j]);
                        modal[i, j].Location = new Point(origen.X + 60 * contador, origen.Y);
                        modal[i, j].Size = new Size(50, 20);
                        modal[i, j].Font = new Font("Dejavu-Sans", 10);
                        modal[i, j].BackColor = Color.White;
                        Controls.Add(modal[i, j]);
                        modal[i, j].Show();
                        modal[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        contador++;
                    }
                    origen.Y += 40;
                    contador = 0;
                }
                // Rotulo de la matriz unidad
                label2.Font = new Font("Dejavu-Sans", 9, FontStyle.Underline);
                label2.AutoSize = false;
                label2.Show();
                label2.Location = new Point(modal[0, 0].Location.X, label1.Location.Y);
                label2.Size = label1.Size;
                label2.TextAlign = ContentAlignment.MiddleCenter;
                label2.Text = "Matriz unidad";
                label2.BackColor = Color.SeaGreen;

                filaactual = contador + 1; // Fila o columna que se va a convertir en cero.
                contador = 0; // Para contar los elementos de la diagonal principal
                contador2 = 1; // Para contar los elementos por debajo y a la derecha de la diagonal principal  
                cocientes = new List<Racional>();
                label3.Text = "Cocientes = ";
                avance++;
                if (directa)
                {
                    ResolucionGauss();
                    return;
                }
            }
            if (avance > 2 && filaactual < orden && contador < orden && !diagonalizada) //Obtener los cocientes, multiplicarlos a la primera fila y restar el resultado a la fila correspondiente.
            {
                if (!directa)
                {
                    label3.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + 100);
                    label3.Show();
                    label3.BackColor = Color.SeaGreen;
                }
                if (avance == 3 || (avance > 3 && avance % 2 != 0) ) // Para hacer este paso una vez cada fila de la matriz
                {
                    bool esnulo = false;
                    if (Racional.StringToRacional(diagonal[contador + 1, contador].Text).Numerador == 0)
                        esnulo = true;
                    if (Racional.StringToRacional(diagonal[contador, contador].Text).Numerador == 0)
                        cocientecero = true;
                    else
                        cocientecero = false;
                    if (avance == 3 && !cocientecero )
                    {
                        if(!directa)
                        lbExplicacion.Text = " Comenzamos convirtiendo en cero todos los elementos por debajo y a la derecha del elemento número : " + (contador + 1) + " de la diagonal principal. Para ello, dividimos el elemento número: " + contador2 + "  por debajo de la diagonal pricipal por el elemento número: " + (contador + 1) + " de la diagonal principal, obteniendo un cociente;";
                    }
                    else if (avance > 3 && !cocientecero && !diagonalizada)
                    {
                        if (!directa)
                        lbExplicacion.Text = " Seguimos convirtiendo en cero todos los elementos por debajo y a la derecha del elemento número : " + (contador + 1) + " de la diagonal principal. Para ello, dividimos el elemento número: " + filaactual + "  por debajo de la diagonal pricipal por el elemento número: " + (contador + 1) + " de la diagonal principal, obteniendo un cociente;";
                    }
                    else if (cocientecero && !diagonalizada)
                    {
                        if (esnulo)
                        {
                            filaactual = orden - 1;
                            columnaactual = orden - 1;
                            contador = 0;
                            diagonalizada = true;
                            lbExplicacion.Text = "La matriz original se ha convertido en una matriz diagonal, y la matriz unidad en la matriz de paso.";
                            label2.Text = "Matriz de paso";
                            foreach (TextBox t in simetric)
                                t.Show();
                            label1.Text = "Matriz diagonal";
                            label3.Hide();
                            foreach (Label l in diagonal)
                                l.BackColor = Color.GreenYellow;
                            foreach (Label l in modal)
                                l.BackColor = Color.ForestGreen;
                            if (!directa)
                                return;
                            else if (directa)
                                ResolucionGauss();
                        }
               
                        if(!directa)
                        lbExplicacion.Text = "\nComo el elemento" + (contador + 1) + " es cero, sumamos a la fila " + (contador + 1) + " la fila " + (contador + 2) + " en ambas matrices. Después sumamos a la columna " + (contador + 1) + " la columna " + (contador + 2) + " solo en la matriz de la izquierda";
                      //  List<string> aux = new List<string>();
                        contador2 = 0;
                        if (!directa)
                        {
                            timer4.Start();
                            btContinuar.Hide();
                        }
                        else if (directa)
                        {
                            CambiarFilas();
                        }
                    }
                    if (!cocientecero)
                    {
                        cocientes.Add(Racional.StringToRacional(diagonal[filaactual, contador].Text) / Racional.StringToRacional(diagonal[contador, contador].Text));
                        diagonal[contador, contador].BackColor = Color.Coral;
                        diagonal[filaactual, contador].BackColor = Color.Coral;
                        label3.Text += Racional.AString(cocientes[cocientes.Count - 1]) + " ; ";
                    }
                }
                else if (!diagonalizada)
                {
                    if (!directa)
                    {
                        if (avance > (orden + 5 + (contador * orden)))
                            rtbExplicaciones.Show();
                        rtbExplicaciones.Location = new Point(label3.Location.X, label3.Location.Y + 50);
                        rtbExplicaciones.Text += "\n";
                    }
                    matrizmemo = new Racional[orden, orden];

                    if (!diagonalizada)
                    {
                        if (!directa)
                        {
                            lbExplicacion.Text = "El cociente obtenido se multiplica por la fila número: " + (contador + 1) + " y el resultado se resta a la fila número: " + (filaactual + 1);
                            btContinuar.Hide();
                            timer1.Start();
                        }
                        else if (directa)
                        {
                            RestarFilasDiagonal();
                        }
                            for (int i = 0; i < orden; i++)
                            {
                                for (int j = 0; j < orden; j++)
                                {
                                    matrizmemo[i, j] = Racional.StringToRacional(diagonal[i, j].Text);
                                }
                            }
                        
                        paso = avance;
                    }
                    filaactualunidad = contador + 1; // Para comenzar a actuar sobre las filas de la matriz unidad.
                }
                avance++;
                if (directa)
                {
                    ResolucionGauss();
                    return;
                }
               
            }
            else if (avance > 2 && (filaactual == orden && filaactualunidad < orden && contador < orden)) // Restar filas a la matriz unidad
            {
                if (!directa)
                {
                    if ((avance == (orden + (orden * contador) + 5)))
                        rtbExplicaciones.Text = "";
                    else
                        rtbExplicaciones.Text += "\n";

                    lbExplicacion.Text = "Multiplicamos cada uno de los cocientes anteriores a la fila número: " + (contador + 1) + " de la matriz unidad y el resultado se lo restamos a la fila número: " + (filaactualunidad + 1);
                    btContinuar.Hide();
                    timer2.Start();
                }
                else if (directa)
                {
                    RestarFilasModal();
                }
                columnaactual = contador + 1;
                if (directa)
                {
                    ResolucionGauss();
                }
            }
            else if (avance > 2 && (filaactualunidad == orden && columnaactual < orden && contador < orden)) // Restar columnas a la matriz diagonal
            {
                if (!directa)
                {
                    if (columnaactual == contador + 1)
                        rtbExplicaciones.Text = "";
                    else
                        rtbExplicaciones.Text += "\n";

                    lbExplicacion.Text = "Multiplicamos cada uno de los cocientes anteriores a la columna número: " + (contador + 1) + " de la matriz, y el resultado se lo restamos a la columna número: " + (filaactualunidad + 1) + "\nEsta operación sólo se realiza sobre la matriz a transformar en diagonal.";
                    timer3.Start();
                    btContinuar.Hide();
                }
                else if (directa)
                {
                    RestarColumnasDiagonal();
                    ResolucionGauss();
                }
            }
            else if (avance > 2 && (columnaactual == orden && contador < orden)) // Repetir el proceso para el siguiente elemento de la diagonal principal
            {
                contador++;
                contador2 = contador + 1;
                filaactual = contador + 1;
                columnaactual = contador + 1;
                filaactualunidad = contador + 1;
                cocientes.Clear();
                label3.Text = "Cocientes: ";
                rtbExplicaciones.Text = "";
                ResolucionGauss();
            }
            // Una vez convertida la matriz copia en diagonal
            else if (filaactual > orden && filaactualunidad > orden && columnaactual > orden && contador == orden && !diagonalizada)
            {
                if(!directa)
                lbExplicacion.Text = "La matriz original se ha convertido en una matriz diagonal, y la matriz unidad es la matriz de paso.\nPara obtener la matriz modal, hay que despejar las variables correspondientes a cada columna de la matriz de paso";
                label1.Text = "Matriz diagonal";
                label2.Text = "Matriz de paso";
                label3.Text = "";
                foreach (TextBox t in simetric)
                {
                    t.Show();
                }
                foreach (Label l in diagonal)
                {
                    l.BackColor = Color.GreenYellow;
                }
                foreach (Label l in modal)
                {
                    l.BackColor = Color.ForestGreen;
                }
                rotulosh[0].Show();
                rotulosh[0].Text = "Matriz original";
                rotulosh[0].Size = label1.Size;
                rotulosh[0].Location = new Point(simetric[0, 0].Location.X, label1.Location.Y);
                rotulosh[0].Font = new Font("Dejavu-Sans", 9, FontStyle.Underline);
                rotulosh[0].TextAlign = ContentAlignment.MiddleCenter;
                contador = 0;
                diagonalizada = true;
                if (directa)
                    ResolucionGauss();
            }
            else if (diagonalizada && contador == 0 && !despejada) // Construir el sistema de ecuaciones en base a la matriz de paso
            {
                if (!directa)
                {
                    if (rtbExplicaciones.Height > 500)
                        rtbExplicaciones.ScrollBars = RichTextBoxScrollBars.Vertical;

                    lbExplicacion.Text = "Para obtener la matriz modal, construimos un sistema de ecuaciones con cada columna de la matriz de paso: ";
                }
                    sistema = new Sistema(new List<Ecuacion>()); // Construir el sistema vacio
                char variable = 'a';
      
                // Añadir las ecuaciones al sistema
                for (int i = 0; i < orden; i++)
                {
                    char cont = (contador2.ToString())[0];
                    Termino ecu = new Termino(1, (char)((int)'P' + i), 1);
                    Ecuacion ecuacion = new Ecuacion(new List<Termino>(), new List<Termino>());
                    ecuacion.AñadirTerminoIzquierda(ecu);
                    for (int j = 0; j < orden; j++)
                    {
                        variable = (char)((int)'a' + j);
                        ecuacion.AñadirTerminoDerecha(new Termino(Racional.StringToRacional(modal[j, i].Text), variable, 1));
                    }
                    ecuacion.ObtenerLadoIzquierdo.EliminarCeros();
                    ecuacion.ObtenerLadoDerecho.EliminarCeros();
                    sistema.AñadirEcuacion(ecuacion);
                }
                if (!directa)
                {
                    for (int i = 0; i < orden; i++)
                    {
                        rtbExplicaciones.Text += sistema.ObtenerEcuacion(i).ToString() + "\n";
                    }
                }
                contador++;
                if (directa)
                    ResolucionGauss();
            }
            else if (diagonalizada && contador == 1 && !despejada) // Despejar cada variable a, b, c, etc cada una en una ecuacion del sistema
            {
                lbExplicacion.Text = "Despejamos cada variable en una de las ecuaciones del sistema: ";
                rtbExplicaciones.Text += "\n";
                for (int i = 0; i < orden; i++)
                {
                    sistema.ObtenerEcuacion(i).DespejarVariable((char)((int)'a' + i));
                    sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.EliminarCeros();
                    if(!directa)
                    rtbExplicaciones.Text += sistema.ObtenerEcuacion(i).ToString() + "\n";
                }
                contador++;
                iteraciones = 0;
                if (directa)
                    ResolucionGauss();
            }
            else if (diagonalizada && contador > 1 && !despejada) // Sustituir las variables despejadas hasta quedar una ecuacion
            {
                if (iteraciones < orden - 1)
                {
                    char asustituir = ' ';
                    lbExplicacion.Text = "Sustituimos la variable " + (char)((int)'a' + (orden - 1) - iteraciones) + " en todas las ecuaciones del sistema.";
                    Ecuacion sustituta = new Ecuacion(sistema.ObtenerEcuacion((orden - 1) - iteraciones)); // Para guardar la ecuacion que se va a sustituir en el sistema
                    asustituir = (char)((int)'a' + (orden - 1) - iteraciones);
                    sistema.Sustituir(sistema.ObtenerEcuacion((orden - 1) - iteraciones));

                    for (int i = 0; i < sistema.CantidadDeEcuaciones; i++) // Simplificar las ecuaciones y depejar de nuevo
                    {
                        foreach (Termino t in sistema.ObtenerEcuacion(i).ObtenerLadoIzquierdo.Terminos)
                        {
                            sistema.ObtenerEcuacion(i).AñadirTerminoDerecha(t * -1);
                        }
                        sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Simplificar();
                        sistema.ObtenerEcuacion(i).DespejarVariable((char)((int)'a' + i));
                        sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.EliminarCeros();
                    }
                    sistema.SustituirEcuacion(sustituta, (orden - 1) - iteraciones);

                    if (!directa)
                    {
                        rtbExplicaciones.Text += "\n" + sistema.ToString();
                        rtbExplicaciones.Select(rtbExplicaciones.Text.Length - 1, 0);
                    }
                }
          
                char adespejar = ' '; // Para almacenar la variable que queda por sustituir si se dá el caso
                despejada = true;
            
                for (int i = 0; i < sistema.CantidadDeEcuaciones; i++) // Comprobar que se han sustituido todas las variables en el lado derecho de las ecuaciones
                {
                    foreach (Termino t in sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Terminos)
                    {
                        if (t.Variables[0] > (char)90)
                        {
                            despejada = false;
                            adespejar = t.Variables[0];
                        }
                    }
                }

                if (despejada)
                    contador = 0;

                if (iteraciones >= orden - 1 && !despejada) // SI QUEDAN VARIABLES SIN SUSTITUIR EN EL LADO DERECHO
                {
                    lbExplicacion.Text = "Sustituimos la variable que falta, variable: " + adespejar + " , y simplificamos de nuevo.";
                 
                    // Determinar que variable falta por sustituir y en que ecuacion está
                    char sinsustituir = ' ';
                    int indiceasustituir = 0;
                    for (int i = 0; i < sistema.CantidadDeEcuaciones; i++)
                    {
                        foreach (Termino t in sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Terminos)
                        {
                            if (char.IsLower(t.Variables[0]))
                            {
                                sinsustituir = t.Variables[0];
                                indiceasustituir = i;
                            }
                        }
                    }
                    // Determinar el indice de la ecuacion que tiene en la izquierda la variable que falta por sustituir
                      int indicesustituta = 0;
                      for (int i = 0; i < sistema.CantidadDeEcuaciones; i++)
                      {
                          if (sistema.ObtenerEcuacion(i).ObtenerTerminoIzquierda(0).Variables[0] == sinsustituir)
                              indicesustituta = i;
                      }
                    // Guardar la variable del lado izquierdo para despejarla despues
                      char var = sistema.ObtenerEcuacion(indiceasustituir).ObtenerTerminoIzquierda(0).Variables[0]; 
                    // Sustituir la ecuacion que tiene la variable que falta por sustituir en su lado izquierdo, en la ecuacion que tiene
                    // la variable sin sustituir en el lado derecho
                      sistema.ObtenerEcuacion(indiceasustituir).Sustituir(sistema.ObtenerEcuacion(indicesustituta));
                      sistema.ObtenerEcuacion(indiceasustituir).ObtenerLadoDerecho.Simplificar();
                      sistema.ObtenerEcuacion(indiceasustituir).ObtenerLadoDerecho.EliminarCeros();
                    // Despejar la variable guardada en el lado izquierdo
                      sistema.ObtenerEcuacion(indiceasustituir).DespejarVariable(var);
                      sistema.ObtenerEcuacion(indiceasustituir).ObtenerLadoDerecho.EliminarCeros();
                      if (!directa)
                      {
                          rtbExplicaciones.Text += "\n" + sistema.ToString();
                          rtbExplicaciones.Select(rtbExplicaciones.Text.Length - 1, 0);
                      }
                    ResolucionGauss();
                }  
                iteraciones++;
                if (directa)
                    ResolucionGauss();
            }
            else if (diagonalizada && despejada && contador == 0) // Construir la matriz modal
            {
                foreach (Label l in modal)
                    l.Text = "0";
                lbExplicacion.Text = "Una vez despejadas las ecuaciones del sistema en terminos de las variables correspondientes a las columnas de la matriz de paso, construimos la matriz modal.\nCada una de las columnas de la matriz modal, se correponde a una de las ecuaciones del sistema: ";
                for (int i = 0; i < sistema.CantidadDeEcuaciones; i++)
                {
                    foreach (Termino t in sistema.ObtenerEcuacion(i).ObtenerLadoDerecho.Terminos)
                    {
                        modal[(int)(t.Variables[0] - 'P'), i].Text = Racional.AString(t.Coeficiente);
                    }
                }
                label2.Text = "Matriz modal";
                if (directa)
                {
                    lbExplicacion.Show();
                    lbExplicacion.Text = "[ Matriz original ] = [ Modal ] x [ Diagonal ] x [ Modal traspuesta]";
                    return;
                }
                contador++;
            }
            else if (diagonalizada && despejada && contador == 1 && !directa) // Realizar el primer paso de la prueba
            {
                rtbExplicaciones.Hide();
                lbExplicacion.Text = "Para probar que el resultado es correcto, se ha de cumplir la siguiente igualdad:\n [ Matriz original ] = [ Modal ] x [ Diagonal ] x [ Modal traspuesta]";
               // Mostrar el rotulo de la matriz de comprobacion
                label4.Show();
                label4.Location = new Point(diagonal[0, 0].Location.X, btContinuar.Location.Y + 50);
                label4.Font = new Font("Dejavu-Sans", 9, FontStyle.Underline);
                label4.Text = "Diagonal x Modal traspuesta";
                label4.AutoSize = false;
                label4.Size = label1.Size;
                label4.BackColor = Color.SeaGreen;
                label4.TextAlign = ContentAlignment.MiddleCenter;
                // Crear las matrices diagonal y modal en formato racional para realizar las operaciones
                modalracional = new Racional[orden, orden];
                matrizdiagonal = new Racional[orden, orden];
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        modalracional[i, j] = Racional.StringToRacional(modal[i, j].Text);
                    }
                }
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        matrizdiagonal[i, j] = Racional.StringToRacional(diagonal[i, j].Text);
                    }
                }
                // Crear la traspuesta de la matriz modal en formato racional
                modaltraspuesta = Matematicas.AlgebraLineal.Traspuesta(modalracional);
                matrizracional = Matematicas.AlgebraLineal.MultiplicarMatrices(matrizdiagonal, modaltraspuesta);
                // Crear la matriz de etiquetas para mostrar la matriz de comprobacion
                comprobacion = new Label[orden, orden];
                Point origen = new Point(label4.Location.X, label4.Location.Y + 25);
                contador2 = 0;
                int colum = 0;
                label3.Hide();
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        comprobacion[i, j] = new Label();
                        comprobacion[i, j].Size = new Size(50, 20);
                        comprobacion[i, j].BackColor = Color.Orange;
                        comprobacion[i, j].Text = Racional.AString(matrizracional[i, j]);
                        comprobacion[i, j].Show();
                        comprobacion[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        Controls.Add(comprobacion[i, j]);
                        comprobacion[i, j].Location = new Point(origen.X + (70 * contador2), origen.Y + (30 * colum));
                        contador2++;
                    }
                    contador2 = 0;
                    colum++;
                }

                contador++;
            }
            else if (diagonalizada && despejada && contador == 2) // Terminar la prueba
            {
                label4.Text = "Modal x Resultado anterior";
                matrizracional = Matematicas.AlgebraLineal.MultiplicarMatrices(modalracional, matrizracional);
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        comprobacion[i, j].Text = Racional.AString(matrizracional[i, j]);
                        comprobacion[i, j].BackColor = Color.LightSeaGreen;
                    }
                }
                btContinuar.Hide();
                lbExplicacion.Focus();
                lbExplicacion.Text += "\nSe comprueba que la matriz diagonal es una matriz semejante a la matriz original. Una de las aplicaciones prácticas de haber obtenido las matrices diagonal y modal, es el facilitar elevar la matriz a una potencia, sobre todo si la potencia es elevada.";

            }

        }



        // PARA REALIZAR LAS RESTAS SOBRE LAS FILAS DE LA MATRIZ DIAGONAL
        private void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (Label l in diagonal)
                l.BackColor = Color.White;
            if (contador2 <= orden)
            {
                Racional resul = Racional.StringToRacional(diagonal[filaactual, contador2 - 1].Text) - (Racional.StringToRacional(diagonal[contador, contador2 - 1].Text) * cocientes[cocientes.Count - 1]);
                rtbExplicaciones.Text += diagonal[filaactual, contador2 - 1].Text + " - (" + (diagonal[contador, contador2 - 1].Text) + " x " + Racional.AString(cocientes[filaactual - contador - 1]) + ") = " + Racional.AString(resul) + "\n";
                rtbExplicaciones.Select(rtbExplicaciones.Text.Length - 1, 0);
                diagonal[contador, contador2 - 1].BackColor = Color.GreenYellow;
                diagonal[filaactual, contador2 - 1].BackColor = Color.GreenYellow;
                diagonal[filaactual, contador2 - 1].Text = Racional.AString(resul);
                contador2++;
            }
            else
            {
                timer1.Stop();
                btContinuar.Show();
                filaactual++;
                contador2 = 1;
            }
        }

        // PARA REALIZAR LAS RESTAS SOBRE LAS FILAS DE LA MATRIZ DIAGONAL EN LA RESOLUCION DIRECTA
        private void RestarFilasDiagonal()
        {
            while (true)
            {
                if (contador2 <= orden)
                {
                    Racional resul = Racional.StringToRacional(diagonal[filaactual, contador2 - 1].Text) - (Racional.StringToRacional(diagonal[contador, contador2 - 1].Text) * cocientes[cocientes.Count - 1]);
                    diagonal[contador, contador2 - 1].BackColor = Color.GreenYellow;
                    diagonal[filaactual, contador2 - 1].BackColor = Color.GreenYellow;
                    diagonal[filaactual, contador2 - 1].Text = Racional.AString(resul);
                    contador2++;
                }
                else
                {
                    filaactual++;
                    contador2 = 1;
                    break;
                }
            }
        }

        // PARA REALIZAR LAS RESTAS SOBRE LAS FILAS DE LA MATRIZ MODAL
        private void Timer2_Tick(object sender, EventArgs e)
        {
            foreach (Label l in modal)
                l.BackColor = Color.White;
            if (contador2 <= orden)
            {
                Racional resul = Racional.StringToRacional(modal[filaactualunidad, contador2 - 1].Text) - (Racional.StringToRacional(modal[contador, contador2 - 1].Text) * cocientes[filaactualunidad - contador - 1]);
                rtbExplicaciones.Text += modal[filaactualunidad, contador2 - 1].Text + " - (" + (modal[contador, contador2 - 1].Text) + " x " + Racional.AString(cocientes[filaactualunidad - 1 - contador]) + ") = " + Racional.AString(resul) + "\n";
                rtbExplicaciones.Select(rtbExplicaciones.Text.Length - 1, 0);
                modal[contador, contador2 - 1].BackColor = Color.GreenYellow;
                modal[filaactualunidad, contador2 - 1].BackColor = Color.GreenYellow;
                modal[filaactualunidad, contador2 - 1].Text = Racional.AString(resul);
                contador2++;
            }
            else
            {
                timer2.Stop();
                btContinuar.Show();
                contador2 = 1;
                filaactualunidad++;
            }
        }

        // PARA REALIZAR LAS RESTAS SOBRE LAS FILAS DE LA MATRIZ MODAL EN LA RESOLUCION DIRECTA
        private void RestarFilasModal()
        {
            while (true)
            {
                if (contador2 <= orden)
                {
                    Racional resul = Racional.StringToRacional(modal[filaactualunidad, contador2 - 1].Text) - (Racional.StringToRacional(modal[contador, contador2 - 1].Text) * cocientes[filaactualunidad - contador - 1]);
                    rtbExplicaciones.Text += modal[filaactualunidad, contador2 - 1].Text + " - (" + (modal[contador, contador2 - 1].Text) + " x " + Racional.AString(cocientes[filaactualunidad - 1 - contador]) + ") = " + Racional.AString(resul) + "\n";
                    rtbExplicaciones.Select(rtbExplicaciones.Text.Length - 1, 0);
                    modal[contador, contador2 - 1].BackColor = Color.GreenYellow;
                    modal[filaactualunidad, contador2 - 1].BackColor = Color.GreenYellow;
                    modal[filaactualunidad, contador2 - 1].Text = Racional.AString(resul);
                    contador2++;
                }
                else
                {
                    contador2 = 1;
                    filaactualunidad++;
                    break;
                }
            }
        }



        // PARA REALIZAR LAS RESTAS SOBRE LAS COLUMNAS DE LA MATRIZ DIAGONAL
        private void Timer3_Tick(object sender, EventArgs e)
        {
            foreach (Label l in diagonal)
                l.BackColor = Color.White;
            if (contador2 <= orden)
            {
                Racional resul = Racional.StringToRacional(diagonal[contador2 - 1, columnaactual].Text) - (Racional.StringToRacional(diagonal[contador2 - 1, contador].Text) * cocientes[columnaactual - 1 - contador]);
                rtbExplicaciones.Text += diagonal[contador2 - 1, columnaactual].Text + " - (" + (diagonal[contador2 - 1, contador].Text) + " x " + Racional.AString(cocientes[columnaactual - 1 - contador]) + ") = " + Racional.AString(resul) + "\n";
                rtbExplicaciones.Select(rtbExplicaciones.Text.Length - 1, 0);
                diagonal[contador2 - 1, contador].BackColor = Color.GreenYellow;
                diagonal[contador2 - 1, columnaactual].BackColor = Color.GreenYellow;
                diagonal[contador2 - 1, columnaactual].Text = Racional.AString(resul);
                contador2++;
            }
            else
            {
                timer3.Stop();
                btContinuar.Show();
                contador2 = 1;
                columnaactual++;
            }
        }

        // PARA REALIZAR LAS RESTAS SOBRE LAS COLUMNAS DE LA MATRIZ DIAGONAL EN LA RESOLUCION DIRECTA
        private void RestarColumnasDiagonal()
        {
            while (true)
            {
                if (contador2 <= orden)
                {
                    Racional resul = Racional.StringToRacional(diagonal[contador2 - 1, columnaactual].Text) - (Racional.StringToRacional(diagonal[contador2 - 1, contador].Text) * cocientes[columnaactual - 1 - contador]);
                    rtbExplicaciones.Text += diagonal[contador2 - 1, columnaactual].Text + " - (" + (diagonal[contador2 - 1, contador].Text) + " x " + Racional.AString(cocientes[columnaactual - 1 - contador]) + ") = " + Racional.AString(resul) + "\n";
                    rtbExplicaciones.Select(rtbExplicaciones.Text.Length - 1, 0);
                    diagonal[contador2 - 1, contador].BackColor = Color.GreenYellow;
                    diagonal[contador2 - 1, columnaactual].BackColor = Color.GreenYellow;
                    diagonal[contador2 - 1, columnaactual].Text = Racional.AString(resul);
                    contador2++;
                }
                else
                {
                    contador2 = 1;
                    columnaactual++;
                    break;
                }
            }
        }

        // PARA REALIZAR LAS SUMAS DE COLUMNAS SI EL ELEMENTO DE LA DIAGONAL PRINCIPAL ES CERO
        private void Timer4_Tick(object sender, EventArgs e)
        {
            foreach (Label l in diagonal)
                l.BackColor = Color.White;
            foreach (Label l in modal)
                l.BackColor = Color.White;
            if (contador2 < orden) // Sumar a la fila donde esta esta el elemento nulo de la diagonal principal, la fila inferior en la matriz diagonal
            {
                Racional suma = Racional.StringToRacional(diagonal[contador, contador2].Text) + Racional.StringToRacional(diagonal[contador+1, contador2 ].Text);
                diagonal[contador, contador2].BackColor = Color.GreenYellow;
                diagonal[contador+1, contador2].BackColor = Color.GreenYellow;
                diagonal[contador, contador2].Text = Racional.AString(suma);
                contador2++;
            }
            else if (contador2 >= orden && contador2-orden < orden) // Sumar a la misma fila de la matriz modal que la sumada anteriormente en la matriz diagonal, la fila inferior
            {
                Racional suma = Racional.StringToRacional(modal[contador, contador2-orden].Text) + Racional.StringToRacional(modal[contador +1, contador2 - orden].Text);
                modal[contador, contador2-orden].BackColor = Color.GreenYellow;
                modal[contador+1, contador2-orden].BackColor = Color.GreenYellow;
                modal[contador, contador2-orden].Text = Racional.AString(suma);
                contador2++;
            }
            else if (contador2 > orden && contador2 - (orden * 2) < orden) // Sumar a la columna donde está el elemento nulo de la diagonal principal, la columna de la derecha
            {
                Racional suma = Racional.StringToRacional(diagonal[contador2 - (orden * 2), contador].Text) + Racional.StringToRacional(diagonal[contador2 - (orden * 2), contador + 1].Text);
                diagonal[contador2 - (orden * 2), contador].BackColor = Color.GreenYellow;
                diagonal[contador2 - (orden * 2), contador + 1].BackColor = Color.GreenYellow;
                diagonal[contador2 - (orden * 2), contador].Text = Racional.AString(suma);
                contador2++;
            }
            else
            {
                timer4.Stop();
                btContinuar.Show();
                contador2 = 1;
                cocientes.Add(Racional.StringToRacional(diagonal[contador + 1, contador].Text) / Racional.StringToRacional(diagonal[contador, contador].Text));
                label3.Text += Racional.AString(cocientes[0]) + " ; ";
            }
        }

        // PARA REALIZAR LAS SUMAS DE COLUMNAS SI EL ELEMENTO DE LA DIAGONAL PRINCIPAL ES CERO EN LA 
        // RESOLUCION DIRECTA
        private void CambiarFilas()
        {
            while (true)
            {
                if (contador2 < orden) // Sumar a la fila donde esta esta el elemento nulo de la diagonal principal, la fila inferior en la matriz diagonal
                {
                    Racional suma = Racional.StringToRacional(diagonal[contador, contador2].Text) + Racional.StringToRacional(diagonal[contador + 1, contador2].Text);
                    diagonal[contador, contador2].Text = Racional.AString(suma);
                    contador2++;
                }
                else if (contador2 >= orden && contador2 - orden < orden) // Sumar a la misma fila de la matriz modal que la sumada anteriormente en la matriz diagonal, la fila inferior
                {
                    Racional suma = Racional.StringToRacional(modal[contador, contador2 - orden].Text) + Racional.StringToRacional(modal[contador + 1, contador2 - orden].Text);;
                    modal[contador, contador2 - orden].Text = Racional.AString(suma);
                    contador2++;
                }
                else if (contador2 > orden && contador2 - (orden * 2) < orden) // Sumar a la columna donde está el elemento nulo de la diagonal principal, la columna de la derecha
                {
                    Racional suma = Racional.StringToRacional(diagonal[contador2 - (orden * 2), contador].Text) + Racional.StringToRacional(diagonal[contador2 - (orden * 2), contador + 1].Text);
                    diagonal[contador2 - (orden * 2), contador].Text = Racional.AString(suma);
                    contador2++;
                }
                else
                {
                    contador2 = 1;
                    cocientes.Add(Racional.StringToRacional(diagonal[contador + 1, contador].Text) / Racional.StringToRacional(diagonal[contador, contador].Text));
                    break;
                }
            }
        }

    }
}

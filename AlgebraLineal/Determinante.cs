using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Matematicas;


namespace AlgebraLineal
{
    class Determinante:FormularioBase
    {
        bool diagonales = false; // Sera true si se selecciona la resolucion por el metodo de las diagonales
        bool menores = false; // Sera true si se selecciona la resolucion por el metodo de las menores principales
        bool gauss = false; // Sera true si se selecciona la resolucion por el metodo de Gauss
       
        private Racional producto = new Racional(1, 1);
        private List<Racional> productos = new List<Racional>();
        private List<Racional> productosizquierda = new List<Racional>();

        TextBox[,] copiamatriz; // Copia de la matriz original en el metodo de Gauss

        private int columnaaconvertir = 0;
        private int filaarestar = 1;
        private Racional determinantemenor; // Será el determinante de cada menor principal en la resolucion por el metodo de las menores principales

        public Determinante(bool direct)
        {
           directa = direct;
        }

        public override void Cargar(object sender, EventArgs e)
        {
	    this.MinimumSize = new Size(1300,700);
            this.Text = "Determinante de una matriz cuadrada.";
            lbExplicacion.Show();
            lbExplicacion.Text = "Introducir un número entero.\n\n(Pulsar el botón [E] para ejemplo con valores por defecto.)";
            EtiquetaFilas.Show();
            EtiquetaFilas.AutoSize = true;
            EtiquetaFilas.Location = new Point(EtiquetaFilas.Location.X, lbExplicacion.Location.Y + 120);
            EtiquetaFilas.Text = "Cantidad de filas y columnas de la matriz: ";
            tbFilas.Show();
            tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Size.Width + 10, EtiquetaFilas.Location.Y);
            tbFilas.TextAlign = HorizontalAlignment.Center;
            btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Size.Width + 5, EtiquetaFilas.Location.Y);
            tbFilas.Focus();
            tbFilas.KeyPress += tbOrden_KeyPress;
            btDefecto.Click += btDefecto_Click;
            btContinuar.Click += btContinuar_Click;
            btNuevo.Click += btNuevo_Click;
            radioButton1.Click += rbSeleccion_Click;
            radioButton2.Click += rbSeleccion_Click;
            radioButton3.Click += rbSeleccion_Click;
            Temporizador1.Tick += Temporizador1_Tick;
            Temporizador1.Interval = 1000;
        }

        /// <summary>
        ///  
        /// CIERRA EL FORMULARIO ACTUAL Y ABRE UN NUEVO MENU DE ALGEBRA LINEAL
        /// 
        /// </summary>

        public override void btSalir_Click(object sender, EventArgs e)
        {
            MenuMatrices menuvectores = new MenuMatrices();
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
        private new void btDefecto_Click(object sender, EventArgs e)
        {
            btDefecto.Hide();
            tbFilas.Text = "3";
            orden = 3;
            ConstruirMatriz();
            matrizracional = new Racional[orden, orden];
            matriz[0, 0].Text = "5"; matrizracional[0, 0] = 5;
            matriz[0, 1].Text = "8"; matrizracional[0, 1] = 8;
            matriz[0, 2].Text = "7"; matrizracional[0, 2] = 7;
            matriz[1, 0].Text = "4"; matrizracional[1, 0] = 4;
            matriz[1, 1].Text = "6"; matrizracional[1, 1] = 6;
            matriz[1, 2].Text = "2"; matrizracional[1, 2] = 2;
            matriz[2, 0].Text = "7"; matrizracional[2, 0] = 7;
            matriz[2, 1].Text = "8"; matrizracional[2, 1] = 8;
            matriz[2, 2].Text = "1"; matrizracional[2, 2] = 1;
            if (!directa)
                SeleccionarResolucion();
            else if (directa)
                MostrarResultado();
           
        }

        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            this.Text = "Deteminante de una matriz cuadrada.";
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            diagonales = false;
            menores = false;
            gauss = false;
            if (copiamatriz != null)
            {
                foreach (TextBox t in copiamatriz)
                    t.Dispose();
            }
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label6.Hide();
            label7.Hide();
            label5.Hide();
            lbExplicacion.Show();
            lbExplicacion.Text = "Introducir un número entero.\n\n(Pulsar el botón [E] para ejemplo con valores por defecto.)";
            btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Size.Width + 5, EtiquetaFilas.Location.Y);
            if (directa)
            {
                EtiquetaFilas.Text = "Cantidad de filas y columnas de la matriz: ";
                tbFilas.BackColor = Color.White;
            }
            producto = new Racional(1, 1);
            productos = new List<Racional>();
            productosizquierda = new List<Racional>();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            EtiquetaFilas.Show();
            tbFilas.Show();
            btDefecto.Show();
            orden = 0;
            paso = 0;
            paso = 0;
            btContinuar.Hide();
            label3.Text = "Total derecha: ";
            label4.Text = "Total izquierda: ";
            tbFilas.ResetText();
            tbFilas.Focus();
            tbFilas.Size = new Size(60, 26);
            if (matriz != null)
            {
                foreach (TextBox t in matriz)
                    t.Dispose();
            }
            filaactual = 0;
            columnaactual = 0;
            columnaaconvertir = 0;
            filaarestar = 1;
        }
        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUZCA UN NUMERO ENTERO DISTINTO DE CERO EN LA CAJA DE TEXTO PARA
        /// EL ORDEN DE LA MATRIZ Y LLAMA AL MÉTODO ConstruirMatriz
        /// 
        /// </summary>
       
        private void tbOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
             TextBox aux = (TextBox)sender;
            if (e.KeyChar == Convert.ToChar(13)) // Si se pulsa la tecla intro
            {
                if (aux.Text.Length > 0)
                {
                    e.Handled = true;
                    orden = Int32.Parse(tbFilas.Text);
                    btDefecto.Hide();
                    lbExplicacion.Text = "Introduzca enteros o racionales.\n\n( Por ejemplo: 3 ; 1/5 ; -7 ; -9/5 ).";
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
        ///  COMPRUEBA QUE LOS DATOS INTRODUCIDO EN CADA TextBox EN matriz SEAN
        ///  ADECUADOS PARA PASARLOS A Racional MAS TARDE
        ///  Y CREA LA MATRIZ EN FORMATO Racional Y LA RELLENA CON DATOS DE matriz
        ///  PASADOS A Racional
        /// </summary>

        private void Matriz_KeyPress(object sender, KeyPressEventArgs e)
        {
            matrizracional = new Racional[matriz.GetLength(0), matriz.GetLength(1)];
            TextBox aux = (TextBox)sender;
            if (e.KeyChar == Convert.ToChar(13)) // Si se pulsa la tecla intro
            {
                if (aux.Text.Length > 0) // Si la caja de texto no está vacia.
                {
                    filaactual = (aux.TabIndex) / matriz.GetLength(0); // primer indice de la caja actual
                    columnaactual = (aux.TabIndex) - (matriz.GetLength(0) * filaactual); // segundo indice de la caja actual
                    if (columnaactual == matriz.GetLength(1) - 1) // Si es la ultima caja de la fila
                    {
                        if (aux.TabIndex < ((matriz.GetLength(0)) * (matriz.GetLength(0)))) // Si no es la ultima caja de la matriz.
                        {
                            columnaactual = 0;
                            filaactual++;
                        }
                        else // Si es la ultima caja de la matriz rellenar la matriz en formato Racional
                        {
                            e.Handled = true;
                            for (int i = 0; i < matriz.GetLength(0); i++)
                            {
                                for (int j = 0; j < matriz.GetLength(1); j++)
                                {
                                    matrizracional[i, j] = Racional.StringToRacional(matriz[i, j].Text);
                                    // lbMensajes.Text = Racional.AString(matrizracional[i,j]) + "  ";
                                }
                            }

                            if (!directa)
                            {
                                SeleccionarResolucion();
                            }
                            else if (directa)
                            {
                                MostrarResultado();
                                lbExplicacion.TabIndex = 0;
                                lbExplicacion.Focus();
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
                        matriz[filaactual, columnaactual].Focus();
                    }
                    catch (IndexOutOfRangeException) // Rellenar la matriz Racional 
                    {
                        e.Handled = true;
                        for (int i = 0; i < matriz.GetLength(0); i++)
                        {
                            for (int j = 0; j < matriz.GetLength(1); j++)
                            {
                                matrizracional[i, j] = Racional.StringToRacional(matriz[i, j].Text);
                            }
                        }
                        if (!directa)
                        {
                            SeleccionarResolucion();
                        }
                        else if (directa)
                        {
                            MostrarResultado();
                            lbExplicacion.TabIndex = 0;
                            lbExplicacion.Focus();
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

            else if (e.KeyChar == '/') // Solo puede haber un caracter 
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

            else if (e.KeyChar == '+' || e.KeyChar == '-') // Asegurar que los signos + o - esten en la primera posicion.
            {
                if (aux.SelectionLength == aux.Text.Length) // Si todo el texto de la caja está seleccionado, es decir estamos en el primer caracter
                    e.Handled = false;
                else
                    e.Handled = true;
            }

            else if (e.KeyChar < '0' || e.KeyChar > '9') // Asegurar que se introduzcan digitos y no otro tipo de caracteres
                e.Handled = true;

        }
        /// <summary>
        ///  
        ///   LLAMA AL METODO DE RESOLUCION PASO A PASO O AL QUE MUESTRA EL DETERMINANTE EN PANTALLA
        ///   SEGUN EL TIPO DE RESOLUCION ELEGIDO ANTERIORMENTE POR EL USUARIO
        ///   
        /// </summary>

        private void SeleccionarResolucion()
        {
            if (matriz.GetLength(0) <= 3)
                radioButton1.Show();
                radioButton1.Text = "Método de las diagonales";
                radioButton1.Size = new Size(15, 15);
                radioButton1.Font = new Font("Dejavu Sans", 12);
                radioButton1.Location = new Point(tbFilas.Location.X + tbFilas.Size.Width + 30, tbFilas.Location.Y);
          
                radioButton2.Show();
                radioButton2.Size = radioButton1.Size;
                radioButton2.Font = radioButton1.Font;
                radioButton2.Location = new Point(radioButton1.Location.X, radioButton1.Location.Y + radioButton1.Size.Height + 10);
                radioButton2.Text = "Método de las menores principales";

                radioButton3.Show();
                radioButton3.Size = radioButton1.Size;
                radioButton3.Font = radioButton1.Font;
                radioButton3.Location = new Point(radioButton2.Location.X, radioButton2.Location.Y + radioButton2.Size.Height + 10);
                radioButton3.Text ="Método de gauss";
                lbExplicacion.TabIndex = 0;
                lbExplicacion.Focus();
        }
        /// <summary>
        ///  
        ///  INICIA EL METODO DE RESOLUCION PASO A PASO POR EL METODO DE LAS DIAGONALES
        ///   
        /// </summary>

        private void rbSeleccion_Click(object sender, EventArgs e)
        {
            if (sender == radioButton1)
            {
                this.Text += "  ( Método de las diagonales )";
                diagonales = true;
                btContinuar.Show();
                paso = 0;
                btContinuar.Location = new Point(900, 200);
                radioButton1.Hide();
                radioButton2.Hide();
                radioButton3.Hide();
                EtiquetaFilas.Hide();
                tbFilas.Hide();
                lbExplicacion.Focus();

                lbExplicacion.Text = " Primero hacemos el producto de los elementos de la diagonal principal y guardamos el resultado.";

                label1.Text = "";
                label1.Show(); // Etiqueta con el planteamiento
                label1.Location = new Point(matriz[matriz.GetLength(0) - 1, 0].Location.X, (matriz[matriz.GetLength(1) - 1, 0].Location.Y + matriz[0, 0].Height + 10));
                label1.BackColor = Color.SeaGreen;
                label1.ForeColor = Color.Silver;
                label1.TextAlign = ContentAlignment.MiddleLeft;

                string planteamiento = PintarDiagonal(matriz, 0, Color.GreenYellow, ref producto);
                label1.Text = planteamiento + " " + Racional.AString(producto);

                label2.Show(); // Etiqueta para Memo
                label2.Text = "Memo: ";
                label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 10);
                label2.Text += ":   " + Racional.AString(producto);
                label2.ForeColor = label1.ForeColor;
                label2.TextAlign = ContentAlignment.MiddleLeft;
                label2.BackColor = label1.BackColor;

                productos.Add(producto);
                paso++;
            }
            else if (sender == radioButton2)
            {
                this.Text += "  ( Método de las menores principales. )";
                menores = true;
                Resolucion();
            }
             else if (sender == radioButton3)
            {
                this.Text += "  ( Método de Gauss. )";
                gauss = true;
                Resolucion();
            }
        }
        /// <summary>
        ///  
        ///  CAMBIA EL CONTENIDO Y EL NOMBRE DE LA ETIQUETA PARA EL ORDEN DE LA MATRIZ, Y MUESTRA EL 
        ///  VALOR DEL DETERMINANTE, EN LA CAJA DE TEXTO DONDE SE INTRODUJO EL ORDEN EL DE LA MISMA.
        ///   
        /// </summary>

        internal void MostrarResultado()
        {
            EtiquetaFilas.Text = " El determinante de la matriz introducida es: ";
            Racional resultado = Matematicas.AlgebraLineal.Determinante(matrizracional);
            tbFilas.Size = new Size(Racional.AString(resultado).Length * 15, tbFilas.Size.Height);
           tbFilas.Text = Racional.AString(resultado);
            tbFilas.BackColor = Color.GreenYellow;
            lbExplicacion.Text = "";
            EtiquetaFilas.Focus();
        }
        /// <summary>
        /// 
        /// LLAMA AL METODO FinalizarResolucion EL CUAL EJECUTA UN BLOQUE DE CODIGO
        /// SEGUN EN QUE PASO SE ENCUENTRE LA RESOLUCION PASO A PASO
        /// 
        /// </summary>
        /// 
        private void btContinuar_Click(object sender, EventArgs e)
        {
            Resolucion();
            paso++;
             
        }

        /// <summary>
        /// 
        ///  MUESTRA EN PANTALLA LA MATRIZ DE CAJAS DE TEXTO DEL ORDEN INTRODUCIDO, DONDE SE
        ///  INTRODUCIRAN LOS VALORES Racional DE LA MATRIZ CUYO DETERMINANTE SE QUIERE CALCULAR
        /// 
        /// </summary>

        internal void ConstruirMatriz()
        {
            int indicetabulador = 0;
            matriz = new TextBox[orden, orden];
            descontador = orden - 1;
            Point origen = new Point(150, 200);
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    matriz[i, j] = new TextBox();
                    matriz[i, j].Size = new Size(50, 20);
                    matriz[i, j].Location = origen;
                    matriz[i, j].TabIndex = indicetabulador;
                    matriz[i, j].TextAlign = HorizontalAlignment.Center;
                    matriz[i, j].KeyPress += Matriz_KeyPress;
                    Controls.Add(matriz[i, j]);
                    origen.X += 60;
                    indicetabulador++;
                }
                origen.X = 150;
                origen.Y += 30;
            }
            matriz[0, 0].Focus();
        }

        /// <summary>
        ///  
        ///  PINTA EL FONDO DE LAS CAJAS DE TEXTO QUE ESTAN EN LA DIAGONAL A DERECHA
        ///  CUYO INDICE ES PASADO COMO ARGUMENTO. LA MATRIZ ES PASADA COMO ARGUMENTO
        ///  Y DEVUELVE EL STRING DEL PLATEAMIENTO DEL PRODUCTO DE ESOS ELEMENTOS
        ///  Y EL RESULTADO DEL PRODUCTO COMO REFERENCIA
        ///     
        /// </summary>


        private string PintarDiagonal(TextBox[,] matriz, int indice, Color color, ref Racional producto)
        {
            int fila = 0;
            string planteamiento = "";
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (indice > matriz.GetLength(0) - 1)
                    indice = 0;

                matriz[fila, indice].BackColor = Color.GreenYellow;
                producto *= Racional.StringToRacional(matriz[fila, indice].Text);
                planteamiento += matriz[fila, indice].Text + " * ";
                indice++;
                fila++;
            }
            planteamiento = planteamiento.Substring(0, planteamiento.Length - 2);
            planteamiento += " = ";
            return planteamiento;
        }



        /// <summary>
        ///  
        ///  LLEVA A CABO LA EXPLICACION PASO A PASO DEL METODO DE RESOLUCION ELEGIDO ANTERIORMENTE
        ///     
        /// </summary>

        private void Resolucion()
        {
            if (diagonales) // Resolucion por el metodo de las diagonales
            {
                label3.Location = new Point(label2.Location.X, label2.Location.Y + label2.Size.Height + 10);
                label3.BackColor = label1.BackColor;
                label4.Location = new Point(label3.Location.X, label3.Location.Y + label3.Height + 10);
                label3.AutoSize = true;
                label4.Location = new Point(label3.Location.X, label3.Location.Y + label3.Height + 10);

                bool ordendos = (matriz.GetLength(0) == 2); // Si la matriz es de orden dos

                lbExplicacion.Text = " Realizamos la misma operacion con todas las diagonales a la derecha.";

                producto = new Racional(1, 1);
                if ((!ordendos & paso < matriz.GetLength(0)) || (ordendos & paso == 0))  // Mientras no se llegue a todas las diagonales a la derecha 
                {
                    DespintarCajas();
                    string planteamiento = PintarDiagonal(matriz, paso, Color.GreenYellow, ref producto);
                    label1.Text = planteamiento + " " + Racional.AString(producto);

                    label2.Show();
                    if (producto.Numerador >= 0)
                        label2.Text += " + ";
                    if (paso == matriz.GetLength(0))
                        label2.Text += " = ";

                    label2.Text += Racional.AString(producto);

                    productos.Add(producto);
                }
                else if ((!ordendos && paso == orden) || (ordendos && paso == 1))// Cuando se hayan realizado todas las diagonales a la derecha
                {
                    Racional totalderecha = 0;
                    foreach (Racional r in productos)
                    {
                        totalderecha += r;
                    }
                    label3.Show(); // Etiqueta para el resultado de las diagonales a la derecha
                    label3.Text = "Total derecha: " + Racional.AString(totalderecha);
                    label3.Location = new Point(label2.Location.X, label2.Location.Y + label2.Size.Height + 10);
                    label3.BackColor = label1.BackColor;

                    lbExplicacion.Text = " Una vez realizado el producto de todas las diagonales a la derecha, los sumamos y procedemos a realizar el producto de los elementos de las diagonales a la izquierda.";
                }
                else // Productos de las diagonales a la izquierda
                {
                    if ((!ordendos && descontador >= 0) || (ordendos && paso == 2))
                    {
                        if (descontador == orden - 1)
                            lbExplicacion.Text = "Comenzamos con el producto de la primera diagonal a la izquierda.";
                        else
                            lbExplicacion.Text = " Continuamos realizando el producto de la siguiente diagonal hacia la izquierda: ";

                        DespintarCajas();
                        producto = 1;
                        string planteamiento = PintarDiagonalIzquierda(matriz, descontador, Color.Coral, ref producto);
                        label1.Text = planteamiento + " = " + Racional.AString(producto);
                        productosizquierda.Add(producto);
                        if (descontador == matriz.GetLength(0) - 1)
                        {
                            label2.Text = " ";
                            label2.Text = "Memo: " + Racional.AString(producto);
                        }
                        else
                        {
                            if (producto.Numerador >= 0)
                                label2.Text += " + ";
                            label2.Text += Racional.AString(producto);
                        }
                        descontador--;
                    }
                    else
                    {
                        label2.Text = " ";
                        lbExplicacion.Text = " Por ultimo restamos al total de la suma de productos de las diagonales a la derecha, el total de la suma de productos de las diagonales a la izquierda. ";
                        Racional totalizquierda = 0;
                        label1.Text = "  ";
                        foreach (Racional r in productosizquierda)
                        {
                            totalizquierda += r;
                        }
                        label4.Show();
                        label4.BackColor = Color.SeaGreen;
                        label4.Text = "Toltal izquierda:  " + Racional.AString(totalizquierda);
                        Racional totalderecha = 0;
                        foreach (Racional r in productos)
                        {
                            totalderecha += r;
                        }
                        label1.Text = " El determinante o módulo de la matriz es: " + Racional.AString(totalderecha) + " - " + Racional.AString(totalizquierda) + " = " + Racional.AString(totalderecha - totalizquierda);
                        btContinuar.Hide();
                        foreach (TextBox t in matriz)
                            t.BackColor = Color.White;
                        lbExplicacion.Focus();
                    }
                }
            } // Fin de la resolucion por el metodo de las diagonales
            else if (menores) // Resolucion por el metodo de las menores principales
           {
                if (paso == 0) // primer paso de la resolucion
                {
                    productos.Clear();
                    producto = 0;
                    btContinuar.Show();
                    btContinuar.Location = new Point(900, 150);
                    radioButton1.Hide();
                    radioButton2.Hide();
                    radioButton3.Hide();
                    EtiquetaFilas.Hide();
                    tbFilas.Hide();
                    label1.BackColor = Color.SeaGreen;
                    label2.BackColor = Color.SeaGreen;
                    // Pintar de negro la fila y columna correspondiente
                    PintarFilaColumna(matriz, 0, 0, Color.Black);
                    lbExplicacion.Focus();
                    // Escribir explicacion en la etiqueta
                    lbExplicacion.Text = " Primero extraemos la submatriz formada por los elementos cuya fila o columna, no coincida con el elemento numero " + paso.ToString() + " de la matriz, y calculamos el determinante de la misma.";
                    lbExplicacion.Text += " \n\r Mulplicamos el determinante de la submatriz " + Racional.AString(determinantemenor) + " por el elemento numero " + paso.ToString() + " de la matriz. ";
                    lbExplicacion.Text += " \n\r Como el indice del elemento 0 de la matriz es par, guardamos el resultado sin mas.";
                    // Mostrar el planteamiento
                    label1.Show();
                    label1.Location = new Point(matriz[0, 0].Location.X, matriz[matriz.GetLength(0) - 1, 0].Location.Y + matriz[0, 0].Height + 10);
                    label1.Text = Racional.AString(determinantemenor) + " * " + matriz[0, 0].Text + " = " + Racional.AString(determinantemenor * Racional.StringToRacional(matriz[0, 0].Text));
                    // Mostrar el resultado anterior
                    label2.Show();
                    label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 10);
                    label2.Text = "Memo: " + Racional.AString(determinantemenor * Racional.StringToRacional(matriz[0, 0].Text));
                    productos.Add(determinantemenor * Racional.StringToRacional(matriz[0, 0].Text));
                    filaactual = 0;
                    columnaactual = 1;
                    paso++;
                }
                else if ( paso > 0) // pasos posteriores al primero hasta llegar al orden de la matriz
                {
                    if (paso < matriz.GetLength(0))
                    {
                        // Pintar de negro la fila y columna correspondiente
                        PintarFilaColumna(matriz, filaactual, columnaactual, Color.Black);
                        // Escribir la explicacion en la etiqueta
                        lbExplicacion.Text = " Continuamos extrayendo la submatriz formada por los elementos cuya fila o columna, no coincida con el elemento numero " + paso.ToString() + " de la matriz, y calculamos el determinante de la misma.";
                        lbExplicacion.Text += " Mulplicamos el determinante de la submatriz " + Racional.AString(determinantemenor) + " por el elemento numero " + paso.ToString() + " de la matriz. ";
                        if (paso % 2 == 0)
                        {
                            lbExplicacion.Text += " \n\r Como el indice del elemento" + paso.ToString() + "  de la matriz es par, guardamos el resultado sin mas.";
                            label1.Show();
                            label1.Text = Racional.AString(determinantemenor) + " * " + matriz[filaactual, columnaactual].Text + " = " + Racional.AString(determinantemenor * Racional.StringToRacional(matriz[filaactual, columnaactual].Text));
                            label2.Show();
                            Racional parcial = determinantemenor * Racional.StringToRacional(matriz[filaactual, columnaactual].Text);
                            if (parcial.Numerador >= 0)
                                label2.Text += " + ";
                            label2.Text += Racional.AString(parcial);
                            productos.Add(determinantemenor * Racional.StringToRacional(matriz[filaactual, columnaactual].Text));
                        }
                        else
                        {
                            lbExplicacion.Text += " \n\r Como el indice del elemento " + paso.ToString() + " de la matriz es impar, cambiamos el signo del resultado";
                            label1.Show();
                            label1.Text = Racional.AString(determinantemenor) + " * " + matriz[filaactual, columnaactual].Text + " = " + Racional.AString(determinantemenor * Racional.StringToRacional(matriz[filaactual, columnaactual].Text));
                            label2.Show();
                            Racional parcial = determinantemenor * Racional.StringToRacional(matriz[filaactual, columnaactual].Text) * -1;
                            if (parcial.Numerador >= 0)
                                label2.Text += " + ";
                            label2.Text += Racional.AString(parcial);

                            productos.Add(determinantemenor * Racional.StringToRacional(matriz[filaactual, columnaactual].Text) * -1);
                        }
                        columnaactual++;
                        if (columnaactual > matriz.GetLength(0) - 1)
                        {
                            columnaactual = 0;
                            filaactual++;
                        }
                    }
                    else
                    {
                        foreach (Racional r in productos)
                        {
                            producto += r;
                        }
                        label3.AutoSize = true;
                        label3.Location = new Point(label2.Location.X, label2.Location.Y + label2.Height + 10);
                        label3.BackColor = Color.SeaGreen;
                        label3.Show();
                        label3.Text = " El determinante de la matriz es la suma de los resultados anteriores: " + Racional.AString(producto);
                        foreach (TextBox t in matriz)
                        {
                            t.BackColor = Color.White;
                        }
                        label1.Hide();
                        lbExplicacion.Hide();
                        btContinuar.Hide();
                        label3.Focus();
                    }
                }
                ////////////////////////////////////
           }   // Fin de la resolucion por el metodo de las menores principales
            // Resolucion por el metodo de Gauss.
            else if (gauss)
            {
                if (paso == 0)
                {
                    lbExplicacion.Focus();
                    foreach (TextBox t in matriz)
                    {
                        t.Location = new Point(t.Location.X + (100 * matriz.GetLength(0)), t.Location.Y);
                    }

                    copiamatriz = new TextBox[matriz.GetLength(0), matriz.GetLength(0)];
                    for (int i = 0; i < matriz.GetLength(0); i++)
                    {
                        for (int j = 0; j < matriz.GetLength(0); j++)
                        {
                            copiamatriz[i, j] = new TextBox();
                            copiamatriz[i, j].Size = matriz[i, j].Size;
                            copiamatriz[i, j].TextAlign = HorizontalAlignment.Center;
                            copiamatriz[i, j].Location = new Point(matriz[i, j].Location.X - (90 * matriz.GetLength(0)), matriz[i, j].Location.Y);
                            Controls.Add(copiamatriz[i, j]);
                            copiamatriz[i, j].Text = matriz[i, j].Text;
                        }
                    }

                    label5.Location = new Point(copiamatriz[0, 0].Location.X, copiamatriz[0, 0].Location.Y - 20);
                    label5.Show(); // Titulo de la matriz
                    label5.Text = "Matriz original.";
                    label5.BackColor = Color.SeaGreen;
                    label5.Font = new Font("Dejavu Sans", 10, FontStyle.Underline);
                    label6.Location = new Point(matriz[0, 0].Location.X, label5.Location.Y);
                    label6.Show(); // Titulo de la matriz copia
                    label6.Text = "Matriz copia.";
                    label6.Font = label5.Font;
                    label6.BackColor = Color.SeaGreen;
                    filaactual = 0;
                    columnaactual = 0;
                    productos.Clear();
                    producto = 0;
                    btContinuar.Show();
                    btContinuar.Location = new Point(900, 150);
                    radioButton1.Hide();
                    radioButton2.Hide();
                    radioButton3.Hide();
                    EtiquetaFilas.Hide();
                    tbFilas.Hide();
                    lbExplicacion.Show();

                    lbExplicacion.Text = " Para empezar, tenemos que convertir todos los elementos por debajo del primer elemento de la diagonal principal, en cero. ";
                    producto = Racional.StringToRacional(matriz[filaarestar, columnaaconvertir].Text) / Racional.StringToRacional(matriz[columnaaconvertir, columnaaconvertir].Text);
                    lbExplicacion.Text += " \n\r Para ello,comenzamos dividiendo el elemento debajo del primer elemento de la diagonal principal por este, y guardamos el cociente.";
                    lbExplicacion.Text += " \n\r Este cociente, lo multiplicamos a cada elemento de la primera fila, y el resultado se lo restamos a cada elemento de la segunda.";

                    label1.Location = new Point(matriz[0, 0].Location.X, matriz[matriz.GetLength(0) - 1, 0].Location.Y + matriz[0, 0].Height + 10);
                    label1.Show();
                    label1.BackColor = Color.SeaGreen;
                    label1.Text = matriz[filaarestar, columnaaconvertir].Text + " / " + matriz[columnaaconvertir, columnaaconvertir].Text + " = " + Racional.AString(producto);
                    label2.Show();
                    label2.BackColor = Color.SeaGreen;
                    label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 10);
                    label2.Text = "Cociente = " + Racional.AString(producto);
                    filaactual = 0;
                    label7.Show();
                    label7.BackColor = Color.SeaGreen;
                    label7.ForeColor = Color.Silver;
                    label7.Text = "Secuencia de operaciones: ";
                    Temporizador1.Start();
                    paso++;
                }
                else if ( paso > 0)
                {
                    lbExplicacion.Focus();
                    btContinuar.Hide();
                    label7.Text = "Secuencia de operaciones:  ";
                    // Si ya se ha alcanzado la ultima fila a pero quedan columnas por convertir a cero los elementos por debajo de la diagonal principal
                    if (filaarestar > matriz.GetLength(0) - 1 && columnaaconvertir < matriz.GetLength(0) - 1)
                    {
                        foreach (TextBox t in matriz)
                        {
                            t.BackColor = Color.White; // Repintar todas las celdas con fondo blanco
                        }
                        columnaaconvertir++;
                        filaarestar = columnaaconvertir + 1;
                        filaactual = columnaaconvertir;
                        columnaactual = columnaaconvertir;
                    }
                    
                    // Fijar la cantidad de pasos a realizar para convertir todos los elemetos por debajo de la diagonal principal en cero
                    int limite = 0;
                    
                    int orden = matriz.GetLength(0); // Orden de la matriz

                    while (orden > 0) // Sumar la cantidad de pasos que seran necesarios para convertir la columna en cero
                    {
                        orden--;
                        limite += orden;
                    }

                    if (paso < limite)
                    {
                        lbExplicacion.Text = " Continuamos convitiendo en cero todos los elementos por debajo del elemento " + (paso + 1) + " de la diagonal principal de la matriz.";
                        producto = Racional.StringToRacional(matriz[filaarestar, columnaaconvertir].Text) / Racional.StringToRacional(matriz[columnaaconvertir, columnaaconvertir].Text);
                        label1.Text = matriz[filaarestar, columnaaconvertir].Text + " / " + matriz[columnaaconvertir, columnaaconvertir].Text + " = " + Racional.AString(producto);
                        label2.Text = "Cociente = " + Racional.AString(producto);
                        Temporizador1.Start();
                    }
                    else
                    {
                        lbExplicacion.Text = " Una vez convertidos en cero todos los elementos por debajo de la diagonal principal, hemos convertido la matriz en una matriz tringular.";
                        lbExplicacion.Text += "\r\n El determinante de las matrices triangulares, es el producto de los elementos de la diagonal principal: ";
                        label2.Hide();
                        label7.Hide();
                        string producto = " ";
                       // Racional resultado = 1;
                        for (int i = 0; i < matriz.GetLength(0); i++)
                        {
                            for (int j = 0; j < matriz.GetLength(0); j++)
                            {
                                if (i == j)
                                {
                                    producto += matriz[i, j].Text + " * ";
                                   // resultado *= Racional.StringToRacional(matriz[i, j].Text);
                                }
                            }
                        }
                        producto = producto.Substring(0, producto.Length - 2);
                       // label1.Text = producto + " = " + Racional.AString(resultado);
                        label1.MaximumSize = new Size(500, 200);
                        label1.Text = producto + " = " + (Matematicas.AlgebraLineal.Determinante(matrizracional)).ToString();
                    }

                }


            }

        }

        /// <summary>
        ///  
        ///  PINTA DE BLANCO EL FONDO DE TODAS LAS CAJAS DE LA MATRIZ
        ///     
        /// </summary>

        private void DespintarCajas()
        {
            foreach (TextBox t in matriz)
            {
                t.BackColor = Color.White;
            }
        }
      
        /// <summary>
        ///  
        ///  PINTA EL FONDO DE LAS CAJAS DE TEXTO QUE ESTAN EN LA DIAGONAL A IZQUIERDA
        ///  CUYO INDICE ES PASADO COMO ARGUMENTO. LA MATRIZ ES PASADA COMO ARGUMENTO
        ///  Y DEVUELVE EL STRING DEL PLATEAMIENTO DEL PRODUCTO DE ESOS ELEMENTOS
        ///  Y EL RESULTADO DEL PRODUCTO COMO REFERENCIA
        ///     
        /// </summary>


        private string PintarDiagonalIzquierda(TextBox[,] matriz, int indice, Color color, ref Racional producto)
        {
            int fila = 0;
            string planteamiento = " ";
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (indice < 0)
                    indice = matriz.GetLength(0) - 1;


                matriz[fila, indice].BackColor = color;
                producto *= Racional.StringToRacional(matriz[fila, indice].Text);
                planteamiento += matriz[fila, indice].Text + " * ";
                fila++;
                indice--;
            }
            planteamiento = planteamiento.Substring(0, planteamiento.Length - 2);
            planteamiento += " = ";
            return planteamiento;
        }
        /// <summary>
        ///  
        ///  PINTA EL FONDO DE LAS CAJAS DE LA MATRIZ PASADA COMO ARGUMENTO, QUE ESTEN EN LA FILA O
        ///  COLUMNA PASADAS COMO ARGUMENTO Y CALCULA EL DETERMINANTE DE LA MENOR PRINCIPAL
        ///   
        /// </summary>

        internal void PintarFilaColumna(TextBox[,] matriz, int fila, int columna, Color color)
        {
            List<Racional> aux = new List<Racional>();
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(0); j++)
                {
                    if (i == fila && j != columna || i != fila && j == columna)
                    {
                        matriz[i, j].BackColor = color;
                    }
                    else if (i == fila && j == columna)
                    {
                        matriz[i, j].BackColor = Color.Coral;
                    }
                    else
                    {
                        matriz[i, j].BackColor = Color.White;
                        aux.Add(Racional.StringToRacional(matriz[i, j].Text));
                    }
                }
            }
            Racional[,] matrizaux = new Racional[matriz.GetLength(0) - 1, matriz.GetLength(0) - 1];
            int contador = 0;
            for (int i = 0; i < matrizaux.GetLength(0); i++)
            {
                for (int j = 0; j < matrizaux.GetLength(0); j++)
                {
                    matrizaux[i, j] = aux[contador];
                    contador++;
                }
            }
            determinantemenor = Matematicas.AlgebraLineal.Determinante(matrizaux);
        }

        /// <summary>
        ///  
        /// PINTA EL FONDO DE LAS CAJAS EN LA FILA POR DEBAJO DE LA FILA ACUAL DE COLOR ROJO, UNA CADA VEZ
        /// QUE SE CUMPLE EL TIEMPO ESTABLECIDO EN EL TEMPORIZADOR
        ///   
        /// </summary>

        private void Temporizador1_Tick(object sender, EventArgs e)
        {
            btNuevo.Hide();
            btContinuar.Hide();
           // label7.Location = new Point(matriz[0, 0].Location.X - 300, label2.Location.Y + label2.Height + 10);
            label7.Location = new Point(50, label2.Location.Y + label2.Height + 10);
            label7.MaximumSize = new Size(1100, 200);
            label7.TextAlign = ContentAlignment.MiddleLeft;

            if (filaarestar > matriz.GetLength(0))
            {
                filaarestar = columnaaconvertir;
            }

            if (filaactual < matriz.GetLength(0) && columnaactual < matriz.GetLength(0))
            {
                label7.Show();
                matriz[filaarestar, columnaactual].BackColor = Color.Coral;
                label7.Font = new Font(label7.Font.FontFamily, (int)(15 * Math.Ceiling((double)(1 / (double) label7.Text.Length))));
                label7.Text += matriz[filaarestar, columnaactual].Text + " - " + " ( " + matriz[filaactual, columnaactual].Text + " * " + Racional.AString(producto) + " ) " + " = " + (Racional.AString((Racional.StringToRacional(matriz[filaarestar, columnaactual].Text) - Racional.StringToRacional(matriz[filaactual, columnaactual].Text) * producto)) + "         ");
                matriz[filaarestar, columnaactual].Text = Racional.AString((Racional.StringToRacional(matriz[filaarestar, columnaactual].Text) - Racional.StringToRacional(matriz[filaactual, columnaactual].Text) * producto));
                columnaactual++;
            }
            else if (columnaactual >= matriz.GetLength(0) - 1)
            {
                columnaactual = columnaaconvertir;
                filaarestar++;
                Temporizador1.Stop();
                btContinuar.Show();
                btNuevo.Show();
            }

        }

    }
}

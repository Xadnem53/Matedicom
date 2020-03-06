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
    class Adjunta : FormularioBase
    {
        // Atributos propios
        Label[,] adjunta;
       new Racional[,] resultado;


        public Adjunta()
        {
            InitializeComponent();
        }

        public override void btNuevo_Click(object sender, EventArgs e)
        {
            if (matriz != null)
            {
                foreach (TextBox t in matriz)
                    t.Dispose();
            }
            if (adjunta != null)
            {
                foreach (Label l in adjunta)
                    l.Dispose();
            }
            defecto = false;
            btDefecto.Show();
            EtiquetaFilas.Show();
            tbFilas.ResetText();
            tbFilas.Show();
            lbExplicacion.Show();
            lbExplicacion.Text = "Introducir números enteros. \n\n( Pulsar el botón 'E' para resolución de ejemplo con valores por defecto. )";
            btContinuar.Hide();
            label1.Hide();
            label2.Hide();
            tbFilas.Focus();
        }

        public Adjunta(bool directamente)
        {
            directa = directamente;
        }

        public override void Cargar(object sender, EventArgs e)
        {
	    this.MinimumSize = new Size(1300,700);
            this.Text = "Matriz adjunta de una matriz cuadrada.";
            EtiquetaFilas.Show();
            tbFilas.Show();
            EtiquetaFilas.Text = "Cantidad de filas y columnas de la matriz:";
            btSalir.Click += btSalir_Click;
            tbFilas.KeyPress += tbFilas_KeyPress;
            lbExplicacion.Show();
            lbExplicacion.Text = "Introducir números enteros. \n\n( Pulsar el botón 'E' para resolución de ejemplo con valores por defecto. )";
            btContinuar.Click += ResolucionPasoAPaso;
            lbExplicacion.Size = new Size(lbExplicacion.Size.Width - 15, lbExplicacion.Size.Height);
            btDefecto.Click += btDefecto_Click;
            btNuevo.Click += btNuevo_Click;
            EtiquetaFilas.Location = new Point(EtiquetaFilas.Location.X, lbExplicacion.Height + 5);
            tbFilas.Location = new Point(tbFilas.Location.X, EtiquetaFilas.Location.Y);
            btDefecto.Location = new Point(btDefecto.Location.X, tbFilas.Location.Y);
        }


        private new void btSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
            MenuMatrices menumatrices = new MenuMatrices();
            menumatrices.Show();
        }

        private new void btDefecto_Click(object sender, EventArgs e)
        {
            defecto = true;
            ConstruirMatriz();
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
        ///  MUESTRA EN PANTALLA LA MATRIZ DE CAJAS DE TEXTO DEL ORDEN INTRODUCIDO, DONDE SE
        ///  INTRODUCIRAN LOS VALORES Racional DE LA MATRIZ CUYO DETERMINANTE SE QUIERE CALCULAR
        /// 
        /// </summary>

        internal void ConstruirMatriz()
        {
            btDefecto.Hide();
            EtiquetaFilas.Hide();
            tbFilas.Hide();
            if( !defecto)
            lbExplicacion.Text = " Introducir enteros o racionales. ( ejemplos; 3 ; 1/3, 5/14 ; etc )";
            if (defecto)
                orden = 3;
            int indicetabulador = 0;
            matriz = new TextBox[orden, orden];
            Point origen = new Point(150, 200);
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    matriz[i, j] = new TextBox();
                    matriz[i, j].Size = new Size(50, 20);
                    matriz[i, j].Location = origen;
                    matriz[i, j].TabIndex = indicetabulador;
                    matriz[i, j].KeyPress += Matriz_KeyPress;
                    matriz[i, j].TextAlign = HorizontalAlignment.Center;
                    Controls.Add(matriz[i, j]);
                    matriz[i, j].Show();
                    origen.X += 60;
                    indicetabulador++;
                }
                origen.X = 150;
                origen.Y += 30;
            }
            matriz[0, 0].Focus();
            label1.Show();
            label1.Text = "Matriz Introducida";
            label1.BackColor = Color.SeaGreen;
            label1.Font = new Font("Dejavu Sans",9,FontStyle.Underline);
            label1.Location = new Point(matriz[0, 0].Location.X, matriz[0, 0].Location.Y - 20);
            if (defecto)
            {
                matriz[0, 0].Text = "3"; matriz[0, 1].Text = "7"; matriz[0, 2].Text = "4";
                matriz[1, 0].Text = "9"; matriz[1, 1].Text = "5"; matriz[1, 2].Text = "1";
                matriz[2, 0].Text = "2"; matriz[2, 1].Text = "4"; matriz[2, 2].Text = "6";
                ConstruirAdjunta();
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
                                for (int j = 0; j < matriz.GetLength(0); j++)
                                {
                                    matrizracional[i, j] = Racional.StringToRacional(matriz[i, j].Text);
                                    // lbMensajes.Text = Racional.AString(matrizracional[i,j]) + "  ";
                                }
                            }
                            // SIGUIENTE METODO
                            ConstruirAdjunta();
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
                        // SIGUIENTE METODO
                        ConstruirAdjunta();
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
        ///  CONSTRUYE UNA MATRIZ DE LA MISMA DIMENSION QUE LA MATRIZ DE LA QUE SE QUIERE OBTENER 
        ///  LA ADJUNTA PARA DAR EL RESULTADO
        ///  ,
        /// </summary>

        private void ConstruirAdjunta()
        {
            if (defecto)
            {
                matrizracional = new Racional[orden, orden];
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz.GetLength(0); j++)
                    {
                        matrizracional[i, j] = Racional.StringToRacional(matriz[i, j].Text);
                    }
                }
            }
            if (!directa)
                lbExplicacion.Text = "La matriz adjunta es del mismo tamaño que la matriz inicial.";
            else
                lbExplicacion.Text = "";
            int indicetabulador = 0;
            adjunta = new Label[orden, orden];
            Point origen = new Point(70 + (matriz.GetLength(0) * 100), 200);
            label2.Location = new Point(90 + (matriz.GetLength(0) * 100), label1.Location.Y);
            label2.Font = label1.Font;
            label2.Text = "Matriz adjunta";
            label2.BackColor = Color.SeaGreen;
            label2.Show();
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    adjunta[i, j] = new Label();
                    adjunta[i, j].BackColor = Color.White;
                    adjunta[i, j].Size = new Size(50, 20);
                    adjunta[i, j].Location = origen;
                    adjunta[i, j].TabIndex = indicetabulador;
                    adjunta[i,j].TextAlign = ContentAlignment.MiddleCenter;
                    Controls.Add(adjunta[i, j]);
                    adjunta[i, j].Show();
                    origen.X += 60;
                    indicetabulador++;
                }
                origen.X = 70 + (matriz.GetLength(0) * 100);
                origen.Y += 30;
            }
            if (directa)
            {
                Racional[,] resultado = Matematicas.AlgebraLineal.Adjunta(matrizracional);
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        adjunta[i, j].Text = Racional.AString(resultado[i, j]);
                        adjunta[i, j].BackColor = Color.YellowGreen;
                    }
                }
            }
            else
            {
                filaactual = 0;
                columnaactual = 0;
                lbExplicacion.Location = new Point(10, 5);
                lbExplicacion.Show();
                if (!directa)
                {
                    btContinuar.Location = new Point(100, matriz[matriz.GetLength(0) - 1,0].Location.Y+50);
                    btContinuar.Show();
                }
                resultado = Matematicas.AlgebraLineal.Adjunta(matrizracional);
            }
            lbExplicacion.Focus();
        }


        /// <summary>
        /// 
        ///  AVANZA EN LA RESOLUCION DE LA MATRIZ ADJUNTA UN PASO CADA VEZ QUE SE CLICA SOBRE EL
        ///  BOTON CONTINUAR
        /// 
        /// </summary>

        internal void ResolucionPasoAPaso(object sender, EventArgs e)
        {
            if (columnaactual > orden - 1)
            {
                columnaactual = 0;
                filaactual++;
                if (filaactual > orden - 1)
                {
                    lbExplicacion.Text = " Hemos calculado la matriz adjunta.";
                    foreach (Label l in adjunta)
                        l.BackColor = Color.YellowGreen;
                    btContinuar.Hide();
                    foreach (TextBox t in matriz)
                        t.BackColor = Color.White;
                    lbExplicacion.Focus();
                    return;
                }

            }
            PintarFilaColumna(matriz, filaactual, columnaactual, Color.Black);
            string parimpar = " ";
            if ((filaactual + columnaactual) % 2 == 0)
                parimpar += "Como la suma de los indices del elemento [" + filaactual + "," + columnaactual + " ], es par, ponemos el determinante calculado en la posicion  [" + columnaactual + "," + filaactual + " ] de la matriz adjunta sin mas";
            else
                parimpar += "Como la suma de los indices del elemento [" + filaactual + "," + columnaactual + " ], es impar, cambiamos el signo del determinante calculado y lo ponemos en la posicion  [" + columnaactual + "," + filaactual + " ] de la matriz adjunta";

            if (filaactual == 0 && columnaactual == 0)
                lbExplicacion.Text = "Para comenzar, eliminamos todos los elementos que estan en la misma fila y columna que el elemento [" + filaactual + "," + columnaactual + " ] , y calculamos el determinante de la matriz compuesta por los elementos que quedan.";
            else
                lbExplicacion.Text = "Eliminamos todos los elementos que estan en la misma fila y columna que el elemento [" + filaactual + "," + columnaactual + " ] , y calculamos el determinante de la matriz compuesta por los elementos que quedan.";

            lbExplicacion.Text += "\r\n " + parimpar;

            adjunta[columnaactual, filaactual].BackColor = Color.Coral;
            adjunta[columnaactual, filaactual].Text = Racional.AString(resultado[columnaactual, filaactual]);


            columnaactual++;
            paso++;
        }


        /// <summary>
        ///  
        ///  PINTA EL FONDO DE LAS CAJAS DE LA MATRIZ PASADA COMO ARGUMENTO, QUE ESTEN EN LA FILA O
        ///  COLUMNA PASADAS COMO ARGUMENTO
        ///   
        /// </summary>

        internal void PintarFilaColumna(TextBox[,] matriz, int fila, int columna, Color color)
        {
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
                    }
                }

            }
        }



    }
}

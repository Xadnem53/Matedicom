using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matematicas;
using System.Drawing;
using System.Windows.Forms;

namespace AlgebraLineal
{
    //!!!!!!!!!!!!!!!!!
    //
    // EN LOS DIALOGOS PARA COMPROBAR SI LA MATRIZ ES DIAGONALIZABLE Y PARA LA OBTENCION DE VECTORES 
    // PROPIOS, CAMBIADA LA ETIQUETA PARA VISUALIZAR LOS PASOS POR RICHTEXTBOX CON SCROLL BARS
    // 13/9/2016
    //
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public class Diagonalizacion:FormularioBase
    {
        /////////////// ATRIBUTOS EXCLUSIVOS DE ESTA CLASE
          Label[,] modal; // Matriz de etiquetas que sera la matriz modal 
          long[,] matrizmodal; // Matriz modal que se construira con los vectores propios de la matriz
          long[,] matrizlong; // Matriz de cajas de texto que sera la matriz original en formato long
        Polinomio caracteristico; // Sera el polinomio caracteristico de la matriz introducida
       //   List<long> divisores; // Sera la lista de divisores del termino independiente delpolinomio caracteristico
          List<Racional> raices;// Sera la lista de raices del polinomio caracteristico
          bool valoresrepetidos = false; // Sera true si la matriz tiene valores propios repetidos
          List<Matematicas.AlgebraLineal.Valorrepetido> repes = new List<Matematicas.AlgebraLineal.Valorrepetido>(); // Lista donde se guardaran los valores propios repetidos si los hay
          Racional[,] matrizpropia; // Sera la matriz propia correspondiente a cada valor propio de la matriz
          List<Label> vectorespropios = new List<Label>(); // Lista de etiquetas que mostraran los vectores propios de la matriz
          List<List<Racional>> resultados = new List<List<Racional>>(); // Lista de listas Racional que contendrá los vectores propios antes de pasarlos a entero
          List<List<long>> vectorespropioslong = new List<List<long>>(); // Lista de listas long que contendrá los vectores propios en formato entero
        Point inicial = new Point(1000,100); // Punto de referencia para situar las etiquetas con los vectores propios
        List<Racional> registro = new List<Racional>(); // Para guardar un valor propio cuando ya se ha usado
        bool valorepetidousado = false; // Indicara si el valor propio ha sido ya usado o no.
        int repeticion = 0; // Almacena la cantidad de apariciones del valor propio repetido.
        Label[,] diagonal;// Para mostrar la matriz diagonal
        Racional[,] matrizmodalinversa; // Inversa de la matriz modal
        Racional[,] matrizmodalracional; // Copia de la anterior
        Racional[,] prueba1; // Matriz producto de la diagonal con la matriz modal
       new Label[,] resultado; // Para mostrar la matriz resultado de la prueba
        Label lbtitulodiagonal;
          int contador2 = 0;
          List<ResultadosSistema> anterior = new List<ResultadosSistema>();
          Racional[,] matrizdiagonal; // Para almacenar la matriz diagonal en la resolucion directa
       //   AlgebraLineal.Valorrepetido repetido = default( AlgebraLineal.Valorrepetido); // Para registrar cuando se usa un valor repetido
        /// <summary>
        ///  CONSTRUCTOR
        /// </summary>
        /// <param name="resoluciondirecta"></param>
        /// 
        public Diagonalizacion(bool resoluciondirecta)
        {
            directa = resoluciondirecta;
            if (directa)
                btDefecto.Hide();
        }

        /// <summary>
        /// VALORES INICIALES 
        /// </summary>
        /// <param name="resoluciondirecta"></param>
        /// 
        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = " Diagonalización de matrices cuadradas.";
            lbtitulodiagonal = new Label();
            lbExplicacion.Show();
            lbExplicacion.MinimumSize = new Size(1150, 0);
            lbExplicacion.MaximumSize = new Size(1150, 500);
            lbExplicacion.AutoSize = true;
            lbExplicacion.Text = "Solo enteros.";
            if (!directa)
                lbExplicacion.Text += "\n\n( Botón [E] para ejemplo con valores por defecto. )";
            EtiquetaFilas.Show();
            EtiquetaFilas.AutoSize = true;
            EtiquetaFilas.Text = "Cantidad de filas y columnas de la matriz:";
            tbFilas.Show();
            tbFilas.Location = new Point(EtiquetaFilas.Width + 15, tbFilas.Location.Y + lbExplicacion.Size.Height);
            tbFilas.Focus();
            EtiquetaFilas.Location = new Point(EtiquetaFilas.Location.X, tbFilas.Location.Y);
            btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 15, tbFilas.Location.Y);
            tbFilas.KeyPress += tbFilas_KeyPress;
            tbFilas.Enter += tbFilas_Enter;
            tbFilas.MouseClick += tbFilas_MouseClick;
            btNuevo.Click += btNuevo_Click;
	    btContinuar.Anchor = AnchorStyles.Top|AnchorStyles.Left;
	    this.MinimumSize = new Size(1300,700);
        }

        ///<summary>
        ///
        ///REINICIA EL FORMULARIO PARA REALIZAR LA DIAGONALIZACION DE UNA NUEVA MATRIZ
        ///
        /// </summary>
        /// 
        public override void btNuevo_Click(object sender, EventArgs e)
        {
            lbExplicacion.ForeColor = Color.Black;
            lbExplicacion.Text = "Solo enteros.";
            lbExplicacion.BackColor = Color.DarkSeaGreen;
            if (!directa)
                lbExplicacion.Text += "\n\n( Botón [E] para ejemplo con valores por defecto. )";
            EtiquetaFilas.Show();
            EtiquetaFilas.Text = "Cantidad de filas y columnas de la matriz:";
            label10.Hide();
            label11.Hide();
            label12.Hide();
            label13.Hide();
            lbtitulodiagonal.Hide();
            btContinuar.Hide();
            tbFilas.ResetText();
            tbFilas.Show();
            tbFilas.Focus();
            if(!directa)
            btDefecto.Show();
            if (matriz != null)
            {
                foreach (TextBox t in matriz)
                    t.Dispose();
                matriz = null;
            }
            if (modal != null)
            {
                foreach (Label l in modal)
                    l.Dispose();
                modal = null;
            }
            if (diagonal != null)
            {
                foreach (Label l in diagonal)
                    l.Dispose();
                diagonal = null;
            }
            if (resultado != null)
            {
                foreach (Label l in resultado)
                    l.Dispose();
                resultado = null;
            }
            if (vectorespropios != null)
            {
                foreach (Label l in vectorespropios)
                    l.Dispose();
                vectorespropios.Clear();
            }
            btContinuar.Click -= MostrarRaices;
            btContinuar.Click -= ConfirmarRaices;
            btContinuar.Click -= Continuar_CalcularVectores;
            btContinuar.Click -= Continuar_CalcularVectores2;
            btContinuar.Click -= Continuar_MostrarModal;
            btContinuar.Click -= Continuar_PruebaDiagonal;
            registro.Clear();
            resultados.Clear();
            contador = 0;
            contador2 = 0;
            repeticion = 0;
            valorepetidousado = false;
            inicial = new Point(1000, 100);
            this.AcceptButton = null;
            defecto = false;
            btContinuar.Hide();
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUZCA UN NUMERO ENTERO DISTINTO DE CERO EN LA CAJA DE TEXTO
        /// DE LA CANTIDAD DE FILAS Y COLUMNAS DE LA MATRIZ
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

            else if (tbFilas.Text.Length < 1) // No permitir introducir menos de orden 2
            {
                if (e.KeyChar < '2')
                    e.Handled = true;
            }

            else if (e.KeyChar < '0' || e.KeyChar > '9') // Asegurar que se introduzcan digitos y no otro tipo de caracteres
            {
                e.Handled = true;
            }

        }
        private void tbFilas_Enter(object sender, EventArgs e)
        {
            TextBox aux = (TextBox)sender;
            aux.SelectAll();
        }

        private void tbFilas_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox aux = (TextBox)sender;
            aux.SelectAll();
        }

        /// <summary>
        /// 
        ///  MANEJADOR PARA EL BOTON PARA VALORES POR DEFECTO
        ///  
        /// </summary>
        /// 
        public override void btDefecto_Click(object sender, EventArgs e)
        {
            defecto = true;
            btDefecto.Hide();
            ConstruirMatriz();
        }



        /// <summary>
        /// 
        ///  MUESTRA EN PANTALLA LA MATRIZ DE CAJAS DE TEXTO DEL ORDEN INTRODUCIDO, DONDE SE
        ///  INTRODUCIRAN LOS VALORES Racional DE LA MATRIZ CUYO DETERMINANTE SE QUIERE CALCULAR
        /// 
        /// </summary>

        internal void ConstruirMatriz()
        {
            // Valores por defecto
            if (defecto)
            {
                orden = 3;
                matrizlong = new long[orden, orden];
            }
            label10.Show();
            label10.Location = new Point(100, 150);
            label10.BackColor = Color.Transparent;
            label10.AutoSize = true;
            label10.Font = new Font("Dejavu Sans", 10, FontStyle.Underline);
            label10.Text = " Introducir sólo números enteros";
            lbExplicacion.Text = "Introducir la matriz a diagonalizar.Sólo enteros.";
            btDefecto.Hide();
            EtiquetaFilas.Hide();
            tbFilas.Hide();
            int indicetabulador = 0;
            matriz = new TextBox[orden, orden];
            matrizracional = new Racional[orden, orden];
            descontador = orden - 1;
            Point origen = new Point(100, 200);
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    matriz[i, j] = new TextBox();
                    matriz[i, j].TextAlign = HorizontalAlignment.Center;
                    matriz[i, j].Size = new Size(50, 20);
                    matriz[i, j].Location = origen;
                    matriz[i, j].TabIndex = indicetabulador;
                    if(!defecto)
                    matriz[i, j].KeyPress += Matriz_KeyPress;
                    Controls.Add(matriz[i, j]);
                    origen.X += 60;
                    indicetabulador++;
                }
                origen.X = 100;
                origen.Y += 30;
            }
            if(!defecto)
            matriz[0, 0].Focus();
            // Valores por defecto
            if (defecto)
            {
                matriz[0, 0].Text = "2"; matrizlong[0, 0] = 2; matriz[0, 1].Text = "1"; matrizlong[0, 1] = 1; matriz[0, 2].Text = "1"; matrizlong[0, 2] = 1;
                matriz[1, 0].Text = "2"; matrizlong[1, 0] = 2; matriz[1, 1].Text = "3"; matrizlong[1, 1] = 3; matriz[1, 2].Text = "2"; matrizlong[1, 2] = 2;
                matriz[2, 0].Text = "3"; matrizlong[2, 0] = 3; matriz[2, 1].Text = "3"; matrizlong[2, 1] = 3; matriz[2, 2].Text = "4"; matrizlong[2, 2] = 4;
                ResolucionDiagonalizacion();
            }
        }

        /// <summary>
        ///  
        ///  COMPRUEBA QUE LOS DATOS INTRODUCIDO EN CADA TextBox EN matriz SEAN
        ///  ADECUADOS PARA PASARLOS A long MAS TARDE
        ///  Y CREA LA MATRIZ EN FORMATO long Y LA RELLENA CON DATOS DE matriz
        ///  PASADOS A long
        /// </summary>

        private void Matriz_KeyPress(object sender, KeyPressEventArgs e)
        {
            matrizlong = new long[matriz.GetLength(0), matriz.GetLength(1)];
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
                        else // Si es la ultima caja de la matriz rellenar la matriz en formato long
                        {
                            e.Handled = true;
                            for (int i = 0; i < matriz.GetLength(0); i++)
                            {
                                for (int j = 0; j < matriz.GetLength(1); j++)
                                {
                                    matrizlong[i, j] = Int64.Parse(matriz[i, j].Text);
                                }
                            }
                            // METODO SIGUIENTE
                            lbExplicacion.Focus();
                            if(!directa)
                            btContinuar.Show();
                            ResolucionDiagonalizacion();
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
                    catch (IndexOutOfRangeException) // Rellenar la matriz Racional y la long
                    {
                        e.Handled = true;
                        for (int i = 0; i < matriz.GetLength(0); i++)
                        {
                            for (int j = 0; j < matriz.GetLength(1); j++)
                            {
                                matrizlong[i, j] = Int64.Parse(matriz[i, j].Text);
                                matrizracional[i, j] = new Racional(Int64.Parse(matriz[i, j].Text), 1);
                            }
                        }
                        // METODO SIGUIENTE
                        ResolucionDiagonalizacion();
                        if(!directa)
                        btContinuar.Show();
                        lbExplicacion.Focus();
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

            else if (e.KeyChar < '0' || e.KeyChar > '9') // Asegurar que se introduzcan digitos y no otro tipo de caracteres
                e.Handled = true;
        }

        /// <summary>
        ///  
        /// CIERRA EL FORMULARIO ACTUAL Y ABRE UN NUEVO MENU DE ALGEBRA LINEAL
        /// 
        /// </summary>

        public override void btSalir_Click(object sender, EventArgs e)
        {
            MenuMatrices menumatrices = new MenuMatrices();
            menumatrices.Show();
            this.Dispose();
        }

        /// <summary>
        ///  
        /// REALIZA EL PROCESO DE DIAGONALIZACION DE LA MATRIZ INTRODUCIDA, DIRECTAMENTE O PASO A PASO
        /// SEGUN PROCEDA
        /// 
        /// </summary>

        private void ResolucionDiagonalizacion()
        {
            label10.Text = "Matriz original";
            Racional[,] matrizracional = new Racional[orden, orden];
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    matrizracional[i, j] = new Racional(matrizlong[i, j], 1);
                }
            }
            EtiquetaFilas.Hide();
            tbFilas.Hide();
            if (!directa) // Solo paso a paso
            {
                lbExplicacion.Location = new Point(10, 5);
                lbExplicacion.Text = " Para comenzar, obtenemos el polinomio caracteristico, que es el resultado de calcular el determinante de la matriz introducida multiplicada por la matriz unidad menos una variable 'K'. ";
                lbExplicacion.Show();
            }
            //Polinomio[,] matrizk = Matematicas.Diagonalizar.MatrizConK(matrizracional);
            Polinomio[,] matrizk = Matematicas.AlgebraLineal.MatrizConK(matrizracional);

            // Construir la matriz de etiquetas para mostrar la matriz menos K
            label11.Show();
            label11.Font = label10.Font;
            label11.BackColor = Color.SeaGreen;
            label11.Location = new Point((matriz[0, orden - 1].Location.X + 100), 150);
            label11.Text = " Matriz modal";
            modal = new Label[orden, orden];
            descontador = orden - 1;
            Point origen = new Point((matriz[0, orden - 1].Location.X + 70), matriz[0, orden - 1].Location.Y);
            //  Point origen = new Point(800, 500);
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    modal[i, j] = new Label();
                    modal[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    modal[i, j].Size = new Size(50, 20);
                    modal[i, j].Location = origen;
                    Controls.Add(modal[i, j]);
                    modal[i, j].Show();
                    modal[i, j].BackColor = Color.White;
                    origen.X += 60;
                }
                origen.X = (matriz[0, orden - 1].Location.X + 70);
                origen.Y += 30;
            }

            // Mostrar la matrizK usando la matriz que se usará para mostrar la matriz modal mas adelante
            string lectura = "";
            for (int i = 0; i < orden; i++)
            {
                for (int j = 0; j < orden; j++)
                {
                    for (int ind = 0; ind < matrizk[i, j].Largo; ind++)
                    {
                        lectura += matrizk[i, j].ObtenerTermino(ind).ToString();
                    }
                    modal[i, j].Text = lectura;

                    lectura = "";
                }
            }
            label11.Text = " Matriz menos K ";

            btContinuar.Location = new Point(matriz[orden - 1, 0].Location.X, matriz[orden - 1, 0].Location.Y+ 100);
           // Racional[,] matrizracionale = Matematicas.AlgebraLineal.MatrizLongARacional(matrizlong);
            caracteristico = Matematicas.AlgebraLineal.Determinante(Matematicas.AlgebraLineal.MatrizConK(matrizracional));
            label12.Show();
            label12.BackColor = Color.SeaGreen;
            label12.Location = new Point(200, matriz[orden - 1, 0].Location.Y + 250);
            label12.Text = "";
           // caracteristico.Simplificar();
           // caracteristico.EliminarCeros();
            label12.Text += "Polinomio caracteristico: " + caracteristico.ToString();
            if (!directa) // Solo resolucion paso a paso
            {
                btContinuar.Show();
                btContinuar.Focus();
                this.AcceptButton = btContinuar;
            }
            btContinuar.Click += new EventHandler(MostrarRaices);
            if (directa) // Solo resolucion directa
                MostrarRaices(new object(), new EventArgs());
        }


        /// <summary>
        ///  
        /// DETERMINA LAS RAICES DEL POLINOMIO CARACTERISTICO Y LAS MUESTRA EN LA ETIQUETA label12 
        /// 
        /// </summary>

        private void MostrarRaices(object sender, EventArgs e)
        {
            raices = caracteristico.Raices();
            if (!directa)
                lbExplicacion.Text = " Una vez construido el polinomio caracteristico, determinamos sus raices.";
                label12.Text += "\nRaices: ";
                foreach (Racional r in raices)
                {
                    label12.Text += "K = " + r.ToString() + " ; ";
                }
                if (!directa)
                {
                    btContinuar.Click -= MostrarRaices;
                    btContinuar.Click += ConfirmarRaices;
                }
                else
                    ConfirmarRaices(new object(), new EventArgs());
            
        }

        /// <summary>
        /// 
        ///  COMPRUEBA SI SE HAN ENCONTRADO TODAS LAS RAICES DEL POLINOMIO CARACTERISTICO, Y CONTINUA O
        ///  ABORTA LA DIAGONALIZACION SI SE HAN ENCONTRADO TODAS O NO
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ConfirmarRaices(object sender, EventArgs e)
        {
            if (raices.Count == caracteristico.Grado)
            {
                if(!directa)
                lbExplicacion.Text = "Las raices del polinomio caracteristico, son los valores propios de la matriz introducida.";
                label12.Text = "Polinomio característico: " + caracteristico.ToString() + "\nValores propios: ";
                int contador = 1;
                foreach (Racional r in raices)
                {
                    label12.Text += "K" + contador + "=" + r.ToString() + " ; ";
                    contador++;
                }

                // reducir el tamaño del numerador de las raices obtenidas a un maximo de 6 cifras y no continuar por riesgo de dar
                // resultados erroneos ( Revisar en futuras versiones )
                bool reducido = false;
                for (int i = 0; i < raices.Count; i++)
                {
                    if (!raices[i].EsEntero())
                    {
                        if (raices[i].Numerador.ToString().Length > 6 && raices[i].Denominador > 6)
                        {
                            int diferencia = raices[i].Numerador.ToString().Length - 6;
                            long nuevonumerador = Int64.Parse(raices[i].Numerador.ToString().Substring(0, 6));
                            int largodenominador = raices[i].Denominador.ToString().Length;
                            largodenominador -= diferencia;
                            long nuevodenominador = Int64.Parse(raices[i].Denominador.ToString().Substring(0, largodenominador));
                            raices[i] = new Racional(nuevonumerador, nuevodenominador);
                            reducido = true;
                        }
                    }
                }
                if (reducido)
                {
                    lbExplicacion.BackColor = Color.Red;
                    btContinuar.Hide();
                    lbExplicacion.Text = "El programa no puede continuar por limitaciones en la precision. Será mejorado en futuras versiones.";
                    if (directa)
                    {
                        MessageBox.Show("El programa no puede continuar por limitaciones en la precision.");
                        btNuevo.PerformClick();
                        tbFilas.Focus();
                        return;
                    }
                    contador = 1;
                    /*
                    label12.Text = "";
                    foreach (Racional r in raices)
                    {
                        label12.Text += "K" + contador + "=" + r.ToString() + " ; ";
                        contador++;
                    }
                     */
                }
                
                if (!directa) // Solo resolucion paso paso
                {
                    btContinuar.Click -= ConfirmarRaices;
                    btContinuar.Click += Continuar_CalcularVectores;
                }
                else if (directa) // Solo resolucion directa
                {
                    matrizdiagonal = new Racional[orden, orden];
                    Racional[,] matrizracio = Matematicas.AlgebraLineal.MatrizLongARacional(matrizlong);
                    Racional[,] modalracional = Matematicas.AlgebraLineal.Diagonalizar(matrizracio, ref matrizdiagonal);
                    if(modalracional == null)
                    {
                        lbExplicacion.Text = "Matriz no diagonalizable.";
                        lbExplicacion.ForeColor = Color.Red;
                        btContinuar.Hide();
                        return;
                    }
                    matrizmodal = new long[orden,orden];
                    for(int i = 0; i < orden; i++)
                        for( int j = 0 ; j < orden ; j++)
                            matrizmodal[i,j] = (long)(modalracional[i,j].ToDouble());
                   
                    Continuar_MostrarModal(new object(), new EventArgs());
                }
            }
            else
            {
                lbExplicacion.Text = "El programa no puede continuar al no haberse encontrado todas las raices del polinomio caracteristico.";
                btContinuar.Hide();
                if (directa)
                {
                    MessageBox.Show("Raices del polinomio caracteristico no encontradas.");
                    return;
                }
            }
        }

        /// <summary>
        ///  
        /// AVANZA EN EL CALCULO DE VECTORES PROPIOS UN PASO CADA VEZ QUE SE PULSA EL
        /// BOTON CONTINUAR
        /// 
        /// </summary>


        private void Continuar_CalcularVectores(object sender, EventArgs e)
        {
            CalcularVectores();
        }




        /// <summary>
        ///  
        /// SI HAY VALORES PROPIOS REPETIDOS, DETERMINA SI LA MATRIZ ES DIAGONALIZABLE.
        /// SI LA MATRIZ ES DIAGONALIZABLE, OBTIENE LOS VECTORES PROPIOS
        /// 
        /// </summary>

        private void CalcularVectores()
        {
            if (contador == 0) // Primer paso del calculo de vectores propios de un valor propio
            {
                contador++;
                label12.Location = new Point(label12.Location.X-100, label12.Location.Y - 60);
                if (!directa) // Solo resolucion paso paso
                {
                    lbExplicacion.Text = " Una vez obtenidos los valores propios, hay que obtener los vectores propios correspondientes.";
                    label12.Text = "Valores propios: ";
                    foreach (Racional r in raices)
                    {
                        label12.Text += r.ToString() + " ; ";
                    }
                }
                repes = new List<Matematicas.AlgebraLineal.Valorrepetido>();
                valoresrepetidos = Matematicas.AlgebraLineal.ValoresRepetidos(raices, ref repes);
                bool diagonalizable = true;

                if (valoresrepetidos)
                {
                    if (!directa) // Solo resolucion paso paso
                        lbExplicacion.Text += "\nComo la matriz tiene valores propios repetidos, hay que comprobar si es diagonalizable. ";
                    foreach (Matematicas.AlgebraLineal.Valorrepetido v in repes)
                    {
                        if (!directa) // Solo resolucion paso paso
                        {
                            label12.Text = "El valor propio: " + v.valorpropio.ToString() + " Está repetido:  " + v.repeticiones + " veces.";
                            label12.Text += "\nPara comprobar si una matriz con valores propios repetidos es diagonalizable, hay que aplicar la siguiente formula: \nrango de (A - K) = n - m. \nDonde: \nA Es la matriz original. \nK Es el valor propio repetido. \nn Es el orden de la matriz.\nm La cantidad de veces que el valor propio se repite.";
                        }
                        Racional[,] matrizracional = new Racional[orden, orden];
                        for (int i = 0; i < orden; i++)
                        {
                            for (int j = 0; j < orden; j++)
                            {
                                matrizracional[i, j] = new Racional(Int64.Parse(matriz[i, j].Text), 1);
                            }
                        }
                        DialogoDiagonalizable pruebadiagonalizable = new DialogoDiagonalizable(matrizracional, v);
                        if (!directa) // Solo resolucion paso paso
                            pruebadiagonalizable.ShowDialog();
                        if (!directa) // Solo resolucion paso a paso
                            diagonalizable = pruebadiagonalizable.diagonalizable;
                        else if (directa) // Solo resolucion directa
                            diagonalizable = Matematicas.AlgebraLineal.Diagonalizable(matrizracional, v);
                        if (!diagonalizable)
                        {
                            if (!directa) // Solo resolucion paso paso
                                lbExplicacion.Text += "\nComo no se cumple la regla anterior, la matriz no es diagonalizable";
                            lbExplicacion.Text += "\nLa matriz no es diagonalizable";
                            if (directa) // Solo resolucion directa
                                label12.Text += "\nNo se cumple la igualdad: \nrango de (A - K) = n - m. \nDonde: \nA Es la matriz original. \nK Es el valor propio repetido. \nn Es el orden de la matriz.\nm La cantidad de veces que el valor propio se repite.";
                            btContinuar.Hide();
                        }
                        else
                        {
                            if (!directa) // Solo resolucion paso paso
                            {
                                lbExplicacion.Text = "La matriz es diagonalizable";
                                label12.Text = "Valores propios: ";
                                foreach (Racional r in raices)
                                {
                                    label12.Text += r.ToString() + " ; ";
                                }
                            }
                        }
                    }
                }
                if (directa) // Solo resolucion directa
                {
                    contador++;
                    if (diagonalizable)
                        CalcularVectores();
                }
            }
            else // Segundo paso y posteriores del calculo de vectores propios de un valor propio
            {
                contador = 0;
                contador2 = 0;
                if (!directa)
                {
                    btContinuar.Click -= Continuar_CalcularVectores;
                    btContinuar.Click += Continuar_CalcularVectores2;
                    CalcularVectores2();
                }
                else if (directa)
                {
                    CalcularVectores2();
                }
            }
        }

        /// <summary>
        ///  
        /// AVANZA EN EL CALCULO DE VECTORES PROPIOS UN PASO CADA VEZ QUE SE PULSA EL
        /// BOTON CONTINUAR
        /// 
        /// </summary>


        private void Continuar_CalcularVectores2(object sender, EventArgs e)
        {
            CalcularVectores2();
        }



        /// <summary>
        ///  
        /// REALIZA EL CALCULO DE VECTORES PROPIOS UN PASO CADA VEZ QUE SE PULSA EL
        /// BOTON CONTINUAR
        /// 
        /// </summary>

        private void CalcularVectores2()
        {
            label12.Size = new Size(200, label12.Size.Height);
            if (contador < orden) // Si no se han calculado todos los vectores propios
            {
                if (contador2 == 0) // Primer paso del calculo del vector propio.
                {
                    if (!directa) // Solo resolucion paso paso
                    {
                        label11.Text = "Matriz propia del valor propio: " + raices[contador].ToString();
                        lbExplicacion.Text = "Para calcular el vector propio correspondiente al valor propio: " + raices[contador].ToString() + " , Restamos el valor propio a los elementos de la diagonal principal de la matriz.";
                        label12.Text = "Valores propios: ";
                        foreach (Racional r in raices)
                        {
                            label12.Text += r.ToString() + " ; ";
                        }
                    }
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            if (i == j)
                            {
                                modal[i, j].BackColor = Color.Coral;
                                modal[i, j].Text = matrizlong[i, j].ToString() + " - " + raices[contador].ToString();
                            }
                        }
                    }
                    if (directa)
                    {
                        contador2++;
                        CalcularVectores2();
                    }
                    contador2++;
                }
                else if (contador2 == 1) // Segundo paso del calculo del vector propio.
                {
                    if (!directa) // Solo resolucion paso paso
                        lbExplicacion.Text = "Realizando la resta obtenemos la matriz propia: ";
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            if (i == j)
                            {
                                modal[i, j].BackColor = Color.GreenYellow;
                                modal[i, j].Text = (matrizlong[i, j] - raices[contador]).ToString();
                            }
                        }
                    }
                    // Construir la matriz propia en formato Racional
                    matrizpropia = new Racional[orden, orden];
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                           // matrizpropia[i, j] = new Racional(Int64.Parse(modal[i, j].Text), 1);
                           
                            Racional lectura = 0; // Será el racional producto de la conversion del texto de las cajas de la matriz modal
                            if (modal[i, j].Text.Contains('/')) // Si se trata de un racional
                            {
                                string texto = modal[i, j].Text;
                                int indice = texto.IndexOf('/');
                                string numerador = texto.Substring(0, indice - 1);
                                string denominador = texto.Substring(indice + 1);
                                long num = Int64.Parse(numerador);
                                long den = Int64.Parse(denominador);
                                float aux = (float)num / (float)den;
                                lectura = (Racional)aux;
                            }
                            else
                                lectura = Racional.StringToRacional(modal[i, j].Text);
                            
                            matrizpropia[i, j] = lectura;
                        }
                    }
                    // Construir el sistema correspondiente a la matrizpropia
                    Sistema sistemapropio = new Sistema(matrizpropia);

                    if (directa) // Solo resolucion directa
                    {
                        contador2++;
                        CalcularVectores2();
                    }
                    contador2++;
                }
                else if (contador < orden) // Tercer paso y siguientes en el calculo del vector propio
                {
                    // Comprobar si se trata de un valor repetido 
                    bool esrepetido = ComprobarRepeticion();
                    // Comprobar si ya se ha usado este valor propio
                    Racional aux = raices[contador];
                    foreach (Racional reg in registro)
                    {
                        if (aux == reg)
                        {
                            valorepetidousado = true;
                        }
                    }
                    // Añadir el valor propio a la lista de registro
                    registro.Add(raices[contador]);

                    // Si no es un valor propio repetido o es un valor propio repetido que no se ha usado anteriormente
                    if (!esrepetido || (esrepetido && !valorepetidousado))
                    {
                        if (!directa) // Solo resolucion paso a paso
                        {
                            if (!esrepetido)
                                lbExplicacion.Text = "Como se trata de un valor propio no repetido, el proceso de obtencion del vector propio correspondiente es el siguiente:";
                            else if (esrepetido)
                                lbExplicacion.Text = "Como se trata de un valor propio repetido pero no usado anteriormenete, el proceso de obtencion del vector propio correspondiente es el siguiente: ";
                            DialogoResolucionVpNoUsado dialogonorepetidos = new DialogoResolucionVpNoUsado(matrizpropia);
                            dialogonorepetidos.ShowDialog();
                            List<Racional> auxx = new List<Racional>();
                            foreach (ResultadosSistema r in dialogonorepetidos.resultados)
                                auxx.Add(new Racional(r.Resultado));
          
                            resultados.Add(auxx);
                        }
                        else if (directa) // Solo resolucion directa
                        {
                            //List<Racional> resul = Sistema.ResolucionMatrizValoresRepetidosIndicaciones(matrizpropia, repes, repetido);
                            Sistema sistem = new Sistema(matrizpropia);
                            List<ResultadosSistema> results = new List<ResultadosSistema>();
                            sistem.ResolucionSistema(ref results);
                            List<Racional> resul = new List<Racional>();
                            foreach (ResultadosSistema r in results)
                                resul.Add(new Racional(r.Resultado));
                            resultados.Add(resul);
                        }
                        repeticion++;

                        if (!directa) // Solo resolucion paso paso
                            lbExplicacion.Text = "El vector obtenido es un vector propio de la matriz. ";
                        // Construir etiqueta con el resultado
                        if (inicial.Y < lbExplicacion.Height + 20)
                            inicial = new Point(inicial.X, lbExplicacion.Height + 30);
                        vectorespropios.Add(new Label());
                        vectorespropios[vectorespropios.Count - 1].AutoSize = true;
                        if ((vectorespropios.Count - 1) != 0)
                        {
                            int desplazamiento = (vectorespropios[vectorespropios.Count - 2].Size.Height + 15);
                            inicial.Y += desplazamiento;
                        }
                        vectorespropios[vectorespropios.Count - 1].Location = inicial;
                        vectorespropios[vectorespropios.Count - 1].BackColor = Color.Transparent;
                        vectorespropios[vectorespropios.Count - 1].Font = new System.Drawing.Font("Dejavu-sans", 10);
                        vectorespropios[vectorespropios.Count - 1].ForeColor = Color.Blue;
                        Controls.Add(vectorespropios[vectorespropios.Count - 1]);
                        vectorespropios[vectorespropios.Count - 1].Show();
                        int cont = 0;
                        if(!directa)
                        vectorespropios[vectorespropios.Count - 1].Text += "Vector: " + resultados.Count + "\n";
                        foreach (Racional r in resultados[resultados.Count - 1])
                        {
                            char incogni = (char)((int)'a' + cont);
                            // Si se trata de un valor propio repetido, guardar los resultados en la lista para usarla en la siguiente repeticion
                            if (esrepetido)
                                anterior.Add(new ResultadosSistema(incogni, r));
                            if (!directa) // Solo resolucion paso a paso
                                lbExplicacion.Text += "\n" + incogni + " = " + Racional.AString(r);
                            vectorespropios[vectorespropios.Count - 1].Text += "\n" + incogni + " = " + Racional.AString(r);
                            cont++;
                        }
                        contador++;
                        contador2 = 0;
                        if (directa)
                            CalcularVectores2();
                        return;
                    }
                    // Si es un valor propio repetido usado anteriormente
                    else if (valorepetidousado)
                    {
                            lbExplicacion.Text = "Como se trata de un valor propio repetido usado anteriormente, el proceso de obtencion del vector propio correspondiente es el siguiente:";
                            DialogoResolucionRepetidoUsado dialogo = new DialogoResolucionRepetidoUsado(matrizpropia, anterior);
                            dialogo.ShowDialog();
                            List<Racional> auxx = new List<Racional>();
                            foreach (ResultadosSistema r in dialogo.resultados)
                            {
                                auxx.Add(new Racional(r.Resultado));
                            }
                        resultados.Add(auxx);

                        if (!directa) // Solo resolucion paso paso
                            lbExplicacion.Text = "El vector obtenido es un vector propio de la matriz. ";
                        // Construir etiqueta con el resultado
                        if (inicial.Y < lbExplicacion.Height + 20)
                            inicial = new Point(inicial.X, lbExplicacion.Height + 30);
                        vectorespropios.Add(new Label());
                        vectorespropios[vectorespropios.Count - 1].AutoSize = true;
                        if ((vectorespropios.Count - 1) != 0)
                        {
                            int desplazamiento = (vectorespropios[vectorespropios.Count - 2].Size.Height + 15);
                            inicial.Y += desplazamiento;
                        }
                        vectorespropios[vectorespropios.Count - 1].Location = inicial;
                        vectorespropios[vectorespropios.Count - 1].BackColor = Color.Transparent;
                        vectorespropios[vectorespropios.Count - 1].Font = new System.Drawing.Font("Dejavu-sans", 10);
                        vectorespropios[vectorespropios.Count - 1].ForeColor = Color.Blue;
                        Controls.Add(vectorespropios[vectorespropios.Count - 1]);
                        vectorespropios[vectorespropios.Count - 1].Show();
                        int cont = 0;
                        if (!directa)
                        {
                            vectorespropios[vectorespropios.Count - 1].Text += "Vector: " + resultados.Count + "\n";
                            foreach (Racional r in resultados[resultados.Count - 1])
                            {
                                char incogni = (char)((int)'a' + cont);
                                // Si se trata de un valor propio repetido, guardar los resultados en la lista para usarla en la siguiente repeticion
                                anterior.Add(new ResultadosSistema(incogni, r));
                                if (!directa) // Solo resolucion paso a paso
                                    lbExplicacion.Text += "\n" + incogni + " = " + Racional.AString(r);
                                vectorespropios[vectorespropios.Count - 1].Text += "\n" + incogni + " = " + Racional.AString(r);
                                cont++;
                            }
                        }
                        contador++;
                        contador2 = 0;
                        if (directa)
                            CalcularVectores2();
                        return;
                    }
                }
            }
            else // Se han calculado todos los vectores propios
            {
                contador = 0;
                contador2 = 0;
                if (!directa) // Solo resolucion paso paso
                {
                    lbExplicacion.Text = "Ya tenemos los " + orden + " vectores propios de la matriz";
                    btContinuar.Click -= Continuar_CalcularVectores2;
                     btContinuar.Click += Continuar_MostrarModal;
                }
                else if (directa) // Solo resolucion directa
                {
                    Continuar_MostrarModal(new object(), new EventArgs());
                }

            }
        }

        /// <summary>
        ///  
        /// PASA LOS VECTORES PROPIOS A ENTERO Y CONSTRUYE LA MATRIZ MODAL
        /// CON LOS VECTORES PROPIOS OBTENIDOS
        /// 
        /// </summary>

        private void Continuar_MostrarModal(object sender, EventArgs e)
        {
            if (!directa)
            {
                if (contador2 == 0) // Primer paso del metodo
                {
                    label12.Text = "";
                    lbExplicacion.Text = "Cada vector propio es una columna de la matriz modal:";
                    label11.Text = "Matriz modal";
                    foreach (List<Racional> L in resultados)
                    {
                        for (int i = 0; i < L.Count; i++)
                        {
                            modal[i, contador].BackColor = Color.Wheat;
                            modal[i, contador].Text = Racional.AString(L[i]);
                        }
                        contador++;
                    }

                    contador2++;
                    contador = 0;

                }
                else if (contador2 == 1)
                {
                    // Comprobar si todos los vectores propios tienen valores enteros
                    bool nosonenteros = false;
                    foreach (List<Racional> l in resultados)
                    {
                        foreach (Racional r in l)
                        {
                            if (!r.EsEntero())
                                nosonenteros = true;
                        }
                    }
                    vectorespropioslong = Matematicas.AlgebraLineal.PasarVectoresAEnteros(resultados);
                    if (nosonenteros)  // Si hay valores racionales en algun vector propio
                    {
                        lbExplicacion.Size = new Size(lbExplicacion.Size.Width, lbExplicacion.Size.Height + 20);
                        lbExplicacion.Text += "\nPodemos convertir los valores decimales  a entero.\nPara ello se multiplica el denominador de cada valor por el numerador de los demas: ";
                    }

                    matrizmodal = Matematicas.AlgebraLineal.VectoresAMatriz(vectorespropioslong);
                    foreach (List<long> L in vectorespropioslong)
                    {
                        for (int i = 0; i < L.Count; i++)
                        {
                            modal[i, contador].BackColor = Color.GreenYellow;
                            modal[i, contador].Text = L[i].ToString();
                        }
                        contador++;
                    }
                    contador2 = 0;
                    contador = 0;

                    btContinuar.Click -= Continuar_MostrarModal;
                    btContinuar.Click += Continuar_PruebaDiagonal;  // Siguiente metodo
                }
            }
            else if (directa)
            {
                label11.Text = "Matriz Modal";
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        modal[i, j].Text = matrizmodal[i, j].ToString();
                        modal[i, j].BackColor = Color.PaleGreen;
                    }
                }
                Continuar_PruebaDiagonal(new object(),new EventArgs ());
            }
        }

        /// <summary>
        ///  
        ///  MUESTRA LA MATRIZ DIAGONAL EN PANTALLA Y REALIZA LA PRUEBA diagonal = M^-1 * A * M 
        /// 
        /// </summary>

        private void Continuar_PruebaDiagonal(object sender, EventArgs e)
        {
            if (!directa)
            {
                if (contador2 == 0)
                {
                    Point origen = new Point(modal[0, modal.GetLength(1) - 1].Location.X + 70, modal[0, modal.GetLength(1) - 1].Location.Y);
                    lbtitulodiagonal.Show();
                    lbtitulodiagonal.Location = new Point(origen.X + 20, 150);
                    lbtitulodiagonal.AutoSize = true;
                    lbtitulodiagonal.Text = "Matriz diagonal";
                    lbtitulodiagonal.Font = new System.Drawing.Font("Dejavu Sans", 10, FontStyle.Underline);
                    Controls.Add(lbtitulodiagonal);
                    lbtitulodiagonal.Show();
                    lbExplicacion.Text += "\nLa matriz diagonal está formada por los valores propios de la matriz original: ";
                    // Construir la matriz de etiquetas para mostrar la matriz diagonal en pantalla
                    diagonal = new Label[orden, orden];
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            diagonal[i, j] = new Label();
                            diagonal[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            diagonal[i, j].Size = new Size(40, 20);
                            diagonal[i, j].BackColor = Color.LimeGreen;
                            diagonal[i, j].Location = new Point(origen.X + (50 * contador), origen.Y);
                            if (i == j)
                            {
                                diagonal[i, j].Text = raices[i].ToString();
                                
                            }
                            else
                                diagonal[i, j].Text = "0";
                            Controls.Add(diagonal[i, j]);
                            diagonal[i, j].Show();
                            contador++;
                        }
                        contador = 0;
                        origen.Y += 30;
                    }
                    contador2++;
                    contador = 0;
                }
                else if (contador2 == 1) // Paso 2 de la prueba
                {
                    lbExplicacion.Text += "\nLa matriz diagonal, es igual a: M^-1 * A * M. Vamos a hacer la prueba.";
                    label12.Text += "\ndiagonal = M^-1 * A * M\nM^-1: Matriz inversa de la matriz modal.\nA: Matriz original.\nM: Matriz modal";
                    matrizmodalinversa = Matematicas.AlgebraLineal.MatrizInversalong(matrizmodal);
                    if (matrizmodalinversa == null)
                    {
                        lbExplicacion.Text = "La inversa de la matriz modal es nula, el programa no puede continuar.";
                        btContinuar.Hide();
                    }
                    foreach (Label l in vectorespropios) // Borrar las etiquetas de cada vector propio
                    {
                        l.Dispose();
                    }
                    contador2++;
                }
                else if (contador2 == 2) // Tercer paso de la prueba
                {
                    matrizmodalracional = new Racional[matrizmodal.GetLength(1), matrizmodal.GetLength(0)];
                    for (int i = 0; i < matrizmodal.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrizmodal.GetLength(1); j++)
                        {
                            matrizmodalracional[i, j] = new Racional(matrizmodal[i, j], 1);
                        }
                    }
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            matrizracional[i, j] = Racional.StringToRacional(matriz[i, j].Text);
                        }
                    }

                    prueba1 = Matematicas.AlgebraLineal.MultiplicarMatrices(matrizracional, matrizmodalracional);

                    lbExplicacion.Text = "Multiplicamos A * M: ";
                    Point origen = new Point(diagonal[0, orden - 1].Location.X + 70, diagonal[0, orden - 1].Location.Y);
                    label13.Show();
                    label13.Location = new Point(origen.X + 20, 150);
                    label13.AutoSize = true;
                    label13.BackColor = Color.Transparent;
                    label13.Text = " A * M";
                    label13.Font = new System.Drawing.Font("Dejavu Sans", 10, FontStyle.Underline);
                    resultado = new Label[orden, orden];
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            resultado[i, j] = new Label();
                            resultado[i, j].Size = new Size(40, 20);
                            resultado[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            resultado[i, j].BackColor = Color.Orange;
                            resultado[i, j].Location = new Point(origen.X + (50 * contador), origen.Y);
                            resultado[i, j].Text = Racional.AString(prueba1[i, j]);
                            Controls.Add(resultado[i, j]);
                            resultado[i, j].Show();
                            contador++;
                        }
                        contador = 0;
                        origen.Y += 30;
                    }
                    contador = 0;
                    contador2++;
                }
                else if (contador2 == 3) // Cuarto paso de la prueba
                {
                    lbExplicacion.Text = "Calculamos la inversa de la matriz modal: ";
                    label12.Location = new Point(label12.Location.X, label12.Location.Y - 50);
                    label12.Text += "\nMatriz inversa de la modal:\n";
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            label12.Text += Racional.AString(matrizmodalinversa[i, j]) + "  ";
                        }
                        label12.Text += "\n";
                    }
                    contador2++;
                }
                else if (contador2 == 4) // Quinto paso de la prueba
                {
                    lbExplicacion.Text = "Multiplicando la inversa de la matriz modal por el resultador anterior, obtenemos la matriz diagonal";
                    lbExplicacion.Text += "\nSi se desea, despejando A, tambien se cumple que: A = M * diagonal * M^-1";
                    Racional[,] prueba2 = Matematicas.AlgebraLineal.MultiplicarMatrices(matrizmodalinversa, prueba1);
                    label13.Text = "M^-1 * A * M";
                    label13.BackColor = Color.Transparent;
                    for (int i = 0; i < orden; i++)
                    {
                        for (int j = 0; j < orden; j++)
                        {
                            resultado[i, j].BackColor = Color.Green;
                            resultado[i, j].Text = Racional.AString(prueba2[i, j]);
                        }
                    }
                    btContinuar.Hide();

                }
            }
            else if (directa)
            {
                Point origen = new Point(modal[0, modal.GetLength(1) - 1].Location.X + 70, modal[0, modal.GetLength(1) - 1].Location.Y);
                lbtitulodiagonal.Show();
                lbtitulodiagonal.Location = new Point(origen.X + 20, 150);
                lbtitulodiagonal.AutoSize = true;
                lbtitulodiagonal.Text = "Matriz diagonal";
                lbtitulodiagonal.Font = new System.Drawing.Font("Dejavu Sans", 10, FontStyle.Underline);
                Controls.Add(lbtitulodiagonal);
                lbtitulodiagonal.Show();
                // Construir la matriz de etiquetas para mostrar la matriz diagonal en pantalla
                diagonal = new Label[orden, orden];
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        diagonal[i, j] = new Label();
                        diagonal[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        diagonal[i, j].Size = new Size(40, 20);
                        diagonal[i, j].BackColor = Color.LimeGreen;
                        diagonal[i, j].Location = new Point(origen.X + (50 * contador), origen.Y);
                        if (i == j)
                        {
                            diagonal[i, j].Text = matrizdiagonal[i, j].ToString();
                        }
                        else
                            diagonal[i, j].Text = "0";
                        Controls.Add(diagonal[i, j]);
                        diagonal[i, j].Show();
                        contador++;
                    }
                    contador = 0;
                    origen.Y += 30;
                }

                label13.Text = "Modal inversa";
                origen = new Point(diagonal[0, orden - 1].Location.X + 70, diagonal[0, orden - 1].Location.Y);
                label13.Show();
                label13.Location = new Point(origen.X + 20, 150);
                label13.Font = label11.Font;
                label13.BackColor = label11.BackColor;
                matrizmodalinversa = Matematicas.AlgebraLineal.MatrizInversalong(matrizmodal);
                resultado = new Label[orden, orden];
                for (int i = 0; i < orden; i++)
                {
                    for (int j = 0; j < orden; j++)
                    {
                        resultado[i, j] = new Label();
                        resultado[i, j].Size = new Size(40, 20);
                        resultado[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        resultado[i, j].BackColor = Color.Orange;
                        resultado[i, j].Location = new Point(origen.X + (50 * contador), origen.Y);
                        resultado[i, j].Text = Racional.AString(matrizmodalinversa[i, j]);
                        Controls.Add(resultado[i, j]);
                        resultado[i, j].Show();
                        contador++;
                    }
                    contador = 0;
                    origen.Y += 30;
                }


                lbExplicacion.Text = " Matriz original = Modal * Diagonal * Modal Inversa";
                btContinuar.Hide();
            }

        }


        ///
        /// METODO AUXILIAR PARA DETERMINAR SI EL VALOR PROPIO ES UN VALOR PROPIO REPETIDO O NO LO ES
        /// 
        private bool ComprobarRepeticion()
        {
            // Determinar si se trata de un valor propio repetido o no lo es comprobando si está en la lista de valores propios repetidos
       
            bool esrepetido = false;
            for (int i = 0; i < repes.Count; i++)
            {
                if (raices[contador] == repes[i].valorpropio)
                {
                    esrepetido = true;
                }
            }
            return esrepetido;
        }


    }
}

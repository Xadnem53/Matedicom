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
    public partial class DialogoResolucionVpNoUsado : Form
    {
        Racional[,] matrizpropia;  // Matriz propia del valor propio que se pasa como argumento
        int contador = 0;
        int contador2 = 0;
        Racional[,] triangular; // Matriz propia reducida
        Sistema sistemapropio; // Sistema que se construye con la matriz triangular
        Sistema sistema; // Sistema que se construye con la matriz propia
        List<char> variables = new List<char>(); // Variables del sistema

       public List<ResultadosSistema> resultados = new List<ResultadosSistema>();
     
        List<ResultadosSistema> ordenada = new List<ResultadosSistema>();

        public DialogoResolucionVpNoUsado(Racional[,] propia)
        {
            matrizpropia = propia;
            InitializeComponent();
            ResolucionMatrizValoresRepetidosIndicaciones();
        }

        /// <summary>
        /// 
        ///  AVANZA UN PASO EN EL PROCESO DE DETERMINAR EL VECTOR PROPIO
        ///
        /// </summary>

        private void btContinuar_Click(object sender, EventArgs e)
        {
            contador++;
            ResolucionMatrizValoresRepetidosIndicaciones();
        }



        /// <summary>
        /// 
        ///  DA LOS RESULTADOS DE UN SISTEMA DE ECUACIONES INDETERMINADO ( EL TERMINO INDEPENDIENTE DE TODAS LAS ECUACIONES
        ///   ES CERO ) A PARTIR DE UNA MATRIZ LA CUAL TIENE VALORES PROPIOS REPETIDOS. ESTE METODO DEVUELVE UNA LISTA DE 
        ///  VALORES RACIONALES ( VECTOR PROPIO ) PERTENECIENTE A LA PRIMERA REPETICION DEL VALOR PROPIO REPETIDO.
        ///
        /// </summary>

        private void ResolucionMatrizValoresRepetidosIndicaciones()
        {
            if (contador == 0) // Convertir en triangular la matriz copia de la matriz propia
            {
               // lbDesarrollo.Text = "";
                triangular = Matematicas.AlgebraLineal.MatrizCopia(matrizpropia); // Copia de la matriz propia
               // lbExplicacion.Text = "Convertimos la matriz propia, en una matriz triangular por medio de operaciones elementales de las matrices.";
                rtbDesarrollo.Text = "Convertimos la matriz propia, en una matriz triangular por medio de operaciones elementales de las matrices.\n\n";
		Matematicas.AlgebraLineal.MatrizATriangular(triangular);
		/*
                for (int i = 0; i < triangular.GetLength(0); i++)
                {
                    for (int j = 0; j < triangular.GetLength(0); j++)
                    {
                        lbDesarrollo.Text += triangular[i, j].ToString() + "   ";
                    }
                    lbDesarrollo.Text += "\n";
                }
		*/
		//lbDesarrollo.Text += Matematicas.AlgebraLineal.MatrizAString(triangular);
		rtbDesarrollo.Text += Matematicas.AlgebraLineal.MatrizAString(triangular);
            }
            else if (contador == 1) //Reconstruir el sistema con la matriz triangular
            {
                sistema = new Sistema(matrizpropia); // Sistema antes de ser reducido
               // lbExplicacion.Text = "Reconstruimos el sistema segun la matriz triangular.";
		  rtbDesarrollo.Text += "\nReconstruimos el sistema segun la matriz triangular.\n";
                sistemapropio = new Sistema(triangular); // Sistema reducido
                sistemapropio.BorrarEcuacionesNulas();
                sistemapropio.SimplificarSistema();
                sistemapropio.PasarNumerosADerecha();
                sistemapropio.OrdenarSistema();
              //  lbDesarrollo.Text = sistemapropio.ToString();
		  rtbDesarrollo.Text += "\n" + sistemapropio.ToString();
            }
            // Mientras queden ecuaciones en el sistema asignar Valor a las variables
            else if (contador >= 2 && (sistemapropio.CantidadDeEcuaciones > 0 && resultados.Count < sistema.VariablesDelSistema.Count))
            {
                if (contador2 == 0)
                    Despejar();
                else if (contador2 == 1)
                    AsignarValores();
                else if (contador2 == 2)
                    Sustituir();
            }
                else // Cuando no queden ecuaciones en el sistema
                {

                    // Si falta una variable por asignar valor, asignarle valor 1
                    List<char> copiavariables = new List<char>(sistema.VariablesDelSistema); // Lista copia de la lista de variables
                    for (int i = 0; i < resultados.Count; i++) // Borrar de la lista copia cada variable que esté en la lista de resultados 
                    {
                        copiavariables.Remove(resultados[i].Variable);
                    }
                    foreach (char c in copiavariables) // Si quedan variables en la lista copia asignarle valor 1
                        resultados.Add(new ResultadosSistema(c, 1));

                    // Ordenar los resultados por la variable
                    List<ResultadosSistema> auxx = new List<ResultadosSistema>();
                    foreach (char c in sistema.VariablesDelSistema)
                    {
                        for (int i = 0; i < resultados.Count; i++)
                        {
                            if (resultados[i].Variable == c)
                                auxx.Add(new ResultadosSistema(resultados[i].Variable, resultados[i].Resultado));
                        }
                    }
                    resultados = auxx;
                   // lbResultados.Text = "";
                    foreach (ResultadosSistema r in resultados)
                    {
                        //lbResultados.Text += "\n" + r.ToString();
			  rtbDesarrollo.Text += "\n" + r.ToString();
                    }
                   // lbExplicacion.Text = "El vector propio se construye con los resultados obtenidos.";
                   // lbDesarrollo.Text = "";
                    rtbDesarrollo.Text += "\nEl vector propio se construye con los resultados obtenidos.";
		    btContinuar.BackColor = Color.Coral;
                    btContinuar.Text = "Salir";
                    btContinuar.Click -= btContinuar_Click;
                    btContinuar.Click += Salir;
                }
            }


        /// <summary>
        /// 
        ///  METODO AUXILIAR PARA DESPEJAR LA VARIABLE MAS A LA IZQUIERDA DE LA ULTIMA ECUACION DEL 
        ///  SISTEMA
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Despejar()
        {
            sistemapropio.SustituirValores(resultados);
            sistemapropio.SimplificarSistema();
            sistemapropio.PasarNumerosADerecha();
            sistemapropio.BorrarEcuacionesNulas();
         
            // Despejar la variable mas a la izquierda en la ultima ecuacion 
           // lbExplicacion.Text = "Despejamos la variable mas a la izquierda de la última ecuacion del sistema.";
       	       rtbDesarrollo.Text += "\nDespejamos la variable mas a la izquierda de la última ecuacion del sistema.";

            // Si la ultima ecuacion tiene un termino a cada lado, reducir el coeficiente del termino del lado izquierdo a la unidad
            if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosIzquierda == 1 && sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosDerecha == 1)
            {
                Racional coeficiente = new Racional(sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Coeficiente);
                sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Coeficiente = new Racional(1, 1);
                Termino derecho = new Termino(sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(0));
                derecho = new Termino(derecho.Coeficiente / coeficiente, derecho.Variables, derecho.Exponentes);
                sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).BorrarLadoDerecho();
                sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).AñadirTerminoDerecha(derecho);
            }
            /*    
                // Si en la ultima ecuacion del sistema hay mas de un termino en el lado derecho, reducir el coeficiente del termino del lado izquierdo a cero
                if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosIzquierda > 0)
                {
                    bool variableobtenida = false;
                    int indice = 0;
                    while (!variableobtenida)
                    {
                        if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(indice).Coeficiente.Numerador != 0)
                        {
                            variable = sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(indice).Variables[0];
                            variableobtenida = true;
                        }
                        else
                            indice++;
                    }
             
                    sistemapropio.DespejarVariableIndeterminada(variable);
                }
                */
            else
            {
                sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).Despejar();
                Racional coeficiente = new Racional(sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Coeficiente);
                sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Coeficiente = new Racional(1, 1);
                List<Termino> auxx = new List<Termino>();
                for (int i = 0; i < sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosDerecha; i++)
                {
                    auxx.Add(new Termino(sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(i).Coeficiente / coeficiente, sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(i).Variables, sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(i).Exponentes));
                }
                sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).BorrarLadoDerecho();
                foreach (Termino t in auxx)
                    sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).AñadirTerminoDerecha(t);
            }
            // Eliminar los ceros redundantes del lado derecho 
            List<Termino> aux = new List<Termino>();
        //    int cuentaceros = 0;
            for (int i = 0; i < sistemapropio.CantidadDeEcuaciones; i++)
            {
                aux.Clear();
                if (sistemapropio.ObtenerEcuacion(i).CantidadDeTerminosDerecha > 1)
                {
                    foreach (Termino t in sistemapropio.ObtenerEcuacion(i).ObtenerLadoDerecho.Terminos)
                    {
                        if (t.Coeficiente != 0)
                            aux.Add(new Termino(t));
                    }
                    sistemapropio.ObtenerEcuacion(i).BorrarLadoDerecho();
                    foreach (Termino t in aux)
                        sistemapropio.ObtenerEcuacion(i).AñadirTerminoDerecha(t);
                }
            }
             // Si no hay terminos en el lado derecho, añadir un cero
            if( sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones-1).CantidadDeTerminosDerecha == 0)
            sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).AñadirTerminoDerecha(new Termino(0, ' ', 1));
          
           // lbDesarrollo.Location = new Point(lbExplicacion.Location.X, lbExplicacion.Location.Y + lbExplicacion.Height + 20);
            //lbDesarrollo.Text = sistemapropio.ToString();
            rtbDesarrollo.Text += "\n" + sistemapropio.ToString()+"\n\n";
	    rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		rtbDesarrollo.ScrollToCaret();
	      contador2++;
        }

          /// <summary>
        /// 
        ///  METODO AUXILIAR PARA ASIGNAR VALOR A LAS VARIABLES  EN LA ULTIMA ECUACION DEL SISTEMA EN EL QUE
        ///  ANTERIORMENTE SE HA DESPEJADO UNA VARIABLE EN EL LADO IZQUIERDO CON EL METODO DESPEJAR
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void AsignarValores()
        {
            //Si en la ultima ecuacion del sistema hay un termino en el lado izquierdo y uno en el derecho
            if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosIzquierda == 1 && sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosDerecha == 1)
            {
                // Si el termino del lado izquierdo es una variable y el del lado derecho tambien
                if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Variables[0] != (char)32 && sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(0).Variables[0] != (char)32)
                {
                    // Asignar valor 1 a la variable de la izquierda, y 1 / por su coeficiente a la variable de la derecha
                    resultados.Add(new ResultadosSistema(sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Variables[0], 1));
                    resultados.Add(new ResultadosSistema(sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(0).Variables[0], sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Coeficiente / sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(0).Coeficiente));
                  //  lbExplicacion.Text = "Como en la última ecuación del sistema hay un termino a cada lado, asignamos valor 1 a la variable de la izquierda y 1 partido por su coeficiente a la variable de la derecha.";
                    rtbDesarrollo.Text += "\nComo en la última ecuación del sistema hay un termino a cada lado, asignamos valor 1 a la variable de la izquierda y 1 partido por su coeficiente a la variable de la derecha.";
		    //lbResultados.Text = "";
                    foreach (ResultadosSistema r in resultados)
                    {
                        //lbResultados.Text += "\n" + r.ToString();
			rtbDesarrollo.Text += "\n" + r.ToString();
                    }
		  rtbDesarrollo.Text += "\n\n";
		  rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		  rtbDesarrollo.ScrollToCaret();
                }
                // Si el termino del lado izquierdo es una variable y el del lado derecho es un numero
                else if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Variables[0] != (char)32 && sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(0).Variables[0] == (char)32)
                {
                    resultados.Add(new ResultadosSistema(sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Variables[0], sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(0).Coeficiente / sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Coeficiente));
		    rtbDesarrollo.Text += "\nEl valor de la variable de la última ecuación del sistema, queda determinado.";
                    //lbExplicacion.Text = "El valor de la variable de la última ecuación del sistema, queda determinado.";
                    //lbResultados.Text = "";
                    foreach (ResultadosSistema r in resultados)
                    {
                       // lbResultados.Text += "\n" + r.ToString();
			rtbDesarrollo.Text += "\n" + r.ToString();
                    }
		 rtbDesarrollo.Text += "\n\n";
		  rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		  rtbDesarrollo.ScrollToCaret();
                }
               // lbDesarrollo.Location = new Point(lbExplicacion.Location.X, lbExplicacion.Location.Y + lbExplicacion.Height + 20);
                //lbDesarrollo.Text = sistemapropio.ToString();
		rtbDesarrollo.Text += "\n" + sistemapropio.ToString() + "\n";
		rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		rtbDesarrollo.ScrollToCaret();
            }
            // Si en la ultima ecuacion, hay mas de un termino en el lado derecho, asignar valor 1 a la variable del termino de
            // de la izquierda, sustituir y simplificar de nuevo
            else if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosDerecha > 1)
            {
                //lbExplicacion.Text = "Como en el lado derecho de la ultima ecuacion del sistema hay mas de un termino, asignamos valor 1 a la variable despejada.";
                rtbDesarrollo.Text += "\nComo en el lado derecho de la ultima ecuacion del sistema hay mas de un termino, asignamos valor 1 a la variable despejada.";
		char variable = sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Variables[0];
                resultados.Add(new ResultadosSistema(variable, 1));
            }
           // lbResultados.Text = "";
            foreach (ResultadosSistema r in resultados)
            {
                //lbResultados.Text += "\n" + r.ToString();
		  rtbDesarrollo.Text += "\n" + r.ToString();
            }
	    rtbDesarrollo.Text += "\n\n\n";
	    rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 	    rtbDesarrollo.ScrollToCaret();
           // lbDesarrollo.Location = new Point(lbExplicacion.Location.X, lbExplicacion.Height + 50);
            contador2++;
        }


        /// <summary>
        /// 
        ///  METODO AUXILIAR QUE SUSTITUYE EL VALOR DE LAS VARIABLES OBTENIDO ANTERIORMENTE Y 
        ///  SIMPLIFICA EL SISTEMA
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Sustituir()
        {
            //lbExplicacion.Text = "Sustituimos el valor de las variables cuyo valor esta determinado, y simplificamos el sistema.";
            rtbDesarrollo.Text += "\nSustituimos el valor de las variables cuyo valor esta determinado, y simplificamos el sistema.";
	    // Eliminar las variables con valor cero y simplificar el sistema
            sistemapropio.SustituirValores(resultados);
            sistemapropio.SimplificarSistema();
            sistemapropio.PasarNumerosADerecha();
            sistemapropio.BorrarEcuacionesNulas();
            //lbDesarrollo.Text = sistemapropio.ToString();
	rtbDesarrollo.Text += "\n" + sistemapropio.ToString();
            contador2 = 0;
            for (int i = 0; i < sistemapropio.CantidadDeEcuaciones; i++)
            {
                for (int j = 0; j < sistemapropio.ObtenerEcuacion(i).CantidadDeTerminosIzquierda; j++)
                {
                    if (sistemapropio.ObtenerEcuacion(i).ObtenerTerminoIzquierda(j).Coeficiente.Numerador == 0)
                    {
                        sistemapropio.ObtenerEcuacion(i).ObtenerTerminoIzquierda(j).Variables[0] = '$';
                    }
                }
                List<Termino> auxi = new List<Termino>();
                for (int j = 0; j < sistemapropio.ObtenerEcuacion(i).CantidadDeTerminosIzquierda; j++)
                {
                    if (sistemapropio.ObtenerEcuacion(i).ObtenerTerminoIzquierda(j).Variables[0] != '$')
                        auxi.Add(new Termino(sistemapropio.ObtenerEcuacion(i).ObtenerTerminoIzquierda(j)));
                }
                sistemapropio.ObtenerEcuacion(i).BorrarLadoIzquierdo();
                foreach (Termino t in auxi)
                    sistemapropio.ObtenerEcuacion(i).AñadirTerminoIzquierda(new Termino(t));

            }
	   rtbDesarrollo.Text += "\n\n";
	    rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 	    rtbDesarrollo.ScrollToCaret();
        }
        

        private void Salir(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}

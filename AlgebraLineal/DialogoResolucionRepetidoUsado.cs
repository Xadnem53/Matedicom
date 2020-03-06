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
    public partial class DialogoResolucionRepetidoUsado : Form
    {
        Racional[,] matrizpropia;
        List<ResultadosSistema> anterior; // Lista con los resultados de la repeticion anterior del valor propio
        public List<ResultadosSistema> resultados = new List<ResultadosSistema>();// Lista donde se meteran los resultados del sistema
        int contador = 0;
                int contador2 = 0;
        
         Sistema sistemapropio; // Sistema que se construira con la matriz propia.
         Sistema sistema; // Sistema construido con la matriz propia
        Racional[,] triangular;
   

        public DialogoResolucionRepetidoUsado(Racional [,] matriz,List <ResultadosSistema>resultadoanterior)
        {
            matrizpropia = matriz;
            anterior = resultadoanterior;
            InitializeComponent();
            ResolverRepetidoUsado();
        }

         /// <summary>
        ///
        ///  DA LOS RESULTADOS DE UN SISTEMA DE ECUACIONES INDETERMINADO ( EL TERMINO INDEPENDIENTE DE TODAS LAS ECUACIONES
        ///   ES CERO ) A PARTIR DE UNA MATRIZ LA CUAL TIENE VALORES PROPIOS REPETIDOS. ESTE METODO DEVUELVE UNA LISTA DE 
        ///  VALORES RACIONALES ( VECTOR PROPIO ) PERTENECIENTE A CADA UNA DE LAS REPETICIONES DEL VALOR PROPIO EXCEPTO LA
        ///  PRIMERA APARICION DEL VALOR PROPIO REPETIDO QUE LA DA EL METODO ANTERIOR "ResolucionMatrizValoresRepetidos"
        ///   ( METODO CON EXPLICACIONES PASO A PASO )
        ///
        /// </summary>

        private void ResolverRepetidoUsado()
        {
            //lbResultados.Location = new Point(lbExplicacion.Width + 50, lbExplicacion.Location.Y );
            if (contador == 0) // 1er Paso de la resolucion
            {
                sistema = new Sistema(matrizpropia); // sistema antes de reducir la matriz a triangular
               // lbDesarrollo.Text = "";
               // lbExplicacion.Text = "Reducimos la matriz propia a una matriz triangular por medio de operaciones elementales de las matrices: ";
                rtbDesarrollo.Text = "Reducimos la matriz propia a una matriz trangular por medio de operaciones elementales de las matrices:\n";
		triangular = Matematicas.AlgebraLineal.MatrizATriangular(matrizpropia);
/*
                for (int i = 0; i < triangular.GetLength(0); i++)
                {
                    for (int j = 0; j < triangular.GetLength(1); j++)
                    {
                        //lbDesarrollo.Text += triangular[i, j].ToString() + "  ";
			  rtbDesarrollo.Text += triangular[i, j].ToString() + "  ";
                    }
                   // lbDesarrollo.Text += "\n";
			rtbDesarrollo.Text += "\n"
                }
*/
		//lbDesarrollo.Text += Matematicas.AlgebraLineal.MatrizAString(triangular);
		  rtbDesarrollo.Text += Matematicas.AlgebraLineal.MatrizAString(triangular);
		rtbDesarrollo.Text += "\n\n";
		rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		rtbDesarrollo.ScrollToCaret();
            }

            else if (contador == 1) // 2º Paso de la resolucion
            {
                sistemapropio = new Sistema(triangular); // Sistema a partir de la matriz triangular
                sistemapropio.BorrarEcuacionesNulas();
                sistemapropio.SimplificarSistema();
                sistemapropio.PasarNumerosADerecha();
                sistemapropio.OrdenarSistema();
		rtbDesarrollo.Text += "\nConstruimos un sistema de ecuaciones con la matriz triangular obtenida."; 
               // lbExplicacion.Text = "Construimos un sistema de ecuaciones con la matriz triangular obtenida.";
               // lbDesarrollo.Text = sistemapropio.ToString();
		rtbDesarrollo.Text += "\n" + sistemapropio.ToString();
		rtbDesarrollo.Text += "\n\n";
		rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		rtbDesarrollo.ScrollToCaret();
            }

            // Mientras queden ecuaciones en el sistema asignar Valor a las variables
            else if (contador >= 2 && (sistemapropio.CantidadDeEcuaciones > 0 && resultados.Count < sistema.VariablesDelSistema.Count)) 
            {
                if(contador2 == 0)
                    Despejar();
              else if (contador2 == 1)
                    AsignarValores();
                else if (contador2 == 2)
                    Sustituir();
            }

                else // Cuando no queden ecuaciones en el sistema
                {
                    // Si falta una variable por asignar valor, asignarle valor 1
                    sistema = new Sistema(matrizpropia);
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
                  //  lbResultados.Text = "";
                    foreach (ResultadosSistema r in resultados)
                    {
                        //lbResultados.Text += "\n" + r.ToString();
			rtbDesarrollo.Text +=  "\n" + r.ToString();
                    }

                   // lbExplicacion.Text = "El vector propio se construye con los resultados obtenidos.";
                   // lbDesarrollo.Text = "";
                    rtbDesarrollo.Text += "\nEl vector propio se construye con los resultados obtenidos.";
		    rtbDesarrollo.Text += "\n\n";
		    rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		    rtbDesarrollo.ScrollToCaret();
		    btContinuar.BackColor = Color.Coral;
                    btContinuar.Text = "Salir";
                    btContinuar.Click -= btContinuar_Click;
                    btContinuar.Click += btSalir_Click;
		 
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
            // Despejar la variable mas a la izquierda en la ultima ecuacion 
	      rtbDesarrollo.Text += "\nDespejamos la variable mas a la izquierda de la última ecuacion del sistema.";
           // lbExplicacion.Text = "Despejamos la variable mas a la izquierda de la última ecuacion del sistema.";
          //  char variable = '\0';
            /*
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
            // Eliminar los ceros del lado derecho 
            List<Termino> aux = new List<Termino>();
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
            if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosDerecha == 0)
                sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).AñadirTerminoDerecha(new Termino(0, ' ', 1));

           // lbDesarrollo.Location = new Point(lbExplicacion.Location.X, lbExplicacion.Location.Y + lbExplicacion.Height + 20);
            rtbDesarrollo.Text += sistemapropio.ToString();
	    rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		rtbDesarrollo.ScrollToCaret();

	  //  lbDesarrollo.Text = sistemapropio.ToString();
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
                    //lbExplicacion.Text = "Como en la última ecuación del sistema hay un termino a cada lado, asignamos valor 1 a la variable de la izquierda y 1 partido por su coeficiente a la variable de la derecha.";
                    rtbDesarrollo.Text += "\nComo en la última ecuación del sistema hay un termino a cada lado, asignamos valor 1 a la variable de la izquierda y 1 partido por su coeficiente a la variable de la derecha.";
		    List<char> variables = sistemapropio.VariablesDelSistema;
                   // lbResultados.Text = "";
                    foreach (ResultadosSistema r in resultados)
                    {
                        //lbResultados.Text += "\n" + r.ToString();
			rtbDesarrollo.Text += "\n" + r.ToString();
                    }
                  
                }
                // Si el termino del lado izquierdo es una variable y el del lado derecho es un numero
                else if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Variables[0] != (char)32 && sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(0).Variables[0] == (char)32)
                {
                    resultados.Add(new ResultadosSistema(sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Variables[0], sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoDerecha(0).Coeficiente / sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Coeficiente));
                   // lbExplicacion.Text = "El valor de la variable de la última ecuación del sistema, está determinado.";
                    rtbDesarrollo.Text += "\nEl valor de la variable de la última ecuación del sistema, está determinado.";
                    foreach (ResultadosSistema r in resultados)
                    {
                        //lbResultados.Text += "\n" + r.ToString();
			  rtbDesarrollo.Text += "\n" + r.ToString();
                    }
                }
               // lbDesarrollo.Location = new Point(lbExplicacion.Location.X, lbExplicacion.Location.Y + lbExplicacion.Height + 20);
                rtbDesarrollo.Text += "\n"+sistemapropio.ToString();
		 // lbDesarrollo.Text = sistemapropio.ToString();
            }
            // Si en la ultima ecuacion, hay mas de un termino en el lado derecho, asignar valor 0 a la variable del termino de
            // la izquierda, sustituir y simplificar de nuevo ( en el metodo para valores repetidos no usados, la variable se sustituye por 1)
            else  if (sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).CantidadDeTerminosDerecha > 1)
            {
		rtbDesarrollo.Text += "\nComo se trata de un valor propio repetido, si la variable tiene valor 1 en la repeticion anterior del valor propio, le asignamos ahora valor 0 y valor 1 si en la repeticion anterior tiene valor cero.";
               // lbExplicacion.Text = "Como se trata de un valor propio repetido, si la variable tiene valor 1 en la repeticion anterior del valor propio, le asignamos ahora valor 0 y valor 1 si en la repeticion anterior tiene valor cero.";
                char variable = sistemapropio.ObtenerEcuacion(sistemapropio.CantidadDeEcuaciones - 1).ObtenerTerminoIzquierda(0).Variables[0];
                // Comparar el valor anterior de la variable, si era 0 asignar 1 y si era 1 asignar cero
                Racional valoranterior = null;
                foreach (ResultadosSistema r in anterior)
                {
                    if (r.Variable == variable)
                        valoranterior = r.Resultado;
                }

                if (valoranterior.Numerador == 0)
                {
                    resultados.Add(new ResultadosSistema(variable, 1));
                }
                else
                {
                    resultados.Add(new ResultadosSistema(variable, 0));
                }
               // lbDesarrollo.Location = new Point(lbExplicacion.Location.X, lbExplicacion.Height + 50);
               // lbResultados.Text = "";
                foreach (ResultadosSistema r in resultados)
                {
                    //lbResultados.Text += "\n" + r.ToString();
			rtbDesarrollo.Text += "\n" + r.ToString();
                }
            }
	     rtbDesarrollo.Text += "\n\n";
	     rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 	     rtbDesarrollo.ScrollToCaret();
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
	    rtbDesarrollo.Text += "\n" + sistemapropio.ToString();
           // lbDesarrollo.Text = sistemapropio.ToString();
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
            contador2 = 0;
        }


        private void btContinuar_Click(object sender, EventArgs e)
        {
            contador++;
            ResolverRepetidoUsado();
        }
        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Matematicas;


	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	//
	// CAMBIADA LA ETIQUETA PARA MOSTRAR LOS PASOS DE LA RESOLUCION POR RICHTEXTBOX CON SCROLL BARS
	// 13/9/2016
	//
	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

namespace AlgebraLineal
{
    public partial class DialogoDiagonalizable : Form
    {
        Racional[,] matriz;
        Matematicas.AlgebraLineal.Valorrepetido repetido;
        public  bool diagonalizable = true;
        Label[,] matrizlb;
        Point Origen = new Point (100,120);
       // int contador = 0;
        int paso = 0; 
         Racional[,] matrizpropia;
         Racional rango = 0;

         public DialogoDiagonalizable(Racional[,] matriu, Matematicas.AlgebraLineal.Valorrepetido repe)
        {
            matriz = matriu;
            repetido = repe;
            InitializeComponent();
            matrizlb = new Label[matriz.GetLength(0), matriz.GetLength(0)];
            DiagonalizableIndicaciones();
        }

        /// <summary>
        /// 
        /// AVANZA EN LA DETERMINACION DE DIAGONALIZACION DE LA MATRIZ CADA VEZ QUE SE PULSA EL
        /// BOTON CONTINUAR
        ///
        /// </summary>

        private void AvanzarPaso(object sender, EventArgs e)
        {
            paso++;
            DiagonalizableIndicaciones();
        }

        /// <summary>
        /// 
        ///  DEVUELVE TRUE SI LA MATRIZ DADA ES DIAGONALIZABLE. COMPROBANDO
        ///  QUE EL RANGO DE LAS MATRICES PROPIAS DE UNA MATRIZ ES IGUAL
        ///  A EL VALOR DEL ORDEN DE LA MATRIZ MENOS LA CANTIDAD DE VECES
        ///  QUE EL VALOR PROPIO SE REPITE ( METODO CON INDICACIONES PASO A PASO
        ///
        /// </summary>

        private void DiagonalizableIndicaciones()
        {
            if (paso == 0)
            {
                diagonalizable = true;
		lbExplicacion.Visible = false;
		rtbDesarrollo.Visible = true;
		rtbDesarrollo.Text = "Para empezar construimos la matriz propia, restando a los elementos en la diagonal principal, el valor propio repetido, en este caso: " + repetido.valorpropio.ToString();
	        
		// Construir el string de la matriz con el valor propio repetido restado y mostrarla
		string matrizs = "";
		 for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz.GetLength(0); j++)
                    {
			  if (i == j)
                           matrizs += Racional.AString(matriz[i, j]) + " - " + repetido.valorpropio.ToString() + "        ";
                        else
                            matrizs += Racional.AString(matriz[i, j]) + "        ";
		    }
		    matrizs += "\n\n";
		}   
		
		rtbDesarrollo.Text += "\n\n" + matrizs;

		/*
                lbExplicacion.Text = "Para empezar construimos la matriz propia, restando a los elementos en la diagonal principal el valor propio repetido, en este caso: " + repetido.valorpropio.ToString();
		
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz.GetLength(0); j++)
                    {
                        matrizlb[i, j] = new Label();
                        matrizlb[i, j].Location = new Point(Origen.X + (contador * 60), Origen.Y);
                        matrizlb[i, j].Size = new Size(50, 30);
                        Controls.Add(matrizlb[i, j]);
                        matrizlb[i, j].Font = new Font(matrizlb[i, j].Font.Name, 12);
                        matrizlb[i, j].Show();
                        if (i == j)
                            matrizlb[i, j].Text = Racional.AString(matriz[i, j]) + " - " + repetido.valorpropio.ToString();
                        else
                            matrizlb[i, j].Text = Racional.AString(matriz[i, j]);
                        contador++;
                    }
                    contador = 0;
                    Origen.Y += 45;
                }
		
		*/
		/*
		Label lbMatriz = new Label();
		lbMatriz.Location = new Point(Origen.X,Origen.Y);
		lbMatriz.AutoSize = true;
		lbMatriz.Font = new Font("Dejavu-Sans",10);
		lbMatriz.Visible = true;
		Controls.Add(lbMatriz);
		lbMatriz.Text = Matematicas.AlgebraLineal.MatrizAString(matriz);
		*/
                matrizpropia = Matematicas.AlgebraLineal.MatrizPropia(matriz, repetido.valorpropio);
            }

            else if (paso == 1)
            {
	     /*
                lbExplicacion.Text = "Realizando la resta de la diagonal mayor, la matriz propia es: ";
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz.GetLength(0); j++)
                    {
                        if( i == j)
                        matrizlb[i, j].Text = Racional.AString(matriz[i, j] - repetido.valorpropio);
                    }
                }
	      */
		rtbDesarrollo.Text += "\n\n Realizando la resta de la diagonal mayor, la matriz propia es: ";
		long valorprop = (long)repetido.valorpropio.Numerador/repetido.valorpropio.Denominador;
		Racional [,] matrizpropia = Matematicas.AlgebraLineal.MatrizValorPropio(matriz,valorprop);
		string matrizpropias = Matematicas.AlgebraLineal.MatrizAString(matrizpropia);
		rtbDesarrollo.Text += "\n\n" + matrizpropias + "\n\n";
		rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		rtbDesarrollo.ScrollToCaret();
            }
            else if (paso == 2)
            {
                rango = Matematicas.AlgebraLineal.Rango(matrizpropia);
		rtbDesarrollo.Text += "El rango de la matriz propia es: " + Racional.AString(rango) + "\n\n\n";
		rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		rtbDesarrollo.ScrollToCaret();
                //lbExplicacion.Text = "El rango de la matriz propia es: " + Racional.AString(rango) + ;
            }
            else if (paso == 3)
            {

                Racional ordenmatriz = matriz.GetLength(0);
                Racional repeticiones = repetido.repeticiones;
                if (rango > (ordenmatriz - repeticiones) || rango < (ordenmatriz - repeticiones))
                {
                    //lbExplicacion.Text = "Rango de la matriz propia del valor propio repetido " + repetido.valorpropio.ToString() + " es de: " + Racional.AString(rango) + " Y el resultado del orden de la matriz menos la cantidad de repeticiones es de: " + matriz.GetLength(0) + " - " + repetido.repeticiones + " = " + Racional.AString(ordenmatriz - repeticiones) + "\nPor lo tanto la matriz no es diagonalizable.";
                    rtbDesarrollo.Text += "Rango de la matriz propia del valor propio repetido " + repetido.valorpropio.ToString() + " es de: " + Racional.AString(rango) + " Y el resultado del orden de la matriz menos la cantidad de repeticiones es de: " + matriz.GetLength(0) + " - " + repetido.repeticiones + " = " + Racional.AString(ordenmatriz - repeticiones) + "\nPor lo tanto la matriz no es diagonalizable.\n";
		    diagonalizable = false;
                    btContinuar.Hide();
                    btSalir.Show();
                }
                else
                {
                    //lbExplicacion.Text = "Rango de la matriz propia del valor propio: " + repetido.valorpropio.ToString() + "   Es de: " + Racional.AString(rango) + "\nEl resultado del orden de la matriz menos la cantidad de repeticiones es de: " + matriz.GetLength(0) + " - " + repetido.repeticiones + " = " + Racional.AString(ordenmatriz - repeticiones);
                    rtbDesarrollo.Text += " Rango de la matriz propia del valor propio: " + repetido.valorpropio.ToString() + "   Es de: " + rango.ToString() + "\nEl resultado del orden de la matriz menos la cantidad de repeticiones es de: " + matriz.GetLength(0) + " - " + repetido.repeticiones + " = " + (ordenmatriz - repeticiones).ToString() ;
		    rtbDesarrollo.Text += "\nPor lo tanto, la matriz es diagonalizable\n\n\n\n";
		    //lbExplicacion.Text += "\nPor lo tanto, la matriz es diagonalizable";
                    btContinuar.Hide();
                    btSalir.Show();
                }
		rtbDesarrollo.SelectionStart = rtbDesarrollo.Text.Length;
 		rtbDesarrollo.ScrollToCaret();
            }
        }



            private void Salir ( object sender,EventArgs e)
            {
                this.Dispose();
            }


    }
}

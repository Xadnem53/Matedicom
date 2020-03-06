using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Matematicas;
using Punto_y_Vector;
using Recta_y_plano;


namespace Matedicom
{
    public  partial class FormularioBase : Form
    {
        public FormularioBase()
        {
            InitializeComponent();
            btCerrar.Location = new Point(1100, 10);
            btSalir.Location = new Point(btCerrar.Location.X, btCerrar.Location.Y + btCerrar.Size.Height + 10);
            btNuevo.Location = new Point(btSalir.Location.X, btSalir.Location.Y + btSalir.Size.Height + 10);
        }
        ///////////////////////////
        //
        // Atributos para algebra lineal
        //
        //
        protected int orden = 0; // Orden de una matriz
        protected int filas = 0; // Filas de una matriz 
        protected int columnas = 0; // Columnas una la matriz
        protected int filasmultiplicadora = 0; // Filas de otra matriz que multiplica a la anterior
        protected int columnasmultiplicadora = 0;  // Columnas de otra matriz que multiplica a la anterior
        protected int paso = 0; // Paso en el que se encuentra la resolucion
        protected int contador = 0; // Contador para diferentes usos
        protected int descontador = 0; // Contador regresivo para diferentes usos

      protected  TextBox[,] matriz = null; // matriz de cajas de texto para una matriz
     protected   TextBox[,] matrizmultiplicadora = null; // matriz de cajas de texto para otra matriz que multiplica a la anterior
        
        protected Label[,] matrizresultado = null; // matriz de cajas de texto con el resultado del producto de las anterioes

        protected Racional[,] matrizracional = null; // matriz racional 
        protected Racional[,] matrizmultiplicadoraracional = null;// matriz racional que multiplicara a la anterior
        protected Racional[,] matrizresultadoracional = null; // matriz racional con el resultado de las dos anteriores

        protected int filaactual = 0; // Para llevar la cuenta de la fila sobre la que se esta operando
        protected int columnaactual = 0; // Para llevar la cuenta de la columna cobre la se esta operando

        protected bool directa; // Si la resolucion es directa, es true
        protected bool defecto; // Es true si se pulsa el boton de valores por defecto

        protected Sistema sistemaNN; // Sistema de n ecuaciones y n incognitas
        protected Sistema sistemaNM; // Sistema de n ecuaciones y m incognitas
        protected Sistema sistem; // // Sistema de ecuaciones con variables en los dos lados
        protected Ecuacion resultado; // Sistema de ecuaciones dos lados obtenido tras operaciones 

        protected Polinomio polinom; // Polinomio para diferentes usos
        protected List<Termino> terminos;// Lista de terminos para diferentes usos 


        ////////////////////////////////////
        //
        // METODOS 
        //
        //
        public virtual void Cargar(object sender, EventArgs e)
        {
        }


        /// <summary>
        ///  
        ///SALE DEL PROGRAMA Y LIBERA TODOS LOS RECURSOS
        /// 
        /// </summary>
        private void btCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.Environment.Exit(0);
        }

        /// <summary>
        ///  
        /// CIERRA EL FORMULARIO ACTUAL Y ABRE UN NUEVO MENU DE ALGEBRA LINEAL
        /// 
        /// </summary>

        public virtual void btSalir_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>

        public virtual  void btNuevo_Click(object sender, EventArgs e)
       {
          
        }

        /// <summary>
        /// 
        ///  PONE EL ATRIBUTO defecto A TRUE PARA QUE SE INICIE LA RESOLUCION CON LOS VALORES POR
        ///  DEFECTO
        /// 
        /// </summary>
        /// 
        public  virtual void btDefecto_Click(object sender, EventArgs e)
        {
          
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN VALORES RACIONAL O ENTEROS EN LAS CAJAS DE TEXTO DE LAS
        /// COORDENADAS DE LOS PUNTOS
        /// 
        /// </summary>
        /// 
        private void Cajas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (e.KeyChar == (char)'-')
            {
                if (caja.Text.Length == 0)
                {
                    e.Handled = false;
                }
                else
                    e.Handled = true;
            }

            else if (e.KeyChar == (char)8)
                e.Handled = false;

            else if (e.KeyChar == (char)'/')
            {
                if (caja.Text.Length == 0)
                {
                    e.Handled = true;
                    caja.Focus();
                }
                else
                {
                    if (caja.Text.IndexOf('/') > 0)
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
            }
            else if (e.KeyChar == (char)13)
            {
                if (caja.Text.Length == 0 || caja.Text == "-")
                {
                    e.Handled = true;
                    caja.Focus();
                }
                else
                {
                    if (caja == this.tbPunto1X)
                    {
                        e.Handled = true;
                        this.tbPunto1Y.Focus();
                    }
                    else if (caja == this.tbPunto1Y)
                    {
                        e.Handled = true;
                        this.tbPunto1Z.Focus();
                    }
                    else if (caja == this.tbPunto1Z)
                    {
                        e.Handled = true;
                        this.tbPunto2X.Focus();
                    }
                    else if (caja == this.tbPunto2X)
                    {
                        e.Handled = true;
                        this.tbPunto2Y.Focus();
                    }
                    else if (caja == this.tbPunto2Y)
                    {
                        e.Handled = true;
                        this.tbPunto2Z.Focus();
                    }
                    else if (caja == this.tbPunto2Z)
                    {
                        e.Handled = true;
                        this.tbPunto3X.Focus();
                    }
                    else if (caja == this.tbPunto3X)
                    {
                        e.Handled = true;
                        this.tbPunto3Y.Focus();
                    }
                    else if (caja == this.tbPunto3Y)
                    {
                        e.Handled = true;
                        this.tbPunto3Z.Focus();
                    }
                    else if (caja == this.tbPunto3Z)
                    {
                        e.Handled = true;
                        this.tbPunto4X.Focus();
                    }
                    else if (caja == this.tbPunto4X)
                    {
                        e.Handled = true;
                        this.tbPunto4Y.Focus();
                    }
                    else if (caja == this.tbPunto4Y)
                    {
                        e.Handled = true;
                        this.tbPunto4Z.Focus();
                    }
                    else if (caja == this.tbPunto4Z)
                    {
                        e.Handled = true;
                        this.Focus();
                        if (!directa)
                            btContinuar.Visible = true;
                        if (!directa)
                        {
                            btContinuar.PerformClick();
                            this.AcceptButton = this.btContinuar;
                            btContinuar.Focus();
                        }
                       // else if (directa)
                          //  IniciarResolucion(defecto);
                    }

                }
            }
            else if (e.KeyChar == '0' || e.KeyChar == '1' || e.KeyChar == '2' || e.KeyChar == '3' || e.KeyChar == '4' || e.KeyChar == '5' ||
                        e.KeyChar == '6' || e.KeyChar == '7' || e.KeyChar == '8' || e.KeyChar == '9')
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }


    }
}

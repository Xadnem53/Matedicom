using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matematicas;
using Matedicom;
using System.Windows.Forms;
using System.Drawing;

namespace Algebra
{
    class Factorizacion:FormularioBase
    {

     // !!!!!!!!!!!!!!!!!!!!!!!!!
     // !! 
     // !! INICIO DE LA CLASE EL 24/9 DIA DE LA MERçE DEL 2015
     // !!
     // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        Polinomio polinomio; // Será el polinomio introducido o por defecto el correspondiente al tipo de resolucion elegido
        RichTextBox rtblistatipos; // RichTextBox para mostrar los tipos de polinomio posibles
        bool factorcomun = false; // Será true si el polinomio tiene un factor comun entre sus terminos
        bool diferenciacuadrados = false; // Será true si el polinomio es un binomio y los coeficientes de los terminos tienen signo distinto 
        bool cuadradoperfecto = false; // Sera true si el polinomio es un trinomio equivalente a un cuadrado perfecto.
        bool segundogrado = false; // Sera true si el polinomio es un trinomio de segundo grado
        bool cuartogrado = false; // Sera true si el polinomio es un trinomio de cuarto grado con exponentes pares
        bool gradomayor = false; // Sera true si el polinomio es de grado mayor a dos diferente de los anteriores
        List <Polinomio> ListaFactores = new List <Polinomio> (); // Para guardar los polinomios correspondientes a la factorizacion del polinomio inicial
        int iniciorojo = 0; // Para almacenar el inicio del texto rojo en el RichTextBox de los tipos de polinomio
        int finalrojo = 0; // Para almacenar el final del texto rojo en el RichTextBox de los tipos de polinomio
        double[] raices; // Para almacenar las dos raices de los trinomios de segundo grado
       
        List<long> divisores; // Para almacenar los divisores del termino independiente del polinomio 
        List<Racional> posiblesraices; // Para alamacenar los divisores del termino independiente divididos por los divisores del termino con el exponente mayor
        List<Racional> arrels; // Para alamacenar las raices de polinomios de grado mayor a dos.
        List<Polinomio> reducidos; // Para almacenar los polinomios en cada reduccion por el método de Ruffini
        PictureBox ruffini; // Para mostrar el desarrollo del metodo de Ruffini. 
        Graphics grafruf; // Objeto graphics del picture box anterior
        Bitmap graf; // Bitmap para el grafico permanente
        Point sitioraiz; // Situacion de la raiz en el metodo de Ruffini
        Point sitiopolinomio;// Situacion del polinomio en el metodo de Ruffini
        Point sitioresultado;// Situacion del resultado en el metodo de Ruffini
        Point sitiofactor; // Situacion del factor en el metodo de Ruffini
        Racional coeficiente; // Coeficiente del termino del polinomio que se esta operando
        Racional factor; // Resultado anterior multiplicado por la raiz
        Racional resultado; // Suma del coeficiente del termino que se esta operando y el factor 
        Polinomio reducido; // Sera el polinomio una vez reducido con la raiz correspondiente
        RichTextBox rtbdesarrollo; // Para mostrar el desarrollo de la factorizacion
        List<int> intervalosrojos; // Para guardar los rangos de letra de color rojo
        int cantidadraices = 0; // Cantidad de raices encontrada en cada iteracion

        public Factorizacion( bool resoluciondirecta )
        {
            directa = resoluciondirecta;
        }


        public override void Cargar(object sender, EventArgs e)
        {
            lbExplicacion.Show();
            lbExplicacion.Text = "Introduzca el polinomio a factorizar ( variable única X ):\n\n O pulse el botón [E] para ejemplo con valores por defecto.";
            EtiquetaFilas.Show();
            EtiquetaFilas.Text = " Polinomio: ";
            EtiquetaFilas.Location = new Point(lbExplicacion.Location.X, lbExplicacion.Location.Y + lbExplicacion.Height + 5);
            EtiquetaFilas.AutoSize = true;
            tbFilas.Show();
            tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Width + 5, EtiquetaFilas.Location.Y);
            tbFilas.Size = new Size(500, tbFilas.Height);
            tbFilas.KeyPress += Cajas_KeyPress;
            btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
            if (directa)
                btDefecto.Hide();
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
             //tbFilas.Text = ("4X^2-3X"); // Factor común simple
            // tbFilas.Text = ("2X^5+X^4-8X^3-X^2+6X"); // Factor común, polinomio grado mayor a dos, trinomio de segundo grado
           // tbFilas.Text = ("5X^3-4X"); // Factor común, diferencia de cuadrados 
           // tbFilas.Text = ("4X^2-4"); // Diferencia de cuadrados
           // tbFilas.Text = ("4X^2-12X+9"); // Cuadrado de una suma
        //  tbFilas.Text = ("4X^3-12X^2+9X"); // Factor comun, Cuadrado de una suma
           // tbFilas.Text = ("X^2+3X+5"); // Trinomino de segundo grado con resultado imaginario
         // tbFilas.Text = ("X^2+3X-5"); // Trinomino de segundo grado con resultado real.
           // tbFilas.Text = ("X^4+3X^2-5"); // Trinomino de cuarto grado con resultado exponentes pares.
           // tbFilas.Text = "2X^4+X^3-8X^2-X+6"; // Polinomio de grado mayor a dos con taices determinadas
           // tbFilas.Text = "2X^5+X^4-8X^3-X+6"; // Polinomio de grado mayor a dos con raices indeterminadas
            tbFilas.Text = "12X^3+8X^2-3X-2"; // Polinomio de grado mayor a dos con raices racionales determinadas
            paso = 0;
            LeerPolinomio();
            //IniciarResolucion();
        }

        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>
        /// 
        public override void btNuevo_Click(object sender, EventArgs e)
        {
            Factorizacion nuevo = new Factorizacion(directa);
            nuevo.Show();
            this.Close();
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN VALORES  RACIONALES O ENTEROS O SIGNOS CORRECTOS EN LAS CAJAS DE TEXTO DE LOS
        /// POLINOMIOS
        /// 
        /// </summary>
        /// 
        private void Cajas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (e.KeyChar == (char)'-') // Signo -
            {
                e.Handled = false;
            }

            else if (e.KeyChar == (char)'+') // Signo +
                e.Handled = false;

           // else if ((e.KeyChar >= (char)65 && e.KeyChar <= (char)90) || (e.KeyChar >= (char)97 && e.KeyChar <= (char)122)) // letras de la A a la Z mayusculas o minusculas
            else if (e.KeyChar == 'x' || e.KeyChar == 'X') // Variable X
            {
                if (e.KeyChar > 90) // Pasar a mayuscula
                {
                    tbFilas.Text += char.ToUpper(e.KeyChar);
                    tbFilas.SelectionStart = tbFilas.Text.Length;
                    e.Handled = true;
                }
                else
                    e.Handled = false;
            }

            else if (e.KeyChar == (char)8) // Retroceso
                e.Handled = false;

            else if (e.KeyChar == (char)'/')// Signo dividir
            {
                if (caja.Text.Length == 0)
                {
                    e.Handled = true;
                    caja.Focus();
                }
                else
                {
                    e.Handled = false;
                }
            }
                
            else if (e.KeyChar == (char)'^')// Signo potencia
            {
                if (caja.Text.Length == 0)
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
                    // Construir el polinomio introducido si es correcto
                    LeerPolinomio();

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


        private void LeerPolinomio()
        {
            lbExplicacion.Focus();
            try
            {
                polinomio = new Polinomio(tbFilas.Text);
                polinomio.EliminarCeros();
                polinomio.Ordenar();
                label1.Location = new Point(10, tbFilas.Location.Y + 100);
                label1.BackColor = Color.SeaGreen;
                label1.ForeColor = Color.DarkBlue;
                label1.Text = "Polinomio:" + "\n( ";
                label1.Text += polinomio.ToString();
                label1.Text += " )";
                label1.Visible = true;
                if (!directa)
                {
                    btDefecto.Hide();
                    btContinuar.Show();
                    btContinuar.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + 30);
                }
                    IniciarResolucion();
                    
            }
            catch (Exception )
            {
                MessageBox.Show("Formato incorrecto, solo se pueden introducir los polinomios como suma o resta de terminos con coeficiente Racional o entero.\nEjemplo: 3X^2+5Y+7/4X^8/3");
            }
            
        }


        private void btContinuar_Click(object sender, EventArgs e)
        {
            paso++;
            if (paso < 9)
            {
                IniciarResolucion();
            }

            else if (paso > 19 && paso < 30)
            {
                CuartoGrado();
            }
            else if (paso > 30 && paso < 40)
            {
                ReduccionRuffini();
            }
            else if(paso > 39 && paso < 50 )
                Reduccion();
        }


        /// <summary>
        /// 
        /// REALIZA LA RESOLUCION PASO A PASO DE LA FACTORIZACION
        /// 
        /// </summary>
        /// 

        private void IniciarResolucion()
        {
            if(polinomio.Largo == 0 )//|| (polinomio.Largo == 2 && polinomio.Terminos[0].Exponentes[0] == 1 ) ) // Determinar las raices si no lo estan y 
            {
                FinalizarResolucion();
                return;
            }
            
            if (paso == 0) // Mostrar el polinomio y los posibles tipos de polinomio
            {
                btDefecto.Hide();
                if (!directa && ListaFactores.Count == 0) // Solo para la primera reduccion del polinomio
                {
                    btContinuar.Show();
                    btContinuar.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + 30);
                    btContinuar.Click += btContinuar_Click;
                    lbExplicacion.Text = "Para comenzar, hay que considerar frente a que tipo de polinomio estamos:";
                    rtblistatipos = new RichTextBox();
                    rtblistatipos.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
                    rtblistatipos.BackColor = Color.SeaGreen;
                    rtblistatipos.Size = new Size(500, 200);
                    rtblistatipos.BorderStyle = BorderStyle.None;
                    rtblistatipos.Font = new Font(rtblistatipos.Font.FontFamily, 15);
                    rtblistatipos.Text = "* Susceptible de sacar factor común. ( a*b + a*c + a*d )\n" + "* Diferencia de cuadrados. (a^2 - b^2)\n" + "* Trinomio cuadrado perfecto. ( a^2 ± 2ab + b^2)\n" + "* Trinomio de segundo grado. ( aX^2 + bX + c )\n" + "* Trinomio de cuarto grado con exponentes pares ( aX^4 - bX^2 + c )\n" + "* Polinomio de grado superior a dos.";
                    Controls.Add(rtblistatipos);
                }
                else if(!directa)
                {
                    lbExplicacion.Text = "Volvemos a considerar que tipo de polinomio ha quedado tras la reducción anterior.";
                    rtblistatipos.Select(0, rtblistatipos.Text.Length - 1);
                    rtblistatipos.SelectionColor = Color.Black;
                    rtblistatipos.Select(0, 0);
                }
                if(directa)
                {
                    paso = 1;
                    IniciarResolucion();
                }
            }
            else if (paso == 1)
            {
                // Si ya se han sacado factores, pintar el polinomio reducido de rojo en el RichTextBox
                if (!directa && polinomio.Largo > 0 && ListaFactores.Count > 0 && arrels != null && arrels.Count == 0)
                {
                    int inicio = rtbExplicaciones.Text.IndexOf(polinomio.ToString()) - 2;
                    int final = rtbExplicaciones.Text.Length;
                    rtbExplicaciones.Select(inicio, (final - inicio));
                    rtbExplicaciones.SelectionColor = Color.Red;
                    rtbExplicaciones.Select(0, 0);
                }
                // Para evitar intentar sacar factor comun cuando el polinomio original es nulo
                if (polinomio.Largo > 0) 
                    factorcomun = true;
                else
                    factorcomun = false;
                // Comprobar si se puede sacar factor comun 
                foreach (Termino t in polinomio.Terminos)
                    if (t.Variables[0] != 'X')
                        factorcomun = false;
                // Si es factible sacar factor común, obtenerlo
                if (factorcomun)
                {
                    paso = 10;
                    SacarFactorComun();
                }
                else
                {
                    if (ListaFactores.Count == 0)
                        lbExplicacion.Text = "En este caso no podemos sacar un factor común útil para la obtencion de las raices del polinomio.";
                    else if( ListaFactores.Count > 0 && polinomio.Largo > 0)
                        lbExplicacion.Text = "No podemos sacar factor común útil del polinomio reducido.";
                    if(directa)
                    {
                        paso = 2;
                        IniciarResolucion();
                    }
                }
                if (!directa)
                {
                    finalrojo = rtblistatipos.Text.IndexOf("\n");
                    rtblistatipos.Select(0, finalrojo);
                    rtblistatipos.SelectionColor = Color.Red;
                    iniciorojo = finalrojo;
                }
            }
            else if (paso == 2)
            {
                // Restaurar el color negro para la descripcion de todos los tipos de polinomio
                if (!directa)
                {
                    rtblistatipos.Select(0, rtblistatipos.Text.Length);
                    rtblistatipos.SelectionColor = Color.Black;
                }
                // Comprobar si se trata de una resta de cuadrados
                diferenciacuadrados = true;
                if (polinomio.Largo != 2)
                    diferenciacuadrados = false;
                if ((polinomio.Terminos[0].Coeficiente.Numerador >= 0 && polinomio.Terminos[1].Coeficiente.Numerador >= 0) || (polinomio.Terminos[0].Coeficiente.Numerador < 0 && polinomio.Terminos[1].Coeficiente.Numerador < 0))
                    diferenciacuadrados = false;
                if (diferenciacuadrados)
                {
                    paso = 20;
                    DiferenciaDeCuadrados();
                }
                else // Si el polinomio no es equivalente a una resta de cuadrados.
                {
                    lbExplicacion.Text = "En este caso tampoco se trata de una resta de cuadrados.";
                    if(directa)
                    {
                        paso = 3;
                        IniciarResolucion();
                    }
                }
                if (!directa)
                {
                    int finalrojo = rtblistatipos.Text.IndexOf("\n", iniciorojo + 1);
                    rtblistatipos.Select(iniciorojo, (finalrojo - iniciorojo));
                    rtblistatipos.SelectionColor = Color.Red;
                    iniciorojo = finalrojo;
                    rtblistatipos.Select(0, 0);
                }
            }
            else if( paso == 3) // Comprobar si se trata de un trinomio cuadrado perfecto
            {
                if (!directa)
                {
                    // Restaurar el color negro para la descripcion de todos los tipos de polinomio
                    rtblistatipos.Select(0, rtblistatipos.Text.Length);
                    rtblistatipos.SelectionColor = Color.Black;
                }
                if (polinomio.Largo == 3) // Si se trata de un trinomio
                {
                    // Obtener la raiz del coeficiente, la variable y la raiz del exponente del primer termino
                    Racional primero = new Racional(polinomio.Terminos[0].Coeficiente);
                    if(primero.Numerador > 0)
                    primero.Raiz();
                    char primera = polinomio.Terminos[0].Variables[0];
                    Racional exp1 = polinomio.Terminos[0].Exponentes[0];
                    if( primera != ' ')
                    exp1 /= 2;
                    Termino primer = new Termino(primero, primera, exp1);
                    // Obtener la raiz del coeficiente, la variable y la raiz del exponente del tercer termino
                    Racional tercero = new Racional(polinomio.Terminos[2].Coeficiente);
                    if (tercero.Numerador > 0)
                        tercero.Raiz();
                    char tercera = polinomio.Terminos[2].Variables[0];
                    Racional exp3 = polinomio.Terminos[2].Exponentes[0];
                    if(tercera != ' ')
                    exp3 /= 2;
                    Termino tercer = new Termino(tercero, tercera, exp3);
                    // Comprobar si el segundo termino se correponde al doble del primero por el segundo
                    Termino segon = primer * tercer * 2;

                    if (polinomio.Terminos[1].Coeficiente == segon.Coeficiente || polinomio.Terminos[1].Coeficiente * -1 == segon.Coeficiente && polinomio.Terminos[1].Variables[0] == segon.Variables[0] && polinomio.Terminos[1].Exponentes[0] == segon.Exponentes[0])
                        cuadradoperfecto = true;
                    // Si el coeficiente del primer o tercer termino es negativo, no se trata de un cuadrado perfecto
                    if (primer.Coeficiente.Numerador < 0 || tercer.Coeficiente < 0)
                        cuadradoperfecto = false;
                }
                if (cuadradoperfecto)
                {
                    CuadradoPerfecto();
                }
                else
                {
                    lbExplicacion.Text = "Tampoco se trata de un trinomio cuadrado perfecto.";
                    if(directa)
                    {
                        paso = 4;
                        IniciarResolucion();
                    }
                }
                // Pintar de rojo el tipo de polinomio que se esta comprobando
                if (!directa)
                {
                    int finalrojo = rtblistatipos.Text.IndexOf("\n", iniciorojo + 1);
                    rtblistatipos.Select(iniciorojo, (finalrojo - iniciorojo));
                    rtblistatipos.SelectionColor = Color.Red;
                    iniciorojo = finalrojo;
                }
            }
            else if( paso == 4) // Comprobar si se trata de un trinomio de segundo grado
            {
                if (!directa)
                {
                    // Restaurar el color negro para la descripcion de todos los tipos de polinomio
                    rtblistatipos.Select(0, rtblistatipos.Text.Length);
                    rtblistatipos.SelectionColor = Color.Black;
                }
                if( polinomio.Largo == 3) // Si se trata de un trinomio
                {
                    if ((polinomio.Terminos[0].Variables[0] != ' ' && polinomio.Terminos[0].Exponentes[0] == 2) && (polinomio.Terminos[1].Variables[0] != ' ' && polinomio.Terminos[1].Exponentes[0] == 1) && polinomio.Terminos[2].Variables[0] == ' ')
                        segundogrado = true;
                }
                if(segundogrado)
                {
                    SegundoGrado();
                }
                else
                {
                    lbExplicacion.Text = "Tampoco es un trinomio de segundo grado.";
                    if (directa)
                    {
                        paso = 5;
                        IniciarResolucion();
                    }
                }
                if (!directa)
                {
                    // Pintar de rojo el tipo de polinomio que se esta comprobando
                    int finalrojo = rtblistatipos.Text.IndexOf("\n", iniciorojo + 1);
                    rtblistatipos.Select(iniciorojo, (finalrojo - iniciorojo));
                    rtblistatipos.SelectionColor = Color.Red;
                    iniciorojo = finalrojo;
                }
            }
            else if( paso == 5)// Comprobar si se trata de un trinomio de cuarto grado con exponentes pares
            {
                if (!directa)
                {
                    // Restaurar el color negro para la descripcion de todos los tipos de polinomio
                    rtblistatipos.Select(0, rtblistatipos.Text.Length);
                    rtblistatipos.SelectionColor = Color.Black;
                }
                if( polinomio.Largo == 3 )
                {
                    if ((polinomio.Terminos[0].Variables[0] != ' ' && polinomio.Terminos[0].Exponentes[0] == 4) && (polinomio.Terminos[1].Variables[0] != ' ' && polinomio.Terminos[1].Exponentes[0] == 2) && polinomio.Terminos[2].Variables[0] == ' ')
                        cuartogrado = true;
                }
                if(cuartogrado)
                {
                    paso = 20;
                    if (directa)
                        paso = 21;
                    CuartoGrado();
                }
                else
                {
                    lbExplicacion.Text = "Tampoco es un trinomio de cuarto grado con exponentes pares.";
                    if (directa)
                    {
                        paso = 6;
                        IniciarResolucion();
                    }
                }
                // Pintar de rojo el tipo de polinomio que se esta comprobando
                if (!directa)
                {
                    int finalrojo = rtblistatipos.Text.IndexOf("\n", iniciorojo + 1);
                    rtblistatipos.Select(iniciorojo, (finalrojo - iniciorojo));
                    rtblistatipos.SelectionColor = Color.Red;
                    iniciorojo = finalrojo;
                }
            }
            else // Si se trata de un polinomio de grado mayor a dos distinto de los casos anteriores
            {
                gradomayor = true;
                if (!directa)
                {
                    // Restaurar el color negro para la descripcion de todos los tipos de polinomio
                    rtblistatipos.Select(0, rtblistatipos.Text.Length);
                    rtblistatipos.SelectionColor = Color.Black;

                    // Pintar de rojo el tipo de polinomio que se esta comprobando
                    iniciorojo = rtblistatipos.Text.LastIndexOf("\n");
                    finalrojo = rtblistatipos.Text.Length;
                    rtblistatipos.Select(iniciorojo, (finalrojo - iniciorojo));
                    rtblistatipos.SelectionColor = Color.Red;
                    //   iniciorojo = finalrojo;
                    Controls.Remove(rtbdesarrollo);
                    rtbdesarrollo = new RichTextBox();
                    Controls.Add(rtbdesarrollo);
                    rtbdesarrollo.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                    rtbdesarrollo.BackColor = Color.SeaGreen;
                    rtbdesarrollo.BorderStyle = BorderStyle.None;
                    rtbdesarrollo.Size = new Size(500, 400);
                    int tamañofuente = rtbdesarrollo.Size.Width / (polinomio.Largo + 31);
                    rtbdesarrollo.Font = new Font(rtbExplicaciones.Font.FontFamily, tamañofuente);
                    paso = 30;
                    label2.Hide();
                    string anterior = rtbExplicaciones.Text;
                    rtbdesarrollo.Text = anterior;
                    rtbExplicaciones.Hide();

                    ReduccionRuffini();
                }
                else if(directa)
                {
                    paso = 30;
                    ReduccionRuffini();
                }
            }
        }

        /// <summary>
        /// 
        /// METODO AUXILIAR DEL ANTERIOR QUE SACA FACTOR COMUN DEL POLINOMIO Y LO METE EN LA LISTA DE FACTORES
        /// 
        /// </summary>
        /// 
        private void SacarFactorComun()
        {
            // Sacar factor comun del polinomio actual
            Polinomio[] factorescomunes = new Polinomio[2]; // Matriz de polinomios para contenener el polinomio factor comun y el reducido
            factorescomunes = polinomio.FactorComun();
            // Mostrar la factorizacion realizada en el RichTextBox
            if (!directa)
            {
                lbExplicacion.Text = "En este caso podemos sacar factor común, obteniendo así una de las raices del polinomio.";
                rtbExplicaciones.Text = factorescomunes[0].ToString() + " * ( " + factorescomunes[1].ToString() + " )";
                rtbExplicaciones.Show();
                rtbExplicaciones.Size = new Size(this.ClientSize.Width, 400);
                rtbExplicaciones.Font = rtblistatipos.Font;
                rtbExplicaciones.BackColor = Color.SeaGreen;
                rtbExplicaciones.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + 300);
                rtbExplicaciones.BorderStyle = BorderStyle.None;
            }
            // Añadir el factor comun a la lista de factores
            ListaFactores.Add(factorescomunes[0]);
            // Asignar al polinomio inicial el polinomio reducido
            polinomio = factorescomunes[1];
            // Mostrar como queda el polinomio inicial una vez reducido
            if (!directa)
            {
                label2.Show();
                label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                label2.BackColor = Color.Transparent;
                label2.Font = rtbExplicaciones.Font;
                label2.Text = "Factores extraidos:\n";
                foreach (Polinomio p in ListaFactores)
                    label2.Text += p.ToString() + "\n";
                label2.Text += "Polinomio reducido = " + polinomio.ToString();
            }
            // Si el polinomio ha sido factorizado totalmente
            if (polinomio.Largo == 2 && polinomio.Terminos[0].Exponentes[0] == 1)
            {
                if (directa)
                {
                    label2.Show();
                    label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                    label2.BackColor = Color.Transparent;
                    label2.Font = new Font("Dejavu Sans", 14);
                    label2.Text = "Polinomio factorizado: \n" + factorescomunes[0].ToString() + " * ( " + factorescomunes[1].ToString() + " )";
                }
                else
                {
                    label2.Text = "Polinomio totalmente factorizado:\nRaices:\nX = 0\nX =" + ((polinomio.Terminos[1].Coeficiente * -1) / polinomio.Terminos[0].Coeficiente).ToString();
                    btContinuar.Hide();
                }
            }
            else // Si hay posibilidades de seguir factorizando el polinomio reducido, volver a intentarlo
            {
                paso = -1;
                if (directa)
                {
                    paso = 0;
                    IniciarResolucion();
                }
            }
           
        }
        
        /// <summary>
        /// 
        /// FACTORIZA LA DIFERENCIA DE CUADRADOS Y METE LOS FACTORES EN LA LISTA 
        /// 
        /// </summary>
        /// 
        private void DiferenciaDeCuadrados()
        {
            lbExplicacion.Text = "En este caso, vemos que el polinomio se corresponde con una diferencia de cuadrados. Aplicamos la regla: (a^2 - b^2) = (a + b) * (a - b)";
            // Obtener la raiz cuadrada del primer termino 
            Termino t1 = polinomio.Terminos[0];
            // Si el coeficiente es negativo, pasarlo a positivo
            if (t1.Coeficiente.Numerador < 0)
                t1.Coeficiente *= -1;
            t1.Coeficiente.Raiz();
            if (t1.Variables[0] != ' ') // Si no es un numero
                t1.Exponentes[0] /= 2;

            // Obtener la raiz cuadrada del segundo termino si el coeficiente es cuadrado perfercto, si no dejar el coeficiente en formato radical
            Termino t2 = polinomio.Terminos[1];
            // Si el coeficiente es negativo, pasarlo a positivo
            if (t2.Coeficiente.Numerador < 0)
                t2.Coeficiente *= -1;
            t2.Coeficiente.Raiz();
            if (t2.Variables[0] != ' ') // Si no es un numero
                t2.Exponentes[0] /= 2;

            // Añadir la suma y resta de los terminos anteriores a la lisa de factores
            ListaFactores.Add(new Polinomio(new List<Termino>() { t1, t2 }));
            ListaFactores[ListaFactores.Count - 1].EliminarCeros();
            ListaFactores.Add(new Polinomio(new List<Termino>() { t1, new Termino(t2.Coeficiente*-1,t2.Variables,t2.Exponentes) }));
            ListaFactores[ListaFactores.Count - 1].EliminarCeros();
            if (directa)
            {
                polinomio.Terminos.Clear();
                paso = 0;
                IniciarResolucion();
            }
            else
            {
                // Mostrar como queda el polinomio inicial una vez reducido
                label2.Show();
                label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                label2.BackColor = Color.Transparent;
                label2.Font = rtblistatipos.Font;
                label2.Text = "Factorización del polinomio introducido: \n";
                foreach (Polinomio p in ListaFactores)
                {
                    label2.Text += " ( ";
                    foreach (Termino t in p.Terminos)
                    {
                        if (t.Coeficiente.Numerador > 0)
                            label2.Text += "+";
                        label2.Text += Math.Round(t.Coeficiente.ToDouble(), 4).ToString() + t.Variables[0];
                        if ((double)t.Exponentes[0].Numerador / (double)t.Exponentes[0].Denominador != (double)1)
                            label2.Text += "^" + t.Exponentes[0].ToString();
                    }
                    label2.Text += ") *";
                }
                // Eliminar el ultimo signo de multiplicar
                string lectura = label2.Text.Substring(0, label2.Text.Length - 2);
                label2.Text = lectura;
                btContinuar.Hide();
            }
        }

        /// <summary>
        /// 
        /// SI EL POLINOMIO ES UN TRINOMIO CUADRADO PERFECTO, LO CONVIERTE EN EL CUADRADO DE UNA SUMA Y METE EN LA LISTA 
        /// DE FACTORES LOS DOS FACTORES RESULTANTES
        /// 
        /// </summary>
        /// 
        private void CuadradoPerfecto()
        {
                lbExplicacion.Text = "En este caso el polinomio es un cuadrado perfecto, por lo que podemos aplicar: a^2 ± 2ab + b^2 = (a ± b )^2.";
                // Obtener la raiz del coeficiente, la variable y la raiz del exponente del primer termino
                Racional primero = new Racional(polinomio.Terminos[0].Coeficiente);
                primero.Raiz();
                char primera = polinomio.Terminos[0].Variables[0];
                Racional exp1 = polinomio.Terminos[0].Exponentes[0];
                if (primera != ' ')
                    exp1 /= 2;
                Termino primer = new Termino(primero, primera, exp1);
                // Obtener la raiz del coeficiente, la variable y la raiz del exponente del tercer termino
                Racional tercero = new Racional(polinomio.Terminos[2].Coeficiente);
                tercero.Raiz();
                char tercera = polinomio.Terminos[2].Variables[0];
                Racional exp3 = polinomio.Terminos[2].Exponentes[0];
                if (tercera != ' ')
                    exp3 /= 2;
                Termino tercer = new Termino(tercero, tercera, exp3);
             
                    // Mostrar como queda el polinomio inicial una vez reducido
                if (!directa)
                {
                    label2.Show();
                    label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                    label2.BackColor = Color.Transparent;
                    label2.Font = rtblistatipos.Font;
                    label2.Text = "Factores extraidos:\n";
                }
                    Polinomio factor1 = new Polinomio(new List<Termino>());
                    Polinomio factor2 = new Polinomio(new List<Termino>());
                    if (polinomio.Terminos[1].Coeficiente.Numerador > 0) // Si se trata de la potencia de una suma
                    {
                        factor1.AñadirTermino(primer);
                        factor1.AñadirTermino(tercer);
                        factor1.EliminarCeros();
                        if(!directa)
                        label2.Text += ("( " + factor1.ToString() + " )^2 = ( " + factor1.ToString() + " ) * ( " + factor1.ToString() + " )");
                        ListaFactores.Add(factor1);
                        ListaFactores.Add(factor1);
                    }
                    else // Si se trata de la potencia de una resta
                    {
                        factor2.AñadirTermino(new Termino(primer));
                        factor2.AñadirTermino(new Termino(tercer * -1));
                        factor2.EliminarCeros();
                        if(!directa)
                        label2.Text += ("( " + factor2.ToString() + " )^2 = ( " + factor2.ToString() + " ) * ( " + factor2.ToString() + " )");
                        ListaFactores.Add(factor2);
                        ListaFactores.Add(factor2);
                    }

                    // Eliminar todos los terminos del polinomio inicial, ya que ha sido totalmente factorizado
                    polinomio.Terminos.Clear();
                    paso = 0;
                
                if (directa)
                {
                    paso = 1;
                    IniciarResolucion();
                }
        }

        /// <summary>
        /// 
        /// RESUELVE EL TRINOMIO DE SEGUNDO GRADO Y METE LOS POLINOMIOS CONTRUIDOS CON LOS RESULTADOS EN LA LISTA DE
        /// FACTORES
        /// 
        /// </summary>
        /// 
        private void SegundoGrado()
        {
            lbExplicacion.Text = "En este caso se trata de un trinomio de segundo grado. Podemos aplicar la formula: -b±√(b^2-4ac)/2a.";
            EcuacionSegundoGrado ecuacion = new EcuacionSegundoGrado(polinomio.Terminos[0], polinomio.Terminos[1], polinomio.Terminos[2].Coeficiente);
            double[] raices = ecuacion.Raices();
        
            if (raices == null) // Si el polinomio tiene raices imaginarias, terminar la resolucion
            {
                MessageBox.Show("Raices imaginarias. No es posible dar un resultado debido a limitaciones del programa.");
                btContinuar.Hide();
            }
            else // Si el polinomio tiene raices reales.Meter los polinomios factores en la lista
            {
                Termino t1 = new Termino(new Racional(raices[0]) *-1, ' ', 1);
                Polinomio factor1 = new Polinomio(new List<Termino>() { new Termino(1, 'X', 1), t1 }); ;
                Termino t2 = new Termino(new Racional(raices[1])*-1, ' ', 1);
                Polinomio factor2 = new Polinomio(new List<Termino>() { new Termino(1, 'X', 1), t2 });
                ListaFactores.Add(factor1);
                ListaFactores.Add(factor2);
                // Mostrar como queda el polinomio inicial una vez reducido
                if (!directa && rtbdesarrollo == null)
                {
                    label2.Show();
                    label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                    label2.BackColor = Color.Transparent;
                    label2.Font = rtblistatipos.Font;
                    label2.Text = "Factores extraidos:\n";
                    label2.Text += "( " + factor1.Terminos[0].ToString();
                    if (raices[0] * -1 > 0)
                        label2.Text += "+";
                    label2.Text += Math.Round(raices[0] * -1, 4).ToString() + " ) * ( " + factor2.Terminos[0].ToString();
                    if (raices[1] * -1 > 0)
                        label2.Text += "+";
                    label2.Text += Math.Round(raices[1] * -1, 4).ToString() + " )";
                }
                else if( !directa)
                {
                    rtbdesarrollo.Text += "\nNuevas raices encontradas:\n + X = " + raices[0].ToString() + " ; X = " + raices[1].ToString();
                    rtbdesarrollo.Text += "\nFactores extraidos:\n" + factor1.ToString() + " ; " + factor2.ToString() ;
                   
                }
                        // Eliminar todos los terminos del polinomio inicial, ya que ha sido totalmente factorizado
                        polinomio.Terminos.Clear();
                        if (reducidos != null)
                            reducidos.Clear();
                // Eliminar los posbiles polinomios nulos en la lista de factores
                        List<Polinomio> aux = new List<Polinomio>();
                        foreach (Polinomio p in ListaFactores)
                        {
                            if (p.Largo > 0)
                                aux.Add(p);
                        }
                        ListaFactores = aux;
                        paso = 0;
                if(directa)
                {
                    paso = 1;
                    IniciarResolucion();
                }
            }
        }

        /// <summary>
        /// 
        /// REALIZA EL CAMBIO DE VARIABLE Y EXTRAE EL VALOR DE LAS RAICES DEL TRINOMIO DE SEGUNDO GRADO CORRESPONDIENTE
        /// 
        /// </summary>
        /// 
        private void CuartoGrado()
        {
            if (!directa && paso == 20) // Mostrar el cambio de variable y el polinomio con la variable cambiada
            {
                lbExplicacion.Text = "En este caso se trata de un trinomio de cuarto grado con exponentes pares. Podemos hacer un cambio de variable: X^2 = t.";
                label2.Show();
                label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                label2.BackColor = Color.Transparent;
                label2.TextAlign = ContentAlignment.MiddleLeft;
                label2.Font = rtblistatipos.Font;
                label2.Text = "Cambio de variable: X^2 = t.\n";
                Polinomio cambiovariable = new Polinomio(new List<Termino>() { new Termino(polinomio.Terminos[0].Coeficiente, 't', 2), new Termino(polinomio.Terminos[1].Coeficiente, 't', 1), new Termino(polinomio.Terminos[2].Coeficiente, ' ', 1) });
                label2.Text += "Polinomio con el cambio de variable:  " + cambiovariable.ToString() + "\n\n";

            }
            else if (paso == 21) // Extraer las raices del polinomio con el cambio de variable si es posible y crear los polinomios factores y añadirlos a la lista
            {
                lbExplicacion.Text = "El polinomio con el cambio de variable, se convierte en un trinomio de segundo grado, del cual podemos extraer dos raices: ";
                // Crear el polinomio con el cambio de variable
                EcuacionSegundoGrado cambiovariable = new EcuacionSegundoGrado(new Termino(polinomio.Terminos[0].Coeficiente, 't', 2), new Termino(polinomio.Terminos[1].Coeficiente, 't', 1), polinomio.Terminos[2].Coeficiente);
                // Obtener las raices del polinomio con el cambio de variable
                raices = cambiovariable.Raices();
                if (raices == null) // Si el polinomio tiene raices imaginarias, terminar la resolucion
                {
                    MessageBox.Show("Raices imaginarias. No es posible dar un resultado debido a limitaciones del programa.");
                    btContinuar.Hide();
                }
                else // Si el polinomio tiene raices reales.Meter los polinomios factores en la lista
                {
                    if (raices[0] > 0) // Si la raiz es positiva, añadir las raices cuadradas de la raiz
                    {
                        double raiz1 = Math.Sqrt(raices[0]);
                        Termino t1 = new Termino(new Racional(raiz1 * -1), ' ', 1);
                        Polinomio factor1 = new Polinomio(new List<Termino>() { new Termino(1, 'X', 1), t1 }); ;
                        Termino t2 = new Termino(new Racional(raiz1), ' ', 1);
                        Polinomio factor2 = new Polinomio(new List<Termino>() { new Termino(1, 'X', 1), t2 });
                        ListaFactores.Add(factor1);
                        ListaFactores.Add(factor2);
                    }
                    else // Si la raiz es negativa, añadir la raiz
                    {
                        Termino t1 = new Termino(new Racional(raices[0] * -1), ' ', 1);
                        Polinomio factor1 = new Polinomio(new List<Termino>() { new Termino(1, 'X', 2), t1 });
                        ListaFactores.Add(factor1);
                    }

                    if (raices[1] > 0) // Si la raiz es positiva, añadir las raices cuadradas de la raiz
                    {
                        double raiz2 = Math.Sqrt(raices[1]);
                        Termino t1 = new Termino(new Racional(raiz2 * -1), ' ', 1);
                        Polinomio factor1 = new Polinomio(new List<Termino>() { new Termino(1, 'X', 1), t1 }); ;
                        Termino t2 = new Termino(new Racional(raiz2), ' ', 1);
                        Polinomio factor2 = new Polinomio(new List<Termino>() { new Termino(1, 'X', 1), t2 });
                        ListaFactores.Add(factor1);
                        ListaFactores.Add(factor2);
                    }
                    else // Si la raiz es negativa, añadir la raiz
                    {
                        Termino t1 = new Termino(new Racional(raices[1] * -1), ' ', 1);
                        Polinomio factor1 = new Polinomio(new List<Termino>() { new Termino(1, 'X', 2), t1 });
                        ListaFactores.Add(factor1);
                    }
                    // Mostrar las raices
                    if (!directa)
                    {
                        int contador = 0;
                        foreach (double d in raices)
                        {
                            contador++;
                            label2.Text += "Raiz " + contador.ToString() + ":  " + d.ToString() + "\n";
                        }
                    }
                    if(directa)
                    {
                        paso = 22;
                        CuartoGrado();
                    }
                }

            }
            else if (paso == 22) // Mostrar la construccion de los polinomios factores segun sean positivas o negativas las raices anteriores
            {
                if (!directa)
                {
                    lbExplicacion.Text = "Una vez obtenidas las raices del polinomio con el cambio de variable, deshacemos el cambio de variable sacando la raiz cuadrada de las raices anteriores para hallar el valor de X, o cuando no sea posible porque la raiz anterior es negativa, asignandola a X^2. ";
                    label2.Text += Environment.NewLine;
                    foreach (double d in raices)
                    {
                        if (d < 0) // Raiz negativa
                        {
                            label2.Text += "X^2 = " + Math.Round(d, 4).ToString() + "\n";
                        }
                        else // Raiz positiva
                        {
                            double raiz = Math.Sqrt(d);
                            raiz = Math.Round(raiz, 4);
                            label2.Text += "X= " + raiz.ToString() + "\n";
                            label2.Text += "X= " + (raiz * -1).ToString() + "\n";
                        }
                    }
                }
                // Eliminar todos los terminos del polinomio inicial, ya que ha sido totalmente factorizado
                polinomio.Terminos.Clear();
                paso = 0;
                if(directa)
                {
                    paso = 1;
                    IniciarResolucion();
                }
            }
                
        }

        /// <summary>
        /// 
        /// REDUCE EL POLINOMIO POR EL METODO DE RUFFINI PARA EXTRAER LAS RAICES DE UN POLINOMIO DE GRADO MAYOR A DOS
        /// 
        /// </summary>
        /// 
        private void ReduccionRuffini()
        {
            if (paso == 30) // Comprobar si el termino independiente es entero y sacar la lista de sus divisores o terminar la resolucion
            {
                if(ruffini != null)
                ruffini.Hide();
                lbExplicacion.Text = "Al tratarse de un polinomio de grado mayor de dos, distinto de los casos anteriores, intentamos reducirlo por el método de Ruffini.";
                // Comprobar si el termino independiente es entero 
                bool entero = polinomio.Terminos[polinomio.Terminos.Count - 1].Coeficiente.EsEntero();
                // Extraer los divisores del término independiente si es entero
                if (!entero)
                {
                    MessageBox.Show("El programa no puede operar con coeficientes racionales.Páselos a entero y vuelva a intentarlo.");
                    btContinuar.Hide();
                    return;
                }
                    lbExplicacion.Text += "\nComenzamos obteniendo la lista de todos los divisores del término independiente por si el polinomio tienen raices enteras. Por si el polinomio tuviera raices racionales, añadimos a la lista los divisores del término independiente divididos por los divisores del coeficiente del término con mayor grado:";
                // Rellenar la lista de los divisores del termino independiente
                        divisores = AlgebraLineal.Divisores(polinomio.Independiente.Numerador);
                        if (!directa)
                        {
                            if (ListaFactores.Count > 0 && reducidos != null)
                                rtbdesarrollo.Text = "Polinomio reducido con las raices encontradas hasta ahora: \n" + reducidos[reducidos.Count - 1].ToString();
                            else
                                rtbdesarrollo.Text = "Polinomio reducido con las raices encontradas hasta ahora: \n" + polinomio.ToString();
                            rtbdesarrollo.Text += "\n\nDivisores del término independiente: \n";
                            int cantidaddivisores = divisores.Count;
                            for (int i = 0; i < cantidaddivisores; i++)
                                divisores.Add(divisores[i] * -1);
                            foreach (long l in divisores)
                                rtbdesarrollo.Text += l.ToString() + " , ";
                        }
                
                    // Determinar el coeficiente del termino con mayor exponente
                    Racional masalto = 0;
                    Racional coeficiente = 0;
                    foreach (Termino t in polinomio.Terminos)
                    {
                        if (t.Exponentes[0] > masalto)
                        {
                            coeficiente = new Racional(t.Coeficiente);
                            masalto = t.Exponentes[0];
                        }
                    }
                    if (!coeficiente.EsEntero())
                    {
                        MessageBox.Show("El programa no puede operar con coeficientes racionales. Páselos a entero y vuelva a intentarlo.");
                        btContinuar.Hide();
                        return;
                    }
                    // Rellenar la lista con los divisores del temino independiente divididos por los divisores del coeficiente del termino con mayor exponente
                    List<long> divisores2 = AlgebraLineal.Divisores(coeficiente.Numerador);
                    posiblesraices = new List<Racional>();
                    foreach (long l in divisores)
                        foreach (long ll in divisores2)
                        {
                            Racional nuevaraiz = new Racional(l, ll);
                            if (!posiblesraices.Contains(nuevaraiz))
                                posiblesraices.Add(new Racional(l, ll));
                        }
                    if (!directa)
                    {
                        rtbdesarrollo.Text += "\n\nDivisores del término independiente divididos por los divisores del coeficiente del término con el exponente mas alto: \n";
                        foreach (Racional r in posiblesraices)
                            rtbdesarrollo.Text += r.ToString() + " ; ";
                    }
                    if (directa)
                    {
                        paso = 31;
                        ReduccionRuffini();
                    }
            }
            else if (paso == 31) // Llenar la lista de raices encontradas
            {
                if(directa)
                {
                    List<Racional> aux = new List<Racional>();
                    foreach (Racional r in posiblesraices)
                        aux.Add(new Racional(-r));
                    foreach (Racional r in aux)
                        posiblesraices.Add(r);
                }
                intervalosrojos = new List<int>();
                if(arrels == null)
                arrels = new List<Racional>();
                cantidadraices = arrels.Count;
                if (!directa)
                {
                    lbExplicacion.Text = "Probamos si cada uno de los divisores anteriores, cumple la igualdad a cero del polinomio:";
                    rtbdesarrollo.Text += "\n\n";
                }
                foreach (Racional r in posiblesraices)
                {
                    int inicio = 0;
                    if(!directa)
                    inicio = rtbdesarrollo.Text.Length-1; // Inicio de la nueva linea
                    Polinomio copia = new Polinomio(polinomio);
                    copia.Sustituir('X', new Racional(r));
                    if(!directa)
                    rtbdesarrollo.Text += "Con X=" + r.ToString() + "--->" + copia.ToString();
                    copia.Simplificar();
                    if (!directa)
                    rtbdesarrollo.Text += " = " + copia.ToString() + "\n";

                    if (copia.Terminos[0].Coeficiente.Numerador == 0)
                    {
                        arrels.Add(r);
                        if (!directa)
                        {
                            intervalosrojos.Add(inicio);
                            intervalosrojos.Add((rtbdesarrollo.Text.Length - 1) - inicio);
                        }
                    }
                }
                cantidadraices  = (arrels.Count - cantidadraices);
               
                if(cantidadraices == 0 ) // Si no se encuentran mas raices, dar por terminada la factorizacion
                {
                    if (!directa)
                    rtbdesarrollo.Text += "No se han encontrado más raices.";
                    FinalizarResolucion();
                }
                if (!directa)
                {
                    rtbdesarrollo.Focus();
                    rtbdesarrollo.Select(rtbdesarrollo.Text.Length - 1, 0);
                }
                if(directa)
                {
                    if (cantidadraices == 0)
                        FinalizarResolucion();
                    else
                    {
                        paso = 32;
                        ReduccionRuffini();
                    }
                }
            }
            else if (paso == 32) // MOstrar las raices encontradas y crear los polinomios factores
            {
                if (!directa)
                {
                    lbExplicacion.Text = "Los valores de X anteriores que cumplen la igualdad a cero del polinomio, son raices del polinomio.\nAhora se puede reducir el grado del polinomio por medio del método de Ruffini.";
                    rtbdesarrollo.Text += "\n\nRaices encontradas: \n";
                }
                foreach (Racional r in arrels)
                {
                    if(!directa)
                    rtbdesarrollo.Text += "X = " + r.ToString() + " ; ";
                    // Crear los polinomios factores con las raices encontradas y meterlos en la lista
                    ListaFactores.Add(new Polinomio(new List<Termino>() { new Termino(1, 'X', 1), new Termino(r * -1, ' ', 1) }));
                }

                if (!directa)
                {
                    rtbdesarrollo.Focus();
                    rtbdesarrollo.Select(rtbdesarrollo.Text.Length - 1, 0);
                }
                if(directa)
                {
                    paso = 33;
                    ReduccionRuffini();
                }
            }
            else if (paso == 33)
            {
                // Crear la lista de polinomios reducidos en cada iteracion el metodo de Ruffini Y añadir el polinomio introducido
                reducidos = new List<Polinomio>();
                // Añadir ceros en los exponentes ausentes del polinomio
                List<Termino> aux = new List<Termino>();
                int conta = 0;
                Racional exponente = 0;
                foreach(Termino t in polinomio.Terminos)
                {
                    if (conta == 0)
                    {
                        aux.Add(new Termino(t));
                        exponente = new Racional(t.Exponentes[0]);
                    }
                    else
                    {
                        Racional diferencia = exponente - t.Exponentes[0];
                        long dif = diferencia.Numerador / diferencia.Denominador;
                        if (dif == 1)
                        {
                            aux.Add(new Termino(t));
                            exponente = new Racional(t.Exponentes[0]);
                        }
                        else
                        {
                            for (int i = 1; i < dif; i++)
                                aux.Add(new Termino(0, 'X', exponente - i));
                            aux.Add(new Termino(t));
                            exponente = new Racional(t.Exponentes[0]);
                        }
                    }
                    conta++;
                }
                polinomio = new Polinomio(aux);
                reducidos.Add(new Polinomio(polinomio));
                if (!directa)
                {
                    // Reducir el polinomio con la primera raiz
                    paso = 39;
                    Reduccion();
                }
                else if ( directa)
                {
                    // Reducir el polinomio con todas las raices encontradas
                    foreach(Racional r in arrels)
                    {
                        Polinomio.Ruffini(ref polinomio, r);
                        // Añadir ceros a los terminos ausentes del polinomio reducido
                        aux = new List<Termino>();
                        conta = 0;
                        exponente = 0;
                        foreach (Termino t in polinomio.Terminos)
                        {
                            if (conta == 0)
                            {
                                aux.Add(new Termino(t));
                                exponente = new Racional(t.Exponentes[0]);
                            }
                            else
                            {
                                Racional diferencia = exponente - t.Exponentes[0];
                                long dif = diferencia.Numerador / diferencia.Denominador;
                                if (dif == 1)
                                {
                                    aux.Add(new Termino(t));
                                    exponente = new Racional(t.Exponentes[0]);
                                }
                                else
                                {
                                    for (int i = 1; i < dif; i++)
                                        aux.Add(new Termino(0, 'X', exponente - i));
                                    aux.Add(new Termino(t));
                                    exponente = new Racional(t.Exponentes[0]);
                                }
                            }
                            conta++;
                        }
                    }
                    ListaFactores.Add(polinomio);
                    FinalizarResolucion();
                }
            }
            else if (paso == 34 && arrels.Count + 1 > reducidos.Count)// Si la lista de raices es mayor que la lista de polinomios reducidos
            {
                if (!directa)
                {
                    lbExplicacion.Text = "Los valores obtenidos en el método de Ruffini, son los coeficientes de los términos del polinomio reducido, los cuales serán un grado menores que el anterior polinomio.";
                    rtbdesarrollo.Text += "\nRaiz: " + arrels[reducidos.Count - 2].ToString() + "  Polinomio reducido: " + reducidos[reducidos.Count - 1].ToString();
                    rtbdesarrollo.Focus();
                    rtbdesarrollo.Select(rtbdesarrollo.Text.Length - 1, 0);
                }
                else if(directa)
                {
                    paso = 35;
                    ReduccionRuffini();
                }
            }
            else if (paso == 35 && arrels.Count + 1 > reducidos.Count)
            {
                if(!directa)
                lbExplicacion.Text = "Volvemos a reducir el polinomio anterior con la siguiente raiz encontrada";
                paso = 39;
                Reduccion();
            }
            else // Una vez reducido el polinomio con todas las raices conocidas
            {
                //Escribir el ultimo polinomio reducido
                if (!directa)
                {
                    rtbdesarrollo.Text += "\nRaiz: " + arrels[reducidos.Count - 2].ToString() + "  Polinomio reducido: " + reducidos[reducidos.Count - 1].ToString();
                    rtbdesarrollo.Focus();
                    rtbdesarrollo.Select(rtbdesarrollo.Text.Length, 0);
                }
                //Volver a considerar el tipo de polinomio que ha quedado tras las reducciones anteriores
                polinomio = reducidos[reducidos.Count - 1];
                if (polinomio.Largo < 3) // Todas las raices encontradas
                {
                    FinalizarResolucion();
                   
                }
                else // Si no se han encontrado todas las raices
                {
                    // Escribir los nuevos factores encontrados en el richTextBox
                    if (!directa)
                    {
                        lbExplicacion.Text = "\nUna vez reducido el polinomio con las raices encontradas, usamos esas raices para obtener mas factores del polinomio inicial:";
                        rtbdesarrollo.Text = "\nFactores determinados hasta ahora:\n";
                        foreach (Polinomio p in ListaFactores)
                            rtbdesarrollo.Text += "\n" + p.ToString();
                        polinomio = new Polinomio(reducidos[reducidos.Count - 1]);
                        rtbdesarrollo.Text += "\nPolinomio reducido:\n" + polinomio.ToString();
                        ruffini.Hide();
                        paso = -1;
                    }
                    gradomayor = false;
                    if(directa)
                    {
                        paso = 0;
                        IniciarResolucion();
                    }
                }

            }
           // Pintar los intervalos rojos en el RichTextBox
            if (!directa)
            {
                if (intervalosrojos != null)
                    for (int i = 0; i < intervalosrojos.Count - 1; i += 2)
                    {
                        rtbdesarrollo.Select(intervalosrojos[i], intervalosrojos[i + 1]);
                        rtbdesarrollo.SelectionColor = Color.Red;
                    }
                rtbdesarrollo.Focus();
                rtbdesarrollo.Select(rtbdesarrollo.Text.Length - 1, 0);
            }
        }



        /// <summary>
        /// 
        /// REDUCE EL ULTIMO POLINOMIO EN LA LISTA DE POLINOMIOS REDUCIDOS CON LA RAIZ DE INDICE EN LA LISTA DE RAICES, 
        /// IGUAL QUE EL INDICE DEL ULTIMO POLINOMIO EN LA LISTA DE POLINOMIOS REDUCIDOS
        /// 
        /// </summary>
        /// 
        private void Reduccion()
        {
            Polinomio areducir = new Polinomio(reducidos[reducidos.Count - 1]); // Polinomio a ser reducido
            Racional raiz = arrels[reducidos.Count - 1]; // Raiz por la que va reducir el polinomio
            
            if( paso == 39 && reducidos.Count == 1) // Para la primera reducción
            {
                reducido = new Polinomio(new List<Termino>()); // Sera el polinomio una vez reducido
                lbExplicacion.Text = "Trazamos dos lineas cruzadas, y escrbimos los coeficientes de los términos del polinomio en la parte superior, y la raiz en la inferior izquierda:";
               // Crear el PictureBox donde se mostrara el desarrollo del metodo
                if (!directa)
                {
                    ruffini = new PictureBox();
                    Controls.Add(ruffini);
                    ruffini.Location = new Point(500, 300);
                    ruffini.BackColor = Color.SeaGreen;
                    ruffini.Size = new Size(this.ClientSize.Width, 300);
                    ruffini.Show();
                    // Trazar las lineas en el picturebox
                    graf = new Bitmap(ruffini.Size.Width, ruffini.Size.Height);
                    ruffini.Image = graf;
                    grafruf = Graphics.FromImage(graf);
                    Pen negro = new Pen(new SolidBrush(Color.Black), 5);
                    grafruf.DrawLine(negro, new Point(100, 0), new Point(100, 250));
                    grafruf.DrawLine(negro, new Point(0, 200), new Point(this.ClientSize.Width, 200));
                    // Escribir el polinomio y la raiz en sus situaciones respecto a las lineas anteriores
                    sitioraiz = new Point(20, 150);
                    grafruf.DrawString(raiz.ToString(), new Font("Dejavu Sans,", 20), new SolidBrush(Color.Black), sitioraiz);
                    sitiopolinomio = new Point(140, 30);
                    int cont = 0;
                    foreach (Termino t in areducir.Terminos)
                    {
                        grafruf.DrawString(t.Coeficiente.ToString(), new Font("Dejavu Sans,", 20), new SolidBrush(Color.Black), new Point(sitiopolinomio.X + (cont * 70), sitiopolinomio.Y));
                        cont++;
                    }
                    ruffini.Invalidate();
                }
                if(directa)
                {
                    paso = 40;
                    Reduccion();
                }
            }
            else if ( paso == 39 && reducidos.Count > 1)
            {
                reducido = new Polinomio(new List<Termino>()); // Sera el polinomio una vez reducido
                lbExplicacion.Text = "Borramos todos los valores anteriores, y escrbimos los coeficientes de los términos del polinomio reducido anterior en la parte superior, y la siguiente raiz en la inferior izquierda:";
                if (!directa)
                {
                    // Trazar las lineas en el picturebox
                    graf = new Bitmap(ruffini.Size.Width, ruffini.Size.Height);
                    ruffini.Image = graf;
                    grafruf = Graphics.FromImage(graf);
                    Pen negro = new Pen(new SolidBrush(Color.Black), 5);
                    grafruf.DrawLine(negro, new Point(100, 0), new Point(100, 250));
                    grafruf.DrawLine(negro, new Point(0, 200), new Point(this.ClientSize.Width, 200));
                    // Escribir el polinomio y la raiz en sus situaciones respecto a las lineas anteriores
                    sitioraiz = new Point(20, 150);
                    grafruf.DrawString(raiz.ToString(), new Font("Dejavu Sans,", 20), new SolidBrush(Color.Black), sitioraiz);
                    sitiopolinomio = new Point(140, 30);
                    int cont = 0;
                    foreach (Termino t in areducir.Terminos)
                    {
                        grafruf.DrawString(t.Coeficiente.ToString(), new Font("Dejavu Sans,", 20), new SolidBrush(Color.Black), new Point(sitiopolinomio.X + (cont * 70), sitiopolinomio.Y));
                        cont++;
                    }
                    ruffini.Invalidate();
                }
                if(directa)
                {
                    paso = 40;
                    Reduccion();
                }
            }
            else if (contador < areducir.Terminos.Count && paso == 40) // Escribir el primer coeficiente bajo la linea horizontal ( solo primera vez )
            {
                lbExplicacion.Text = "Bajamos el coeficiente del primer témino del polinomio y lo escribimos debajo de la linea horizontal.";
                sitioresultado = new Point(sitiopolinomio.X + (40*contador), 220);
                coeficiente = areducir.Terminos[contador].Coeficiente;
                if(!directa)
                grafruf.DrawString(coeficiente.ToString(), new Font("Dejavu Sans,", 20), new SolidBrush(Color.Chartreuse), sitioresultado);
                reducido.AñadirTermino(new Termino(coeficiente,areducir.Terminos[0].Variables[0],areducir.Terminos[0].Exponentes[0]-1));
                if (!directa)
                ruffini.Invalidate();
                contador++;
                if(directa)
                {
                    paso = 41;
                    Reduccion();
                }
            }
            else if ( contador < areducir.Terminos.Count && paso == 41 ) // Escribir el factor 
            {
                sitiofactor = new Point(sitiopolinomio.X + (contador * 65), sitiopolinomio.Y + 75);
                if (!directa && contador == 1) // Si es el primer factor el mismo sera la raiz por el coeficiente del primer termino
                {
                    factor = coeficiente * raiz;
                    if (!directa)
                    {
                        lbExplicacion.Text = "Multiplicamos la raiz por el coeficiente bajo la linea horizontal, y el resultado lo escribimos debajo del segundo término del polinomio.";
                        grafruf.DrawString(factor.ToString(), new Font("Dejavu Sans,", 20), new SolidBrush(Color.Red), sitiofactor);
                    }
                } 
                else if(!directa)// Si no es el primer factor, el mismo sera la raiz por el resultado anterior
                {
                    factor = resultado * raiz;
                    if (!directa)
                    {
                        lbExplicacion.Text = "Multiplicamos la raiz por el resultado anterior y escribimos el producto debajo de los coeficientes del polinomio.";
                        grafruf.DrawString(factor.ToString(), new Font("Dejavu Sans,", 20), new SolidBrush(Color.Red), sitiofactor);
                    }
                }
                if (!directa)
                ruffini.Invalidate();
                else if(directa)
                {
                    paso = 42;
                    Reduccion();
                }
            }
            else if( contador < areducir.Terminos.Count && paso == 42 ) // Escribir el resultado
            {
                if (!directa)
                {
                    lbExplicacion.Text = "Sumamos el coeficiente y el producto anteriores y el resultado lo escribimos bajo la línea horizontal.";
                    coeficiente = areducir.Terminos[contador].Coeficiente;
                    resultado = coeficiente + factor;
                    reducido.AñadirTermino(new Termino(resultado, areducir.Terminos[contador].Variables[0], areducir.Terminos[contador].Exponentes[0] - 1));
                    grafruf.DrawString(resultado.ToString(), new Font("Dejavu Sans", 20), new SolidBrush(Color.Chartreuse), new Point(sitioresultado.X + (70 * contador), sitioresultado.Y));
                    ruffini.Invalidate();
                }
                contador++;
                if (contador < areducir.Terminos.Count)
                {
                    paso = 40;
                    if(directa)
                    {
                        paso = 41;
                        Reduccion();
                    }
                }
                else
                {
                    if (!directa)
                    {
                        reducido.EliminarCeros();
                        reducidos.Add(reducido);
                    }
                    else
                    {
                        MessageBox.Show("Lin 1358 polinomio sin reducir: " + reducido.ToString());
                       Polinomio.Ruffini(ref reducido,raiz); 
                       reducidos.Add(reducido);
                       MessageBox.Show("Lin 1360   Raiz: " + raiz.ToString() + "Pol reducido: " + reducidos[reducidos.Count - 1].ToString());
                    }
                    paso = 33;
                    contador = 0;
                    if (directa)
                    {
                        paso = 35;
                        ReduccionRuffini();
                    }
                }
            }
           
        }

        /// <summary>
        /// 
        /// MUESTRA LOS POLINOMIOS FACTORES Y FINALIZA LA RESOLUCION
        /// 
        /// </summary>
        /// 
        private void FinalizarResolucion()
        {
            if (!directa)
            {
                // Meter el ultimo polinomio factor en la lista
                if (reducidos != null && reducidos.Count > 0)
                    ListaFactores.Add(new Polinomio(reducidos[reducidos.Count - 1]));
                else // Si no se han encontrado mas raices
                {
                    if (polinomio.Largo > 0)
                        ListaFactores.Add(new Polinomio(polinomio));
                    polinomio.Terminos.Clear();
                }
            }
            if (polinomio.Largo > 0 && !directa)
            {
                lbExplicacion.Text = "El polinomio reducido final, es uno de los polinomios en los que se ha factorizado el polinomio original. Los demás polinomios factores se construyen usando las raices encontradas.";
                int cont = 0;
                foreach (Racional r in arrels)
                {
                    rtbdesarrollo.Text += "\nX = " + r.ToString() + "  -----> " + ListaFactores[cont].ToString();
                    cont++;
                }
            }
            else if(!directa)
            {
                lbExplicacion.Text = "Hemos factorizado al máximo el polinomio inicial:";
                rtblistatipos.Select(0, rtblistatipos.Text.Length);
                rtblistatipos.SelectionColor = Color.Black;
                rtblistatipos.Select(0, 0);
            }
            if( !directa && rtbdesarrollo == null)
            {
                rtbdesarrollo = new RichTextBox();
                Controls.Add(rtbdesarrollo);
                rtbdesarrollo.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                rtbdesarrollo.BackColor = Color.SeaGreen;
                rtbdesarrollo.BorderStyle = BorderStyle.None;
                rtbdesarrollo.Size = new Size(500, 400);
                int tamañofuente = rtbdesarrollo.Size.Width / (polinomio.Largo + 31);
                rtbdesarrollo.Font = new Font(rtbExplicaciones.Font.FontFamily, tamañofuente);
                label2.Hide();
            }
            if (!directa)
            {
                rtbExplicaciones.Hide();
                // Mostrar como se construyen los polinomios factores
                rtbdesarrollo.Text += "\n\nFactorización del polinomio introducido:\n";
                foreach (Polinomio p in ListaFactores)
                    rtbdesarrollo.Text += "( " + p.ToString() + " ) *";
                // Eliminar el ultimo signo de multiplicar
                rtbdesarrollo.Select(0, rtbdesarrollo.Text.Length - 2);
                string lectura = rtbdesarrollo.SelectedText;
                rtbdesarrollo.Text = lectura;
                rtbdesarrollo.Focus();
                rtbdesarrollo.Select(rtbdesarrollo.Text.Length - 1, 0);
                btContinuar.Hide();
            }
            else if(directa)
            {
                lbExplicacion.Hide();
                if (polinomio.Largo == 0 || polinomio.Largo == 1 || cantidadraices == 0 )
                {
                    if (polinomio.Largo == 1)
                    {
                        Polinomio pol = new Polinomio(polinomio);
                        pol.EliminarCeros();
                        ListaFactores.Add(pol);
                        polinomio.Terminos.Clear();
                    }
                    // Eliminar posibles factores nulos
                    List <Polinomio> aux = new List <Polinomio>();
                    foreach (Polinomio p in ListaFactores)
                        if (p.Largo > 0)
                            aux.Add(p);
                    ListaFactores = aux;
                    rtbdesarrollo = new RichTextBox();
                    Controls.Add(rtbdesarrollo);
                    rtbdesarrollo.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                    rtbdesarrollo.BackColor = Color.SeaGreen;
                    rtbdesarrollo.BorderStyle = BorderStyle.None;
                    rtbdesarrollo.Size = new Size(500, 400);
                    int tamañofuente = rtbdesarrollo.Size.Width / (polinomio.Largo + 31);
                    rtbdesarrollo.Font = new Font(rtbExplicaciones.Font.FontFamily, tamañofuente);
                    rtbdesarrollo.Text += "\n\nFactorización del polinomio introducido:\n";
                    foreach (Polinomio p in ListaFactores)
                        rtbdesarrollo.Text += "( " + p.ToString() + " ) *";
                    // Eliminar el ultimo signo de multiplicar
                    rtbdesarrollo.Select(0, rtbdesarrollo.Text.Length - 2);
                    string lectura = rtbdesarrollo.SelectedText;
                    rtbdesarrollo.Text = lectura;
                    rtbdesarrollo.Focus();
                    rtbdesarrollo.Select(rtbdesarrollo.Text.Length - 1, 0);
                }
                else
                {
                    paso = 0;
                    IniciarResolucion();
                }
            }
        }



    }
}

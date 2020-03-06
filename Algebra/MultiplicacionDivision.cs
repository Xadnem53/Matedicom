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
    class MultiplicacionDivision:FormularioBase
    {
        Polinomio polinomio1; // Primer polinomio introducido 
        Polinomio polinomio2; // Segundo polinomio introducido a ser sumado o resta por el anterior.
        bool multiplicacion = true; // Si es false se tratara de una division
        Polinomio resto; // Para almacenar el resto de la division de polinomios
        Polinomio resultado = new Polinomio(new List<Termino>()); // Será el resultado de la multiplicacion o division de los dos polinomios introducidos
        List<int> intervalorojo = new List<int>(); // Para almacenar los intervalos de color rojo en el RichTextBox
        List<int> intervalozaul = new List<int>(); // Para almacenar los intervalos en color azul en el RichTextBox
        List<int> intervalonaranja = new List<int>(); // Para almacenar los intervalos en color naranja en el RichTextBox
        RichTextBox desarrollo;
        PictureBox desarrollodivision;
        new Timer timer1; // Para ejecucion automatica de operaciones
        int numerotermino1 = 0; // Para llevar la cuenta del termino del primer polinomio que esta operando
        int numerotermino2 = 0; // Para llevar la cuenta del termino del segundo polinomio que esta operando
        List<Polinomio> polinomios = new List<Polinomio>(); // Para guardar los polinomios producto del primer polinomio con algun termino del segundo
        Graphics lineas; // Para dibujar las lineas del cajetin en la division
        Bitmap areagrafica; // Para almacenar la imagen del PictureBox de forma permanente
      //  Point situacion; // Punto de referencia para situar el cajetin de la division
        RichTextBox cocientes; // Para escribir el polinomio resultado en cada estado de la resolucion de la division
        Polinomio resta; // Para almacenar el polinomio a restar en cada estado de resolucion en la division
        int inicio = 0;
        int final = 0; // Para usarlos como indices en los cambios de tamaño de letra en la division
        Graphics resaltes; // Para dibujar los resaltes en la division

        public MultiplicacionDivision(bool resoluciondirecta)
        {
            directa = resoluciondirecta;
        }

        public override void Cargar(object sender, EventArgs e)
        {
            this.Text = "Multiplicación y división de polinomios";
            lbExplicacion.Show();
            lbExplicacion.Text = "Introduzca el primer polinomio:";
            if (!directa)
                lbExplicacion.Text += "\n\nO pulse el botón [E] para resolución de ejemplo con valores por omisión.";
            EtiquetaFilas.Show();
            EtiquetaFilas.Location = new Point(20, lbExplicacion.Height + 20);
            EtiquetaFilas.Text = "Primer polinomio:";
            EtiquetaFilas.AutoSize = true;
            tbFilas.Show();
            tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Width + 10, EtiquetaFilas.Location.Y);
            tbFilas.Size = new Size(600, tbFilas.Height);
            tbFilas.KeyPress += Cajas_KeyPress;
            tbFilas.Focus();
            if (directa)
                btDefecto.Hide();
            btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 10, tbFilas.Location.Y);
            timer1 = new Timer();
            timer1.Interval = 1500;
            timer1.Tick += timer1_Tick;
        }


        /// <summary>
        ///  
        ///SALE DEL PROGRAMA Y LIBERA TODOS LOS RECURSOS
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
            timer1.Stop();
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
            defecto = true;
            polinomio1 = new Polinomio("3X^3+5X+3Y+2");
            label1.Location = new Point(10, tbFilas.Location.Y + 100);
            label1.BackColor = Color.SeaGreen;
            label1.ForeColor = Color.DarkBlue;
            label1.Text = "Polinomio 1" + "\n( ";
            label1.Text += polinomio1.ToString();
            label1.Text += " )";
            label1.Visible = true;

            polinomio2 = new Polinomio("5X^2-2X-Y+10");
            label2.Visible = true;
            label2.ForeColor = Color.DarkOrange;
            label2.Location = new Point(label1.Location.X + label1.Width + 25, label1.Location.Y);
            label2.BackColor = Color.Transparent;
            label2.Text = "Polinomio 2" + "\n( ";
            label2.Text += polinomio2.ToString();
            label2.Text += " )";

            IniciarResolucion();
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
            timer1.Stop();
            MultiplicacionDivision nuevo = new MultiplicacionDivision(directa);
            nuevo.Show();
            this.Close();
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN VALORES RACIONAL ENTEROS O SIGNOS CORRECTOS EN LAS CAJAS DE TEXTO DE LOS
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

            else if ((e.KeyChar >= (char)65 && e.KeyChar <= (char)90) || (e.KeyChar >= (char)97 && e.KeyChar <= (char)122)) // letras de la A a la Z mayusculas o minusculas
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
            else if (e.KeyChar == (char)'*')// Signo multiplicar
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
            else if (e.KeyChar == '0' || e.KeyChar == '1' || e.KeyChar == '2' || e.KeyChar == '3' || e.KeyChar == '4' || e.KeyChar == '5' ||
                        e.KeyChar == '6' || e.KeyChar == '7' || e.KeyChar == '8' || e.KeyChar == '9')
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }


        private void LeerPolinomio()
        {
            if (paso == 0)
            {
                try
                {
                    polinomio1 = new Polinomio(tbFilas.Text);
                    polinomio1.Ordenar();
                    tbFilas.Clear();
                    label1.Location = new Point(10, tbFilas.Location.Y + 100);
                    label1.BackColor = Color.SeaGreen;
                    label1.ForeColor = Color.DarkBlue;
                    label1.Text = "Polinomio 1" + "\n( ";
                    label1.Text += polinomio1.ToString();
                    label1.Text += " )";
                    label1.Visible = true;
                    EtiquetaFilas.Text = "Segundo polinomio";
                    lbExplicacion.Text = "Introduzca el segundo polinomio";
                    tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Width + 5, tbFilas.Location.Y);
                    btDefecto.Hide();
                    paso++;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Formato incorrecto, solo se pueden introducir los polinomios como suma o resta de terminos con coeficiente Racional o entero.\nEjemplo: 3X^2+5Y+7/4X^8/3");
                }
            }
            else
                try
                {
                    polinomio2 = new Polinomio(tbFilas.Text);
                    polinomio2.Ordenar();
                    tbFilas.Clear();
                    label2.Visible = true;
                    label2.ForeColor = Color.DarkOrange;
                    label2.Location = new Point(label1.Location.X + label1.Width + 20 , label1.Location.Y);
                    label2.BackColor = Color.Transparent;
                    label2.Text = "Polinomio 2" + "\n( ";
                    label2.Text += polinomio2.ToString();
                    label2.Text += " )";
                    paso++;
                    IniciarResolucion();
                }
                catch (Exception)
                {
                    MessageBox.Show("Formato incorrecto, solo se pueden introducir los polinomios como suma o resta de terminos con coeficiente Racional o entero.\nEjemplo: 3X^2+5Y+7/4X^8/3");
                }
        }
        /// <summary>
        /// 
        /// MOSTRAR LOS BOTONES DE OPCION
        /// 
        /// </summary>
        ///
        private void IniciarResolucion()
        {
            this.AcceptButton = null;
            // Mostrar los botones de opcion
            lbExplicacion.Text = "Elija la operación.";
            EtiquetaFilas.Text = "Operacion:";
            btPerfil.Show();
            btPerfil.Location = tbFilas.Location;
            btPerfil.Text = "Multiplicar";
            btPerfil.BackColor = Color.Silver;
            btPerfil.Click += Opcion;
            btAlzado.Show();
            btAlzado.Text = "Dividir";
            btAlzado.BackColor = Color.LightGray;
            btAlzado.Location = new Point(btPerfil.Location.X + btPerfil.Width, btPerfil.Location.Y);
            btAlzado.Click += Opcion;
            tbFilas.Hide();
            label3.Location = new Point(label1.Location.X + label1.Width + 5, label1.Location.Y);
            label3.BackColor = Color.Transparent;
            label3.Font = new Font(label3.Font.FontFamily, 20);
        }


        private void Opcion(object sender, EventArgs e)
        {
            // Ocultar la etiqueta 
            EtiquetaFilas.Hide();
            // Registrar si se trata de una multiplicacion o de una division
            Button pulsado = (Button)sender;
            if (pulsado.Name == "btPerfil") // Multiplicacion
                multiplicacion = true;
            else if (pulsado.Name == "btAlzado") // Division
                multiplicacion = false;
           
            label3.Show();
            if (multiplicacion)
                label3.Text = "*";
            else
                label3.Text = "/";
     
            btPerfil.Hide();
            btAlzado.Hide();
            resultado = new Polinomio(new List<Termino>());

            if (directa) // Para resolucion directa
            {
                if (multiplicacion)
                    lbExplicacion.Text = "Producto de los polinomios introducidos:";
                else
                    lbExplicacion.Text = "Cociente de los polinomios introducidos";

                label4.Show();
                label4.BackColor = Color.Transparent;
                label4.Location = new Point(label1.Location.X, label2.Location.Y + label2.Height + 20);
                label4.ForeColor = Color.Chartreuse;
                label4.Font = new Font(label4.Font.FontFamily, 20);
                if (multiplicacion)
                {
                    Polinomio resul = polinomio1 * polinomio2;
                    resul.EliminarCeros();
                    resul.Ordenar();
                    label4.Text = "Producto: " + resul.ToString();
                }
                else
                {
                    Polinomio resul = polinomio1.Cociente(polinomio2, ref resto);
                    if (resto.Largo == 0)
                        resto.AñadirTermino(new Termino("0"));
                    resul.EliminarCeros();
                    resul.Ordenar();
                    label4.Text = "Cociente: " + resul.ToString() + "    Resto: " + resto.ToString();
                }
                return;
            }
            else // Para resolución paso a paso
            {
                   // Crear el RichTextBox
                desarrollo = new RichTextBox();
                desarrollo.Show();
                desarrollo.BackColor = Color.SeaGreen;
                desarrollo.Location = new Point(label1.Location.X, label1.Location.Y + 100);
                desarrollo.Font = new Font("Dejavu Sans", 15);
                desarrollo.BorderStyle = BorderStyle.None;
                desarrollo.Size = new Size(this.ClientSize.Width, 300);
                this.Controls.Add(desarrollo);

                if (multiplicacion) // Multiplicacion paso a paso
                {
                    btContinuar.Click += ContinuarMultiplicacion;
                }
                else // Division paso a paso
                {

                    // Cambiar el tamaño del RichTextBox segun el largo del producto del polinomio a ser dividido y el primer termino del polinomio divisor
                    Polinomio aux = new Polinomio(new List<Termino>() { polinomio2.ObtenerTermino(0) });
                    desarrollo.Size = new Size((polinomio1 * aux).ToString().Length * 12, 300);
                    // Asignar el manejador adecuado al boton continuar
                    btContinuar.Click += ContinuarDivision;
                    // Copia del polinomio a ser dividido que se irá transformando en la resolucion
                    resto = new Polinomio(polinomio1);
                    // Crar el PictureBox para dibujar las lineas
                    desarrollodivision = new PictureBox();
                    // Crear el objeto Graphics para dibujar los resaltes
                    resaltes = this.CreateGraphics();
                    desarrollodivision.BackColor = Color.Transparent;
                    Controls.Add(desarrollodivision);
                    desarrollodivision.Visible = true;
                    desarrollodivision.Location = new Point(desarrollo.Location.X + desarrollo.Width + 10, desarrollo.Location.Y);
                    desarrollodivision.Size = new Size(this.ClientSize.Width, 40);
                    areagrafica = new Bitmap(desarrollodivision.Width, desarrollodivision.Height);
                    desarrollodivision.Image = areagrafica;
                    lineas = Graphics.FromImage(areagrafica);

                }
            }
          
            btContinuar.Show();
            btContinuar.Location = new Point(20, label2.Location.Y + label2.Height + 10);
          
            paso = 0;
            btContinuar.PerformClick();
        }

        ///<Summary
        ///
        /// CONTINUA LA RESOLUCION DE LA MULTIPLICACION SEGUN EL PASO EN EL QUE SE ENCUENTRE LA MISMA
        /// 
        ///</Summary>
        ///
        private void ContinuarMultiplicacion(object sender, EventArgs e)
        {
            if (paso < polinomio2.Largo) // Mientrar no se hayan multiplicado todos los terminos del 2º polinomio
            {
                if (paso == 0)
                {
                    desarrollo.Text = "Multiplicar el primer término del 2º polinomio por cada término del 1º:\n" + polinomio1.ToString() + " * ";
                    int inicio = desarrollo.Text.Length; // Inicio del termino del 2º polinomio
                    desarrollo.Text += polinomio2.ObtenerTermino(numerotermino2).ToString();
                    int largotermino = desarrollo.Text.Length - inicio; // Largo del termino del 2º polinomio
                    desarrollo.Text += " = ";

                    // Añadir el intervalo a la lista de intervalos de color naranja
                    intervalonaranja.Add(inicio);
                    intervalonaranja.Add(largotermino);

                    // Cambiar los caracteres de color naranja
                    for (int i = 0; i < intervalonaranja.Count; i += 2)
                    {
                        desarrollo.Select(intervalonaranja[i], intervalonaranja[i + 1]);
                        desarrollo.SelectionColor = Color.DarkOrange;
                        desarrollo.Select(0,0);
                    }
                    lbExplicacion.Text = "Para empezar, multiplicamos el primer término del segundo polinomio por cada uno de los termino del primer polinomio.";
                    btContinuar.Hide();
                    timer1.Start();
                }
                else
                {
                    string ordinal = "";
                    switch (paso)
                    {
                        case 1:
                            ordinal = " segundo ";
                            break;
                        case 2:
                            ordinal = " tercer ";
                            break;
                        case 3:
                            ordinal = " cuarto ";
                            break;
                        case 4:
                            ordinal = " quinto ";
                            break;
                        case 5:
                            ordinal = " sexto ";
                            break;
                        case 6:
                            ordinal = " septimo ";
                            break;
                        case 7:
                            ordinal = " octavo ";
                            break;
                        case 8:
                            ordinal = " noveno ";
                            break;
                        case 9:
                            ordinal = " décimo ";
                            break;
                        case 10:
                            ordinal = " undécimo ";
                            break;
                        default:
                            ordinal = " siguiente ";
                            break;
                    }

                    lbExplicacion.Text = " Continuamos multiplicando el siguiente termino del segundo polinomio por el primer polinomio.";
                    desarrollo.Text += "\nMultiplicar el " + ordinal + "término del segundo polinomio, por el primer polinomio:\n" + polinomio1.ToString() + " * ";
                    int inicio = desarrollo.Text.Length; // Inicio del termino del 2º polinomio
                    desarrollo.Text += polinomio2.ObtenerTermino(numerotermino2).ToString();
                    int largotermino = desarrollo.Text.Length - inicio; // Largo del termino del 2º polinomio
                    desarrollo.Text += " = ";
                    // Añadir el intervalo a la lista de intervalos de color naranja
                    intervalonaranja.Add(inicio);
                    intervalonaranja.Add(largotermino);
                    // Cambiar los caracteres de color rojo
                    for (int i = 0; i < intervalorojo.Count; i += 2)
                    {
                        desarrollo.Select(intervalorojo[i], intervalorojo[i + 1]);
                        desarrollo.SelectionColor = Color.Red;
                    }
                    // Cambiar los caracteres de color rojo
                    for (int i = 0; i < intervalonaranja.Count; i += 2)
                    {
                        desarrollo.Select(intervalonaranja[i], intervalonaranja[i + 1]);
                        desarrollo.SelectionColor = Color.DarkOrange;
                        desarrollo.Select(0,0);
                    }
                    btContinuar.Hide();
                    timer1.Start();
                }
            }
            else
            {
                btContinuar.Hide();
                lbExplicacion.Text = "Por último, sumamos todos los polinomios obtenidos en los productos anteriores ( en color cyan ).";
                Polinomio resul = polinomio1 * polinomio2;
                resul.EliminarCeros();
                    desarrollo.Text += "\n\nSumar los polinomios obtenidos en los productos anteriores y simplificar:\n" + resul.ToString();
                    // Cambiar los caracteres de color rojo
                    for (int i = 0; i < intervalorojo.Count; i += 2)
                    {
                        desarrollo.Select(intervalorojo[i], intervalorojo[i + 1]);
                        desarrollo.SelectionColor = Color.Red;
                    }
                    // Cambiar los caracteres de color naranja
                    for (int i = 0; i < intervalonaranja.Count; i += 2)
                    {
                        desarrollo.Select(intervalonaranja[i], intervalonaranja[i + 1]);
                        desarrollo.SelectionColor = Color.DarkOrange;
                        desarrollo.Select(0,0);
                    }
                    // Cambiar los caracteres de color azul
                    for (int i = 0; i < intervalozaul.Count; i += 2)
                    {
                        desarrollo.Select(intervalozaul[i], intervalozaul[i + 1]);
                        desarrollo.SelectionColor = Color.Cyan;
                        desarrollo.Select(0, 0);
                    }
            }
        }

        /// <summary>
        /// 
        /// TEMPORIZADOR PARA REALIZAR CADENAS DE PRODUCTOS EN LA MULTIPLICACION Y DIVISION
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private new void timer1_Tick(object sender, EventArgs e)
        {
            if (multiplicacion)
            {
                // Determinar el punto de inicio del primer polinomio
                int inicio = desarrollo.Text.LastIndexOf(':') + 4;
                if (numerotermino1 < polinomio1.Largo) // Si no se han multiplicado todos los terminos del primer polinomio
                {
                    //Determinar el punto de inicio del termino del primer polinomio a multiplicar
                    string aux = "";
                    for (int i = 0; i < numerotermino1; i++)
                    {
                        aux += polinomio1.ObtenerTermino(i).ToString() + " ";
                    }

                    inicio += aux.Length;
                    // Determinar el largo del termino del primer polinomio a multiplicar
                    int largotermino = polinomio1.ObtenerTermino(numerotermino1).ToString().Length;
                    // Añadir los intervalos de color rojo a la lista
                    intervalorojo.Add(inicio);
                    intervalorojo.Add(largotermino);
                    // Escribir el producto
                    desarrollo.Text += (polinomio1.ObtenerTermino(numerotermino1) * polinomio2.ObtenerTermino(numerotermino2));
                    numerotermino1++;

                    // Cambiar los caracteres de color rojo
                    for (int i = 0; i < intervalorojo.Count; i += 2)
                    {
                        desarrollo.Select(intervalorojo[i], intervalorojo[i + 1]);
                        desarrollo.SelectionColor = Color.Red;
                    }
                    // Cambiar los caracteres de color naranja
                    for (int i = 0; i < intervalonaranja.Count; i += 2)
                    {
                        desarrollo.Select(intervalonaranja[i], intervalonaranja[i + 1]);
                        desarrollo.SelectionColor = Color.DarkOrange;
                        desarrollo.Select(0, 0);
                    }
                }
                else
                {
                    int indiceigual = desarrollo.Text.LastIndexOf('=');
                    int largo = desarrollo.Text.Length - indiceigual;
                    intervalozaul.Add(indiceigual);
                    intervalozaul.Add(largo);
                    // Cambiar los caracteres de color azul
                    for (int i = 0; i < intervalozaul.Count; i += 2)
                    {
                        desarrollo.Select(intervalozaul[i], intervalozaul[i + 1]);
                        desarrollo.SelectionColor = Color.Cyan;
                        desarrollo.Select(0, 0);
                    }
                    btContinuar.Show();
                    btContinuar.Focus();
                    numerotermino1 = 0;
                    numerotermino2++;
                    timer1.Stop();
                    paso++;
                }
            }
            else if (!multiplicacion) // Para la division
            {
                btContinuar.Hide();
                if (contador < polinomio2.Largo)
                {
                    Resaltes(contador, 0);
                    desarrollo.Text += resta.ObtenerTermino(contador).ToString();
                    contador++;
                }
                else
                {
                    timer1.Stop();
                    desarrollo.Text += "\n ";
                    btContinuar.Show();
                    int final = desarrollo.Text.Length - intervalonaranja[intervalonaranja.Count-1];
                    intervalonaranja.Add(final);
                    CambioColor();
                    cocientes.SelectAll();
                    cocientes.SelectionColor = Color.Chartreuse;
                    cocientes.Select(0, 0);
                }
            }
            
        }


        ///<Summary
        ///
        /// CONTINUA LA RESOLUCION DE LA DIVISION SEGUN EL PASO EN EL QUE SE ENCUENTRE LA MISMA
        /// 
        ///</Summary>
        ///
        private void ContinuarDivision(object sender, EventArgs e)
        {
            if (paso == 0)
            {
                // Dibujar el cajetin
                Pen lapiz = new Pen(Color.Red);
                lapiz.Width = 1.5F;
                lineas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                lineas.DrawLine(lapiz, new Point(0,30), new Point(0,0));
                  lineas.DrawLine(lapiz, new Point(0,30), new Point(0 + (polinomio2.ToString().Length * 12) + 20, 30));
                lbExplicacion.Text = "Escribirmos los dos polinomios uno al lado del otro y cerramos el polinomio divisor entre líneas.";
                lineas.DrawString(polinomio2.ToString(), new Font("Dejavu Sans", 16), new SolidBrush(Color.Orange), new Point(10, 0));
                // Escribir el polinomio a ser dividido en el RichTextBox y añadir los intervalos de color azul a la lista
                intervalozaul.Add(0);
                desarrollo.Text = polinomio1.ToString() + "\n";
                
              /* 
                // Eliminar los espacios en blanco
                string lectura = desarrollo.Text;
                lectura = lectura.Substring(2);
                desarrollo.Text = lectura;
               */
                intervalozaul.Add(desarrollo.Text.Length);
                
                // Crear el RichTextBox para el polinomio resultado
                cocientes = new RichTextBox();
                cocientes.Location = new Point(desarrollodivision.Location.X, desarrollo.Location.Y+40);
                cocientes.Font = new Font("Dejavu Sans", 16);
                cocientes.BackColor = desarrollo.BackColor;
                cocientes.Size = new Size(this.ClientSize.Width, 100);
                cocientes.BorderStyle = BorderStyle.None;
                Controls.Add(cocientes);
                cocientes.Visible = true;
                cocientes.ForeColor = Color.Chartreuse;
                CambioColor();
                paso++;
            }
            else if ( paso == 1)
            {
                intervalorojo.Add(0);
                intervalorojo.Add(0);
               if( resto.ObtenerTermino(0).EsDivisible(polinomio2.ObtenerTermino(0))) // Si el termino es divisible
               {
                   if(resto.Largo == polinomio1.Largo) // Si es la primera iteracion
                   lbExplicacion.Text = "Dividimos el primer término del polinomio a dividir entre el primer termino del polinomio divisor, y el resultado lo anotamos bajo la línea trazada anteriormente:";
                   else// Para las siguientes iteraciones
                       lbExplicacion.Text = "Dividimos el primer término del resto obtenido entre el primer termino del polinomio divisor, y el resultado lo anotamos bajo la línea trazada anteriormente:";
                  
                   Termino cociente = resto.ObtenerTermino(0) / polinomio2.ObtenerTermino(0);
                   resultado.AñadirTermino(new Termino(cociente));
                   // Añadir el intervalo a resaltar en rojo en la lista
                   int iniciorojo = cocientes.Text.Length;
                   intervalorojo[0] = iniciorojo;            
                   cocientes.Text += cociente.ToString();                           
                   intervalorojo[1] = (cocientes.Text.Length-iniciorojo);
                   Resaltes(0, 0);
                   paso++;
               }
               else // Si los terminos no son divisibles, finalizar la division
               {
                   lbExplicacion.Text = "Como el primer término del resto no es divisible por el primer término del polinomio divisor, no se puede continuar la división. Se ha obtenido el cociente y resto finales.";
                   cocientes.ForeColor = Color.Chartreuse;
                   btContinuar.Hide();
               }
            }
            else if (paso == 2 ) // Para los pasos pares
            {
                CambioColor();
                this.Invalidate();
                lbExplicacion.Text = "Multiplicamos cada termino del polinomio divisor por el término obtenido anteriormente, y el resultado lo añadimos a un polinomio que anotamos debajo del polinomio a ser dividido.";
                contador = 0; // Puesta a cero del contador que se usara para finalizar el temporizador
                // Convertir el primer termino del polinomio divisor en un polinomio para poder realizar la multiplicacion
                Polinomio aux = new Polinomio(cocientes.Text);
                Polinomio factor = new Polinomio(new List<Termino> { aux.ObtenerTermino(aux.Terminos.Count - 1) });
              //  Polinomio factor = new Polinomio(new List<Termino> { resultado.ObtenerTermino(resultado.Terminos.Count - 1) });
                // Obtener el polinomio a restar
                factor.EliminarCeros();
                resta = polinomio2 * factor;
                resta.EliminarCeros();
                // Escribir en pantalla con los colores y alto de caracteres adecuados
                btContinuar.Hide();
                intervalonaranja.Add(desarrollo.Text.Length);
                timer1.Start();
                int finalnegro = desarrollo.Text.Length;
                paso++;
            }
            else // Para los pasos impares
            {
                intervalorojo.Clear();
                this.Invalidate();
                lbExplicacion.Text = "Restamos los polinomios anteriores";
                intervalonaranja.Add(desarrollo.Text.Length);
                // Calcular el resto
                resto = resto - resta;
                resto.EliminarCeros();
                resto.Simplificar();
                resto.Ordenar();
                // Dibujar la linea para la resta
                for (int i = 0; i < resto.ToString().Length; i++) 
                    desarrollo.Text += "_";
                int finalnegro = desarrollo.Text.Length - intervalonaranja[intervalonaranja.Count - 1];
                intervalonaranja.Add(finalnegro);
                int inicioazul = desarrollo.Text.Length;
               // Escribir el resto y cambiarlo a color azul
              //  resto.Ordenar();
                desarrollo.Text += "\n" + resto.ToString() + "\n";
                int finalazul = desarrollo.Text.Length;
                intervalozaul.Add(inicioazul);
                intervalozaul.Add(finalazul - inicioazul);
                CambioColor();
               // desarrollo.Text += "\n";
                paso = 1;
            }
            
          

        }


        private void Resaltes(int indicerojo, int indiceazul)
        {
            // Establecer el desplazamiento del resalte sobre el termino del divisor
            string anteriordivisor = "";
            for (int i = 0; i < indicerojo; i++)
                anteriordivisor += polinomio2.ObtenerTermino(i).ToString();
            int desplazamientorojo = anteriordivisor.Length * 12;
            // Dibujar el rectangulo para resaltar el termino del divisor
            Point inicio = new Point(desarrollodivision.Location.X + 30 + desplazamientorojo , desarrollodivision.Location.Y - 5);
            resaltes.FillRectangle(new SolidBrush(Color.Red) , new Rectangle(inicio, new Size(polinomio2.ObtenerTermino(0).ToString().Length * 12, 10)));
            resaltes.FillRectangle(new SolidBrush(Color.SeaGreen), new Rectangle(inicio, new Size(polinomio2.ObtenerTermino(0).ToString().Length * -12, 10)));

            // Dibujar el rectangulo para resaltar el primer termino del dividendo si es el primer paso
            //if (paso == 1 && resto.Largo == polinomio1.Largo)
              //  resaltes.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(new Point(desarrollo.Location.X + 20, desarrollodivision.Location.Y - 5), new Size(polinomio1.ObtenerTermino(0).ToString().Length * 12, 10)));
           // else if (paso == 1 && resto.Largo != polinomio1.Largo)
           // {
                desarrollo.Select(intervalozaul[intervalozaul.Count - 2]+1, resto.ObtenerTermino(0).ToString().Length+2);
                desarrollo.SelectionBackColor = Color.FromArgb(5, Color.Blue);
                desarrollo.SelectionColor = Color.White;
            //}
        }


        /// <summary>
        /// 
        /// PARA CAMBIAR DE COLOR LOS TERMINOS ADECUADOS EN LA RESOLUCION DE LA DIVISION
        /// 
        /// </summary>
        /// 
        private void CambioColor()
        {
            for (int i = 0; i < intervalorojo.Count; i += 2)
            {
                cocientes.Select(intervalorojo[i], intervalorojo[i + 1]);
                cocientes.SelectionColor = Color.Red;
            }
            for (int i = 0; i < intervalozaul.Count; i += 2)
            {
                desarrollo.Select(intervalozaul[0], intervalozaul[1]);
                desarrollo.SelectionColor = Color.Blue;
            }
            for (int i = 0; i < intervalonaranja.Count; i += 2)
            {
                desarrollo.Select(intervalonaranja[i], intervalonaranja[i + 1]);
                desarrollo.SelectionColor = Color.Black;
            }
            desarrollo.Select(0, 0);
        }


    }
}

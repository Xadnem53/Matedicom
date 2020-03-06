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
    class DecimalesAFraccion:FormularioBase
    {
        double numero = 0; // Será el numero decimal introducido
        long periodo = 0; // Será el periodo de los periodicos puros o mixtos
        long parteentera = 0; // Parte entera del decimal introducido
        long anteperiodo = 0; // Será el anteperiodo en los periodicos mixtos
        int digitosdecimales = 0; // Será la cantidad de digitos que tiene el decimal después de la coma.
        long numerad = 0; // Será el numerador de la fraccion equivalente al numero decimal
        long denominad = 0; // Será el denominador de la fraccion equivalente al numero decimal
        long auxi1 = 0; // Para usar en la resolucion del periodico mixto
        long auxi2 = 0; // Idem anterior

        public DecimalesAFraccion(bool tiporesolucion)
        {
            directa = tiporesolucion;
        }

        public override void Cargar(object sender, EventArgs e)
        {
            if (directa)
                btDefecto.Hide();
            this.Text = "Convertir decimales en racional";
            lbExplicacion.Show();
            if (!directa)
                lbExplicacion.Text = "Introducir un número decimal.\n\n O pulse el botón [E] para ejemplo con valores por defecto. ";
            else
                lbExplicacion.Text = "Introducir un número decimal.";
            EtiquetaFilas.Show();
            EtiquetaFilas.Text = "Decimal:";
            EtiquetaFilas.Location = new Point(100, 100);
            EtiquetaFilas.AutoSize = true;
            tbFilas.Show();
            tbFilas.Location = new Point(EtiquetaFilas.Location.X + EtiquetaFilas.Width + 5, EtiquetaFilas.Location.Y);
            tbFilas.Size = new Size(105, EtiquetaFilas.Height);
            tbFilas.Focus();
            tbFilas.KeyPress += tbFilas_KeyPress;
            if (directa)
                btDefecto.Hide();
            btDefecto.Location = new Point(tbFilas.Location.X + tbFilas.Width + 5, tbFilas.Location.Y);
            btContinuar.Click += btContinuar_Click;
            btContinuar.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + 50);
            label1.Location = new Point(btContinuar.Location.X, btContinuar.Location.Y + btContinuar.Height + 5);
            label1.BackColor = Color.Transparent;
            label1.AutoSize = true;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label2.BackColor = Color.Chartreuse;
            label2.AutoSize = true;
            label2.TextAlign = ContentAlignment.MiddleLeft;
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
            btDefecto.Hide();
            radioButton1.Show();
            radioButton2.Show();
            radioButton3.Show();
            lbExplicacion.Text = "Elija una opción:";
            radioButton1.Text = "Decimal no periodico a fraccioón.";
            radioButton2.Text = "Decimal periodico puro a fracción.";
            radioButton3.Text = "Decimal periodico mixto a fracción.";
            radioButton1.Click += RadioButton1_Click;
            radioButton2.Click += RadioButton2_Click;
            radioButton3.Click += RadioButton3_Click;
            radioButton1.Location = new Point(EtiquetaFilas.Location.X, EtiquetaFilas.Location.Y + EtiquetaFilas.Height + 5);
            radioButton2.Location = new Point(radioButton1.Location.X + 200, radioButton1.Location.Y);
            radioButton3.Location = new Point(radioButton2.Location.X + 200, radioButton2.Location.Y);
        }

        private void RadioButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            tbFilas.Text = "23,8635718341527";
            IniciarResolucion();
        }
        private void RadioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            tbFilas.Text = "1,33333333333333";
            IniciarResolucion();
        }
        private void RadioButton3_Click(object sender, EventArgs e)
        {
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            tbFilas.Text = "15,7853737373737";
            IniciarResolucion();
        }
        /// <summary>
        ///  
        /// REINICIA UN NUEVO FORMULARIO 
        /// 
        /// </summary>
        /// 
        public override void btNuevo_Click(object sender, EventArgs e)
        {
            DecimalesAFraccion nuevo = new DecimalesAFraccion(directa);
            nuevo.Show();
            this.Close();
        }

        /// <summary>
        /// 
        /// CONTROLA QUE SE INTRODUCEN SOLO NUMEROS O EL PUNTO COMO CARACTER DECIMAL
        /// 
        /// </summary>
        /// 
        private void tbFilas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (caja.Text.Length == 16 && caja.Text[0] != '0')
                IniciarResolucion();
            else if (caja.Text.Length == 17)
                IniciarResolucion();
            else if (caja.Text.Length > 17)
                e.Handled = true;
            else if (e.KeyChar == ',')
            {
                if (caja.Text.Contains(','))
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            else if (e.KeyChar == '.')
            {
                if (caja.Text.Contains(','))
                    e.Handled = true;
                else
                    e.KeyChar = ',';
            }
            else if (e.KeyChar == 13)
            {
                if (caja.Text.Length == 0)
                    e.Handled = true;
                else
                {
                    IniciarResolucion();
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == 8)
                e.Handled = false;
            else if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }


        private void btContinuar_Click(object sender,EventArgs e)
        {
            if (paso < 10)
                ResolucionDecimal();
            else if (paso >= 10 && paso < 20)
                ResolucionPeriodicoPuro();
            else if (paso >= 20)
                ResolucionPeriodicoMixto();
        }


        /// <summary>
        /// 
        /// INICIA LA RESOLUCION 
        /// 
        /// </summary>
        /// 
        private void IniciarResolucion()
        {
            btDefecto.Hide();
           if( !tbFilas.Text.Contains(','))
           {
               MessageBox.Show("El número introducido es un entero.");
               return;
           }
            // Obtener el numero introducido
            numero = Double.Parse(tbFilas.Text);
            if (directa)
            {
                Racional resultado = (Racional)numero;
                label2.Show();
                label2.Location = btContinuar.Location;
                btContinuar.Hide();
                label2.Text = resultado.ToString();
                lbExplicacion.Focus();
                return;
            }
            // Pasar el numero a Racional para comprobar de que tipo se trata
            Racional numeror = numero;
            // Comprobar de que tipo se trata ( decimal, periodico puro o periodico mixto )
            if( numeror.EsEntero())
            {
                MessageBox.Show("El número introducido es un entero.");
                return;
            }

            bool esperiodicopuro = numeror.EsPeriodicoPuro();
            bool esperiodicomixto = numeror.EsPeriodicoMixto();
            bool esdecimal = numeror.EsDecimal();

            btContinuar.Show();
            if (esdecimal)
            {
                lbExplicacion.Text = "El número introdocido, es un decimal aperiodico";
                paso = 1;
            }
            else if( esperiodicopuro)
            {
                lbExplicacion.Text = "El número introducido, es un decimal periodico puro.";
                paso = 10;
            }
            else if( esperiodicomixto)
            {
                lbExplicacion.Text = "El número introducido, es un decimal periodico mixto.";
                paso = 20;
            }
        }

        /// <summary>
        /// 
        /// CONVIERTE EL DECIMAL INTRODUCIDO EN RACIONAL, PASO A PASO
        /// 
        /// </summary>
        /// 
        private void ResolucionDecimal()
        {
            if(paso == 1)
            {
                int indicecoma = tbFilas.Text.IndexOf(',');
                string digits = tbFilas.Text.Substring(indicecoma + 1);
                digitosdecimales = digits.Length;
                lbExplicacion.Text += "\nPara pasar a fracción este decimal aperiodico, multiplicamos el número por la potencia de 10 equivalente a la cantidad de digitos después de la coma. En este caso: " + digitosdecimales.ToString();
                label1.Show();
                numerad = (long)(numero * Math.Pow(10, digitosdecimales));
                label1.Text = numero.ToString() + " * 10^" + digitosdecimales.ToString() + " = " + numerad.ToString();
                paso++;
            }
            else if( paso == 2)
            {
                lbExplicacion.Text += "\nEl resultado anterior, será el numerador de la fracción. El denominador, será la potencia de 10 por la que hemos multiplicado el decimal anteriormente, es decir: 10^"+digitosdecimales.ToString();
                denominad = (long)Math.Pow(10,digitosdecimales);
                label1.Text += "\n" + numerad + " / " + denominad.ToString();
                paso++;
            }
            else
            {
                label2.Show();
                Racional racio = new Racional(numerad, denominad);
                lbExplicacion.Text += "\nSimplificando, obtenemos el número racional equivalente al decimal introducido:";
                label2.Text = racio.ToString();
                label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                btContinuar.Hide();
                lbExplicacion.Focus();
            }
        }

        /// <summary>
        /// 
        /// CONVIERTE EL PERIODICO PURO EN RACIONAL, PASO A PASO
        /// 
        /// </summary>
        /// 
        private void ResolucionPeriodicoPuro()
        {
            if( paso == 10)
            {
                lbExplicacion.Text += "\nPara convertir el periodico puro en fracción, primero construimos un número compuesto por la parte entera y el periodo. A este número se le resta la parte entera, obteniendo así el numerador de la fracción:";
                label1.Show();
                string parteenteras = tbFilas.Text.Substring(0,tbFilas.Text.IndexOf(','));
                Racional numeror = numero;
                string periodos = numeror.Periodo().ToString();
                parteentera = Int64.Parse(parteenteras);
                periodo = Int64.Parse(periodos);
                string aux = parteenteras + periodos;
                long auxi = Int64.Parse(aux);
                numerad = auxi - parteentera;
                label1.Text = parteenteras + periodos + " - " + parteenteras +" = " + numerad.ToString();
                paso++;
            }
            else if( paso == 11)
            {
                lbExplicacion.Text += "\nEl denominador de la fracción, es un número compuesto por tantos ' 9 ' como digitos tenga el periodo:";
                string denominads = "";
                for (int i = 0; i < periodo.ToString().Length; i++)
                    denominads += "9";
                label1.Text += "\n" + denominads;
                denominad = Int64.Parse(denominads);
                paso++;
            }
            else if(paso == 12)
            {
                lbExplicacion.Text += "\nPor último, simplificando el numerador y denominador, obtenemos el racional equivalente al decimal periodico puro introducido.";
                label2.Show();
                label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                label2.Text = new Racional(numerad, denominad).ToString();
                btContinuar.Hide();
            }
        }


        private void ResolucionPeriodicoMixto()
        {
            if(paso == 20)
            {
                lbExplicacion.Text += "\nComenzamos construyendo un número con la parte entera, el anteperiodo y el periodo. ";
                label1.Show();
                string parteenteras = tbFilas.Text.Substring(0, tbFilas.Text.IndexOf(','));
                parteentera = Int64.Parse(parteenteras);
                Racional numeror = (Racional)(numero);
                anteperiodo = numeror.AntePeriodo();
                string anteperiodos = anteperiodo.ToString();
                periodo = numeror.Periodo();
                string periodos = periodo.ToString();
                string aux = parteenteras + anteperiodos + periodos;
                auxi1 = Int64.Parse(aux);
                label1.Text = aux;
                paso++;
            }
            else if( paso == 21)
            {
                lbExplicacion.Text +="\nAhora construimos otro número con la parte entera seguida del anteperiodo y se lo restamos al número contruido anteriormente, obteniendo así el numerador de la fracción:";
                string aux = parteentera.ToString() + anteperiodo.ToString();
                auxi2 = Int64.Parse(aux);
                numerad = auxi1 - auxi2;
                label1.Text += " - " + aux + " = " + numerad.ToString();
                paso++;
            }
            else if( paso == 22)
            {
                lbExplicacion.Text += "\nPara obtener el denominador de la fracción, construimos un número compuesto por tantos nueves como digitos tenga el periodo, y tantos ceros como digitos tenga el anteperiodo:";
                string denominads = "";
                for (int i = 0; i < periodo.ToString().Length; i++)
                    denominads += "9";
                for (int i = 0; i < anteperiodo.ToString().Length; i++)
                    denominads += "0";
                denominad = Int64.Parse(denominads);
                label1.Text += "\n Denominador = " + denominads;
                paso++;
            }
            else if(paso == 23)
            {
                lbExplicacion.Text += "\nPor último construimos el racional simplificando numerador y denominador:";
                label2.Show();
                label2.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + 5);
                label2.Text = new Racional(numerad, denominad).ToString();
                btContinuar.Hide();
                lbExplicacion.Focus();
            }
        }

    }
}

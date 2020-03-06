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
    public partial class DialogoValorIncognita : Form
    {
        private char variable;
        public Racional valorincognita;

        public DialogoValorIncognita(char var)
        {
            InitializeComponent();
            variable = var;
            lbIncognita.Text = "" + var;
        }

        public string EtiquetaDialogo
        {
            get
            {
                return lbPeticion.Text;
            }
            set
            {
                lbPeticion.Text = value;
            }
        }

        /// <summary>
        /// 
        ///  CONTROLA QUE SE INTRODUZCA UN RACIONAL O UN ENTERO CORRECTAMENTE
        /// 
        /// </summary>

        internal void tbValorIncognita_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox aux = (TextBox)sender;
            if (e.KeyChar == Convert.ToChar(13)) // Si se pulsa la tecla intro
            {
                if (aux.Text.Length > 0) // Si la caja de texto no está vacia.
                {
                    btAceptar.PerformClick();
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

        internal void btAceptar_Click(object sender, EventArgs e)
        {
             valorincognita = Racional.StringToRacional(tbValorIncognita.Text);
             this.Hide();
        }

    }    
}

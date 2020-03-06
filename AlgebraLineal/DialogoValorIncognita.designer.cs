namespace AlgebraLineal
{
    partial class DialogoValorIncognita
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
       // private System.ComponentModel.IContainer components = null;

      

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbValorIncognita = new System.Windows.Forms.TextBox();
            this.lbPeticion = new System.Windows.Forms.Label();
            this.btAceptar = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.lbIgual = new System.Windows.Forms.Label();
            this.lbIncognita = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbValorIncognita
            // 
            this.tbValorIncognita.Location = new System.Drawing.Point(136, 96);
            this.tbValorIncognita.Name = "tbValorIncognita";
            this.tbValorIncognita.Size = new System.Drawing.Size(100, 26);
            this.tbValorIncognita.TabIndex = 0;
            // 
            // lbPeticion
            // 
            this.lbPeticion.Font = new System.Drawing.Font("DejaVu Sans Condensed", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPeticion.Location = new System.Drawing.Point(24, 32);
            this.lbPeticion.Name = "lbPeticion";
            this.lbPeticion.Size = new System.Drawing.Size(224, 48);
            this.lbPeticion.TabIndex = 1;
            this.lbPeticion.Text = "Introduzca un valor para la incognita ";
            // 
            // btAceptar
            // 
            this.btAceptar.BackColor = System.Drawing.Color.PaleGreen;
            this.btAceptar.Location = new System.Drawing.Point(160, 192);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(104, 40);
            this.btAceptar.TabIndex = 2;
            this.btAceptar.Text = "Aceptar";
            this.btAceptar.UseVisualStyleBackColor = false;
            this.btAceptar.Click += new System.EventHandler(this.btAceptar_Click);
            // 
            // btCancelar
            // 
            this.btCancelar.BackColor = System.Drawing.Color.Coral;
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Location = new System.Drawing.Point(16, 192);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(104, 40);
            this.btCancelar.TabIndex = 3;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = false;
            // 
            // lbIgual
            // 
            this.lbIgual.AutoSize = true;
            this.lbIgual.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIgual.Location = new System.Drawing.Point(112, 96);
            this.lbIgual.Name = "lbIgual";
            this.lbIgual.Size = new System.Drawing.Size(24, 25);
            this.lbIgual.TabIndex = 4;
            this.lbIgual.Text = "=";
            // 
            // lbIncognita
            // 
            this.lbIncognita.AutoSize = true;
            this.lbIncognita.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIncognita.Location = new System.Drawing.Point(88, 96);
            this.lbIncognita.Name = "lbIncognita";
            this.lbIncognita.Size = new System.Drawing.Size(25, 29);
            this.lbIncognita.TabIndex = 5;
            this.lbIncognita.Text = "?";
            // 
            // DialogoValorIncognita
            // 
            this.AcceptButton = this.btAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancelar;
            this.ClientSize = new System.Drawing.Size(278, 245);
            this.Controls.Add(this.lbIncognita);
            this.Controls.Add(this.lbIgual);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btAceptar);
            this.Controls.Add(this.lbPeticion);
            this.Controls.Add(this.tbValorIncognita);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DialogoValorIncognita";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogoValorIncognita";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbValorIncognita;
        private System.Windows.Forms.Label lbPeticion;
        private System.Windows.Forms.Button btAceptar;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Label lbIgual;
        private System.Windows.Forms.Label lbIncognita;
    }
}
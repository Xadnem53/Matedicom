namespace AlgebraLineal
{
    partial class DialogoDiagonalizable
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btSalir = new System.Windows.Forms.Button();
            this.btContinuar = new System.Windows.Forms.Button();
            this.lbExplicacion = new System.Windows.Forms.Label();
	    this.rtbDesarrollo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btSalir
            // 
            this.btSalir.BackColor = System.Drawing.Color.Coral;
            this.btSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSalir.Location = new System.Drawing.Point(38, 475);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(112, 40);
            this.btSalir.TabIndex = 20;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = false;
            this.btSalir.Visible = false;
            this.btSalir.Click += new System.EventHandler(this.Salir);
            // 
            // btContinuar
            // 
            this.btContinuar.BackColor = System.Drawing.Color.LightGreen;
            this.btContinuar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btContinuar.Location = new System.Drawing.Point(38, 483);
            this.btContinuar.Name = "btContinuar";
            this.btContinuar.Size = new System.Drawing.Size(112, 40);
            this.btContinuar.TabIndex = 19;
            this.btContinuar.Text = "Continuar";
            this.btContinuar.UseVisualStyleBackColor = false;
            this.btContinuar.Click += new System.EventHandler(this.AvanzarPaso);
            // 
            // lbExplicacion
            // 
            this.lbExplicacion.Font = new System.Drawing.Font("DejaVu Sans Condensed", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExplicacion.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lbExplicacion.Location = new System.Drawing.Point(54, 27);
            this.lbExplicacion.Name = "lbExplicacion";
            this.lbExplicacion.Size = new System.Drawing.Size(900, 101);
            this.lbExplicacion.TabIndex = 18;
	     //
	    // rtbDesarrollo
	    //
	    this.rtbDesarrollo.Font = new System.Drawing.Font("DejaVu Sans Condensed", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
	    this.rtbDesarrollo.Location = new System.Drawing.Point(5,5);
	    this.rtbDesarrollo.Size = new System.Drawing.Size(993, 551);
	    this.rtbDesarrollo.Visible = true;
	    this.rtbDesarrollo.BackColor = System.Drawing.Color.Gray;
            // 
            // DialogoDiagonalizable
            // 
            this.AcceptButton = this.btContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 551);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.btContinuar);
            this.Controls.Add(this.lbExplicacion);
	    this.Controls.Add(this.rtbDesarrollo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(350, 80);
            this.Name = "DialogoDiagonalizable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Probar si la matriz con valores repetidos es diagonalizable";
            this.ResumeLayout(false);
	   

        }

        #endregion

        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Button btContinuar;
        private System.Windows.Forms.Label lbExplicacion;
	private System.Windows.Forms.RichTextBox rtbDesarrollo;
    }
}
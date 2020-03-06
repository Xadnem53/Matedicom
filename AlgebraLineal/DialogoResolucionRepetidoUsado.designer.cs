namespace AlgebraLineal
{
    partial class DialogoResolucionRepetidoUsado
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
           // this.lbDesarrollo = new System.Windows.Forms.Label();
            this.btContinuar = new System.Windows.Forms.Button();
           // this.lbExplicacion = new System.Windows.Forms.Label();
           // this.lbResultados = new System.Windows.Forms.Label();
	    this.rtbDesarrollo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
/*
            // 
            // lbDesarrollo
            // 
            this.lbDesarrollo.AutoSize = true;
            this.lbDesarrollo.Font = new System.Drawing.Font("DejaVu Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDesarrollo.Location = new System.Drawing.Point(152, 208);
            this.lbDesarrollo.MaximumSize = new System.Drawing.Size(900, 1500);
            this.lbDesarrollo.Name = "lbDesarrollo";
            this.lbDesarrollo.Size = new System.Drawing.Size(130, 24);
            this.lbDesarrollo.TabIndex = 25;
            this.lbDesarrollo.Text = "lbDesarrollo";
*/	    //
	    // rtbDesarrollo
	    //
	    this.rtbDesarrollo.Font = new System.Drawing.Font("DejaVu Sans Condensed", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
	    this.rtbDesarrollo.Location = new System.Drawing.Point(5,5);
	    this.rtbDesarrollo.Size = new System.Drawing.Size(679, 635);
	    this.rtbDesarrollo.Visible = true;
	    this.rtbDesarrollo.BackColor = System.Drawing.Color.Gray;
            // 
            // btContinuar
            // 
            this.btContinuar.BackColor = System.Drawing.Color.LightGreen;
            this.btContinuar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btContinuar.Location = new System.Drawing.Point(24, 600);
            this.btContinuar.Name = "btContinuar";
            this.btContinuar.Size = new System.Drawing.Size(112, 40);
            this.btContinuar.TabIndex = 24;
            this.btContinuar.Text = "Continuar";
            this.btContinuar.UseVisualStyleBackColor = false;
            this.btContinuar.Click += new System.EventHandler(this.btContinuar_Click);
/*
            // 
            // lbExplicacion
            // 
            this.lbExplicacion.AutoSize = true;
            this.lbExplicacion.Font = new System.Drawing.Font("DejaVu Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExplicacion.Location = new System.Drawing.Point(40, 32);
            this.lbExplicacion.MaximumSize = new System.Drawing.Size(500, 1500);
            this.lbExplicacion.Name = "lbExplicacion";
            this.lbExplicacion.Size = new System.Drawing.Size(143, 24);
            this.lbExplicacion.TabIndex = 23;
            this.lbExplicacion.Text = "lbExplicacion";

            // 
            // lbResultados
            // 
            this.lbResultados.AutoSize = true;
            this.lbResultados.Font = new System.Drawing.Font("DejaVu Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultados.Location = new System.Drawing.Point(552, 16);
            this.lbResultados.MaximumSize = new System.Drawing.Size(900, 1500);
            this.lbResultados.Name = "lbResultados";
            this.lbResultados.Size = new System.Drawing.Size(0, 24);
            this.lbResultados.TabIndex = 27;
*/
            // 
            // DialogoResolucionRepetidoUsado
            // 
            this.AcceptButton = this.btContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 635);
           // this.Controls.Add(this.lbResultados);
          //  this.Controls.Add(this.lbDesarrollo);
            this.Controls.Add(this.btContinuar);
            //this.Controls.Add(this.lbExplicacion);
	    this.Controls.Add(this.rtbDesarrollo);
            this.Location = new System.Drawing.Point(700, 150);
            this.Name = "DialogoResolucionRepetidoUsado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DialogoResolucionRepetidoUsado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

      // private System.Windows.Forms.Label lbDesarrollo;
        private System.Windows.Forms.Button btContinuar;
        //private System.Windows.Forms.Label lbExplicacion;
        //private System.Windows.Forms.Label lbResultados;
	private System.Windows.Forms.RichTextBox rtbDesarrollo;
    }
}
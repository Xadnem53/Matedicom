namespace Matedicom
{
    partial class ControlesFlotantes
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
            this.pnParametro = new System.Windows.Forms.Panel();
            this.tbParametro = new System.Windows.Forms.TextBox();
            this.sbValor = new System.Windows.Forms.HScrollBar();
            this.lbTituloParametro = new System.Windows.Forms.Label();
            this.pnParametro.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnParametro
            // 
            this.pnParametro.BackColor = System.Drawing.Color.Gray;
            this.pnParametro.Controls.Add(this.tbParametro);
            this.pnParametro.Controls.Add(this.sbValor);
            this.pnParametro.Controls.Add(this.lbTituloParametro);
            this.pnParametro.Location = new System.Drawing.Point(24, -2);
            this.pnParametro.Name = "pnParametro";
            this.pnParametro.Size = new System.Drawing.Size(680, 192);
            this.pnParametro.TabIndex = 339;
            this.pnParametro.Visible = false;
            // 
            // tbParametro
            // 
            this.tbParametro.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbParametro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbParametro.Location = new System.Drawing.Point(304, 56);
            this.tbParametro.Name = "tbParametro";
            this.tbParametro.Size = new System.Drawing.Size(92, 35);
            this.tbParametro.TabIndex = 196;
            this.tbParametro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sbValor
            // 
            this.sbValor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.sbValor.Location = new System.Drawing.Point(32, 112);
            this.sbValor.Maximum = 1000;
            this.sbValor.Minimum = -1000;
            this.sbValor.Name = "sbValor";
            this.sbValor.Size = new System.Drawing.Size(608, 40);
            this.sbValor.TabIndex = 196;
            this.sbValor.Value = 1;
            // 
            // lbTituloParametro
            // 
            this.lbTituloParametro.AutoSize = true;
            this.lbTituloParametro.BackColor = System.Drawing.Color.Gray;
            this.lbTituloParametro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTituloParametro.ForeColor = System.Drawing.Color.LightGray;
            this.lbTituloParametro.Location = new System.Drawing.Point(248, 16);
            this.lbTituloParametro.Name = "lbTituloParametro";
            this.lbTituloParametro.Size = new System.Drawing.Size(201, 25);
            this.lbTituloParametro.TabIndex = 93;
            this.lbTituloParametro.Text = "Valor del parámetro";
            // 
            // ControlesFlotantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(732, 189);
            this.ControlBox = false;
            this.Controls.Add(this.pnParametro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ControlesFlotantes";
            this.Opacity = 0.05D;
            this.Text = "Controles_Flotantes";
            this.TopMost = true;
            this.pnParametro.ResumeLayout(false);
            this.pnParametro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnParametro;
        public System.Windows.Forms.TextBox tbParametro;
        public System.Windows.Forms.HScrollBar sbValor;
        public System.Windows.Forms.Label lbTituloParametro;
    }
}
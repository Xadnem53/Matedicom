using System.Windows.Forms;


namespace Matedicom
{
    partial class MenuPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.btSalir = new System.Windows.Forms.Button();
            this.CalculoVectorial = new System.Windows.Forms.Button();
            this.AlgebraLineal = new System.Windows.Forms.Button();
            this.Titulo = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btAlgebra = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSalir
            // 
            this.btSalir.BackColor = System.Drawing.Color.Red;
            this.btSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSalir.ForeColor = System.Drawing.SystemColors.Control;
            this.btSalir.Location = new System.Drawing.Point(668, 450);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(112, 48);
            this.btSalir.TabIndex = 9;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = false;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
	    this.btSalir.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            // 
            // CalculoVectorial
            // 
            this.CalculoVectorial.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.CalculoVectorial.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalculoVectorial.Location = new System.Drawing.Point(116, 212);
            this.CalculoVectorial.Name = "CalculoVectorial";
            this.CalculoVectorial.Size = new System.Drawing.Size(568, 40);
            this.CalculoVectorial.TabIndex = 7;
            this.CalculoVectorial.Text = "Cálculo Vectorial";
            this.CalculoVectorial.UseVisualStyleBackColor = false;
            this.CalculoVectorial.Click += new System.EventHandler(this.Vectores_Click);
	    this.CalculoVectorial.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
	    this.CalculoVectorial.MaximumSize = new System.Drawing.Size(568,40);
            // 
            // AlgebraLineal
            // 
            this.AlgebraLineal.BackColor = System.Drawing.Color.DarkKhaki;
            this.AlgebraLineal.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlgebraLineal.Location = new System.Drawing.Point(116, 148);
            this.AlgebraLineal.Name = "AlgebraLineal";
            this.AlgebraLineal.Size = new System.Drawing.Size(568, 40);
            this.AlgebraLineal.TabIndex = 6;
            this.AlgebraLineal.Text = "Álgebra Lineal";
            this.AlgebraLineal.UseVisualStyleBackColor = false;
            this.AlgebraLineal.Click += new System.EventHandler(this.AlgebraLineal_Click);
	    this.AlgebraLineal.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            // 
            // Titulo
            // 
            this.Titulo.BackColor = System.Drawing.Color.SeaGreen;
            this.Titulo.Font = new System.Drawing.Font("DejaVu Sans Mono", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titulo.Location = new System.Drawing.Point(450, 52);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(1, 56);
	   // this.Titulo.AutoSize = true;
            this.Titulo.TabIndex = 5;
            this.Titulo.Text = "Matedicom";
            this.Titulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Titulo.Anchor = (AnchorStyles.Top |  AnchorStyles.Left);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowItemReorder = true;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1769, 33);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.acercaDeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem});
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(101, 29);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            // 
            // copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem
            // 
            this.copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem.Name = "copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem";
            this.copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem.Size = new System.Drawing.Size(385, 26);
            this.copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem.Text = "Pogramado por: José Antonio Álvarez Rodríguez\nSoftware libre.\nCódigo fuente en C# \nVersión Linux de AlgebraLineal disponible.";
            // 
            // btAlgebra
            // 
            this.btAlgebra.BackColor = System.Drawing.Color.OliveDrab;
            this.btAlgebra.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAlgebra.Location = new System.Drawing.Point(116, 276);
            this.btAlgebra.Name = "btAlgebra";
            this.btAlgebra.Size = new System.Drawing.Size(568, 40);
            this.btAlgebra.TabIndex = 10;
            this.btAlgebra.Text = "Álgebra";
            this.btAlgebra.UseVisualStyleBackColor = false;
            this.btAlgebra.Click += new System.EventHandler(this.Algebra_Click);
	    this.btAlgebra.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.btAlgebra);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.CalculoVectorial);
            this.Controls.Add(this.AlgebraLineal);
            this.Controls.Add(this.Titulo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuPrincipal";
            this.Text = "MatedicomR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
	    this.ClientSizeChanged += Maximizado;
        }

        #endregion

        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Button CalculoVectorial;
        private System.Windows.Forms.Button AlgebraLineal;
        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRighJoseAntonioAlvarezRodriguezToolStripMenuItem;
        private System.Windows.Forms.Button btAlgebra;
    }
}


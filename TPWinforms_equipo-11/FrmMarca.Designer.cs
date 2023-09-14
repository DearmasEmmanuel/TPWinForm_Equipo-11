namespace TPWinforms_equipo_11
{
    partial class FrmMarca
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvMarca = new System.Windows.Forms.DataGridView();
            this.btnMarcaAgregar = new System.Windows.Forms.Button();
            this.lbTituloMarca = new System.Windows.Forms.Label();
            this.lbNuevaMarca = new System.Windows.Forms.Label();
            this.txNuevaMarca = new System.Windows.Forms.TextBox();
            this.btnEliminarMarca = new System.Windows.Forms.Button();
            this.lbMarcaEliminar = new System.Windows.Forms.Label();
            this.txtIdmarca = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarca)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMarca
            // 
            this.dgvMarca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarca.Location = new System.Drawing.Point(119, 58);
            this.dgvMarca.Name = "dgvMarca";
            this.dgvMarca.Size = new System.Drawing.Size(240, 150);
            this.dgvMarca.TabIndex = 0;
            // 
            // btnMarcaAgregar
            // 
            this.btnMarcaAgregar.Location = new System.Drawing.Point(296, 258);
            this.btnMarcaAgregar.Name = "btnMarcaAgregar";
            this.btnMarcaAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnMarcaAgregar.TabIndex = 1;
            this.btnMarcaAgregar.Text = "Agregar";
            this.btnMarcaAgregar.UseVisualStyleBackColor = true;
            this.btnMarcaAgregar.Click += new System.EventHandler(this.btnMarcaAgregar_Click);
            // 
            // lbTituloMarca
            // 
            this.lbTituloMarca.AutoSize = true;
            this.lbTituloMarca.Location = new System.Drawing.Point(74, 24);
            this.lbTituloMarca.Name = "lbTituloMarca";
            this.lbTituloMarca.Size = new System.Drawing.Size(42, 13);
            this.lbTituloMarca.TabIndex = 2;
            this.lbTituloMarca.Text = "Marcas";
            // 
            // lbNuevaMarca
            // 
            this.lbNuevaMarca.AutoSize = true;
            this.lbNuevaMarca.Location = new System.Drawing.Point(85, 260);
            this.lbNuevaMarca.Name = "lbNuevaMarca";
            this.lbNuevaMarca.Size = new System.Drawing.Size(72, 13);
            this.lbNuevaMarca.TabIndex = 3;
            this.lbNuevaMarca.Text = "Nueva Marca";
            // 
            // txNuevaMarca
            // 
            this.txNuevaMarca.Location = new System.Drawing.Point(179, 260);
            this.txNuevaMarca.Name = "txNuevaMarca";
            this.txNuevaMarca.Size = new System.Drawing.Size(100, 20);
            this.txNuevaMarca.TabIndex = 4;
            // 
            // btnEliminarMarca
            // 
            this.btnEliminarMarca.Location = new System.Drawing.Point(296, 344);
            this.btnEliminarMarca.Name = "btnEliminarMarca";
            this.btnEliminarMarca.Size = new System.Drawing.Size(75, 23);
            this.btnEliminarMarca.TabIndex = 6;
            this.btnEliminarMarca.Text = "Eliminar";
            this.btnEliminarMarca.UseVisualStyleBackColor = true;
            this.btnEliminarMarca.Click += new System.EventHandler(this.btnEliminarMarca_Click);
            // 
            // lbMarcaEliminar
            // 
            this.lbMarcaEliminar.AutoSize = true;
            this.lbMarcaEliminar.Location = new System.Drawing.Point(108, 346);
            this.lbMarcaEliminar.Name = "lbMarcaEliminar";
            this.lbMarcaEliminar.Size = new System.Drawing.Size(49, 13);
            this.lbMarcaEliminar.TabIndex = 7;
            this.lbMarcaEliminar.Text = "Id Marca";
            // 
            // txtIdmarca
            // 
            this.txtIdmarca.Location = new System.Drawing.Point(179, 346);
            this.txtIdmarca.Name = "txtIdmarca";
            this.txtIdmarca.Size = new System.Drawing.Size(100, 20);
            this.txtIdmarca.TabIndex = 8;
            // 
            // FrmMarca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 450);
            this.Controls.Add(this.txtIdmarca);
            this.Controls.Add(this.lbMarcaEliminar);
            this.Controls.Add(this.btnEliminarMarca);
            this.Controls.Add(this.txNuevaMarca);
            this.Controls.Add(this.lbNuevaMarca);
            this.Controls.Add(this.lbTituloMarca);
            this.Controls.Add(this.btnMarcaAgregar);
            this.Controls.Add(this.dgvMarca);
            this.Name = "FrmMarca";
            this.Text = "FrmMarca";
            this.Load += new System.EventHandler(this.FrmMarca_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarca)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMarca;
        private System.Windows.Forms.Button btnMarcaAgregar;
        private System.Windows.Forms.Label lbTituloMarca;
        private System.Windows.Forms.Label lbNuevaMarca;
        private System.Windows.Forms.TextBox txNuevaMarca;
        private System.Windows.Forms.Button btnEliminarMarca;
        private System.Windows.Forms.Label lbMarcaEliminar;
        private System.Windows.Forms.TextBox txtIdmarca;
    }
}
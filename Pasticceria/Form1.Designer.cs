namespace Pasticceria
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxOrdini = new System.Windows.Forms.ListBox();
            this.listBoxDispensa = new System.Windows.Forms.ListBox();
            this.listBoxSpesa = new System.Windows.Forms.ListBox();
            this.buttonCarica = new System.Windows.Forms.Button();
            this.buttonCompra = new System.Windows.Forms.Button();
            this.buttonCucina = new System.Windows.Forms.Button();
            this.labelMessaggio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxOrdini
            // 
            this.listBoxOrdini.FormattingEnabled = true;
            this.listBoxOrdini.Location = new System.Drawing.Point(48, 65);
            this.listBoxOrdini.Name = "listBoxOrdini";
            this.listBoxOrdini.Size = new System.Drawing.Size(219, 368);
            this.listBoxOrdini.TabIndex = 0;
            this.listBoxOrdini.SelectedIndexChanged += new System.EventHandler(this.listBoxOrdini_SelectedIndexChanged);
            // 
            // listBoxDispensa
            // 
            this.listBoxDispensa.FormattingEnabled = true;
            this.listBoxDispensa.Location = new System.Drawing.Point(300, 65);
            this.listBoxDispensa.Name = "listBoxDispensa";
            this.listBoxDispensa.Size = new System.Drawing.Size(219, 368);
            this.listBoxDispensa.TabIndex = 1;
            this.listBoxDispensa.SelectedIndexChanged += new System.EventHandler(this.listBoxDispensa_SelectedIndexChanged);
            // 
            // listBoxSpesa
            // 
            this.listBoxSpesa.FormattingEnabled = true;
            this.listBoxSpesa.Location = new System.Drawing.Point(556, 65);
            this.listBoxSpesa.Name = "listBoxSpesa";
            this.listBoxSpesa.Size = new System.Drawing.Size(219, 368);
            this.listBoxSpesa.TabIndex = 2;
            // 
            // buttonCarica
            // 
            this.buttonCarica.Location = new System.Drawing.Point(12, 3);
            this.buttonCarica.Name = "buttonCarica";
            this.buttonCarica.Size = new System.Drawing.Size(75, 23);
            this.buttonCarica.TabIndex = 3;
            this.buttonCarica.Text = "Carica Ordini";
            this.buttonCarica.UseVisualStyleBackColor = true;
            this.buttonCarica.Click += new System.EventHandler(this.buttonCarica_Click);
            // 
            // buttonCompra
            // 
            this.buttonCompra.Location = new System.Drawing.Point(93, 3);
            this.buttonCompra.Name = "buttonCompra";
            this.buttonCompra.Size = new System.Drawing.Size(75, 23);
            this.buttonCompra.TabIndex = 4;
            this.buttonCompra.Text = "Compra Ingredienti";
            this.buttonCompra.UseVisualStyleBackColor = true;
            this.buttonCompra.Click += new System.EventHandler(this.buttonCompra_Click);
            // 
            // buttonCucina
            // 
            this.buttonCucina.Location = new System.Drawing.Point(174, 3);
            this.buttonCucina.Name = "buttonCucina";
            this.buttonCucina.Size = new System.Drawing.Size(75, 23);
            this.buttonCucina.TabIndex = 5;
            this.buttonCucina.Text = "Cucina Dolci";
            this.buttonCucina.UseVisualStyleBackColor = true;
            this.buttonCucina.Click += new System.EventHandler(this.buttonCucina_Click);
            // 
            // labelMessaggio
            // 
            this.labelMessaggio.AutoSize = true;
            this.labelMessaggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.4F);
            this.labelMessaggio.Location = new System.Drawing.Point(297, 8);
            this.labelMessaggio.Name = "labelMessaggio";
            this.labelMessaggio.Size = new System.Drawing.Size(0, 15);
            this.labelMessaggio.TabIndex = 6;
            this.labelMessaggio.Click += new System.EventHandler(this.labelMessaggio_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelMessaggio);
            this.Controls.Add(this.buttonCucina);
            this.Controls.Add(this.buttonCompra);
            this.Controls.Add(this.buttonCarica);
            this.Controls.Add(this.listBoxSpesa);
            this.Controls.Add(this.listBoxDispensa);
            this.Controls.Add(this.listBoxOrdini);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxOrdini;
        private System.Windows.Forms.ListBox listBoxDispensa;
        private System.Windows.Forms.ListBox listBoxSpesa;
        private System.Windows.Forms.Button buttonCarica;
        private System.Windows.Forms.Button buttonCompra;
        private System.Windows.Forms.Button buttonCucina;
        private System.Windows.Forms.Label labelMessaggio;
    }
}


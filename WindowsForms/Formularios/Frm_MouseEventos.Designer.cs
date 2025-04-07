namespace WindowsForms
{
    partial class Frm_MouseEventos
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Mouse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Mouse
            // 
            this.Btn_Mouse.Location = new System.Drawing.Point(268, 154);
            this.Btn_Mouse.Name = "Btn_Mouse";
            this.Btn_Mouse.Size = new System.Drawing.Size(247, 107);
            this.Btn_Mouse.TabIndex = 0;
            this.Btn_Mouse.UseVisualStyleBackColor = true;
            this.Btn_Mouse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Mouse_MouseDown);
            this.Btn_Mouse.MouseEnter += new System.EventHandler(this.Btn_Mouse_MouseEnter);
            this.Btn_Mouse.MouseLeave += new System.EventHandler(this.Btn_Mouse_MouseLeave);
            this.Btn_Mouse.MouseHover += new System.EventHandler(this.Btn_Mouse_MouseHover);
            this.Btn_Mouse.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Mouse_MouseUp);
            // 
            // Frm_MouseEventos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_Mouse);
            this.Name = "Frm_MouseEventos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Mouse;
    }
}

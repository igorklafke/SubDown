namespace SubDown
{
    partial class SubDownForm
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
            this.components = new System.ComponentModel.Container();
            this.textBoxUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSenha = new System.Windows.Forms.TextBox();
            this.treeViewLegendas = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxDiretorios = new System.Windows.Forms.ListBox();
            this.contextMenuStripDiretorios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.adicionarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonBaixarLegenda = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.adicionarPastaButton = new System.Windows.Forms.Button();
            this.contextMenuStripDiretorios.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUsuario
            // 
            this.textBoxUsuario.Location = new System.Drawing.Point(12, 25);
            this.textBoxUsuario.Name = "textBoxUsuario";
            this.textBoxUsuario.Size = new System.Drawing.Size(157, 20);
            this.textBoxUsuario.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuário";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Senha";
            // 
            // textBoxSenha
            // 
            this.textBoxSenha.Location = new System.Drawing.Point(12, 64);
            this.textBoxSenha.Name = "textBoxSenha";
            this.textBoxSenha.Size = new System.Drawing.Size(157, 20);
            this.textBoxSenha.TabIndex = 3;
            this.textBoxSenha.UseSystemPasswordChar = true;
            // 
            // treeViewLegendas
            // 
            this.treeViewLegendas.Location = new System.Drawing.Point(175, 10);
            this.treeViewLegendas.Name = "treeViewLegendas";
            this.treeViewLegendas.Size = new System.Drawing.Size(468, 199);
            this.treeViewLegendas.TabIndex = 4;
            this.treeViewLegendas.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewLegendas_NodeMouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pastas Utilizadas";
            // 
            // listBoxDiretorios
            // 
            this.listBoxDiretorios.ContextMenuStrip = this.contextMenuStripDiretorios;
            this.listBoxDiretorios.FormattingEnabled = true;
            this.listBoxDiretorios.Location = new System.Drawing.Point(12, 103);
            this.listBoxDiretorios.Name = "listBoxDiretorios";
            this.listBoxDiretorios.Size = new System.Drawing.Size(157, 108);
            this.listBoxDiretorios.TabIndex = 6;
            // 
            // contextMenuStripDiretorios
            // 
            this.contextMenuStripDiretorios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adicionarToolStripMenuItem,
            this.deletarToolStripMenuItem});
            this.contextMenuStripDiretorios.Name = "contextMenuStripDiretorios";
            this.contextMenuStripDiretorios.Size = new System.Drawing.Size(126, 48);
            // 
            // adicionarToolStripMenuItem
            // 
            this.adicionarToolStripMenuItem.Name = "adicionarToolStripMenuItem";
            this.adicionarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.adicionarToolStripMenuItem.Text = "Adicionar";
            this.adicionarToolStripMenuItem.Click += new System.EventHandler(this.adicionarToolStripMenuItem_Click);
            // 
            // deletarToolStripMenuItem
            // 
            this.deletarToolStripMenuItem.Name = "deletarToolStripMenuItem";
            this.deletarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deletarToolStripMenuItem.Text = "Deletar";
            this.deletarToolStripMenuItem.Click += new System.EventHandler(this.deletarToolStripMenuItem_Click);
            // 
            // buttonBaixarLegenda
            // 
            this.buttonBaixarLegenda.Location = new System.Drawing.Point(12, 247);
            this.buttonBaixarLegenda.Name = "buttonBaixarLegenda";
            this.buttonBaixarLegenda.Size = new System.Drawing.Size(157, 24);
            this.buttonBaixarLegenda.TabIndex = 7;
            this.buttonBaixarLegenda.Text = "Baixar Legendas";
            this.buttonBaixarLegenda.UseVisualStyleBackColor = true;
            this.buttonBaixarLegenda.Click += new System.EventHandler(this.buttonBaixarLegenda_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(175, 215);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(468, 56);
            this.textBoxLog.TabIndex = 8;
            // 
            // adicionarPastaButton
            // 
            this.adicionarPastaButton.Location = new System.Drawing.Point(12, 218);
            this.adicionarPastaButton.Name = "adicionarPastaButton";
            this.adicionarPastaButton.Size = new System.Drawing.Size(157, 23);
            this.adicionarPastaButton.TabIndex = 9;
            this.adicionarPastaButton.Text = "Adicionar Pasta";
            this.adicionarPastaButton.UseVisualStyleBackColor = true;
            this.adicionarPastaButton.Click += new System.EventHandler(this.adicionarPastaButton_Click);
            // 
            // SubDownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 283);
            this.Controls.Add(this.adicionarPastaButton);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.buttonBaixarLegenda);
            this.Controls.Add(this.listBoxDiretorios);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeViewLegendas);
            this.Controls.Add(this.textBoxSenha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxUsuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SubDownForm";
            this.Text = "SubDown - LegendasTV";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SubDownForm_FormClosed);
            this.Load += new System.EventHandler(this.SubDownForm_Load);
            this.contextMenuStripDiretorios.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSenha;
        private System.Windows.Forms.TreeView treeViewLegendas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxDiretorios;
        private System.Windows.Forms.Button buttonBaixarLegenda;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDiretorios;
        private System.Windows.Forms.ToolStripMenuItem adicionarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletarToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button adicionarPastaButton;
    }
}


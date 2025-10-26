namespace Gerador;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.rbTabelas = new System.Windows.Forms.RadioButton();
            this.rbGrupo = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Front = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Back = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txtConnectionString);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtDestino);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1078, 150);
            this.panel1.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Image = global::Gerador.Properties.Resources.Refresh_icon;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(915, 97);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(142, 38);
            this.button2.TabIndex = 23;
            this.button2.Text = "Carregar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(24, 105);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(880, 27);
            this.txtConnectionString.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Connection String (Suporta SQL Server e MySQL)";
            // 
            // button1
            // 
            this.button1.Image = global::Gerador.Properties.Resources.search_icon;
            this.button1.Location = new System.Drawing.Point(1015, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 39);
            this.button1.TabIndex = 14;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(24, 53);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.ReadOnly = true;
            this.txtDestino.Size = new System.Drawing.Size(985, 27);
            this.txtDestino.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Destino";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.rbTabelas);
            this.panel2.Controls.Add(this.rbGrupo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 984);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1078, 117);
            this.panel2.TabIndex = 13;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(272, 74);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(785, 29);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // button3
            // 
            this.button3.Image = global::Gerador.Properties.Resources.Generate_tables_icon;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(22, 68);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(242, 38);
            this.button3.TabIndex = 2;
            this.button3.Text = "Gerar Arquivos";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // rbTabelas
            // 
            this.rbTabelas.AutoSize = true;
            this.rbTabelas.Location = new System.Drawing.Point(190, 13);
            this.rbTabelas.Name = "rbTabelas";
            this.rbTabelas.Size = new System.Drawing.Size(146, 24);
            this.rbTabelas.TabIndex = 1;
            this.rbTabelas.Text = "Gerar por Tabelas";
            this.rbTabelas.UseVisualStyleBackColor = true;
            // 
            // rbGrupo
            // 
            this.rbGrupo.AutoSize = true;
            this.rbGrupo.Checked = true;
            this.rbGrupo.Location = new System.Drawing.Point(22, 13);
            this.rbGrupo.Name = "rbGrupo";
            this.rbGrupo.Size = new System.Drawing.Size(144, 24);
            this.rbGrupo.TabIndex = 0;
            this.rbGrupo.TabStop = true;
            this.rbGrupo.Text = "Gerar por Grupos";
            this.rbGrupo.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.TableName,
            this.Front,
            this.Back});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 150);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(1078, 834);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Selected
            // 
            this.Selected.DataPropertyName = "Selected";
            this.Selected.HeaderText = "Ok";
            this.Selected.MinimumWidth = 6;
            this.Selected.Name = "Selected";
            this.Selected.Width = 80;
            // 
            // TableName
            // 
            this.TableName.DataPropertyName = "TableName";
            this.TableName.HeaderText = "TableName";
            this.TableName.MinimumWidth = 6;
            this.TableName.Name = "TableName";
            this.TableName.ReadOnly = true;
            this.TableName.Width = 600;
            // 
            // Front
            // 
            this.Front.DataPropertyName = "GenerateFront";
            this.Front.HeaderText = "Front";
            this.Front.MinimumWidth = 6;
            this.Front.Name = "Front";
            this.Front.Width = 80;
            // 
            // Back
            // 
            this.Back.DataPropertyName = "GenerateBack";
            this.Back.HeaderText = "Back";
            this.Back.MinimumWidth = 6;
            this.Back.Name = "Back";
            this.Back.Width = 80;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 1101);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private Panel panel1;
    private Button button2;
    private TextBox txtConnectionString;
    private Label label2;
    private Button button1;
    private TextBox txtDestino;
    private Label label1;
    private Panel panel2;
    private Button button3;
    private RadioButton rbTabelas;
    private RadioButton rbGrupo;
    private DataGridView dataGridView1;
    private ProgressBar progressBar1;
    private DataGridViewCheckBoxColumn Selected;
    private DataGridViewTextBoxColumn TableName;
    private DataGridViewCheckBoxColumn Front;
    private DataGridViewCheckBoxColumn Back;
}

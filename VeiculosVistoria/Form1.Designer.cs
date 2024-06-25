namespace VeiculosVistoria
{
    partial class FormBusca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBusca));
            this.lblAnoFab = new System.Windows.Forms.Label();
            this.cmbAnoFab = new System.Windows.Forms.ComboBox();
            this.txbCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.rdbPlaca = new System.Windows.Forms.RadioButton();
            this.rdbMotor = new System.Windows.Forms.RadioButton();
            this.rdbChassi = new System.Windows.Forms.RadioButton();
            this.gridVeiculos = new System.Windows.Forms.DataGridView();
            this.btnProcurar = new System.Windows.Forms.Button();
            this.groupBoxCadastro = new System.Windows.Forms.GroupBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.txbObservacoes = new System.Windows.Forms.TextBox();
            this.lblObservacoes = new System.Windows.Forms.Label();
            this.cmbAnoModelo = new System.Windows.Forms.ComboBox();
            this.txbChassi = new System.Windows.Forms.TextBox();
            this.lblChassi = new System.Windows.Forms.Label();
            this.lblAnoModelo = new System.Windows.Forms.Label();
            this.txbMotor = new System.Windows.Forms.TextBox();
            this.lblMotor = new System.Windows.Forms.Label();
            this.txbDescricao = new System.Windows.Forms.TextBox();
            this.cmbAnoFabCadastro = new System.Windows.Forms.ComboBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.lblAnoFabCadastro = new System.Windows.Forms.Label();
            this.txbLinha = new System.Windows.Forms.TextBox();
            this.lblLinha = new System.Windows.Forms.Label();
            this.txbPlaca = new System.Windows.Forms.TextBox();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.txbPotencia = new System.Windows.Forms.TextBox();
            this.lblPotencia = new System.Windows.Forms.Label();
            this.txbMarca = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.progressBarExcel = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.gridVeiculos)).BeginInit();
            this.groupBoxCadastro.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAnoFab
            // 
            this.lblAnoFab.AutoSize = true;
            this.lblAnoFab.Location = new System.Drawing.Point(12, 22);
            this.lblAnoFab.Name = "lblAnoFab";
            this.lblAnoFab.Size = new System.Drawing.Size(105, 15);
            this.lblAnoFab.TabIndex = 0;
            this.lblAnoFab.Text = "Ano de Fabricação";
            // 
            // cmbAnoFab
            // 
            this.cmbAnoFab.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbAnoFab.FormattingEnabled = true;
            this.cmbAnoFab.IntegralHeight = false;
            this.cmbAnoFab.ItemHeight = 15;
            this.cmbAnoFab.Location = new System.Drawing.Point(123, 19);
            this.cmbAnoFab.MaxLength = 4;
            this.cmbAnoFab.Name = "cmbAnoFab";
            this.cmbAnoFab.Size = new System.Drawing.Size(66, 23);
            this.cmbAnoFab.TabIndex = 1;
            this.cmbAnoFab.SelectedIndexChanged += new System.EventHandler(this.cmbAnoFab_SelectedIndexChanged);
            this.cmbAnoFab.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbAnoFab_KeyPress);
            // 
            // txbCodigo
            // 
            this.txbCodigo.Location = new System.Drawing.Point(256, 19);
            this.txbCodigo.MaxLength = 100;
            this.txbCodigo.Name = "txbCodigo";
            this.txbCodigo.Size = new System.Drawing.Size(182, 23);
            this.txbCodigo.TabIndex = 2;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(204, 22);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(46, 15);
            this.lblCodigo.TabIndex = 3;
            this.lblCodigo.Text = "Código";
            // 
            // rdbPlaca
            // 
            this.rdbPlaca.AutoSize = true;
            this.rdbPlaca.Location = new System.Drawing.Point(256, 48);
            this.rdbPlaca.Name = "rdbPlaca";
            this.rdbPlaca.Size = new System.Drawing.Size(53, 19);
            this.rdbPlaca.TabIndex = 3;
            this.rdbPlaca.TabStop = true;
            this.rdbPlaca.Text = "Placa";
            this.rdbPlaca.UseVisualStyleBackColor = true;
            // 
            // rdbMotor
            // 
            this.rdbMotor.AutoSize = true;
            this.rdbMotor.Location = new System.Drawing.Point(380, 48);
            this.rdbMotor.Name = "rdbMotor";
            this.rdbMotor.Size = new System.Drawing.Size(58, 19);
            this.rdbMotor.TabIndex = 5;
            this.rdbMotor.TabStop = true;
            this.rdbMotor.Text = "Motor";
            this.rdbMotor.UseVisualStyleBackColor = true;
            // 
            // rdbChassi
            // 
            this.rdbChassi.AutoSize = true;
            this.rdbChassi.Location = new System.Drawing.Point(315, 48);
            this.rdbChassi.Name = "rdbChassi";
            this.rdbChassi.Size = new System.Drawing.Size(59, 19);
            this.rdbChassi.TabIndex = 4;
            this.rdbChassi.TabStop = true;
            this.rdbChassi.Text = "Chassi";
            this.rdbChassi.UseVisualStyleBackColor = true;
            // 
            // gridVeiculos
            // 
            this.gridVeiculos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridVeiculos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVeiculos.Location = new System.Drawing.Point(12, 84);
            this.gridVeiculos.Name = "gridVeiculos";
            this.gridVeiculos.RowHeadersVisible = false;
            this.gridVeiculos.RowTemplate.Height = 25;
            this.gridVeiculos.Size = new System.Drawing.Size(1224, 542);
            this.gridVeiculos.TabIndex = 7;
            this.gridVeiculos.TabStop = false;
            this.gridVeiculos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridVeiculos_CellDoubleClick);
            // 
            // btnProcurar
            // 
            this.btnProcurar.Location = new System.Drawing.Point(453, 19);
            this.btnProcurar.Name = "btnProcurar";
            this.btnProcurar.Size = new System.Drawing.Size(105, 48);
            this.btnProcurar.TabIndex = 6;
            this.btnProcurar.Text = "Procurar";
            this.btnProcurar.UseVisualStyleBackColor = true;
            this.btnProcurar.Click += new System.EventHandler(this.btnProcurar_Click);
            // 
            // groupBoxCadastro
            // 
            this.groupBoxCadastro.Controls.Add(this.btnLimpar);
            this.groupBoxCadastro.Controls.Add(this.btnSalvar);
            this.groupBoxCadastro.Controls.Add(this.txbObservacoes);
            this.groupBoxCadastro.Controls.Add(this.lblObservacoes);
            this.groupBoxCadastro.Controls.Add(this.cmbAnoModelo);
            this.groupBoxCadastro.Controls.Add(this.txbChassi);
            this.groupBoxCadastro.Controls.Add(this.lblChassi);
            this.groupBoxCadastro.Controls.Add(this.lblAnoModelo);
            this.groupBoxCadastro.Controls.Add(this.txbMotor);
            this.groupBoxCadastro.Controls.Add(this.lblMotor);
            this.groupBoxCadastro.Controls.Add(this.txbDescricao);
            this.groupBoxCadastro.Controls.Add(this.cmbAnoFabCadastro);
            this.groupBoxCadastro.Controls.Add(this.lblDescricao);
            this.groupBoxCadastro.Controls.Add(this.lblAnoFabCadastro);
            this.groupBoxCadastro.Controls.Add(this.txbLinha);
            this.groupBoxCadastro.Controls.Add(this.lblLinha);
            this.groupBoxCadastro.Controls.Add(this.txbPlaca);
            this.groupBoxCadastro.Controls.Add(this.lblPlaca);
            this.groupBoxCadastro.Controls.Add(this.txbPotencia);
            this.groupBoxCadastro.Controls.Add(this.lblPotencia);
            this.groupBoxCadastro.Controls.Add(this.txbMarca);
            this.groupBoxCadastro.Controls.Add(this.lblMarca);
            this.groupBoxCadastro.Location = new System.Drawing.Point(12, 658);
            this.groupBoxCadastro.Name = "groupBoxCadastro";
            this.groupBoxCadastro.Size = new System.Drawing.Size(1224, 135);
            this.groupBoxCadastro.TabIndex = 9;
            this.groupBoxCadastro.TabStop = false;
            this.groupBoxCadastro.Text = "Cadastro / Alteração";
            this.groupBoxCadastro.Enter += new System.EventHandler(this.groupBoxCadastro_Enter);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(1109, 81);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(105, 48);
            this.btnLimpar.TabIndex = 19;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1109, 21);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(105, 48);
            this.btnSalvar.TabIndex = 18;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txbObservacoes
            // 
            this.txbObservacoes.Location = new System.Drawing.Point(796, 42);
            this.txbObservacoes.Multiline = true;
            this.txbObservacoes.Name = "txbObservacoes";
            this.txbObservacoes.Size = new System.Drawing.Size(299, 76);
            this.txbObservacoes.TabIndex = 17;
            // 
            // lblObservacoes
            // 
            this.lblObservacoes.AutoSize = true;
            this.lblObservacoes.Location = new System.Drawing.Point(796, 24);
            this.lblObservacoes.Name = "lblObservacoes";
            this.lblObservacoes.Size = new System.Drawing.Size(74, 15);
            this.lblObservacoes.TabIndex = 14;
            this.lblObservacoes.Text = "Observações";
            // 
            // cmbAnoModelo
            // 
            this.cmbAnoModelo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbAnoModelo.FormattingEnabled = true;
            this.cmbAnoModelo.IntegralHeight = false;
            this.cmbAnoModelo.ItemHeight = 15;
            this.cmbAnoModelo.Location = new System.Drawing.Point(288, 42);
            this.cmbAnoModelo.MaxLength = 4;
            this.cmbAnoModelo.Name = "cmbAnoModelo";
            this.cmbAnoModelo.Size = new System.Drawing.Size(89, 23);
            this.cmbAnoModelo.TabIndex = 10;
            this.cmbAnoModelo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbAnoModelo_KeyPress);
            // 
            // txbChassi
            // 
            this.txbChassi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txbChassi.Location = new System.Drawing.Point(14, 94);
            this.txbChassi.MaxLength = 100;
            this.txbChassi.Name = "txbChassi";
            this.txbChassi.Size = new System.Drawing.Size(215, 23);
            this.txbChassi.TabIndex = 11;
            // 
            // lblChassi
            // 
            this.lblChassi.AutoSize = true;
            this.lblChassi.Location = new System.Drawing.Point(14, 76);
            this.lblChassi.Name = "lblChassi";
            this.lblChassi.Size = new System.Drawing.Size(41, 15);
            this.lblChassi.TabIndex = 11;
            this.lblChassi.Text = "Chassi";
            // 
            // lblAnoModelo
            // 
            this.lblAnoModelo.AutoSize = true;
            this.lblAnoModelo.Location = new System.Drawing.Point(288, 24);
            this.lblAnoModelo.Name = "lblAnoModelo";
            this.lblAnoModelo.Size = new System.Drawing.Size(73, 15);
            this.lblAnoModelo.TabIndex = 13;
            this.lblAnoModelo.Text = "Ano Modelo";
            // 
            // txbMotor
            // 
            this.txbMotor.Location = new System.Drawing.Point(295, 94);
            this.txbMotor.MaxLength = 100;
            this.txbMotor.Name = "txbMotor";
            this.txbMotor.Size = new System.Drawing.Size(215, 23);
            this.txbMotor.TabIndex = 12;
            // 
            // lblMotor
            // 
            this.lblMotor.AutoSize = true;
            this.lblMotor.Location = new System.Drawing.Point(295, 76);
            this.lblMotor.Name = "lblMotor";
            this.lblMotor.Size = new System.Drawing.Size(40, 15);
            this.lblMotor.TabIndex = 11;
            this.lblMotor.Text = "Motor";
            this.lblMotor.Click += new System.EventHandler(this.lblMotor_Click);
            // 
            // txbDescricao
            // 
            this.txbDescricao.Location = new System.Drawing.Point(576, 95);
            this.txbDescricao.MaxLength = 100;
            this.txbDescricao.Name = "txbDescricao";
            this.txbDescricao.Size = new System.Drawing.Size(202, 23);
            this.txbDescricao.TabIndex = 16;
            // 
            // cmbAnoFabCadastro
            // 
            this.cmbAnoFabCadastro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbAnoFabCadastro.FormattingEnabled = true;
            this.cmbAnoFabCadastro.IntegralHeight = false;
            this.cmbAnoFabCadastro.ItemHeight = 15;
            this.cmbAnoFabCadastro.Location = new System.Drawing.Point(156, 42);
            this.cmbAnoFabCadastro.MaxLength = 4;
            this.cmbAnoFabCadastro.Name = "cmbAnoFabCadastro";
            this.cmbAnoFabCadastro.Size = new System.Drawing.Size(89, 23);
            this.cmbAnoFabCadastro.TabIndex = 9;
            this.cmbAnoFabCadastro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbAnoFabCadastro_KeyPress);
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(576, 76);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(58, 15);
            this.lblDescricao.TabIndex = 1;
            this.lblDescricao.Text = "Descrição";
            // 
            // lblAnoFabCadastro
            // 
            this.lblAnoFabCadastro.AutoSize = true;
            this.lblAnoFabCadastro.Location = new System.Drawing.Point(156, 24);
            this.lblAnoFabCadastro.Name = "lblAnoFabCadastro";
            this.lblAnoFabCadastro.Size = new System.Drawing.Size(89, 15);
            this.lblAnoFabCadastro.TabIndex = 13;
            this.lblAnoFabCadastro.Text = "Ano Fabricação";
            // 
            // txbLinha
            // 
            this.txbLinha.Location = new System.Drawing.Point(420, 42);
            this.txbLinha.MaxLength = 100;
            this.txbLinha.Name = "txbLinha";
            this.txbLinha.Size = new System.Drawing.Size(104, 23);
            this.txbLinha.TabIndex = 13;
            // 
            // lblLinha
            // 
            this.lblLinha.AutoSize = true;
            this.lblLinha.Location = new System.Drawing.Point(420, 24);
            this.lblLinha.Name = "lblLinha";
            this.lblLinha.Size = new System.Drawing.Size(36, 15);
            this.lblLinha.TabIndex = 1;
            this.lblLinha.Text = "Linha";
            // 
            // txbPlaca
            // 
            this.txbPlaca.Location = new System.Drawing.Point(14, 42);
            this.txbPlaca.MaxLength = 7;
            this.txbPlaca.Name = "txbPlaca";
            this.txbPlaca.Size = new System.Drawing.Size(99, 23);
            this.txbPlaca.TabIndex = 8;
            this.txbPlaca.TextChanged += new System.EventHandler(this.txbPlaca_TextChanged);
            this.txbPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbPlaca_KeyPress);
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Location = new System.Drawing.Point(14, 24);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(35, 15);
            this.lblPlaca.TabIndex = 1;
            this.lblPlaca.Text = "Placa";
            // 
            // txbPotencia
            // 
            this.txbPotencia.Location = new System.Drawing.Point(714, 42);
            this.txbPotencia.MaxLength = 4;
            this.txbPotencia.Name = "txbPotencia";
            this.txbPotencia.Size = new System.Drawing.Size(65, 23);
            this.txbPotencia.TabIndex = 15;
            this.txbPotencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbPotencia_KeyPress);
            // 
            // lblPotencia
            // 
            this.lblPotencia.AutoSize = true;
            this.lblPotencia.Location = new System.Drawing.Point(714, 24);
            this.lblPotencia.Name = "lblPotencia";
            this.lblPotencia.Size = new System.Drawing.Size(53, 15);
            this.lblPotencia.TabIndex = 1;
            this.lblPotencia.Text = "Potência";
            // 
            // txbMarca
            // 
            this.txbMarca.Location = new System.Drawing.Point(567, 42);
            this.txbMarca.MaxLength = 100;
            this.txbMarca.Name = "txbMarca";
            this.txbMarca.Size = new System.Drawing.Size(104, 23);
            this.txbMarca.TabIndex = 14;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(567, 24);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(40, 15);
            this.lblMarca.TabIndex = 1;
            this.lblMarca.Text = "Marca";
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(1110, 12);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(126, 63);
            this.btnExportar.TabIndex = 19;
            this.btnExportar.Text = "Exportar dados";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // progressBarExcel
            // 
            this.progressBarExcel.Location = new System.Drawing.Point(991, 632);
            this.progressBarExcel.Name = "progressBarExcel";
            this.progressBarExcel.Size = new System.Drawing.Size(245, 23);
            this.progressBarExcel.Step = 1;
            this.progressBarExcel.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarExcel.TabIndex = 20;
            this.progressBarExcel.UseWaitCursor = true;
            // 
            // FormBusca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1248, 805);
            this.Controls.Add(this.progressBarExcel);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.groupBoxCadastro);
            this.Controls.Add(this.btnProcurar);
            this.Controls.Add(this.gridVeiculos);
            this.Controls.Add(this.rdbChassi);
            this.Controls.Add(this.rdbMotor);
            this.Controls.Add(this.rdbPlaca);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txbCodigo);
            this.Controls.Add(this.cmbAnoFab);
            this.Controls.Add(this.lblAnoFab);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBusca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Veículos";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridVeiculos)).EndInit();
            this.groupBoxCadastro.ResumeLayout(false);
            this.groupBoxCadastro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblAnoFab;
        private ComboBox cmbAnoFab;
        private TextBox txbCodigo;
        private Label lblCodigo;
        private RadioButton rdbPlaca;
        private RadioButton rdbMotor;
        private RadioButton rdbChassi;
        private DataGridView gridVeiculos;
        private Button btnProcurar;
        private GroupBox groupBoxCadastro;
        private TextBox txbMotor;
        private Label lblMotor;
        private TextBox txbChassi;
        private Label lblChassi;
        private TextBox txbPlaca;
        private Label lblPlaca;
        private ComboBox cmbAnoModelo;
        private Label lblAnoModelo;
        private ComboBox cmbAnoFabCadastro;
        private Label lblAnoFabCadastro;
        private TextBox txbDescricao;
        private Label lblDescricao;
        private TextBox txbLinha;
        private Label lblLinha;
        private TextBox txbMarca;
        private Label lblMarca;
        private TextBox txbObservacoes;
        private Label lblObservacoes;
        private Button btnSalvar;
        private TextBox txbPotencia;
        private Label lblPotencia;
        private Button btnLimpar;
        private Button btnExportar;
        private ProgressBar progressBarExcel;
    }
}
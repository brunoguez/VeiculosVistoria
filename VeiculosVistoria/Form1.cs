using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using VeiculosVistoria.DataAccess;
using VeiculosVistoria.Models;

namespace VeiculosVistoria
{
    public partial class FormBusca : Form
    {
        int tamanhoColunaChassiMotor = 145;
        int tamanhoColunaObs = 200;
        int tamanhoColunaMarca = 150;
        int tamanhoColunaAno = 80;

        public FormBusca()
        {
            InitializeComponent();
        }

        DAO_Veiculos dao = new();
        List<String> ano = new();
        List<String> anosFab = new();
        List<String> anosMod = new();

        private void Form1_Load(object sender, EventArgs e)
        {

            gridVeiculos.Rows.Clear();
            gridVeiculos.ColumnCount = 10;
            gridVeiculos.ColumnHeadersVisible = true;
            gridVeiculos.Width = (423) + (2 * tamanhoColunaChassiMotor) + tamanhoColunaObs + tamanhoColunaMarca + (2 * tamanhoColunaAno);
            gridVeiculos.Columns[0].Name = "Placa";
            gridVeiculos.Columns[1].Name = "Chassi";
            gridVeiculos.Columns[1].Width = tamanhoColunaChassiMotor;
            gridVeiculos.Columns[2].Name = "Motor";
            gridVeiculos.Columns[2].Width = tamanhoColunaChassiMotor;
            gridVeiculos.Columns[3].Name = "Ano Fabricação";
            gridVeiculos.Columns[3].Width = tamanhoColunaAno;
            gridVeiculos.Columns[4].Name = "Ano Modelo";
            gridVeiculos.Columns[4].Width = tamanhoColunaAno;
            gridVeiculos.Columns[5].Name = "Marca";
            gridVeiculos.Columns[5].Width = tamanhoColunaMarca;
            gridVeiculos.Columns[6].Name = "Linha";
            gridVeiculos.Columns[7].Name = "Descrição";
            gridVeiculos.Columns[7].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridVeiculos.Columns[8].Name = "Potência";
            gridVeiculos.Columns[9].Name = "Observações";
            gridVeiculos.Columns[9].Width = tamanhoColunaObs;

            anosFab.Add("");
            anosMod.Add("");
            for (int i = DateTime.Now.Year + 1; i > 1956; i--)
            {
                anosMod.Add(Convert.ToString(i));
                if (i! <= DateTime.Now.Year)
                {
                    anosFab.Add(Convert.ToString(i));
                }
            }
            cmbAnoFabCadastro.DataSource = anosFab;
            cmbAnoModelo.DataSource = anosMod;
            cmbAnoFab.DataSource = dao.GetAnoFabricacao();
            ano = dao.GetAnoFabricacao();

            rdbPlaca.Checked = true;
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            var apenasDigitos = new Regex(@"[^\d+$^\w+$%]");
            txbCodigo.Text = apenasDigitos.Replace(txbCodigo.Text, "");

            int teste = 0;
            foreach (var item in ano)
            {
                if (cmbAnoFab.Text == item)
                {
                    teste = 1;
                }
            }
            if (teste == 0)
            {
                var erroListaAno = MessageBox.Show("Insira um Ano que está na lista");
                goto aqui;
            }
            if (txbCodigo.Text == "")
            {
                var erroCodigo = MessageBox.Show("Insira um filtro de código");
                txbCodigo.Focus();
                goto aqui;
            }

            gridVeiculos.Columns.Clear();

            if (rdbPlaca.Checked == true)
                gridVeiculos.DataSource = converteVeiculos(dao.GetPlaca(txbCodigo.Text, cmbAnoFab.Text));

            if (rdbChassi.Checked == true)
                gridVeiculos.DataSource = converteVeiculos(dao.GetChassi(txbCodigo.Text, cmbAnoFab.Text));

            if (rdbMotor.Checked == true)
                gridVeiculos.DataSource = converteVeiculos(dao.GetMotor(txbCodigo.Text, cmbAnoFab.Text));

            gridVeiculos.Columns["Chassi"].Width = tamanhoColunaChassiMotor;
            gridVeiculos.Columns["Motor"].Width = tamanhoColunaChassiMotor;
            gridVeiculos.Columns["Ano_Fabricacao"].HeaderText = "Ano Fabricação";
            gridVeiculos.Columns["Ano_Fabricacao"].Width = tamanhoColunaAno;
            gridVeiculos.Columns["Ano_Modelo"].HeaderText = "Ano Modelo";
            gridVeiculos.Columns["Ano_Modelo"].Width = tamanhoColunaAno;
            gridVeiculos.Columns["Marca"].Width = tamanhoColunaMarca;
            gridVeiculos.Columns["Observacoes"].HeaderText = "Observações";
            gridVeiculos.Columns["Observacoes"].Width = tamanhoColunaObs;
            gridVeiculos.Columns[7].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridVeiculos.AllowUserToAddRows = false;
            gridVeiculos.AllowUserToDeleteRows = false;
            gridVeiculos.AllowUserToOrderColumns = true;
            gridVeiculos.ReadOnly = true;
            gridVeiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVeiculos.MultiSelect = false;
            gridVeiculos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridVeiculos.AllowUserToResizeColumns = true;
            gridVeiculos.AllowUserToOrderColumns = false;
            gridVeiculos.AllowUserToResizeRows = false;
            gridVeiculos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

        aqui:;
        }

        public List<ConverteVeiculos> converteVeiculos(List<Veiculo> chassi)
        {
            List<ConverteVeiculos> x = new();
            foreach (var item in chassi)
            {
                var a = new ConverteVeiculos()
                {
                    Placa = item.Placa,
                    Chassi = item.Chassi,
                    Motor = item.Motor,
                    Ano_Fabricacao = item.Ano_Fabricacao,
                    Ano_Modelo = item.Ano_Modelo,
                    Marca = item.Marca,
                    Linha = item.Linha,
                    Descricao = item.Descricao,
                    Observacoes = item.Observacoes,
                };
                if (item.Potencia != 0 && item.Potencia is not null)
                {
                    a.Potencia = item.Potencia.Value.ToString("F").Replace(",", ".");
                }
                else
                {
                    a.Potencia = "";
                }

                x.Add(a);
            }

            return x;
        }

        public class ConverteVeiculos
        {
            public String Placa { get; set; }
            public String Chassi { get; set; }
            public String Motor { get; set; }
            public Int32 Ano_Fabricacao { get; set; }
            public Int32 Ano_Modelo { get; set; }
            public String Marca { get; set; }
            public String Linha { get; set; }
            public String Descricao { get; set; }
            public String Potencia { get; set; }
            public String Observacoes { get; set; }
        }

        private void lblMotor_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxCadastro_Enter(object sender, EventArgs e)
        {

        }

        public static bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa)) { return false; }

            if (placa.Length > 8) { return false; }

            placa = placa.Replace("-", "").Trim();

            /*
             *  Verifica se o caractere da posição 4 é uma letra, se sim, aplica a validação para o formato de placa do Mercosul,
             *  senão, aplica a validação do formato de placa padrão.
             */
            if (char.IsLetter(placa, 4))
            {
                /*
                 *  Verifica se a placa está no formato: três letras, um número, uma letra e dois números.
                 */
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return padraoMercosul.IsMatch(placa);
            }
            else
            {
                // Verifica se os 3 primeiros caracteres são letras e se os 4 últimos são números.
                var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padraoNormal.IsMatch(placa);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var apenasDigitos = new Regex(@"[^\d+$^\w+$]");
            txbChassi.Text = apenasDigitos.Replace(txbChassi.Text, "");
            txbMotor.Text = apenasDigitos.Replace(txbMotor.Text, "");

            int teste = 0;
            string mensagem = "";

            if (txbChassi.Text == "")
            {
                txbChassi.BackColor = Color.FromArgb(255, 192, 192);
                txbChassi.Focus();
                mensagem += "\n > Chassi";
                teste = 1;
            }
            if (cmbAnoFabCadastro.Text == "")
            {
                cmbAnoFabCadastro.BackColor = Color.FromArgb(255, 192, 192);
                cmbAnoFabCadastro.Focus();
                mensagem += "\n > Ano de fabricação";
                teste = 1;
            }
            if (cmbAnoModelo.Text == "")
            {
                cmbAnoModelo.BackColor = Color.FromArgb(255, 192, 192);
                cmbAnoModelo.Focus();
                mensagem += "\n > Ano de modelo";
                teste = 1;
            }

            if (txbPlaca.Text.Length < 7)
            {
                mensagem += "\n > Placa faltando caracteres";
                teste = 1;
            }
            else if (ValidarPlaca(txbPlaca.Text) == false)
            {
                txbPlaca.BackColor = Color.FromArgb(255, 192, 192);
                txbPlaca.Focus();
                mensagem += "\n > Placa no modelo errado";
                teste = 1;
            }



            if (teste == 1)
            {
                var erroCodigo = MessageBox.Show("Faltando: " + mensagem);
                goto faltando;
            }

            Veiculo chassi = new()
            {
                Chassi = txbChassi.Text.ToUpper(),
                Placa = txbPlaca.Text.ToUpper(),
                Motor = txbMotor.Text.ToUpper(),
                Ano_Fabricacao = Convert.ToInt32(cmbAnoFabCadastro.Text),
                Ano_Modelo = Convert.ToInt32(cmbAnoModelo.Text),
                Marca = txbMarca.Text.ToUpper(),
                Linha = txbLinha.Text.ToUpper(),
                Descricao = txbDescricao.Text.ToUpper(),
                Observacoes = txbObservacoes.Text.ToUpper()
            };

            if (txbPotencia.Text == "")
            {
                chassi.Potencia = 0;
            }
            else
            {
                if (txbPotencia.Text.Contains(".") == true)
                {
                    chassi.Potencia = Convert.ToDouble(txbPotencia.Text.Replace(".", ","));
                }
                else
                {
                    chassi.Potencia = Convert.ToDouble(txbPotencia.Text);
                }
                //chassi.Potencia = Convert.ToDouble(txbPotencia.Text.Replace(".",","));
            }

            Veiculo testar = new() { };
            testar = dao.GetChassi(txbChassi.Text.ToUpper());

            if (testar.Chassi == null)
            {
                dao.Insert(chassi);
            }
            else
            {
                var verifica = MessageBox.Show("O Chassi: " + testar.Chassi + " já está cadastrado, deseja alterar?\n\nPlaca: " + testar.Placa + "\nMotor: " + testar.Motor + "\nAno_Fabricacao: " + testar.Ano_Fabricacao + "\nAno_Modelo: " + testar.Ano_Modelo + "\nMarca: " + testar.Marca + "\nLinha: " + testar.Linha + "\nDescricao: " + testar.Descricao + "\nPotencia: " + testar.Potencia + "\nObservacoes: " + testar.Observacoes, "Chassi cadastrado", MessageBoxButtons.YesNo);
                if (verifica == DialogResult.Yes)
                {
                    dao.Update(chassi);
                }
                else
                {
                    goto faltando;
                }
            }


        limpar:
            limpar();

        faltando:;
        }

        private void cmbAnoFab_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        private void cmbAnoFab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbAnoFabCadastro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        private void cmbAnoModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        private void txbPlaca_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && !char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void txbPotencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txbPotencia.Text.Contains("."))
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                    e.Handled = true;
            }
            else
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
                    e.Handled = true;
            }
        }

        public void limpar()
        {
            txbChassi.Clear();
            txbPlaca.Clear();
            txbMotor.Clear();
            cmbAnoFabCadastro.Text = "";
            cmbAnoModelo.Text = "";
            txbMarca.Clear();
            txbLinha.Clear();
            txbDescricao.Clear();
            txbObservacoes.Clear();
            txbPotencia.Clear();

            cmbAnoFab.DataSource = null;
            cmbAnoFab.DataSource = dao.GetAnoFabricacao();
            ano.Clear();
            ano = dao.GetAnoFabricacao();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void gridVeiculos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            var chassis = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Chassi"].Value;
            if (chassis != null)
            {
                txbChassi.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Chassi"].Value.ToString();
                txbPlaca.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Placa"].Value.ToString();
                txbMotor.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Motor"].Value.ToString();
                cmbAnoFabCadastro.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Ano_Fabricacao"].Value.ToString();
                cmbAnoModelo.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Ano_Modelo"].Value.ToString();
                txbMarca.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Marca"].Value.ToString();
                txbLinha.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Linha"].Value.ToString();
                txbDescricao.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Descricao"].Value.ToString();
                txbObservacoes.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Observacoes"].Value.ToString();
                txbPotencia.Text = gridVeiculos.Rows[gridVeiculos.CurrentRow.Index].Cells["Potencia"].Value.ToString();
            }




        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //Aqui criamos um objeto que representa a aplicação, no qual iremos interagir
            //Inicialmente apenas este objeto é suficiente para nossa exportação básica

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            //Adicionamos  um novo WorkBook em nosso documento
            //Type.Missing representa a ausencia de um informação.
            //Usamos para não nenhum template para nosso documento

            excelApp.Workbooks.Add(Type.Missing);

            //Podemos usar o visible para exibir a planilha
            //Isto é opicional, podemos simplesmente salvar a planilha sem exibi-la

            excelApp.Visible = false;

            //Acessando a propriedade Cells, temos acesso á uma determinada célula da planilha
            //Lembrando que, como estamos trabalhando em cima de excelApp, ou seja, um objeto do
            //tipo ApplicationClass, qualquer manipulação que fizermos, será realizada na planilha
            //que estiver ativa no nosso documento. Por Default é a plan1
            //Outro ponto importante que vale ressaltar é que os índices de Cells[Row,Col] começam de 1

            excelApp.Cells[1, 1] = "Placa";
            excelApp.Cells[1, 2] = "Chassi";
            excelApp.Cells[1, 3] = "Motor";
            excelApp.Cells[1, 4] = "Ano Fabricação";
            excelApp.Cells[1, 5] = "Ano Modelo";
            excelApp.Cells[1, 6] = "Marca";
            excelApp.Cells[1, 7] = "Linha";
            excelApp.Cells[1, 8] = "Descricao";
            excelApp.Cells[1, 9] = "Potencia";
            excelApp.Cells[1, 10] = "Observações";

            int divisao = 0;
            progressBarExcel.Maximum = gridVeiculos.RowCount;

            for (int i = 0; i <= gridVeiculos.RowCount - 1; i++)
            {
                for (int j = 0; j <= gridVeiculos.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = gridVeiculos[j, i];
                    if (gridVeiculos.Rows[0].Cells["Chassi"].Value == null)
                        goto nada;
                    excelApp.Cells[i + 2, j + 1] = cell.Value;
                }
                divisao++;
                progressBarExcel.Value = divisao;
            }

            //Salvamos a planilha passando o caminho como parâmetro

            SaveFileDialog salvar = new SaveFileDialog(); // novo
            salvar.Title = "Exportar para Excel";
            salvar.Filter = "Arquivo do Excel *.xlsx | *.xlsx";
            salvar.FileName = "AnoFab_" + cmbAnoFab.Text + " Pesquisa_" + (rdbChassi.Checked == true ? "Chassi_" : (rdbMotor.Checked == true ? "Motor_" : "Placa_")) + (txbCodigo.Text == "%" ? "Todos" : txbCodigo.Text);

            if (salvar.ShowDialog() != DialogResult.Cancel)
            {
                excelApp.ActiveWorkbook.SaveCopyAs(salvar.FileName);
            }

        //Informamos que nossa planilha foi salva para que não sejamos
        //questionados ao fechar a mesma
        nada:
            excelApp.ActiveWorkbook.Saved = true;

            //fechamos a aplicação

            excelApp.Quit();
            progressBarExcel.Value = 0;
        }

        private void txbPotencia_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
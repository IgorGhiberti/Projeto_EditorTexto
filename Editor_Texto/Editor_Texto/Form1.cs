using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Editor_Texto
{
    public partial class editorDeTextoForm : Form
    {
        StreamReader leitura = null;
        public editorDeTextoForm()
        {
            InitializeComponent();
        }

        //Sempre que o usuário clicar em "Novo" irá aparecer esse método para que ele salve ou não o arquivo antes de criar outro.
        private void ChamaSalvarArquivo()
        {
            if (!string.IsNullOrEmpty(richTextBox.Text))
            {
                if ((MessageBox.Show("Deseja Salvar o arquivo ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {
                    Salvar_Arquivo();
                }
            }
        }
        //Faz o mesmo que o método acima, porém agora quando o usuário clica em "Sair".
        private void ChamaSalvarArquivoParaSair()
        {
            if (!string.IsNullOrEmpty(richTextBox.Text))
            {
                if ((MessageBox.Show("Deseja Salvar o Arquivo Antes de Sair?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {
                    Salvar_Arquivo();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
        //Limpa tudo o que foi digitado e volta com o cursos para o começo da caixa de texto. 
        private void Novo()
        {
            richTextBox.Clear();
            richTextBox.Focus();
        }
        //Método utilizado para salvar o arquivo na máquina do usuário.
        private void Salvar_Arquivo()
        {
            try
            {
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream arquivo = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter n_streamWriter = new StreamWriter(arquivo);
                    n_streamWriter.Flush();
                    n_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                    n_streamWriter.Write(this.richTextBox.Text);
                    n_streamWriter.Flush();
                    n_streamWriter.Close();  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar o arquivo :" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Método utilizado para abrir um arquivo existente na máquina do usuário dentro do programa.
        private void Abrir()
        {
            this.openFileDialog1.Title = "Selecionar arquivo";
            openFileDialog1.InitialDirectory = @"Área de Trabalho";
            openFileDialog1.Filter = "(* rtf)|*.rtf";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream arquivo = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader leitor = new StreamReader(arquivo);
                    leitor.BaseStream.Seek(0, SeekOrigin.Begin);
                    this.richTextBox.Text = "";
                    string linha = leitor.ReadLine();
                    while (linha != null)
                    {
                        this.richTextBox.Text += linha + "\n";
                        linha = leitor.ReadLine();
                    }
                    leitor.Close();
                }
                catch (Exception ex )
                {
                    MessageBox.Show("Erro ao ler o arquivo :" + ex.Message, "Erro ao ler", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        //Método utilizado para copiar o texto selecionado.
        private void Copiar()
        {
            if(richTextBox.SelectionLength > 0)
            {
                richTextBox.Copy();
            }
        }
        //Método utilizado para colar o texto copiado.
        private void Colar()
        {
            richTextBox.Paste();
        }
        //Método utilizado para aplicar os estilos de fonte, como negrito, itálico e sublinhado..
        private void AplicarEstilo(RichTextBox rtx, FontStyle ft)
        {
            Font fonteSelecao = rtx.SelectionFont;

            ft = fonteSelecao.Style ^ ft;
            rtx.SelectionFont = new Font(fonteSelecao,ft);
        }

        private void Negrito()
        {
            AplicarEstilo(richTextBox, FontStyle.Bold);
        }

        private void Italico()
        {
            AplicarEstilo(richTextBox, FontStyle.Italic);
        }

        private void Sublinhado()
        {
            AplicarEstilo(richTextBox, FontStyle.Underline);
        }

        private void AlterarAFonte()
        {
                DialogResult result = fontDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (richTextBox.SelectionFont != null)
                    {
                        richTextBox.SelectionFont = fontDialog1.Font;
                    }
                }
        }

        private void Excluir()
        {
            richTextBox.Clear();
        }

        private void SelecionarTudo()
        {
            richTextBox.SelectAll();
        }
        //Método utilizado para mostrar a hora, minuto, dia, mês e ano atuais no programa.
        private void MostrarDataHora()
        {
            DateTime n = DateTime.Now;
            richTextBox.Text = String.Format("{0}:{1}  {2}/{3}/{4}", n.Hour, n.Minute, n.Day, n.Month, n.Year);
        }

        //Métodos de alinhamento.
        private void AlinharCentro()
        {
            richTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void AlinharEsquerda()
        {
            richTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void AlinharDireita()
        {
            richTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }
        private void Salvar()
        {

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void negritoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Negrito();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChamaSalvarArquivo();
            Novo();
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar_Arquivo();
        }

        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copiar();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Colar();
        }

        private void itálicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Italico();
        }

        private void sublinhadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sublinhado();
        }

        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlterarAFonte();
        }

        private void centralizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharCentro();
        }

        private void esquerdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharEsquerda();
        }

        private void direitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharDireita();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChamaSalvarArquivoParaSair();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        private void selecionarTudoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelecionarTudo();
        }

        private void dataEHoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarDataHora();
        }

        private void desfazerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void refazerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

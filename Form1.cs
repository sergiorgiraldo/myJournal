using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Outlook.Application;

namespace MyJournal
{
    public partial class Form1 : Form
    {
        private DateTime _horaInicioPomodoro;
        private string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),@"Journal");
        private string _tarefaPomodoro;
        DateTime toGo;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1Load(object sender, EventArgs e)
        {
            Height = 200;

            SetIcon(1);

            tbxTarefa.Text = "";
            tbxTarefa.Focus();
            
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["path"]))
            {
                _path = ConfigurationManager.AppSettings["path"];
            }

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            LoadToDo();
        }

        private void LoadToDo()
        {
            if (!File.Exists(Path.Combine(_path, "ToDo.txt"))) return;

            listBox1.Items.Clear();

            var items = new List<string>();
            using (var stream = File.OpenRead(Path.Combine(_path, "ToDo.txt")))  
            using (var reader = new StreamReader(stream))  
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    items.Add(line);
                }
            }

            listBox1.Items.AddRange(items.ToArray());
        }

        private void SetIcon(int tipo)
        {
            string basePath =
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                             "Resources");

            switch (tipo)
            {
                case 1:
                    Icon = new Icon(Path.Combine(basePath, "note.ico"));
                    break;
                default:
                    Icon = new Icon(Path.Combine(basePath, "clock.ico"));
                    break;
            }
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (tbxTarefa.Text.Length == 0)
            {
                MessageBox.Show(@"Qual a tarefa ?");
                return;
            }

            string tarefa = tbxTarefa.Text;
            string hora = DateTime.Now.ToString("HHmmss");
            bool horaAssinalada = false;
            if (tbxTarefa.Text.Length > 6 && tbxTarefa.Text.Contains("@") && 
                !tbxTarefa.Text.ToLowerInvariant().StartsWith("td"))
            {
                string fim = tbxTarefa.Text.Substring(tbxTarefa.Text.IndexOf('@'));
                if (fim.StartsWith("@"))
                {
                    hora = fim.Substring(1).Replace(":", "").PadRight(6, '0');
                    tarefa = tbxTarefa.Text.Substring(0, tbxTarefa.Text.IndexOf('@'));
                    horaAssinalada = true;
                }
            }

            if (tbxTarefa.Text.Length > 6 && tbxTarefa.Text.Contains("\\d"))
            {
                var regex = new Regex(@"\\d[0-9](\*?)", RegexOptions.IgnoreCase);
                Match match = regex.Match(tbxTarefa.Text);

                if (!string.IsNullOrEmpty(match.Value))
                {
                    string tarefaTask = tbxTarefa.Text.Replace(match.Value, "");
                    DateTime data = DateTime.Today.AddDays(Int16.Parse(match.Value.Substring(2, 1)));

                    if (string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                            CreateTaskInOutlook(tarefaTask, data, tbxTarefa.Text.EndsWith("*"));
                        
                    }
                    GravarAFazer("due:" + data.ToString("yyyy-MM-dd") + " " + tarefaTask, false);
                }
            }

            string tmpConteudo = DateTime.Now.ToString("yyyyMMdd") + " " + hora + "\t";
            if (tarefa.ToUpperInvariant().StartsWith("TD"))
                tmpConteudo += tarefa.Substring(2) + " [ToDo]" + Environment.NewLine;
            else
                tmpConteudo += tarefa + Environment.NewLine;

            string path = GetPath();

            if (horaAssinalada)
            {
                var listaConteudo = new List<string>();

                if (File.Exists(path))
                {
                    string[] atualConteudo = File.ReadAllLines(path);
                    listaConteudo = atualConteudo.ToList();
                    File.Delete(path + ".backup");
                    File.Move(path, path + ".backup");
                }
                listaConteudo.Add(tmpConteudo);
                listaConteudo.Sort();

                foreach (string linha in listaConteudo.Where(linha => !string.IsNullOrEmpty(linha)))
                {
                    File.AppendAllText(path, linha + Environment.NewLine);
                }
            }
            else
            {
                File.AppendAllText(path, tmpConteudo);
            }

            GravarAFazer(tbxTarefa.Text, true);

            if (tbxTarefa.Text.StartsWith(";"))
            {
                _tarefaPomodoro = tbxTarefa.Text;
                pomodoro.Interval = (int) updPomodoro.Value*60*1000;
                pomodoro.Enabled = true;
                _horaInicioPomodoro = DateTime.Now;
                timerBarra.Enabled = true;
                TopMost = true;
                SetIcon(2);
                Opacity = 1;
                ShowBtnTimers(true);
            }

            tbxTarefa.Text = "";

            WindowState = FormWindowState.Minimized;

            //Environment.Exit(0);
        }

        private void CreateTaskInOutlook(string tarefaTask, DateTime data, bool DoReminder)
        {
            var app = new Application();
            NameSpace ns = app.GetNamespace("mapi");
            ns.Logon("Outlook", Missing.Value, false, true);
            var oTask =
                app.CreateItem(OlItemType.olTaskItem) as
                TaskItem;
            if (oTask != null)
            {
                oTask.Assign();
                oTask.Subject = tarefaTask;
                oTask.Body = tarefaTask;
                oTask.StartDate = new DateTime(data.Year, data.Month, data.Day);
                oTask.DueDate = new DateTime(data.Year, data.Month, data.Day);
                if (DoReminder)
                {
                    oTask.ReminderSet = false;
                }
                else
                {
                    oTask.ReminderSet = true;
                    oTask.ReminderTime = new DateTime(data.Year, data.Month, data.Day, 10, 0, 0);
                }
                oTask.Save();
            }
            ns.Logoff();
        }

        private void GravarAFazer(string tarefa, bool check)
        {
            if (check)
            {
                if (tarefa.ToUpper().StartsWith("TD"))
                {
                    GravarAFazer(tarefa.Substring(2));
                    LoadToDo();
                }
            }
            else
            {
                GravarAFazer(tarefa);
            }
        }

        private void GravarAFazer(string tarefa)
        {
            string aFazerPath = Path.Combine(_path, "todo.txt");

            File.AppendAllText(aFazerPath, tarefa + Environment.NewLine);
        }

        private string GetPath(DateTime data)
        {
            return Path.Combine(_path, data.ToString("dd.MM.yyyy") + ".txt");
        }

        private string GetPath()
        {
            return Path.Combine(_path, DateTime.Now.ToString("dd.MM.yyyy") + ".txt");
        }

        private void Form1KeyDown(object sender, KeyEventArgs e)
        {
            if (Height == 200)
                if (e.Alt && e.KeyCode == Keys.Down)
                    label1_DoubleClick(sender, null);
            if (Height == 420)
                if (e.Alt && e.KeyCode == Keys.Up)
                    label1_DoubleClick(sender, null);
            if (e.Alt && e.KeyCode == Keys.C)
                button2_Click(sender, null);
            if (e.Alt && e.KeyCode == Keys.A)
                LinkLabel2LinkClicked(sender, null);
            if (e.Alt && e.KeyCode == Keys.D)
               button3_Click(sender, null);
            if (e.Alt && e.KeyCode == Keys.S)
                Button1Click(sender, null);
            if (e.Control && e.KeyCode == Keys.S)
                Button1Click(sender, null);
            if (e.Control && e.KeyCode == Keys.F)
                textBox1.Focus();
            if (e.Control && e.KeyCode == Keys.O && textBox2.Visible)
            {
                GetJournal();
            }
            if (e.KeyCode == Keys.Escape)
                WindowState = FormWindowState.Minimized;
                //Environment.Exit(0);
        }
        private void Form1Resize(object sender, EventArgs e)
        {
            tbxTarefa.Focus();
        }


        private void PomodoroTick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            pomodoro.Enabled = false;
            timerBarra.Enabled = false;
            Text = @"Journal";
            updPomodoro.Value = 20;

            if (!sender.Equals(btnStopTimer))
                MessageBox.Show(string.Format(@"FIM::{0}{1}Next !", _tarefaPomodoro, Environment.NewLine), @"Pomodoro",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            TopMost = false;
            SetIcon(1);
            Opacity = 0.85;
            ShowBtnTimers(false);
        }

        private void TimerBarraTick(object sender, EventArgs e)
        {
            var tempoTotal = new DateTime(1900, 1, 1, 0, (int) updPomodoro.Value, 0);
            toGo = tempoTotal - (DateTime.Now - _horaInicioPomodoro);
            Text = string.Format("Journal - {0}:{1}", toGo.Minute.ToString("00"), toGo.Second.ToString("00"));
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Font = new Font(Font, FontStyle.Regular);
            textBox1.Text = "";
            AcceptButton = null;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Gray;
            textBox1.Font = new Font(Font, FontStyle.Italic);
            textBox1.Text = "procurar ...";
            AcceptButton = button1;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;

            var hits = new List<string>();

            var files = Directory.GetFiles(_path, "*.txt");

            IComparer fileComparer = new SortFiles.CompareFileByDate();

            Array.Sort(files, fileComparer);

            foreach (var file in files)
            {
                string[] linhas = File.ReadAllLines(file);
                var data = string.Empty;
                var tarefa = string.Empty;
                foreach (var linha in linhas)
                {
                    const RegexOptions options = RegexOptions.None;
                    var regex = new Regex(@"\d{8}", options);
                    var isMatch = regex.IsMatch(linha);
                    if (isMatch)
                    {
                        if (data != string.Empty)
                        {
                            if (tarefa.ToLower().Contains(textBox1.Text.ToLower()))
                            {
                                hits.Add(data + " >> ");
                                hits.Add(tarefa);
                            }
                        }
                        tarefa = linha;
                        data = file;
                    }
                    else
                    {
                        tarefa += Environment.NewLine + linha;
                    }
                }

                if (tarefa.ToLower().Contains(textBox1.Text.ToLower()))
                {
                    hits.Add(data + ">>");
                    hits.Add(tarefa);
                }

            }

            using (var form = new FormResultado())
            {
                form.criterio = textBox1.Text;
                form.resultados.AddRange(hits);
                form.ShowDialog();
            }
        }

        private void linkLabel1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(@"Arquivo contendo as atividades do dia", @"My Journal");
        }

        private void linkLabel4_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(@"Abre diretório onde estão os arquivos", @"My Journal");

        }

        private void updPomodoro_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(@"tempo da atividade em minutos(pomodoro)", @"My Journal");

        }

        private void textBox1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(@"Procura pelo texto dentro das atividades", @"My Journal");
        }

        private void tbxTarefa_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            const string help = @"
                    grava tarefas. Pode colocar os textos adicionais no final da tarefa:
                    \d[0-9]{1}(*)?: gravar tarefa no outlook até 9 dias à frente com lembrete. * para não gravar lembrete
                        este switch grava a tarefa como ""a fazer""
                    @hh(:)?mm: grava a tarefa nesta hora específica
                    se começa com ; (ponto-e-vírgula), conta o tempo(técnica Pomodoro)
                    se começa com td (tê dê) grava a tarefa como ""a fazer""
                        utiliza a sintaxe do TODO.txt da Gina Trapani(http://todotxt.com/)
                    ao pressionar {alt}+{down}, abre-se visualizador das datas. Navegue com {left} ou {right}
                    ----
                    version: 12-03-2014 12:25
                ";
            MessageBox.Show(help, @"My Journal");
        }

        private void Form1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            tbxTarefa_HelpRequested(sender, hlpevent);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = GetPath(DateTime.Now.Date);

            OpenJournalFile(path);
        }

        private static void OpenJournalFile(string path)
        {
            try
            {
                using (var p = new Process())
                {
                    var psi = new ProcessStartInfo
                        {
                            FileName = path
                        };
                    p.StartInfo = psi;
                    p.Start();
                }
            }
            catch
            {
                MessageBox.Show(@"Não consegui abrir o arquivo");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (var p = new Process())
                {
                    var psi = new ProcessStartInfo
                    {
                        Arguments = _path,
                        FileName = "explorer"
                    };
                    p.StartInfo = psi;
                    p.Start();
                }
            }
            catch
            {
                MessageBox.Show(@"Não consegui abrir o diretorio");
            }
        }

        private void tbxTarefa_TextChanged(object sender, EventArgs e)
        {

        }

        private void updPomodoro_Enter(object sender, EventArgs e)
        {
            updPomodoro.Select(0,updPomodoro.Text.Length);
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                GetJournal();
            }
        }

        private void GetJournal()
        {
            string path = GetPath(dateTimePicker2.Value);
            OpenJournalFile(path);
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (Height == 200)
            {
                label1.Text = "é";
                Height = 420;
                ReadJournal();

                textBox2.Visible = true;
                textBox2.Focus();
                textBox2.Select(0,0);
            }
            else
            {
                label1.Text = "ê";
                Height = 200;
                textBox2.Visible = false;
                dateTimePicker2.Value = DateTime.Today;
            }
        }

        private void ReadJournal()
        {
            string path = GetPath(dateTimePicker2.Value);

            try
            {
                textBox2.Text = File.ReadAllText(path).Replace(dateTimePicker2.Value.ToString("yyyyMMdd") + " ","");
            }
            catch
            {
                textBox2.Text = @"Não consegui abrir o arquivo " + path;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                dateTimePicker2.Value = dateTimePicker2.Value.AddDays(1);
                ReadJournal();
            }
            if (e.KeyCode == Keys.Left)
            {
                dateTimePicker2.Value = dateTimePicker2.Value.AddDays(-1);
                ReadJournal();
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            StringBuilder sb = new StringBuilder();
            foreach (var item in listBox1.Items)
            {
                sb.AppendLine(item.ToString());
            }
            File.WriteAllText(Path.Combine(_path, "ToDo.txt"), sb.ToString());
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ReadJournal();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void btnPauseTimer_Click(object sender, EventArgs e)
        {
            if (timerBarra.Enabled)
            {
                timerBarra.Stop();
                pomodoro.Stop();
            }
            else
            {
                timerBarra.Start();
                pomodoro.Start();
            }
        }

        private void btnStopTimer_Click(object sender, EventArgs e)
        {
            PomodoroTick(btnStopTimer, EventArgs.Empty);
        }

        private void ShowBtnTimers(bool show)
        {
            if (show)
            {
                btnStopTimer.Visible = true;
                updPomodoro.Visible = false;
            }
            else
            {
                btnStopTimer.Visible = false;
                updPomodoro.Visible = true;
            }
        }
    }

    public class SortFiles
    {
        public class CompareFileByDate : IComparer
        {
            int IComparer.Compare(Object a, Object b)
            {
                var fileInfoA = new FileInfo((string)a);
                var fileInfoB = new FileInfo((string)b);

                var dateA = fileInfoA.CreationTime;
                var dateB = fileInfoB.CreationTime;

                return DateTime.Compare(dateB, dateA);
            }
        }
    }
}
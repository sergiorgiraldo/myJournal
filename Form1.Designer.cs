using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MyJournal
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbxTarefa = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.updPomodoro = new System.Windows.Forms.NumericUpDown();
            this.pomodoro = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.timerBarra = new System.Windows.Forms.Timer(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.updPomodoro)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxTarefa
            // 
            this.tbxTarefa.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbxTarefa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxTarefa.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTarefa.ForeColor = System.Drawing.Color.Brown;
            this.tbxTarefa.Location = new System.Drawing.Point(5, 9);
            this.tbxTarefa.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbxTarefa.Multiline = true;
            this.tbxTarefa.Name = "tbxTarefa";
            this.tbxTarefa.Size = new System.Drawing.Size(613, 117);
            this.tbxTarefa.TabIndex = 0;
            this.tbxTarefa.Text = "todo ....";
            this.tbxTarefa.TextChanged += new System.EventHandler(this.tbxTarefa_TextChanged);
            this.tbxTarefa.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.tbxTarefa_HelpRequested);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(557, 231);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "G&ravar";
            this.toolTip1.SetToolTip(this.button1, "CTRL-S | ENTER");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // updPomodoro
            // 
            this.updPomodoro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.updPomodoro.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updPomodoro.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.updPomodoro.Location = new System.Drawing.Point(5, 232);
            this.updPomodoro.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.updPomodoro.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updPomodoro.Name = "updPomodoro";
            this.updPomodoro.Size = new System.Drawing.Size(47, 27);
            this.updPomodoro.TabIndex = 1;
            this.toolTip1.SetToolTip(this.updPomodoro, "Pomodoro");
            this.updPomodoro.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.updPomodoro.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.updPomodoro_HelpRequested);
            this.updPomodoro.Enter += new System.EventHandler(this.updPomodoro_Enter);
            // 
            // pomodoro
            // 
            this.pomodoro.Interval = 1000;
            this.pomodoro.Tick += new System.EventHandler(this.PomodoroTick);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Location = new System.Drawing.Point(368, 232);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 27);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "procurar ...";
            this.toolTip1.SetToolTip(this.textBox1, "CTRL-F");
            this.textBox1.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.textBox1_HelpRequested);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(64, 231);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "&Hoje";
            this.toolTip1.SetToolTip(this.button2, "ALT-C para abrir o dia de hoje");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.linkLabel1_HelpRequested);
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(282, 231);
            this.button4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(78, 25);
            this.button4.TabIndex = 6;
            this.button4.Text = "&Diretório";
            this.toolTip1.SetToolTip(this.button4, "ALT-D");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button3_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(131, 232);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(121, 27);
            this.dateTimePicker2.TabIndex = 3;
            this.toolTip1.SetToolTip(this.dateTimePicker2, "CTRl-ENTER para abrir o dia");
            this.dateTimePicker2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker2_KeyDown);
            // 
            // timerBarra
            // 
            this.timerBarra.Interval = 1000;
            this.timerBarra.Tick += new System.EventHandler(this.TimerBarraTick);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Silver;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(0, 260);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(624, 209);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "~~~~~";
            this.textBox2.Visible = false;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Wingdings", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label1.Location = new System.Drawing.Point(258, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "ê";
            this.label1.Click += new System.EventHandler(this.label1_DoubleClick);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.listBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.Color.DarkGreen;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 28;
            this.listBox1.Location = new System.Drawing.Point(5, 129);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(613, 88);
            this.listBox1.TabIndex = 10;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(624, 469);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.updPomodoro);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbxTarefa);
            this.Controls.Add(this.textBox2);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journal";
            this.Load += new System.EventHandler(this.Form1Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.Form1_HelpRequested);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1KeyDown);
            this.Resize += new System.EventHandler(this.Form1Resize);
            ((System.ComponentModel.ISupportInitialize)(this.updPomodoro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxTarefa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown updPomodoro;
        private System.Windows.Forms.Timer pomodoro;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timerBarra;

        private void LinkLabel2LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathAFazer = Path.Combine(_path, "MyJournal.AFazer");
            try
            {
                using (var p = new Process())
                {
                    var psi = new ProcessStartInfo
                                  {
                                      Arguments = pathAFazer,
                                      FileName = "notepad"
                                  };
                    p.StartInfo = psi;
                    p.Start();
                }
            }
            catch
            {
                MessageBox.Show("Não consegui abrir o arquivo");
            }
        }

        private TextBox textBox1;
        private Button button2;
        private Button button4;
        private DateTimePicker dateTimePicker2;
        private TextBox textBox2;
        private Label label1;
        private ListBox listBox1;
    }
}


using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;
using SphubCore;
using SphubCore.Classes.Server;
using SphubCore.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphubInterceptServer
{
    public partial class MainDiag : MetroForm
    {
        public static MainDiag MD;
        public static RichTextBox RDB;

        public MainDiag()
        {
            InitializeComponent();
            MD = this;
            RDB = richTextBox1;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void MainDiag_Shown(object sender, EventArgs e)
        {
            var th = new Thread(new ThreadStart(delegate
            {
                MainServerLoop MSL = new MainServerLoop();
                MSL.Run();
            }));
            th.Start();
        }
    }

    public static class Console
    {
        public static void WriteLine(string input)
        {
            MainDiag.RDB.Invoke((MethodInvoker)delegate
            {
                MainDiag.RDB.AppendText(input);
                MainDiag.RDB.AppendText(Environment.NewLine);
            });
        }

        public static void Write(string input)
        {
            MainDiag.RDB.Invoke((MethodInvoker)delegate
            {
                MainDiag.RDB.AppendText(input);
            });
        }

        public static void NewLine()
        {
            MainDiag.RDB.Invoke((MethodInvoker)delegate
            {
                MainDiag.RDB.AppendText(Environment.NewLine);
            });
        }

        public static void Clear()
        {
            MainDiag.RDB.Invoke((MethodInvoker)delegate
            {
                MainDiag.RDB.Text = "";
            });
        }
    }
}
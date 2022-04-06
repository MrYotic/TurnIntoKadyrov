using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
namespace TurnIntoKadyrov
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();
        private Thread updater = new Thread(() => new Form1().Update());
        private Random rnd = new Random("За путина".Select(zа => (int)zа).Sum());
        private static string lastText = "";
        private int amplitude = 5;
        private static bool enabled = false;
        private string ConvertToKadyrov(string str) => string.Join(" ", Replace(str, new List<(dynamic, dynamic)>()
            .Concat(Z.Checked ? new List<(dynamic, dynamic)> { ("з", "z"), ("З", "Z") } : new List<(dynamic, dynamic)>())
            .Concat(V.Checked ? new List<(dynamic, dynamic)> { ("в", "v"), ("В", "V") } : new List<(dynamic, dynamic)>())
            .Concat(AddInfo.Checked ? new List<(dynamic, dynamic)> { ("украинец", "хохол") } : new List<(dynamic, dynamic)>())
            .ToArray()
            ).Split(' ').ToList().Select(z => z + (rnd.Next(0, amplitude) == 0 ? " Дон" : "")));
        private void Update()
        {
            while (true)
                try
                {
                    Thread.Sleep(100);
                    if (enabled && Clipboard.GetText() != lastText)
                        Clipboard.SetText(lastText = ConvertToKadyrov(Clipboard.GetText()));
                }catch { }
        }
        private void CloseB_Click(object sender, EventArgs e) => Environment.Exit(1);
        private void AmplitudeT_TextChanged(object sender, EventArgs e) => int.TryParse(AmplitudeT.Text, out amplitude);
        private void Enable_Click(object sender, EventArgs e) => Enable.Text = (enabled = !enabled) ? "Off" : "Send rockets with hohlo-liquid to NATO";
        public string Replace(string str, params (dynamic, dynamic)[] args)
        {
            args.ToList().ForEach(z => str.Replace(z.Item1.ToSting(), z.Item2.ToString()));
            return str;
        }
    }
}
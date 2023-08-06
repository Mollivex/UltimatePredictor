using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace UltimatePredictor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();  
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e) { }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) { }

        private async void bPredict_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        // To get around the progressive animation, we need to move the
                        // progress bar backwards.
                        if (i == progressBar1.Maximum)
                        {
                            // Special case as value can't be set greater than Maximum
                            progressBar1.Maximum = i + 1;   // Temporarily increase Maximum
                            progressBar1.Value = i + 1;     // Move past
                            progressBar1.Minimum = i;       // Temporarily increase Maximum
                        }
                        else
                        {
                            progressBar1.Value = i + 1;     // Move past
                        }
                        progressBar1.Value = i;             // Move to correct value
                    }));
                    Thread.Sleep(20);
                }
            });
            MessageBox.Show("Prediction");
          
        }
    }
}

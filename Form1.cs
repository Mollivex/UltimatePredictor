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
using System.IO;
using Newtonsoft.Json;

namespace UltimatePredictor
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Application name
        /// </summary>
        private const string APP_NAME = "ULTIMATE_PREDICTOR";

        /// <summary>
        /// Path to predictionsConfig.json file
        /// </summary>
        private readonly string PREDICTIONS_CONFIG_PATH = $"{Environment.CurrentDirectory}\\predictionsConfig.json";
        /// <summary>
        /// Array of strings of json data file
        /// </summary>
        private string[] _predictions;
        Random _random = new Random();
        public Form1()
        {
            InitializeComponent();  
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e) { }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) { }

        /// <summary>
        /// Predict button generator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void bPredict_Click(object sender, EventArgs e)
        {
            bPredict.Enabled = false;
            await Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateProgressBar(i);
                        this.Text = $"{i}%";
                    }));
                    Thread.Sleep(20);
                }
            });

            var index = _random.Next(_predictions.Length); // generate random index depends how much predictions you have in array

            var prediction = _predictions[index]; // choose the prediction with random generated index

            MessageBox.Show($"{prediction}!");

            progressBar1.Value = 0;
            this.Text = APP_NAME;
            bPredict.Enabled = true;
        }

        /// <summary>
        /// Update progressBar and set right timing to progressive animation
        /// </summary>
        /// <param name="i"></param>
        private void UpdateProgressBar(int i)
        {
            // To get around the progressive animation, we need to move the
            // progress bar backwards.
            if (i == progressBar1.Maximum)
            {
                // Special case as value can't be set greater than Maximum
                progressBar1.Maximum = i + 1;   // Temporarily increase Maximum
                progressBar1.Value = i + 1;     // Move past
                progressBar1.Maximum = i;       // Temporarily increase Maximum
            }
            else
            {
                progressBar1.Value = i + 1;     // Move past
            }
            progressBar1.Value = i;             // Move to correct value
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = APP_NAME;

            try
            {
                var data = File.ReadAllText(PREDICTIONS_CONFIG_PATH);           // create variable data using to show data from json file using System.IO
                _predictions = JsonConvert.DeserializeObject<string[]>(data);   // converts predictionsConfig.json one string data into the array of strings <string[]> using Newtonsoft.Json library
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // If predictionsConfig.json will be empty or it will doesn't have the right source path
                if (_predictions == null)
                {
                    this.Close();
                }
                else if (_predictions.Length == 0)
                {
                    MessageBox.Show("Predictions are over! There will be no movie!");
                    this.Close();
                }
            }
        }
    }
}

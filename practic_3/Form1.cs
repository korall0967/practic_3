using ScottPlot;
using ScottPlot.PathStrategies;
using ScottPlot.WinForms;
using MathNet.Numerics.Interpolation;
using System.Text.RegularExpressions;

namespace practic_3
{
    public partial class Form1 : Form
    {
        private MathNet.Numerics.Interpolation.CubicSpline spline;
        public Form1()
        {
            InitializeComponent();

            double[] xData = {
                -2000, -1500, -1000, -500, 0, 500, 1000, 1500, 2000,
                2500, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000,
                11000, 12000, 14000, 16000, 18000, 20000, 24000, 28000,
                32000, 36000, 40000, 50000, 60000, 80000
            };

            double[] yData = {
                301.2, 297.9, 294.7, 291.4, 288.2, 284.9, 281.7, 278.4, 275.2,
                271.9, 268.7, 262.2, 255.7, 249.2, 242.7, 236.2, 229.7, 223.3,
                216.8, 216.7, 216.7, 216.7, 216.7, 216.7, 220.6, 224.5,
                228.5, 239.3, 250.4, 270.7, 247.0, 198.6
            };

            spline = MathNet.Numerics.Interpolation.CubicSpline.InterpolateNatural(xData, yData);

            double[] xSmooth = ScottPlot.Generate.Consecutive(821, 100, -2000);
            double[] ySmooth = new double[xSmooth.Length];

            for (int i = 0; i < xSmooth.Length; i++)
            {
                ySmooth[i] = spline.Interpolate(xSmooth[i]);
            }

            formsPlot1.Plot.Add.ScatterLine(xSmooth, ySmooth);
            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }

        private void Marker(double x, double y)
        {
            formsPlot1.Plot.Add.Marker(x, y);
        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            if (!Regex.IsMatch(textBox1.Text, @"^-?\d+([.]\d+)?$"))
            {
                MessageBox.Show("Введите число в виде десятичной дроби через .");
            }

            else
            {
                double height = double.Parse(textBox1.Text);
                double temperature = spline.Interpolate(height);
                Marker(height, temperature);
            }
        }
    }
}

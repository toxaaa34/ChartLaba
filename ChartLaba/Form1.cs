using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartLaba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int count = 0;
        public double shift = 0;
        string str0 = "Chart", str_name;

        Random random = new Random();


        private void button1_Click(object sender, EventArgs e)
        {
            double x, y, dx = 0.5;
            count++;
            str_name = str0 + count.ToString();
            chart1.Series.Add(str_name);
            if (comboBox1.SelectedIndex == 0) chart1.Series[str_name].ChartType = SeriesChartType.Spline;
            else if (comboBox1.SelectedIndex == 1)
            {
                chart1.Series[str_name].ChartType = SeriesChartType.Bubble;
                chart1.Series[str_name].MarkerStyle = MarkerStyle.Square;
                chart1.Series[str_name]["BubbleMaxSize"] = "1";
            }

            else if (comboBox1.SelectedIndex == 2) chart1.Series[str_name].ChartType = SeriesChartType.Column;
            else { MessageBox.Show("Select CharType", "Chart1"); chart1.Series.RemoveAt(count); count--; shift -= 0.05; return; }

            chart1.Series[str_name].Color = Color.Red;

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            chart1.ChartAreas[0].CursorX.Interval = 0.001;
            chart1.ChartAreas[0].CursorY.Interval = 0.001;

            chart2.Series.Add(str_name);
            chart2.Series[str_name].ChartType = SeriesChartType.Spline;
            chart2.Series[str_name].Color = Color.Green;

            x = 0;
            for (int i = 0; i < 100; i++)
            {
                y = Math.Sin(shift + x);
                chart1.Series[str_name].Points.AddXY(x, y);

                y = Math.Cos(shift + x);

                x = x + dx;
            }

            for (int pointIndex = 0; pointIndex < 10; pointIndex++)
            {
                chart2.Series[str_name].Points.AddY(random.Next(5, 95));
            }
            shift += 1;

            if (count == 1)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.Title = "X, mm";
                chart1.ChartAreas["ChartArea1"].AxisY.Title = "Cos(X), mm";

                chart2.ChartAreas["ChartArea1"].AxisX.Title = "X, mm";
                chart2.ChartAreas["ChartArea1"].AxisY.Title = "Sin(X), mm";
            }
            button2.Enabled = true;
        }

        private void chart1_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            double x, y;
            x = chart1.ChartAreas["ChartArea1"].CursorX.Position;
            if(double.IsNaN(x))return;
            textBox1.Text = Convert.ToString(x);

            y = chart1.ChartAreas["ChartArea1"].CursorY.Position;
            if(double.IsNaN(y))return;
            textBox2.Text = Convert.ToString(y);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text != "None")
            {
                chart2.Series[str_name].IsValueShownAsLabel = true;
                if(comboBox2.Text != "Auto")
                {
                    chart2.Series[str_name]["LabelStyle"] = comboBox2.Text;
                }
            }
            else
            {
                chart2.Series[str_name].IsValueShownAsLabel = false;
            }
        }

        private void pointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(); 
            f.ShowDialog();

            if (f.comboBox1.Text != "None")
            {
                chart2.Series[str_name].IsValueShownAsLabel = true;
                if (f.comboBox1.Text != "Auto")
                {
                    chart2.Series[str_name]["LabelStyle"] = f.comboBox1.Text;
                }
            }
            else
            {
                chart2.Series[str_name].IsValueShownAsLabel = false;
            }

            /*
            if (f.points != "None")
            {
                chart2.Series[str_name].IsValueShownAsLabel = true;
                if (f.points != "Auto")
                {
                    chart2.Series[str_name]["LabelStyle"] = f.points;
                }
            }
            else
            {
                chart2.Series[str_name].IsValueShownAsLabel = false;
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (count <= 0) { button2.Enabled = false; return; }
            chart1.Series.RemoveAt(count);
            chart2.Series.RemoveAt(count);
            count--;
        }



    }
}

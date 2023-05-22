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
using System.Windows.Forms.DataVisualization.Charting;

namespace Funciones_lineales
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void EcuacionCreate()
        {
            // Código que se ejecutará en el hilo separado

            // Verifica si los valores ingresados son válidos
            if (int.TryParse(textBox1.Text, out int x1) && int.TryParse(textBox2.Text, out int y1) &&
                int.TryParse(textBox3.Text, out int x2) && int.TryParse(textBox4.Text, out int y2))
            {
                double pendiente = (double)(y2 - y1) / (x2 - x1);
                double constante = y1 - pendiente * x1;

                string ecuacion = $"y = {pendiente}x + {constante}";

                // Actualiza el resultado en el control de texto (txtResultado)
                // Utiliza el método Invoke para actualizar los controles desde el hilo principal
                txtResultado.Invoke(new Action(() =>
                {
                    txtResultado.Clear();
                    txtResultado.Text = "La fórmula de la pendiente (m) es:\r\n\r\n";
                    txtResultado.Text += $"m = ({y2} - {y1}) / ({x2} - {x1})\r\n";
                    txtResultado.Text += $"m = {pendiente}\r\n\r\n";
                    txtResultado.Text += $"Ahora, usando la ecuación y = mx + b y el punto ({x1}, {y1}), podemos encontrar el valor de la constante (b):\r\n\r\n";
                    txtResultado.Text += $"{y1} = {pendiente} * {x1} + b\r\n";
                    txtResultado.Text += $"{y1} = {pendiente * x1} + b\r\n";
                    txtResultado.Text += $"b = {y1 - pendiente * x1}\r\n\r\n";
                    txtResultado.Text += $"Por lo tanto, la ecuación de la recta que pasa por los puntos ({x1}, {y1}) y ({x2}, {y2}) es:\r\n\r\n";
                    txtResultado.Text += $"y = {pendiente}x + {constante}";
                }));
            }
            else
            {
                // Muestra un mensaje de error en un cuadro de diálogo
                MessageBox.Show("Ingrese valores numéricos válidos en los TextBox.");
            }
        }

        private void GraficarEcuation()
        {
            if (int.TryParse(textBox1.Text, out int x1) && int.TryParse(textBox2.Text, out int y1) &&
                int.TryParse(textBox3.Text, out int x2) && int.TryParse(textBox4.Text, out int y2))
            {
                double pendiente = (double)(y2 - y1) / (x2 - x1);
                double constante = y1 - pendiente * x1;

                chart1.Invoke((MethodInvoker)delegate {
                    chart1.Series.Clear();
                    Series series = chart1.Series.Add("Puntos");
                    series.ChartType = SeriesChartType.Point;

                    for (int x = 0; x <= 4; x++)
                    {
                        double y = pendiente * x + constante;
                        series.Points.AddXY(x, y);
                    }
                });
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int number) || number < 0 || number > 999)
            {
                // Si el valor no es un número válido o está fuera del rango permitido, puedes mostrar un mensaje de error o tomar alguna otra acción.
                // Por ejemplo, mostrar un MessageBox:
                MessageBox.Show("Ingrese un número válido entre 0 y 999.");
                textBox1.Text = ""; // Limpia el contenido del TextBox si es inválido.
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out int number) || number < 0 || number > 999)
            {
                // Si el valor no es un número válido o está fuera del rango permitido, puedes mostrar un mensaje de error o tomar alguna otra acción.
                // Por ejemplo, mostrar un MessageBox:
                MessageBox.Show("Ingrese un número válido entre 0 y 999.");
                textBox2.Text = ""; // Limpia el contenido del TextBox si es inválido.
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox4.Text, out int number) || number < 0 || number > 999)
            {
                // Si el valor no es un número válido o está fuera del rango permitido, puedes mostrar un mensaje de error o tomar alguna otra acción.
                // Por ejemplo, mostrar un MessageBox:
                MessageBox.Show("Ingrese un número válido entre 0 y 999.");
                textBox4.Text = ""; // Limpia el contenido del TextBox si es inválido.
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out int number) || number < 0 || number > 999)
            {
                // Si el valor no es un número válido o está fuera del rango permitido, puedes mostrar un mensaje de error o tomar alguna otra acción.
                // Por ejemplo, mostrar un MessageBox:
                MessageBox.Show("Ingrese un número válido entre 0 y 999.");
                textBox3.Text = ""; // Limpia el contenido del TextBox si es inválido.
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtResultado.Clear();
            // Crea un nuevo hilo
            Thread thread = new Thread(EcuacionCreate);

            // Inicia el hilo
            thread.Start();

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Crea un nuevo hilo
            Thread thread2 = new Thread(GraficarEcuation);

            // Inicia el hilo
            thread2.Start();
        }
    }
}

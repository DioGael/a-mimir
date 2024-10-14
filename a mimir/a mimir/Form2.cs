using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace a_mimir
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ':' && e.KeyChar != (char)Keys.Back) // && e.KeyChar != 'h' && e.KeyChar != 'm' && e.KeyChar != 'm')
            {
                e.Handled = true; // Prevent invalid character from being entered
            }

            // Prevent more than two colons (:) in the TextBox
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == ':' && textBox.Text.Count(c => c == ':') >= 2)
            {
                e.Handled = true; // Block the third colon
            }
        }
        public string InputText
        {
            get { return textBox1.Text; }
        }
        private void Form2_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("Puedes ingresar un valor numérico separado por ':' para delimitar horas, minutos y segundos.\n\n" +
                "El valor del tiempo ingresado dependerá de cuántos símbolos agregues.\n" +
                "Ejemplo:\n-\"1:40:12\" iniciará el temporizador con un tiempo de 1 hora, 40 minutos y 12 segundos.\n" +
                "-\"23:40\" iniciará el temporizador con un tiempo de 23 minutos y 40 segundos.\n" +
                "-\"4433\" inicia el temporizador con un tiempo de 4433 segundos.\n" +
                "Como muestra el último ejemplo, el temporizador ajusta automáticamente el tiempo ingresado, por lo que no hay que preocuparse por ingresar valores de los minutos o segundos mayores a 59.", "Ayuda");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

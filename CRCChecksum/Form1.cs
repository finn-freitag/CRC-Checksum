using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCChecksum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var textBox1 = (TextBox)sender;
            int selIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Replace("+1", "+x0");
            textBox1.Text = textBox1.Text
                .Replace("1", "¹")
                .Replace("2", "²")
                .Replace("3", "³")
                .Replace("4", "⁴")
                .Replace("5", "⁵")
                .Replace("6", "⁶")
                .Replace("7", "⁷")
                .Replace("8", "⁸")
                .Replace("9", "⁹")
                .Replace("0", "⁰");
            textBox1.Text = textBox1.Text.Replace("x+", "x¹+");
            string finalString = "";
            for(int i = 0; i < textBox1.Text.Length; i++)
            {
                if (char.IsLetter(textBox1.Text[i]))
                {
                    finalString += "x";
                    continue;
                }
                if (char.IsPunctuation(textBox1.Text[i]))
                {
                    finalString += "+";
                    continue;
                }
                finalString += textBox1.Text[i];
            }
            textBox1.Text = finalString;
            textBox1.SelectionStart = selIndex;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var textBox2 = (TextBox)sender;
            int selIndex = textBox2.SelectionStart;
            string finalString = "";
            for(int i = 0; i < textBox2.Text.Length; i++)
            {
                if (textBox2.Text[i] == '1' || textBox2.Text[i] == '0')
                    finalString += textBox2.Text[i];
            }
            textBox2.Text = finalString;
            textBox2.SelectionStart = selIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Polynom polynom = null;
            try
            {
                polynom = Polynom.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Ungültiges Polynom!");
                return;
            }
            Bits bits = new Bits();
            for(int i = 0; i < textBox2.Text.Length; i++)
            {
                bits.Add(textBox2.Text[i] == '1');
            }
            Bits sicherheitsfolge = CRC.GetChecksum(polynom, bits);
            textBox3.Text = sicherheitsfolge.ToString();
            textBox4.Text = bits.ToString() + sicherheitsfolge.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Polynom polynom = null;
            try
            {
                polynom = Polynom.Parse(textBox6.Text);
            }
            catch
            {
                MessageBox.Show("Ungültiges Polynom!");
                return;
            }
            Bits bits = new Bits();
            for (int i = 0; i < textBox5.Text.Length; i++)
            {
                bits.Add(textBox5.Text[i] == '1');
            }
            Bits sicherheitsfolge = CRC.GetChecksum(polynom, bits, false);
            if(Convert.ToInt32(sicherheitsfolge.ToString()) == 0)
            {
                textBox7.Text = "Bitfolge ist korrekt!";
            }
            else
            {
                textBox7.Text = "Bitfolge ist nicht korrekt!";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crvCalc_v0._5
{
    public partial class Form1 : Form
    {
		string buffer;
		double Memory = 0;
		bool oldTextDelete = true;
		public Form1()
        {
            InitializeComponent();
			timer1.Start();
		}
        private void timer1_Tick(object sender, EventArgs e)
        {
			toolStripStatusLabel_Date.Text = DateTime.Now.ToShortDateString();
			toolStripStatusLabel_Time.Text = DateTime.Now.ToLongTimeString();
		}
		public static double Add(double num1, double num2) { return num1 + num2; }
		public static double Subtract(double num1, double num2) { return num1 - num2; }
		public static double Multiply(double num1, double num2) { return num1 * num2; }
		public static double Divide(double num1, double num2)
		{
			if (num2 == 0) throw new Exception("Попытка деления на ноль.");
			double res = num1 / num2;
			return res;
		}
		public static string FindBracket(string _expr)
		{
			string sub;
			int openBracket;
			int closeBracket = _expr.IndexOf(')');
			if (closeBracket != -1)
			{
				openBracket = (_expr.Substring(0, closeBracket)).LastIndexOf('(');
				sub = _expr.Substring(openBracket + 1, closeBracket - openBracket - 1);
			}
			else sub = _expr;
			return sub;
		}
		public static string BracketsToSimple(string sub)
		{
			IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
			char[] opers = { '*', '/', '-', '+' };
			string[] numbers;
			while (sub.IndexOfAny(opers, 1) != -1)
			{
				foreach (char op in opers)
				{
					if (sub.IndexOf('-') == 0)
					{
						numbers = (sub.Remove(0, 1)).Split(opers);
						numbers[0] = '-' + numbers[0];
					}
					else numbers = sub.Split(opers);
					while (sub.IndexOf(op, 1) != -1)
					{
						for (int i = 1; i < numbers.Length; i++)
						{
							if (sub.IndexOf(numbers[i], sub.IndexOf(op, 1)) == sub.IndexOf(op, 1) + 1)
							{
								double num1 = Convert.ToDouble(numbers[i - 1], formatter);
								double num2 = Convert.ToDouble(numbers[i], formatter);
								double result = 0;
								switch (op)
								{
									case '*':
										result = Multiply(num1, num2);
										break;
									case '/':
										result = Divide(num1, num2);
										break;
									case '+':
										result = Add(num1, num2);
										break;
									case '-':
										result = Subtract(num1, num2);
										break;
								}
								sub = sub.Replace(numbers[i - 1] + op + numbers[i], Convert.ToString(result));
								break;
							}
						}
						if (sub.IndexOf('-') == 0)
						{
							numbers = sub.Remove(0, 1).Split(opers);
							numbers[0] = '-' + numbers[0];
						}
						else numbers = sub.Split(opers);
					}
				}
			}
			return sub;
		}
		public static string ExpressionToResult(string _expr)
		{
			string tempExp = _expr;
			do
			{
				string sub = FindBracket(tempExp);
				string tempSub = sub;
				string simple = BracketsToSimple(sub);
				if (tempExp.IndexOfAny(new char[] { '(', ')' }) != -1) tempExp = tempExp.Replace('(' + tempSub + ')', simple);
				else tempExp = tempExp.Replace(tempSub, simple);
			} while (tempExp.Split(new char[] { '*', '/', '+', '-' }).Length >= 3);
			return tempExp;
		}
		static void WriteFileTXT(List<object> listTemp)
		{
			string pathTxt = "log.txt";
			bool addToFile = false;
			if (File.Exists(pathTxt))
				addToFile = true;
			using (StreamWriter sw = new StreamWriter(pathTxt, addToFile))
			{
				foreach (object item in listTemp)
				{
					sw.WriteLine(item);
				}
			}
			MessageBox.Show("Файл \"" + pathTxt + "\" записан.", "crvCalc_v0.5");
		}


		private void button_0_Click(object sender, EventArgs e)
		{
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "0";
		}
		private void button_1_Click(object sender, EventArgs e)
		{
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "1";
		}
		private void button_2_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Text = string.Empty;
			textBox.Text += "2";
		}
        private void button_3_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "3";
		}
        private void button_4_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "4";
		}
        private void button_5_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "5";
		}
        private void button_6_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "6";
		}
        private void button_7_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "7";
		}
        private void button_8_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "8";
		}
        private void button_9_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "9";
		}
        private void button_Point_Click(object sender, EventArgs e)
        {
			textBox.Text += ".";
		}
        private void button_BracketLeft_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += "(";
		}
        private void button_BracketRight_Click(object sender, EventArgs e)
        {
			if (textBox.Text == "0") textBox.Clear();
			textBox.Text += ")";
		}
        private void button_Add_Click(object sender, EventArgs e)
        {
			textBox.Text += "+";
		}
        private void button_Subtract_Click(object sender, EventArgs e)
        {
			textBox.Text += "-";
		}
        private void button_Multiply_Click(object sender, EventArgs e)
        {
			textBox.Text += "*";
		}
        private void button_Divide_Click(object sender, EventArgs e)
        {
			textBox.Text += "/";
		}
        private void button_Backspace_Click(object sender, EventArgs e)
        {

			if (textBox.Text == string.Empty) textBox.Text = "0";
			else textBox.Text = textBox.Text.ToString().Remove(textBox.Text.ToString().Length - 1);
        }
		private void button_Discharge_Click(object sender, EventArgs e)
		{
			textBox.Text = "0";
		}
		private void button_Calculate_Click(object sender, EventArgs e)
		{
			string tempResult = ExpressionToResult(textBox.Text);
			listBox.Items.Add(textBox.Text + "=" + tempResult);
			textBox.Text = tempResult;
		}
        private void ToolStripMenuItem_File_Save_Click(object sender, EventArgs e)
        {
			List<object> listTemp = new List<object>();
			if (listBox.Items.Count != 0)
			{
				foreach (string item in listBox.Items)
					listTemp.Add(item);
			}
			WriteFileTXT(listTemp);
		}
        private void ToolStripMenuItem_File_Exit_Click(object sender, EventArgs e)
        {
			Close();
        }
        private void ToolStripMenuItem_Help_About_Click(object sender, EventArgs e)
        {
			MessageBox.Show("Простой калькулятор.\n\nchmilrv@gmail.com", "crvCalc_v0.5");
        }
        private void ToolStripMenuItem_Edit_Copy_Click(object sender, EventArgs e)
        {
				buffer = textBox.SelectedText;
		}
        private void ToolStripMenuItem_Edit_Paste_Click(object sender, EventArgs e)
        {
			textBox.Clear();
			textBox.Paste(buffer);
		}

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
			if (listBox.SelectedItems != null)
				textBox.Text = listBox.SelectedItem.ToString();
		}
        private void button_MAdd_Click(object sender, EventArgs e)
        {
			Memory += Convert.ToDouble(textBox.Text);
        }
        private void button_MSubtract_Click(object sender, EventArgs e)
        {
			Memory -= Convert.ToDouble(textBox.Text);
        }

        private void button_MemorySave_Click(object sender, EventArgs e)
        {
			Memory = Convert.ToDouble(textBox.Text);
        }

        private void button_MemoryRead_Click(object sender, EventArgs e)
        {
			textBox.Text = string.Empty;
			textBox.Text = Memory.ToString();
        }
        private void button_MemoryClear_Click(object sender, EventArgs e)
        {
			Memory = 0;
        }
    }
}
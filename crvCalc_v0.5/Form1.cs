using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crvCalc_v0._5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
			timer1.Start();
			
            
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
			toolStripStatusLabel_DayOfWeek.Text = DateTime.Now.DayOfWeek.ToString();
			toolStripStatusLabel_Date.Text = DateTime.Now.ToLongDateString();
			toolStripStatusLabel_Time.Text = DateTime.Now.ToLongTimeString();
		}
    }










}


/*
using System;
using static System.Console;
using System.Globalization;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Text.RegularExpressions;
//using System.Xml.Schema;
//using System.Net;
//using System.Security.Cryptography.X509Certificates;
Разработать консольный калькулятор.
При разработке рекомендуется:
	использовать ООП (создать класс математических действий, в котором необходимо реализовать методы для выполнения арифметических действий и т.д.);
	использовать обработку исключений;
	написать документацию к использованию калькулятора и выводить ее;
	использовать приоритет математических операций;
	уметь вычислять выражения типа: "5-4*(4-3)-6+5-(3/2)", если калькулятор не использует псевдографику;
	разработать интерфейс, используя символы псевдографики.
namespace crvCalc
{
	class Operations
	{
		double num1;
		double num2;
		public double Num1
		{
			get
			{
				return num1;
			}
			set
			{
				num1 = value;
			}
		}
		public double Num2
		{
			get
			{
				return num2;
			}
			set
			{
				num2 = value;
			}
		}
		public Operations(double _num1, double _num2)
		{
			Num1 = _num1;
			Num2 = _num2;
		}
		public double Add()
		{
			return Num1 + Num2;
		}
		public double Subtract()
		{
			return Num1 - Num2;
		}
		public double Multiply()
		{
			return Num1 * Num2;
		}
		public double Divide()
		{
			if (Num2 == 0) throw new Exception("Попытка деления на ноль.");
			double res = Num1 / Num2;
			return res;
		}
	}
	class ExpressionLogic
	{
		string expression;
		public string Expression
		{
			get
			{
				return expression;
			}
			set
			{
				while (value.IndexOf(" ") != -1) value = value.Remove(value.IndexOf(" "), 1);
				while (value.IndexOf(")(") != -1) value = value.Insert(value.IndexOf(")(") + 1, "*");
				while (value.IndexOf("++") != -1) value = value.Remove(value.IndexOf("++"), 1);
				while (value.IndexOf("--") != -1) value = value.Remove(value.IndexOf("--"), 1);
				while (value.IndexOf("**") != -1) value = value.Remove(value.IndexOf("**"), 1);
				while (value.IndexOf("//") != -1) value = value.Remove(value.IndexOf("//"), 1);
				//добавить конвертацию 2(  ) -> 2*(  )
				expression = value;
			}
		}
		public ExpressionLogic(string _expression)
		{
			Expression = _expression;
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
		public static string KillTwoSign(string sub)
		{
			sub = sub.Replace("+-", "-");
			sub = sub.Replace("--", "+");
			if (sub.IndexOf("*-") != -1)
			{
				char[] tempExpArray = sub.ToCharArray();
				for (int ii = 1; ii < tempExpArray.Length; ii++)
				{
					if (tempExpArray[ii] == '*' && tempExpArray[ii + 1] == '-')
					{
						for (int j = ii; j >= 0; j--)
						{
							if (tempExpArray[j] == '-')
							{
								if (j == 0) tempExpArray[j] = ' ';
								else tempExpArray[j] = '+';
								break;
							}
							if (tempExpArray[j] == '+')
							{
								tempExpArray[j] = '-';
								break;
							}
						}
						tempExpArray[ii + 1] = ' ';
					}
				}
				sub = new string(tempExpArray);
				while (sub.IndexOf(" ") != -1) sub = sub.Remove(sub.IndexOf(" "), 1);
			}
			if (sub.IndexOf("/-") != -1)
			{
				char[] tempExpArray = sub.ToCharArray();
				for (int iii = 1; iii < tempExpArray.Length; iii++)
				{
					if (tempExpArray[iii] == '/' && tempExpArray[iii + 1] == '-')
					{
						for (int j = iii; j >= 0; j--)
						{
							if (tempExpArray[j] == '-')
							{
								if (j == 0) tempExpArray[j] = ' ';
								else tempExpArray[j] = '+';
								break;
							}
							if (tempExpArray[j] == '+')
							{
								tempExpArray[j] = '-';
								break;
							}
						}
						tempExpArray[iii + 1] = ' ';
					}
				}
				sub = new string(tempExpArray);
				while (sub.IndexOf(" ") != -1) sub = sub.Remove(sub.IndexOf(" "), 1);
			}
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
								Operations calc = new Operations(num1, num2);
								double result = 0;
								switch (op)
								{
									case '*':
										result = calc.Multiply();
										break;
									case '/':
										result = calc.Divide();
										break;
									case '+':
										result = calc.Add();
										break;
									case '-':
										result = calc.Subtract();
										break;
								}
								sub = sub.Replace(numbers[i - 1] + op + numbers[i], Convert.ToString(result));
								sub = KillTwoSign(sub);
								//WriteLine(sub);
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
				//WriteLine(sub);
				string simple = BracketsToSimple(sub);
				if (tempExp.IndexOfAny(new char[] { '(', ')' }) != -1) tempExp = tempExp.Replace('(' + tempSub + ')', simple);
				else tempExp = tempExp.Replace(tempSub, simple);
				//WriteLine(tempExp);
				tempExp = KillTwoSign(tempExp);
				WriteLine("=> " + tempExp);
			} while (tempExp.Split(new char[] { '*', '/', '+', '-' }).Length > 2);
			return tempExp;
		}
	}
	class Help
	{
		public static void HelpText()
		{
			WriteLine(
				"\tКонсольный калькулятор.\n" +
				"Версия: " +
				Title + ".\n" +
				"Предназначен для вычисления выражений, содержащие основные математические операции:\n" +
				"сложение, вычитание, умножение, деление.\n" +
				"Допускается использование выражений со скобками.\n" +
				"Вычисления производятся с учетом приоритета математических операций.\n");
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Title = "crvCalc v0.01";
			bool exit = false;
			string expressionString;
			//expressionString = "-2*(-10.5*10-12)- 2*(100+5) ( 8-13)+(34.8-(72.5+5.98)(78-5)+(4-5)/4)/2";
			//expressionString = "(-125 + 25-15 )/0 + (3-18)*2-25";
			//expressionString = "5 - 4 * (4 - 3) - 6 + 5 - (3 / 2)";
			//expressionString = "5445/0";
			if (args.Length != 0)
			{
				if (args[0] == "-h" || args[0] == "-help")
				{
					Help.HelpText();
					WriteLine("\tКонсольный калькулятор.\n");
					WriteLine("Введите выражение (e-выход, h-помощь):");
					expressionString = ReadLine();
				}
				else
				{
					expressionString = args[0];
					if (args.Length > 1) for (int i = 1; i < args.Length; i++) expressionString += args[i];
					exit = false;
				}
			}
			else
			{
				WriteLine("\tКонсольный калькулятор.\n");
				WriteLine("Вычисление значения выражения.");
				WriteLine("Введите выражение (e-выход, h-помощь):");
				expressionString = ReadLine();
				if (expressionString == "e" || string.IsNullOrEmpty(expressionString)) exit = true;
				if (expressionString == "h")
				{
					Help.HelpText();
					exit = true;
				}
			}

			try
			{
				while (!exit)
				{
					ExpressionLogic expression = new ExpressionLogic(expressionString);
					WriteLine("=> " + expression.Expression);
					string tempExp = expression.Expression;
					string resultExp = ExpressionLogic.ExpressionToResult(tempExp);
					WriteLine(resultExp);
					WriteLine("Введите выражение (e-выход)");
					expressionString = ReadLine();
					if (expressionString == "e" || string.IsNullOrEmpty(expressionString)) exit = true;
				}
			}
			catch (Exception ex)
			{
				WriteLine($"{ex.Message}");

			}
			WriteLine("Press any key to continue...");
			ReadKey();
		}
	}
}
*/
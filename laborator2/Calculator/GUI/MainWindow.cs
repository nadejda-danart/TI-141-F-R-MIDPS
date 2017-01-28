using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Core;

namespace GUI
{
    public partial class MainWindow : Form
    {
        private readonly LabelConverter labelConverter = new LabelConverter();
        private readonly Calculator calculator = new Calculator();
        private bool lastOperation;

        public MainWindow()
        {
            InitializeComponent();
            labelConverter.OnChange += text => textBox.Text = text;
            calculator.OnCalculate += labelConverter.SetNumber;
            calculator.OnError += Disable;
            button1.Click += AddToLabel;
            button2.Click += AddToLabel;
            button3.Click += AddToLabel;
            button4.Click += AddToLabel;
            button5.Click += AddToLabel;
            button6.Click += AddToLabel;
            button7.Click += AddToLabel;
            button8.Click += AddToLabel;
            button9.Click += AddToLabel;
            button0.Click += AddToLabel;
            buttonDot.Click += (sender, args) => labelConverter.AddDot();
            buttonClear.Click += Clear;
            buttonAdd.Click += AddOperation;
            buttonSubtract.Click += AddOperation;
            buttonMultiply.Click += AddOperation;
            buttonDivide.Click += AddOperation;
            buttonPower.Click += AddOperation;
            buttonRoot.Click += AddOperation;
            buttonEquals.Click += Calculate;
            buttonSign.Click += ChangeSign;
            KeyPress += KeyPressed;
        }

        private void AddToLabel(object sender, EventArgs eventArgs)
        {
            labelConverter.AddDigit(((Button)sender).Text);
            lastOperation = false;
            Enable();
        }

        private void AddOperation(object sender, EventArgs eventArgs)
        {
            if (!lastOperation)
            {
                double number = labelConverter.Number;
                labelConverter.Clear();

                calculator.Add(number);
            }
            calculator.Add(((Button)sender).Text);
            lastOperation = true;
        }

        private void ChangeSign(object sender, EventArgs eventArgs)
        {
            labelConverter.SetNumber(-labelConverter.Number);
            Enable();
        }

        private void Calculate(object sender, EventArgs e)
        {
            if (calculator.CanCalculate)
            {
                double number = labelConverter.Number;
                labelConverter.Clear();

                calculator.Add(number);
                calculator.Add("");
            }
        }

        private void Clear(object sender, EventArgs eventArgs)
        {
            labelConverter.Clear();
            calculator.Clear();
            Enable();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                PerformClickForOther((char) 13);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            PerformClickForTextMatch(e.KeyChar);
            PerformClickForOther(e.KeyChar);
        }

        private void PerformClickForTextMatch(char e)
        {
            Button button = GetAll<Button>(this).FirstOrDefault(c => c.Text == e.ToString());
            if (button != null)
            {
                button.PerformClick();
            }
        }

        private void PerformClickForOther(char e)
        {
            switch (e)
            {
                case (char) 13:
                    buttonEquals.PerformClick();
                    break;
                case (char) 127:
                    buttonPower.PerformClick();
                    break;
                case '*':
                    buttonMultiply.PerformClick();
                    break;
                case '^':
                    buttonPower.PerformClick();
                    break;
            }
        }

        private IEnumerable<T> GetAll<T>(Control control) where T : Control
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(GetAll<T>)
                .Concat(controls)
                .Where(c => c.GetType() == typeof(T))
                .Cast<T>();
        }

        private void Disable()
        {
            buttonAdd.Enabled = false;
            buttonSubtract.Enabled = false;
            buttonDivide.Enabled = false;
            buttonMultiply.Enabled = false;
            buttonPower.Enabled = false;
            buttonRoot.Enabled = false;
            buttonEquals.Enabled = false;
            buttonSign.Enabled = false;
            buttonDot.Enabled = false;
        }

        private void Enable()
        {
            buttonAdd.Enabled = true;
            buttonSubtract.Enabled = true;
            buttonDivide.Enabled = true;
            buttonMultiply.Enabled = true;
            buttonPower.Enabled = true;
            buttonRoot.Enabled = true;
            buttonEquals.Enabled = true;
            buttonSign.Enabled = true;
            buttonDot.Enabled = true;
        }
    }
}

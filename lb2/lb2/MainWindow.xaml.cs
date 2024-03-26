//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Text.RegularExpressions;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using static System.Net.Mime.MediaTypeNames;

//namespace lb2
//{
//    public partial class MainWindow : Window
//    {
//        private Stack<string> history = new Stack<string>();

//        public MainWindow()
//        {
//            InitializeComponent();

//            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-US");

//            foreach (UIElement item in MainGrid.Children)
//            {
//                if (item is Button)
//                {
//                    ((Button)item).Click += Button_Click;
//                }
//            }

//            textInput.Focus();
//            textInput.TextChanged += textInput_TextChanged;
//            textInput.PreviewKeyDown += textInput_PreviewKeyDown;


//            MainGrid.Children.Remove(btnE);
//            MainGrid.Children.Remove(btnRoot);
//            MainGrid.Children.Remove(btnExp);
//            MainGrid.Children.Remove(btnLog);
//            MainGrid.Children.Remove(btnPi);
//            ChangeColumnCount(4);
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            string strItem = (string)((Button)e.OriginalSource).Content;

//            if (strItem == "▶️")
//            {
//                ((Button)e.OriginalSource).Content = "◀️";
//                Button btn = btnE;
//                Grid.SetColumn(btn, 4);
//                Grid.SetRow(btn, 3);
//                MainGrid.Children.Add(btn);

//                Button btn2 = btnRoot;
//                Grid.SetColumn(btn2, 4);
//                Grid.SetRow(btn2, 4);
//                MainGrid.Children.Add(btn2);

//                Button btn3 = btnExp;
//                Grid.SetColumn(btn3, 4);
//                Grid.SetRow(btn3, 5);
//                MainGrid.Children.Add(btn3);

//                Button btn4 = btnLog;
//                Grid.SetColumn(btn4, 4);
//                Grid.SetRow(btn4, 6);
//                MainGrid.Children.Add(btn4);

//                Button btn5 = btnPi;
//                Grid.SetColumn(btn5, 4);
//                Grid.SetRow(btn5, 7);
//                MainGrid.Children.Add(btn5);

//                ChangeColumnCount(5);
//            }
//            else if (strItem == "◀️")
//            {
//                ((Button)e.OriginalSource).Content = "▶️";
//                MainGrid.Children.Remove(btnE);
//                MainGrid.Children.Remove(btnRoot);
//                MainGrid.Children.Remove(btnExp);
//                MainGrid.Children.Remove(btnLog);
//                MainGrid.Children.Remove(btnPi);
//                ChangeColumnCount(4);
//            }
//            else if (strItem == "CE")
//            {
//                if (history.Count > 0)
//                {
//                    textInput.Text = history.Pop();
//                }
//                else
//                {
//                    textInput.Text = "";
//                }
//            }
//            else if (strItem == "C")
//            {
//                history.Clear();
//                textInput.Text = "";
//            }
//            else if (strItem == "⌦")
//            {
//                if (!string.IsNullOrEmpty(textInput.Text))
//                {
//                    textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
//                }
//            }
//            else if (strItem == "ln")
//            {
//                textInput.Text.Replace("ln", "");
//                double num;
//                if (double.TryParse(textInput.Text, out num) && num > 0)
//                {
//                    textInput.Text = Math.Log(num).ToString();
//                }
//                else
//                {
//                    textInput.Text = "Error";
//                }
//            }
//            else if (strItem == "π")
//            {
//                textInput.Text.Replace("π", "");
//                textInput.Text += Math.PI.ToString();
//            }
//            else if (strItem == "e")
//            {
//                textInput.Text.Replace("e", "");
//                textInput.Text += Math.E.ToString();
//            }
//            else if (strItem == "√")
//            {
//                textInput.Text.Replace("√", "");
//                double num;
//                if (double.TryParse(textInput.Text, out num) && num > 0)
//                {
//                    textInput.Text = Math.Sqrt(num).ToString();
//                }
//                else
//                {
//                    textInput.Text = "Error";
//                }
//            }
//            else if (strItem == "^")
//            {
//                textInput.Text += "^";
//            }
//            else if (strItem == "=")
//            {
//                try
//                {
//                    Degree(textInput.Text);

//                    string value = new DataTable().Compute(textInput.Text, null).ToString();
//                    history.Clear();
//                    history.Push(textInput.Text);
//                    textInput.Text = value;
//                }
//                catch
//                {
//                    textInput.Text = "Error";
//                }
//            }
//            else
//            {
//                if (strItem != "CE" && strItem != "C" && strItem != "⌦")
//                {
//                    textInput.Text += strItem;
//                }
//            }

//            textInput.Focus();
//        }

//        //private void textInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
//        //{
//        //    //if (!Regex.IsMatch(e.Text, "[0-9+\\-*/()]") || e.Text == ",")
//        //    //{
//        //    //    e.Handled = true;
//        //    //}

//        //    if (!Regex.IsMatch(e.Text, "[0-9+\\-*/().,]") || (e.Text == "," && !e.Text.Contains(".")))
//        //    {
//        //        e.Handled = true;
//        //    }
//        //}

//        private bool CanAppendText(string newText)
//        {
//            // Перевіряємо, чи текст додається вже містить одну крапку
//            if (newText == ".")
//            {
//                if (textInput.Text.Contains("."))
//                    return false;
//            }

//            if (textInput.Text == "" && (newText == "*" || newText == "/" || newText == "+"))
//            {
//                return false;
//            }

//            // Перевіряємо, чи текст додається не є нулем
//            if (newText == "0")
//            {
//                if (textInput.Text == "0")
//                    return false;
//            }

//            // Дозволяємо додавати текст, якщо він відповідає усім критеріям
//            return true;
//        }

//        private void textInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
//        {
//            // Перевіряємо, чи можна додати новий текст
//            if (!CanAppendText(e.Text))
//            {
//                e.Handled = true;
//            }
//            else if (!Regex.IsMatch(e.Text, "[0-9+\\-*/().,]") || (e.Text == "," && !e.Text.Contains(".")))
//            {
//                // Перевіряємо, чи вхідний текст є допустимим для калькулятора
//                e.Handled = true;
//            }
//        }

//        private void textInput_TextChanged(object sender, RoutedEventArgs e)
//        {
//            var textBox = (TextBox)sender;
//            textBox.CaretIndex = textBox.Text.Length;
//        }

//        private void textInput_PreviewKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Enter)
//            {
//                try
//                {
//                    Degree(textInput.Text);

//                    string value = new DataTable().Compute(textInput.Text, null).ToString();
//                    history.Clear();
//                    history.Push(textInput.Text);
//                    textInput.Text = value;
//                }
//                catch
//                {
//                    textInput.Text = "Error";
//                }
//            }
//        }
//        private void ChangeColumnCount(int columnCount)
//        {
//            while (MainGrid.ColumnDefinitions.Count < columnCount)
//            {
//                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
//            }

//            while (MainGrid.ColumnDefinitions.Count > columnCount)
//            {
//                MainGrid.ColumnDefinitions.RemoveAt(MainGrid.ColumnDefinitions.Count - 1);
//            }
//        }

//        private void Degree(string expression)
//        {
//            if (expression.Contains("^"))
//            {
//                string[] parts = expression.Split('^');
//                if (parts.Length == 2)
//                {
//                    double baseNum, exponent;
//                    if (double.TryParse(parts[0], out baseNum) && double.TryParse(parts[1], out exponent))
//                    {
//                        double result = Math.Pow(baseNum, exponent);
//                        history.Clear();
//                        history.Push(expression);
//                        textInput.Text = result.ToString();
//                        return;
//                    }
//                    else
//                    {
//                        textInput.Text = "Error";
//                        return;
//                    }
//                }
//                else
//                {
//                    textInput.Text = "Error";
//                    return;
//                }
//            }
//        }
//    }
//}
























using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace lb2
{
    public interface ICommand
    {
        void Execute(TextBox textBox);
    }

    public class ClearCommand : ICommand
    {
        public void Execute(TextBox textBox)
        {
            textBox.Text = "";
        }
    }

    public class DeleteCommand : ICommand
    {
        public void Execute(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }
    }

    public class ClearLastEntryCommand : ICommand
    {
        public void Execute(TextBox textBox)
        {
            int lastOperatorIndex = FindLastOperatorIndex(textBox.Text);

            if (lastOperatorIndex != -1)
            {
                textBox.Text = textBox.Text.Substring(0, lastOperatorIndex + 1);
            }
            else
            {
                textBox.Text = "";
            }
        }

        private int FindLastOperatorIndex(string text)
        {
            char[] operators = new char[] { '+', '-', '*', '/', '^'};

            int lastIndex = -1;

            foreach (char op in operators)
            {
                int index = text.LastIndexOf(op);
                if (index > lastIndex)
                {
                    lastIndex = index;
                }
            }

            return lastIndex;
        }
    }



    public partial class MainWindow : Window
    {
        private readonly ICommand _clearCommand;
        private readonly ICommand _deleteCommand;
        private readonly ICommand _clearLastEntryCommand;

        public MainWindow()
        {
            InitializeComponent();

            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            foreach (UIElement item in MainGrid.Children)
            {
                if (item is Button)
                {
                    ((Button)item).Click += Button_Click;
                }
            }

            _clearCommand = new ClearCommand();
            _deleteCommand = new DeleteCommand();
            _clearLastEntryCommand = new ClearLastEntryCommand();

            textInput.Focus();
            textInput.TextChanged += textInput_TextChanged;
            textInput.PreviewKeyDown += textInput_PreviewKeyDown;


            MainGrid.Children.Remove(btnE);
            MainGrid.Children.Remove(btnRoot);
            MainGrid.Children.Remove(btnExp);
            MainGrid.Children.Remove(btnLog);
            MainGrid.Children.Remove(btnPi);
            ChangeColumnCount(4);
        }

        private bool CanAppendText(string newText)
        {
            string updatedText = textInput.Text + newText;

            if (Regex.IsMatch(updatedText, @"\.\.") || Regex.IsMatch(updatedText, @"\+\+") || Regex.IsMatch(updatedText, @"\-\-") || Regex.IsMatch(updatedText, @"\/\/") || Regex.IsMatch(updatedText, @"\*\*") || Regex.IsMatch(updatedText, @"\^\^"))
            {
                return false;
            }

            if (!Regex.IsMatch(updatedText, @"^[^+].*"))
            {
                return false;
            }

            if (!Regex.IsMatch(updatedText, @"^[^-].*"))
            {
                return false;
            }

            if (!Regex.IsMatch(updatedText, @"^[^*].*"))
            {
                return false;
            }

            if (!Regex.IsMatch(updatedText, @"^[^/].*"))
            {
                return false;
            }

            if (newText == "0")
            {
                if (textInput.Text == "0" || textInput.Text.StartsWith("0"))
                    return false;
            }

            return true;
        }

        private void textInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!CanAppendText(e.Text))
            {
                e.Handled = true;
            }
            if (!Regex.IsMatch(e.Text, "[0-9+\\-*/().,^]") || (e.Text == "," && !e.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private void textInput_TextChanged(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.CaretIndex = textBox.Text.Length;
        }

        private void textInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CalculateExpression();
            }
        }
        private void ChangeColumnCount(int columnCount)
        {
            while (MainGrid.ColumnDefinitions.Count < columnCount)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            while (MainGrid.ColumnDefinitions.Count > columnCount)
            {
                MainGrid.ColumnDefinitions.RemoveAt(MainGrid.ColumnDefinitions.Count - 1);
            }
        }

        private void Degree(string expression)
        {
            if (expression.Contains("^"))
            {
                string[] parts = expression.Split('^');
                if (parts.Length == 2)
                {
                    double baseNum, exponent;
                    if (double.TryParse(parts[0], out baseNum) && double.TryParse(parts[1], out exponent))
                    {
                        double result = Math.Pow(baseNum, exponent);
                        textInput.Text = result.ToString();
                        return;
                    }
                    else
                    {
                        textInput.Text = "Error";
                        return;
                    }
                }
                else
                {
                    textInput.Text = "Error";
                    return;
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string strItem = (string)((Button)e.OriginalSource).Content;

            switch (strItem)
            {
                case "▶️":
                    ((Button)e.OriginalSource).Content = "◀️";
                    Button btn = btnE;
                    Grid.SetColumn(btn, 4);
                    Grid.SetRow(btn, 3);
                    MainGrid.Children.Add(btn);

                    Button btn2 = btnRoot;
                    Grid.SetColumn(btn2, 4);
                    Grid.SetRow(btn2, 4);
                    MainGrid.Children.Add(btn2);

                    Button btn3 = btnExp;
                    Grid.SetColumn(btn3, 4);
                    Grid.SetRow(btn3, 5);
                    MainGrid.Children.Add(btn3);

                    Button btn4 = btnLog;
                    Grid.SetColumn(btn4, 4);
                    Grid.SetRow(btn4, 6);
                    MainGrid.Children.Add(btn4);

                    Button btn5 = btnPi;
                    Grid.SetColumn(btn5, 4);
                    Grid.SetRow(btn5, 7);
                    MainGrid.Children.Add(btn5);

                    ChangeColumnCount(5);
                    break;
                case "◀️":
                    ((Button)e.OriginalSource).Content = "▶️";
                    MainGrid.Children.Remove(btnE);
                    MainGrid.Children.Remove(btnRoot);
                    MainGrid.Children.Remove(btnExp);
                    MainGrid.Children.Remove(btnLog);
                    MainGrid.Children.Remove(btnPi);
                    ChangeColumnCount(4);
                    break;
                case "CE":
                    _clearLastEntryCommand.Execute(textInput);
                    break;
                case "C":
                    _clearCommand.Execute(textInput);
                    break;
                case "⌦":
                    _deleteCommand.Execute(textInput);
                    break;
                case "=":
                    CalculateExpression();
                    break;
                case "ln":
                    textInput.Text.Replace("ln", "");
                    double num;
                    if (double.TryParse(textInput.Text, out num) && num > 0)
                    {
                        textInput.Text = Math.Log(num).ToString();
                    }
                    else
                    {
                        textInput.Text = "Error";
                    }
                    break;
                case "π":
                    textInput.Text.Replace("π", "");
                    textInput.Text += Math.PI.ToString();
                    break;
                case "e":
                    textInput.Text.Replace("e", "");
                    textInput.Text += Math.E.ToString();
                    break;
                case "√":
                    textInput.Text.Replace("√", "");
                    double num1;
                    if (double.TryParse(textInput.Text, out num1) && num1 > 0)
                    {
                        textInput.Text = Math.Sqrt(num1).ToString();
                    }
                    else
                    {
                        textInput.Text = "Error";
                    }
                    break;
                case "0":
                    if (!textInput.Text.StartsWith("0"))
                    {
                        textInput.Text += strItem;
                    }
                    break;
                case "00":
                    if (!textInput.Text.StartsWith("0"))
                    {
                        textInput.Text += strItem;
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                    }
                    break;
                case ".":
                    textInput.Text += strItem;
                    if (Regex.IsMatch(textInput.Text, @"\.\."))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    break;
                case "+":
                    textInput.Text += strItem;
                    if (textInput.Text.StartsWith("+"))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    if (Regex.IsMatch(textInput.Text, @"\+\+"))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    break;
                case "*":
                    textInput.Text += strItem;
                    if (textInput.Text.StartsWith("*"))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    if (Regex.IsMatch(textInput.Text, @"\*\*"))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    break;
                case "/":
                    textInput.Text += strItem;
                    if (textInput.Text.StartsWith("/"))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    if (Regex.IsMatch(textInput.Text, @"\/\/"))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    break;
                case "-":
                    textInput.Text += strItem;
                    if (Regex.IsMatch(textInput.Text, @"\-\-"))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    break;
                case "^":
                    textInput.Text += strItem;
                    if (Regex.IsMatch(textInput.Text, @"\^\^"))
                    {
                        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                        break;
                    }
                    break;
                default:
                    textInput.Text += strItem;
                    break;
            }

            textInput.Focus();
        }

        private void CalculateExpression() 
        {
            try
            {
                Degree(textInput.Text);

                string value = new DataTable().Compute(textInput.Text, null).ToString();
                textInput.Text = value;
            }
            catch
            {
                textInput.Text = "Error";
            }
        }
    }
}





































































//using System;
//using System.Data;
//using System.Text.RegularExpressions;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;

//namespace lb2
//{
//    public partial class MainWindow : Window
//    {
//        private ICommand command;

//        public MainWindow()
//        {
//            InitializeComponent();

//            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-US");

//            foreach (UIElement item in MainGrid.Children)
//            {
//                if (item is Button)
//                {
//                    ((Button)item).Click += Button_Click;
//                }
//            }

//            textInput.Focus();
//            textInput.TextChanged += textInput_TextChanged;
//            textInput.PreviewKeyDown += textInput_PreviewKeyDown;

//            InitializeCommand();

//            MainGrid.Children.Remove(btnE);
//            MainGrid.Children.Remove(btnRoot);
//            MainGrid.Children.Remove(btnExp);
//            MainGrid.Children.Remove(btnLog);
//            MainGrid.Children.Remove(btnPi);
//            ChangeColumnCount(4);
//        }

//        private void InitializeCommand()
//        {
//            command = new CalculatorCommand(textInput);
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            Button button = (Button)sender;
//            string strItem = (string)button.Content;

//            if (strItem != null)
//            {
//                command.Execute(strItem);
//            }

//            textInput.Focus();
//        }


//        private bool CanAppendText(string newText)
//        {
//            // Перевіряємо, чи текст додається вже містить одну крапку
//            if (newText == ".")
//            {
//                if (textInput.Text.Contains("."))
//                    return false;
//            }

//            if (textInput.Text == "" && (newText == "*" || newText == "/" || newText == "+"))
//            {
//                return false;
//            }

//            // Перевіряємо, чи текст додається не є нулем
//            if (newText == "0")
//            {
//                if (textInput.Text == "0")
//                    return false;
//            }

//            // Дозволяємо додавати текст, якщо він відповідає усім критеріям
//            return true;
//        }

//        private void textInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
//        {
//            // Перевіряємо, чи можна додати новий текст
//            if (!CanAppendText(e.Text))
//            {
//                e.Handled = true;
//            }
//            else if (!Regex.IsMatch(e.Text, "[0-9+\\-*/().,]") || (e.Text == "," && !e.Text.Contains(".")))
//            {
//                // Перевіряємо, чи вхідний текст є допустимим для калькулятора
//                e.Handled = true;
//            }
//        }

//        private void textInput_TextChanged(object sender, RoutedEventArgs e)
//        {
//            var textBox = (TextBox)sender;
//            textBox.CaretIndex = textBox.Text.Length;
//        }

//        private void textInput_PreviewKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Enter)
//            {
//                command.Execute("=");
//            }
//        }


//        private void ChangeColumnCount(int columnCount)
//        {
//            while (MainGrid.ColumnDefinitions.Count < columnCount)
//            {
//                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
//            }

//            while (MainGrid.ColumnDefinitions.Count > columnCount)
//            {
//                MainGrid.ColumnDefinitions.RemoveAt(MainGrid.ColumnDefinitions.Count - 1);
//            }
//        }

//        private class CalculatorCommand : ICommand
//        {
//            private TextBox textInput;

//            public CalculatorCommand(TextBox textBox)
//            {
//                textInput = textBox;
//            }

//            public event EventHandler CanExecuteChanged;

//            public bool CanExecute(object parameter)
//            {
//                return true; // Can add conditions here if needed
//            }

//            public void Execute(object parameter)
//            {
//                string strItem = (string)parameter;

//                // Implement command execution based on parameter
//                switch (strItem)
//                {
//                    case "CE":
//                        if (!string.IsNullOrEmpty(textInput.Text))
//                        {
//                            textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
//                        }
//                        break;
//                    case "C":
//                        textInput.Text = "";
//                        break;
//                    case "⌦":
//                        if (!string.IsNullOrEmpty(textInput.Text))
//                        {
//                            textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
//                        }
//                        break;
//                    case "=":
//                        try
//                        {
//                            Degree(textInput.Text);

//                            string value = new DataTable().Compute(textInput.Text, null).ToString();
//                            textInput.Text = value;
//                        }
//                        catch
//                        {
//                            textInput.Text = "Error";
//                        }
//                        break;
//                    // Add other cases as needed
//                    default:
//                        textInput.Text += strItem;
//                        break;
//                }
//            }


//            private void Degree(string expression)
//            {
//                if (expression.Contains("^"))
//                {
//                    string[] parts = expression.Split('^');
//                    if (parts.Length == 2)
//                    {
//                        double baseNum, exponent;
//                        if (double.TryParse(parts[0], out baseNum) && double.TryParse(parts[1], out exponent))
//                        {
//                            double result = Math.Pow(baseNum, exponent);
//                            textInput.Text = result.ToString();
//                            return;
//                        }
//                        else
//                        {
//                            textInput.Text = "Error";
//                            return;
//                        }
//                    }
//                    else
//                    {
//                        textInput.Text = "Error";
//                        return;
//                    }
//                }
//            }
//        }
//    }
//}





















































//using System;
//using System.Collections.Generic;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Data;
//using System.Data.Common;
//using System.Text.RegularExpressions;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using static System.Net.Mime.MediaTypeNames;

//namespace lb2
//{
//    public partial class MainWindow : Window
//    {
//        private CommandManager commandManager = new CommandManager();

//        private void textInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
//        {
//            if (!Regex.IsMatch(e.Text, "[0-9+\\-*/()]") || e.Text == ",")
//            {
//                e.Handled = true;
//            }

//            if (!Regex.IsMatch(e.Text, "[0-9+\\-*/().,]") || (e.Text == "," && !e.Text.Contains(".")))
//            {
//                e.Handled = true;
//            }
//        }


//        public MainWindow()
//        {
//            InitializeComponent();

//            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-US");

//            foreach (UIElement item in MainGrid.Children)
//            {
//                if (item is Button)
//                {
//                    ((Button)item).Click += Button_Click;
//                }
//            }

//            textInput.Focus();
//            textInput.TextChanged += textInput_TextChanged;
//            textInput.PreviewKeyDown += textInput_PreviewKeyDown;
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            string strItem = (string)((Button)e.OriginalSource).Content;

//            // Handling Undo separately
//            if (strItem == "Undo")
//            {
//                commandManager.Undo();
//            }
//            else if (strItem != "CE" && strItem != "C" && strItem != "⌦" && strItem != "=")
//            {
//                // For all buttons other than control buttons, add their text to the input
//                ICommand addTextCommand = new AddTextCommand(textInput, strItem);
//                commandManager.ExecuteCommand(addTextCommand);
//            }
//            else
//            {
//                // Handling control buttons (CE, C, ⌦, =) separately as needed
//                switch (strItem)
//                {
//                    case "CE":
//                        if (commandManager.CanUndo())
//                        {
//                            commandManager.Undo();
//                        }
//                        break;
//                    case "C":
//                        ICommand clearCommand = new ClearCommand(textInput);
//                        commandManager.ExecuteCommand(clearCommand);
//                        break;
//                    case "⌦":
//                        ICommand backspaceCommand = new BackspaceCommand(textInput);
//                        commandManager.ExecuteCommand(backspaceCommand);
//                        break;
//                    case "=":
//                        // Assume Calculate method is implemented to evaluate expressions
//                        Calculate();
//                        break;
//                }
//            }

//            textInput.Focus();
//        }

//        private void Calculate()
//        {
//            // Implement the calculation logic here, possibly using another command or directly in this method

//            try
//            {
//                Degree(textInput.Text);

//                string value = new DataTable().Compute(textInput.Text, null).ToString();
//                history.Clear();
//                history.Push(textInput.Text);
//                textInput.Text = value;
//            }
//            catch
//            {
//                textInput.Text = "Error";
//            }
//        }

//        private void textInput_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            var textBox = (TextBox)sender;
//            textBox.CaretIndex = textBox.Text.Length; // Ensure the caret is at the end of the input
//        }

//        private void textInput_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
//        {
//            if (e.Key == System.Windows.Input.Key.Enter)
//            {
//                Calculate();
//            }
//        }
//    }

//    public interface ICommand
//    {
//        void Execute();
//        void Unexecute();
//    }

//    public class AddTextCommand : ICommand
//    {
//        private TextBox _textInput;
//        private string _textToAdd;
//        private string _previousText;

//        public AddTextCommand(TextBox textInput, string textToAdd)
//        {
//            _textInput = textInput;
//            _textToAdd = textToAdd;
//            _previousText = textInput.Text;
//        }

//        public void Execute()
//        {
//            _textInput.Text += _textToAdd;
//        }

//        public void Unexecute()
//        {
//            _textInput.Text = _previousText;
//        }
//    }

//    public class ClearCommand : ICommand
//    {
//        private TextBox _textInput;
//        private string _previousText;

//        public ClearCommand(TextBox textInput)
//        {
//            _textInput = textInput;
//            _previousText = textInput.Text;
//        }

//        public void Execute()
//        {
//            _textInput.Text = "";
//        }

//        public void Unexecute()
//        {
//            _textInput.Text = _previousText;
//        }
//    }

//    public class BackspaceCommand : ICommand
//    {
//        private TextBox _textInput;
//        private string _previousText;

//        public BackspaceCommand(TextBox textInput)
//        {
//            _textInput = textInput;
//            _previousText = textInput.Text;
//        }

//        public void Execute()
//        {
//            if (_textInput.Text.Length > 0)
//            {
//                _textInput.Text = _textInput.Text.Substring(0, _textInput.Text.Length - 1);
//            }
//        }

//        public void Unexecute()
//        {
//            _textInput.Text = _previousText;
//        }
//    }

//    public class CommandManager
//    {
//        private Stack<ICommand> _commands = new Stack<ICommand>();

//        public void ExecuteCommand(ICommand command)
//        {
//            command.Execute();
//            _commands.Push(command);
//        }

//        public void Undo()
//        {
//            if (_commands.Count > 0)
//            {
//                var command = _commands.Pop();
//                command.Unexecute();
//            }
//        }

//        public bool CanUndo()
//        {
//            return _commands.Count > 0;
//        }
//    }
//}



























//private void Button_Click(object sender, RoutedEventArgs e)
//{
//    string strItem = (string)((Button)e.OriginalSource).Content;

//    if (strItem == "CE")
//    {
//        if (history.Count > 0)
//        {
//            textInput.Text = history.Pop(); // Відновлення останнього стану з історії
//        }
//        else
//        {
//            textInput.Text = ""; // Якщо історія пуста, просто очищаємо текст
//        }
//        resultLabel.Text = ""; // Очищення результату
//    }
//    else if (strItem == "C")
//    {
//        history.Clear(); // Очищення історії
//        textInput.Text = "";
//        resultLabel.Text = "";
//    }
//    else if (strItem == "⌦")
//    {
//        if (!string.IsNullOrEmpty(textInput.Text))
//        {
//            textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
//        }
//    }
//    else if (strItem == "=")
//    {
//        try
//        {
//            string value = new DataTable().Compute(textInput.Text, null).ToString();
//            history.Clear(); // Очищення історії після обчислення
//            history.Push(textInput.Text); // Додаємо поточний вираз в історію перед обчисленням
//            textInput.Text = value; // Виводимо результат в поле вводу
//            resultLabel.Text = value;
//        }
//        catch
//        {
//            resultLabel.Text = "Error";
//        }
//    }
//    else
//    {
//        if (strItem != "CE" && strItem != "C" && strItem != "⌦")
//        {
//            textInput.Text += strItem; // Додаємо натиснутий символ до введеного виразу
//        }
//    }

//    textInput.Focus();
//}














//private void Button_Click(object sender, RoutedEventArgs e)
//{
//    string strItem = (string)((Button)e.OriginalSource).Content;

//    if (strItem == "C")
//    {
//        textInput.Text = "";
//        resultLabel.Text = "";
//    }
//    else if (strItem == "⌦")
//    {
//        textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
//    }
//    else if(strItem == "=")
//    {
//        string value = new DataTable().Compute(textInput.Text, null).ToString();
//        resultLabel.Text = value;
//    }
//    else
//    {
//        textInput.Text += strItem;
//    }
//}

//private void Button_Click(object sender, RoutedEventArgs e)
//{
//    string strItem = (string)((Button)e.OriginalSource).Content;

//    if (strItem == "CE")
//    {
//        if (history.Count > 0)
//        {
//            textInput.Text = history.Pop(); // Відновлення останнього стану з історії
//        }
//        else
//        {
//            textInput.Text = ""; // Якщо історія пуста, просто очищаємо текст
//        }
//        resultLabel.Text = ""; // Очищення результату
//    }
//    else if (strItem == "C")
//    {
//        history.Clear(); // Очищення історії
//        textInput.Text = "";
//        resultLabel.Text = "";
//    }
//    else if (strItem == "⌦")
//    {
//        if (!string.IsNullOrEmpty(textInput.Text))
//        {
//            textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
//        }
//    }
//    else if (strItem == "=")
//    {
//        try
//        {
//            string value = new DataTable().Compute(textInput.Text, null).ToString();
//            history.Push(textInput.Text); // Додаємо поточний стан в історію перед обчисленням
//            resultLabel.Text = value;
//        }
//        catch
//        {
//            resultLabel.Text = "Error";
//        }
//    }
//    else
//    {
//        if (strItem != "=" && strItem != "CE" && strItem != "C" && strItem != "⌦")
//        {
//            history.Push(textInput.Text); // Збереження поточного стану перед додаванням нового символу
//        }
//        textInput.Text += strItem;
//    }


//    textInput.TextChanged += textInput_TextChanged;
//    textInput.Focus();


//}

























//private void textInput_PreviewKeyDown(object sender, KeyEventArgs e)
//{
//    // Перевіряємо, чи натиснута клавіша Enter
//    if (e.Key == Key.Enter)
//    {
//        Button_Click(btnEqual, new RoutedEventArgs(Button.ClickEvent)); // Викликаємо Button_Click при натисканні Enter
//    }
//}





//private void LogButton_Click(object sender, RoutedEventArgs e)
//{
//    double num;
//    if (double.TryParse(textInput.Text, out num) && num > 0)
//    {
//        textInput.Text = Math.Log10(num).ToString();
//    }
//    else
//    {
//        textInput.Text = "Error";
//    }
//}









//private void EButton_Click(object sender, RoutedEventArgs e)
//{
//    textInput.Text += Math.E.ToString();
//}

//private void RootButton_Click(object sender, RoutedEventArgs e)
//{
//    double num;
//    if (double.TryParse(textInput.Text, out num) && num > 0)
//    {
//        textInput.Text = Math.Sqrt(num).ToString();
//    }
//    else
//    {
//        textInput.Text = "Error";
//    }
//}

//private void PowerButton_Click(object sender, RoutedEventArgs e)
//{
//    textInput.Text += "";
//}

//private void LogButton_Click(object sender, RoutedEventArgs e)
//{
//    double num;
//    if (double.TryParse(textInput.Text, out num) && num > 0)
//    {
//        textInput.Text = Math.Log(num).ToString();
//    }
//    else
//    {
//        textInput.Text = "Error";
//    }
//}

//private void PiButton_Click(object sender, RoutedEventArgs e)
//{
//    textInput.Text += Math.PI.ToString();
//}











//else if (strItem == "^")
//{
//    textInput.Text += "^";
//}
//else if (textInput.Text.Contains("^"))
//{
//    int index = textInput.Text.IndexOf('^');
//    double num;
//    double degr;


//    string number = textInput.Text.Substring(0, index);
//    string degree = textInput.Text.Substring(index + 1);


//    if (double.TryParse(number, out num) && double.TryParse(degree, out degr))
//    {
//        textInput.Text = Math.Pow(num, degr).ToString();
//    }
//    else
//    {
//        textInput.Text = "Error";
//    }
//}
//else if (strItem == "=")
//{
//    try
//    {
//        string value = new DataTable().Compute(textInput.Text, null).ToString();
//        history.Clear();
//        history.Push(textInput.Text);
//        textInput.Text = value;

//    }
//    catch
//    {
//        textInput.Text = "Error";
//    }
//}


















//string expression = textInput.Text;
//Degree(expression);
//if (expression.Contains("^"))
//{
//    string[] parts = expression.Split('^');
//    if (parts.Length == 2)
//    {
//        double baseNum, exponent;
//        if (double.TryParse(parts[0], out baseNum) && double.TryParse(parts[1], out exponent))
//        {
//            double result = Math.Pow(baseNum, exponent);
//            history.Clear();
//            history.Push(expression);
//            textInput.Text = result.ToString();
//            return;
//        }
//        else
//        {
//            textInput.Text = "Error";
//            return;
//        }
//    }
//    else
//    {
//        textInput.Text = "Error";
//        return;
//    }
//}



//string value = new DataTable().Compute(expression, null).ToString();
//history.Clear();
//history.Push(expression);
//textInput.Text = value;
//string expression = textInput.Text;
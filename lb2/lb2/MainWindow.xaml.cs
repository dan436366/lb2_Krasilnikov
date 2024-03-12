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
    public partial class MainWindow : Window
    {
        private Stack<string> history = new Stack<string>();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string strItem = (string)((Button)e.OriginalSource).Content;

            if (strItem == "≡")
            {
                ((Button)e.OriginalSource).Content = "<";

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
            }
            else if (strItem == "<")
            {
                ((Button)e.OriginalSource).Content = "≡";
                MainGrid.Children.Remove(btnE);
                MainGrid.Children.Remove(btnRoot);
                MainGrid.Children.Remove(btnExp);
                MainGrid.Children.Remove(btnLog);
                MainGrid.Children.Remove(btnPi);
                ChangeColumnCount(4);
            }
            else if (strItem == "CE")
            {
                if (history.Count > 0)
                {
                    textInput.Text = history.Pop();
                }
                else
                {
                    textInput.Text = "";
                }
            }
            else if (strItem == "C")
            {
                history.Clear();
                textInput.Text = "";
            }
            else if (strItem == "⌦")
            {
                if (!string.IsNullOrEmpty(textInput.Text))
                {
                    textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
                }
            }
            else if (strItem == "ln")
            {
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
            }
            else if (strItem == "π")
            {
                textInput.Text.Replace("π", "");
                textInput.Text += Math.PI.ToString();
            }
            else if (strItem == "e")
            {
                textInput.Text.Replace("e", "");
                textInput.Text += Math.E.ToString();
            }
            else if (strItem == "√")
            {
                textInput.Text.Replace("√", "");
                double num;
                if (double.TryParse(textInput.Text, out num) && num > 0)
                {
                    textInput.Text = Math.Sqrt(num).ToString();
                }
                else
                {
                    textInput.Text = "Error";
                }
            }
            else if (strItem == "^")
            {
                    textInput.Text += "^";
            }
            else if (strItem == "=")
            {
                try
                {
                    string expression = textInput.Text;
                    if (expression.Contains("^"))
                    {
                        string[] parts = expression.Split('^');
                        if (parts.Length == 2)
                        {
                            double baseNum, exponent;
                            if (double.TryParse(parts[0], out baseNum) && double.TryParse(parts[1], out exponent))
                            {
                                double result = Math.Pow(baseNum, exponent);
                                history.Clear();
                                history.Push(expression);
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

                    string value = new DataTable().Compute(expression, null).ToString();
                    history.Clear();
                    history.Push(expression);
                    textInput.Text = value;
                }
                catch
                {
                    textInput.Text = "Error";
                }
            }
            else
            {
                if (strItem != "CE" && strItem != "C" && strItem != "⌦")
                {
                    textInput.Text += strItem;
                }
            }

            textInput.Focus();
        }

        private void textInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "[0-9+\\-*/()]") || e.Text == ",")
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
                try
                {
                    string value = new DataTable().Compute(textInput.Text, null).ToString();
                    history.Clear();
                    history.Push(textInput.Text);
                    textInput.Text = value;
                }
                catch
                {
                    textInput.Text = "Error";
                }
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
    }
}

































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
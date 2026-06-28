using System;
using System.Windows.Forms;

namespace CurrencyConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Настройки формы
            this.Text = "💱 Конвертер валют";
            this.Size = new System.Drawing.Size(450, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Метка "Сумма:"
            Label lblAmount = new Label()
            {
                Text = "Сумма:",
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(30, 30),
                AutoSize = true
            };

            // Поле для ввода суммы
            TextBox txtAmount = new TextBox()
            {
                Name = "txtAmount",
                Location = new System.Drawing.Point(120, 25),
                Size = new System.Drawing.Size(150, 20),
                Font = new System.Drawing.Font("Arial", 10)
            };

            // Метка "Из валюты:"
            Label lblFrom = new Label()
            {
                Text = "Из валюты:",
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(30, 70),
                AutoSize = true
            };

            // Выпадающий список "Из валюты"
            ComboBox cmbFrom = new ComboBox()
            {
                Name = "cmbFrom",
                Location = new System.Drawing.Point(120, 65),
                Size = new System.Drawing.Size(120, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFrom.Items.AddRange(new object[] { "RUB", "USD", "EUR", "GBP", "CNY" });
            cmbFrom.SelectedIndex = 0;

            // Метка "В валюту:"
            Label lblTo = new Label()
            {
                Text = "В валюту:",
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(30, 110),
                AutoSize = true
            };

            // Выпадающий список "В валюту"
            ComboBox cmbTo = new ComboBox()
            {
                Name = "cmbTo",
                Location = new System.Drawing.Point(120, 105),
                Size = new System.Drawing.Size(120, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTo.Items.AddRange(new object[] { "RUB", "USD", "EUR", "GBP", "CNY" });
            cmbTo.SelectedIndex = 1;

            // Кнопка "Конвертировать"
            Button btnConvert = new Button()
            {
                Text = "🔄 Конвертировать",
                Location = new System.Drawing.Point(120, 145),
                Size = new System.Drawing.Size(150, 35),
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.LightBlue
            };
            btnConvert.Click += (sender, e) =>
            {
                MessageBox.Show("Пока ничего не работает :)");
            };

            // Метка для результата
            Label lblResult = new Label()
            {
                Name = "lblResult",
                Text = "Результат: --",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DarkGreen,
                Location = new System.Drawing.Point(30, 200),
                AutoSize = true
            };

            // Добавляем все элементы на форму
            this.Controls.Add(lblAmount);
            this.Controls.Add(txtAmount);
            this.Controls.Add(lblFrom);
            this.Controls.Add(cmbFrom);
            this.Controls.Add(lblTo);
            this.Controls.Add(cmbTo);
            this.Controls.Add(btnConvert);
            this.Controls.Add(lblResult);
        }
    }
}
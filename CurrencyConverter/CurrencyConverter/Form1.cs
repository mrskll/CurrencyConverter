using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
    public partial class Form1 : Form
    {
        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();
        private readonly HttpClient httpClient = new HttpClient();
        private bool isRatesLoaded = false;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
            LoadExchangeRates();
        }

        private void InitializeUI()
        {
            this.Text = "💱 Конвертер валют";
            this.Size = new System.Drawing.Size(450, 350);
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

            // Кнопка "Конвертировать" с ПОЛНОЙ ЛОГИКОЙ
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
                ConvertCurrency(txtAmount, cmbFrom, cmbTo, lblResult);
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

        // 📡 Загрузка курсов из API
        private async void LoadExchangeRates()
        {
            try
            {
                string url = "https://api.exchangerate-api.com/v4/latest/RUB";
                HttpResponseMessage response = await httpClient.GetAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                JObject data = JObject.Parse(jsonResponse);
                JObject rates = (JObject)data["rates"];

                exchangeRates.Clear();
                foreach (var rate in rates)
                {
                    exchangeRates[rate.Key] = (decimal)rate.Value;
                }

                isRatesLoaded = true;
                MessageBox.Show("✅ Курсы валют успешно загружены!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка загрузки курсов: {ex.Message}", "Ошибка");
            }
        }

        // 🧮 НОВЫЙ МЕТОД: логика конвертации
        private void ConvertCurrency(TextBox txtAmount, ComboBox cmbFrom, ComboBox cmbTo, Label lblResult)
        {
            // Проверка: загружены ли курсы?
            if (!isRatesLoaded || exchangeRates.Count == 0)
            {
                lblResult.Text = "⏳ Подождите, курсы загружаются...";
                lblResult.ForeColor = System.Drawing.Color.Orange;
                return;
            }

            // Проверка: введено ли число?
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                lblResult.Text = "❌ Ошибка: введите число!";
                lblResult.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Проверка: существуют ли такие валюты?
            string fromCurrency = cmbFrom.SelectedItem.ToString();
            string toCurrency = cmbTo.SelectedItem.ToString();

            if (!exchangeRates.ContainsKey(fromCurrency) || !exchangeRates.ContainsKey(toCurrency))
            {
                lblResult.Text = "❌ Ошибка: валюта не найдена";
                lblResult.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Конвертация
            decimal fromRate = exchangeRates[fromCurrency];
            decimal toRate = exchangeRates[toCurrency];
            decimal result = amount / fromRate * toRate;

            // Вывод результата
            lblResult.Text = $"💵 Результат: {result:F2} {toCurrency}";
            lblResult.ForeColor = System.Drawing.Color.DarkGreen;
        }
    }
}
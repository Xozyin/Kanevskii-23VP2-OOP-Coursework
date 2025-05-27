using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;


namespace OOP_Curs_Kanevskii_23VP2
{
    /// <summary>
    /// Главная форма приложения для работы с базой данных товаров
    /// </summary>
    public partial class MainForm : Form
    {
        private DatabaseContext _dbContext; // Контекст базы данных

        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeMenu();
            // Инициализация подключения к SQLite базе данных
            _dbContext = new DatabaseContext("Data Source=products.db");
            _dbContext.InitializeDatabase(); // Создание таблиц, если их нет
            LoadData(); // Первоначальная загрузка данных
        }

        /// <summary>
        /// Обработчик ввода для текстовых полей, разрешающий только цифры и точку
        /// </summary>
        private void txtBoxWithoutCharacters_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем цифры, точку и управляющие клавиши (например, Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод
            }

            // Запрещаем ввод более одной точки
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Обработчик изменения выбранной строки в DataGridView
        /// </summary>
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                // Заполняем поля формы данными из выбранной строки
                textBoxName.Text = selectedRow.Cells["dataGridViewTextBoxColumn2"].Value.ToString();
                textBoxMass.Text = selectedRow.Cells["dataGridViewTextBoxColumn3"].Value.ToString();
                textBoxPrice.Text = selectedRow.Cells["dataGridViewTextBoxColumn4"].Value.ToString();
                numericUpDownAmount.Value = Convert.ToInt32(selectedRow.Cells["dataGridViewTextBoxColumn5"].Value);
            }
        }

        /// <summary>
        /// Инициализация состояния пунктов меню
        /// </summary>
        private void InitializeMenu()
        {
            deleteDatabaseToolStripMenuItem.Enabled = false; // Удаление недоступно
            openDatabaseToolStripMenuItem.Enabled = true; // Открытие доступно
            createDatabaseToolStripMenuItem.Enabled = true; // Создание доступно
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void ClientForm_Load(object sender, EventArgs e)
        {
            comboBoxFilter.SelectedIndex = 0; // Устанавливаем фильтр по умолчанию

            // Загружаем данные только если база данных открыта
            if (_dbContext != null)
            {
                LoadData();
            }
        }

        /// <summary>
        /// Загрузка данных из базы в DataGridView
        /// </summary>
        private void LoadData()
        {
            if (_dbContext == null || _dbContext.CachedProducts == null)
            {
                dataGridView.Rows.Clear();
                return;
            }

            try
            {
                dataGridView.Rows.Clear();
                foreach (var product in _dbContext.CachedProducts)
                {
                    // Форматируем числа для корректного отображения
                    string mass = product.Mass.ToString("F3", CultureInfo.InvariantCulture);
                    string price = product.Price.ToString("F2", CultureInfo.InvariantCulture);
                    string amount = product.Amount.ToString(CultureInfo.InvariantCulture);

                    dataGridView.Rows.Add(product.Id, product.Name, mass, price, amount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки добавления нового товара
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Проверка заполнения обязательных полей
            if (string.IsNullOrEmpty(textBoxMass.Text) || string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Создаем новый товар на основе введенных данных
                var product = new Product
                {
                    Name = textBoxName.Text,
                    Mass = double.Parse(textBoxMass.Text, CultureInfo.InvariantCulture),
                    Price = double.Parse(textBoxPrice.Text, CultureInfo.InvariantCulture),
                    Amount = (int)numericUpDownAmount.Value
                };

                _dbContext.AddProduct(product); // Добавляем в базу данных
                LoadData(); // Обновляем отображение
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный формат числа. Используйте точку как разделитель.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Редактировать" - обновляет выбранный товар в базе данных
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбрана строка для редактирования
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                try
                {
                    // Создаем объект товара с обновленными данными из формы
                    var product = new Product
                    {
                        Id = (int)selectedRow.Cells[0].Value, // Берем ID из выбранной строки
                        Name = textBoxName.Text,
                        Mass = double.Parse(textBoxMass.Text, CultureInfo.InvariantCulture),
                        Price = double.Parse(textBoxPrice.Text, CultureInfo.InvariantCulture),
                        Amount = (int)numericUpDownAmount.Value
                    };

                    _dbContext.UpdateProduct(product); // Обновляем товар в базе
                    LoadData(); // Перезагружаем данные для отображения
                }
                catch (FormatException)
                {
                    MessageBox.Show("Некорректный формат числа. Используйте точку как разделитель.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при редактировании записи: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить" - удаляет выбранный товар из базы данных
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбрана строка для удаления
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];

                // Проверяем, что это не новая пустая строка
                if (selectedRow.IsNewRow)
                {
                    MessageBox.Show("Выберите строку для удаления.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем ID товара для удаления
                int id = (int)selectedRow.Cells[0].Value;
                _dbContext.DeleteProduct(id); // Удаляем из базы
                LoadData(); // Обновляем отображение
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Фильтровать" - фильтрует товары по выбранному критерию
        /// </summary>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            string filterValue = txtFilterValue.Text; // Значение для фильтрации
            string filterColumn = comboBoxFilter.SelectedItem.ToString(); // Столбец для фильтрации

            List<Product> products = new List<Product>();

            // Применяем фильтр в зависимости от выбранного столбца
            switch (filterColumn)
            {
                case "ID":
                    products = _dbContext.CachedProducts.Where(f => f.Id.ToString().Contains(filterValue)).ToList();
                    break;
                case "Название":
                    products = _dbContext.CachedProducts.Where(f => f.Name.Contains(filterValue, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Масса шт (кг)":
                    products = _dbContext.CachedProducts.Where(f => f.Mass.ToString().Contains(filterValue)).ToList();
                    break;
                case "Цена шт (руб)":
                    products = _dbContext.CachedProducts.Where(f => f.Price.ToString().Contains(filterValue)).ToList();
                    break;
                case "Всего шт":
                    products = _dbContext.CachedProducts.Where(f => f.Amount.ToString().Contains(filterValue)).ToList();
                    break;
            }

            // Обрабатываем случаи, когда ничего не найдено
            if (products.Count == 0)
            {
                MessageBox.Show("Совпадений не найдено.",
                    "Результат поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Отображаем отфильтрованные данные
                dataGridView.Rows.Clear();
                foreach (var product in products)
                {
                    dataGridView.Rows.Add(product.Id, product.Name,
                        product.Mass.ToString("F3", CultureInfo.InvariantCulture),
                        product.Price.ToString("F2", CultureInfo.InvariantCulture),
                        product.Amount);
                }

                // Показываем количество найденных записей
                textBoxFilter.Visible = true;
                labelFilter.Visible = true;
                textBoxFilter.Text = products.Count.ToString();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить фильтр" - сбрасывает фильтрацию
        /// </summary>
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFilterValue.Clear(); // Очищаем поле фильтра
            textBoxFilter.Text = "0"; // Сбрасываем счетчик
            textBoxFilter.Visible = false; // Скрываем элементы фильтра
            labelFilter.Visible = false;
            LoadData(); // Загружаем полный список товаров
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Сортировать" - сортирует данные по выбранному столбцу
        /// </summary>
        private void btnSort_Click(object sender, EventArgs e)
        {
            // Проверяем, что база данных подключена
            if (_dbContext == null)
            {
                MessageBox.Show("База данных не открыта.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sortColumn = comboBoxFilter.SelectedItem.ToString(); // Столбец для сортировки
            string sortOrder = comboBoxSortOrder.SelectedItem?.ToString(); // Направление сортировки

            var ProductList = _dbContext.CachedProducts;

            // Применяем сортировку в зависимости от выбранного столбца и направления
            switch (sortColumn)
            {
                case "ID":
                    ProductList = sortOrder == "По возрастанию"
                        ? ProductList.OrderBy(f => f.Id).ToList()
                        : ProductList.OrderByDescending(f => f.Id).ToList();
                    break;

                case "Название":
                    ProductList = sortOrder == "От А до Я"
                        ? ProductList.OrderBy(f => f.Name).ToList()
                        : ProductList.OrderByDescending(f => f.Name).ToList();
                    break;

                case "Масса шт (кг)":
                    ProductList = sortOrder == "По возрастанию"
                        ? ProductList.OrderBy(f => f.Mass).ToList()
                        : ProductList.OrderByDescending(f => f.Mass).ToList();
                    break;

                case "Цена шт (руб)":
                    ProductList = sortOrder == "По возрастанию"
                        ? ProductList.OrderBy(f => f.Price).ToList()
                        : ProductList.OrderByDescending(f => f.Price).ToList();
                    break;
                case "Всего шт":
                    ProductList = sortOrder == "По возрастанию"
                        ? ProductList.OrderBy(f => f.Amount).ToList()
                        : ProductList.OrderByDescending(f => f.Amount).ToList();
                    break;

                default:
                    MessageBox.Show("Неверный столбец для сортировки.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            // Обновляем DataGridView отсортированными данными
            dataGridView.Rows.Clear();
            foreach (var product in ProductList)
            {
                dataGridView.Rows.Add(product.Id, product.Name,
                    product.Mass.ToString("F3", CultureInfo.InvariantCulture),
                    product.Price.ToString("F2", CultureInfo.InvariantCulture),
                    product.Amount);
            }
        }

        /// <summary>
        /// Обработчик изменения выбранного элемента в комбобоксе фильтра
        /// Обновляет варианты сортировки в зависимости от выбранного столбца
        /// </summary>
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Для столбца "Название" используем текстовые варианты сортировки
            if (comboBoxFilter.SelectedItem.ToString() == "Название")
            {
                comboBoxSortOrder.Items.Clear();
                comboBoxSortOrder.Items.AddRange(new object[] { "От А до Я", "От Я до А" });
            }
            else // Для числовых столбцов используем стандартные варианты сортировки
            {
                comboBoxSortOrder.Items.Clear();
                comboBoxSortOrder.Items.AddRange(new object[] { "По возрастанию", "По убыванию" });
            }

            comboBoxSortOrder.SelectedIndex = 0; // Устанавливаем первый элемент по умолчанию
        }

        /// <summary>
        /// Обработчик кнопки поиска - выполняет поиск по точному совпадению в выбранном столбце
        /// </summary>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // Проверяем подключение к базе данных
            if (_dbContext == null)
            {
                MessageBox.Show("База данных не открыта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем наличие текста для поиска
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                MessageBox.Show("Введите значение для поиска.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string searchText = textBoxSearch.Text.Trim().ToLower(); // Нормализуем поисковый запрос
            string filterColumn = comboBoxFilter.SelectedItem?.ToString();

            // Проверяем, что выбран столбец для поиска
            if (string.IsNullOrEmpty(filterColumn))
            {
                MessageBox.Show("Выберите столбец для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Определяем индекс столбца по его названию
            int columnIndex = filterColumn switch
            {
                "ID" => 0,
                "Название" => 1,
                "Масса шт (кг)" => 2,
                "Цена шт (руб)" => 3,
                "Всего шт" => 4,
                _ => -1
            };

            if (columnIndex == -1)
            {
                MessageBox.Show("Неверное имя столбца.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Сброс цвета всех строк перед новым поиском
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }

            // Поиск и подсветка строк с точным совпадением
            int count = 0;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue; // Пропускаем пустую строку для добавления

                var cellValue = row.Cells[columnIndex].Value?.ToString()?.ToLower();
                if (cellValue == searchText)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen; // Подсвечиваем найденные строки
                    count++;
                }
            }

            // Выводим результаты поиска
            if (count == 0)
            {
                MessageBox.Show("Совпадений не найдено.", "Результат поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                labelFilter.Visible = true;
                textBoxFilter.Visible = true;
                textBoxFilter.Text = count.ToString(); // Показываем количество найденных записей
            }
        }

        /// <summary>
        /// Обработчик пункта меню "Открыть базу данных"
        /// Позволяет выбрать существующий файл базы данных SQLite
        /// </summary>
        private void OpenDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|All Files (*.*)|*.*";
                openFileDialog.Title = "Выберите файл базы данных";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;

                    try
                    {
                        // Создаем новое подключение к выбранной базе данных
                        _dbContext = new DatabaseContext($"Data Source={selectedFile};Version=3;");
                        _dbContext.InitializeDatabase(); // Проверяем/создаем необходимые таблицы
                        LoadData(); // Загружаем данные в таблицу

                        // Активируем элементы управления для работы с данными
                        EnableDataControls(true);

                        deleteDatabaseToolStripMenuItem.Enabled = true; // Разрешаем удаление базы
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при открытии базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _dbContext = null; // Сбрасываем подключение при ошибке
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик пункта меню "Создать базу данных"
        /// Создает новый файл базы данных SQLite
        /// </summary>
        private void CreateDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|All Files (*.*)|*.*";
                saveFileDialog.Title = "Создайте новый файл базы данных";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string newDatabasePath = saveFileDialog.FileName;

                    try
                    {
                        File.Create(newDatabasePath).Close(); // Создаем пустой файл базы данных
                        _dbContext = new DatabaseContext($"Data Source={newDatabasePath};Version=3;");
                        _dbContext.InitializeDatabase(); // Инициализируем структуру таблиц
                        LoadData(); // Загружаем пустую таблицу

                        // Активируем элементы управления для работы с данными
                        EnableDataControls(true);

                        deleteDatabaseToolStripMenuItem.Enabled = true; // Разрешаем удаление базы
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при создании базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _dbContext = null; // Сбрасываем подключение при ошибке
                    }
                }
            }
        }

        /// <summary>
        /// Включает или отключает элементы управления для работы с данными
        /// </summary>
        /// <param name="enable">True - включить, False - отключить</param>
        private void EnableDataControls(bool enable)
        {
            textBoxName.Enabled = enable;
            textBoxMass.Enabled = enable;
            textBoxPrice.Enabled = enable;
            btnAdd.Enabled = enable;
            btnEdit.Enabled = enable;
            btnDelete.Enabled = enable;
            comboBoxFilter.Enabled = enable;
            txtFilterValue.Enabled = enable;
            btnFilter.Enabled = enable;
            btnClearFilter.Enabled = enable;
            buttonSort.Enabled = enable;
            comboBoxSortOrder.Enabled = enable;
            numericUpDownAmount.Enabled = enable;
            textBoxSearch.Enabled = enable;
            buttonSearch.Enabled = enable;
            textBoxFilter.Visible = false;
            labelFilter.Visible = false;
        }

        /// <summary>
        /// Обработчик пункта меню "Сохранить базу данных как..."
        /// Позволяет сохранить текущую базу данных под новым именем
        /// </summary>
        private void SaveAsDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Проверяем, что база данных открыта
            if (_dbContext == null || string.IsNullOrEmpty(_dbContext.ConnectionString))
            {
                MessageBox.Show("База данных не открыта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|All Files (*.*)|*.*";
                    saveFileDialog.Title = "Сохранить базу данных как";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string newDatabasePath = saveFileDialog.FileName;

                        // Получаем путь к текущей базе данных из строки подключения
                        var connectionStringBuilder = new SQLiteConnectionStringBuilder(_dbContext.ConnectionString);
                        string currentDatabasePath = connectionStringBuilder.DataSource;

                        // Освобождаем ресурсы перед копированием
                        _dbContext.Dispose();
                        _dbContext = null;
                        SQLiteConnection.ClearAllPools();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        System.Threading.Thread.Sleep(500); // Даем время системе освободить файл

                        // Проверяем, не заблокирован ли файл другим процессом
                        if (IsFileLocked(currentDatabasePath, out string lockingProcessName))
                        {
                            MessageBox.Show($"Файл базы данных используется процессом: {lockingProcessName}.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Копируем базу данных в новое место
                        File.Copy(currentDatabasePath, newDatabasePath, overwrite: true);

                        // Удаляем старый файл, если он существует
                        if (File.Exists(currentDatabasePath))
                        {
                            File.Delete(currentDatabasePath);
                        }

                        // Создаем новое подключение к сохраненной базе данных
                        _dbContext = new DatabaseContext($"Data Source={newDatabasePath};Version=3;");
                        LoadData(); // Обновляем отображение данных

                        MessageBox.Show("База данных успешно сохранена.", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении базы данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик пункта меню "Сохранить таблицу как PDF"
        /// Экспортирует содержимое DataGridView в PDF файл
        /// </summary>

        private void SaveTableAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли данные для экспорта
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Таблица пуста. Нечего сохранять.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить таблицу как PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pdfFilePath = saveFileDialog.FileName;

                    // Валидация пути к файлу
                    if (string.IsNullOrEmpty(pdfFilePath) || Path.GetInvalidPathChars().Any(pdfFilePath.Contains))
                    {
                        MessageBox.Show("Недопустимый путь к файлу.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        // Удаляем существующий файл, если он есть
                        if (File.Exists(pdfFilePath))
                        {
                            File.Delete(pdfFilePath);
                        }

                        // Настраиваем лицензию для QuestPDF (бесплатная версия)
                        QuestPDF.Settings.License = LicenseType.Community;

                        // Генерируем PDF документ
                        Document.Create(container =>
                        {
                            container.Page(page =>
                            {
                                page.Content().Column(column =>
                                {
                                    // Добавляем заголовок документа
                                    column.Item().Text("Таблица данных").FontSize(20).Bold();

                                    // Создаем таблицу в PDF
                                    column.Item().Table(table =>
                                    {
                                        // Настраиваем столбцы таблицы
                                        table.ColumnsDefinition(columns =>
                                        {
                                            foreach (DataGridViewColumn column in dataGridView.Columns)
                                            {
                                                columns.RelativeColumn(); // Автоматическая ширина столбцов
                                            }
                                        });

                                        // Добавляем заголовки столбцов
                                        table.Header(header =>
                                        {
                                            foreach (DataGridViewColumn column in dataGridView.Columns)
                                            {
                                                header.Cell().Text(column.HeaderText).Bold();
                                            }
                                        });

                                        // Добавляем данные из DataGridView
                                        foreach (DataGridViewRow row in dataGridView.Rows)
                                        {
                                            if (row.IsNewRow) continue; // Пропускаем пустые строки

                                            foreach (DataGridViewCell cell in row.Cells)
                                            {
                                                string cellValue = cell.Value?.ToString() ?? "";
                                                table.Cell().Text(cellValue);
                                            }
                                        }
                                    });
                                });
                            });
                        }).GeneratePdf(pdfFilePath); // Генерируем PDF файл

                        MessageBox.Show("Таблица успешно сохранена в PDF.", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при создании PDF: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик пункта меню "Удалить базу данных"
        /// Удаляет текущую открытую базу данных
        /// </summary>
        private void DeleteDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что база данных открыта
                if (_dbContext == null || string.IsNullOrEmpty(_dbContext.ConnectionString))
                {
                    MessageBox.Show("База данных не открыта.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем путь к файлу базы данных
                var connectionStringBuilder = new SQLiteConnectionStringBuilder(_dbContext.ConnectionString);
                string databasePath = connectionStringBuilder.DataSource;

                if (!File.Exists(databasePath))
                {
                    MessageBox.Show("Файл базы данных не найден.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Запрашиваем подтверждение удаления
                if (MessageBox.Show("Вы уверены, что хотите удалить текущую базу данных?",
                    "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        // Освобождаем ресурсы перед удалением
                        _dbContext.Dispose();
                        _dbContext = null;
                        SQLiteConnection.ClearAllPools();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        System.Threading.Thread.Sleep(500);

                        // Проверяем блокировку файла
                        if (IsFileLocked(databasePath, out string lockingProcessName))
                        {
                            MessageBox.Show($"Файл базы данных используется процессом: {lockingProcessName}.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Перемещаем файл во временную папку перед удалением
                        string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(databasePath));
                        File.Move(databasePath, tempPath);
                        File.Delete(tempPath);

                        // Очищаем интерфейс
                        dataGridView.Rows.Clear();

                        // Обновляем состояние меню
                        deleteDatabaseToolStripMenuItem.Enabled = false;
                        openDatabaseToolStripMenuItem.Enabled = true;
                        createDatabaseToolStripMenuItem.Enabled = true;

                        MessageBox.Show("База данных успешно удалена.", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException ex)
                    {
                        // Восстанавливаем подключение в случае ошибки
                        MessageBox.Show($"Не удалось удалить файл базы данных: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _dbContext = new DatabaseContext($"Data Source={databasePath};Version=3;");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении базы данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверяет, заблокирован ли файл другим процессом
        /// </summary>
        /// <param name="filePath">Путь к проверяемому файлу</param>
        /// <param name="lockingProcessName">Имя процесса, блокирующего файл (если есть)</param>
        /// <returns>True, если файл заблокирован; иначе False</returns>
        private bool IsFileLocked(string filePath, out string lockingProcessName)
        {
            lockingProcessName = null;

            try
            {
                // Пытаемся открыть файл с эксклюзивным доступом
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                // Если файл заблокирован, пытаемся определить процесс
                Process[] processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    try
                    {
                        if (process.MainWindowHandle != IntPtr.Zero && !string.IsNullOrEmpty(process.MainWindowTitle))
                        {
                            foreach (ProcessModule module in process.Modules)
                            {
                                if (module.FileName.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                                {
                                    lockingProcessName = process.ProcessName;
                                    return true;
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Пропускаем процессы, к которым нет доступа
                        continue;
                    }
                }

                return true; // Файл заблокирован, но процесс не определен
            }

            return false; // Файл доступен для записи
        }
    }
}
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
    /// ������� ����� ���������� ��� ������ � ����� ������ �������
    /// </summary>
    public partial class MainForm : Form
    {
        private DatabaseContext _dbContext; // �������� ���� ������

        /// <summary>
        /// ����������� ������� �����
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeMenu();
            // ������������� ����������� � SQLite ���� ������
            _dbContext = new DatabaseContext("Data Source=products.db");
            _dbContext.InitializeDatabase(); // �������� ������, ���� �� ���
            LoadData(); // �������������� �������� ������
        }

        /// <summary>
        /// ���������� ����� ��� ��������� �����, ����������� ������ ����� � �����
        /// </summary>
        private void txtBoxWithoutCharacters_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ��������� �����, ����� � ����������� ������� (��������, Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // ���������� ����
            }

            // ��������� ���� ����� ����� �����
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// ���������� ��������� ��������� ������ � DataGridView
        /// </summary>
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                // ��������� ���� ����� ������� �� ��������� ������
                textBoxName.Text = selectedRow.Cells["dataGridViewTextBoxColumn2"].Value.ToString();
                textBoxMass.Text = selectedRow.Cells["dataGridViewTextBoxColumn3"].Value.ToString();
                textBoxPrice.Text = selectedRow.Cells["dataGridViewTextBoxColumn4"].Value.ToString();
                numericUpDownAmount.Value = Convert.ToInt32(selectedRow.Cells["dataGridViewTextBoxColumn5"].Value);
            }
        }

        /// <summary>
        /// ������������� ��������� ������� ����
        /// </summary>
        private void InitializeMenu()
        {
            deleteDatabaseToolStripMenuItem.Enabled = false; // �������� ����������
            openDatabaseToolStripMenuItem.Enabled = true; // �������� ��������
            createDatabaseToolStripMenuItem.Enabled = true; // �������� ��������
        }

        /// <summary>
        /// ���������� �������� �����
        /// </summary>
        private void ClientForm_Load(object sender, EventArgs e)
        {
            comboBoxFilter.SelectedIndex = 0; // ������������� ������ �� ���������

            // ��������� ������ ������ ���� ���� ������ �������
            if (_dbContext != null)
            {
                LoadData();
            }
        }

        /// <summary>
        /// �������� ������ �� ���� � DataGridView
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
                    // ����������� ����� ��� ����������� �����������
                    string mass = product.Mass.ToString("F3", CultureInfo.InvariantCulture);
                    string price = product.Price.ToString("F2", CultureInfo.InvariantCulture);
                    string amount = product.Amount.ToString(CultureInfo.InvariantCulture);

                    dataGridView.Rows.Add(product.Id, product.Name, mass, price, amount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������� ������: {ex.Message}", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ���������� ������ ���������� ������ ������
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // �������� ���������� ������������ �����
            if (string.IsNullOrEmpty(textBoxMass.Text) || string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("��������� ��� ����.", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ������� ����� ����� �� ������ ��������� ������
                var product = new Product
                {
                    Name = textBoxName.Text,
                    Mass = double.Parse(textBoxMass.Text, CultureInfo.InvariantCulture),
                    Price = double.Parse(textBoxPrice.Text, CultureInfo.InvariantCulture),
                    Amount = (int)numericUpDownAmount.Value
                };

                _dbContext.AddProduct(product); // ��������� � ���� ������
                LoadData(); // ��������� �����������
            }
            catch (FormatException)
            {
                MessageBox.Show("������������ ������ �����. ����������� ����� ��� �����������.",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ���������� ������: {ex.Message}",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ���������� ������� ������ "�������������" - ��������� ��������� ����� � ���� ������
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // ���������, ��� ������� ������ ��� ��������������
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                try
                {
                    // ������� ������ ������ � ������������ ������� �� �����
                    var product = new Product
                    {
                        Id = (int)selectedRow.Cells[0].Value, // ����� ID �� ��������� ������
                        Name = textBoxName.Text,
                        Mass = double.Parse(textBoxMass.Text, CultureInfo.InvariantCulture),
                        Price = double.Parse(textBoxPrice.Text, CultureInfo.InvariantCulture),
                        Amount = (int)numericUpDownAmount.Value
                    };

                    _dbContext.UpdateProduct(product); // ��������� ����� � ����
                    LoadData(); // ������������� ������ ��� �����������
                }
                catch (FormatException)
                {
                    MessageBox.Show("������������ ������ �����. ����������� ����� ��� �����������.",
                        "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ ��� �������������� ������: {ex.Message}",
                        "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// ���������� ������� ������ "�������" - ������� ��������� ����� �� ���� ������
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // ���������, ��� ������� ������ ��� ��������
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];

                // ���������, ��� ��� �� ����� ������ ������
                if (selectedRow.IsNewRow)
                {
                    MessageBox.Show("�������� ������ ��� ��������.",
                        "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // �������� ID ������ ��� ��������
                int id = (int)selectedRow.Cells[0].Value;
                _dbContext.DeleteProduct(id); // ������� �� ����
                LoadData(); // ��������� �����������
            }
            else
            {
                MessageBox.Show("�������� ������ ��� ��������.",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// ���������� ������� ������ "�����������" - ��������� ������ �� ���������� ��������
        /// </summary>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            string filterValue = txtFilterValue.Text; // �������� ��� ����������
            string filterColumn = comboBoxFilter.SelectedItem.ToString(); // ������� ��� ����������

            List<Product> products = new List<Product>();

            // ��������� ������ � ����������� �� ���������� �������
            switch (filterColumn)
            {
                case "ID":
                    products = _dbContext.CachedProducts.Where(f => f.Id.ToString().Contains(filterValue)).ToList();
                    break;
                case "��������":
                    products = _dbContext.CachedProducts.Where(f => f.Name.Contains(filterValue, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "����� �� (��)":
                    products = _dbContext.CachedProducts.Where(f => f.Mass.ToString().Contains(filterValue)).ToList();
                    break;
                case "���� �� (���)":
                    products = _dbContext.CachedProducts.Where(f => f.Price.ToString().Contains(filterValue)).ToList();
                    break;
                case "����� ��":
                    products = _dbContext.CachedProducts.Where(f => f.Amount.ToString().Contains(filterValue)).ToList();
                    break;
            }

            // ������������ ������, ����� ������ �� �������
            if (products.Count == 0)
            {
                MessageBox.Show("���������� �� �������.",
                    "��������� ������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // ���������� ��������������� ������
                dataGridView.Rows.Clear();
                foreach (var product in products)
                {
                    dataGridView.Rows.Add(product.Id, product.Name,
                        product.Mass.ToString("F3", CultureInfo.InvariantCulture),
                        product.Price.ToString("F2", CultureInfo.InvariantCulture),
                        product.Amount);
                }

                // ���������� ���������� ��������� �������
                textBoxFilter.Visible = true;
                labelFilter.Visible = true;
                textBoxFilter.Text = products.Count.ToString();
            }
        }

        /// <summary>
        /// ���������� ������� ������ "�������� ������" - ���������� ����������
        /// </summary>
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFilterValue.Clear(); // ������� ���� �������
            textBoxFilter.Text = "0"; // ���������� �������
            textBoxFilter.Visible = false; // �������� �������� �������
            labelFilter.Visible = false;
            LoadData(); // ��������� ������ ������ �������
        }

        /// <summary>
        /// ���������� ������� ������ "�����������" - ��������� ������ �� ���������� �������
        /// </summary>
        private void btnSort_Click(object sender, EventArgs e)
        {
            // ���������, ��� ���� ������ ����������
            if (_dbContext == null)
            {
                MessageBox.Show("���� ������ �� �������.",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sortColumn = comboBoxFilter.SelectedItem.ToString(); // ������� ��� ����������
            string sortOrder = comboBoxSortOrder.SelectedItem?.ToString(); // ����������� ����������

            var ProductList = _dbContext.CachedProducts;

            // ��������� ���������� � ����������� �� ���������� ������� � �����������
            switch (sortColumn)
            {
                case "ID":
                    ProductList = sortOrder == "�� �����������"
                        ? ProductList.OrderBy(f => f.Id).ToList()
                        : ProductList.OrderByDescending(f => f.Id).ToList();
                    break;

                case "��������":
                    ProductList = sortOrder == "�� � �� �"
                        ? ProductList.OrderBy(f => f.Name).ToList()
                        : ProductList.OrderByDescending(f => f.Name).ToList();
                    break;

                case "����� �� (��)":
                    ProductList = sortOrder == "�� �����������"
                        ? ProductList.OrderBy(f => f.Mass).ToList()
                        : ProductList.OrderByDescending(f => f.Mass).ToList();
                    break;

                case "���� �� (���)":
                    ProductList = sortOrder == "�� �����������"
                        ? ProductList.OrderBy(f => f.Price).ToList()
                        : ProductList.OrderByDescending(f => f.Price).ToList();
                    break;
                case "����� ��":
                    ProductList = sortOrder == "�� �����������"
                        ? ProductList.OrderBy(f => f.Amount).ToList()
                        : ProductList.OrderByDescending(f => f.Amount).ToList();
                    break;

                default:
                    MessageBox.Show("�������� ������� ��� ����������.",
                        "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            // ��������� DataGridView ���������������� �������
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
        /// ���������� ��������� ���������� �������� � ���������� �������
        /// ��������� �������� ���������� � ����������� �� ���������� �������
        /// </summary>
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ��� ������� "��������" ���������� ��������� �������� ����������
            if (comboBoxFilter.SelectedItem.ToString() == "��������")
            {
                comboBoxSortOrder.Items.Clear();
                comboBoxSortOrder.Items.AddRange(new object[] { "�� � �� �", "�� � �� �" });
            }
            else // ��� �������� �������� ���������� ����������� �������� ����������
            {
                comboBoxSortOrder.Items.Clear();
                comboBoxSortOrder.Items.AddRange(new object[] { "�� �����������", "�� ��������" });
            }

            comboBoxSortOrder.SelectedIndex = 0; // ������������� ������ ������� �� ���������
        }

        /// <summary>
        /// ���������� ������ ������ - ��������� ����� �� ������� ���������� � ��������� �������
        /// </summary>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // ��������� ����������� � ���� ������
            if (_dbContext == null)
            {
                MessageBox.Show("���� ������ �� �������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ��������� ������� ������ ��� ������
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                MessageBox.Show("������� �������� ��� ������.", "��������������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string searchText = textBoxSearch.Text.Trim().ToLower(); // ����������� ��������� ������
            string filterColumn = comboBoxFilter.SelectedItem?.ToString();

            // ���������, ��� ������ ������� ��� ������
            if (string.IsNullOrEmpty(filterColumn))
            {
                MessageBox.Show("�������� ������� ��� ������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ���������� ������ ������� �� ��� ��������
            int columnIndex = filterColumn switch
            {
                "ID" => 0,
                "��������" => 1,
                "����� �� (��)" => 2,
                "���� �� (���)" => 3,
                "����� ��" => 4,
                _ => -1
            };

            if (columnIndex == -1)
            {
                MessageBox.Show("�������� ��� �������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ����� ����� ���� ����� ����� ����� �������
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }

            // ����� � ��������� ����� � ������ �����������
            int count = 0;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue; // ���������� ������ ������ ��� ����������

                var cellValue = row.Cells[columnIndex].Value?.ToString()?.ToLower();
                if (cellValue == searchText)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen; // ������������ ��������� ������
                    count++;
                }
            }

            // ������� ���������� ������
            if (count == 0)
            {
                MessageBox.Show("���������� �� �������.", "��������� ������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                labelFilter.Visible = true;
                textBoxFilter.Visible = true;
                textBoxFilter.Text = count.ToString(); // ���������� ���������� ��������� �������
            }
        }

        /// <summary>
        /// ���������� ������ ���� "������� ���� ������"
        /// ��������� ������� ������������ ���� ���� ������ SQLite
        /// </summary>
        private void OpenDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|All Files (*.*)|*.*";
                openFileDialog.Title = "�������� ���� ���� ������";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;

                    try
                    {
                        // ������� ����� ����������� � ��������� ���� ������
                        _dbContext = new DatabaseContext($"Data Source={selectedFile};Version=3;");
                        _dbContext.InitializeDatabase(); // ���������/������� ����������� �������
                        LoadData(); // ��������� ������ � �������

                        // ���������� �������� ���������� ��� ������ � �������
                        EnableDataControls(true);

                        deleteDatabaseToolStripMenuItem.Enabled = true; // ��������� �������� ����
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"������ ��� �������� ���� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _dbContext = null; // ���������� ����������� ��� ������
                    }
                }
            }
        }

        /// <summary>
        /// ���������� ������ ���� "������� ���� ������"
        /// ������� ����� ���� ���� ������ SQLite
        /// </summary>
        private void CreateDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|All Files (*.*)|*.*";
                saveFileDialog.Title = "�������� ����� ���� ���� ������";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string newDatabasePath = saveFileDialog.FileName;

                    try
                    {
                        File.Create(newDatabasePath).Close(); // ������� ������ ���� ���� ������
                        _dbContext = new DatabaseContext($"Data Source={newDatabasePath};Version=3;");
                        _dbContext.InitializeDatabase(); // �������������� ��������� ������
                        LoadData(); // ��������� ������ �������

                        // ���������� �������� ���������� ��� ������ � �������
                        EnableDataControls(true);

                        deleteDatabaseToolStripMenuItem.Enabled = true; // ��������� �������� ����
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"������ ��� �������� ���� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _dbContext = null; // ���������� ����������� ��� ������
                    }
                }
            }
        }

        /// <summary>
        /// �������� ��� ��������� �������� ���������� ��� ������ � �������
        /// </summary>
        /// <param name="enable">True - ��������, False - ���������</param>
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
        /// ���������� ������ ���� "��������� ���� ������ ���..."
        /// ��������� ��������� ������� ���� ������ ��� ����� ������
        /// </summary>
        private void SaveAsDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ���������, ��� ���� ������ �������
            if (_dbContext == null || string.IsNullOrEmpty(_dbContext.ConnectionString))
            {
                MessageBox.Show("���� ������ �� �������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|All Files (*.*)|*.*";
                    saveFileDialog.Title = "��������� ���� ������ ���";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string newDatabasePath = saveFileDialog.FileName;

                        // �������� ���� � ������� ���� ������ �� ������ �����������
                        var connectionStringBuilder = new SQLiteConnectionStringBuilder(_dbContext.ConnectionString);
                        string currentDatabasePath = connectionStringBuilder.DataSource;

                        // ����������� ������� ����� ������������
                        _dbContext.Dispose();
                        _dbContext = null;
                        SQLiteConnection.ClearAllPools();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        System.Threading.Thread.Sleep(500); // ���� ����� ������� ���������� ����

                        // ���������, �� ������������ �� ���� ������ ���������
                        if (IsFileLocked(currentDatabasePath, out string lockingProcessName))
                        {
                            MessageBox.Show($"���� ���� ������ ������������ ���������: {lockingProcessName}.",
                                "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // �������� ���� ������ � ����� �����
                        File.Copy(currentDatabasePath, newDatabasePath, overwrite: true);

                        // ������� ������ ����, ���� �� ����������
                        if (File.Exists(currentDatabasePath))
                        {
                            File.Delete(currentDatabasePath);
                        }

                        // ������� ����� ����������� � ����������� ���� ������
                        _dbContext = new DatabaseContext($"Data Source={newDatabasePath};Version=3;");
                        LoadData(); // ��������� ����������� ������

                        MessageBox.Show("���� ������ ������� ���������.", "�����",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ���������� ���� ������: {ex.Message}",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ���������� ������ ���� "��������� ������� ��� PDF"
        /// ������������ ���������� DataGridView � PDF ����
        /// </summary>

        private void SaveTableAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ���������, ���� �� ������ ��� ��������
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("������� �����. ������ ���������.", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                saveFileDialog.Title = "��������� ������� ��� PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pdfFilePath = saveFileDialog.FileName;

                    // ��������� ���� � �����
                    if (string.IsNullOrEmpty(pdfFilePath) || Path.GetInvalidPathChars().Any(pdfFilePath.Contains))
                    {
                        MessageBox.Show("������������ ���� � �����.", "������",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        // ������� ������������ ����, ���� �� ����
                        if (File.Exists(pdfFilePath))
                        {
                            File.Delete(pdfFilePath);
                        }

                        // ����������� �������� ��� QuestPDF (���������� ������)
                        QuestPDF.Settings.License = LicenseType.Community;

                        // ���������� PDF ��������
                        Document.Create(container =>
                        {
                            container.Page(page =>
                            {
                                page.Content().Column(column =>
                                {
                                    // ��������� ��������� ���������
                                    column.Item().Text("������� ������").FontSize(20).Bold();

                                    // ������� ������� � PDF
                                    column.Item().Table(table =>
                                    {
                                        // ����������� ������� �������
                                        table.ColumnsDefinition(columns =>
                                        {
                                            foreach (DataGridViewColumn column in dataGridView.Columns)
                                            {
                                                columns.RelativeColumn(); // �������������� ������ ��������
                                            }
                                        });

                                        // ��������� ��������� ��������
                                        table.Header(header =>
                                        {
                                            foreach (DataGridViewColumn column in dataGridView.Columns)
                                            {
                                                header.Cell().Text(column.HeaderText).Bold();
                                            }
                                        });

                                        // ��������� ������ �� DataGridView
                                        foreach (DataGridViewRow row in dataGridView.Rows)
                                        {
                                            if (row.IsNewRow) continue; // ���������� ������ ������

                                            foreach (DataGridViewCell cell in row.Cells)
                                            {
                                                string cellValue = cell.Value?.ToString() ?? "";
                                                table.Cell().Text(cellValue);
                                            }
                                        }
                                    });
                                });
                            });
                        }).GeneratePdf(pdfFilePath); // ���������� PDF ����

                        MessageBox.Show("������� ������� ��������� � PDF.", "�����",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"������ ��� �������� PDF: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                            "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// ���������� ������ ���� "������� ���� ������"
        /// ������� ������� �������� ���� ������
        /// </summary>
        private void DeleteDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // ���������, ��� ���� ������ �������
                if (_dbContext == null || string.IsNullOrEmpty(_dbContext.ConnectionString))
                {
                    MessageBox.Show("���� ������ �� �������.", "������",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // �������� ���� � ����� ���� ������
                var connectionStringBuilder = new SQLiteConnectionStringBuilder(_dbContext.ConnectionString);
                string databasePath = connectionStringBuilder.DataSource;

                if (!File.Exists(databasePath))
                {
                    MessageBox.Show("���� ���� ������ �� ������.", "������",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ����������� ������������� ��������
                if (MessageBox.Show("�� �������, ��� ������ ������� ������� ���� ������?",
                    "�������������", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        // ����������� ������� ����� ���������
                        _dbContext.Dispose();
                        _dbContext = null;
                        SQLiteConnection.ClearAllPools();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        System.Threading.Thread.Sleep(500);

                        // ��������� ���������� �����
                        if (IsFileLocked(databasePath, out string lockingProcessName))
                        {
                            MessageBox.Show($"���� ���� ������ ������������ ���������: {lockingProcessName}.",
                                "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // ���������� ���� �� ��������� ����� ����� ���������
                        string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(databasePath));
                        File.Move(databasePath, tempPath);
                        File.Delete(tempPath);

                        // ������� ���������
                        dataGridView.Rows.Clear();

                        // ��������� ��������� ����
                        deleteDatabaseToolStripMenuItem.Enabled = false;
                        openDatabaseToolStripMenuItem.Enabled = true;
                        createDatabaseToolStripMenuItem.Enabled = true;

                        MessageBox.Show("���� ������ ������� �������.", "�����",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException ex)
                    {
                        // ��������������� ����������� � ������ ������
                        MessageBox.Show($"�� ������� ������� ���� ���� ������: {ex.Message}",
                            "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _dbContext = new DatabaseContext($"Data Source={databasePath};Version=3;");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������� ���� ������: {ex.Message}",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ���������, ������������ �� ���� ������ ���������
        /// </summary>
        /// <param name="filePath">���� � ������������ �����</param>
        /// <param name="lockingProcessName">��� ��������, ������������ ���� (���� ����)</param>
        /// <returns>True, ���� ���� ������������; ����� False</returns>
        private bool IsFileLocked(string filePath, out string lockingProcessName)
        {
            lockingProcessName = null;

            try
            {
                // �������� ������� ���� � ������������ ��������
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                // ���� ���� ������������, �������� ���������� �������
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
                        // ���������� ��������, � ������� ��� �������
                        continue;
                    }
                }

                return true; // ���� ������������, �� ������� �� ���������
            }

            return false; // ���� �������� ��� ������
        }
    }
}
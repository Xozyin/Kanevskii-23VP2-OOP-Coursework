namespace OOP_Curs_Kanevskii_23VP2
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox textBoxMass;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Label LabelPrice;
        private System.Windows.Forms.Label LabelWeight;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDatabaseToolStripMenuItem; 
        private System.Windows.Forms.ToolStripMenuItem createDatabaseToolStripMenuItem; 
        private System.Windows.Forms.ToolStripMenuItem deleteDatabaseToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridView = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            LabelName = new Label();
            LabelWeight = new Label();
            LabelPrice = new Label();
            textBoxMass = new TextBox();
            textBoxPrice = new TextBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            comboBoxFilter = new ComboBox();
            txtFilterValue = new TextBox();
            btnFilter = new Button();
            btnClearFilter = new Button();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openDatabaseToolStripMenuItem = new ToolStripMenuItem();
            createDatabaseToolStripMenuItem = new ToolStripMenuItem();
            saveAsDatabaseToolStripMenuItem = new ToolStripMenuItem();
            SaveTableAsPDFToolStripMenuItem = new ToolStripMenuItem();
            deleteDatabaseToolStripMenuItem = new ToolStripMenuItem();
            labelFilterByValue = new Label();
            textBoxName = new TextBox();
            numericUpDownAmount = new NumericUpDown();
            LabelAmount = new Label();
            panel1 = new Panel();
            comboBoxSortOrder = new ComboBox();
            buttonSort = new Button();
            SortLabel = new Label();
            textBoxFilter = new TextBox();
            labelFilter = new Label();
            buttonSearch = new Button();
            textBoxSearch = new TextBox();
            labelSearch = new Label();
            labelF = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAmount).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dataGridView.Location = new Point(532, 0);
            dataGridView.Margin = new Padding(4);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(833, 458);
            dataGridView.TabIndex = 0;
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Id";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Название";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 190;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Масса шт (кг)";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 190;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Цена шт (руб)";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 190;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Всего шт";
            dataGridViewTextBoxColumn5.MinimumWidth = 8;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.Width = 150;
            // 
            // LabelName
            // 
            LabelName.AutoSize = true;
            LabelName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic);
            LabelName.Location = new Point(13, 205);
            LabelName.Margin = new Padding(4, 0, 4, 0);
            LabelName.Name = "LabelName";
            LabelName.Size = new Size(124, 32);
            LabelName.TabIndex = 1;
            LabelName.Text = "Название";
            // 
            // LabelWeight
            // 
            LabelWeight.AutoSize = true;
            LabelWeight.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic);
            LabelWeight.Location = new Point(13, 251);
            LabelWeight.Margin = new Padding(4, 0, 4, 0);
            LabelWeight.Name = "LabelWeight";
            LabelWeight.Size = new Size(179, 32);
            LabelWeight.TabIndex = 3;
            LabelWeight.Text = "Масса шт (кг)";
            // 
            // LabelPrice
            // 
            LabelPrice.AutoSize = true;
            LabelPrice.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic);
            LabelPrice.Location = new Point(15, 304);
            LabelPrice.Margin = new Padding(4, 0, 4, 0);
            LabelPrice.Name = "LabelPrice";
            LabelPrice.Size = new Size(136, 32);
            LabelPrice.TabIndex = 5;
            LabelPrice.Text = "Цена (руб)";
            // 
            // textBoxMass
            // 
            textBoxMass.Enabled = false;
            textBoxMass.Location = new Point(220, 252);
            textBoxMass.Margin = new Padding(4);
            textBoxMass.Name = "textBoxMass";
            textBoxMass.Size = new Size(186, 31);
            textBoxMass.TabIndex = 4;
            textBoxMass.KeyPress += txtBoxWithoutCharacters_KeyPress;
            // 
            // textBoxPrice
            // 
            textBoxPrice.Enabled = false;
            textBoxPrice.Location = new Point(220, 305);
            textBoxPrice.Margin = new Padding(4);
            textBoxPrice.Name = "textBoxPrice";
            textBoxPrice.Size = new Size(186, 31);
            textBoxPrice.TabIndex = 6;
            textBoxPrice.KeyPress += txtBoxWithoutCharacters_KeyPress;
            // 
            // btnAdd
            // 
            btnAdd.Enabled = false;
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAdd.Location = new Point(15, 412);
            btnAdd.Margin = new Padding(4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(143, 42);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Добавить";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Enabled = false;
            btnEdit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnEdit.Location = new Point(166, 412);
            btnEdit.Margin = new Padding(4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(206, 42);
            btnEdit.TabIndex = 8;
            btnEdit.Text = "Редактировать";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Enabled = false;
            btnDelete.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDelete.Location = new Point(380, 412);
            btnDelete.Margin = new Padding(4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(142, 42);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Удалить";
            btnDelete.Click += btnDelete_Click;
            // 
            // comboBoxFilter
            // 
            comboBoxFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFilter.Enabled = false;
            comboBoxFilter.FormattingEnabled = true;
            comboBoxFilter.Items.AddRange(new object[] { "ID", "Название", "Масса шт (кг)", "Цена шт (руб)", "Всего шт" });
            comboBoxFilter.Location = new Point(13, 537);
            comboBoxFilter.Margin = new Padding(4);
            comboBoxFilter.Name = "comboBoxFilter";
            comboBoxFilter.Size = new Size(186, 33);
            comboBoxFilter.TabIndex = 10;
            comboBoxFilter.SelectedIndexChanged += comboBoxFilter_SelectedIndexChanged;
            // 
            // txtFilterValue
            // 
            txtFilterValue.Enabled = false;
            txtFilterValue.Location = new Point(525, 493);
            txtFilterValue.Margin = new Padding(4);
            txtFilterValue.Name = "txtFilterValue";
            txtFilterValue.Size = new Size(186, 31);
            txtFilterValue.TabIndex = 11;
            // 
            // btnFilter
            // 
            btnFilter.Enabled = false;
            btnFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFilter.Location = new Point(719, 477);
            btnFilter.Margin = new Padding(4);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(184, 47);
            btnFilter.TabIndex = 12;
            btnFilter.Text = "Фильтровать";
            btnFilter.Click += btnFilter_Click;
            // 
            // btnClearFilter
            // 
            btnClearFilter.Enabled = false;
            btnClearFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClearFilter.Location = new Point(911, 477);
            btnClearFilter.Margin = new Padding(4);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(140, 164);
            btnClearFilter.TabIndex = 13;
            btnClearFilter.Text = "Сбросить действие";
            btnClearFilter.Click += btnClearFilter_Click;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(8, 2, 0, 2);
            menuStrip.Size = new Size(1378, 33);
            menuStrip.TabIndex = 14;
            menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = SystemColors.ActiveCaption;
            fileToolStripMenuItem.DropDownDirection = ToolStripDropDownDirection.BelowRight;
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openDatabaseToolStripMenuItem, createDatabaseToolStripMenuItem, saveAsDatabaseToolStripMenuItem, SaveTableAsPDFToolStripMenuItem, deleteDatabaseToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(69, 29);
            fileToolStripMenuItem.Text = "Файл";
            // 
            // openDatabaseToolStripMenuItem
            // 
            openDatabaseToolStripMenuItem.Name = "openDatabaseToolStripMenuItem";
            openDatabaseToolStripMenuItem.Size = new Size(322, 34);
            openDatabaseToolStripMenuItem.Text = "Открыть БД";
            openDatabaseToolStripMenuItem.Click += OpenDatabaseToolStripMenuItem_Click;
            // 
            // createDatabaseToolStripMenuItem
            // 
            createDatabaseToolStripMenuItem.Name = "createDatabaseToolStripMenuItem";
            createDatabaseToolStripMenuItem.Size = new Size(322, 34);
            createDatabaseToolStripMenuItem.Text = "Создать БД";
            createDatabaseToolStripMenuItem.Click += CreateDatabaseToolStripMenuItem_Click;
            // 
            // saveAsDatabaseToolStripMenuItem
            // 
            saveAsDatabaseToolStripMenuItem.Name = "saveAsDatabaseToolStripMenuItem";
            saveAsDatabaseToolStripMenuItem.Size = new Size(322, 34);
            saveAsDatabaseToolStripMenuItem.Text = "Сохранить как";
            saveAsDatabaseToolStripMenuItem.Click += SaveAsDatabaseToolStripMenuItem_Click;
            // 
            // SaveTableAsPDFToolStripMenuItem
            // 
            SaveTableAsPDFToolStripMenuItem.Name = "SaveTableAsPDFToolStripMenuItem";
            SaveTableAsPDFToolStripMenuItem.Size = new Size(322, 34);
            SaveTableAsPDFToolStripMenuItem.Text = "Сохранить таблицу в PDF";
            SaveTableAsPDFToolStripMenuItem.Click += SaveTableAsPDFToolStripMenuItem_Click;
            // 
            // deleteDatabaseToolStripMenuItem
            // 
            deleteDatabaseToolStripMenuItem.Enabled = false;
            deleteDatabaseToolStripMenuItem.Name = "deleteDatabaseToolStripMenuItem";
            deleteDatabaseToolStripMenuItem.Size = new Size(322, 34);
            deleteDatabaseToolStripMenuItem.Text = "Удалить текущую БД";
            deleteDatabaseToolStripMenuItem.Click += DeleteDatabaseToolStripMenuItem_Click;
            // 
            // labelFilterByValue
            // 
            labelFilterByValue.AutoSize = true;
            labelFilterByValue.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic);
            labelFilterByValue.Location = new Point(6, 492);
            labelFilterByValue.Margin = new Padding(4, 0, 4, 0);
            labelFilterByValue.Name = "labelFilterByValue";
            labelFilterByValue.Size = new Size(282, 32);
            labelFilterByValue.TabIndex = 17;
            labelFilterByValue.Text = "Действия по значению";
            // 
            // textBoxName
            // 
            textBoxName.Enabled = false;
            textBoxName.Location = new Point(220, 206);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(186, 31);
            textBoxName.TabIndex = 19;
            // 
            // numericUpDownAmount
            // 
            numericUpDownAmount.Enabled = false;
            numericUpDownAmount.Location = new Point(220, 358);
            numericUpDownAmount.Name = "numericUpDownAmount";
            numericUpDownAmount.Size = new Size(186, 31);
            numericUpDownAmount.TabIndex = 20;
            // 
            // LabelAmount
            // 
            LabelAmount.AutoSize = true;
            LabelAmount.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic);
            LabelAmount.Location = new Point(15, 357);
            LabelAmount.Name = "LabelAmount";
            LabelAmount.Size = new Size(124, 32);
            LabelAmount.TabIndex = 21;
            LabelAmount.Text = "Всего шт";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(0, 465);
            panel1.Name = "panel1";
            panel1.Size = new Size(1376, 5);
            panel1.TabIndex = 22;
            // 
            // comboBoxSortOrder
            // 
            comboBoxSortOrder.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSortOrder.Enabled = false;
            comboBoxSortOrder.FormattingEnabled = true;
            comboBoxSortOrder.Items.AddRange(new object[] { "По возрастанию", "По убыванию" });
            comboBoxSortOrder.Location = new Point(525, 548);
            comboBoxSortOrder.Margin = new Padding(4);
            comboBoxSortOrder.Name = "comboBoxSortOrder";
            comboBoxSortOrder.Size = new Size(186, 33);
            comboBoxSortOrder.TabIndex = 23;
            // 
            // buttonSort
            // 
            buttonSort.Enabled = false;
            buttonSort.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonSort.Location = new Point(719, 537);
            buttonSort.Margin = new Padding(4);
            buttonSort.Name = "buttonSort";
            buttonSort.Size = new Size(184, 44);
            buttonSort.TabIndex = 24;
            buttonSort.Text = "Сортировать";
            buttonSort.Click += btnSort_Click;
            // 
            // SortLabel
            // 
            SortLabel.AutoSize = true;
            SortLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic);
            SortLabel.Location = new Point(340, 549);
            SortLabel.Name = "SortLabel";
            SortLabel.Size = new Size(158, 32);
            SortLabel.TabIndex = 25;
            SortLabel.Text = "Сортировка";
            // 
            // textBoxFilter
            // 
            textBoxFilter.BackColor = SystemColors.ButtonHighlight;
            textBoxFilter.Enabled = false;
            textBoxFilter.Location = new Point(1070, 516);
            textBoxFilter.Name = "textBoxFilter";
            textBoxFilter.ReadOnly = true;
            textBoxFilter.Size = new Size(306, 31);
            textBoxFilter.TabIndex = 26;
            textBoxFilter.Visible = false;
            // 
            // labelFilter
            // 
            labelFilter.AutoSize = true;
            labelFilter.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelFilter.Location = new Point(1058, 477);
            labelFilter.Name = "labelFilter";
            labelFilter.Size = new Size(320, 32);
            labelFilter.TabIndex = 27;
            labelFilter.Text = "Число найденных товаров:";
            labelFilter.Visible = false;
            // 
            // buttonSearch
            // 
            buttonSearch.Enabled = false;
            buttonSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonSearch.Location = new Point(719, 597);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(184, 44);
            buttonSearch.TabIndex = 28;
            buttonSearch.Text = "Подсветить";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Enabled = false;
            textBoxSearch.Location = new Point(525, 610);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(188, 31);
            textBoxSearch.TabIndex = 29;
            // 
            // labelSearch
            // 
            labelSearch.AutoSize = true;
            labelSearch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic);
            labelSearch.Location = new Point(415, 609);
            labelSearch.Name = "labelSearch";
            labelSearch.Size = new Size(83, 32);
            labelSearch.TabIndex = 30;
            labelSearch.Text = "Поиск";
            // 
            // labelF
            // 
            labelF.AutoSize = true;
            labelF.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic);
            labelF.Location = new Point(388, 493);
            labelF.Name = "labelF";
            labelF.Size = new Size(110, 32);
            labelF.TabIndex = 31;
            labelF.Text = "Фильтр";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1378, 654);
            Controls.Add(labelF);
            Controls.Add(labelSearch);
            Controls.Add(textBoxSearch);
            Controls.Add(buttonSearch);
            Controls.Add(labelFilter);
            Controls.Add(textBoxFilter);
            Controls.Add(SortLabel);
            Controls.Add(buttonSort);
            Controls.Add(comboBoxSortOrder);
            Controls.Add(panel1);
            Controls.Add(LabelAmount);
            Controls.Add(numericUpDownAmount);
            Controls.Add(textBoxName);
            Controls.Add(labelFilterByValue);
            Controls.Add(btnClearFilter);
            Controls.Add(btnFilter);
            Controls.Add(txtFilterValue);
            Controls.Add(comboBoxFilter);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(textBoxPrice);
            Controls.Add(LabelPrice);
            Controls.Add(textBoxMass);
            Controls.Add(LabelWeight);
            Controls.Add(LabelName);
            Controls.Add(dataGridView);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Margin = new Padding(4);
            MaximumSize = new Size(1400, 710);
            MinimumSize = new Size(1400, 710);
            Name = "MainForm";
            Text = "Каневский Глеб 23ВП2 БД \"СКЛАД\"";
            Load += ClientForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAmount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStripMenuItem saveAsDatabaseToolStripMenuItem;
        private ToolStripMenuItem SaveTableAsPDFToolStripMenuItem;
        private Label labelFilterByValue;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private TextBox textBoxName;
        private NumericUpDown numericUpDownAmount;
        private Label LabelAmount;
        private Panel panel1;
        private ComboBox comboBoxSortOrder;
        private Button buttonSort;
        private Label SortLabel;
        private TextBox textBoxFilter;
        private Label labelFilter;
        private Button buttonSearch;
        private TextBox textBoxSearch;
        private Label labelSearch;
        private Label labelF;
    }
}
﻿namespace OOP_Curs_Kanevskii_23VP2
{
    partial class HelloForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelloForm));
            labelNAME = new Label();
            label1 = new Label();
            label2 = new Label();
            buttonNext = new Button();
            SuspendLayout();
            // 
            // labelNAME
            // 
            labelNAME.AutoSize = true;
            labelNAME.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            labelNAME.Location = new Point(421, 141);
            labelNAME.Name = "labelNAME";
            labelNAME.Size = new Size(543, 65);
            labelNAME.TabIndex = 0;
            labelNAME.Text = "Каневский Глеб 23ВП2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            label1.Location = new Point(441, 308);
            label1.Name = "label1";
            label1.Size = new Size(514, 65);
            label1.TabIndex = 1;
            label1.Text = "База данных: \"Склад\"";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            label2.Location = new Point(481, 231);
            label2.Name = "label2";
            label2.Size = new Size(406, 65);
            label2.TabIndex = 2;
            label2.Text = "Курсовая работа";
            // 
            // buttonNext
            // 
            buttonNext.BackColor = Color.Azure;
            buttonNext.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonNext.Location = new Point(481, 393);
            buttonNext.Name = "buttonNext";
            buttonNext.Size = new Size(441, 60);
            buttonNext.TabIndex = 3;
            buttonNext.Text = "Перейти к программе";
            buttonNext.UseVisualStyleBackColor = false;
            buttonNext.Click += buttonNext_Click;
            // 
            // HelloForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(1378, 654);
            Controls.Add(buttonNext);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelNAME);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1400, 710);
            MinimumSize = new Size(1400, 710);
            Name = "HelloForm";
            Text = "Каневский Глеб 23ВП2 БД \"СКЛАД\"";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNAME;
        private Label label1;
        private Label label2;
        private Button buttonNext;
    }
}
using ZedGraph;

namespace Task4
{
    partial class Graphics
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            zedGraphControl = new ZedGraphControl();
            comboBoxOfTests = new ComboBox();
            comboBoxOfSorts = new ComboBox();
            generate = new Button();
            run = new Button();
            save = new Button();
            comboBoxOfTypes = new ComboBox();
            SuspendLayout();
            // 
            // zedGraphControl
            // 
            zedGraphControl.Location = new Point(457, 35);
            zedGraphControl.Margin = new Padding(4, 5, 4, 5);
            zedGraphControl.Name = "zedGraphControl";
            zedGraphControl.ScrollGrace = 0D;
            zedGraphControl.ScrollMaxX = 0D;
            zedGraphControl.ScrollMaxY = 0D;
            zedGraphControl.ScrollMaxY2 = 0D;
            zedGraphControl.ScrollMinX = 0D;
            zedGraphControl.ScrollMinY = 0D;
            zedGraphControl.ScrollMinY2 = 0D;
            zedGraphControl.Size = new Size(820, 564);
            zedGraphControl.TabIndex = 0;
            zedGraphControl.UseExtendedPrintDialog = true;
            // 
            // comboBoxOfTests
            // 
            comboBoxOfTests.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfTests.Font = new Font("Segoe UI", 12F);
            comboBoxOfTests.FormattingEnabled = true;
            comboBoxOfTests.Items.AddRange(new object[] { "Случайные числа", "Разбитые на подмассивы", "Отсортированные массивы", "Смешанные массивы" });
            comboBoxOfTests.Location = new Point(40, 115);
            comboBoxOfTests.Name = "comboBoxOfTests";
            comboBoxOfTests.Size = new Size(350, 36);
            comboBoxOfTests.TabIndex = 1;
            comboBoxOfTests.SelectedIndexChanged += comboBoxOfTests_SelectedIndexChanged;
            // 
            // comboBoxOfSorts
            // 
            comboBoxOfSorts.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfSorts.Font = new Font("Segoe UI", 12F);
            comboBoxOfSorts.FormattingEnabled = true;
            comboBoxOfSorts.Items.AddRange(new object[] { "Первая группа сортировок", "Вторая группа сортировок", "Третья группа сортировок" });
            comboBoxOfSorts.Location = new Point(40, 40);
            comboBoxOfSorts.Name = "comboBoxOfSorts";
            comboBoxOfSorts.Size = new Size(350, 36);
            comboBoxOfSorts.TabIndex = 2;
            // 
            // generate
            // 
            generate.Font = new Font("Segoe UI", 12F);
            generate.Location = new Point(40, 440);
            generate.Name = "generate";
            generate.Size = new Size(350, 60);
            generate.TabIndex = 3;
            generate.Text = "Сгенерировать массивы";
            generate.UseVisualStyleBackColor = true;
            generate.Click += generate_Click;
            // 
            // run
            // 
            run.Font = new Font("Segoe UI", 12F);
            run.Location = new Point(40, 506);
            run.Name = "run";
            run.Size = new Size(350, 60);
            run.TabIndex = 4;
            run.Text = "Запустить тесты";
            run.UseVisualStyleBackColor = true;
            run.Click += run_Click;
            // 
            // save
            // 
            save.Font = new Font("Segoe UI", 12F);
            save.Location = new Point(40, 570);
            save.Name = "save";
            save.Size = new Size(350, 60);
            save.TabIndex = 5;
            save.Text = "Сохранить результаты";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // comboBoxOfTypes
            // 
            comboBoxOfTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfTypes.Font = new Font("Segoe UI", 12F);
            comboBoxOfTypes.FormattingEnabled = true;
            comboBoxOfTypes.Items.AddRange(new object[] { "Целые числа", "Вещественные числа", "Строки", "Даты" });
            comboBoxOfTypes.SelectedItem = "Целые числа";
            comboBoxOfTypes.Location = new Point(1365, 40);
            comboBoxOfTypes.Name = "comboBoxOfTypes";
            comboBoxOfTypes.Size = new Size(230, 36);
            comboBoxOfTypes.TabIndex = 6;
            // 
            // Graphics
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1642, 649);
            Controls.Add(comboBoxOfTypes);
            Controls.Add(save);
            Controls.Add(run);
            Controls.Add(generate);
            Controls.Add(comboBoxOfSorts);
            Controls.Add(comboBoxOfTests);
            Controls.Add(zedGraphControl);
            Name = "Graphics";
            Text = "Graphics";
            ResumeLayout(false);
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;
        private ComboBox comboBoxOfTests;
        private ComboBox comboBoxOfSorts;
        private Button generate;
        private Button run;
        private Button save;
        private ComboBox comboBoxOfTypes;
    }
}

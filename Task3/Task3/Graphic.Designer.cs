namespace Task3
{
    partial class Graphic
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
            zedGraphControl = new ZedGraph.ZedGraphControl();
            comboBoxOfTests = new ComboBox();
            comboBoxOfSorts = new ComboBox();
            generate = new Button();
            run = new Button();
            save = new Button();
            SuspendLayout();
            // 
            // zedGraphControl
            // 
            zedGraphControl.Location = new Point(391, 35);
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
            zedGraphControl.GraphPane.Title.Text = "Зависимость времени выполнения сортировок от размера массива";
            zedGraphControl.GraphPane.XAxis.Title.Text = "Размер массива, в десятках штук";
            zedGraphControl.GraphPane.YAxis.Title.Text = "Время выполнения, мс";
            // 
            // comboBoxOfTests
            // 
            comboBoxOfTests.FormattingEnabled = true;
            comboBoxOfTests.Items.AddRange(new object[] { "Случайные числа", "Разбитые на подмассивы", "Отсортированные массивы", "Смешанные массивы" });
            comboBoxOfTests.Location = new Point(40, 115);
            comboBoxOfTests.Name = "comboBoxOfTests";
            comboBoxOfTests.Text = "Выберите набор тестовых данных";
            comboBoxOfTests.Size = new Size(300, 28);
            comboBoxOfTests.TabIndex = 1;
            comboBoxOfTests.SelectedIndexChanged += comboBoxOfTests_SelectedIndexChanged;
            // 
            // comboBoxOfSorts
            // 
            comboBoxOfSorts.FormattingEnabled = true;
            comboBoxOfSorts.Items.AddRange(new object[] { "Первая группа", "Вторая группа", "Третья группа" });
            comboBoxOfSorts.Location = new Point(40, 40);
            comboBoxOfSorts.Name = "comboBoxOfSorts";
            comboBoxOfSorts.Text = "Выберите группу сортировок";
            comboBoxOfSorts.Size = new Size(300, 28);
            comboBoxOfSorts.TabIndex = 2;
            // 
            // generate
            // 
            generate.Location = new Point(40, 440);
            generate.Name = "generate";
            generate.Size = new Size(300, 29);
            generate.TabIndex = 3;
            generate.Text = "Сгенерировать массивы";
            generate.UseVisualStyleBackColor = true;
            generate.Click += generate_Click;
            // 
            // run
            // 
            run.Location = new Point(40, 506);
            run.Name = "run";
            run.Size = new Size(300, 29);
            run.TabIndex = 4;
            run.Text = "Запустить тесты";
            run.UseVisualStyleBackColor = true;
            run.Click += run_Click;
            // 
            // save
            // 
            save.Location = new Point(40, 570);
            save.Name = "save";
            save.Size = new Size(300, 29);
            save.TabIndex = 5;
            save.Text = "Сохранить результаты";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // Graphic
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 649);
            Controls.Add(save);
            Controls.Add(run);
            Controls.Add(generate);
            Controls.Add(comboBoxOfSorts);
            Controls.Add(comboBoxOfTests);
            Controls.Add(zedGraphControl);
            Name = "Graphic";
            Text = "Graphic";
            ResumeLayout(false);
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;
        private ComboBox comboBoxOfTests;
        private ComboBox comboBoxOfSorts;
        private Button generate;
        private Button run;
        private Button save;
    }
}

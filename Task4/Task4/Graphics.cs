using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ZedGraph;
namespace Task4
{
    public partial class Graphics : Form
    {
        int group = 0;
        int sorts = 0;
        IComparer<SomeObject> globalComparer;
        SomeObject[][][] tests;
        SomeObject[][][] savedTests;
        delegate void Sort(SomeObject[] arr, IComparer<SomeObject> comparer = null);
        System.Windows.Forms.Label countL = new System.Windows.Forms.Label();
        System.Windows.Forms.Label countMP = new System.Windows.Forms.Label();
        TextBox count = new TextBox();
        TextBox module = new TextBox();
        TextBox permutations = new TextBox();
        CheckBox sorted = new CheckBox();
        CheckBox reversed = new CheckBox();
        CheckBox repeat = new CheckBox();
        ComboBox repeats = new ComboBox();
        System.Windows.Forms.Label error = new System.Windows.Forms.Label();
        public Graphics()
        {
            InitializeComponent();
            zedGraphControl.GraphPane.Title.Text = "Зависимость времени выполнения сортировок от размера массива";
            zedGraphControl.GraphPane.XAxis.Title.Text = "Размер массива, в штучечках";
            zedGraphControl.GraphPane.YAxis.Title.Text = "Время выполнения, мс";
            comboBoxOfTests.Text = "Выберите группу сортировок";
            comboBoxOfSorts.Text = "Выберите набор тестовых данных";
            comboBoxOfTypes.Text = "Выберите тип данных";
        }

        private void comboBoxOfTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (group != 0)
            {
                this.Controls.Clear();
                this.Controls.Add(comboBoxOfTests);
                this.Controls.Add(comboBoxOfSorts);
                this.Controls.Add(comboBoxOfTypes);
                this.Controls.Add(zedGraphControl);
                this.Controls.Add(generate);
                this.Controls.Add(run);
                this.Controls.Add(save);
            }
            string tests = comboBoxOfTests.SelectedItem.ToString();
            if (tests == "Случайные числа")
            {
                group = 1;
            }
            if (tests == "Разбитые на подмассивы")
            {
                countL.Text = "Число массивов";
                countL.Location = new Point(40, 195);
                countL.Size = new Size(150, 60);
                countL.Font = new Font("Segoe UI", 12F);
                this.Controls.Add(countL);
                count.Location = new Point(200, 195);
                this.Controls.Add(count);
                countMP.Text = "Макс размер";
                countMP.Location = new Point(40, 275);
                countMP.Size = new Size(150, 60);
                countMP.Font = new Font("Segoe UI", 12F);
                this.Controls.Add(countMP);
                module.Location = new Point(200, 275);
                this.Controls.Add(module);
                group = 2;
            }
            if (tests == "Отсортированные массивы")
            {
                countL.Text = "Число массивов";
                countL.Location = new Point(40, 195);
                countL.Size = new Size(150, 60);
                countL.Font = new Font("Segoe UI", 12F);
                this.Controls.Add(countL);
                count.Location = new Point(200, 195);
                this.Controls.Add(count);
                countMP.Text = "Число перестановок";
                countMP.Location = new Point(40, 275);
                countMP.Size = new Size(160, 60);
                countMP.Font = new Font("Segoe UI", 12F);
                this.Controls.Add(countMP);
                permutations.Location = new Point(200, 275);
                this.Controls.Add(permutations);
                group = 3;
            }
            if (tests == "Смешанные массивы")
            {
                countL.Text = "Число массивов";
                countL.Location = new Point(40, 195);
                countL.Size = new Size(150, 60);
                countL.Font = new Font("Segoe UI", 12F);
                this.Controls.Add(countL);
                count.Location = new Point(200, 195);
                this.Controls.Add(count);
                sorted.Location = new Point(40, 275);
                sorted.Text = "Сорт";
                sorted.Size = new Size(120, 60);
                sorted.Font = new Font("Segoe UI", 12F);
                this.Controls.Add(sorted);
                reversed.Location = new Point(160, 275);
                reversed.Text = "Обратн";
                reversed.Size = new Size(120, 60);
                reversed.Font = new Font("Segoe UI", 12F);
                this.Controls.Add(reversed);
                repeat.Location = new Point(280, 275);
                repeat.Text = "Повторы";
                repeat.Size = new Size(120, 60);
                repeat.Font = new Font("Segoe UI", 12F);
                this.Controls.Add(repeat);
                repeats.Location = new Point(40, 355);
                repeats.Text = "Число повторений";
                repeats.Size = new Size(300, 60);
                repeats.Font = new Font("Segoe UI", 12F);
                repeats.Items.AddRange(new string[] { "10%", "25%", "50%", "75%", "90%" });
                this.Controls.Add(repeats);
                group = 4;
            }
        }
        private void generate_Click(object sender, EventArgs e)
        {
            if (comboBoxOfSorts.SelectedItem.ToString == null)
            {
                error.Location = new Point(100, 270);
                error.Text = "Ошибка: выберете сортировку!";
                this.Controls.Add(error);
                Thread.Sleep(3000);
                this.Controls.Remove(error);
                return;
            }
            generate.Enabled = false;
            run.Enabled = false;
            save.Enabled = false;
            string s = comboBoxOfSorts.SelectedItem.ToString();
            if (s == "Первая группа сортировок") sorts = 1;
            if (s == "Вторая группа сортировок") sorts = 2;
            if (s == "Третья группа сортировок") sorts = 3;
            s = comboBoxOfTypes.SelectedItem.ToString();
            if (s == "Целые числа") globalComparer = ObjectComparers.DataComparer;
            if (s == "Вещественные числа") globalComparer = ObjectComparers.DDataComparer;
            if (s == "Строки") globalComparer = ObjectComparers.NameComparer;
            if (s == "Даты") globalComparer = ObjectComparers.DatetimeComparer;
            switch (group)
            {
                case 0:
                    error.Location = new Point(100, 270);
                    error.Text = "Ошибка: выберете тип массива!";
                    this.Controls.Add(error);
                    Thread.Sleep(3000);
                    this.Controls.Remove(error);
                    break;
                case 1:
                    tests = new SomeObject[3 + sorts][][];
                    for (int i = 0; i < 3 + sorts; ++i)
                    {
                        tests[i] = new SomeObject[1][];
                        tests[i][0] = new SomeObject[(int)Math.Pow(10, 1 + i)];
                        TestGroups.TestGroup1 test = new TestGroups.TestGroup1((int)Math.Pow(10, 1 + i));
                        tests[i][0] = test.testArray;
                    }
                    break;
                case 2:
                    int n = int.Parse(count.Text);
                    int[] data = new int[1]; data[0] = int.Parse(module.Text);
                    tests = new SomeObject[3 + sorts][][];
                    for (int i = 0; i < 3 + sorts; ++i)
                    {
                        tests[i] = new SomeObject[n][];
                        for (int j = 0; j < n; ++j)
                            tests[i][j] = new SomeObject[(int)Math.Pow(10, 1 + i)];
                        TestGroups.TestGroup2 test = new TestGroups.TestGroup2((int)Math.Pow(10, 1 + i), n, data[0], globalComparer);
                        tests[i] = test.testArray;
                    }
                    break;
                case 3:
                    n = int.Parse(count.Text);
                    data = new int[1]; data[0] = int.Parse(permutations.Text);
                    tests = new SomeObject[3 + sorts][][];
                    for (int i = 0; i < 3 + sorts; ++i)
                    {
                        tests[i] = new SomeObject[n][];
                        for (int j = 0; j < n; ++j)
                            tests[i][j] = new SomeObject[(int)Math.Pow(10, 1 + i)];
                        TestGroups.TestGroup3 test = new TestGroups.TestGroup3((int)Math.Pow(10, 1 + i), n, data[0], globalComparer);
                        tests[i] = test.testArray;
                    }
                    break;
                case 4:
                    n = int.Parse(count.Text);
                    if (repeat.Checked)
                        data = new int[3];
                    else data = new int[2];
                    if (sorted.Checked)
                        if (reversed.Checked)
                            data[0] = 1;
                        else data[0] = 2;
                    else data[0] = 0;
                    if (permutations.Text != null)
                        data[0] = 1;
                    if (repeat.Checked)
                        data[2] = int.Parse(repeats.SelectedItem.ToString().Remove(2));
                    tests = new SomeObject[3 + sorts][][];
                    for (int i = 0; i < 3 + sorts; ++i)
                    {
                        tests[i] = new SomeObject[n][];
                        for (int j = 0; j < n; ++j)
                            tests[i][j] = new SomeObject[(int)Math.Pow(10, 1 + i)];
                        TestGroups.TestGroup4 test = new TestGroups.TestGroup4((int)Math.Pow(10, 1 + i), n, data, globalComparer);
                        tests[i] = test.testArray;
                    }
                    break;
            }
            savedTests = tests;
            generate.Enabled = true;
            run.Enabled = true;
            save.Enabled = true;
        }

        private double[][] OneTest(Sort sortList, int arr)
        {
            int m;
            double[][] results = new double[arr][];
            for (int i = 0; i < arr; ++i)
                results[i] = new double[tests.Length];
            for (int i = 0; i < arr; ++i)
                for (int j = 0; j < tests.Length; ++j)
                    results[i][j] = 0;
            for (int i = 0; i < tests.Length; ++i)
                for (int j = 0; j < tests[0].Length; ++j)
                    for (int k = 0; k < 20; ++k)
                    {
                        m = 0;
                        foreach (Sort sort in sortList.GetInvocationList())
                        {
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();
                            sort(tests[i][j], globalComparer);
                            stopwatch.Stop();
                            tests = savedTests;
                            results[m][i] += stopwatch.ElapsedMilliseconds;
                            ++m;
                        }
                    }
            for (int k = 0; k < arr; ++k)
                for (int j = 0; j < tests.Length; ++j)
                    results[k][j] /= tests[0].Length * 20.0;
            return results;
        }
        private double[][] Test()
        {
            Sort sort = null;
            double[][] results;
            switch (sorts)
            {
                case 1:
                    sort += Sortings.BubbleSort;
                    sort += Sortings.InsertionSort;
                    sort += Sortings.SelectionSort;
                    sort += Sortings.ShakerSort;
                    sort += Sortings.GnomeSort;
                    results = OneTest(sort, 5);
                    break;
                case 2:
                    sort += Sortings.BitonicSort;
                    sort += Sortings.Shellsort;
                    sort += Sortings.TreeSort;
                    results = OneTest(sort, 3);
                    break;
                case 3:
                    sort += Sortings.CombSort;
                    sort += Sortings.Heapsort;
                    sort += Sortings.QuickSort;
                    sort += Sortings.MergeSort;
                    sort += Sortings.RadixSort;
                    results = OneTest(sort, 5);
                    break;
                default:
                    results = new double[0][];
                    break;
            }
            return results;
        }
        private void run_Click(object sender, EventArgs e)
        {
            generate.Enabled = false;
            run.Enabled = false;
            save.Enabled = false;
            double[][] results = Test();
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            PointPairList list;
            switch (sorts)
            {
                case 1:
                    list = new PointPairList();
                    for (int i = 0; i < results[0].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[0][i]);
                    LineItem lineB = pane.AddCurve("Сортировка пузырьком", list, Color.Blue, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[1].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[1][i]);
                    LineItem lineI = pane.AddCurve("Сортировка вставками", list, Color.Red, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[2].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[2][i]);
                    LineItem lineS = pane.AddCurve("Сортировка выбором", list, Color.Green, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[3].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[3][i]);
                    LineItem lineSha = pane.AddCurve("Шейкерная сортировка", list, Color.Purple, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[4].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[4][i]);
                    LineItem lineG = pane.AddCurve("Гномья сортировка", list, Color.Yellow, SymbolType.Default);
                    break;
                case 2:
                    list = new PointPairList();
                    for (int i = 0; i < results[0].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[0][i]);
                    LineItem lineBi = pane.AddCurve("Битонная сортировка", list, Color.Blue, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[1].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[1][i]);
                    LineItem lineShe = pane.AddCurve("Сортировка Шелла", list, Color.Red, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[2].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[2][i]);
                    LineItem lineT = pane.AddCurve("Сортировка деревом", list, Color.Green, SymbolType.Default);
                    break;
                case 3:
                    list = new PointPairList();
                    for (int i = 0; i < results[0].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[0][i]);
                    LineItem lineC = pane.AddCurve("Сортировка расчёской", list, Color.Blue, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[1].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[1][i]);
                    LineItem lineH = pane.AddCurve("Пирамидальная сортировка", list, Color.Red, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[2].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[2][i]);
                    LineItem lineQ = pane.AddCurve("Быстрая сортировка", list, Color.Green, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[3].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[3][i]);
                    LineItem lineM = pane.AddCurve("Сортировка слиянием", list, Color.Purple, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[4].Length; ++i)
                        list.Add(Math.Pow(10, i + 1), results[4][i]);
                    LineItem lineR = pane.AddCurve("Поразрядная сортировка", list, Color.Yellow, SymbolType.Default);
                    break;
            }
            pane.XAxis.Scale.Max = Math.Pow(10, results[0].Length);
            for (int i = 0; i < results.Length; ++i)
                pane.YAxis.Scale.Max = Math.Max(pane.YAxis.Scale.Max, results[i][results[i].Length - 1]);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
            generate.Enabled = true;
            run.Enabled = true;
            save.Enabled = true;
        }
        private void save_Click(object sender, EventArgs e)
        {
            generate.Enabled = false;
            run.Enabled = false;
            save.Enabled = false;
            using (StreamWriter sw = new StreamWriter("results.txt", false))
            {
                sw.WriteLine("Сгенерированные массивы:\n");
                for (int i = 0; i < savedTests.Length; ++i)
                    for (int j = 0; j < savedTests[i].Length; ++j)
                    {
                        for (int k = 0; k < savedTests[i][j].Length; ++k)
                            sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                        sw.WriteLine();
                    }
                sw.WriteLine("\nОтсортированные версии:\n");
                switch (sorts)
                {
                    case 1:
                        tests = savedTests;
                        sw.WriteLine("\nСортировка пузырьком: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.BubbleSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка вставками: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.InsertionSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка выбором: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.SelectionSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nШейкерная сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.ShakerSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nГномья сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.GnomeSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        break;
                    case 2:
                        tests = savedTests;
                        sw.WriteLine("\nБитонная сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.BitonicSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка Шелла: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.Shellsort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка деревом: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.TreeSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        break;
                    case 3:
                        tests = savedTests;
                        sw.WriteLine("\nСортировка расчёской: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.CombSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nПирамидальная сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.Heapsort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nБыстрая сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.QuickSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка слиянием: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.MergeSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nПоразрядная сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.RadixSort(tests[i][j], globalComparer);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(savedTests[i][j][k].Name + " " + savedTests[i][j][k].Datetime + " " + savedTests[i][j][k].Data + " " + savedTests[i][j][k].DData + "\n");
                                sw.WriteLine();
                            }
                        break;
                }
            }
            generate.Enabled = true;
            run.Enabled = true;
            save.Enabled = true;
        }
    }
    public class SomeObject
    {
        public string Name { get; set; } = string.Empty;
        public int Data { get; set; }
        public double DData { get; set; }
        public DateTime Datetime { get; set; }
        public SomeObject()
        {
            Name = string.Empty;
            Data = 0;
            DData = 0.0;
            Datetime = DateTime.MinValue;
        }
        public SomeObject(string name, int data, double ddata, DateTime datetime)
        {
            Name = name;
            Data = data;
            DData = ddata;
            Datetime = datetime;
        }
    }
    public class ObjectComparers
    {
        public static IComparer<SomeObject> NameComparer { get; } = new NameComparerC();
        public static IComparer<SomeObject> DataComparer { get; } = new DataComparerC();
        public static IComparer<SomeObject> DDataComparer { get; } = new DDataComparerC();
        public static IComparer<SomeObject> DatetimeComparer { get; } = new DatetimeComparerC();
        private class NameComparerC : IComparer<SomeObject>
        {
            public int Compare(SomeObject x, SomeObject y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }
        private class DataComparerC : IComparer<SomeObject>
        {
            public int Compare(SomeObject x, SomeObject y)
            {
                return x.Data.CompareTo(y.Data);
            }
        }
        private class DDataComparerC : IComparer<SomeObject>
        {
            public int Compare(SomeObject x, SomeObject y)
            {
                return x.DData.CompareTo(y.DData);
            }
        }
        private class DatetimeComparerC : IComparer<SomeObject>
        {
            public int Compare(SomeObject x, SomeObject y)
            {
                return x.Datetime.CompareTo(y.Datetime);
            }
        }
    }
    public class Sortings
    {
        public static void BubbleSort(SomeObject[] arr, IComparer<SomeObject> comparer = null)    //O(n^2)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            SomeObject r;
            for (int i = 0; i < arr.Length - 1; ++i)
                for (int j = 0; j < arr.Length - i - 1; ++j)
                    if (comparer.Compare(arr[j], arr[j + 1]) > 0)
                    {
                        r = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = r;
                    }
        }

        public static void ShakerSort(SomeObject[] arr, IComparer<SomeObject> comparer = null)    //O(n^2)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            int left = 0, right = arr.Length - 1;
            SomeObject r;
            while (left < right)
            {
                for (int i = left; i < right; ++i)
                    if (comparer.Compare(arr[i], arr[i + 1]) > 0)
                    {
                        r = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = r;
                    }
                --right;
                for (int i = right; i > left; --i)
                    if (comparer.Compare(arr[i], arr[i - 1]) < 0)
                    {
                        r = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = r;
                    }
                ++left;
            }
        }

        public static void CombSort(SomeObject[] arr, IComparer<SomeObject> comparer = null)  //O(n^2)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            int space = arr.Length;
            SomeObject r;
            bool swapped = true;
            while (space > 1 || swapped)
            {
                space = Math.Max(1, space / 5 * 4);
                swapped = false;
                for (int i = 0; i + space < arr.Length; ++i)
                    if (comparer.Compare(arr[i], arr[i + space]) > 0)
                    {
                        r = arr[i];
                        arr[i] = arr[i + space];
                        arr[i + space] = r;
                        swapped = true;
                    }
            }
        }

        public static void InsertionSort(SomeObject[] arr, IComparer<SomeObject> comparer = null) //O(n^2)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            int j;
            SomeObject key;
            for (int i = 1; i < arr.Length; ++i)
            {
                key = arr[i];
                j = i - 1;
                while (j >= 0 && comparer.Compare(arr[j], key) > 0)
                {
                    arr[j + 1] = arr[j]; --j;
                }
                arr[j + 1] = key;
            }
        }

        public static void Shellsort(SomeObject[] arr, IComparer<SomeObject> comparer = null) //O(n^2)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            int j; SomeObject r;
            for (int space = arr.Length / 2; space > 0; space /= 2)
                for (int i = space; i < arr.Length; ++i)
                {
                    r = arr[i];
                    for (j = i; j >= space && comparer.Compare(arr[j - space], r) > 0; j -= space)
                        arr[j] = arr[j - space];
                    arr[j] = r;
                }
        }

        class Tree
        {
            SomeObject?[] tree;
            int Size { get; set; }
            IComparer<SomeObject> comparer;
            public Tree(int size, IComparer<SomeObject> comparer)
            {
                Size = 0;
                tree = new SomeObject?[1 << (size * 2)];
                this.comparer = comparer;
            }
            public void Add(SomeObject x)
            {
                if (Size == 0)
                {
                    tree[0] = x; ++Size; return;
                }
                int i = 0, count = 0;
                while (i < tree.Length && count < tree.Length)
                {
                    ++count;
                    if (tree[i] == null)
                    {
                        tree[i] = x; ++Size; return;
                    }
                    if (comparer.Compare(x, tree[i]) < 0)
                        if (2 * i + 1 < tree.Length)
                            i = 2 * i + 1;
                        else break;
                    else
                        if (2 * i + 2 < tree.Length)
                        i = 2 * i + 2;
                    else break;
                }
            }
            public void LVR(int i, ref int k, SomeObject[] arr)
            {
                if (i >= tree.Length || tree[i] == null) return;
                if (2 * i + 1 < tree.Length && tree[2 * i + 1] != null)
                    LVR(2 * i + 1, ref k, arr);
                arr[k] = (SomeObject)tree[i]; ++k;
                if (2 * i + 2 < tree.Length && tree[2 * i + 2] != null)
                    LVR(2 * i + 2, ref k, arr);

            }
        }

        public static void TreeSort(SomeObject[] arr, IComparer<SomeObject> comparer = null)  //O(n^2)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            Tree tree = new Tree(arr.Length, comparer);
            for (int i = 0; i < arr.Length; ++i)
                tree.Add(arr[i]);
            int k = 0;
            tree.LVR(0, ref k, arr);
        }

        public static void GnomeSort(SomeObject[] arr, IComparer<SomeObject> comparer = null) //O(n^2)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            int i = 1; SomeObject r;
            while (i < arr.Length)
                if (i == 0 || comparer.Compare(arr[i - 1], arr[i]) <= 0)
                {
                    ++i;
                }
                else
                {
                    r = arr[i];
                    arr[i] = arr[i - 1];
                    arr[i - 1] = r;
                    --i;
                }
        }

        public static void SelectionSort(SomeObject[] arr, IComparer<SomeObject> comparer = null) //O(n^2)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            int min; SomeObject r;
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                min = i;
                for (int j = i + 1; j < arr.Length; ++j)
                    if (comparer.Compare(arr[j], arr[min]) < 0)
                        min = j;
                r = arr[i];
                arr[i] = arr[min];
                arr[min] = r;
            }
        }

        static void SiftDownMax(SomeObject[] arr, int ind, int maxIndex, IComparer<SomeObject> comparer)
        {
            while (ind * 2 + 1 < maxIndex)
            {
                int left = ind * 2 + 1;
                int right = ind * 2 + 2;
                int maxChildInd = left;
                if (right < maxIndex && comparer.Compare(arr[right], arr[left]) > 0)
                    maxChildInd = right;
                if (comparer.Compare(arr[ind], arr[maxChildInd]) >= 0)
                    break;
                else
                {
                    SomeObject r = arr[ind];
                    arr[ind] = arr[maxChildInd];
                    arr[maxChildInd] = r;
                    ind = maxChildInd;
                }
            }
        }
        static void Heapify(SomeObject[] arr, IComparer<SomeObject> comparer)
        {
            int startInd = arr.Length / 2 - 1;
            for (int i = startInd; i >= 0; i--)
            {
                SiftDownMax(arr, i, arr.Length, comparer);
            }
        }
        public static void Heapsort(SomeObject[] arr, IComparer<SomeObject> comparer = null)  //O(nlogn)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            Heapify(arr, comparer);
            SomeObject r;
            for (int i = arr.Length - 1; i > 0; --i)
            {
                r = arr[0];
                arr[0] = arr[i];
                arr[i] = r;
                SiftDownMax(arr, 0, i, comparer);
            }
        }

        static int Partition(SomeObject[] arr, int left, int right, SomeObject pivot, IComparer<SomeObject> comparer)
        {
            int m = left; SomeObject r;
            for (int i = left; i < right; ++i)
                if (comparer.Compare(arr[i], pivot) < 0)
                {
                    r = arr[i];
                    arr[i] = arr[m];
                    arr[m] = r; ++m;
                }
            r = arr[right];
            arr[right] = arr[m];
            arr[m] = r;
            return m;
        }
        static void QuickSort(SomeObject[] arr, int left, int right, IComparer<SomeObject> comparer)
        {

            if (left >= right) return;
            Random rand = new Random();
            int pivot = rand.Next(left, right + 1);
            SomeObject r = arr[pivot];
            arr[pivot] = arr[right]; arr[right] = r;
            int m = Partition(arr, left, right, arr[right], comparer);
            QuickSort(arr, left, m - 1, comparer);
            QuickSort(arr, m + 1, right, comparer);
        }
        public static void QuickSort(SomeObject[] arr, IComparer<SomeObject> comparer = null) //O(nlogn)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            QuickSort(arr, 0, arr.Length - 1, comparer);
        }

        static SomeObject[] Merge(SomeObject[] left, SomeObject[] right, IComparer<SomeObject> comparer)
        {
            SomeObject[] res = new SomeObject[left.Length + right.Length];
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                if (comparer.Compare(left[i], right[j]) <= 0)
                {
                    res[k] = left[i]; ++i;
                }
                else
                {
                    res[k] = right[j]; ++j;
                }
                ++k;
            }
            while (i < left.Length)
            {
                res[k] = left[i]; ++i; ++k;
            }
            while (j < right.Length)
            {
                res[k] = right[j]; ++j; ++k;
            }
            return res;
        }
        static SomeObject[] Mergesort(SomeObject[] arr, IComparer<SomeObject> comparer)
        {
            if (arr.Length <= 1) return arr;
            int mid = arr.Length / 2;
            SomeObject[] leftHalf = new SomeObject[mid];
            SomeObject[] rightHalf = new SomeObject[arr.Length - mid];
            for (int i = 0; i < mid; ++i)
                leftHalf[i] = arr[i];
            for (int i = 0; i < arr.Length - mid; ++i)
                rightHalf[i] = arr[mid + i];
            SomeObject[] leftSorted = Mergesort(leftHalf, comparer);
            SomeObject[] rightSorted = Mergesort(rightHalf, comparer);
            return Merge(leftSorted, rightSorted, comparer);
        }
        public static void MergeSort(SomeObject[] arr, IComparer<SomeObject> comparer = null) //O(nlogn)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            arr = Mergesort(arr, comparer);
        }

        static void CountingSort(SomeObject[] arr, SomeObject radix)
        {
            int[] count = new int[10];
            for (int i = 0; i < 10; ++i)
                count[i] = 0;
            for (int i = 0; i < arr.Length; ++i)
                ++count[arr[i].Data / radix.Data % 10];
            for (int i = 1; i < 10; ++i)
                count[i] += count[i - 1];
            SomeObject[] sorts = new SomeObject[arr.Length];
            for (int i = arr.Length - 1; i >= 0; --i)
            {
                sorts[count[arr[i].Data / radix.Data % 10] - 1] = arr[i];
                --count[arr[i].Data / radix.Data % 10];
            }
            Array.Copy(sorts, arr, arr.Length);
        }
        public static void RadixSort(SomeObject[] arr, IComparer<SomeObject> comparer) //O(nlogn)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            if (comparer == ObjectComparers.DataComparer)
            {
                SomeObject max = arr.Max(comparer);
                SomeObject radix = new SomeObject();
                radix.Data = 1;
                while (max.Data / radix.Data > 0)
                {
                    CountingSort(arr, radix);
                    radix.Data *= 10;
                }
            }
            else return;
        }

        static void BitonicMerge(SomeObject[] arr, int left, int n, bool up, IComparer<SomeObject> comparer)
        {
            if (n < 2) return;
            int k = n / 2; SomeObject r;
            for (int i = left; i < left + k; ++i)
                if (up == (comparer.Compare(arr[i], arr[i + k]) > 0))
                {
                    r = arr[i];
                    arr[i] = arr[i + k];
                    arr[i + k] = r;
                }
            BitonicMerge(arr, left, k, up, comparer);
            BitonicMerge(arr, left + k, k, up, comparer);
        }
        static void Bitonicsort(SomeObject[] arr, int left, int n, bool up, IComparer<SomeObject> comparer)
        {
            if (n < 2) return;
            int k = n / 2;
            Bitonicsort(arr, left, k, true, comparer);
            Bitonicsort(arr, left + k, k, false, comparer);
            BitonicMerge(arr, left, n, up, comparer);
        }
        public static void BitonicSort(SomeObject[] arr, IComparer<SomeObject> comparer = null)   //О(logn*logn)
        {
            if (comparer == null) comparer = ObjectComparers.DataComparer;
            Bitonicsort(arr, 0, arr.Length, true, comparer);
        }
    }
    public class TestGroups
    {
        const int NAME_LENGTH_MODULE = 256;
        public class TestGroup1
        {
            public SomeObject[] testArray;
            const int MODULE = 1000;

            public TestGroup1(int size)
            {
                testArray = new SomeObject[size];
                Random rand = new Random();
                int n;
                for (int i = 0; i < size; ++i)
                {
                    testArray[i] = new SomeObject();
                    n = rand.Next(0, NAME_LENGTH_MODULE);
                    for (int j = 0; j < n; ++j)
                        testArray[i].Name += (char)rand.Next(0, NAME_LENGTH_MODULE);
                    testArray[i].Data = rand.Next(0, MODULE);
                    testArray[i].DData = rand.NextDouble();
                    testArray[i].Datetime = DateTime.MinValue.AddSeconds(rand.Next());
                }
            }
        }
        public class TestGroup2
        {
            public SomeObject[][] testArray;
            public TestGroup2(int size, int count, int module, IComparer<SomeObject> comparer)
            {
                testArray = new SomeObject[count][];
                Random rand = new Random();
                for (int t = 0; t < count; ++t)
                {
                    testArray[t] = new SomeObject[size];
                    int sizeOfSorted = rand.Next(1, module);
                    SomeObject[] sortedPart; int n;
                    for (int i = 0; i < size; i += sizeOfSorted)
                    {
                        sortedPart = new SomeObject[sizeOfSorted];
                        for (int j = 0; j < sizeOfSorted; ++j)
                        {
                            sortedPart[j] = new SomeObject();
                            n = rand.Next(0, NAME_LENGTH_MODULE);
                            for (int k = 0; k < n; ++k)
                                sortedPart[j].Name += (char)rand.Next(0, NAME_LENGTH_MODULE);
                            sortedPart[j].Data = rand.Next();
                            sortedPart[j].DData = rand.NextDouble();
                            sortedPart[j].Datetime = DateTime.MinValue.AddSeconds(rand.Next());
                        }
                        Array.Sort(sortedPart, comparer);
                        for (int j = i; j < i + sizeOfSorted && j < size; ++j)
                            testArray[t][j] = sortedPart[j - i];
                    }
                }
            }
        }
        public class TestGroup3
        {
            public SomeObject[][] testArray;
            public TestGroup3(int size, int count, int permutations, IComparer<SomeObject> comparer)
            {
                testArray = new SomeObject[count][];
                for (int t = 0; t < count; ++t)
                {
                    testArray[t] = new SomeObject[size];
                    Random rand = new Random();
                    int n;
                    for (int i = 0; i < size; ++i)
                    {
                        testArray[t][i] = new SomeObject();
                        n = rand.Next(0, NAME_LENGTH_MODULE);
                        for (int ka = 0; ka < n; ++ka)
                            testArray[t][i].Name += (char)rand.Next(0, NAME_LENGTH_MODULE);
                        testArray[t][i].Data = rand.Next();
                        testArray[t][i].DData = rand.NextDouble();
                        testArray[t][i].Datetime = DateTime.MinValue.AddSeconds(rand.Next());
                    }
                    Array.Sort(testArray[t], comparer);
                    int j, k; SomeObject r;
                    for (int i = 0; i < permutations; ++i)
                    {
                        j = rand.Next(0, size);
                        k = rand.Next(0, size);
                        r = testArray[t][j];
                        testArray[t][j] = testArray[t][k];
                        testArray[t][k] = r;
                    }
                }
            }
        }
        public class TestGroup4
        {
            public SomeObject[][] testArray;
            const int MODULE = 1000;
            public TestGroup4(int size, int count, int[] commands, IComparer<SomeObject> comparer)
            {
                testArray = new SomeObject[count][];
                for (int t = 0; t < count; ++t)
                {
                    testArray[t] = new SomeObject[size];
                    int n;
                    Random rand = new Random();
                    if (commands.Length == 2)
                        for (int i = 0; i < size; ++i)
                        {
                            testArray[t][i] = new SomeObject();
                            n = rand.Next(0, NAME_LENGTH_MODULE);
                            for (int ka = 0; ka < n; ++ka)
                                testArray[t][i].Name += (char)rand.Next(0, NAME_LENGTH_MODULE);
                            testArray[t][i].Data = rand.Next();
                            testArray[t][i].DData = rand.NextDouble();
                            testArray[t][i].Datetime = DateTime.MinValue.AddSeconds(rand.Next());
                        }
                    else
                    {
                        SomeObject repeater = new SomeObject();
                        n = rand.Next(0, NAME_LENGTH_MODULE);
                        for (int ka = 0; ka < n; ++ka)
                            repeater.Name += (char)rand.Next(0, NAME_LENGTH_MODULE);
                        repeater.Data = rand.Next();
                        repeater.DData = rand.NextDouble();
                        repeater.Datetime = DateTime.MinValue.AddSeconds(rand.Next());
                        int i = 0;
                        for (; i < size * commands[2] / 100; ++i)
                            testArray[t][i] = repeater;
                        for (; i < size; ++i)
                        {
                            testArray[t][i] = new SomeObject();
                            n = rand.Next(0, NAME_LENGTH_MODULE);
                            for (int ka = 0; ka < n; ++ka)
                                testArray[t][i].Name += (char)rand.Next(0, NAME_LENGTH_MODULE);
                            testArray[t][i].Data = rand.Next();
                            testArray[t][i].DData = rand.NextDouble();
                            testArray[t][i].Datetime = DateTime.MinValue.AddSeconds(rand.Next());
                        }
                    }
                    if (commands[0] != 0)
                        Array.Sort(testArray[t], comparer);
                    if (commands[0] == 1)
                        Array.Reverse(testArray[t]);
                    if (commands[1] != 0)
                    {
                        int permutations = rand.Next(0, MODULE);
                        int j, k; SomeObject r;
                        for (int i = 0; i < permutations; ++i)
                        {
                            j = rand.Next(0, size);
                            k = rand.Next(0, size);
                            r = testArray[t][j];
                            testArray[t][j] = testArray[t][k];
                            testArray[t][k] = r;
                        }
                    }
                }
            }
        }
    }
}
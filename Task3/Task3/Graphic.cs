using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using ZedGraph;
namespace Task3
{
    public partial class Graphic : Form
    {
        int group = 0;
        int sorts = 0;
        int[][][] tests;
        int[][][] savedTests;
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
        public Graphic()
        {
            InitializeComponent();
        }

        private void comboBoxOfTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (group != 0)
            {
                this.Controls.Clear();
                this.Controls.Add(comboBoxOfTests);
                this.Controls.Add(comboBoxOfSorts);
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
                countL.Size = new Size(150, 29);
                this.Controls.Add(countL);
                count.Location = new Point(200, 195);
                this.Controls.Add(count);
                countMP.Text = "Макс размер";
                countMP.Location = new Point(40, 275);
                countMP.Size = new Size(150, 29);
                this.Controls.Add(countMP);
                module.Location = new Point(200, 275);
                this.Controls.Add(module);
                group = 2;
            }
            if (tests == "Отсортированные массивы")
            {
                countL.Text = "Число массивов";
                countL.Location = new Point(40, 195);
                countL.Size = new Size(150, 29);
                this.Controls.Add(countL);
                count.Location = new Point(200, 195);
                this.Controls.Add(count);
                countMP.Text = "Число перестановок";
                countMP.Location = new Point(40, 275);
                countMP.Size = new Size(160, 29);
                this.Controls.Add(countMP);
                permutations.Location = new Point(200, 275);
                this.Controls.Add(permutations);
                group = 3;
            }
            if (tests == "Смешанные массивы")
            {
                countL.Text = "Число массивов";
                countL.Location = new Point(40, 195);
                countL.Size = new Size(150, 29);
                this.Controls.Add(countL);
                count.Location = new Point(200, 195);
                this.Controls.Add(count);
                sorted.Location = new Point(40, 275);
                sorted.Text = "Сорт";
                this.Controls.Add(sorted);
                reversed.Location = new Point(160, 275);
                reversed.Text = "Обратн";
                this.Controls.Add(reversed);
                repeat.Location = new Point(280, 275);
                repeat.Text = "Повторы";
                this.Controls.Add(repeat);
                repeats.Location = new Point(40, 355);
                repeats.Text = "Число повторений";
                repeats.Size = new Size(300, 29);
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
            string s = comboBoxOfSorts.SelectedItem.ToString();
            if (s == "Первая группа") sorts = 1;
            if (s == "Вторая группа") sorts = 2;
            if (s == "Третья группа") sorts = 3;
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
                    tests = new int[3 + sorts][][];
                    for (int i = 0; i < 3 + sorts; ++i)
                    {
                        tests[i] = new int[1][];
                        tests[i][0] = new int[(int)Math.Pow(10, 1 + i)];
                        TestGroups.TestGroup1 test = new TestGroups.TestGroup1((int)Math.Pow(10, 1 + i));
                        tests[i][0] = test.testArray;
                    }
                    break;
                case 2:
                    int n = int.Parse(count.Text);
                    int[] data = new int[1]; data[0] = int.Parse(module.Text);
                    tests = new int[3 + sorts][][];
                    for (int i = 0; i < 3 + sorts; ++i)
                    {
                        tests[i] = new int[n][];
                        for (int j = 0; j < n; ++j)
                            tests[i][j] = new int[(int)Math.Pow(10, 1 + i)];
                        TestGroups.TestGroup2 test = new TestGroups.TestGroup2((int)Math.Pow(10, 1 + i), n, data[0]);
                        tests[i] = test.testArray;
                    }
                    break;
                case 3:
                    n = int.Parse(count.Text);
                    data = new int[1]; data[0] = int.Parse(permutations.Text);
                    tests = new int[3 + sorts][][];
                    for (int i = 0; i < 3 + sorts; ++i)
                    {
                        tests[i] = new int[n][];
                        for (int j = 0; j < n; ++j)
                            tests[i][j] = new int[(int)Math.Pow(10, 1 + i)];
                        TestGroups.TestGroup3 test = new TestGroups.TestGroup3((int)Math.Pow(10, 1 + i), n, data[0]);
                        tests[i] = test.testArray;
                    }
                    break;
                case 4:
                    n = int.Parse(count.Text);
                    if (repeat.Checked) data = new int[3];
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
                    tests = new int[3 + sorts][][];
                    for (int i = 0; i < 3 + sorts; ++i)
                    {
                        tests[i] = new int[n][];
                        for (int j = 0; j < n; ++j)
                            tests[i][j] = new int[(int)Math.Pow(10, 1 + i)];
                        TestGroups.TestGroup4 test = new TestGroups.TestGroup4((int)Math.Pow(10, 1 + i), n, data);
                        tests[i] = test.testArray;
                    }
                    break;
            }
            savedTests = tests;
        }
        private double[][] Test()
        {
            Stopwatch stopwatch;
            double[][] results;

            switch (sorts)
            {
                case 1:
                    results = new double[5][];
                    for (int i = 0; i < 5; ++i)
                        results[i] = new double[tests.Length];
                    for (int i = 0; i < 5; ++i)
                        for (int j = 0; j < tests.Length; ++j)
                            results[i][j] = 0;
                    for (int i = 0; i < tests.Length; ++i)
                        for (int j = 0; j < tests[0].Length; ++j)
                            for (int k = 0; k < 20; ++k)
                            {
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.BubbleSort(tests[i][j]);
                                stopwatch.Stop();
                                results[0][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.InsertionSort(tests[i][j]);
                                stopwatch.Stop();
                                results[1][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.SelectionSort(tests[i][j]);
                                stopwatch.Stop();
                                results[2][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.ShakerSort(tests[i][j]);
                                stopwatch.Stop();
                                results[3][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.GnomeSort(tests[i][j]);
                                stopwatch.Stop();
                                results[4][i] += stopwatch.ElapsedMilliseconds;
                            }
                    for (int k = 0; k < 5; ++k)
                        for (int j = 0; j < tests.Length; ++j)
                            results[k][j] /= tests[0].Length * 20.0;
                    break;
                case 2:
                    results = new double[3][];
                    for (int i = 0; i < 3; ++i)
                        results[i] = new double[tests.Length];
                    for (int i = 0; i < 3; ++i)
                        for (int j = 0; j < tests.Length; ++j)
                            results[i][j] = 0;
                    for (int i = 0; i < tests.Length; ++i)
                        for (int j = 0; j < tests[0].Length; ++j)
                            for (int k = 0; k < 20; ++k)
                            {
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.Shellsort(tests[i][j]);
                                stopwatch.Stop();
                                results[1][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.TreeSort(tests[i][j]);
                                stopwatch.Stop();
                                results[2][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.BitonicSort(tests[i][j]);
                                stopwatch.Stop();
                                results[0][i] += stopwatch.ElapsedMilliseconds;
                            }
                    for (int k = 0; k < 3; ++k)
                        for (int j = 0; j < tests.Length; ++j)
                            results[k][j] /= tests[0].Length * 20.0;
                    break;
                case 3:
                    results = new double[5][]; 
                    for (int i = 0; i < 5; ++i)
                        results[i] = new double[tests.Length];
                    for (int i = 0; i < 5; ++i)
                        for (int j = 0; j < tests.Length; ++j)
                            results[i][j] = 0;
                    for (int i = 0; i < tests.Length; ++i)
                        for (int j = 0; j < tests[0].Length; ++j)
                            for (int k = 0; k < 20; ++k)
                            {
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.CombSort(tests[i][j]);
                                stopwatch.Stop();
                                results[0][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.Heapsort(tests[i][j]);
                                stopwatch.Stop();
                                results[1][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.QuickSort(tests[i][j]);
                                stopwatch.Stop();
                                results[2][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.MergeSort(tests[i][j]);
                                stopwatch.Stop();
                                results[3][i] += stopwatch.ElapsedMilliseconds;
                                tests = savedTests;
                                stopwatch = new Stopwatch();
                                stopwatch.Start();
                                Sortings.RadixSort(tests[i][j]);
                                stopwatch.Stop();
                                results[4][i] += stopwatch.ElapsedMilliseconds;
                            }
                    for (int k = 0; k < 5; ++k)
                        for (int j = 0; j < tests.Length; ++j)
                            results[k][j] /= tests[0].Length * 20.0;
                    break;
                default:
                    results = new double[0][];
                    break;
            }
            return results;
        }
        private void run_Click(object sender, EventArgs e)
        {
            double[][] results = Test();
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            PointPairList list;
            switch (sorts)
            {
                case 1:
                    list = new PointPairList();
                    for (int i = 0; i < results[0].Length; ++i)
                        list.Add(Math.Pow(10, i), results[0][i]);
                    LineItem lineB = pane.AddCurve("Сортировка пузырьком", list, Color.Blue, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[1].Length; ++i)
                        list.Add(Math.Pow(10, i), results[1][i]);
                    LineItem lineI = pane.AddCurve("Сортировка вставками", list, Color.Red, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[2].Length; ++i)
                        list.Add(Math.Pow(10, i), results[2][i]);
                    LineItem lineS = pane.AddCurve("Сортировка выбором", list, Color.Green, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[3].Length; ++i)
                        list.Add(Math.Pow(10, i), results[3][i]);
                    LineItem lineSha = pane.AddCurve("Шейкерная сортировка", list, Color.Purple, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[4].Length; ++i)
                        list.Add(Math.Pow(10, i), results[4][i]);
                    LineItem lineG = pane.AddCurve("Гномья сортировка", list, Color.Yellow, SymbolType.Default);
                    break;
                case 2:
                    list = new PointPairList();
                    for (int i = 0; i < results[0].Length; ++i)
                        list.Add(Math.Pow(10, i), results[0][i]);
                    LineItem lineBi = pane.AddCurve("Битонная сортировка", list, Color.Blue, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[1].Length; ++i)
                        list.Add(Math.Pow(10, i), results[1][i]);
                    LineItem lineShe = pane.AddCurve("Сортировка Шелла", list, Color.Red, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[2].Length; ++i)
                        list.Add(Math.Pow(10, i), results[2][i]);
                    LineItem lineT = pane.AddCurve("Сортировка деревом", list, Color.Green, SymbolType.Default);
                    break;
                case 3:
                    list = new PointPairList();
                    for (int i = 0; i < results[0].Length; ++i)
                        list.Add(Math.Pow(10, i), results[0][i]);
                    LineItem lineC = pane.AddCurve("Сортировка расчёской", list, Color.Blue, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[1].Length; ++i)
                        list.Add(Math.Pow(10, i), results[1][i]);
                    LineItem lineH = pane.AddCurve("Пирамидальная сортировка", list, Color.Red, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[2].Length; ++i)
                        list.Add(Math.Pow(10, i), results[2][i]);
                    LineItem lineQ = pane.AddCurve("Быстрая сортировка", list, Color.Green, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[3].Length; ++i)
                        list.Add(Math.Pow(10, i), results[3][i]);
                    LineItem lineM = pane.AddCurve("Сортировка слиянием", list, Color.Purple, SymbolType.Default);
                    list = new PointPairList();
                    for (int i = 0; i < results[4].Length; ++i)
                        list.Add(Math.Pow(10, i), results[4][i]);
                    LineItem lineR = pane.AddCurve("Поразрядная сортировка", list, Color.Yellow, SymbolType.Default);
                    break;
            }
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        private void save_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("results.txt", false))
            {
                sw.WriteLine("Сгенерированные массивы:\n");
                for (int i = 0; i < savedTests.Length; ++i)
                    for (int j = 0; j < savedTests[i].Length; ++j)
                    {
                        for (int k = 0; k < savedTests[i][j].Length; ++k)
                            sw.Write(savedTests[i][j][k].ToString() + " ");
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
                                Sortings.BubbleSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка вставками: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.InsertionSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка выбором: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.SelectionSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nШейкерная сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.ShakerSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nГномья сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.GnomeSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        break;
                    case 2:
                        tests = savedTests;
                        sw.WriteLine("\nБитонная сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.BitonicSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка Шелла: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.Shellsort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка деревом: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.TreeSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        break;
                    case 3:
                        tests = savedTests;
                        sw.WriteLine("\nСортировка расчёской: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.CombSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nПирамидальная сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.Heapsort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nБыстрая сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.QuickSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nСортировка слиянием: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.MergeSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        tests = savedTests;
                        sw.WriteLine("\nПоразрядная сортировка: ");
                        for (int i = 0; i < tests.Length; ++i)
                            for (int j = 0; j < tests[i].Length; ++j)
                            {
                                Sortings.RadixSort(tests[i][j]);
                                for (int k = 0; k < tests[i][j].Length; ++k)
                                    sw.Write(tests[i][j][k].ToString() + " ");
                                sw.WriteLine();
                            }
                        break;
                
                }
            }
        }
    }
    public class Sortings
    {
        public static void BubbleSort(int[] arr)    //O(n^2)
        {
            int r;
            for (int i = 0; i < arr.Length - 1; ++i)
                for (int j = 0; j < arr.Length - i - 1; ++j)
                    if (arr[j] > arr[j + 1])
                    {
                        r = arr[j]; 
                        arr[j] = arr[j + 1];
                        arr[j + 1] = r;
                    }
        }

        public static void ShakerSort(int[] arr)    //O(n^2)
        {
            int r, left = 0, right = arr.Length - 1;
            while (left < right)
            {
                for(int i = left; i < right; ++i)
                    if (arr[i] > arr[i + 1])
                    {
                        r = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = r;
                    }
                --right;
                for(int i = right; i > left; --i)
                    if (arr[i] < arr[i - 1])
                    {
                        r = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = r;
                    }
                ++left;
            }
        }

        public static void CombSort(int[] arr)  //O(n^2)
        {
            int r, space = arr.Length;
            bool swapped = true;
            while (space > 1 || swapped)
            {
                space = Math.Max(1, space / 5 * 4);
                swapped = false;
                for(int i = 0; i + space< arr.Length;++i)
                    if (arr[i] > arr[i + space])
                    {
                        r = arr[i]; 
                        arr[i] = arr[i + space];
                        arr[i + space] = r;
                        swapped = true;
                    }
            }
        }

        public static void InsertionSort(int[] arr) //O(n^2)
        {
            int j, key;
            for (int i = 1; i < arr.Length; ++i)
            {
                key = arr[i];
                j = i - 1;
                while (j>=0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j]; --j;
                }
                arr[j + 1] = key;
            }
        }

        public static void Shellsort(int[] arr) //O(n^2)
        {
            for (int space = arr.Length / 2; space > 0; space /= 2)
                for (int i = space; i < arr.Length; ++i)
                {
                    int j, r = arr[i];
                    for (j = i; j >= space && arr[j - space] > r; j -= space)
                        arr[j] = arr[j - space];
                    arr[j] = r;
                }
        }

        class Tree
        {
            int?[] tree;
            int Size { get; set; }
            public Tree(int size)
            {
                Size = 0;
                tree = new int?[1 << (size * 2)];
            }
            public void Add(int x)
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
                    if (x < tree[i])
                        if (2 * i + 1 < tree.Length)
                            i = 2 * i + 1;
                        else break;
                    else
                        if (2 * i + 2 < tree.Length)
                            i = 2 * i + 2;
                        else break;
                }
            }
            public void LVR(int i, ref int k, int[] arr)
            {
                if (i >= tree.Length || tree[i] == null) return;
                if (2 * i + 1 < tree.Length && tree[2 * i + 1] != null)
                    LVR(2 * i + 1, ref k, arr);
                arr[k] = (int)tree[i]; ++k;
                if (2 * i + 2 < tree.Length && tree[2 * i + 2] != null)
                    LVR(2 * i + 2, ref k, arr);

            }
        }

        public static void TreeSort(int[] arr)  //O(n^2)
        {
            Tree tree = new Tree(arr.Length);
            for (int i = 0; i < arr.Length; ++i)
                tree.Add(arr[i]);
            int k = 0;
            tree.LVR(0, ref k, arr);
        }

        public static void GnomeSort(int[] arr) //O(n^2)
        {
            int r, i = 1;
            while (i < arr.Length)
                if (i == 0 || arr[i - 1] <= arr[i])
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

        public static void SelectionSort(int[] arr) //O(n^2)
        {
            int r, min;
            for(int i = 0; i < arr.Length - 1; ++i)
            {
                min = i;
                for (int j = i + 1; j < arr.Length; ++j)
                    if (arr[j] < arr[min])
                        min = j;
                r = arr[i];
                arr[i] = arr[min];
                arr[min] = r;
            }
        }

        static void SiftDownMax(int[] arr, int ind, int maxIndex)
        {
            while (ind * 2 + 1 < maxIndex)
            {
                int left = ind * 2 + 1;
                int right = ind * 2 + 2;
                int maxChildInd = left;
                if (right < maxIndex && arr[right] > arr[left])
                    maxChildInd = right;
                if (arr[ind] >= arr[maxChildInd])
                    break;
                else
                {
                    int r = arr[ind];
                    arr[ind] = arr[maxChildInd];
                    arr[maxChildInd] = r;
                    ind = maxChildInd;
                }
            }
        }
        static void Heapify(int[] arr)
        {
            int startInd = arr.Length / 2 - 1;
            for (int i = startInd; i >= 0; i--)
            {
                SiftDownMax(arr, i, arr.Length);
            }
        }
        public static void Heapsort(int[] arr)  //O(nlogn)
        {
            Heapify(arr);
            int r;
            for (int i = arr.Length - 1; i > 0; --i)
            {
                r = arr[0];
                arr[0] = arr[i];
                arr[i] = r;
                SiftDownMax(arr, 0, i);
            }
        }

        static int Partition(int[] arr, int left, int right, int pivot)
        {
            int r, m = left;
            for (int i = left; i < right; ++i)
                if (arr[i] < pivot)
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
        static void QuickSort(int[] arr, int left, int right)
        {
            if (left >= right) return;
            Random rand = new Random();
            int pivot = rand.Next(left, right + 1);
            int r = arr[pivot];
            arr[pivot] = arr[right]; arr[right] = r;
            int m = Partition(arr, left, right, arr[right]);
            QuickSort(arr, left, m - 1);
            QuickSort(arr, m + 1, right);
        }
        public static void QuickSort(int[] arr) //O(nlogn)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        static int[] Merge(int[] left, int[] right)
        {
            int[] res = new int[left.Length + right.Length];
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
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
        static int[] Mergesort(int[] arr)
        {
            if (arr.Length <= 1) return arr;
            int mid = arr.Length / 2;
            int[] leftHalf = new int[mid];
            int[] rightHalf = new int[arr.Length - mid];
            for (int i = 0; i < mid; ++i)
                leftHalf[i] = arr[i];
            for (int i = 0; i < arr.Length - mid; ++i)
                rightHalf[i] = arr[mid + i];
            int[] leftSorted = Mergesort(leftHalf);
            int[] rightSorted = Mergesort(rightHalf);
            return Merge(leftSorted, rightSorted);
        }
        public static void MergeSort(int[] arr) //O(nlogn)
        {
            arr = Mergesort(arr);
        }

        static void CountingSort(int[] arr, int radix)
        {
            int[] count = new int[10];
            for (int i = 0; i < 10; ++i)
                count[i] = 0;
            for (int i = 0; i < arr.Length; ++i)
                ++count[arr[i] / radix % 10];
            for (int i = 1; i < 10; ++i)
                count[i] += count[i - 1];
            int[] sorts = new int[arr.Length];
            for(int i = arr.Length - 1; i >= 0; --i)
            {
                sorts[count[arr[i]/radix%10]-1]=arr[i];
                --count[arr[i] / radix % 10];
            }
            Array.Copy(sorts, arr, arr.Length);
        }
        public static void RadixSort(int[] arr) //O(nlogn)
        {
            int max = arr.Max();
            for(int radix = 1; max/radix>0;radix*=10)
                CountingSort(arr, radix);
        }

        static void BitonicMerge(int[]arr, int left, int n, bool up)
        {
            if (n < 2) return;
            int r, k = n / 2;
            for(int i = left; i<left+k; ++i)
                if (up == (arr[i] > arr[i + k]))
                {
                    r = arr[i];
                    arr[i] = arr[i + k];
                    arr[i + k] = r;
                }
            BitonicMerge(arr, left, k, up);
            BitonicMerge(arr, left + k, k, up);
        }
        static void Bitonicsort(int[]arr, int left, int n, bool up)
        {
            if (n < 2) return;
            int k = n / 2;
            Bitonicsort(arr, left, k, true);
            Bitonicsort(arr, left + k, k, false);
            BitonicMerge(arr, left, n, up);
        }
        public static void BitonicSort(int[] arr)   //О(logn*logn)
        {
            Bitonicsort(arr, 0, arr.Length, true);
        }
    }
    public class TestGroups
    {
        public class TestGroup1
        {
            public int[] testArray;
            const int MODULE = 1000;

            public TestGroup1(int size)
            {
                    testArray = new int[size];
                    Random rand = new Random();
                    for (int i = 0; i < size; ++i)
                        testArray[i] = rand.Next(0, MODULE);
                }
            }
        public class TestGroup2
        {
            public int[][] testArray;
            public TestGroup2(int size, int count, int module)
            {
                testArray = new int[count][];
                Random rand = new Random();
                for (int t = 0; t < count; ++t)
                {
                    testArray[t] = new int[size];
                    int sizeOfSorted = rand.Next(1, module);
                    int[] sortedPart;
                    for (int i = 0; i < size; i += sizeOfSorted)
                    {
                        sortedPart = new int[sizeOfSorted];
                        for (int j = 0; j < sizeOfSorted; ++j)
                            sortedPart[j] = rand.Next();
                        Array.Sort(sortedPart);
                        for (int j = i; j < i + sizeOfSorted && j < size; ++j)
                            testArray[t][j] = sortedPart[j - i];
                    }
                }
            }
        }
        public class TestGroup3
        {
            public int[][] testArray;
            const int MODULE = 1000;
            public TestGroup3(int size, int count, int permutations)
            {
                testArray = new int[count][];
                for (int t = 0; t < count; ++t)
                {
                    testArray[t] = new int[size];
                    Random rand = new Random();
                    for (int i = 0; i < size; ++i)
                        testArray[t][i] = rand.Next();
                    Array.Sort(testArray[t]);
                    int j, k, r;
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
            public int[][] testArray;
            const int MODULE = 1000;
            public TestGroup4(int size, int count, int[]commands)
            {
                testArray = new int[count][];
                for (int t = 0; t < count; ++t)
                {
                    testArray[t] = new int[size];
                    Random rand = new Random();
                    if (commands.Length == 2)
                        for (int i = 0; i < size; ++i)
                            testArray[t][i] = rand.Next();
                    else
                    {
                        int repeater = rand.Next();
                        int i = 0;
                        for (; i < size * commands[2] / 100; ++i)
                            testArray[t][i] = repeater;
                        for (; i < size; ++i)
                            testArray[t][i] = rand.Next();
                    }
                    if (commands[0] != 0)
                        Array.Sort(testArray[t]);
                    if (commands[0] == 1)
                        Array.Reverse(testArray[t]);
                    if (commands[1] != 0)
                    {
                        int permutations = rand.Next(0, MODULE);
                        int j, k, r;
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

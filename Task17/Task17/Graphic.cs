using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Task17
{
    public partial class Graphic : Form
    {
        MyArrayList<int>[] array = new MyArrayList<int>[4];
        MyLinkedList<int>[] linked = new MyLinkedList<int>[4];
        public double[][][] MakeTests()
        {
            double[][][] result = new double[4][][];
            for (int i = 0; i < 4; ++i)
            {
                result[i] = new double[2][];
                for (int j = 0; j < 2; ++j)
                    result[i][j] = new double[5];
            }

            Random rand = new Random();
            Stopwatch st;
            int el;
            for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
            {

                long size = (long)Math.Pow(10, 5 + sizeIdx);
                for (int k = 0; k < 20; ++k)
                {
                    array[sizeIdx] = new MyArrayList<int>();
                    linked[sizeIdx] = new MyLinkedList<int>();
                    for (int i = 0; i < size; ++i)
                    {
                        el = rand.Next();
                        st = Stopwatch.StartNew();
                        array[sizeIdx].Add(el);
                        st.Stop();
                        result[sizeIdx][0][0] += st.ElapsedTicks;

                        st = Stopwatch.StartNew();
                        linked[sizeIdx].Add(el);
                        st.Stop();
                        result[sizeIdx][1][0] += st.ElapsedTicks;
                    }
                }

                result[sizeIdx][0][0] /= (20 * size);
                result[sizeIdx][1][0] /= (20 * size);
            }

            for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
            {
                long size = (long)Math.Pow(10, 5 + sizeIdx);
                for (int i = 0; i < size; ++i)
                {
                    for (int k = 0; k < 20; ++k)
                    {
                        el = rand.Next(array[sizeIdx].Size());
                        st = Stopwatch.StartNew();
                        int ono = array[sizeIdx][el];
                        st.Stop();
                        result[sizeIdx][0][1] += st.ElapsedTicks;

                        el = rand.Next(linked[sizeIdx].Size());
                        st = Stopwatch.StartNew();
                        ono = linked[sizeIdx][el];
                        st.Stop();
                        result[sizeIdx][1][1] += st.ElapsedTicks;
                    }
                }

                result[sizeIdx][0][1] /= (20 * size);
                result[sizeIdx][1][1] /= (20 * size);
            }

            int setValue = 0;
            for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
            {
                long size = (long)Math.Pow(10, 5 + sizeIdx);
                for (int i = 0; i < size; ++i)
                {
                    for (int k = 0; k < 20; ++k)
                    {
                        el = rand.Next(array[sizeIdx].Size());
                        st = Stopwatch.StartNew();
                        array[sizeIdx][el] = setValue;
                        st.Stop();
                        result[sizeIdx][0][2] += st.ElapsedTicks;

                        el = rand.Next(linked[sizeIdx].Size());
                        st = Stopwatch.StartNew();
                        linked[sizeIdx][el] = setValue;
                        st.Stop();
                        result[sizeIdx][1][2] += st.ElapsedTicks;
                    }
                }

                result[sizeIdx][0][2] /= (20 * size);
                result[sizeIdx][1][2] /= (20 * size);
            }

            for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
            {
                long size = (long)Math.Pow(10, 5 + sizeIdx);
                for (int i = 0; i < Math.Min(size, 1000); ++i) 
                {
                    for (int k = 0; k < 20; ++k)
                    {
                        int insertValue = rand.Next();
                        el = rand.Next(array[sizeIdx].Size());
                        st = Stopwatch.StartNew();
                        array[sizeIdx].Add(el, insertValue);
                        st.Stop();
                        result[sizeIdx][0][3] += st.ElapsedTicks;

                        el = rand.Next(linked[sizeIdx].Size());
                        st = Stopwatch.StartNew();
                        linked[sizeIdx].Add(el, insertValue);
                        st.Stop();
                        result[sizeIdx][1][3] += st.ElapsedTicks;
                    }
                }

                result[sizeIdx][0][3] /= (20 * Math.Min(size, 1000));
                result[sizeIdx][1][3] /= (20 * Math.Min(size, 1000));
            }
            for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
            {
                long size = (long)Math.Pow(10, 5 + sizeIdx);
                for (int i = 0; i < Math.Min(size, 1000); ++i)
                {
                    for (int k = 0; k < 20; ++k)
                    {
                        el = rand.Next();
                        st = Stopwatch.StartNew();
                        array[sizeIdx].Remove(el);
                        st.Stop();
                        result[sizeIdx][0][4] += st.ElapsedTicks;

                        el = rand.Next();
                        st = Stopwatch.StartNew();
                        linked[sizeIdx].Remove(el);
                        st.Stop();
                        result[sizeIdx][1][4] += st.ElapsedTicks;
                    }
                }

                result[sizeIdx][0][4] /= (20 * Math.Min(size, 1000));
                result[sizeIdx][1][4] /= (20 * Math.Min(size, 1000));
            }

            return result;
        }

        public void Draw()
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            pane.Title.Text = "Сравнение времени выполнения операций";
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Среднее время выполнения (мс)";

            PointPairList list;
            double[][][] tests = MakeTests();

            double[] sizes = { 1e5, 1e6, 1e7, 1e8 };
            pane.XAxis.Type = AxisType.Log;
            pane.XAxis.Scale.Min = 1e4;
            pane.XAxis.Scale.Max = 1e9;

            Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Orange };
            string[] operationNames = { "Add", "Get", "Set", "Insert", "Remove" };

            for (int op = 0; op < 5; op++)
            {
                list = new PointPairList();
                for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
                {
                    list.Add(sizes[sizeIdx], tests[sizeIdx][0][op]);
                }
                pane.AddCurve($"ArrayList {operationNames[op]}", list, colors[op], SymbolType.Circle);
            }

            for (int op = 0; op < 5; op++)
            {
                list = new PointPairList();
                for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
                {
                    list.Add(sizes[sizeIdx], tests[sizeIdx][1][op]);
                }
                pane.AddCurve($"LinkedList {operationNames[op]}", list, colors[op], SymbolType.Square);
            }

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        public Graphic()
        {
            InitializeComponent();
            Draw();
        }
    }
}
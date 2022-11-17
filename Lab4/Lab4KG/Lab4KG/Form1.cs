namespace Lab4KG
{
    using System.Threading;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private float[,] kv = new float[4, 3];
        private float[,] osi = new float[4, 3];
        private float[,] matr_sdv = new float[3, 3];
        private float[,] shape = new float[4, 3];
        private float[,] working_matrix = new float[3, 3];
        
        private int k;
        private int l;
        private int k1;
        private int l1;
        private int thickness = 2;

        private double angle=10; //угол в градусах
        private const double pi = 3.14f;


        private bool f;
        private bool shapeF;
        private bool KvF;
        private bool AlreadeInit = false;
        private Color color = Color.DarkGreen;
        bool X = true;



        private void Init_kvadrat()
        {
            kv[0, 0] = -50;
            kv[0, 1] = 0;
            kv[0, 2] = 1;
            kv[1, 0] = 0;
            kv[1, 1] = 50;
            kv[1, 2] = 1;
            kv[2, 0] = 50;
            kv[2, 1] = 0;
            kv[2, 2] = 1;
            kv[3, 0] = 0;
            kv[3, 1] = -50;
            kv[3, 2] = 1;
            KvF = true;
        }

        private void Init_Shape()
        {
            shape[0, 0] = 0;
            shape[0, 1] = 0;
            shape[0, 2] = 1;

            shape[1, 0] = 50;
            shape[1, 1] = -50;
            shape[1, 2] = 1;

            shape[2, 0] = 0;
            shape[2, 1] = 100;
            shape[2, 2] = 1;

            shape[3, 0] = -50;
            shape[3, 1] = -50;
            shape[3, 2] = 1;

            shapeF = true;
        }

        private void Init_matr_preob(int k1, int l1)
        {
            matr_sdv[0, 0] = 1;
            matr_sdv[0, 1] = 0;
            matr_sdv[0, 2] = 0;
            matr_sdv[1, 0] = 0;
            matr_sdv[1, 1] = 1;
            matr_sdv[1, 2] = 0;
            matr_sdv[2, 0] = k1;
            matr_sdv[2, 1] = l1;
            matr_sdv[2, 2] = 1;
        }

        private void Init_osi()
        {
            osi[0, 0] = -200;
            osi[0, 1] = 0;
            osi[0, 2] = 1;
            osi[1, 0] = 200;
            osi[1, 1] = 0;
            osi[1, 2] = 1;
            osi[2, 0] = 0;
            osi[2, 1] = 200;
            osi[2, 2] = 1;
            osi[3, 0] = 0;
            osi[3, 1] = -200;
            osi[3, 2] = 1;
        }

        private float[,] Multiply_matr(float[,] a, float[,] b)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);
            float[,] r = new float[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    r[i, j] = 0;
                    for (int ii = 0; ii < m; ii++)
                    {
                        r[i, j] += a[i, ii] * b[ii, j];
                    }
                }
            }
            return r;
        }

        private void Draw_Kv()
        {
            Init_kvadrat(); //инициализация матрицы тела
            Init_matr_preob(k, l); //инициализация матрицы преобразования
            float[,] kv1 = Multiply_matr(kv, matr_sdv); //перемножение матриц
            Pen myPen = new Pen(Color.DarkGreen, thickness);// цвет линии и ширина

            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.Clear(Color.Honeydew);
            Draw_osi();
            // рисуем 1 сторону квадрата
            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[1, 0], kv1[1, 1]);

            // рисуем 2 сторону квадрата
            g.DrawLine(myPen, kv1[1, 0], kv1[1, 1], kv1[2, 0], kv1[2, 1]);

            // рисуем 3 сторону квадрата
            g.DrawLine(myPen, kv1[2, 0], kv1[2, 1], kv1[3, 0], kv1[3, 1]);

            // рисуем 4 сторону квадрата
            g.DrawLine(myPen, kv1[3, 0], kv1[3, 1], kv1[0, 0], kv1[0, 1]);

            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой

            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }

        private void Draw_Shape()
        {
            if (AlreadeInit == true)
            {

            }
            else
            {
                Init_Shape();
                AlreadeInit = true;
            }
            Init_matr_preob(k, l);
            float[,] shape1 = Multiply_matr(shape, matr_sdv);
            Pen myPen = new Pen(color, thickness);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.Clear(Color.Honeydew);
            Draw_osi();

            g.DrawLine(myPen, shape1[0, 0], shape1[0, 1], shape1[1, 0], shape1[1, 1]);

            // рисуем 2 сторону квадрата
            g.DrawLine(myPen, shape1[1, 0], shape1[1, 1], shape1[2, 0], shape1[2, 1]);

            // рисуем 3 сторону квадрата
            g.DrawLine(myPen, shape1[2, 0], shape1[2, 1], shape1[3, 0], shape1[3, 1]);

            // рисуем 4 сторону квадрата
            g.DrawLine(myPen, shape1[3, 0], shape1[3, 1], shape1[0, 0], shape1[0, 1]);

            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой

            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }

        private void button2_Click(object sender, EventArgs e)
        {
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            Draw_Kv();
            f = true;
        }

        private void Draw_osi()
        {
            Init_osi();
            Init_matr_preob(k1, l1);
            float[,] osi1 = Multiply_matr(osi, matr_sdv);
            Pen myPen = new Pen(Color.Green, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.DrawLine(myPen, osi1[0, 0], osi1[0, 1], osi1[1, 0], osi1[1, 1]);
            g.DrawLine(myPen, osi1[2, 0], osi1[2, 1], osi1[3, 0], osi1[3, 1]);
            g.Dispose();
            myPen.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            k1 = pictureBox1.Width / 2;
            l1 = pictureBox1.Height / 2;
            Draw_osi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            k += 5;  //изменение соответствующего элемента матрицы сдвига
            if (shapeF == true)
            {
                Draw_Shape();
            }
            if (KvF == true)
            {
                Draw_Kv(); //вывод квадратика
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            k -= 5;
            if (shapeF == true)
            {
                Draw_Shape();
            }
            if (KvF == true)
            {
                Draw_Kv(); //вывод квадратика
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            l += 5;
            if (shapeF == true)
            {
                Draw_Shape();
            }
            if (KvF == true)
            {
                Draw_Kv(); //вывод квадратика
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            l -= 5;
            if (shapeF == true)
            {
                Draw_Shape();
            }
            if (KvF == true)
            {
                Draw_Kv(); //вывод квадратика
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            button8.Text = "Стоп";
            if (f == true)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                button8.Text = "Старт";
            }
            f = !f;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                k++;
                if (shapeF == true)
                {
                    Draw_Shape();
                }
                if (KvF == true)
                {
                    Draw_Kv(); //вывод квадратика
                }
                Thread.Sleep(100);
            }
            else if (checkBox2.Checked == true)
            {
                k--;
                if (shapeF == true)
                {
                    Draw_Shape();
                }
                if (KvF == true)
                {
                    Draw_Kv(); //вывод квадратика
                }
                Thread.Sleep(100);
            }
            else if (checkBox3.Checked == true)
            {
                l++;
                if (shapeF == true)
                {
                    Draw_Shape();
                }
                if (KvF == true)
                {
                    Draw_Kv(); //вывод квадратика
                }
                Thread.Sleep(100);
            }
            else if (checkBox4.Checked == true)
            {
                l--;
                if (shapeF == true)
                {
                    Draw_Shape();
                }
                if (KvF == true)
                {
                    Draw_Kv(); //вывод квадратика
                }
                Thread.Sleep(100);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox4.Checked = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            KvF = false;
            shapeF = false;
            AlreadeInit = false;
            thickness = 2;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            Draw_Shape();
            f = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            working_matrix[0, 0] = 1;
            working_matrix[0, 1] = 0;
            working_matrix[0, 2] = 0;
            working_matrix[1, 0] = 0;
            working_matrix[1, 1] = -1;
            working_matrix[1, 2] = 0;
            working_matrix[2, 0] = 0;
            working_matrix[2,1]= 0;
            working_matrix[2, 2] = 1;
  
            shape = Multiply_matr(shape, working_matrix);
   
            Draw_Shape();
        }


        private void button11_Click(object sender, EventArgs e)
        {
            working_matrix[0, 0] = 1.5f;
            working_matrix[0, 1] = 0;
            working_matrix[0,2]= 0;
            working_matrix[1, 0] = 0;
            working_matrix[1, 1] = 1.5f;
            working_matrix[1, 2] = 0;
            working_matrix[2, 0] = 0;
            working_matrix[2, 1] = 0;
            working_matrix[2, 2] = 1;

            shape = Multiply_matr(shape, working_matrix);

            Draw_Shape();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            working_matrix[0, 0] = 0.5f;
            working_matrix[0, 1] = 0;
            working_matrix[0, 2] = 0;
            working_matrix[1, 0] = 0;
            working_matrix[1, 1] = 0.5f;
            working_matrix[1, 2] = 0;
            working_matrix[2, 0] = 0;
            working_matrix[2, 1] = 0;
            working_matrix[2, 2] = 1;

            shape= Multiply_matr(shape, working_matrix);

            Draw_Shape();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
            {
                color = colorDialog1.Color;
            }
            Draw_Shape();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            thickness = Int32.Parse(textBox1.Text);//толщина
            Draw_Shape();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            working_matrix[0, 0] = (float)Math.Cos(Radian());
            working_matrix[0, 1] = (float)Math.Sin(Radian());
            working_matrix[0, 2] = 0;
            working_matrix[1, 0] = -(float)Math.Sin(Radian());
            working_matrix[1, 1] = (float)Math.Cos(Radian());
            working_matrix[1, 2] = 0;
            working_matrix[2, 0] = 0;
            working_matrix[2, 1] = 0;
            working_matrix[2, 2] = 1;

            shape = Multiply_matr(shape,working_matrix);

            Draw_Shape();
        }

        private void button14_Click(object sender, EventArgs e)
        {
       
        }

        private double Radian()
        {
            double rad;
            if (textBox2.Text =="")
            {
                rad = (angle * pi) / 180;
            }
            else
            {
                angle = Int32.Parse(textBox2.Text);
                rad = (angle * pi) / 180;
            }
            return rad;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace KRIP2_LAB2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BigInteger E = BigInteger.Parse(textBox1.Text), N = BigInteger.Parse(textBox7.Text);

            char[] alph = "-абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ".ToCharArray();

            // шифрование

            string BinaryValue = null;
            BigInteger x1 = 0;
            while (E > 0)
            {

                x1 = E % 2;
                if (E == 0)
                    break;
                BinaryValue = BinaryValue + x1.ToString();
                E /= 2;
                //    x = Math.Truncate(x);
            }

            String open_text = (textBox5.Text);
            byte[] asciiBytes_open = Encoding.Unicode.GetBytes(open_text);
            byte[] asciiBytes_shifr = new byte[asciiBytes_open.Length];

            for (int i = 0; i < open_text.Length/*asciiBytes_open.Length*/; i++)
            {

                int n = BinaryValue.Length;
                BigInteger c = 1, s = (BigInteger)open_text[i];
                for (int j = 0; j < alph.Length; j++)
                {
                    if (open_text[i] == alph[j])
                    {
                        s = j;
                    }
                }

                for (int j = 0; j < n; j++)
                {
                    if (BinaryValue[j] == '1')
                    {
                        c = (c * s) % N;
                    }
                    s = (s * s) % N;
                }
                if(i + 1 == open_text.Length)
                    textBox6.Text = textBox6.Text + c;
                else
                    textBox6.Text = textBox6.Text + c + ", ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // расшифрование
            BigInteger D = BigInteger.Parse(textBox4.Text), N = BigInteger.Parse(textBox7.Text);
            char[] alph = "-абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ".ToCharArray();
            String shifr_text = textBox6.Text;
            string BinaryValue_2 = null;
            BigInteger x2 = 0;
            while (D > 0)
            {
                x2 = D % 2;
                if (D == 0)
                    break;
                BinaryValue_2 = BinaryValue_2 + x2.ToString();
                D /= 2;
            }

            BigInteger[] intArray = shifr_text.Split(',').Select(x => BigInteger.Parse(x)).ToArray();


            for (int i = 0; i < intArray.Length; i++)
            {

                int n = BinaryValue_2.Length;

                BigInteger m = 1, s = intArray[i];
                for (int j = 0; j < n; j++)
                {
                    if (BinaryValue_2[j] == '1')
                    {
                        m = (m * s) % N;
                    }
                    s = (s * s) % N;
                }
                textBox5.Text = textBox5.Text + alph[(int)m];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BigInteger P = BigInteger.Parse(textBox3.Text), E = 0, Q = BigInteger.Parse(textBox2.Text);
            BigInteger N = 0, Z = 0;
            BigInteger ost = 0, ost_2 = 0, mul = 0, a1 = 0, b1 = 0;
            N = P * Q;
            textBox7.Text = N.ToString();
            Z = (P - 1) * (Q - 1);
            textBox8.Text = Z.ToString();

            // нахождение простого числа E
            // bool check = false;
            //   E = Z - 1;
            E = Z;

            //for (BigInteger i = E - 1; i >= 2; i--)
            //{
            //    if (check)
            //    {
            //        E = i + 1;
            //        break;
            //    }
            //    check = true;
            //    for (int j = 2; j <= (i / 2); j++)
            //    {
            //        if (i % j == 0)
            //            check = false;
            //    }
            //}

            bool[] table;
            List<BigInteger> primearray;
            if (Z < 3000000)
            {
                table = new bool[(ulong)Z];
                primearray = new List<BigInteger>();
                for (BigInteger i = 0; i < Z; i++)
                    table[(ulong)i] = true;
                for (BigInteger i = 2; i * i < Z; i++)
                    if (table[(ulong)i])
                        for (var j = 2 * i; j < Z; j += i)
                            table[(ulong)j] = false;
                for (BigInteger i = 1; i < Z; i++)
                {
                    if (table[(ulong)i])
                    {
                        primearray.Add(i);
                    }
                }
            }
            else
            {
                table = new bool[(ulong)3000000];
                primearray = new List<BigInteger>();
                for (BigInteger i = 0; i < 3000000; i++)
                    table[(ulong)i] = true;
                for (BigInteger i = 2; i * i < 3000000; i++)
                    if (table[(ulong)i])
                        for (var j = 2 * i; j < 3000000; j += i)
                            table[(ulong)j] = false;
                for (BigInteger i = 1; i < 3000000; i++)
                {
                    if (table[(ulong)i])
                    {
                        primearray.Add(i);
                    }
                }
            }

            //проверка на взаимную простоту
            BigInteger[] U_1 = new BigInteger[3];
            BigInteger[] V_1 = new BigInteger[3];
            BigInteger[] T_1 = new BigInteger[3];
            do
            {

                Random rnd = new Random();

                int value = rnd.Next(0, primearray.Count);
                E = primearray[value];

                U_1[0] = Z;
                U_1[1] = 1;
                U_1[2] = 0;
                V_1[0] = E;
                V_1[1] = 0;
                V_1[2] = 1;
                ost = Z; ost_2 = 0; mul = 1; a1 = Z; b1 = E;
                BigInteger q_1 = 0;
                while (V_1[0] != 0)
                {

                    q_1 = U_1[0] / V_1[0];
                    T_1[0] = U_1[0] % V_1[0];
                    T_1[1] = U_1[1] - q_1 * V_1[1];
                    T_1[2] = U_1[2] - q_1 * V_1[2];
                    U_1[0] = V_1[0];
                    U_1[1] = V_1[1];
                    U_1[2] = V_1[2];
                    V_1[0] = T_1[0];
                    V_1[1] = T_1[1];
                    V_1[2] = T_1[1];
                }
                textBox1.Text = E.ToString();

            } while (U_1[0] != 1);


            // вычисление D
            BigInteger[] U = new BigInteger[2];
            BigInteger[] V = new BigInteger[2];
            BigInteger[] T = new BigInteger[2];

            U[0] = Z;
            U[1] = 0;
            V[0] = E;
            V[1] = 1;
            //    ost = Z; ost_2 = 0; mul = 1; a1 = Z; b1 = E;
            BigInteger D = 0, q = 0;
            while (V[0] != 0)
            {
                D = T[1];
                q = U[0] / V[0];
                T[0] = U[0] % V[0];
                T[1] = U[1] - q * V[1];
                U[0] = V[0];
                U[1] = V[1];
                V[0] = T[0];
                V[1] = T[1];
            }
            if (D < 0)
                D += a1;
            if (D == E)
                D = D + Z;
            textBox4.Text = D.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool[] table;
            List<BigInteger> primearray;

            table = new bool[(ulong)3000000];
            primearray = new List<BigInteger>();
            for (BigInteger i = 0; i < 3000000; i++)
                table[(ulong)i] = true;
            for (BigInteger i = 2; i * i < 3000000; i++)
                if (table[(ulong)i])
                    for (var j = 2 * i; j < 3000000; j += i)
                        table[(ulong)j] = false;
            for (BigInteger i = 1; i < 3000000; i++)
            {
                if (table[(ulong)i])
                {
                    primearray.Add(i);
                }
            }
            Random rnd = new Random();
            int value = rnd.Next(0, primearray.Count);
            textBox2.Text = primearray[value].ToString();
            value = rnd.Next(0, primearray.Count);
            textBox3.Text = primearray[value].ToString();
        }
    }
}

﻿using System;
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
            BigInteger P = BigInteger.Parse(textBox3.Text), E = 0, Q = BigInteger.Parse(textBox2.Text);
            BigInteger N = 0, Z = 0;
            N = P * Q;
            Z = (P - 1) * (Q - 1);

            // нахождение простого числа E
            
            bool[] is_prime = new bool[(int)N + 1];
            for (int i = 2; i <= N; ++i)
                is_prime[i] = true;

            List<int> primes = new List<int>();

            for (int i = 2; i <= N; ++i)
                if (is_prime[i])
                {
                    primes.Add(i);
                    E = i;
                    if (i * i <= N)
                        for (int j = i * i; j <= N; j += i)
                            is_prime[j] = false;
                }

            BigInteger ost = 0, ost_2 = 0, mul = 0, a1 = 0, b1 = 0;
            //проверка на взаимную простоту
            while (ost_2 != 1)
            {
                if (E > Z)
                {
                    ost = E; ost_2 = 0; mul = 1; a1 = E; b1 = Z;
                }
                else
                {
                    ost = Z; ost_2 = 0; mul = 1; a1 = Z; b1 = E;
                }
                while (ost != 0)
                {
                    mul = 1;
                    if (b1 * 2 < a1)
                    {
                        mul = 2;
                        for (int i = 3; ; i++)
                        {
                            if (b1 * i < a1)
                            {
                                mul = i;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    ost_2 = ost;
                    ost = a1 - b1 * mul;
                    a1 = b1;
                    b1 = ost;
                }

                if(ost_2 != 1)
                {
                    
                    for (int i = (int)E - 1; i > 2; i--)
                    {
                        if(is_prime[i])
                        {
                            E = i;
                            break;
                        }
                    }
                }
            }
            textBox1.Text = E.ToString();

            // вычисление D

            BigInteger[] U = new BigInteger[2];
            BigInteger[] V = new BigInteger[2];
            BigInteger[] T = new BigInteger[2];

            U[0] = Z;
            U[1] = 0;
            V[0] = E;
            V[1] = 1;
            ost = Z; ost_2 = 0; mul = 1; a1 = Z; b1 = E;


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
            textBox4.Text = D.ToString();


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

            String open_text = (textBox5.Text), shifr_text = "";
            byte[] asciiBytes_open = Encoding.ASCII.GetBytes(open_text);
            byte[] asciiBytes_shifr = new byte[asciiBytes_open.Length];

            for (int i = 0; i < asciiBytes_open.Length; i++)
            {

                int n = BinaryValue.Length;

                BigInteger c = 1, s = asciiBytes_open[i];
                for (int j = 0; j < n; j++)
                {
                    if (BinaryValue[j] == '1')
                    {
                        c = (c * s) %  N;
                    }
                    s = (s * s) % N;
                }
                asciiBytes_shifr[i] = (byte)c;
                
                
            }
            shifr_text = System.Text.Encoding.ASCII.GetString(asciiBytes_shifr);

            // расшифрование

            byte[] asciiBytes_shifr_2 = Encoding.ASCII.GetBytes(shifr_text);
          
            string BinaryValue_2 = null;
            BigInteger x2 = 0;
            while (D > 0)
            {

                x2 = D % 2;
                if (D == 0)
                    break;
                BinaryValue_2 = BinaryValue_2 + x2.ToString();
                D /= 2;
                //    x = Math.Truncate(x);
            }

            String open_text_2 = "";
            for (int i = 0; i < asciiBytes_shifr_2.Length; i++)
            {

                int n = BinaryValue_2.Length;

                BigInteger m = 1, s = asciiBytes_shifr_2[i];
                for (int j = 0; j < n; j++)
                {
                    if (BinaryValue_2[j] == '1')
                    {
                        m = (m * s) % N;
                    }
                    s = (s * s) % N;
                }
                asciiBytes_shifr_2[i] = (byte)m;
            }
            open_text_2 = System.Text.Encoding.ASCII.GetString(asciiBytes_shifr_2);
        }

    }
}

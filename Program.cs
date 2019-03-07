using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BitFlyer
{
    class Program
    {
        public double[] findMaxReward(int[] transacSize, double[] fee, int limit, Stack<double> res)
        {

            for (int i = 0; i < transacSize.Length; i++)
            {  
                 
                int inc = 1;
                int subInc = 0;
                var subTrans = transacSize.Skip(i + inc).ToArray();

                while (inc <= subTrans.Length) 
                {
                    double reward = 0.0;
                    int sum = 0;

                    reward += fee[i];
                    sum = transacSize[i];


                    for (int j = subInc; j < subTrans.Length; j++)
                    {
                        sum += subTrans[j];
                        
                        if (sum <= limit)
                        {
                            reward += fee[i + j + 1];
                            if (subTrans.Length == 1 || j == subTrans.Length - 1) {
                                inc++;
                                res.Push(reward);
                            }       
                        }
                        else
                        {
                            res.Push(reward);
                            inc++;
                            subInc++;
                            break;
                        }
                    }
                }
            }
            
            return res.ToArray();
        }

        static void Main(string[] args)
        {
            string[] userInput = File.ReadAllLines("BitFlyerQuiz\\input.txt", Encoding.UTF8);
            
            int[] transacSize = new int[userInput.Length];
            double[] fee = new double[userInput.Length];

            int limit = 1000000;
            
            double[] combinations = new double[userInput.Length];
            Stack<double> result = new Stack<double>();

            for (int i = 0; i < userInput.Length; i++)
            {
                string[] tokens = userInput[i].Split();
                transacSize[i] = int.Parse(tokens[0]);
                fee[i] = Convert.ToDouble(tokens[1]);
            }
            Program bitQuiz = new Program();

            combinations = bitQuiz.findMaxReward(transacSize, fee, limit, result);

            double maxReward = combinations.Max() + 12.5;
            Console.WriteLine("Max reward = " + maxReward);
            Console.ReadLine();
        }
    }
}

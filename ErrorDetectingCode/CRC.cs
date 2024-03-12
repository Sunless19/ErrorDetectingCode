using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
1.	Se introduce de la tastatura un sir de caractere binare si un polinom generator (cu coeficienti 0 si 1).
2.	Se fac urmatoarele verificari: sirurile sa fie binare si lungimea mesajului sa fie mai mare decat numarul de coeficienti ai polinomului generator.
3.	Se extinde mesajul cu un numar de 0-uri egal cu gradul polinomului introdus.
4.	Se efectueaza succesiv operatii de XOR intre mesajul extins si coeficientii polinomului pana cand lungimea restului obtinut este strict mai mica decat lungimea sirului de coeficienti.
5.	Se vor afisa rezultatele intermediare ale operatiei de XOR.
6.	Se executa iarasi operatia de sau exclusiv intre mesajul extins si restul final obtinut, dar pozitionarea restului se va face sub finalul mesajului extins. 
7.	Acest rezultat se va afisa.
*/


namespace ErrorDetectingCode
{
    internal class CRC
    {
        // Method to perform CRC calculation
        public static string CalculateCRC(string binaryMessage, string generatorPolynomial)
        {
            // Step 2: Check if inputs are binary and message length is greater than polynomial length
            if (!IsBinary(binaryMessage) || !IsBinary(generatorPolynomial) || binaryMessage.Length <= generatorPolynomial.Length)
            {
                throw new ArgumentException("Invalid input.");
            }

            // Step 3: Extend message with zeros equal to polynomial degree
            binaryMessage += new string('0', generatorPolynomial.Length);

            // Convert generator polynomial and extended message to arrays of integers
            int[] generator = ToIntArray(generatorPolynomial);
            int[] message = ToIntArray(binaryMessage);

            // Step 4: Perform XOR operations between extended message and polynomial coefficients
            string intermediateResults = "";
            for (int i = 0; i <= message.Length - generator.Length; i++)
            {
                if (message[i] == 1)
                {
                    intermediateResults += " " + string.Join("", message) + Environment.NewLine; // Step 5: Display intermediate results
                    for (int j = 0; j < generator.Length; j++)
                    {
                        message[i + j] ^= generator[j];
                    }
                }
            }

            // Step 6: Perform XOR again with remainder positioned under extended message
            string remainder = string.Join("", message).Substring(binaryMessage.Length);
            string finalResult = binaryMessage.Substring(0, binaryMessage.Length - generator.Length) + remainder;

            Console.WriteLine("Intermediate Results:");
            Console.WriteLine(intermediateResults);
            Console.WriteLine("Final Result:");
            Console.WriteLine(finalResult);

            return finalResult;
        }

        // Method to check if a string contains only binary digits (0 and 1)
        private static bool IsBinary(string input)
        {
            foreach (char c in input)
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }
            return true;
        }

        // Method to convert a binary string to an integer array
        private static int[] ToIntArray(string binaryString)
        {
            int[] array = new int[binaryString.Length];
            for (int i = 0; i < binaryString.Length; i++)
            {
                array[i] = binaryString[i] - '0';
            }
            return array;
        }
    }

}

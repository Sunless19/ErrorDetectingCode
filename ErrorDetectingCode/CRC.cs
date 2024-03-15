using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
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
        public static string CalculateCRC()
        {
            Console.WriteLine("Enter the binary message");
            string binaryMessage = Console.ReadLine();

            Console.WriteLine("Enter the generator polynomial");
            string generatorPolynomial = Console.ReadLine();

            // Remove leading zeros from generator polynomial
            generatorPolynomial = generatorPolynomial.TrimStart('0');

            // Check if inputs are binary and message length is greater than polynomial length
            if (!IsBinary(binaryMessage) || !IsBinary(generatorPolynomial) || binaryMessage.Length <= generatorPolynomial.Length)
            {
                throw new ArgumentException("Invalid input.");
            }

            // Step 3: Extend message with zeros equal to polynomial degree
            binaryMessage += new string('0', generatorPolynomial.Length - 1);

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
            // If the message length (without leading zeros) is equal to the generator length,
            // perform one more iteration to handle the final remainder
            if (message.Length == generator.Length)
            {
                intermediateResults += " " + string.Join("", message) + Environment.NewLine;
                for (int j = 0; j < generator.Length; j++)
                {
                    message[j] ^= generator[j];
                }
            }

            // Append the last intermediate result
            intermediateResults += " " + string.Join("", message) + Environment.NewLine;

            Console.WriteLine("Intermediate Results:");
            Console.WriteLine(intermediateResults);

            // Perform XOR again with remainder positioned under extended message
            string remainder = string.Join("", message).Substring(message.Length - (generator.Length - 1));
            string finalResult = binaryMessage.Substring(0, binaryMessage.Length - generator.Length) + remainder;

            // Perform XOR between extended message and final remainder
            string finalRemainder = "";
            for (int i = 0; i < remainder.Length; i++)
            {
                finalRemainder += (binaryMessage[i] ^ remainder[i]).ToString();
            }
            Console.WriteLine($"remainder : {remainder}");

            //adaug restul final la mesajul extins
            string[] intermediateResultsArray = intermediateResults.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string extendedMessage = intermediateResultsArray[0];
            bool result = IsAllZeros(remainder);
            if (result)
            {
                finalResult = extendedMessage;
                Console.WriteLine($"Final : {finalResult}");
            }
            else
            {
                finalResult = AddRemainderToExtendedMessage(extendedMessage, remainder);
                if (finalResult[0] == '0')
                    finalResult = finalResult.Substring(1);
                Console.WriteLine($"Final : {finalResult}");
            }

            return finalRemainder;
        }
        public static string AddBinaryStrings(string a, string b)
        {
            // Asigurăm că a și b au aceeași lungime, adăugând zero-uri la începutul celui mai mic șir
            int maxLength = Math.Max(a.Length, b.Length);
            a = a.PadLeft(maxLength, '0');
            b = b.PadLeft(maxLength, '0');

            string result = "";
            int carry = 0;

            // Parcurgem șirurile de la dreapta la stânga
            for (int i = maxLength - 1; i >= 0; i--)
            {
                int sum = (a[i] - '0') + (b[i] - '0') + carry; // Adunăm cifrele și transportul de la poziția anterioară
                result = (sum % 2) + result; // Adăugăm cifra rezultatului la începutul șirului rezultat
                carry = sum / 2; // Actualizăm transportul pentru următoarea adunare
            }

            // Dacă există un transport final, îl adăugăm la începutul rezultatului
            if (carry > 0)
            {
                result = carry + result;
            }

            return result;
        }

        public static string AddRemainderToExtendedMessage(string extendedMessage, string remainder)
        {
            // Adăugăm zero-uri la începutul șirului remainder până când are aceeași lungime cu extendedMessage
            remainder = remainder.PadLeft(extendedMessage.Length, '0');

            string result = "";
            int carry = 0;

            // Parcurgem șirurile de la dreapta la stânga
            for (int i = extendedMessage.Length - 1; i >= 0; i--)
            {
                int sum = (extendedMessage[i] - '0') + (remainder[i] - '0') + carry; // Adunăm cifrele și transportul de la poziția anterioară
                result = (sum % 2) + result; // Adăugăm cifra rezultatului la începutul șirului rezultat
                carry = sum / 2; // Actualizăm transportul pentru următoarea adunare
            }

            // Adăugăm transportul suplimentar la începutul rezultatului, dacă este necesar
            while (carry > 0)
            {
                result = (carry % 2) + result;
                carry /= 2;
            }

            return result;
        }
        public static bool IsAllZeros(string input)
        {
            foreach (char c in input)
            {
                if (c != '0')
                {
                    return false; // Dacă găsim un caracter diferit de '0', returnăm fals
                }
            }
            return true; // Dacă nu găsim niciun caracter diferit de '0', returnăm adevărat
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

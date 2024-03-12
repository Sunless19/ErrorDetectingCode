using System.Xml.Serialization;
using System;
using System.Runtime.CompilerServices;

/*
1.	Se introduce de la tastatura un sir format dintr-un numar multiplu de 7 caractere binare (Se vor face verificari in acest sens). 
2.	Mesajul va fi pozitionat intr-o matrice cu 7 coloane si numar de linii variabil (in functie de lungimea mesajului introdus).
3.	Se vor calcula bitii de paritate pe fiecare linie si fiecare coloana astfel:
-	pentru  numar par de 1 pe o linie/coloana  se va adauga un 0 pe ultima coloana/linie;
-	pentru un numar impar de 1 se va adauga un 1;
-	bitul semnificativ este bitul din coltul dreapta-jos al matricei.
4.	Se va afisa matricea rezultata.
5.	Se va simula coruperea mesajului prin generarea aleatoare a unei pozitii a unui caracter binar al mesajului. In functie de pozitia generata, se va modifica bitul de pe acea pozitie. 
6.	Destinatia va reface calculul bitilor de paritate bidimensionala si va identifica bitul corupt.
7.	Destinatia va afisa apoi pozitia corupta.
 */


namespace ErrorDetectingCode
{
    internal class PBB
    {
        private string message;
        public PBB() { }
        private Random random = new Random();

        private void Initialization()
        {
            Console.WriteLine("Enter the message (Bits)");
            message = Console.ReadLine();

        }
        private string Verify()
        {
            //characters of message to be bits.
            foreach (char x in message)
            {
                if (x < 0 && x > 1)
                {
                    return "Not good";
                }
            }
            //message multiple of 7.
            if (message.Length % 7 != 0)
            {
                return "Not Good";
            }

            //returns appropriate message after passing all tests.
            return "good";
        }

        public void ProcessMessage()
        {
            Initialization();

            string verificationResult = Verify();
            if (verificationResult == "good")
            {
                int numRows = message.Length / 7;
                int numColumns = 7;
                char[,] matrix = new char[numRows + 1, numColumns + 1]; // Add one extra row and column for parity bits
                int index = 0;

                for (int i = 0; i < numRows; i++)
                {
                    int countOnes = 0; // Count the number of ones in the current row
                    for (int j = 0; j < numColumns; j++)
                    {
                        matrix[i, j] = message[index++];
                        if (matrix[i, j] == '1')
                            countOnes++;
                    }

                    // Calculate and set the parity bit for the row
                    matrix[i, numColumns] = countOnes % 2 == 0 ? '0' : '1';
                }

                // Calculate parity bits for columns
                for (int j = 0; j < numColumns; j++)
                {
                    int countOnes = 0; // Count the number of ones in the current column
                    for (int i = 0; i < numRows; i++)
                    {
                        if (matrix[i, j] == '1')
                            countOnes++;
                    }

                    // Calculate and set the parity bit for the column
                    matrix[numRows, j] = countOnes % 2 == 0 ? '0' : '1';
                }

                // Calculate and set the parity bit for the last cell (bottom-right corner)
                int totalOnes = 0;
                for (int i = 0; i < numRows; i++)
                {
                    for (int j = 0; j < numColumns; j++)
                    {
                        if (matrix[i, j] == '1')
                            totalOnes++;
                    }
                }
                matrix[numRows, numColumns] = totalOnes % 2 == 0 ? '0' : '1';

                // Display the matrix with parity bits
                Console.WriteLine("Matrix with parity bits:");
                for (int i = 0; i <= numRows; i++)
                {
                    for (int j = 0; j <= numColumns; j++)
                    {
                        Console.Write(matrix[i, j]);
                    }
                    Console.WriteLine();
                }
                int rowIndex = random.Next(0, numRows);
                int colIndex = random.Next(0, 7);

                // Modify the bit at the random position
                matrix[rowIndex, colIndex] = (matrix[rowIndex, colIndex] == '0') ? '1' : '0';

                // Recalculate parity bits
                Destination destination = new Destination();
                destination.RecalculateParity(matrix, numRows);

                Console.WriteLine("Matrix with corrupted bit");
                //Matrix after a bit was corrupted.
                for (int i = 0; i <= numRows; i++)
                {
                    for (int j = 0; j <= numColumns; j++)
                    {
                        Console.Write(matrix[i, j]);
                    }
                    Console.WriteLine();
                }

                // Identify the corrupted bit
                Console.WriteLine($"Corrupted bit position: Row {rowIndex + 1}, Column {colIndex + 1}");
            }
            else
            {
                Console.WriteLine("Message does not meet requirements.");
            }
        }
    }
    internal class Destination
    {
        public void RecalculateParity(char[,] matrix, int numRows)
        {
            // Check parity for each row
            for (int i = 0; i < numRows; i++)
            {
                int parityCount = 0;
                for (int j = 0; j < 7; j++)
                {
                    if (matrix[i, j] == '1')
                    {
                        parityCount++;
                    }
                }
                // Add parity bit
                matrix[i, 7] = (parityCount % 2 == 0) ? '0' : '1';
            }

            // Check parity for each column
            for (int j = 0; j < 7; j++)
            {
                int parityCount = 0;
                for (int i = 0; i < numRows; i++)
                {
                    if (matrix[i, j] == '1')
                    {
                        parityCount++;
                    }
                }
                // Add parity bit
                matrix[numRows, j] = (parityCount % 2 == 0) ? '0' : '1';
            }

            // Calculate the parity bit for the bottom-right corner
            int cornerParityCount = 0;
            for (int i = 0; i < numRows; i++)
            {
                if (matrix[i, 7] == '1')
                {
                    cornerParityCount++;
                }
            }
            for (int j = 0; j < 7; j++)
            {
                if (matrix[numRows, j] == '1')
                {
                    cornerParityCount++;
                }
            }
            matrix[numRows, 7] = (cornerParityCount % 2 == 0) ? '0' : '1';
        }
    }
    
}
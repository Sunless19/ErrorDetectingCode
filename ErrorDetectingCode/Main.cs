namespace ErrorDetectingCode
{
    internal class ErrorDetectingCode
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1)PBB (Bidimensional Parity Bits)\n2)CRC (Cyclic Redundancy Check)");
            string a = Console.ReadLine();
            switch (a)
            {
                case "PBB":
                    PBB pbbInstance = new PBB();
                    pbbInstance.ProcessMessage();
                    break;
                case "CRC":
                    try
                    {
                        CRC.CalculateCRC();
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default: 
                    Console.WriteLine("Program Finished");
                    break;

            }
        }
    }
}
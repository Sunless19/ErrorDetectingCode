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
                    CRC crcInstance = new CRC();

                    break;

            }
        }
    }
}
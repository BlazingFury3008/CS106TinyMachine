using System;

namespace TinyMachineCSharp
{
    class Program
    {
        public static bool runCommands = true;
        public static string commands;
        public static char[] commandchar;
        public static char[] hexList = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f', };
        public static char[] lowercaseList = { 'a', 'b', 'c', 'd', 'e', 'f' };
        public static char[] uppercaseList = { 'A', 'B', 'C', 'D', 'E', 'F' };
        public static string outputQueue;
        public static string OPR;
        public static string Inp;
        public static string Out;
        public static string Addr;
        public static int addrInt;

        //Registers
        public static int IpInt = 0;
        public static string IP;
        public static int LiInt = 0;
        public static string LI;
        public static int FrInt = 0;
        public static string FR;
        public static int AcInt = 0;
        public static string AC;

        //Flag Bits
        public static bool HF = false;
        public static bool OF = false;
        public static bool ZF = false;
        public static bool CF = false;

        //Conversions
        public static void ConvertAllToInt()
        {
            HexToInt(IP, out IpInt);
            HexToInt(LI, out LiInt);
            HexToInt(FR, out FrInt);
            HexToInt(AC, out AcInt);
        }
        public static void ConvertAllToHex()
        {
            IntToHex(IpInt, out IP);
            IntToHex(LiInt, out LI);
            IntToHex(FrInt, out FR);
            IntToHex(AcInt, out AC);
        }

        //FlagBit Conversions
        public static void BitToRegister()
        {
            FrInt = 0;

            if (HF)
            {
                FrInt += 8;
            }

            if (OF)
            {
                FrInt += 4;
            }

            if (ZF)
            {
                FrInt += 2;
            }

            if (CF)
            {
                FrInt += 1;
            }

            ConvertAllToHex();
        }
        public static void RegisterToBit()
        {
            HF = false;
            OF = false;
            ZF = false;
            CF = false;

            ConvertAllToInt();
            int tempVar = FrInt;

            if (tempVar >= 8)
            {
                HF = true;
                tempVar -= 8;
            }
            if (tempVar >= 4)
            {
                OF = true;
                tempVar -= 4;
            }
            if (tempVar >= 2)
            {
                ZF = true;
                tempVar -= 2;
            }
            if (tempVar >= 1)
            {
                CF = true;
                tempVar -= 1;
            }
        }

        //Conversion Functions
        public static void IntToHex(int integer, out string hex)
        {
            if (integer >= 0 && integer <= 9)
            {
                hex = integer.ToString();
            }
            else if (integer == 10)
            {
                hex = "A";
            }
            else if (integer == 11)
            {
                hex = "B";
            }
            else if (integer == 12)
            {
                hex = "C";
            }
            else if (integer == 13)
            {
                hex = "D";
            }
            else if (integer == 14)
            {
                hex = "E";
            }
            else if (integer == 15)
            {
                hex = "F";
            }
            else
            {
                hex = null;
            }

        }
        public static void HexToInt(string hex, out int integer)
        {
            integer = 0;

            if (int.TryParse(hex, out int result))
            {
                if (result >= 0 && result <= 9)
                {
                    integer = result;
                }
            }
            else if (hex == "A")
            {
                integer = 10;
            }
            else if (hex == "B")
            {
                integer = 11;
            }
            else if (hex == "C")
            {
                integer = 12;
            }
            else if (hex == "D")
            {
                integer = 13;
            }
            else if (hex == "E")
            {
                integer = 14;
            }
            else if (hex == "F")
            {
                integer = 15;
            }
            else
            {
                integer = 16;
            }
        }
        public static void IntToBin(int input, out string binary)
        {
            int tempvar = input;
            string A8 = "0";
            string A4 = "0";
            string A2 = "0";
            string A1 = "0";

            if(tempvar > 8)
            {
                tempvar -= 8;
                A8 = "1";
            }

            if (tempvar > 4)
            {
                tempvar -= 4;
                A4 = "1";
            }

            if (tempvar > 2)
            {
                tempvar -= 2;
                A2 = "1";
            }

            if (tempvar > 1)
            {
                tempvar -= 1;
                A1 = "1";
            }

            binary = A8 + A4 + A2 + A1;
        }

        //Other Vars
        public static string inputQueue;
        public static char[] inputchar;

        static void Main(string[] args)
        {
            TinyMachine();
        }

        private static void TinyMachine()
        {
            ConvertAllToHex();
            //Command Line
            commands = "";
            bool CommandShort = true;


            FrInt = 0;
            AcInt = 0;
            IpInt = 0;
            LiInt = 0;

            TinyInitialRegisterValue();

            ConvertAllToHex();
            RegisterToBit();

            Console.Clear();
            Console.WriteLine("Enter Command Line");

            while (CommandShort)
            {
                commands = Console.ReadLine();

                if (commands.Length != 16)
                {
                    Console.Clear();
                    if (commands.Length < 16)
                    {
                        Console.WriteLine("Not Long Enough");
                        Console.WriteLine("Enter Command Line");
                    }
                    else
                    {
                        Console.WriteLine("Too Long");
                        Console.WriteLine("Enter Command Line");
                    }
                }
                else if (commands.Length == 16)
                {
                    bool charCheck = false;
                    int correctDig = 0;
                    char[] characterlist = commands.ToCharArray();

                    for (int i = 0; i < characterlist.Length; i++)
                    {
                        for (int j = 0; j < hexList.Length; j++)
                        {
                            if (characterlist[i] == hexList[j])
                            {
                                correctDig++;
                            }
                        }
                    }

                    if (correctDig == 16)
                    {
                        charCheck = true;
                    }

                    if (charCheck)
                    {
                        CommandShort = false;
                    }
                    else
                    {
                        CommandShort = true;
                    }
                }
            }

            char[] commandschar = commands.ToCharArray();

            for (int i = 0; i < commands.Length; i++)
            {
                for (int j = 0; j < lowercaseList.Length; j++)
                {
                    if (commandschar[i] == lowercaseList[j])
                    {
                        commandschar[i] = uppercaseList[j];
                    }
                }
            }
            commands = "";
            for (int i = 0; i < commandschar.Length; i++)
            {
                commands += commandschar[i];
            }

            //Input Queue
            Console.Clear();
            Console.WriteLine("Enter Input Queue");
            inputQueue = Console.ReadLine();
            inputchar = inputQueue.ToCharArray();
            int input = 0;

            bool inputCheck = true;
            while (inputCheck)
            {
                for (int i = 0; i < inputchar.Length; i++)
                {
                    for (int j = 0; j < hexList.Length; j++)
                    {
                        if (inputchar[i] == hexList[j])
                        {
                            input++;
                        }
                    }
                }

                if (input == inputchar.Length)
                {
                    inputCheck = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Enter Input Queue");
                    inputQueue = Console.ReadLine();
                }
            }

            for (int i = 0; i < inputchar.Length; i++)
            {
                for (int j = 0; j < lowercaseList.Length; j++)
                {
                    if (inputchar[i] == lowercaseList[j])
                    {
                        inputchar[i] = uppercaseList[j];
                    }
                }
            }

            inputQueue = "";
            for (int i = 0; i < inputchar.Length; i++)
            {
                inputQueue += inputchar[i];
            }

            Console.Clear();
            Console.WriteLine("Command Line: " + commands);
            Console.WriteLine("Input Queue: " + inputQueue);

            OPR = "OPR";
            Addr = "&";
            Inp = "?";
            Out = "!";

            Console.WriteLine("I L F A  MEMORY----------  TRACE---");
            Console.WriteLine("P I R C  0123456789ABCDEF  " + OPR + " " + Addr + " " + Inp + " " + Out);


            while (runCommands)
            {
                string precommands = commands;
                string registers = IP + " " + LI + " " + FR + " " + AC;
                OPR = "OPR";
                Addr = "-";
                Inp = "-";
                Out = "-";
                commandchar = commands.ToCharArray();
                if (inputQueue != null)
                {
                    inputchar = inputQueue.ToCharArray();
                }

                ConvertAllToInt();

                if (commandchar[IpInt] == '0')
                {
                    HLT();
                    OPR = "HLT";
                }
                else if (commandchar[IpInt] == '1')
                {
                    JMP();
                    OPR = "JMP";
                }
                else if (commandchar[IpInt] == '2')
                {
                    JZE();
                    OPR = "JZE";
                }
                else if (commandchar[IpInt] == '3')
                {
                    JNZ();
                    OPR = "JNZ";
                }
                else if (commandchar[IpInt] == '4')
                {
                    LDA();
                    OPR = "LDA";
                }
                else if (commandchar[IpInt] == '5')
                {
                    STA();
                    OPR = "STA";
                }
                else if (commandchar[IpInt] == '6')
                {
                    GET();
                    OPR = "GET";
                }
                else if (commandchar[IpInt] == '7')
                {
                    PUT();
                    OPR = "PUT";
                }
                else if (commandchar[IpInt] == '8')
                {
                    ROL();
                    OPR = "ROL";
                }
                else if (commandchar[IpInt] == '9')
                {
                    ROR();
                    OPR = "ROR";
                }
                else if (commandchar[IpInt] == 'A')
                {
                    ADC();
                    OPR = "ADC";
                }
                else if (commandchar[IpInt] == 'B')
                {
                    CCF();
                    OPR = "CCF";
                }
                else if (commandchar[IpInt] == 'C')
                {
                    SCF();
                    OPR = "SCF";
                }
                else if (commandchar[IpInt] == 'D')
                {
                    DEL();
                    OPR = "DEL";
                }
                else if (commandchar[IpInt] == 'E')
                {
                    LDL();
                    OPR = "LDL";
                }
                else if (commandchar[IpInt] == 'F')
                {
                    FLA();
                    OPR = "FLA";
                }


                RegisterToBit();

                if(IpInt > 15)
                {
                    IpInt -= 16;
                }

                ConvertAllToHex();


                Console.WriteLine(registers + "  " + precommands + "  " + OPR + " " + Addr + " " + Inp + " " + Out);

                if (HF == true)
                {
                    Console.WriteLine(IP + " " + LI + " " + FR + " " + AC);
                    Console.WriteLine("");
                    Console.WriteLine("The Machine Halts");
                    Console.WriteLine("Output Queue: " + outputQueue);
                    runCommands = false;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("");

            bool loop = true;
            
            while (loop)
            {
                Console.WriteLine("Do you want to do another run? Y/N");
                string answer = Console.ReadLine();
                if (answer == "y" | answer == "Y")
                {
                    TinyMachine();
                    loop = false;
                }
                else if (answer == "n" | answer == "N")
                {
                    Environment.Exit(0);
                    loop = false;
                }
            }


        }

        private static void TinyInitialRegisterValue()
        {
            string initialIP;
            string initialFR;
            string initialLI;
            string initialAC;

            bool changeVals = true;
            bool changeIP = true;
            bool IPinp = true;
            bool changeFR = true;
            bool FRinp = true;
            bool changeLI = true;
            bool LIinp = true;
            bool changeAC = true;
            bool ACinp = true;

            while (changeVals)
            {
                Console.Clear();
                Console.WriteLine("Preset any values? (Y/N)");
                string check = Console.ReadLine();
                Console.Clear();
                if(check == "Y"|check =="y")
                {
                    while (changeIP)
                    {
                        Console.WriteLine("Set IP value (Y/N)");
                        check = Console.ReadLine();
                        Console.Clear();
                        if (check == "Y" | check == "y")
                        {
                            while (IPinp)
                            {
                                Console.WriteLine("Write new IP value");
                                initialIP = Console.ReadLine();
                                for (int i = 0; i < hexList.Length; i++)
                                {
                                    if(initialIP == hexList[i].ToString())
                                    {
                                        IPinp = false;
                                        IP = initialIP;
                                        break;
                                    }
                                }

                                if(IPinp)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Input Hexidecimal digit");
                                }
                            }
                            changeIP = false;
                        }
                        else if (check == "N" | check == "n")
                        {
                            changeIP = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Enter valid answer");
                        }
                    }
                    Console.Clear();
                    while (changeFR)
                    {
                        Console.WriteLine("Set FR value (Y/N)");
                        check = Console.ReadLine();
                        Console.Clear();
                        if (check == "Y" | check == "y")
                        {
                            while (FRinp)
                            {
                                Console.WriteLine("Write new FR value");
                                initialFR = Console.ReadLine();
                                for (int i = 0; i < hexList.Length; i++)
                                {
                                    if (initialFR == hexList[i].ToString())
                                    {
                                        FRinp = false;
                                        FR = initialFR;
                                        break;
                                    }
                                }

                                if (FRinp)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Input Hexidecimal digit");
                                }
                            }
                            changeFR = false;
                        }
                        else if (check == "N" | check == "n")
                        {
                            changeFR = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Enter valid answer");
                        }
                    }
                    Console.Clear();
                    while (changeLI)
                    {
                        Console.WriteLine("Set LI value (Y/N)");
                        check = Console.ReadLine();
                        Console.Clear();
                        if (check == "Y" | check == "y")
                        {
                            while (LIinp)
                            {
                                Console.WriteLine("Write new LI value");
                                initialLI = Console.ReadLine();
                                for (int i = 0; i < hexList.Length; i++)
                                {
                                    if (initialLI == hexList[i].ToString())
                                    {
                                        LIinp = false;
                                        LI = initialLI;
                                        break;
                                    }
                                }

                                if (LIinp)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Input Hexidecimal digit");
                                }
                            }
                            changeLI = false;
                        }
                        else if (check == "N" | check == "n")
                        {
                            changeLI = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Enter valid answer");
                        }
                    }
                    Console.Clear();
                    while (changeAC)
                    {
                        Console.WriteLine("Set AC value (Y/N)");
                        check = Console.ReadLine();
                        Console.Clear();
                        if (check == "Y" | check == "y")
                        {
                            while (ACinp)
                            {
                                Console.WriteLine("Write new AC value");
                                initialAC = Console.ReadLine();
                                for (int i = 0; i < hexList.Length; i++)
                                {
                                    if (initialAC == hexList[i].ToString())
                                    {
                                        ACinp = false;
                                        AC = initialAC;
                                        break;
                                    }
                                }

                                if (ACinp)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Input Hexidecimal digit");
                                }
                            }
                            changeAC = false;
                        }
                        else if (check == "N" | check == "n")
                        {
                            changeAC = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Enter valid answer");
                        }
                    }
                    Console.Clear();

                    changeVals = false; 
                }
                else if(check == "N" | check == "n")
                {
                    changeVals = false;
                }
            }
        }

        //Tiny Machine Functions
        private static void FLA()
        {
            ConvertAllToInt();
            AcInt = 15 - AcInt;
            if(AcInt == 0)
            {
                ZF = true;
            }
            else
            {
                ZF = false;
            }

            IpInt++;

            ConvertAllToHex();
            BitToRegister();
        }

        private static void LDL()
        {
            int variable;
            string tempvar = commandchar[IpInt + 1].ToString();
            HexToInt(tempvar, out variable);

            Addr = tempvar;

            LI = commandchar[variable].ToString();
            HexToInt(LI, out LiInt);
            if(LiInt == 0)
            {
                ZF = true;
            }
            else
            {
                ZF = false;
            }

            IpInt += 2;

            ConvertAllToHex();
            BitToRegister();
        }

        private static void DEL()
        {
            IpInt++;

            LiInt -= 1;
            if(LiInt == -1)
            {
                LiInt = 15;
            }

            if(LiInt == 0)
            {
                ZF = true;
            }
            else
            {
                ZF = false;
            }

            ConvertAllToHex();
            BitToRegister();
        }

        private static void SCF()
        {
            CF = true;
            BitToRegister();
            IpInt++;
            ConvertAllToHex();
        }

        private static void CCF()
        {
            CF = false;
            BitToRegister();
            IpInt++;
            ConvertAllToHex();
        }

        private static void ADC()
        {
            int variable;
            string tempvar = commandchar[IpInt + 1].ToString();
            HexToInt(tempvar, out variable);
            Addr = tempvar;
            string otherLoc = commandchar[variable].ToString();
            int add2;
            HexToInt(otherLoc, out add2);
            HexToInt(AC, out AcInt);
            int carry = 0;
            if(CF)
            {
                carry = 1;
            }
            else
            {
                carry = 0;
            }

            int answer = add2 + AcInt + carry;
            int answer2 = 0;
            if(answer > 15)
            {
                CF = true;
                answer2 = answer - 16;
            }
            else
            {
                CF = false;
                answer2 = answer;
            }

            if((answer2 >= 8 & (add2 < 8 & AcInt < 8)) || (answer2 < 8 & (add2 >= 8 & AcInt >= 8)))
            {
                OF = true;
            }
            else
            {
                OF = false;
            }


            if(answer2 == 0)
            {
                ZF = true;
            }
            else
            {
                ZF = false;
            }

            IpInt += 2;
            AcInt = answer2;
            IntToHex(AcInt, out AC);
            ConvertAllToHex();
            BitToRegister();
        }

        private static void ROR()
        {
            string AC3;
            string AC2;
            string AC1;
            string AC0;

            int tempvar = AcInt;

            if (tempvar > 8)
            {
                AC3 = "1";
                tempvar -= 8;
            }
            else
            {
                AC3 = "0";
            }

            if (tempvar > 4)
            {
                AC2 = "1";
                tempvar -= 4;
            }
            else
            {
                AC2 = "0";
            }

            if (tempvar > 2)
            {
                AC1 = "1";
                tempvar -= 2;
            }
            else
            {
                AC1 = "0";
            }

            if (tempvar > 1)
            {
                AC0 = "1";
                tempvar -= 1;
            }
            else
            {
                AC0 = "0";
            }

            string tempcf;
            string cfstring;

            if (CF)
            {
                tempcf = "1";
                cfstring = "1";
            }
            else
            {
                tempcf = "0";
                cfstring = "0";
            }

            cfstring = AC0;
            AC0 = AC1;
            AC1 = AC2;
            AC2 = AC3;
            AC3 = tempcf;

            if (cfstring == "1")
            {
                CF = true;
            }
            else
            {
                CF = false;
            }

            if (cfstring == tempcf)
            {
                OF = false;
            }
            else
            {
                OF = true;
            }

            AcInt = int.Parse(AC3) * 8 + int.Parse(AC2) * 4 + int.Parse(AC1) * 2 + int.Parse(AC0) * 1;
            IpInt++;
        }

        private static void ROL()
        {
            string AC3;
            string AC2;
            string AC1;
            string AC0;

            int tempvar = AcInt;

            if (tempvar > 8)
            {
                AC3 = "1";
                tempvar -= 8;
            }
            else
            {
                AC3 = "0";
            }

            if (tempvar > 4)
            {
                AC2 = "1";
                tempvar -= 4;
            }
            else
            {
                AC2 = "0";
            }

            if (tempvar > 2)
            {
                AC1 = "1";
                tempvar -= 2;
            }
            else
            {
                AC1 = "0";
            }

            if (tempvar > 1)
            {
                AC0 = "1";
                tempvar -= 1;
            }
            else
            {
                AC0 = "0";
            }

            string tempcf;
            string cfstring;

            if (CF)
            {
                tempcf = "1";
                cfstring = "1";
            }
            else
            {
                tempcf = "0";
                cfstring = "0";
            }

            cfstring = AC3;
            AC3 = AC2;
            AC2 = AC1;
            AC1 = AC0;
            AC0 = tempcf;

            if (cfstring == "1")
            {
                CF = true;
            }
            else
            {
                CF = false;
            }

            if(cfstring == tempcf)
            {
                OF = false;
            }
            else
            {
                OF = true;
            }

            AcInt = int.Parse(AC3) * 8 + int.Parse(AC2) * 4 + int.Parse(AC1) * 2 + int.Parse(AC0) * 1;
            IpInt++;
        }

        private static void PUT()
        {
            outputQueue += AC;
            Out = AC;
            IpInt++;
            ConvertAllToHex();
        }

        private static void GET()
        {
            IpInt++;
            ConvertAllToHex();
            ConvertAllToInt();

            if(inputQueue == null)
            {
                HF = true;
                Console.WriteLine("Machine Starves");
            }

            if(inputchar.Length >= 1)
            {
                AC = inputchar[0].ToString();
                Inp = inputchar[0].ToString();
            }

            if(inputQueue != null)
            {
                if (inputQueue.Length > 1)
                {
                    inputQueue = "";
                    for (int i = 1; i < inputchar.Length; i++)
                    {
                        inputQueue += inputchar[i].ToString();
                    }
                }
                else
                {
                    inputQueue = null;
                }
            }

            if (AC == "0")
            {
                ZF = true;
            }
            else
            {
                ZF = false;
            }

            ConvertAllToInt();

            BitToRegister();
        }

        private static void STA()
        {
            int variable;
            string tempvar = commandchar[IpInt + 1].ToString();
            HexToInt(tempvar, out variable);
            Addr = tempvar;
            commandchar[variable] = Char.Parse(AC);
            IpInt += 2;
            commands = "";
            for (int i = 0; i < commandchar.Length; i++)
            {
                commands += commandchar[i];
            }
            ConvertAllToHex();
        }

        private static void LDA()
        {
            int variable;
            string tempvar = commandchar[IpInt + 1].ToString();
            HexToInt(tempvar, out variable);
            AC = commandchar[variable].ToString();
            Addr = tempvar;
            HexToInt(AC, out AcInt);

            if(AcInt == 0)
            {
                ZF = true;
            }
            else
            {
                ZF = false;
            }

            BitToRegister();
            IpInt += 2;
            ConvertAllToHex();
        }

        private static void JNZ()
        {
            string tempvar = commandchar[IpInt + 1].ToString();
            Addr = tempvar;

            if (ZF == false)
            {
                JMP();
            }
            else
            {
                IpInt += 2;
                ConvertAllToHex();
            }



            ConvertAllToHex();
        }

        private static void JZE()
        {
            if(ZF == true)
            {
                JMP();
            }
            else
            {
                IpInt += 2;
                ConvertAllToHex();
            }

            string tempvar = commandchar[IpInt + 1].ToString();
            Addr = tempvar;

            ConvertAllToHex();
        }

        private static void JMP()
        {
            int variable;
            string tempvar = commandchar[IpInt + 1].ToString();
            HexToInt(tempvar, out variable);

            Addr = tempvar;
            IP = tempvar;
            HexToInt(IP, out IpInt);
        }

        public static void HLT()
        {
            runCommands = false;
            HF = true;

            BitToRegister();
        }
    }
}

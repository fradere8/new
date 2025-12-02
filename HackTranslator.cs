using System.Collections.Generic;
using System.Linq;
using System;

namespace Assembler
{
    public class HackTranslator
    {
        public static readonly Dictionary<string, string> CompCodes = 
        new Dictionary<string, string>
        {
            {"0",   "101010"},
            {"1",   "111111"},
            {"-1",  "111010"},
            {"D",   "001100"},
            {"A",   "110000"},
            {"!D",  "001101"},
            {"!A",  "110001"},
            {"-D",  "001111"},
            {"-A",  "110011"},
            {"D+1", "011111"},
            {"A+1", "110111"},
            {"D-1", "001110"},
            {"A-1", "110010"},
            {"D+A", "000010"},
            {"D-A", "010011"},
            {"A-D", "000111"},
            {"D&A", "000000"},
            {"D|A", "010101"},
            {"M",   "110000"},     
            {"!M",  "110001"},     
            {"-M",  "110011"},     
            {"M+1", "110111"},     
            {"M-1", "110010"},     
            {"D+M", "000010"},     
            {"D-M", "010011"},     
            {"M-D", "000111"},     
            {"D&M", "000000"},     
            {"D|M", "010101"}      
        };

        public static readonly Dictionary<string, string> DestCodes = 
        new Dictionary<string, string>
        {
            {"null", "000"},
            {"M",    "001"},
            {"D",    "010"},
            {"MD",   "011"},
            {"A",    "100"},
            {"AM",   "101"},
            {"AD",   "110"},
            {"AMD",  "111"}
        };

        public static readonly Dictionary<string, string> JumpCodes = 
        new Dictionary<string, string>
        {
            {"null", "000"},
            {"JGT",  "001"},
            {"JEQ",  "010"},
            {"JGE",  "011"},
            {"JLT",  "100"},
            {"JNE",  "101"},
            {"JLE",  "110"},
            {"JMP",  "111"}
        };
        
        private int VariableAddress = 16;

        public string[] TranslateAsmToHack(string[] instructions, Dictionary<string, int> symbolTable)
        {
            var codes = new List<string>();
            foreach (var instruction in instructions)
            {
                if (instruction.StartsWith("@"))
                {
                    codes.Add(AInstructionToCode(instruction, symbolTable));
                }
                else
                {
                    codes.Add(CInstructionToCode(instruction));
                }
            }
            return codes.ToArray();
        }

        public string AInstructionToCode(string aInstruction, Dictionary<string, int> symbolTable)
        {
            var aInstr = aInstruction.Substring(1);
            string binaryAinstr;
            if (char.IsDigit(aInstr[0]))
            {
                var temp = int.Parse(aInstr);
                binaryAinstr = ConvertToBinary(temp);
            }
            else
            {
                var address = DetermineAddress(aInstr, symbolTable);
                binaryAinstr = ConvertToBinary(address);    
            }
            return binaryAinstr;
        }

        public string CInstructionToCode(string cInstruction)
        {
            SplitCInstruction(cInstruction, out string dest, out string comp, out string jump);

            var aBit = GetABit(comp);
            var destBinary = DestCodes[dest];
            var compBinary = CompCodes[comp];
            var jumpBinary = JumpCodes[jump];

            return "111" + aBit + compBinary + destBinary + jumpBinary;
        }

        private void SplitCInstruction(string cInstr, out string dest, out string comp, out string jump)
        {
            dest = "null";
            comp = "";
            jump = "null";

            if (cInstr.Contains(";"))
            {
                var parts = cInstr.Split(';');
                cInstr = parts[0];
                jump = parts[1];
            }

            if (cInstr.Contains("="))
            {
                var parts = cInstr.Split('=');
                dest = parts[0];
                comp = parts[1];
            }
            else
            {
                comp = cInstr;
            }
        }

        public static string GetABit(string comp)
        {
            return comp.Contains("M") ? "1" : "0";
        }

        private string ConvertToBinary(int value)
        {
            return Convert.ToString(value, 2).PadLeft(16, '0');
        }

        private int DetermineAddress(string aInstr, Dictionary<string, int> symbolTable)
        {
            int address;
            if (symbolTable.ContainsKey(aInstr))
            {
                address = symbolTable[aInstr];
            }
            else
            {
                address = VariableAddress;
                symbolTable[aInstr] = address;
                VariableAddress++;
            }
            
            return address;
        }
    }
}


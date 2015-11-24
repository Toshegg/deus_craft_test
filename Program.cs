using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeusCraft
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "";

            if (args.Contains<string>("-h"))
            {
                Console.WriteLine("Type -f 'filename' to enter filename\nType -m 'find' -s 'string' to find byte offset of each entry of 'string' in file\nType -m 'checksum' to find checksum of each 32-bit word in file\nType -h to display info about commands and parameters");
                return;
            }

            if (args.Contains<string>("-f"))
            {
                filename = args[Array.IndexOf(args, "-f") + 1];
                if (!File.Exists(filename))
                {
                    Console.WriteLine("File {0} doesn't exist", filename);
                    return;
                }
            }

            if (args.Contains<string>("-m"))
            {
                var mode = args[Array.IndexOf(args, "-m") + 1];
                switch (mode) {
                    case "find":
                        string str;
                        if (args.Contains<string>("-s"))
                        {
                            str = args[Array.IndexOf(args, "-s") + 1];
                        }
                        else
                        {
                            Console.WriteLine("Please enter -s 'string_to_find'");
                            return;
                        }
                        var offsets = ProgramHelper.Find(filename, str);
                        Console.WriteLine("Offsets: {0}", string.Join(" ", offsets.ToArray().Select(x => x.ToString()).ToArray()) );
                        break;
                    case "checksum":
                        var checksum = ProgramHelper.Checksum(filename);
                        Console.WriteLine("Checksum: {0}", checksum);
                        break;
                    default:
                        Console.WriteLine("Please, enter 'find' or 'checksum' as mode");
                        return;
                }
            }
           
        }
    }
}

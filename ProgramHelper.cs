using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeusCraft
{
    class ProgramHelper
    {
        public static long Checksum(string filename)
        {
            byte[] data = new byte[4];
            long checksum = 0;
            int read_count;
            using (Stream fs = File.OpenRead(filename))
            {

                for (int i = 0; i < fs.Length; i += 4)
                {
                    read_count = 4;

                    if (i + 4 >= fs.Length)
                    {
                        read_count = (int)fs.Length - i;
                    }
                    
                    fs.Read(data, 0, read_count);
                    checksum += BitConverter.ToInt32(data, 0);
                }
                fs.Close();
            }
            return checksum;
        }

        public static List<int> Find(string filename, string str)
        {
            int equal_count;
            List<int> offsets = new List<int>();
            byte[] byte_str = GetBytes(str);
            byte[] data;

            using (Stream fs = File.OpenRead(filename))
            {
                data = new byte[fs.Length];
                fs.Read(data, 0, (int) fs.Length);
                fs.Close();
            }

            data = Array.FindAll(data, b => b != 0);
            for (int i = 0; i < data.Length; i++)
            { 
                if (data[i] == byte_str[0])
                {
                    equal_count = 1;
                    for (int j = 1; j < byte_str.Length && i + j < data.Length; j++)
                    {
                        if (data[i + j] == byte_str[j])
                            equal_count++;
                        else break;
                    }

                    if (equal_count == byte_str.Length)
                        offsets.Add(i);
                }
            }
            return offsets;
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            //this deletes zero bytes. need for letters, which value < 256 in 2-bytes encodings
            return Array.FindAll(bytes, b => b != 0);
        }

    }
}

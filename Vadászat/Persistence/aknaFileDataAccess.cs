using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Vadaszat.Persistence
{
    public class aknaFileDataAccess : IsaknaDataAccess
    {
        public async Task<(aknaTable, int, int, int, int, int)> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    String line = await reader.ReadLineAsync() ?? String.Empty;
                    string[] ser = line.Split(' ');
                    int a = Convert.ToInt32(ser[0]);
                    aknaTable table = new aknaTable(a-2);
                    for (Int32 i = 0; i < a; i++)
                    {
                        line = await reader.ReadLineAsync() ?? String.Empty;
                        string[] numbers = line.Split(' ');

                        for (int j = 0; j < a; j++)
                        {
                            table.SetValue(i, j, Convert.ToInt32(numbers[j]));
                        }
                    }
                    for (Int32 i = 0; i < a; i++)
                    {
                        line = await reader.ReadLineAsync() ?? String.Empty;
                        string[] numbers = line.Split(' ');

                        for (int j = 0; j < a; j++)
                        {
                            table.Setbomb(i, j, Convert.ToInt32(numbers[j]));
                        }
                    }
                    int bon = 0;
                    for (Int32 i = 0; i < a; i++)
                    {
                        line = await reader.ReadLineAsync() ?? String.Empty;
                        string[] numbers = line.Split(' ');
                        for (int j = 0; j < a; j++)
                        {
                            table.SetFelnyil(i, j, Convert.ToInt32(numbers[j]));
                            if (Convert.ToInt32(numbers[j])==0)
                            {
                                bon++;
                            }
                        }
                    }
                    table.Setfelnyit(Convert.ToInt32(ser[5]));
                    table.Setbomb(bon);

                    return (table, a, Convert.ToInt32(ser[1]), Convert.ToInt32(ser[2]), Convert.ToInt32(ser[3]), Convert.ToInt32(ser[4]));
                }
            }
            catch
            {
                throw new aknaDataException();
            }
        }

        public async Task SaveAsync(String path, aknaTable table, int ax, int ay, int bx, int by, int bol)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
                {
                    writer.WriteLine(table.Lengthlist() + " " + ax + " " + ay + " " + bx + " " + by + " " + bol + " " + table.GetFelnyit()); // kiírjuk a méreteket
                    for (int i = 0; i < table.Lengthlist(); i++)
                    {
                        for (int j = 0; j < table.Lengthlist(); j++)
                        {
                            await writer.WriteAsync(table.GetValue(i,j) + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                    for (int i = 0; i < table.Lengthlist(); i++)
                    {
                        for (int j = 0; j < table.Lengthlist(); j++)
                        {
                            await writer.WriteAsync(table.Getbomb(i,j) + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                    for (int i = 0; i < table.Lengthlist(); i++)
                    {
                        for (int j = 0; j < table.Lengthlist(); j++)
                        {
                            await writer.WriteAsync(table.GetFelnyit(i, j) + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new aknaDataException();
            }
        }
    }
}

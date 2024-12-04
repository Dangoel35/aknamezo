using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vadaszat.Model;
using System.Drawing;

namespace Vadaszat.Persistence;

public class aknaTable
{
    private int[,] list;
    private bool[,] bomb = null!;
    private bool[,] felfedett;
    private int felnyitott;
    private int bombszam;
    public void Setbomb(int a)
    {
        bombszam = a;
    }
    public int GetFelnyit()
    {
        return felnyitott;
    }
    public void Setfelnyit(int a)
    {
        felnyitott = a;
    }
    public int Lengthlist()
    {
        return list.GetLength(0);
    }
    public int GetValue(int x, int y)
    {
        return list[x, y];

    }
    public void SetValue(int x, int y, int value)
    {
        list[x, y] = value;
    }
    public void Tolt(int x)
    {
        for (int i = 1; i < x + 1; i++)
        {
            for (int j = 1; j < x + 1; j++)
            {
                list[i, j] = 0;
                bomb[i, j] = false;
                felfedett[i, j] = false;
            }
        }
        for (int i = 0; i < x + 2; i++)
        {
            list[i, 0] = -30;
            list[i, x + 1] = -30;
            list[0, i] = -30;
            list[x + 1, i] = -30;
        }
        if (x == 6)
        {
            generate(5);
        }
        if (x == 10)
        {
            generate(10);
        }
        if (x == 16)
        {
            generate(25);
        }
    }
    public aknaTable(int x)
    {
        list = new int[x+2,x+2];
        bomb = new bool[x+2,x+2];
        for (int i = 0; i< x+2; i++)
        {
            for (int u = 0;u < x+2; u++)
            {
                bomb[i,u] = false;
            }
        }
        felfedett = new bool[x+2,x+2];
        felnyitott = 0;
    }
    public (int rb, List<FieldEventArgs> listaa) felnyit(int x, int y, bool elso)
    {
        List<FieldEventArgs> ere = new List<FieldEventArgs>();
        if (felfedett[x,y] == false)
        {
            if (bomb[x, y] == false)
            {
                if (list[x, y] == 0)
                {
                    felfedett[x, y] = true;
                    if (elso) {
                        ere.Add(new FieldEventArgs(x, y, Color.Gray));
                    }
                    else
                    {
                        ere.Add(new FieldEventArgs(x, y, Color.White));
                    }
                    var r = felnyit(x + 1, y, false);
                    var wer = felnyit(x + 1, y + 1, false);
                    var ter = felnyit(x + 1, y - 1, false);
                    var ser = felnyit(x, y - 1, false);
                    var ler = felnyit(x, y + 1, false);
                    var qer = felnyit(x - 1 + 1, y - 1, false);
                    var cer = felnyit(x - 1, y, false);
                    var per = felnyit(x - 1, y + 1, false);
                    if (r.rb != 0 && wer.rb != 0 && ter.rb != 0 && ser.rb != 0 && ler.rb != 0 && qer.rb != 0 && cer.rb != 0 && per.rb != 0)
                    {
                        foreach (FieldEventArgs da in r.listaa)
                        {
                            ere.Add(da);
                        }
                        foreach (FieldEventArgs da in wer.listaa)
                        {
                            ere.Add(da);
                        }
                        foreach (FieldEventArgs da in ter.listaa)
                        {
                            ere.Add(da);
                        }
                        foreach (FieldEventArgs da in ser.listaa)
                        {
                            ere.Add(da);
                        }
                        foreach (FieldEventArgs da in ler.listaa)
                        {
                            ere.Add(da);
                        }
                        foreach (FieldEventArgs da in qer.listaa)
                        {
                            ere.Add(da);
                        }
                        foreach (FieldEventArgs da in cer.listaa)
                        {
                            ere.Add(da);
                        }
                        foreach (FieldEventArgs da in per.listaa)
                        {
                            ere.Add(da);
                        }
                        if (x != 0 && x != Lengthlist() - 1 && y != 0 && y != Lengthlist() - 1)
                        {

                            felnyitott++;
                            if (felnyitott + bombszam >= (Lengthlist() - 2) * (Lengthlist() - 2))
                            {
                                //MessageBox.Show("Döntetlen");
                                return (0, ere);

                            }
                            else
                            {
                                return (1, ere);
                            }
                        }
                        return (1, ere);
                    }
                    else
                    {
                        //MessageBox.Show("Döntetlen");
                        return (0, ere);
                    }
            
                }
                else
                {
                    felfedett[x, y] = true;
                    if (x != 0 && x != list.GetLength(0) - 1 && y != 0 && y != list.GetLength(0) - 1)
                    {
                        if (elso)
                        {
                            ere.Add(new FieldEventArgs(x, y, Color.Gray));
                        }
                        else
                        {
                            ere.Add(new FieldEventArgs(x, y, Color.White));
                        }
                        if (x != 0 && x != Lengthlist() - 1 && y != 0 && y != Lengthlist() - 1)
                        {
                            felnyitott++;
                            if (felnyitott + bombszam >= (Lengthlist() - 2) * (Lengthlist() - 2))
                            {
                                //MessageBox.Show("Döntetlen");
                                return (0,ere);

                            }
                            else
                            {
                                return (1,ere);
                            }
                        }
                        return (1,ere);
                    }
                }
            }
            else
            {
                return (2,ere);
            }
        }
        else
        {
            List<FieldEventArgs> arat = new List<FieldEventArgs>();
            if (elso)
            {
                arat.Add(new FieldEventArgs(x,y,Color.Gray));
                return (1, arat);
            }
            else
            {
                return (1, arat);
            }
        
        }
        return (-1, ere);
    }
    private void generate(int t)
    {
        for (int i = 0; i < t; i++)
        {
            random();
        }
        bombszam = t;
    }
    private void random()
    {
        Random rand = new Random();
        int randomSzam = rand.Next(1, list.GetLength(1) - 2);
        int randomitas = rand.Next(1, list.GetLength(1) - 2);
        if ((randomSzam == 1 && randomitas == 1) || (randomSzam == list.GetLength(1) - 2 && randomitas == list.GetLength(1)-2) != true)
        {
            //list[randomSzam,randomitas].bomb
            if (this.bomb[randomSzam, randomitas] != true)
            {
                this.bomb[randomSzam, randomitas] = true;
                this.list[randomSzam+1, randomitas+1]++;
                this.list[randomSzam+1, randomitas]++;
                this.list[randomSzam+1, randomitas-1]++;
                this.list[randomSzam, randomitas-1]++;
                this.list[randomSzam, randomitas+1]++;
                this.list[randomSzam-1, randomitas+1]++;
                this.list[randomSzam-1, randomitas]++;
                this.list[randomSzam-1, randomitas-1]++;
            }
            else
            {
                random();
            }
        }
        else
        {
            random();
        }
    }
    public int Getbomb(int xx, int yy)
    {
        if(this.bomb[xx, yy])
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
    public void Setbomb(int xx, int yy, int b)
    {
        if (b==0)
        {
            bomb[xx, yy] = true;
        }
        else
        {
            bomb[xx, yy] = false;
        }
    }
    public int GetFelnyit(int xx, int yy)
    {
        if (this.felfedett[xx,yy])
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
    public void SetFelnyil(int xx, int yy, int b)
    {
        if (b==0)
        {
            felfedett[xx, yy] = true;
        }
        else
        {
            felfedett[xx, yy] = false;
        }
    }

}

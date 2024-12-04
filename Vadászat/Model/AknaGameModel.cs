using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vadaszat.Persistence;
using System.Drawing;

namespace Vadaszat.Model
{
    public class AknaGameModel
    {
        private aknaTable table = null!;
        private IsaknaDataAccess isDataAccess;
        private int ax;
        private int ay;
        private int bx;
        private int by;
        private bool ab;
        private bool isGameOver;
        private int hossz;
        public void Sethossz(int x)
        {
            hossz = x;

        }
        public void Setax(int a)
        {
            ax = a;
        }
        public int Getax()
        {
            return ax;
        }
        public void Setab(int a)
        {
            if (a==0)
            {
                ab = true;
            }
            else
            {
                ab = false;
            }
        }
        public void Setay(int a)
        {
            ay = a;
        }
        public int Getay()
        {
            return ay;
        }
        public void Setbx(int a)
        {
            bx = a;
        }
        public int Getbx()
        {
            return bx;  
        }
        public void Setby(int a)
        {
            by = a;
        }
        public int Getby()
        {
            return by;
        }
        public int GetFelfed(int x, int y)
        {
            return table.GetFelnyit(x ,y);
        }
        public int GetValue(int x, int y)
        {
            return table.GetValue(x ,y);
        }
        public bool GetisGameover()
        {
            return isGameOver;
        }
        public AknaGameModel(IsaknaDataAccess ad)
        {
            isGameOver = false;
            this.isDataAccess = ad;
        }
        public void setaknaTable(aknaTable ta)
        {
            table = ta;
        }
        public aknaTable Table { get { return table; } }
        public void megnez(int x, int y)
        {
          
            x = x + 1;
            y = y + 1;
            if (ab)
            {
                if (((x == ax - 1 || x == ax + 1) && (y == ay)) || (x == ax && (y == ay + 1 || y == ay - 1)))
                {
                    var t = table.felnyit(x, y, true);
                    if (t.rb==1)
                    {
                        ab = false;
                        t.listaa.Add(new FieldEventArgs(ax, ay, Color.White));
                        felfed(t.listaa);
                        ax = x;
                        ay = y;
                    }
                    else if (t.rb == 0)
                    {
                        //MessageBox.Show("Döntetlen");
                        isGameOver = true;
                        Gameover?.Invoke(this, new OverEventArgs(2));
                
                    }
                    else
                    {
                        //MessageBox.Show("A jobboldali játékos nyert");
                        isGameOver=true;
                        Gameover?.Invoke(this, new OverEventArgs(1));
                    }
                }
                else
                {

                }
            }
            else
            {
                if ((x == bx - 1 || x == bx + 1) && (y == by - 1 || y == by + 1 || y == by) || (x == bx && (y == by + 1 || y == by - 1)))
                {
                    var t = table.felnyit(x, y, true);
                    if (t.rb==1)
                    {
                        ab = true;
                        t.listaa.Add(new FieldEventArgs(bx, by, Color.White));
                        felfed(t.listaa);
                        bx = x;
                        by = y;
                    }
                    else if (t.rb==0)
                    {
                        //MessageBox.Show("Döntetlen");
                        isGameOver = true;
                        Gameover?.Invoke(this, new OverEventArgs(2));
                    }
                    else
                    {
                        //MessageBox.Show("A baloldali játékos nyert");
                        isGameOver = true;
                        Gameover?.Invoke(this, new OverEventArgs(0));
                    }
                }
                else
                {
                }
            }
        }
        public void newgame(int x)
        {
            isGameOver = false;
            ab = true;
            ax = 1;
            ay = 1;
            bx = x;
            by = x;
            hossz = x + 2;
            table = new aknaTable(x);
            table.Tolt(x);
            //var tas = table.felnyit(1, 1, true);
            //var sem = table.felnyit(x, x, true);
            //felfed(tas.listaa);
            //felfed(sem.listaa);
        }
        public int Gethossz() {
            return hossz;
        }
        public async Task SaveGameAsync(String path)
        {
            if (this.isDataAccess == null)
                throw new InvalidOperationException("No data access is provided.");
            if (ab)
            {
                await isDataAccess.SaveAsync(path, table, ax, ay, bx, by, 0);
            }
            else
            {
                await isDataAccess.SaveAsync(path, table, ax, ay, bx, by, 1);
            }
        }
        public async Task LoadGameAsync(String path)
        {
            if (this.isDataAccess == null)
                throw new InvalidOperationException("No data access is provided.");
            var s = await isDataAccess.LoadAsync(path);
            table = s.Item1;
            hossz = s.Item2;
            ax = s.Item3;
            ay = s.Item4;
            bx = s.Item5;
            by = s.Item6;
        }

        public event EventHandler<FieldEventArgs>? Mezofelfed;

        public event EventHandler<OverEventArgs>? Gameover;
        public void felfed(List<FieldEventArgs> a)
        {
            foreach (FieldEventArgs ata in a)
            {
                Mezofelfed?.Invoke(this, ata);
            }
        }
    }
}

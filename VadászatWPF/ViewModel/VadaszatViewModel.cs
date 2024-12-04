using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using Vadaszat;
using Vadaszat.Model;
using Vadaszat.Persistence;

namespace VadászatWPF.ViewModel
{
    public class VadaszatViewModel : ViewModelBase
    {

        private AknaGameModel _model;

        private aknaFileDataAccess _dataAccess;

        public DelegateCommand LoadGameCommand { get; private set; }


        public DelegateCommand SaveGameCommand { get; private set; }


        public DelegateCommand Newgame6 { get; private set; }
        public DelegateCommand Newgame10 { get; private set; }
        public DelegateCommand Newgame16 { get; private set; }



        public ObservableCollection<Field> Fields { get; set; }

        



        

        private int mapSize;
        public int MapSize
        {
            get { return mapSize; }
            set 
            {
              mapSize = value;
              RefreshTable();
              OnPropertyChanged(nameof(MapSize)); 
            }
        }


        public event EventHandler? LoadGame;
        public event EventHandler? SaveGame;
        


        public VadaszatViewModel(AknaGameModel model)
        {
            _model = model;
            _dataAccess = new aknaFileDataAccess();
            //esemenyek
            _model.Gameover += new EventHandler<OverEventArgs>(Model_GameOver);
            _model.Mezofelfed += new EventHandler<FieldEventArgs>(Felfedes);

            //parancsok
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            Newgame6 = new DelegateCommand(param => MakenewGame6());
            Newgame10 = new DelegateCommand(param => MakenewGame10());
            Newgame16 = new DelegateCommand(param => MakenewGame16());

            Fields = new ObservableCollection<Field>();

            for (int i = 0; i < _model.Gethossz()-2; i++) // inicializáljuk a mezőket
            {
                for (int j = 0; j < _model.Gethossz()-2; j++)
                {
                    Fields.Add(new Field
                    {
                        IsHidden = 1,
                        Text = i.ToString() + " " + j.ToString(),
                        X = i,
                        Y = j,
                        ButtonClick = new DelegateCommand(param =>
                        {
                            if (param is Tuple<Int32, Int32> position)
                                Felnyit(position.Item1, position.Item2);

                        })
                    }); ;
                }
            }



        }
        private void felf(int x, int y)
        {
            _model.megnez(x, y);
        }
        private void Felfedes(object? sender, FieldEventArgs e)
        {
            foreach (Field t in Fields)
            {
                if (t.X == e.X-1 && t.Y == e.Y-1)
                {
                    t.Text = _model.GetValue(t.X + 1, t.Y + 1).ToString();
                    if (e.Color==System.Drawing.Color.Gray)
                    {
                        t.IsHidden = 3;
                    }
                    else
                    {
                        t.IsHidden=2;
                    }
                }
            }
        }
        private void Model_GameOver(object? sender, EventArgs e)
        {
            Fields.Clear();
        }
        private void Felnyit(int x, int y)
        {

            _model.megnez(x,y);
        }


        private void RefreshTable()
        {
            Fields.Clear();

            for (int i = 0; i < _model.Gethossz()-2; i++)
            {
                for (int j = 0; j < _model.Gethossz()-2; j++)
                {
                    int a =0;
                    string z = string.Empty;
                    if ((i==_model.Getax()-1 && j ==_model.Getay()-1) || (i ==_model.Getbx()-1 && j==_model.Getby()-1))
                    {
                        a = 3;
                        z = _model.GetValue(i + 1, j + 1).ToString();
                    }
                    else if (_model.GetFelfed(i+1, j+1)==0)
                    {
                        a = 2;
                        z = _model.GetValue(i + 1, j + 1).ToString();
                    }
                    else if (_model.GetFelfed(i+1, j+1) == 1)
                    {
                        a = 1;
                        z = string.Empty;
                    }
                    Fields.Add(new Field
                    {
                        IsHidden = a,
                        Text = z,
                        X = i,
                        Y = j,
                        ButtonClick = new DelegateCommand(param =>
                        {
                            if (param is Tuple<Int32, Int32> position)
                                Felnyit(position.Item1, position.Item2);
                        })
                    });
                }
            }


            OnPropertyChanged(nameof(MapSize));

        }
        private void MakenewGame16()
        {
            _model.newgame(16);
            MapSize = 16;
            felf(0, 0);
            felf(15, 15);

        }
        private void MakenewGame10()
        {
            _model.newgame(10);
            MapSize = 10;
            felf(0, 0);
            felf(9, 9);
        }
        private void MakenewGame6()
        {
            _model.newgame(6);
            MapSize = 6;
            felf(0, 0);
            felf(5, 5);
        }


        private void OnLoadGame()
        {
            // Update properties
            LoadGame?.Invoke(this, EventArgs.Empty);
            
        }
        private void OnSaveGame()
        {
            SaveGame?.Invoke(this, EventArgs.Empty);
        }


    }
}

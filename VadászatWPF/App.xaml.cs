using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Vadaszat.Model;
using Vadaszat.Persistence;
using VadászatWPF.ViewModel;


namespace VadászatWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AknaGameModel _model = null!;
        private VadaszatViewModel _viewModel = null!;
        private MainWindow _view = null!;

        private IsaknaDataAccess? _dataAccess;

        
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object? sender, StartupEventArgs e)
        {
            _dataAccess = new aknaFileDataAccess();
            _model = new AknaGameModel(_dataAccess);
            _model.Gameover += model_GotWinner;

            //viewmodel
            _viewModel = new VadaszatViewModel(_model);
            _viewModel.LoadGame += ViewModel_LoadGame;
            _viewModel.SaveGame += ViewModel_SaveGame;

            //view
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();

        }
        private async void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        await _model.SaveGameAsync(saveFileDialog.FileName);
                    }
                    catch (aknaDataException)
                    {
                        
                    }
                }
            }
            catch
            {
                MessageBox.Show("A fájl mentése sikertelen!", "Vadászat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    // játék betöltése
                    await _model.LoadGameAsync(openFileDialog.FileName);
                    _viewModel.MapSize = _model.Gethossz()-2;
                    
                                        
                }
            }
            catch (aknaDataException)
            {
                MessageBox.Show("A fájl betöltése sikertelen!", "Vadászat", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void model_GotWinner(object? sender, OverEventArgs e)
        {
            if (e.nyertes()==0)
            {
                MessageBox.Show("A bal oldali jatekos nyert");
            }
            else if (e.nyertes() == 1)
            {
                MessageBox.Show("A jobb oldali jatekos nyert");
            }
            else
            {
                MessageBox.Show("Döntetlen lett");
            }
        }
      
    }
}

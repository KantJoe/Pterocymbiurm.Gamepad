using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Pterocymbiurm.Gamepad.WpfUI.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        private GamepadInputViewModel _gamepadInputViewModel;
        public GamepadInputViewModel GamepadInputViewModel
        {
            get => _gamepadInputViewModel;
            set
            {
                SetProperty(ref _gamepadInputViewModel, value, nameof(GamepadInputViewModel));
            }
        }

        public ICommand StartCaptureCMD { get; set; }

        public ICommand StopCaptureCMD { get; set; }

        public MainViewModel()
        {
            GamepadInputViewModel = new GamepadInputViewModel();
            StartCaptureCMD = new RelayCommand<object>(OnStartCapture);
            StopCaptureCMD = new RelayCommand<object>(OnStopCapture);
        }

        private void OnStartCapture(object? state)
        {
            GamepadInputViewModel.StartCapture();
        }

        private void OnStopCapture(object? state)
        {
            GamepadInputViewModel.StopCapture();
        }
    }
}

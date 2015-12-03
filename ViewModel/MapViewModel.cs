﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LevelEditor.Model;
using Microsoft.Practices.ServiceLocation;

namespace LevelEditor.ViewModel
{
    public class MapViewModel : ViewModelBase 
    {
        public MainViewModel MainViewModel { get; set; }

        public EditorWindow LevelView
        {
            get
            {
                return MainViewModel.MainModel.MapView;
            }
            set
            {
                if (MainViewModel.MainModel.MapView != value)
                {
                    MainViewModel.MainModel.MapView = value;
                    RaisePropertyChanged(() => LevelView);
                }
            }
        }

        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand ScrollChangedCommand { get; private set; }
        private void CreateCommands()
        {
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);
            ScrollChangedCommand = new RelayCommand(ScrollChanged);
        }

        public MapViewModel()
        {
            CreateCommands();
            MainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();

        }

        private void Undo()
        {
            LevelView.Editor.Undo();
        }
        private void ScrollChanged()
        {
            //if()
        }

        private void Redo()
        {
            LevelView.Editor.Redo();
        }
    }
}

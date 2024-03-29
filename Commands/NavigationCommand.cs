﻿using System;
using Autosalon.Base;
using Autosalon.Infrastructure;

namespace Autosalon.Commands
{

    class NavigationCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly Navigator _navigator;
        private readonly Func<TViewModel> _createViewModel;
        public NavigationCommand(Navigator navigator, Func<TViewModel> createViewModel)
        {
            _navigator = navigator;
            _createViewModel = createViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _navigator.CurrentViewModel = _createViewModel();
        }
    }
}
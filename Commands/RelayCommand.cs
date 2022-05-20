using System;
using Autosalon.Base;


namespace Autosalon.Commands;

public class RelayCommand : CommandBase
{
    readonly Action<object> _execute;
    readonly Func<object, bool> _canExecute;
    public RelayCommand(Action<object> executeDelegate, Func<object, bool> canExecuteDelegate)
    {
        _execute = executeDelegate;
        _canExecute = canExecuteDelegate;
    }
    public override bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }
    public override void Execute(object parameter)
    {
        _execute(parameter);
    }
}
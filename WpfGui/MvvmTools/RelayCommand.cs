using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfGui.MvvmTools
{
    /// <summary>
    /// Реализация ICommand для создания методов для кнопок в WPF
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand where T : class {
        private readonly Func<bool> _canExecute;
        private readonly Action<T> _execute;
        
        //Ассинхронный метод обработки кнопки
        private readonly Func<T, Task> _executeAsync;

        public RelayCommand(Func<bool> canUpdateUser, Action<T> executeUser) {
            _canExecute = canUpdateUser;
            _execute = executeUser;
        }
        public RelayCommand(Func<bool> canUpdateUser, Func<T, Task> executeAsync) {
            _canExecute = canUpdateUser;
            _executeAsync = executeAsync;
        }

        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) {
            return _canExecute();
        }

        //попытка реализовать ассинхронный обработчик кнопки
        //!!await в async void - плохо, однако методы бизнес логики все равно асинхронные, добавление асинхронного обработчика здесь позволит оставить здесь все такие моменты
        public async void Execute(object parameter) {
            if (_executeAsync != null)
               await _executeAsync((T)parameter);
            else if (_execute != null)
                _execute((T)parameter);
        }
    }
}

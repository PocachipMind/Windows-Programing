using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbAsyncEx.ViewModel.Command
{
    public class AsyncTestButtonCommand : ICommand
    {
        private MainWindowVM mainWindowVM { get; set; }

        public AsyncTestButtonCommand(MainWindowVM mainWindowVM)
        {
            this.mainWindowVM = mainWindowVM;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Task task1 = Task.Run(() =>
            {
                mainWindowVM.AsyButtonIsEnable = false;
                for (int i = 0; i <= 100; i++)
                {
                    mainWindowVM.ProgressValue = i;
                    Thread.Sleep(250);
                }
                mainWindowVM.AsyButtonIsEnable = true;
            });

            // task1.Wait();
        }
        // AsynRelayCommand (NuGet 패키지 : Microsoft.Toolkit.Mvvm ) 가능

        // 즉, VM에서 
        // Command = new AsynRelayCommand();
        // async 및 await를 사용해 함수를 선언하고 넣어줍니다.
    }
}

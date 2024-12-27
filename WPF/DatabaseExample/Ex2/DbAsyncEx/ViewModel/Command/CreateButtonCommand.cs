using DbAsyncEx.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Xml.Linq;

// 인프런 강의 기반
namespace DbAsyncEx.ViewModel.Command
{

    public class CreateButtonCommand : ICommand
    {
        private MainWindowVM mainWindowVM { get; set; }

        public CreateButtonCommand(MainWindowVM mainWindowVM)
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
            DataSet ds = new DataSet();
            List<USERINFO> listUserTemp = new List<USERINFO>();
            Exception exectpion = null;

            Task t = Task.Run(() =>
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionStr))
                    {
                        sqlConnection.Open();
                        SqlCommand sqlCommand = sqlConnection.CreateCommand();
                        sqlCommand.CommandText = "INSERT INTO USERINFO(USERNAME,USERGENDER,USERAGE,USERJOB,USERMBTI) VALUES('"+ mainWindowVM.Name + "','"+ mainWindowVM .Gender+ "',"+ mainWindowVM.Age+ ",'"+ mainWindowVM.Job+ "','"+ mainWindowVM.Mbti + "');";
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }

                }
                catch (Exception ex)
                {
                    exectpion = ex;
                }
            });

            t.Wait();

            if (exectpion != null)
            {
                MessageBox.Show(exectpion.Message.ToString());
            }

            mainWindowVM.ReadButtonCommand.Execute(null);
        }
    }
}

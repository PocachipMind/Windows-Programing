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

namespace DbAsyncEx.ViewModel.Command
{
    public class DeleteButtonCommand: ICommand
    {
        public MainWindowVM VM { get; set; }

        public DeleteButtonCommand(MainWindowVM vm)
        {
            VM = vm;
        }

        // 해당 갱신을 통해 CanExecute 갱신 가능
        public event EventHandler? CanExecuteChanged;
        
        // Command 컨트롤 실행 가능 상태 반환
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        // Command 실행 동작 로직
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
                        // 모든 속성과 일치하는 데이터를 삭제하는 SQL 쿼리
                        sqlCommand.CommandText = "DELETE FROM USERINFO " +
                                                 "WHERE USERNAME = '" + VM.Name + "' " +
                                                 "AND USERGENDER = '" + VM.Gender + "' " +
                                                 "AND USERAGE = " + VM.Age + " " +
                                                 "AND USERJOB = '" + VM.Job + "' " +
                                                 "AND USERMBTI = '" + VM.Mbti + "';";
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

            VM.ReadButtonCommand.Execute(null);
        }
    }
}

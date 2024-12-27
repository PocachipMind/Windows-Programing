using DbAsyncEx.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

// 학부 강의 기반
namespace DbAsyncEx.ViewModel.Command
{
    public class ReadButtonCommand : ICommand
    {
        public MainWindowVM VM { get; set; }


        public ReadButtonCommand(MainWindowVM vm)
        {
            VM = vm;
        }

        // 해당 갱신을 통해 CanExecute 갱신 가능
        public event EventHandler? CanExecuteChanged;
        //{
        //    add
        //    {
        //        CommandManager.RequerySuggested += value;
        //    }

        //    remove
        //    {
        //        CommandManager.RequerySuggested -= value;
        //    }
        //}

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

                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT * FROM USERINFO;", sqlConnection);
                        sqlDataAdapter.Fill(ds);
                    }

                    if (ds.Tables.Count != 0)
                    {
                        DataTable dt = ds.Tables[0];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            USERINFO userInfo = new USERINFO();
                            userInfo.USERNAME = dt.Rows[i]["USERNAME"].ToString();
                            userInfo.USERGENDER = dt.Rows[i]["USERGENDER"].ToString();
                            userInfo.USERAGE = Int32.Parse(dt.Rows[i]["USERAGE"].ToString());
                            userInfo.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                            userInfo.USERMBTI = dt.Rows[i]["USERMBTI"].ToString();

                            listUserTemp.Add(userInfo);
                        }

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

            VM.MyListUser = listUserTemp;
        }
    }
}

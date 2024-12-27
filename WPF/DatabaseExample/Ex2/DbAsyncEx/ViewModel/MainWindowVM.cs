using DbAsyncEx.Model;
using DbAsyncEx.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAsyncEx.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private int progressValue;
        public int ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                OnPropertyChanged("ProgressValue");
            }
        }

        private bool asyButtonIsEnable;
        public bool AsyButtonIsEnable
        {
            get { return asyButtonIsEnable; }
            set
            {
                asyButtonIsEnable = value;
                OnPropertyChanged("AsyButtonIsEnable");
            }
        }

        private List<USERINFO> myListUser = new List<USERINFO>();

        private string name;
        private int age;
        private string gender;
        private string job;
        private string mbti;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }


        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string Job
        {
            get { return job; }
            set
            {
                job = value;
                OnPropertyChanged(nameof(Job));
            }
        }

        public string Mbti
        {
            get { return mbti; }
            set
            {
                mbti = value;
                OnPropertyChanged(nameof(Mbti));
            }
        }



        public List<USERINFO> MyListUser
        {
            get { return myListUser; }
            set
            {
                myListUser = value;
                OnPropertyChanged(nameof(MyListUser));
            }
        }


        public ReadButtonCommand ReadButtonCommand { get; set; }
        public CreateButtonCommand CreateButtonCommand { get; set; }
        public DeleteButtonCommand DeleteButtonCommand { get; set; }
        public AsyncTestButtonCommand AsyncTestButtonCommand { get; set; }


        public MainWindowVM()
        {
            ReadButtonCommand = new ReadButtonCommand(this);
            CreateButtonCommand = new CreateButtonCommand(this);
            DeleteButtonCommand = new DeleteButtonCommand(this);
            AsyncTestButtonCommand = new AsyncTestButtonCommand(this);
            AsyButtonIsEnable = true;
        }

        //의존속성 함수 구현
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using System;
using System.Windows.Forms;
using OpenCvSharp;

namespace OpenCV_Practice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IplImage src;

        private void Form1_Load(object sender, EventArgs e)
        {
            src = new IplImage("../../Lamp.jpg");

            // 클래스이름, 지역변수 이름, 생성자, 클래스 이름
            OpenCV_Class Convert = new OpenCV_Class();

            pictureBoxIpl1.ImageIpl = src;
            pictureBoxIpl2.ImageIpl = Convert.BitwiseMat(src);
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cv.ReleaseImage(src); // 헤더 데이터 해제
            if (src != null) { src.Dispose(); } // src데이터 해제
        }

    }
}
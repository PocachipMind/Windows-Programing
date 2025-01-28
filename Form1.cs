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

        CvCapture capture; // CVCapture는 카메라의 속성설정과 프레임을 불러올 수 있음
        IplImage src; // IplImage형식으로 프레임을 불러와 이미지 형식으로 저장합니다.

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                capture = CvCapture.FromCamera(CaptureDevice.DShow, 0);
                capture.SetCaptureProperty(CaptureProperty.FrameWidth, 640);
                capture.SetCaptureProperty(CaptureProperty.FrameHeight, 480);

            }
            catch
            {
                timer1.Enabled = false;
            }
         
        }

        private void timer1_Tick(object sender, EventArgs e) // 타이머가 작동될 때 마다 영상을 받아와 src에 저장한 후 픽쳐박스ilp에 출력  
        {
            src = capture.QueryFrame();
            pictureBoxIpl1.ImageIpl = src;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cv.ReleaseImage(src); // 헤더 데이터 해제
            if (src != null) { src.Dispose(); } // src데이터 해제
        }
    }
}

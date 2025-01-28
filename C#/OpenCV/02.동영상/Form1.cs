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
        int frame_count = 0;


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                capture = CvCapture.FromFile("../../Saigon.mp4");//디버그 폴더가 최초 경로이므로 상위 상위 폴더를 들어감

            }
            catch
            {
                timer1.Enabled = false;
            }
         
        }

        private void timer1_Tick(object sender, EventArgs e) // 타이머가 작동될 때 마다 영상을 받아와 src에 저장한 후 픽쳐박스ilp에 출력  
        {
            frame_count++;
            label1.Text = frame_count.ToString() + "/" + capture.FrameCount.ToString();
            // 동영상 출력이 끝났을때 src값이 비어있다면 타이머를 작동하지 않게하여 src값이 비어있지 않은 경우에만 출력하게 합니다.
            src = capture.QueryFrame();
            //if (src != null) // src가 null인지 확인하는 동작때문에 잠깐 끊김이 발생함. 이 현상을 방지하기 위해 전체 프레임 수를 확인한 뒤 현재 프레임 번호와 마지막 프레임 번호를 비교하여 마지막 프레임일 경우 다시 맨 처음부터 재생 시키게 합니다.
            if(frame_count != capture.FrameCount)
            {
                pictureBoxIpl1.ImageIpl = src;
            }
            else
            {
                frame_count = 0;
                // 동영상 재생이 끝날경우 마지막 프레임이 표시되는데 만약 빈 화면으로 하고싶다면 PictureBox IPL 이미지 값을 널값으로 바꿔주면 됩니다.
                pictureBoxIpl1.ImageIpl = null;
                timer1.Enabled=false;
                // 만약 동영상을 계속 재생시키고싶다면 타이머 종료를 시키지않고 파일 경로를 넣어주면 됩니다.
                // capture = CvCapture.FromFile("../../Saigon.mp4");
            }
        }
        
        // 타이머 인터벌이 33보다 작으면 더 빠르게 재생, 크면 느리게 재생

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {            
            Cv.ReleaseImage(src); // 헤더 데이터 해제
            if (src != null) { src.Dispose(); } // src데이터 해제
        }
    }
}

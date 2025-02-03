using System;
using System.Reflection.Emit;
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
            if (frame_count != capture.FrameCount)
            {
                pictureBoxIpl1.ImageIpl = src;
            }
            else
            {
                frame_count = 0;
                // 동영상 재생이 끝날경우 마지막 프레임이 표시되는데 만약 빈 화면으로 하고싶다면 PictureBox IPL 이미지 값을 널값으로 바꿔주면 됩니다.
                // pictureBoxIpl1.ImageIpl = null;
                // timer1.Enabled = false;
                // 만약 동영상을 계속 재생시키고싶다면 타이머 종료를 시키지않고 파일 경로를 넣어주면 됩니다.
                capture = CvCapture.FromFile("../../Saigon.mp4");
            }
        }

        // 타이머 인터벌이 33보다 작으면 더 빠르게 재생, 크면 느리게 재생

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cv.ReleaseImage(src); // 헤더 데이터 해제
            if (src != null) { src.Dispose(); } // src데이터 해제
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 단일 캡쳐
            // 상대 경로를 사용
            Cv.SaveImage("../../capture.jpg", src);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 다중 캡쳐
            // 단일 캡쳐의 코드와 동일하지만 현재 시간을 활용하기 위해 스트링형태의 save name 변수 생성
            string save_name = DateTime.Now.ToString("dd-hh시mm분ss초"); // 현재 시간을 String 형식으로 변환하고 오늘 일수부터 초단위까지 저장
            Cv.SaveImage("../../" + save_name + ".jpg", src);


        }

        // 비디오 저장 함수 적용
        // < 경로 및 파일 이름, 인코딩 방식, FPS, 저장 크기 >
        // 인코딩 방식은 XVID, FPS는 15 사용, 저장 크기는 현재 프레임의 크기와 동일해야 합니다.
        CvVideoWriter video = new CvVideoWriter("../../Record.avi", "XVID", 15, Cv.Size(1280, 720));
        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            label2.Text = "녹화 중"; // 타이머 켜졌을때 라벨 값
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            label2.Text = "미사용";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            video.WriteFrame(src);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string save_name = DateTime.Now.ToString("dd-hh시mm분ss초");
            video = new CvVideoWriter("../../" + save_name + ".avi", "XVID", 15, Cv.GetSize(src));
            timer2.Enabled = true;
            label2.Text = "녹화 중";
        }
    }

    // 단일로 캡쳐나 녹화할 경우 이름을 미리 설정하여 결과를 저장합니다.
    // 다중 녹화 또한 다중 캡쳐 방식과 동일하며 차이점으로는 비디오 변수를 프로그램 실행시 속성을 설정하는 것이 아니라 버튼을 클릭했을 때 속성을 설정합니다.
    // 이외에도 현재시간 대신에 배열을 사용하거나 타이머 대신에 반복문 등을 활용하여 결과를 저장할 수 있습니다.
}
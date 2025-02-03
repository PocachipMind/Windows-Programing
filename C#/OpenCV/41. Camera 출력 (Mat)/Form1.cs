using System;
using System.Windows.Forms;
using OpenCvSharp;

// Mat 형식 카메라 출력을 위해 네임스페이스 추가
using OpenCvSharp.CPlusPlus;
// 이 네임스페이스에 비디오 캡쳐 함수가 담겨 있습니다.

namespace OpenCV_Practice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        VideoCapture video;
        Mat frame = new Mat();

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 비디오 캡쳐 장치의 속성 설정 
                // 첫 번째 상수의 인수는 캡쳐 디바이스의 인덱스
                // 디바이스의 인덱스 의미와 동일합니다.
                video = new VideoCapture(0);
                // 프레임의 너비와 높이 설정
                video.FrameWidth = 640;
                video.FrameHeight = 480;

            }
            catch
            {
                // 비디오 캡쳐 장치가 인식되지 않았을 경우 오류가 발생함으로 타이머 실행 시키지 않게 하여 오류 방지
                timer1.Enabled = false;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 타이머가 작동할 때 마다 비디오를 읽어 프레임에 저장합니다.
            video.Read(frame); // 인수에 Mat 형식의 프레임 사용.

            // 이제 픽쳐박스 IPL에 이미지를 출력.
            // 형식 변환
            // 픽쳐박스 IPL이 아닌 C#에서 기본적으로 지원하는 픽쳐박스도 출력이 가능합니다.
            // C#에서 사용하는 PictureBox를 사용할 경우 toIPL이 아닌 toBitmap으로 변환해야 합니다.
            pictureBoxIpl1.ImageIpl = frame.ToIplImage();

            // 이제 폼 이벤트에서 폼 클로징 이벤트를 활성화시켜 폼이 닫힐 때 메모리를 해제합니다.


            Window win = new Window("Frame", frame);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 프레임 메모리 제거
            frame.Dispose();
        }

        // 이 방법은 3점대 버전에서 카메라를 출력하는 방법과 동일합니다.
        // Mat 형식을 사용하는 경우 이 방식을 사용해 카메라를 사용하며 Mat 형식만 지원되는 함수가 존재합니다. 
        // 또한 OpenCv 3.x 버전을 사용하는 경우에도 이 방식을 사용합니다.
        // 또한 Mat 형식의 이미지를 윈도우 창으로 표시할 수 있습니다.
        // 단 CvWindow가 아닌 윈도우 형식으로 출력합니다.
        // 인수의 순서 및 윈도우 창 함수는 모두 동일하며 형식만 다릅니다.
        // 타이머 창의 새로운 윈도우로도 프레임을 표시해보도록 하겠습니다.
    }
        
    // 함수의 형태나 형식이 달라졌다고 해서 사용하는 방법까지 달라지지는 않습니다.
    // 앞서 배웠던 방법들에도 모두 똑같은 인수가 들어가 어렵지 않게 사용하실 수 있습니다.
}
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
        CvWindow win; // win 필드 선언

        private void Form1_Load(object sender, EventArgs e)
        {
            src = new IplImage("../../dandelion.jpg");

            // 클래스이름, 지역변수 이름, 생성자, 클래스 이름
            OpenCV_Class Convert = new OpenCV_Class();

            pictureBoxIpl1.ImageIpl = src;
            //pictureBoxIpl2.ImageIpl = Convert.BinarizerMethod_Hist(src);

            // 다시 폼이 로드되었을 때 win 필드의 속성을 설정합니다.
            win = new CvWindow("Binary", WindowMode.StretchImage, src);

            // 윈도우 창에 트랙바를 부착하도록 하겠습니다.
            // win.CreateTrackbar를 사용, < 트랙바의 이름, 트랙바 조절기의 초기 위치, 트랙바 조절기의 최대값, 트랙바 이벤트>
            win.CreateTrackbar("Threshold", 127, 255, TrackbarEvent); // 이름은 Threshold, 초기위치 127, 최대값은 255
            // 여기서 트랙바 조절계의 초기 위치를 127로 두었다 해서 바로 127의 값이 적용되지 않습니다.
            // 그러므로 초기 값을 바로 적용하기 위해서 트랙바 이벤트를 127의 값으로 적용하여 이미지를 갱신합니다.
            TrackbarEvent(127);
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cv.ReleaseImage(src); // 헤더 데이터 해제
            if (src != null) { src.Dispose(); } // src데이터 해제
        }

        // 마우스 콜백 함수와 동일하게 트랙바에 사용할 이벤트 생성
        // 매개변수로 int 형식의 pos 추가. 해당 매개변수는 트랙바 조절기의 조정값을 의미
        private void TrackbarEvent(int pos)
        {
            // src 값 설정하고 OpenCV 클래스 생성
            src = new IplImage("../../dandelion.jpg");
            OpenCV_Class Convert = new OpenCV_Class();

            // win 필드에 표시할 이미지를 이진화 메소드로 사용하고 임계값을 포스 변수로 할당합니다.
            // 트랙바 이벤트가 실행될 때마다 포스의 임계값으로 이진화메소드가 적용되어 윈도우 창에 표시됩니다.
            win.ShowImage(Convert.Binary(src, pos));
            // 이제 다시 폼이 로드되었을 때 win 필드의 속성을 설정합니다. ( 폼 로드 함수 내부로 )
        }


        // 트랙바를 사용하여 상수 값에 대한 함수 변환을 빠르게 확인할 수 있으며 사용자가 편리하게 범위 내의 값을 간단하게 조절할 수 있습니다.
        // 꼭 상수가 아니더라도 트랙바의 이벤트 포스의 값에 따라 조건문을 추가하여 상수 이외의 스트링이나 IPL 이미지 등의 상수가 아닌 다른 변수에 대한 값으로 적용이 가능합니다.
        // 이외에도 마우스 이벤트와 같이 사용하여 마우스가 클릭된 부분의 데이터를 기반으로 트랩가 조절기 값의 변화에 따른 작업을 진행하는 용도로도 응용이 가능합니다.
        // 또한 메소드나 함수의 상수, 변수 선택 시 정확도 높은 값을 선택할 수 있습니다.
    }
}
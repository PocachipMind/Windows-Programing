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

        // 마우스 콜백 함수를 사용해보도록 하겠습니다.
        CvWindow win; // win 필드 선언

        private void Form1_Load(object sender, EventArgs e)
        {
            src = new IplImage("../../dandelion.jpg");

            // 클래스이름, 지역변수 이름, 생성자, 클래스 이름
            OpenCV_Class Convert = new OpenCV_Class();

            pictureBoxIpl1.ImageIpl = src;
            //pictureBoxIpl2.ImageIpl = Convert.BinarizerMethod_Hist(src);

            // 폼이 로드되었을 때 src의 이미지를 Win 필드 창에 담습니다.
            win = new CvWindow("OpenCV", src);

            // 마우스 콜백을 적용해보도록 하겠습니다. += 을 통해 설정합니다. +=가 아닐시 오류가 발생합니다.
            win.OnMouseCallback += new CvMouseCallback(click);


        }

        // 마우스 콜백 이벤트를 사용할 때 기존의 함수를 할당하는 방식과 약간 다르므로 혼동이 올 수 있습니다.
        // 마우스 콜백 함수에는 += 기호를 사용하며 마우스 플래그 이벤트를 사용할 땐 비트 연산자를 사용한다는 것을 기억하시면 쉽게 사용할 수 있습니다.


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cv.ReleaseImage(src); // 헤더 데이터 해제
            if (src != null) { src.Dispose(); } // src데이터 해제
        }

        // 이벤트 추가. 사용할 매개변수는 마우스가 어떤 이벤트를 실행하는지를 저장할 eve와 마우스의 xy 좌표값, 마우스가 어떤 플래그를 실행시키는지 저장할 플래그를 사용합니다. 
        // 여기서 마우스 이벤트와 마우스 플래그의 선언 형식은 동일합니다.
        private void click(MouseEvent eve, int x, int y, MouseEvent flag)
        {
            // 마우스의 이벤트 중 마우스의 왼쪽 버튼을 누를 때 그때의 마우스 좌표를 윈도우 창에 표시해보기
            // if 문을 이용하여 이벤트가 마우스 왼쪽 버튼을 눌렀을 때 실행되게 합니다.
            if (eve == MouseEvent.LButtonDown)
            {
                // text 변수를 생성하여 현재 X의 좌표와 Y의 좌표를 문자열 형식으로 저장합니다.
                string text = "X : " + x.ToString() + "Y : " + y.ToString();
                // PutText 함수를 사용하여 문자를 표시해보도록 하겠습니다.
                // 표시되는 위치는 마우스 클릭 위치와 동일하게 사용하도록 하겠습니다.
                Cv.PutText(src, text, new CvPoint(x, y), new CvFont(FontFace.HersheyComplex, 0.5, 0.5), CvColor.Red);

                // 이후 클릭을 통하여 src 이미지에 문자를 그렸으므로 윈도우 창에 다시 표시해야 합니다.
                // 이전 강좌에서 배운 show 이미지를 통하여 윈도우 창에 표시되는 이미지를 src로 갱신시킵니다.
                win.ShowImage(src);
            }

            // 이제 마우스의 오른쪽 버튼을 클릭하면서 컨트롤 키를 누르고 있을 때 원을 그려봅니다.
            // 동일하게 if 문을 사용하여 eve 변수가 마우스 오른쪽 버튼을 누를 때 작동하게 합니다.
            // 또한 엔드 연산자인 &을 사용하여 플래그 키에 대한 조건을 추가합니다. 여기서 마우스의 이벤트와 선언하는 방식이 다르므로 주의하시길 바랍니다.
            // 괄호를 연 다음 현재 입력된 플래그 매개변수와 마우스 이벤트 플래그를 비트 연산 and 사용하여 참값인지를 판단합니다.
            // 입력된 플래그가 컨트롤키라면 마우스 이벤트의 플래그와 동일한지 확인하는 조건식입니다.
            // 이 값이 0 이 아닐 때, 즉 이 값이 거짓이 아닐 때 플래그가 발생합니다.
            if (eve == MouseEvent.RButtonDown && (flag & MouseEvent.FlagCtrlKey) != 0 )
            {
                // drawCircle 함수를 사용하여 이벤트가 발생했을 때의 마우스 좌표로 원을 그려봅니다.
                Cv.DrawCircle(src, x, y, 15, CvColor.GreenYellow);
                win.ShowImage(src); // show image를 통해 src 이미지 갱신
            }
        }
    }
}
using System;
using System.Windows.Forms;
using OpenCvSharp;

// 배경 삭제 알고리즘은 맷 형식을 기반으로 하여 C++ 네임스페이스가 추가되어야 합니다.
using OpenCvSharp.CPlusPlus;
// 배경 삭제 알고리즘 또한 이 네임 스페이스에 담겨 있습니다.

namespace OpenCV_Practice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackgroundSubtractor();
        }

        private void BackgroundSubtractor()
        {
            // 따로 메모리를 해제하는 구문인 Dispose를 사용하지 않기 위해 모두 using문을 이용하여 묶도록 하겠습니다.
            // VideoCapture를 사용하여 동영상을 불러옵니다.
            // 생성자의 속성에 경로를 입력합니다.
            using (VideoCapture video = new VideoCapture("../../Salad.mp4"))
            // 다음으로 Mat 형식 동영상의 프레임 변수와 결과를 출력할 remove 변수를 생성합니다.
            using (Mat frame = new Mat())
            using (Mat remove = new Mat())
            // 배경삭제 알고리즘인 MOG,MOG2,GMG 를 생성하겠습니다. 속성은 설정하지 않아도됩니다.
            using ( BackgroundSubtractorMOG MOG = new BackgroundSubtractorMOG())
            using (BackgroundSubtractorMOG2 MOG2 = new BackgroundSubtractorMOG2())
            using (BackgroundSubtractorGMG GMG = new BackgroundSubtractorGMG())
            // 이제 폼창이 아닌 윈도우 창에 3가지 이미지를 출력하기 위해서 이전 강좌에서 배운 Mat 형식용 윈도우 창을 선언합니다. 모두 윈도우 모드를 스트레치 이미지로 사용하고 초기 이미지는 null값을 사용하도록 하겠습니다.
            using (Window win_MOG = new Window("MOG", WindowMode.StretchImage,null))
            using (Window win_MOG2 = new Window("MOG2", WindowMode.StretchImage, null))
            using (Window win_GMG = new Window("GMG", WindowMode.StretchImage, null))
            {
                // 초기설정이 끝났으므로 중괄호를 열어 코드 작성.
                // 먼저 윈도우 창의 크기 변경
                win_MOG.Resize(640, 480);
                win_MOG2.Resize(640, 480);
                win_GMG.Resize(640, 480);

                // while 문을 이용해 키 입력이 있을 때 까지 출력하도록 합니다.
                while (Cv.WaitKey(1) < 0) // Cv.WaitKey의 인수는 마이크로초를 의미합니다. 1의 인수인 경우 1 마이크로 초마다 와일문이 반복됩니다. 이 값이 0보다 작을 경우에는 키가 입력되었으므로 와일문이 종료됩니다.
                {
                    // 비디오에서 프레임 읽기
                    video.Read(frame);

                    try
                    {
                        // 배경삭제 알고리즘 적용
                        MOG.Run(frame, remove); // < 원본이미지, 결과 이미지 >
                        Cv2.BitwiseAnd(frame, remove.CvtColor(ColorConversion.GrayToBgr), remove); // 비트연산 인수의 순서는 < 첫번째 이미지, 두번째 이미지, 결과 > 첫번째 이미지와 두번째 이미지를 and 연산후 결과에 저장합니다.
                            // 여기서 프레임은 다채널 이미지이며, remove는 단일 채널 이미지이기 때문에 이미지를 연산할 수 없습니다. 색상을 표시할 예정이므로 remove 단일 채널을 다채널로 변경해야합니다.
                            // 그레이스케일을 bgr속성으로 변경하여 적용하도록 하겠습니다. 첫번째 인수에는 프레임, 두번째 인수에는 remove 이미지를 BGR로 변경하기위해 CvtColor를 적용합니다. 이후 remove 변수에 다시 저장하도록 하겠습니다.
                            // 비트연산은 다음 강좌에서 자세히 알아보도록 하겠습니다.
                        win_MOG.ShowImage(remove); // 결과 표시

                        MOG2.Run(frame, remove);
                        Cv2.BitwiseAnd(frame, remove.CvtColor(ColorConversion.GrayToBgr), remove);
                        win_MOG2.ShowImage(remove);

                        GMG.Run(frame, remove);
                        Cv2.BitwiseAnd(frame, remove.CvtColor(ColorConversion.GrayToBgr), remove);
                        win_GMG.ShowImage(remove);

                    }
                    catch
                    {
                        // 비디오가 모두 재생되었을 때 프레임이 존재하지 않아 오류가 발생합니다.
                        // 동영상을 반복 재생하는 코드를 작성하지 않았으므로 프레임이 존재하지 않을 때 반복을 종료하도록 하겠습니다.
                        break;
                    }
                }


            }
        }

        // MOG 알고리즘에는 첫번째 프레임이 잔상이 남으며 MOG2 알고리즘은 흑백이 아닌 회색도 포함되어 있다는 것을 알 수 있습니다. 이 회색은 그림자를 의미합니다.
        // GMG 알고리즘은 배경 계산을 위해 몇몇 프레임이 지난 후에 출력되는 것을 알 수 있습니다.
        // PPT에서는 지금 확인한 결과와 다르게 색상으로 출력되었다는 것을 알 수 있습니다.
        // 색상으로 표시하는 방법은 비트 연산을 이용하여 검출 결과와 원본을 and 연산하여 표시할 수 있습니다.
        // 비트연산을 적용해보겠습니다.

        // MOG와 GMG 알고리즘은 검은색과 하얀색만 구성되어 있어 색상이 깨짐없이 출력되는 것을 알 수 있습니다.
        // 하지만 MOG2 알고리즘은 회색빛이 존재하여 일부 색상이 깨지는 것을 알 수 있습니다.
        // 이 회색빛은 그림자를 의미하며 이 회색 색상을 이용하여 더 정확도 높고 효율적인 알고리즘으로 구성할 수 있습니다.
        // MOG 알고리즘은 초기 배경이 정지되어 있다면 정확도 높은 검출 결과를 얻어 낼 수 있으며
        // GMG 알고리즘의 경우에는 오브젝트의 움직임이 활발하더라도 초기의 배경을 계산함으로 오브젝트만 정확히 검출할 수 있습니다.
        // 각각의 알고리즘마다 장단점이 있으므로 사용할 장소의 전경의 배경을 고려하여 적절한 알고리즘을 사용하면 정확도 높은 프로젝트를 구성할 수 있습니다.


        
    }

}
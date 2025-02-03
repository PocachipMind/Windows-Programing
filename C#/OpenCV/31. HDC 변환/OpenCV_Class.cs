using System;
using OpenCvSharp;

// 형식변환을 이용하기 위해선 기본적으로 포함되어야하는 참조가 있습니다.
// 1강에서 OpenCV를 설치할 때 사용하였던 참조관리자를 통해 DLL하나를 추가해야합니다.
// PresentationCore DLL 추가 : 해당 DLL은 Bitmap 소스를 사용할 수 있게 해줌. 또한 HDC 변환을 할수 있게해줌.

// HDC 변환을 사용하기 위해서는 총 3가지의 네임스페이스를 추가해야합니다.
// 비트맵과 그래픽스 사용하기 위해
using System.Drawing;
// 픽셀 포맷을 사용할 수 있도록
using System.Drawing.Imaging;
// 비트맵으로 변환하기 위해서
using OpenCvSharp.Extensions;

// HDC 변환을 하기 위해선 DLL 1개와 네임스페이스 3개가 추가되어 있어야합니다.


namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        IplImage hdcgrahics;

        public IplImage DrawToHdc(IplImage src)
        {
            hdcgrahics = new IplImage(src.Size, BitDepth.U8, 3);

            // 비트맵 변수를 생성하고 크기를 설정하며 픽셀 포맷을 설정.
            // 픽셀 포맷은 색상 데이터 형식을 설정합니다. 8비트 RGB값으로 변경하도록 하겠습니다. 이 값은 HDC 그래픽스 필드의 속성의 설정과 동일한 의미를 가집니다.
            Bitmap bitmap = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppRgb);

            // 비트맵의 이미지에 그래픽스 만들기
            Graphics grp = Graphics.FromImage(bitmap);

            // 그래픽스와 관련된 장치 컨텍스트에 대한 핸들을 가져옵니다.
            IntPtr hdc = grp.GetHdc();

            // 비트맵 컨버터를 이용하여 hdc를 적용.
            // DrawToHdc를 이용하며 인수 순서는 < 원본, hdc, 반환할 원본 이미지의 크기 >
            // 크기는 CvRect 형식을 사용하므로 시작위치는 0,150 이며 크기는 너비, 높이 -150 사용
            BitmapConverter.DrawToHdc(src, hdc, new CvRect(new CvPoint(0, 150), new CvSize(src.Width, src.Height - 150)));

            // 그래픽스에서 장치 컨텍스트를 해제. 장치 컨텍스트 해제를 하지 않을 경우 드로잉 할 수 없음.
            grp.ReleaseHdc(hdc);

            // 그래픽스에 문자열 그리기. C# 그래픽스와 동일한 사용 방법
            // 글꼴은 맑은 고딕, 크기는 72포인트, 브러시는 붉은색, (5,5)위치에 그리도록 하겠습니다.
            grp.DrawString("안녕하세요.", new Font("맑은 고딕", 72), Brushes.Red, 5, 5);

            // HDC 그래픽스 필드에 비트맵을 적용하도록 하겠습니다.
            hdcgrahics.CopyFrom(bitmap); // 비트맵에 적용되어 있는 값을 그대로 HDC 그래픽스 필드에 복사하게 됩니다.

            return hdcgrahics;

        }


        // 이번 강좌는 그래픽스 장치 컨택스트를 가져와 IPL 형식의 이미지에 적용하는 방법을 배웠습니다.
        // 그래픽스를 적용하는 방법으로는 이전 강좌에서 배운 형식 변환을 통하여 비트맵으로 변환 후 그래픽스를 적용하고 다시 IPL 이미지로 변환하여 사용하셔도 됩니다.
        // HDC는 현재 이미지에서 어떤 영역이 허가된 영역인가를 계산하여 해당 영역에만 출력이 가능하게 합니다.
        // 따로 영역을 설정하기 때문에 관심 영역을 할당하여 적용할 수 있으며 컨택스트를 가져와 그래픽스를 적용할 수 있습니다.
        // 현재 코드와 같이 적용할 경우 HDC 그래픽스 필드는 원본 이미지에 할당된 크기와 동일한 크기를 유지하며 그래픽스를 적용합니다.
        // 장치 컨택스트를 가져와 적용하는 방법과 단순하게 비트맵 형식으로 변환하여 그래픽스를 적용하는 방법 두가지 모두 이해하고 활용할 수 있어야 합니다.


        public void Dispose()
        {
            if (hdcgrahics != null) Cv.ReleaseImage(hdcgrahics);
        }
    }
}


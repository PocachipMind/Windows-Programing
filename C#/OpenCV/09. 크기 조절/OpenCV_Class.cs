using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage resize;

        public IplImage ResizeImage(IplImage src)
        {
            resize = new IplImage(new CvSize(src.Width / 4, src.Height - 1200), BitDepth.U8, 3);
            // Cv resize 함수를 이용하여 이미지 크기를 조절합니다.
            // 자동적으로 resize 필드의 크기로 이미지가 변경됩니다.
            // 매개변수 순서는 원본 결과 보간법
            // 보간법 - 수치해석등에 사용되는 이웃 보간법, 쌍선형 보간법, 바이쿠빅 보간법, 랭크조스 보간법 등을 사용합니다.
            // 크기를 변환할 경우 존재하지 않던 공간에 새로운 픽셀들이 생기거나 변환됩니다.
            // 픽셀들은 모두 2차원 벡터 형식 xy 좌표로 표현할 수 있으며 픽셀들의 값을 채우거나 변경하기 위하여 수치 해석 기법을 사용합니다.
            // 이번 경우 가장 보편적으로 사용되는 쌍선형 보간을 사용하도록 하겠습니다.
            Cv.Resize(src, resize, Interpolation.Linear);
            return resize;
        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (resize != null) Cv.ReleaseImage(resize);
        }
    }
}

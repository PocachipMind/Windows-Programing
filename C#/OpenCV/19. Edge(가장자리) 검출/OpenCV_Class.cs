using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        // 06 이진화 코드에서 추가적으로 수정합니다.
        IplImage canny;
        IplImage sobel;
        IplImage laplace;

        public IplImage CannyEdge(IplImage src)
        {

            canny = new IplImage(src.Size, BitDepth.U8, 1);

            // 캐니 엣지 적용 < 원본, 결과, 최소값, 최대값, 커널 > 커널크기 3x3으로 적용해봄
            Cv.Canny(src, canny, 100, 255, ApertureSize.Size3);
            return canny;
        }

        public IplImage SobelEdge(IplImage src)
        {
            sobel = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, sobel, ColorConversion.BgrToGray);
            // < 원본, 결과, x방향미분, y방향미분, 커널의 크기 >
            Cv.Sobel(sobel, sobel, 1, 1, ApertureSize.Size3);
            return sobel;
        }

        public IplImage LaplaceEdge(IplImage src)
        {
            laplace = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, laplace, ColorConversion.BgrToGray);
            // < 원본, 결과, 커널의 크기 >
            Cv.Laplace(laplace, laplace, ApertureSize.Size3);
            return laplace;
        }

        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문
        {
            if (canny != null) Cv.ReleaseImage(canny);
            if (sobel != null) Cv.ReleaseImage(sobel);
            if (laplace != null) Cv.ReleaseImage(laplace);

        }
    }
}

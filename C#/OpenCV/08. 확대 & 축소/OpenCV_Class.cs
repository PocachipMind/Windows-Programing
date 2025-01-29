using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage zoomin;
        IplImage zoomout;


        public IplImage ZoomIn(IplImage src)
        {
            // 피라미드 업의 경우 이미지를 두배 키웁니다
            zoomin = new IplImage(new CvSize(src.Width * 2, src.Height * 2), BitDepth.U8, 3);

            // 피라미드 업 < 원본, 결과, 필터 >, 원본 이미지를 불러와 계산하여 2배의 크기를 지니는 결과에 저장합니다.
            Cv.PyrUp(src, zoomin, CvFilter.Gaussian5x5);
            // 필터는 컨볼루션 필터를 의미하는데, 오로지 하나의 방법만 존재합니다.
            return zoomin;
        }

        public IplImage ZoomOut(IplImage src)
        {
            zoomout = new IplImage(new CvSize(src.Width / 2, src.Height / 2), BitDepth.U8, 3);

            // 피라미드 다운 < 원본, 결과, 필터 >.
            Cv.PyrDown(src, zoomout, CvFilter.Gaussian5x5);
            // 필터는 컨볼루션 필터를 의미하는데, 오로지 하나의 방법만 존재합니다.
            return zoomout;
        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (zoomin != null) Cv.ReleaseImage(zoomin);
            if (zoomout != null) Cv.ReleaseImage(zoomout);
        }
    }
}

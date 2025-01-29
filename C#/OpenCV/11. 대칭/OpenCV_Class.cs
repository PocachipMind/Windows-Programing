using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage symm;

        public IplImage Symmetry(IplImage src)
        {
            // 대칭이므로 이미지 크기는 그대로, 색상 존재하므로 채널은 3
            symm = new IplImage(src.Size, BitDepth.U8, 3);

            // 원본, 결과, 대칭방법
            Cv.Flip(src, symm, FlipMode.X); // 상하 대칭 ( X축 대칭 )
            return symm;
        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (symm != null) Cv.ReleaseImage(symm);
        }
    }
}

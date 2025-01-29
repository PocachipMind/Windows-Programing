using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        // 06 이진화 코드에서 추가적으로 수정합니다.
        IplImage bin;
        IplImage morp;

        public IplImage Binary(IplImage src, int threshold)
        {
            // 이진화는 흑백 색상만 존재해서 채널 1
            bin = new IplImage(src.Size, BitDepth.U8, 1);

            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);

            Cv.Threshold(bin, bin, threshold, 255, ThresholdType.Binary);

            return bin;
        }

        
        public IplImage MorphologyImage(IplImage src)
        {

            morp = new IplImage(src.Size, BitDepth.U8, 1);
            bin = this.Binary(src, 50);

            // 모폴로지 연산은 컨볼루션 커널이 있어야함
            IplConvKernel element = new IplConvKernel(3, 3, 1, 1, ElementShape.Rect); // < 행, 열, x앵커, y앵커, 구조 >

            // 모폴로지 연산 < 원본, 결과, 임시, 컨볼루션 커널, 연산 방법, 반복 횟수 > 임시 = 잠시동안 결과를 저장할 공간. 잠깐 동안 위치를 바꿔즈는 역할이기 때문에 크기가 동일한 변수를 사용하면 됩니다.
            Cv.MorphologyEx(bin, morp, bin, element, MorphologyOperation.Gradient, 10);

            return morp;
        }


        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (morp != null) Cv.ReleaseImage(morp);
        }
    }
}

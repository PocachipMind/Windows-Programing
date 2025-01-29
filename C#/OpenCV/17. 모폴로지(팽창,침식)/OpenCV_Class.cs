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

        // 팽창 메소드
        public IplImage DilateImage(IplImage src)
        {
            // 단색이므로 채널 1
            morp = new IplImage(src.Size, BitDepth.U8, 1);

            bin = this.Binary(src, 50); // bin 필드의 속성을 따로 설정하지 않아도 됩니다. 
            // 이진화 필드에서 최종적으로 반환되는 형식으로 현재 bin필드에 저장됩니다.
            // 또한 bin 필드는 메소드 구문을 나가면 모두 초기화 되므로 다른 메소드에서 불러와 사용해도 누적되지 않습니다.
            // 즉 딜레이트 이미지 메소드 안에 bin필드의 속성은 바이너리 메소드에서 리턴되는 속성과 동일하게 적용됩니다.

            // 팽창 < 원본, 결과, 컨볼루션 커널, 반복 횟수 > 커널이 null일경우 3x3 사각형 구조로 변환되어 입력
            Cv.Dilate(bin, morp, null, 10);
            
            return morp;
        }

        // 침식 메소드
        public IplImage ErodeImage(IplImage src)
        {

            morp = new IplImage(src.Size, BitDepth.U8, 1);
            bin = this.Binary(src, 50);

            // 컨볼루션 커널 생성
            IplConvKernel element = new IplConvKernel(3, 3, 1, 1, ElementShape.Rect); // < 행, 열, x앵커, y앵커, 구조 >

            // 침식 < 원본, 결과, 컨볼루션 커널, 반복 횟수 >
            Cv.Erode(bin, morp, element, 10);

            return morp;
        }

        // 팽창 후 침식
        public IplImage DEImage(IplImage src)
        {

            morp = new IplImage(src.Size, BitDepth.U8, 1);
            bin = this.Binary(src, 50);

            Cv.Dilate(bin, morp, null, 10);
            Cv.Erode(morp, morp, null, 10);

            return morp;
        }

        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (morp != null) Cv.ReleaseImage(morp);
        }
    }
}

using System;
using OpenCvSharp;
using OpenCvSharp.Extensions;

// Mat 형식으로 비트연산을 사용해보겠습니다.
// Mat 형식은 C++ 네임스페이스에 담겨있으므로 추가하도록 하겠습니다.
using OpenCvSharp.CPlusPlus;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        IplImage bin;

        public IplImage Binary(IplImage src, int threshold)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, threshold, 255, ThresholdType.Binary);

            return bin;
        }

        public IplImage BitwiseMat(IplImage src)
        {
            // 총 3개의 변수를 선언합니다.
            Mat Input1 = new Mat(src); // src를 담아 MAT형식으로 변환
            Mat Input2 = new Mat(this.Binary(src, 150)); // 이진화로 변화하여 MAT형식으로 변환
            Mat bitwise = new Mat(); // 결과 저장할 변수

            // 이전 강좌에서 사용했듯이 cv2.bitwise이며 인수순서는 < 계산이미지, 결과이미지 >
            // 원본 이미지를 반전시키도록 하겠습니다.
            Cv2.BitwiseNot(Input1, bitwise);

            // 윈도우 창을 이용하여 결과 출력
            Window win_Not = new Window("BitwiseNot", WindowMode.StretchImage, bitwise);

            // and 연산
            // 이전 강좌에서도 단일 채널 이미지를 다채널 이미지로 변경하였으므로 이번에도 CvtColor를 이용하여 BGR 속성으로 변경하여 적용합니다.
            // < 계산 이미지1, 계산 이미지2, 결과 이미지 >
            Cv2.BitwiseAnd(Input1, Input2.CvtColor(ColorConversion.GrayToBgr), bitwise);
            Window win_And = new Window("BitwiseAnd", WindowMode.StretchImage, bitwise);

            Cv2.BitwiseOr(Input1, Input2.CvtColor(ColorConversion.GrayToBgr), bitwise);
            Window win_Or = new Window("BitwiseOr", WindowMode.StretchImage, bitwise);

            Cv2.BitwiseXor(Input1, Input2.CvtColor(ColorConversion.GrayToBgr), bitwise);
            Window win_Xor = new Window("BitwiseXor", WindowMode.StretchImage, bitwise);

            // 이진화 처리된 이미지를 IPL 형식으로 반환하여 출력
            return Input2.ToIplImage();
        }

        // 이번 강좌에서는 Mat 형식으로 변환하고 cv가아닌 cv2 형식을 사용하여 비트 연산을 적용해 보았습니다.
        // Mat 형식과 Cv2는 OpenCV 3점대 버전에서 사용하는 방식이며 2점대 버전에서도 사용할 수 있습니다.
        // 코드의 순서는 모두 동일하고 그대로 3점대 버전에서도 코드의 변환 없이 적용 할 수 있습니다.
        // 비트연산은 CV 형식도 존재하며 비트 연산을 적용하기 위해서 IPL 이미지를 일부러 Mat 형식으로 변환해서 적용하지 않아도 됩니다.
        // Cv.Not(); Cv.And(); Cv.Or(); Cv.Xor(); 로 사용할 수 있습니다.


        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
        }
    }
}

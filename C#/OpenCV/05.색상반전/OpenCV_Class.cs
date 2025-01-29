using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage gray;
        IplImage inversion;

        // 그레이 스케일 메소드
        public IplImage GrayScale(IplImage src)
        {
            // 그레이라는 필드는 어떤 값을 지니는지 생성하지 않았으므로 생성자를 이용하여 설정 
            // 중요한 의미를 가지는 구문 ! 중요
            gray = new IplImage(src.Size, BitDepth.U8, 1);

            // cvt 컬러 메소드를 불러와 흑백 이미지로 변경
            // 매개변수의 순서는 원본, 결과, 변환타입. 원본에서 입력된 이미지를 가지고 계산을 진행하게 됩니다.
            // 이후 결과 이미지에 계산 결과를 저장하게 됩니다.
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            // 변환 타입은 어떻게 변환할지를 의미합니다. 그레이스케일로 변환할 예정이므로 블루,그린,레드에서 그레이로 변경합니다.

            //결과로 저장된 그레이 필드를 반환
            return gray;
        }

        public IplImage InversionImage(IplImage src)
        {
            // 색상이 존자해는 이미지를 색상을 반전시키기 때문에 채널은 3
            inversion = new IplImage(src.Size, BitDepth.U8, 3);

            // 원본의 src 결과의 inverted 필드 입력하고 inverted을 반환
            Cv.Not(src, inversion);
            return inversion;
        }

        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문을 추가
        {
            // 이미지를 흑백 색상으로 바꾸는 Grayscale메소드를 생성
            // 만약 gray라는 필드가 비어있는 값이 아니라면 이미지의 메모리를 해제한다
            if(gray != null) Cv.ReleaseImage(gray);
            if(inversion != null) Cv.ReleaseImage(inversion);
        }
    }
}

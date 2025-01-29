using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage gray;
        IplImage inversion;
        IplImage bin;

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

        public IplImage Binary(IplImage src)
        {
            // 이진화는 흑백 색상만 존재해서 채널 1
            bin = new IplImage(src.Size, BitDepth.U8, 1);

            // src의 색상은 다색임으로 그레이 스케일로 단색으로 변환합니다.
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);

            // CV Threshold 함수를 이용하여 임계점과 최대값, 임계값 종류를 설정합니다.
            // 여기서 원본과 결과를 같은 인수로 사용하게 되는데 이는 빈을 사용하고 계산후에 빈을 그대로 덧씌우는 방법입니다.
            Cv.Threshold(bin, bin, 150, 255, ThresholdType.Binary);
            // 그레이스케일을 변환할때 원본의 src, 결과의 bin을 사용하였는데 src에는 영향을 미치지 않습니다. 즉 결과만 바뀌게 됩니다.
            // 지금은 원본과 결과를 같은 값을 두어 bin 자체가 새롭게 변화하게 됩니다.
            // 임계점은 임계점 기준보다 이하면 0의 값으로 바꾸고 임계점 기준보다 이상이면 최대값으로 바뀌게 됩니다.
            // 0은 흑색, 255는 백색
            // 임계점은 150, 최대값은 255를 사용합니다
            // 즉 픽셀의 값이 150보다 이하면 흑색으로 바꾸고 150보다 이상이면 백색으로 바뀌게 됩니다.
            // 이진법처럼 0과 1의 값만 가지듯이 이진화 메소드는 0과 최대값만 가지는 픽셀만 남게 됩니다.

            return bin;
        }

        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문을 추가
        {
            // 이미지를 흑백 색상으로 바꾸는 Grayscale메소드를 생성
            // 만약 gray라는 필드가 비어있는 값이 아니라면 이미지의 메모리를 해제한다
            if(gray != null) Cv.ReleaseImage(gray);
            if(inversion != null) Cv.ReleaseImage(inversion);
            if(bin != null) Cv.ReleaseImage(bin);
        }
    }
}

using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage hsv;

        // HSV를 각각 나누어 하나의 채널에 대해서만 범위를 지정. 
        public IplImage HSV(IplImage src)
        {
            hsv = new IplImage(src.Size, BitDepth.U8, 3);

            IplImage h = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage s = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage v = new IplImage(src.Size, BitDepth.U8, 1);

            // BGR을 HSV로 변환 < 원본, 결과, 변환방법 >
            Cv.CvtColor(src, hsv, ColorConversion.BgrToHsv);
            // 나눌 이미지, 채널1, 채널2, 채널3, 채널4. 현재 이미지에 3개의 채널만 존재하므로 마지막 알파의 값은 null
            Cv.Split(hsv, h, s, v, null);

            // 현재 hsv필드는 원본 이미지가 hsv로 변환된 이미지가 저장되어 있으며 h,s,v 변수에는 hsv의 이미지의 각각 채널이 저장되어 있습니다.

            // hsv필드를 이용하여 채널을 나누었으므로 hsv필드를 초기화시켜 비어있는 이미지로 변경
            hsv.SetZero(); // hsv필드를 결과값으로 사용하기 위하여
            // 카피를 사용하여 마스크를 적용해 표현할 예정.

            // 카피는 이미지를 덧씌우는 형식이기에 필드에 이미지가 저장되어 있다면 올바르지 않게 표시됩니다.
            // 이것을 방지하기 위해 hsv 필드를 비운것.



            //// Hue
            //// 채널의 최소치와 최대치를 설정 < 원본, 최소, 최대, 결과 >
            //// 노란색만 출력해보기 위해서 최소를 20, 최대를 30으로 설정. 원본의 결과를 덧씌우기 때문에 원본과 결과의 변수가 동일
            //Cv.InRangeS(h, 20, 30, h);
            //Cv.Copy(src, hsv, h); // 마스크 적용

            //// Saturation
            //Cv.InRangeS(s, 20, 30, s);
            //Cv.Copy(src, hsv, s);

            //// Value
            //Cv.InRangeS(v, 20, 30, v);
            //Cv.Copy(src, hsv, v);

            // Red검출하기
            IplImage lower_red = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage upper_red = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage red = new IplImage(src.Size, BitDepth.U8, 1);

            Cv.InRangeS(h, 0, 10, lower_red);
            Cv.InRangeS(h, 170, 179, upper_red);
            // 서로 다른 범위를 가지는 채널을 합침 < 첫번째 이미지, a , 두번째이미지, b, 감마, 결과 > a = 첫 이미지 가중치, b = 두번째 이미지 가중치, 감마 = 각 합계에 더해질 값
            Cv.AddWeighted(lower_red, 1.0, upper_red, 1.0, 0.0, red);

            // hsv 필드에 결과 저장. 여기서 마스크는 레드를 사용
            Cv.Copy(src, hsv, red);

            return hsv;

        }

        // 3가지 범위를 한번에 지정하여 세밀히 지정. 한번에 3개의 범위를 다루어 빨간색만 출력해보기
        public IplImage HSV2(IplImage src)
        {
            hsv = new IplImage(src.Size, BitDepth.U8, 3);

            // Red검출하기
            IplImage lower_red = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage upper_red = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage red = new IplImage(src.Size, BitDepth.U8, 1);

            Cv.CvtColor(src, hsv, ColorConversion.BgrToHsv);

            // 전 함수는 1개의 채널만 사용하여 상수 1개의 값만 입력해도 되었지만 이번에는 3개의 채널을 사용해야하므로 new CvScalar를 사용
            Cv.InRangeS(hsv, new CvScalar(0, 100, 100), new CvScalar(10, 255, 255), lower_red); // lower_red에는 0,100,100이 최소값 10,255,255 가 최대값
            // CvScalar의 인수는 각각 h,s,v에 대한 값
            Cv.InRangeS(hsv, new CvScalar(170, 100, 100), new CvScalar(179, 255, 255), upper_red);

            Cv.AddWeighted(lower_red, 1.0, upper_red, 1.0, 0.0, red);

            hsv.SetZero();
            Cv.Copy(src, hsv, red);

            return hsv;

        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (hsv != null) Cv.ReleaseImage(hsv);
        }
    }
}

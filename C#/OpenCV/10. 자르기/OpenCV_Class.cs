using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage slice;

        public IplImage SliceImage(IplImage src)
        {
            // 이미지 350x150크기로 잘라내기
            slice = new IplImage(new CvSize(350,150), BitDepth.U8, 3);
            // 관심영역을 설정하는 방법은 크게 3가지 있습니다.
            // 모두 영역을 설정하는데 사용되며 가장 편한 방법 한가지만 숙지하셔도 앞으로 사용하는데 큰 문제가 없습니다.

            // 1번 방법 : 로이
            // 바꿀이미지.ROI = 사각형 크기 설정
            src.ROI = new CvRect(750, 840, slice.Width, slice.Height);

            // 2번 방법 : 셋 로이
            // 바꿀이미지.SetROI( 사각형 크기 설정 )
            src.SetROI(new CvRect(750, 840, slice.Width, slice.Height));

            // 3번 방법 : 셋 이미지 로이
            // CV.SetImage(바꿀이미지 , 사각형 크기 설정 )
            Cv.SetImageROI(src, new CvRect( 750, 840, slice.Width, slice.Height));

            // 이미지에서 사각형의 크기만큼 관심 영역을 설정하게 됩니다.
            // New CvRect를 이용하여 사각형을 생성하게 됩니다.
            // 매개변수의 순서는 사각형 좌측상단 모서리의 X 좌표, Y좌표, 사각형의 너비, 높이 
            // 좌표는 왼측 상단이 0,0 이며 우측 하단이 Max,Max 입니다.
            // 수학에서 사용하는 XY와 방향이 다르니 주의



            //이미지에 적용 방법 3가지

            // 1번. 카피: 원본 이미지를 결과 이미지에 그대로 복사
            Cv.Copy(src,slice);

            // 2번 리사이즈 : 이미지 변형
            Cv.Resize(src, slice);

            // 3번 클론 : 카피처럼 그대로 복사하지만 차이점 존재. 카피는 여러분이 속성을 설정해놓은 방법을 따라간다.
            // 만약 채널을 1로 설정하였는데 색상이 있는 이미지를 복사한다면 오류가 발생합니다. 하지만 클론은 속성까지 복사해옵니다.
            // 만약 채널을 1로 설정하였는데 색상이 있는 이미지를 복제한다면 채널 설정까지 복제해와 따라갑니다. 즉 오류가 발생하지 않고 스스로 설정해놓은 속성을 변경시킵니다.
            // 즉 카피는 이미지를 복사할 때 형식이 일치해야 복사가 되며 클론은 그대로 모든 것을 복제해옴으로 형식이 일치하지 않아도 됩니다.
            // 카피는 마스크를 씌워 약간의 변형을 줄 수 있지만 클론은 그대로 복제해 옵니다.
            slice = src.Clone();




            // 관심 영역 해제 2가지 방법

            // 1번 이미지점리셋로이
            src.ResetROI();

            // 2번 cv 리셋 이미지 로이
            Cv.ResetImageROI(src);

            // src에서 관심 영역을 설정하였기 때문에 그대로 src에 관심 영역을 해제합니다.

            return slice;

        }

        public IplImage SliceImage_2(IplImage src)
        {
            // 클론 속성덕분에 채널이 달라도 잘 동작
            slice = new IplImage(new CvSize(350, 150), BitDepth.U8, 1);

            src.ROI = new CvRect(750, 840, slice.Width, slice.Height);

            slice = src.Clone();

            src.ResetROI();

            return slice;
        }

         public IplImage SliceImage_3(IplImage src)
        {
            // 관심 영역을 사용하지 않고 이미지를 잘라보기.

            slice = new IplImage(new CvSize(350, 150), BitDepth.U8, 1);

            // 관심영역 구문없이 클론의 속성 설정에 사각형 크기를 설정할 수 있습니다.
            // 사각형 설정을 변수로 생성하여 볼러올 수 있으며 값을 유동적으로 변경 시켜줄 수 있습니다.
            CvRect rect = new CvRect(750, 840, slice.Width, slice.Height);
            slice = src.Clone(rect);

            return slice;


        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (slice != null) Cv.ReleaseImage(slice);
        }
    }
}

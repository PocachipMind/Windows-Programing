using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage blur;
        
        public IplImage BlurImage(IplImage src)
        {
            // 색상 이미지를 흐림효과 처리할 예정이므로 채널을 3으로
            blur = new IplImage(src.Size, BitDepth.U8, 3);

            // 블러처리. 매개변수 - 원본, 결과, 블러처리방법, 파라미터1, 파라미터2, 파라미터3, 파라미터 4 ( 파라미터 3,4는 가우시안블러 처리하는데 사용 )
            Cv.Smooth(src, blur, SmoothType.Blur, 9);
            // 파라미터를 위와 같이 1개만 입력할 경우 자동적으로 파라미터2의 값은 1과 동일한 값으로 생각합니다. 즉 여기에선 파라미터2는 9로 자동 입력
            // 파라미터는 가능하면 홀수만을 사용해야합니다. n곱하기n 크기의 사각형 중심부의 픽셀을 재조정하게 되는데 짝수일 경우 중심부의 픽셀을 선택할 수 없습니다. 
            // 가우시안 블러나 중간값 블러를 선택하였을 경우 아예 실행조차 할 수가 없습니다.

            return blur;
        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (blur != null) Cv.ReleaseImage(blur);
        }
    }
}

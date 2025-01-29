using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage draw;

        public IplImage DrawingImage()
        {
            draw = new IplImage(new CvSize(640, 480), BitDepth.U8, 3);

            // 필드 선언하고 속성을 설정한 후 클론이나 카피 등을 이용해 이미지를 포함시키지 않으면 초기상태가 검은 이미지입니다.
            // 이 검은 이미지 필드에 드로잉 진행



            // 1. 선 그리기 < 이미지, 시작점, 종료점, 색상, 두께 >
            Cv.DrawLine(draw, new CvPoint(100, 100), new CvPoint(500, 100), CvColor.Blue, 20);
            // 인트형을 사용하여 그리기도 가능, 색상도 RGB 가능
            Cv.DrawLine(draw, 100, 100, 500, 100, new CvColor(0, 0, 255), 20);
            // Cv.Line() >> 동일한 동작 인수도 동일함


            // 2. 원 그리기 < 이미지, 중심점, 반지름, 색상, 두께 >
            Cv.DrawCircle(draw, new CvPoint(200, 200), 50, CvColor.Red, -1);
            // -1을 두께로 할경우 내부가 채워짐, int형으로 좌표 설정가능


            // 3. 사각형 그리기 < 이미지, 좌 상단 모서리점, 우측하단 모서리점, 색상, 두께 >
            Cv.DrawRect(draw, new CvPoint(300, 150), new CvPoint(500, 300), CvColor.Green, 10);
            // -1을 두께로 할경우 내부가 채워짐, int형으로 좌표 설정가능

            // 4. 호 그리기 < 이미지, 중심점, 크기, 기준각도, 시작각도, 종료각도, 색상, 두께 >
            Cv.DrawEllipse(draw, new CvPoint(150, 400), new CvSize(100, 50), 0, 90, 360, CvColor.Yellow, 5);
            // 중심점은 Cv 포인트이며 크기는 Cv 사이즈입니다.
            // 각도의 종류가 3가지나 되는데 이 각도를 선정하는 부분이 호를 그리는데 있어서 가장 중요한 요소입니다.
            // 호는 인수가 복잡하여 사용하기 까다롭습니다.

            // 5. 텍스트 그리기 < 이미지, 문자, 위치, 폰트 정보, 색상 > 
            Cv.PutText(draw, "Open CV", new CvPoint(400, 400), new CvFont(FontFace.HersheyComplex, 1, 1), CvColor.White);
            // 텍스트는 산세리프 글꼴만 지원하여 영문만 사용 가능. 만약 특정 폰트 및 한글이나 한문등의 타국어를 사용해야한다면
            // HDC 변환을 통하여 사용할 수 있습니다.
            // 매개변수 위치의 경우 텍스트의 문자 열의 좌측 아래가 기준점.
            // 폰트 정보의 인수 순서는 글자 모양, 글자 높이, 글자 너비

            return draw;
            
        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (draw != null) Cv.ReleaseImage(draw);
        }
    }
}

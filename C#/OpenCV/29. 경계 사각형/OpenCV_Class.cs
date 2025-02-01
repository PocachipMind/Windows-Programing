using System;
using OpenCvSharp;


namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage bound;

        public IplImage BoundingRectangle()
        {
            bound = new IplImage(new CvSize(640, 480), BitDepth.U8, 3);

            // 난수를 통해 임의의점 생성
            // 무작위로 생성된 점들의 경계사각형을 만들어보도록 하겠습니다.

            // 임의의 점들의 생성 개수 선언
            int num = 7;

            // 난수 발생. 시간데이터를 이용하여 현재 시간으로 초기화해 무작위 난수를 생성.
            CvRNG rng = new CvRNG(DateTime.Now);
            CvPoint[] points = new CvPoint[num];

            // for문을 이용하여 포인트 배열의 임의의 점들을 생성해 포함시키도록 하겠습니다.
            for (int i = 0; i < num; i++)
            {
                // 소괄호를 닫은 이후 중괄호를 다시 열어 변수의 x점과 y점을 설정할 수 있습니다.
                points[i] = new CvPoint()
                {
                    // 좌표는 정수형이므로 인트형으로 변환시킨 뒤 랜덤한 값을 생성하고 너비만큼 나눈 나머지 값을 사용하게 하여 이미지의 너비를 넘어가지 않게 합니다.
                    // 콤마를 사용하셔야합니다. Y 좌표 또한 높이만큼 나눈 나머지 값을 사용하여 이미지의 높이를 넘어가지 않게 합니다.
                    X = (int)(rng.RandInt() % (bound.Width)),
                    Y = (int)(rng.RandInt() % (bound.Height))
                };
                // bound필드에 난수 지점 표시. 내부를 채우는 -1 말고 다른 방법은 Cv.FILLED
                Cv.DrawCircle(bound, points[i], 3, CvColor.Green, Cv.FILLED);
            }

            // 경계 사각형 함수 사용
            // 사각형에 대한 정보를 담습니다.
            CvRect rect = Cv.BoundingRect(points); // Cv.BoundingRect를 사용하며 CvPoint형식의 배열을 인수로 사용합니다.
            
            Cv.DrawRect(bound, rect, CvColor.Red, 2); // 그대로 Rect를 담거나 시작점과 도착점을 활용하여 그립니다. x 좌표, y좌표, 너비, 높이에 대한 값이 모두 Rect에 담겨있습니다.
            return bound;
        }


        // 앞서 배운 강좌들의 포인트들을 배열에 담아 간단하게 적용할 수 있습니다.
        // 난수 생성 부분 대신에 앞서 배운 강좌들의 검출 지점으로 대체하셔서 사용하시면 됩니다.
        // 이번 강좌에선 난수를 생성하고 응용하는 방법을 좀 더 중점적으로 다루었습니다.
        // 난수 생성은 검출된 결과를 테스트하거나 정확도를 검사하는 방법으로 응용할 수 있습니다.
        // 예를 들면 이미지에서 붉은색 오브젝트를 검출했다 가정하였을 경우 임의의 위치에서 붉은색이 맞는지 정확도를 검사하는 용도 등으로 활용할 수 있습니다.
        // 난수를 생성하고 적용하는 방법과 이번 메소드에서 난수 부분을 대체하여 이전에 배운 강좌들의 지점 검출법을 적용해 최소 경계 사각형을 만드는 방법 등으로 활용하시면 됩니다.

        public void Dispose()
        {
            if (bound != null) Cv.ReleaseImage(bound);
        }
    }
}


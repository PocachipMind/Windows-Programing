using System;
using System.Linq; 
using OpenCvSharp;

// 링큐 네임스페이스는 통합 데이터 질의 기능을 포함하고있습니다. 간단하게 배열에서 조금 더 편하게 원소를 찾을 수 있습니다.
// 예를들면 데이터 안에서 마지막 번째의 값이나 총 개수, 최대값, 최소값 등을 쉽게 검색할 수 있습니다.

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage gray;
        IplImage houcircle;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage HoughCircles(IplImage src)
        {
            houcircle = new IplImage(src.Size, BitDepth.U8, 3);
            // 카피를 통해 houcircle필드에 src복사
            Cv.Copy(src, houcircle);
            gray = this.GrayScale(src); // 그레이 스케일 적용
            // 가우시안 블러 적용. 원본과 결과 이미지에 동일하게 Gray Field를 사용하며 덮어 씌우도록 하겠습니다.
            // 파라미터는 9 사용
            Cv.Smooth(gray, gray, SmoothType.Gaussian, 9);

            // houcircle 함수 적용하기 앞서 메모리 저장소 생성
            CvMemStorage Storage = new CvMemStorage();

            // houcircle 또한 Seqence로 circleSegment 형식에 저장된 값으로 반환합니다.
            CvSeq<CvCircleSegment> circles = Cv.HoughCircles(gray, Storage, HoughCirclesMethod.Gradient, 1, 100, 150, 50, 0, 0); // < 계산이미지, 메모리 저장소, 계산방법, DP, 최소거리, 엣지 임계값, 중심 임계값, 최소 반지름, 최대 반지름 >
            // 최소거리는 100. 엣지 임계값은 150, 중심 임계값은 50, 반지름 고려없이 검출


            // foreach문을 통해 검출 원 확인
            foreach (CvCircleSegment item in circles) // 아임템값에는 중심에 대한 값과 반지름에 대한 값이 담겨있습니다.
            {
                // DrawCircle을 이용하여 중심점에 반지름만큼의 원을 그립니다. 여기서 반지름은 Float형식이므로 int로 형변환을 시켜줍니다.
                Cv.DrawCircle(houcircle, item.Center, (int)item.Radius, CvColor.Blue, 3);
            }

            return houcircle;
        }

        // 허프 서클 함수에서 계산 이미지를 최대한 깔끔하게 생성하는 것이 가장 중요하며, 허프 서클 함수에서 계산에 중요한 영향을 미치는 인수는 엣지 임계점과 중심 임계값입니다.
        // 허프 서클 함수 내에서 Canny Edge를 적용함으로 어떤 값을 주어야 할지 판단하는 것이 주된 요소입니다.
        // 또한 히스토그램에서도 임계값을 사용함으로 적절한 값을 주셔야 합니다.
        // 간단하게 엣지 임계값이 낮을수록 같은 위치에서 더 많은 원이 검출되고 중심 임계값이 낮을수록 다른 위치에서 더 많은 원이 검출된다 생각하시면 됩니다.
        // 이 함수가 이해가 되지 않는다면 캐니 엣지와 시퀀스를 다루는 부분을 복습하셔야합니다.

        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (houcircle != null) Cv.ReleaseImage(houcircle);
        }
    }
}


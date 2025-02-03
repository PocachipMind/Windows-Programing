using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        // 마스크를 적용하는 방법으로 이전 강좌에서 배운 마우스 콜백 함수를 이용하여 마스크를 생성하도록 하겠습니다.
        IplImage dist;

        public IplImage DistTransform(IplImage src)
        {
            // 거리 변환 함수는 F32비트 깊이와 채널 1을 사용합니다.
            dist = new IplImage(src.Size, BitDepth.F32, 1);
            
            // 이진화 적용을 위한 bin 변수
            IplImage bin = new IplImage(src.Size, BitDepth.U8, 1);

            // 이진화 적용을 위하여 그레이 스케일로 변환
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);

            // 이진화 적용. 현재 이미지는 배경이 하얀색이고 오브젝트가 검은색에 더 가까움으로 기존에 적용하던 Binary가 아닌 
            // Binary Inverse ( BinaryInv ) 를 적용하고 임계값은 230을 사용하도록 하겠습니다.
            Cv.Threshold(bin, bin, 230, 255, ThresholdType.BinaryInv);

            // 거리변환 함수 적용
            // Cv.DistTransform 를 사용하며 < 이진화 이미지, 결과, 거리 유형, 마스크 크기 >
            // 거리 유형은 유클리드 거리를 사용하고 마스크의 크기는 3으로 설정하도록 하겠습니다.
            Cv.DistTransform(bin, dist, DistanceType.L2, 3);

            // 거리 변환되어 바뀐 이미지에서 가장 밝은 곳을 찾아보도록 하겠습니다.
            CvPoint minval, maxval;

            // 결과 이미지에서 최대값을 찾기 위해서 Cv.MinMaxLoc 사용
            // < 계산 이미지, 최소 위치, 최대 위치 > 입니다.
            // 이 함수는 이미지에서 가장 낮은 값과 가장 높은 값을 가지는 지점을 반환합니다.
            Cv.MinMaxLoc(dist, out minval, out maxval);

            // 이제 가장 밝은 위치인 최대 위치에 검은색 원을 그려보도록 하겠습니다.
            Cv.DrawCircle(dist, maxval, 40, CvColor.Black, 10);

            return dist;
        }

        // 거리 변환이 적용되어 노이즈가 사라진 후 최대 위치인 손바닥 정 중앙에 원이 표시되는 것을 확인하였습니다.
        // 거리 변환 함수를 사용하여 불필요한 노이즈를 한번에 제거하여 오브젝트를 검출하기 쉽게 만들어줍니다.
        // 이 함수를 사용해 불필요한 함수를 사용하지 않게 됩니다.
        // 그 결과 연산 속도를 높이고 메모리 사용량을 크게 줄일 수 있습니다.
        // 하얀색 픽셀이 뭉쳐있을수록 그 지점이 점점 밝아지고 크기가 작은 하얀색 픽셀들은 사라져 검출 결과를 높일 수 있습니다.
        // 또한 하얀색 픽셀의 밝기로 오브젝트의 다양한 정보를 얻을 수 있습니다.
        

        public void Dispose()
        {
            if (dist != null) Cv.ReleaseImage(dist);
        }
    }
}

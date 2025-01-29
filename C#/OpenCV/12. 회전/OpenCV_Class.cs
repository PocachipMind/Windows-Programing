using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage rotate;

        public IplImage RotateImage(IplImage src, int angle)
        {
            rotate = new IplImage(src.Size, BitDepth.U8, 3);

            // 회전 행렬을 생성해줄 함수 선언 < 중심점, 각도, 스케일 > 스케일은 1:1이므로 1
            CvMat matrix = Cv.GetRotationMatrix2D(new CvPoint2D32f(src.Width / 2, src.Height / 2), angle, 1);


            // 회전은 기하학에서 사용되는 Affine 변환을 적용하여 변환시킬 수 있습니다.
            // 아핀 변환은 평행이동, 반사, 회전 등을 직선을 직선으로 대응시키는 곳에 모두 적용할 수 있습니다.
            // 픽셀은 2차원 벡터 형식으로 나타낼 수 있으니 대부분의 함수는 수학적인 요소를 포함하고있습니다.
            // < 원본, 결과, 행렬, 보간법, 여백 색상 > 행렬은 당연하게 회전을 적용하기 위해 포함하며 보간은 45도 회전 시켰을때 일부분이 표시되지 않았던 부분을 해결하기 위해 보간을 진행합니다. 쌍선형 보간을 적용하도록 작성합니다.
            Cv.WarpAffine(src, rotate, matrix, Interpolation.Linear, CvScalar.ScalarAll(0));
            return rotate;

        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (rotate != null) Cv.ReleaseImage(rotate);
        }
    }
}

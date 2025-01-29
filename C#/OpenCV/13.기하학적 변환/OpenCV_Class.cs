using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage affine;

        public IplImage AffineImage(IplImage src)
        {
            affine = new IplImage(src.Size, BitDepth.U8, 3);

            // 회전을 적용하기위해 2x3 행렬 필요함.
            // 회전 행렬을 생성해준 함수와 동일하게 아핀변환 매트릭스 생성 함수가 존재.
            // 매트릭스를 생성하기 전에 우리는 3개의 점을 매핑하기 위해서 6개의 점을 필요로한다는 사실을 알고있습니다.
            // 원본 이미지에서 점 3개와 결과 이미지에서 점 3개를 선택하기 위해서 배열을 생성하도록 하겠습니다.
            // 먼저 원본에서 점을 선택하기 위해서 src 포인트 배열을 생성하며 결과에서 점을 매핑하기 위해서 dst 포인트 배열을 생성합니다.

            // 각각 3개의 점을 사용하므로 배열의 크기는 둘 다 3으로 설정합니다.
            CvPoint2D32f[] srcPoint = new CvPoint2D32f[3];
            CvPoint2D32f[] dstPoint = new CvPoint2D32f[3];

            // 첫번째 점 설정 ( 좌상점 ) float 형식이므로 소수점을 포함하여 형식을 맞춰줍니다.
            srcPoint[0] = new CvPoint2D32f(100.0, 100.0);
            // 두번째 점 (우상점)
            srcPoint[1] = new CvPoint2D32f(src.Width - 100.0, 100.0);
            // 세번째 점 (좌하점)
            srcPoint[2] = new CvPoint2D32f(100.0, src.Height - 100.0);

            dstPoint[0] = new CvPoint2D32f(300.0, 100.0);
            dstPoint[1] = new CvPoint2D32f(src.Width - 100.0, 100.0);
            dstPoint[2] = new CvPoint2D32f(100.0, src.Height - 100.0);


            // 아핀 변환을 위해 매트릭스 생성
            CvMat matrix = Cv.GetAffineTransform(srcPoint, dstPoint);
            Console.WriteLine(matrix);

            // < 원본, 결과, 매트릭스, 보간법, 여백색상 >
            Cv.WarpAffine(src, affine, matrix, Interpolation.Linear, CvScalar.ScalarAll(0));

            return affine;

        }

        //메모리 해지 구문
        public void Dispose()
        {
            if (affine != null) Cv.ReleaseImage(affine);
        }
    }
}

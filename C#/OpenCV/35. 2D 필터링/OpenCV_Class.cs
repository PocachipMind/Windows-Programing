using System;
using OpenCvSharp;
using System.Collections.Generic;
// 이 네임스페이스는 목록, 배열, 테이블, 사전 등에 대한 개체의 제너릭 콜렉션을 정의하는 함수가 포함되어 있습니다.
// 이 네임스페이스에 포함되어 있는 Key Value Pair 함수를 사용하여 블롭에서 키 값을 호출해 Value 값을 불러와 적용합니다.

using OpenCvSharp.Blob;
// 해당 네임스페이스에 블롭 함수 포함되어있음.


namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        IplImage filter;

        public IplImage Filter2D(IplImage src)
        {
            filter = new IplImage(src.Size, BitDepth.U8, 3);

            // float 형식의 배열 생성
            float[] data = {    1,  4,  7,  4,  1,
                                4, 16,  26, 16, 4,
                                7, 26,  41, 26, 7,
                                4, 16,  26, 16, 4,
                                1,  4,  7,  4,  1 };

            // 매트릭스 형식의 커널을 생성하고 열의 5, 행의 5를 입력한 다음 매트릭스 타입을 8비트 1 채널 형식으로 사용하도록 하겠습니다.
            // 변환할 배열을 데이터로 사용합니다. 이 함수를 통하여 데이터의 배열이 5x5 매트릭스 형식으로 변환됩니다.
            CvMat kernel = new CvMat(5, 5, MatrixType.F32C1, data);

            // 이제 노멀라이즈 함수를 사용하여 정규화하도록 합니다.
            // 원본의 커널, 결과의 커널을 사용하여 덮어 씌우며 최대값을 1, 최소값을 0으로 사용합니다.
            // 노름 타입 L1을 사용하며 배열의 모든 원소의 합을 기준으로 정규화합니다.
            Cv.Normalize(kernel, kernel, 1.0, 0.0, NormType.L1);

            // 2D 필터 적용 < 원본, 결과, 커널 >
            // 필터2d 함수는 사용자가 정의한 마스크를 가지고 영상이나 이미지를 컨볼루션 할 수 있습니다.
            Cv.Filter2D(src, filter, kernel);

            return filter;
        }

        // 필터가 가우시안 마스크이므로 가우시안 마스크가 적용
        // Filter2D 함수를 사용하여 이미지를 컨볼루션 할 수 있습니다.
        // 컨볼루션이란 앞서 코드를 통해 값을 변경하였듯 가중치를 갖는 마스크를 이용해 영상에서 마스크를 씌운 후 입력 영상의 픽셀 값과 마스크의 
        // 가중치에 대한 값을 곱하여 픽셀을 재정의합니다.ppt를 통하여 정규화하는 방법과 동일한 의미를 갖습니다.
        // 이미지 전체에 대해 처리하는 방식은 이전 강좌들에서 픽셀들을 처리하는 방법과 동일합니다.
        // 이미지 좌측에서부터 우측으로 이동해가면서 커널을 모두 적용합니다.
        // 이번 강좌에서는 임의의 마스크를 사용하여 정규화한 뒤에 적용하는 방법을 배웠습니다.
        // 정규화하는 방법은 수학적인 작업을 최소화 할 수 있습니다.
        // 가우시안 마스크 이외에도 다른 마스크를 적용하거나 정규화하는 방법을 이해하신다면 손쉽게 사용하실 수 있는 함수입니다.

        public void Dispose()
        {
            if (filter != null) Cv.ReleaseImage(filter);
        }
    }
}


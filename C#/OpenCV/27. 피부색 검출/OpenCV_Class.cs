using System;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

// 피부색 검출 메소드는 OpenCV Sharp.C++ 에 담겨있습니다.

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage skin;

        public IplImage SkinDetection(IplImage src)
        {
            skin = new IplImage(src.Size, BitDepth.U8, 3);
            // 계산 이미지로 사용할 변수 생성
            IplImage output = new IplImage(src.Size, BitDepth.U8, 1); // 계산 이미지는 흑백이므로 채널 1 사용

            // skin 필드에 원본 복사
            Cv.Copy(src, skin);

            // 피부색 검출 메소드 사용
            // 이 변수의 속성에서 첫 번째 인수는 1로 고정적으로 사용하며, 모폴로지 연산을 자체적으로 적용할 수 있습니다.
            CvAdaptiveSkinDetector detector = new CvAdaptiveSkinDetector(1,MorphingMethod.ErodeDilate); // 침식후 팽창 적용
            // 피부색이 검출된 이미지 생성 < 원본, 결과 >
            detector.Process(src, output);

            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    // if문을 이용하여 검출 이미지인 output 변수에서 해당 위치의 픽셀값을 확인해보도록 합니다.
                    if (output[y, x].Val0 != 0) // output[] y좌표, x 좌표 순서입니다. yx 순서에 유의하십길 바랍니다. Val0는 첫 번째 엘리먼트 값을 의미합니다. 즉 현재 이미지의 픽셀을 의미합니다.
                    {                           // 현재 픽셀의 값이 0이 아니고 어떤 색상도 포함되어 있지 않다면 실행합니다.

                        // 결과 이미지인 skin 필드의 픽셀값을 초록색으로 변경하겠습니다.
                        skin[y,x] = CvColor.Green;
                    }
                }
            }

            return skin;

        }


        // 피부색 검출 메소드를 사용하지 않고 임의 범위의 픽셀들을 조작해보도록 합니다.
        public IplImage SkinDetection2(IplImage src)
        {
            skin = new IplImage(src.Size, BitDepth.U8, 3);

            IplImage output = new IplImage(src.Size, BitDepth.U8, 1); // 계산 이미지는 흑백이므로 채널 1 사용
            Cv.Copy(src, skin);


            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    // 해당 위치의 색상을 Color 변수에 담습니다.
                    CvColor Color = skin[y, x];

                    // if문을 이용하여 해당 위치의 레드 속성이 100보다 낮은 값을 지닐 경우 값을 변경하도록 하겠습니다.
                    // 조건에 부합하는 경우 해당 픽셀의 값을 빨강 0, 녹색 255, 파랑 0의 값을 지니는 픽셀로 변경하도록 하겠습니다.
                    if (Color.R < 100)
                    {
                        // 즉 모든 이미지의 픽셀에서 RGB 속성 중 레드의 속성이 100보다 낮은 값을 가지는 픽셀의 경우 255의 녹색 값을 가지는 픽셀로 변경됩니다.
                        skin[y, x] = new CvColor(0, 255, 0);
                    }
                }
            }

            return skin;

        }

        // 이번 강좌에서는 피부색 검출 메소드보다 이중 포문을 이용하여 픽셀의 값을 변경하는 방법을 숙지하시는 것이 가장 중요합니다.
        // HSV나 이중포문을 이용하여 더 좋은 피부색 검출 메소드를 생성할 수 있습니다.
        // 만약 피부색을 검출해야하는 알고리즘이 필요하다면 HSV와 이중포문 RGB값을 사용하여 더 정확도 높은 알고리즘을 제작할 수 있습니다.


        public void Dispose()
        {
            if (skin != null) Cv.ReleaseImage(skin);
        }
    }
}


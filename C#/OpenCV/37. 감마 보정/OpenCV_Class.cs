using System;
using OpenCvSharp;


namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        IplImage gamma;

        public IplImage GammaCorrect(IplImage src)
        {
            gamma = new IplImage(src.Size, BitDepth.U8, 3);

            // Gamma 값을 선언하고 값을 할당
            double gamma_value = 0.5;
            
            // byte 형식의 배열 LUT를 생성하고 256의 크기를 할당합니다.
            // 256크기 이외의 값을 사용할 경우 오류가 발생합니다.
            byte[] lut = new byte[256];

            for (int i = 0; i<lut.Length; i++)
            {
                // lut 배열의 i번째 값에 Gamma 보정 공식을 적용해 보도록 하겠습니다.
                lut[i] = (byte)(Math.Pow(i / 255.0, 1.0 / gamma_value) * 255.0);
            }

            // LUT 함수 적용
            // Cv.LUT 사용, < 원본, 결과, LUT 배열 >
            Cv.LUT(src, gamma, lut);

            return gamma;

        }

        // LUT는 배열 색인화 과정으로 대체하는데 사용됩니다.
        // 감마 보정을 사용하여 이미지가 흐리게 보이거나 어둡게 보이는 경우 이 이미지를 보정하여 선명하게 만들 수 있습니다.
        // 또한 의도적인 효과를 적용하여 검출용 이미지를 생성하기 위한 기초 단계로 사용할 수 있습니다.

        public void Dispose()
        {
            if (gamma != null) Cv.ReleaseImage(gamma);
        }
    }
}


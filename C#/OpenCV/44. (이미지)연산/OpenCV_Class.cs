using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        IplImage symm;
        IplImage calc;

        public IplImage Symmetry(IplImage src)
        {
            symm = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.Flip(src, symm, FlipMode.Y);
            return symm;
        }

        public void Calculate(IplImage src)
        {
            calc = new IplImage(src.Size, BitDepth.U8, 3);
            IplImage src_symm = this.Symmetry(src); // 두번째 이미지를 원본 이미지에서 대칭된 이미지로 사용.

            // 덧셈 연산 < 첫번째 이미지, 두번째 이미지, 결과 >
            Cv.Add(src, src_symm, calc);
            CvWindow window_add = new CvWindow("Add", WindowMode.StretchImage, calc);

            // 뺄셈과 나눗셈 연산은 인수의 위치에 따라 이미지의 결과가 달라지므로 순서에 신경쓰셔야합니다.
            Cv.Sub(src, src_symm, calc);
            CvWindow window_sub = new CvWindow("Sub", WindowMode.StretchImage, calc);

            Cv.Mul(src, src_symm, calc);
            CvWindow window_mul = new CvWindow("Mul", WindowMode.StretchImage, calc);

            Cv.Div(src, src_symm, calc);
            CvWindow window_div = new CvWindow("Div", WindowMode.StretchImage, calc);

            Cv.Max(src, src_symm, calc);
            CvWindow window_max = new CvWindow("Max", WindowMode.StretchImage, calc);

            Cv.Min(src, src_symm, calc);
            CvWindow window_min = new CvWindow("Min", WindowMode.StretchImage, calc);

            Cv.AbsDiff(src, src_symm, calc);
            CvWindow window_absdiff = new CvWindow("AbsDiff", WindowMode.StretchImage, calc);

            Cv.WaitKey(0); // Cv.WaitKey 중괄호를 열어 키가 눌릴때 모든 윈도우창을 닫아보도록 하겠습니다. 
            {
                CvWindow.DestroyAllWindows();
            }
        
        }

        // 덧셈의 경우 서로의 이미지에서 픽셀을 더해 밝아집니다.
        // 뺄셈의 경우 서로의 이미지에서 픽셀을 빼 어두워집니다.
        // 곱셈의 경우 이미지가 극단적으로 밝아지며 
        // 나눗셈의 경우 이미지가 극단적으로 어두워집니다.
        // 최대값과 최소값의 경우 더 높거나 낮은 값을 선택하여 적절하게 밝거나 어두워집니다.
        // 절대값 차이의 경우 음수가 양수로 변경되어 색상이 매우 밝거나 매우 어두워집니다.

        // 이미지 연산을 통하여 실제 이미지를 적용할 경우 어떻게 변하는지 확인하였습니다.
        // 하지만 두번째 이미지를 단순히 대칭된 원본 이미지를 사용하여 표시하였기 때문에 좋은 예가 아닙니다.
        // 이미지 연산을 유용하게 사용하기 위해선 ppt에서 확인하였듯이 원본 이미지와 크기가 동일한 단색 이미지를 마스크로 사용하는 방법입니다.
        // 회색의 색상을 가지는 이미지를 두 번째 인수로 사용하여 약간 더 밝거나 어둡게 하거나 일정값 이상이나 이하를 회색으로 바꾸는 등 검출하기 쉬운 상태로 변경할 수 있습니다.
        // 또한 이미지로 표시된 타이포그래피 등을 원본 이미지 위에 덧셈이나 최대값 연산 등을 활용하여 합성할 수 있습니다.



        public void Dispose()
        {
            if (symm != null) Cv.ReleaseImage(symm);
            if (calc != null) Cv.ReleaseImage(calc);
        }
    }
}

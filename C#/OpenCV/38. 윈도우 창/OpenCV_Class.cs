using System;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        IplImage gray;
        IplImage bina;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage BinarizerMethod(IplImage src)
        {
            bina = new IplImage(src.Size, BitDepth.U8, 1);
            gray = this.GrayScale(src);
            Binarizer.Bernsen(gray, bina, 51, 60, 150); 

            return bina;

        }

        public IplImage BinarizerMethod_Hist(IplImage src)
        {
            // 이전 이진화 메소드 강좌에서 사용했었던 임의의 이진화 메소드에 윈도우 창을 적용해보도록 하겠습니다.
            // 피스 배열에 담겨있는 이미지는 총 20개가 담겨있습니다.
            // 이 이미지를 일일이 리턴 구문에 반환하여 확인하기에는 매우 비효율적입니다.
            // 또한 코드의 중간중간 흐름마다 계속 리턴 구문으로 대체하여 일일이 확인하는 것도 비효율적입니다.
            // 이를 효율적으로 확인하기 위해서 윈도우창 함수를 사용하여 일부분을 확인해 보도록 하겠습니다.

            bina = new IplImage(src.Size, BitDepth.U8, 1);
            gray = this.GrayScale(src);


            int area = 200;
            int num = 0;

            int row = (src.Width % area == 0) ? (int)(src.Width / area) : (int)(src.Width / area + 1);
            int col = (src.Height % area == 0) ? (int)(src.Height / area) : (int)(src.Height / area + 1);
            int count = row * col;

            
            float[] data = new float[count];
            IplImage[] piece = new IplImage[count]; 
            CvRect[] piece_roi = new CvRect[count];

            for (int x = 0; x < src.Width; x = x + area)
            {
                for (int y = 0; y < src.Height; y = y + area)
                {
                    
                    CvRect roi = new CvRect
                    {
                        X = x,
                        Y = y,
                        Width = area,
                        Height = area
                    };

                    if (roi.X + roi.Width > src.Width) roi.Width = area - ((roi.X + roi.Width) - src.Width);
                    if (roi.Y + roi.Height > src.Height) roi.Height = area - ((roi.Y + roi.Height) - src.Height);

                    gray.SetROI(roi);
                    piece[num] = new IplImage(gray.ROI.Size, BitDepth.U8, 1);
                    Cv.Copy(gray, piece[num]);
                    gray.ResetROI();

                    // 히스토그램 계산 //
                    int[] size = { area };
                    CvHistogram hist = new CvHistogram(size, HistogramFormat.Array); 
                    Cv.CalcHist(piece[num], hist); 

                    float minValue, maxValue;
                    hist.GetMinMaxValue(out minValue, out maxValue); 


                    int highlevel = 0;
                    for (int i = 0; i < area; i++)
                    {
                        if (maxValue == hist.Bins[i].Val0) highlevel = i;
                    }

                    
                    piece_roi[num] = roi;
                    data[num] = highlevel;
                    num++; 
                }

            }
            
            CvMat kernel = new CvMat(row, col, MatrixType.F32C1, data);
            Cv.Normalize(kernel, kernel, 255, 0, NormType.C);


            for (int r = 0; r < count; r++)
            {
                
                Cv.Threshold(piece[r], piece[r], kernel[r], 255, ThresholdType.Otsu);

                Cv.SetImageROI(bina, piece_roi[r]);
                Cv.Copy(piece[r], bina);
                bina.ResetROI(); 
            }

            // 윈도우창 함수 사용. 
            // 제목은 window로 사용하고 크기 모드는 오토사이즈
            CvWindow win = new CvWindow("window", WindowMode.AutoSize, src);

            // 너무 고화질이여서 크기가 너무 크므로 크기 조절.
            // 변수명 점 resize를 통하여 윈도우 창의 너비와 높이를 설정할 수 있습니다.
            win.Resize(640, 480);
            // 사이즈는 변경되었지만 윈도우 창의 크기만 줄어들어서 다 안보임 

            // 모드를 스트레치이미지로 변경
            CvWindow win2 = new CvWindow("window2", WindowMode.StretchImage, src);
            win2.Resize(640, 480);
            // 적절한 사이즈에 맞추어져 출력 됨. 또한 마우스를 이용하여 창 크기를 조절할 수 있습니다.

            // 윈도우 창의 초기 위치 조정
            // 윈도우 창을 사용하다 보면 윈도우 창의 초기 위치가 화면의 끝 쪽에 위치하게 되어 이미지를 제대로 확인할 수 없을 뿐만 아니라 이미지의 크기가 작다면 드래그하여 움직일 수도 없습니다.
            // win.move를 이용하여 지정된 X,Y 좌표 위치에 윈도우 창을 출력할 수 있습니다.
            win2.Move(100, 0);

            // 이번엔 출력되는 이미지를 강제로 변경해보겠습니다.
            // win2 변수에 할당된 이미지를 변경합니다.
            win2.ShowImage(piece[0]);

            // win.close로 윈도우창을 닫을 수 있습니다.
            win2.Close();

            // 이외에도 변수를 일일이 선언하지 않고 new cv.Window만 사용하여 빠르게 확인할 수 있습니다.
            new CvWindow(piece[0]).Move(0, 0);
            new CvWindow(piece[1]).Move(0, 200);
            new CvWindow(piece[2]).Move(0, 400);

            // 이 윈도우 창은 크기 조절이 불가능합니다. 그러나 바로 뒤에 점 무브나 리사이징을 통하여 간단하게 조작이 가능합니다.

            // 윈도우 창을 적절히 사용하여 계산 과정 중 여러 계산 방식을 적용하여 한번에 간단하게 확인이 가능합니다.
            // 또한 계산 과정의 일부를 확인하기 위해서도 사용이 가능합니다.
            // 중간 과정이나 결과를 표시하는 창으로 사용한다면 프로젝트나 알고리즘을 구성하는데 소요시간을 단축할 수 있습니다.

            return bina;
        }



        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (bina != null) Cv.ReleaseImage(bina);
        }
    }
}

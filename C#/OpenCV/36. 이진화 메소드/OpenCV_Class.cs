using System;
using OpenCvSharp;

// 이진화 메소드 사용할 수 있도록 네임스페이스 추가
using OpenCvSharp.Extensions;
// 해당 네임스페이스에 이진화 메소드 함수가 포함되어 있습니다.


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

            // 번슨 이진화 메소드 적용 < 계산 이미지, 결과 이미지, 커널의 크기, 대비 최소값, 임계값 >
            // 커널의 크기는 51, 대비 최소값은 60, 임계값은 150을 사용
            Binarizer.Bernsen(gray, bina, 51, 60, 150); // 여기서 커널은 홀수만 가능합니다.

            return bina;

        }

        // 이진화 메소드 함수는 간단한 인수만을 이용하여 어렵지 않게 사용할 수 있습니다.
        // 번슨 이외에도 Niblack, Sauvola도 동일한 방법으로 사용할 수 있습니다.
        // 이진화 메소드 형식과 비슷하게 구현해본 임계값 메소드를 확인해봅니다.

        // 이전 강좌들에서 사용했었던 형식들과 함수들을 그대로 응용하여 구현
        // 간단하게 이미지를 에어리어의 크기를 가지는 조각으로 카운트의 개수만큼 나누게 됩니다.
        // 조각 이미지마다 히스토그램을 계산하여 가장 높을 때의 히스토 위치를 임계값으로 사용합니다. 즉 각각의 부분마다 서로 다른 이진화를 적용하는 방법입니다.
        public IplImage BinarizerMethod_Hist(IplImage src)
        {
            // bina, gray 결과용 이미지와 계산용 이미지를 구분하기 위해 사용.
            // 그레이 스케일로 변경하여 그레이 필드를 기반으로 계산 실시 
            bina = new IplImage(src.Size, BitDepth.U8, 1);
            gray = this.GrayScale(src);


            // area = 관심 영역 크기 num = count 변수 index 확인용
            // area의 숫자가 높을 수록 관심 영역의 간격이 커지게 되며 값이 작을 수록 더 세밀하게 나누게 됩니다.
            int area = 200;
            int num = 0;

            // row = 관심 영역의 행 col = 관심 영역의 열 count = 관심 영역의 갯수
            // 삼항 연산자를 사용해 행과 열의 개수 설정
            // 이미지 크기 너비가 1001로 가정할 경우 200의 크기로 나눈다면 200크기의 5개의 행과 1 크기의 1개의 행이 생성되어 총 6개의 행이 생성됩니다.
            // 이미지 크기 너비가 1000일 경우 200으로 나눈다면 나머지가 발생하지않아 총 5개의 행이 생성됩니다. 행이나 열 개수가 딱 나누어질 때를 대비하여 행과 열의 개수를 조정합니다.
            // count로 행과 열을 곱하여 관심영역으로 나눈 이미지가 총 몇 조각인지 확인할 수 있습니다.
            int row = (src.Width % area == 0) ? (int)(src.Width / area) : (int)(src.Width / area + 1);
            int col = (src.Height % area == 0) ? (int)(src.Height / area) : (int)(src.Height / area + 1);
            int count = row * col;

            // 관심 영역에 대한 정보 담기
            // data = 커널로 사용할 행렬 piece = 관심영역으로 설정된 이미지를 저장할 배열 piece_roi = 관심영역을 저장할 배열
            float[] data = new float[count]; // 커널로 사용할 행렬이며 정규화에 사용할 배열. 각각의 관심 영역에 따른 이진화 임계값을 적용할 때 사용.
            IplImage[] piece = new IplImage[count]; // 피스는 각각의 관심 영역의 이미지를 피스에 담아 각각의 이진화를 처리하기 위해 사용. 
            CvRect[] piece_roi = new CvRect[count];// 피스로이 배열에는 관심 영역의 크기를 담아 관심 영역으로 나누어진 피스들을 합쳐서 bina필드에 다시 하나의 이미지로 담기위해 사용합니다.

            // 피부색 검출 강좌에서 사용했었던 이중 포문을 이용하여 관심 영역을 영역의 크기만큼 나누기 위해 사용합니다.
            for (int x = 0; x < src.Width; x = x + area)
            {
                for (int y = 0; y < src.Height; y = y + area)
                {
                    // 이후 로이를 생성하여 X, Y로 부터 에어리어의 크기만큼 관심영역을 설정합니다.
                    CvRect roi = new CvRect
                    {
                        X = x,
                        Y = y,
                        Width = area,
                        Height = area
                    };

                    // if문을 사용하여 가장 모서리쪽에 있는 관심 영역들의 크기는 다를 수 있기 때문에 로이의 크기를 변환하여 오류가 발생하지 않도록 합니다.
                    if (roi.X + roi.Width > src.Width) roi.Width = area - ((roi.X + roi.Width) - src.Width);
                    if (roi.Y + roi.Height > src.Height) roi.Height = area - ((roi.Y + roi.Height) - src.Height);

                    // 이제 계산 이미지를 관심 영역으로 변경하여 IPL 이미지 배열인 피스에 담습니다.
                    // 여기서 piece[num]들의 속성이 설정되어 있지 않으므로 크기, 비트 깊이, 채널을 설정해야 합니다.
                    // 그 이후 관심영역을 해제합니다.
                    gray.SetROI(roi);
                    piece[num] = new IplImage(gray.ROI.Size, BitDepth.U8, 1);
                    Cv.Copy(gray, piece[num]);
                    gray.ResetROI();

                    // 히스토그램 계산 // > 히스토그램을 이용하여 임계값 설정해보도록 하겠습니다.
                    int[] size = { area }; // 인트배열의 사이즈는 히스토그램의 개수를 의미합니다. 꼭 area 만큼 입력하지 않아도 됩니다.
                    CvHistogram hist = new CvHistogram(size, HistogramFormat.Array); // 히스트 변수를 생성하여 area의 개수만큼 히스토그램을 계산합니다. // 히스토그램 포맷 인수는 area를 사용하며 다차원 배열을 의미합니다. 
                    Cv.CalcHist(piece[num], hist); // 해당 관심영역에 히스토그램을 계산합니다.

                    // 플롯 형식의 최소값 최대값을 선언하고 
                    float minValue, maxValue;
                    hist.GetMinMaxValue(out minValue, out maxValue); // 해당 관심 영역에서 히스토그램의 최소값과 최대값을 얻습니다.


                    // for문을 이용하여 area의 크기만큼 반복하며 해당 관심 영역의 모든 히스토그램을 검색하여 가장 높은 값을 가질 때의 히스토그램 위치를 하이레벨 변수에 담습니다.
                    int highlevel = 0;
                    for ( int i = 0; i <area; i++ )
                    {
                        if (maxValue == hist.Bins[i].Val0) highlevel = i;
                    }

                    // CvRect형식의 piece_roi 배열에 현재 설정된 관심 영역의 크기를 담고 
                    piece_roi[num] = roi;
                    data[num] = highlevel; // 데이터 배열에는 최대 값을 가질 때의 히스토그램의 위치를 담습니다.
                    num++; // 이후 카운트를 증가시키기 위해 num 값을 더합니다.
                }

            }
            // 이 이중 for문 안에서 카운트의 개수만큼 반복하게 되고 모든 배열 변수를 다 채우게 됩니다.


            // 이후 데이터를 행과 열의 개수만큼 커널을 생성하고 Normalize 함수를 통하여 매트릭스를 정규화 합니다.
            CvMat kernel = new CvMat(row, col, MatrixType.F32C1, data);
            Cv.Normalize(kernel, kernel, 255, 0, NormType.C); // 이진화의 최대 임계값은 255이며 최소값은 0을 가질 수 있기 때문에 그 값을 그대로 사용하고
            // 노름 타입을 C로 사용하여 매트릭스의 최대값 인수를 기준으로 정규화하게 합니다. 즉, 가장 높은 임계값을 가질 때는 255를 가지게 됩니다.


            // 이제 다시 for문을 이용하여 카운트의 개수만큼 반복하여 관심영역으로 나누어진 조각조각들에 이진화를 처리합니다.
            for ( int r = 0; r < count; r++)
            {
                // 이진화. piece의 r 번째 이미지의 커널에 저장된 임계값을 사용하며 이진화 형식은 Otsu를 사용하도록 합니다. 
                Cv.Threshold(piece[r], piece[r], kernel[r], 255, ThresholdType.Otsu);

                // 이후 bina 필드에서 piece_roi의 위치와 크기로 관심 영역을 설정하게 되고 그 부분에 피스를 복사합니다.
                Cv.SetImageROI(bina, piece_roi[r]);
                Cv.Copy(piece[r], bina);
                bina.ResetROI(); //다시 bina 필드의 ROI를 초기화하고 카운트의 개수만큼 반복하여 하나의 이미지로 이어 붙입니다.
            }

            return bina;
            // 각각 영역에서 히스토그램이 최대값을 가질 때의 위치를 기준으로 영역마다 이진화 처리한 것을 확인하였습니다.
            // 간단하게 이렇게 자신만의 이진화 메소드를 생성할 수 있습니다.
            // 이 방법은 이진화 메소드의 방법을 흉내낸 방식입니다.
            // 이진화 임계값을 정할 때 단순히 가장 높은 임계값의 위치를 기준으로 임계값을 설정하여 정확도가 높지 않습니다.
            // 그 이후로는 히스토그램의 모양이 정규분포도 같은 모양이 아닐 경우가 더 많습니다.
            // 분산이나 표준편차 등을 고려하거나 더 밀집된 값을 가지는 임계값을 사용한다면 더 높은 정확도를 얻는 자신만의 이진화 메소드를 구현할 수 있습니다.
            // 지금 확인한 임의의 메소드는 이전 강좌들을 통해 배운 형식을 응용하여 구성해 어렵지 않게 사용할 수 있습니다.
            // 또한 쉽게 수정할 수 있으며 히스토그램을 통하여 임계값을 계산하는 방식이 아닌 다른 공식이나 함수를 적용해서 정확도를 높일 수 있습니다.
        }



        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (bina != null) Cv.ReleaseImage(bina);
        }
    }
}


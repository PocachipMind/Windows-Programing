using System;
using System.Linq; 
using OpenCvSharp;

// 링큐 네임스페이스는 통합 데이터 질의 기능을 포함하고있습니다. 간단하게 배열에서 조금 더 편하게 원소를 찾을 수 있습니다.
// 예를들면 데이터 안에서 마지막 번째의 값이나 총 개수, 최대값, 최소값 등을 쉽게 검색할 수 있습니다.

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage bin;
        IplImage houline;

        public IplImage Binary(IplImage src, int threshold)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, threshold, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage HoughLines(IplImage src)
        {
            houline = new IplImage(src.Size, BitDepth.U8, 3);
            bin = this.Binary(src, 150);

            // 모폴로지 적용
            // 팽창 1회 , 침식 3회, 팽창 2회 > 노이즈를 1회 팽창시킨 후 3회 침식을 시켜 노이즈를 최대한 감소후 다시 팽창을 2번시켜 원래 크기로 복구시키기 위함
            Cv.Dilate(bin, bin, null, 1);
            Cv.Erode(bin, bin, null, 3);
            Cv.Dilate(bin, bin, null, 2);

            // 캐니 엣지 적용
            // 이미 이진화 처리하여 이미지는 0과 255의 값을 지니는 픽셀밖에 남지않아 캐니엣지의 임계값은 큰 의미가 없습니다. 단순하게 임계값 1은 0, 임계값 2는 255 사용. 어떤 값을 넣더라도 이진화 처리되어 결과는 동일합니다.
            Cv.Canny(bin, bin, 0, 255);

            // houline 에 캐니엣지 처리된 bin 필드를 색상의 값을 지니는 이미지로 저장해 보도록 하겠습니다
            // BGR 속성을 지니는 이미지로 변경하여 저장.
            Cv.CvtColor(bin, houline, ColorConversion.GrayToBgr); // 꼭 Cv.Copy나 클론등을 통해 복제하지 않아도 이미지가 변환되어 저장됩니다.
            // 이 경우 눈으로 볼 때는 똑같이 캐니 엣지 처리된 이미지이지만 bin 필드에는 채널 1을 가지는 컬러 이미지, houline필드에는 채널 3을 가지는 컬러 이미지입니다.
            // 채널 3을 가지는 컬러 이미지는 그 위에 새로운 색상을 덧칠할 수 있다는 차이점이 있습니다.
            // 즉 draw line 등을 통해 노란선이나 초록선을 그릴 때에는 houline 에서만 그릴수 있습니다.


            // HoughLines 사용하기 위한 메모리 선언
            CvMemStorage Storage = new CvMemStorage();

            // HoughLines을 사용하기 위해서 시퀀스 형태를 가지는 Lines 변수를 생성하고 bin 필드에 HoughLines2 함수를 적용해 봅니다.
            // < 계산 이미지, 메모리 저장소, 검출 방법, 로, 세타, 임계값, 파라미터1, 파라미터2 >
            // 로와 세타를 반환하는 스탠다드 방법 먼저 사용해봅니다.
            CvSeq lines = Cv.HoughLines2(bin, Storage, HoughLinesMethod.Standard, 1, Math.PI / 180, 140, 0, 0); // 임계값은 140을 사용하며 파라미터1과 파라미터2는 사용하지 않으므로 0을 사용합니다.

            // for문을 이용하여 검출된 선의 개수만큼 반복
            for(int i = 0; i<Math.Min(lines.Total,20); i++ )//여기서 반복횟수를 min을 이용하여 너무 많은 선이 검출되지 않게 합니다. Line.Total과 임의의수 20과 비교하여 더 작은 숫자를 반환하게 합니다. // 즉 검출된 선이 20개보다 많을 경우 20개 까지만 반복
            {
                // 검출된 로와 세타를 얻어내기 위해 Cv.GetSeqElem 함수 사용
                // 반환 형식은 폴라 형식이므로 CvLineSegmentPolar 선언. 이 형식은 허프라인2에서만 사용되는 형식입니다.

                // 이 변수에 검출 결과의 데이터를 포함시키도록 하겠습니다.
                CvLineSegmentPolar element = lines.GetSeqElem<CvLineSegmentPolar>(i).Value; // i 번째 값을 넣음 // 저장 방법이 생소하므로 기억해야함
                // element에 검출된 선의 값이 저장되었음.

                float r = element.Rho;
                float theta = element.Theta;

                double a = Math.Cos(theta); // 더블 형석인 a,b를 선언후 코사인 세타와 사인 세타 저장
                double b = Math.Sin(theta);

                // ppt에서 확인한 x0와 y0를 계산하기 위해
                double x0 = r * a;
                double y0 = r * b;

                // 스케일 선언. 넉넉히 주기 위해서 원본 이미지의 너비와 높이를 더한 값으로 사용하도록 하겠습니다.
                int scale = src.Size.Width + src.Size.Height;

                // ppt 에서 확인한 좌측 끝점인 PT 원점을 선언
                // cvPoint는 인트형만 포함하기 때문에 convert to int 32로 변환
                CvPoint pt1 = new CvPoint(Convert.ToInt32(x0 - scale * b), Convert.ToInt32(y0 + scale * a));
                CvPoint pt2 = new CvPoint(Convert.ToInt32(x0 + scale * b), Convert.ToInt32(y0 - scale * a));

                // Cv Draw Line으로 pt1점부터 pt2점까지 노란색으로 그려보겠습니다.
                Cv.DrawLine(houline, pt1, pt2, CvColor.Yellow, 1);
            }
            return houline;
            // 로와 세타를 반환하기 때문에 이 방식은 직선의 방정식을 만들어 시작점을 이미지 밖으로 또한 도착점도 이미지 밖으로 그리는 방법입니다.
            // 로와 세타를 반환하기 때문에 시작점이 정확히 어디인지 도착점이 정확하게 어디인지 계산하기는 매우 복잡해집니다.
            // 검출된 직선의 방정식을 필요할때 사용합니다.
        }

        public IplImage HoughLines2(IplImage src)
        {
            houline = new IplImage(src.Size, BitDepth.U8, 3);
            bin = this.Binary(src, 150);

            Cv.Dilate(bin, bin, null, 1);
            Cv.Erode(bin, bin, null, 3);
            Cv.Dilate(bin, bin, null, 2);
            Cv.Canny(bin, bin, 0, 255);

            Cv.CvtColor(bin, houline, ColorConversion.GrayToBgr); 

            CvMemStorage Storage = new CvMemStorage();

            // HoughLines을 사용하기 위해서 시퀀스 형태를 가지는 Lines 변수를 생성하고 bin 필드에 HoughLines2 함수를 적용해 봅니다.
            // < 계산 이미지, 메모리 저장소, 검출 방법, 로, 세타, 임계값, 파라미터1, 파라미터2 >
            // 시작점과 도착점을 반환하는 Probabilistic 방법 사용. 최소 선길이 50, 최대 선 간격 10으로 선언
            CvSeq lines = Cv.HoughLines2(bin, Storage, HoughLinesMethod.Probabilistic, 1, Math.PI / 180, 140, 50, 10);

            for (int i = 0; i < Math.Min(lines.Total, 20); i++)
            {
                // 엘리먼트 변수 생성. 이번엔 Point로. 시작점과 도착점을 반환할 예정이므로 포인트 형식입니다.
                CvLineSegmentPoint element = lines.GetSeqElem<CvLineSegmentPoint>(i).Value; // 시작점과 도착점을 반환함으로 추가적 계산식 만들 필요 없음

                // P1이 시작점 P2가 도착점
                Cv.DrawLine(houline, element.P1, element.P2, CvColor.Yellow, 1);
            }


            return houline;

        }



        // 허프라인 함수는 모든 점에 대하여 직선의 방정식을 생성해 직선을 검출하기 때문에 이미지를 깔끔하게 만드는 것이 가장 중요합니다.
        // 직선의 방정식이 필요한 경우 로와 세타를 반환하는 검출 방법을 사용하거나 검출 지점이 필요한 경우 시작점과 도착점을 반환하는 검출 방법을 사용하시면 됩니다.
        // 임의의 점을 이용하여 직선을 찾기 때문에 좀 더 쉽게 표시할 수 있고 간단하게 데이터를 얻을 수 있습니다.
        // 이 역시 동일하게 이미지를 깔끔하게 만드는 것이 가장 중요합니다. 함수 자체는 크게 어려운 것이 없습니다. 가장 중요한 것은 검출 이미지를 어떻게 더 깔끔하게 만드느냐 입니다.
        // 검출 이미지를 최대한 깔끔하게 만들고 노이즈를 최소화하는 것이 연산 속도를 빠르게 하고 정확도 높은 결과를 얻어낼 수 있습니다. 



        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문을 추가
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (houline != null) Cv.ReleaseImage(houline);
        }
    }
}


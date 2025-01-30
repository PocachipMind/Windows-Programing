using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        // 06 윤곽선 검출함수는 이진화 이미지를 사용하므로 이진화 코드에서 추가적으로 수정합니다.
        IplImage bin;
        IplImage con;

        public IplImage Binary(IplImage src, int threshold)
        {
            // 이진화는 흑백 색상만 존재해서 채널 1
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, threshold, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage FindContour(IplImage src)
        {
            // 콘필드의 채널 3인 이유 : 검출 결과를 표시할 때 드로잉의 색상이 포함되어 있기 때문.
            // 빨강이나 노랑으로 표시해야하는데 채널이 1일 경우 오류 발생
            con = new IplImage(src.Size, BitDepth.U8, 3);

            Cv.Copy(src, con);
            
            // 콘 필드는 불러온 이미지 위에 그대로 결과를 표시하는 역할이며 빈 필드는 윤곽선 계산을 위한 이미지입니다.
            // 즉 계산용 이미지와 결과용 이미지를 따로 사용합니다.
            bin = this.Binary(src, 150);

            // 윤곽선의 메모리를 저장할 스토리지, 윤곽선의 정보가 담겨있는 시퀀스 생성
            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;

            // 윤곽선 검출 함수 < 이진화 이미지, 메모리 저장소, 윤곽선 저장 공간, 자료 구조의 크기, 검색 방법, 근사화 방법 >
            // 여기서 윤곽선 저장 공간의 인수를 채울 때 out 키워드를 포함하셔야 합니다.
            // 자료구조의 크기는 cvContourSize나 cv Chain sizeoff를 사용하시면 됩니다.
            // 검색 방법과 근사화 방법은 list방법과 approxNone 방법
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);

            // CvPoint는 2차원 좌표를 포함하고있습니다. 시퀀스는 이 CvPoint들의 묶음입니다.
            // 검출된 오브젝트마다 모든 좌표를 저장하고 있습니다.
            // 드로우 서클을 사용해서 검출된 Contours 위치에 원을 그려보도록 합니다.
            for(int i = 0; i< contours.Total; i++)
            {
                // 배열 형식을 사용하여 불러오되, 점 밸류를 포함하셔야합니다.
                Cv.DrawCircle(con, contours[i].Value, 3, CvColor.Red);
            }
            // 즉 시퀀스는 하나의 덩어리이며 그 안에 모든 점들이 포함되어 있습니다.

            // 다음 시퀀스로 넘겨보도록 합니다.
            // HNext : 다음 시퀀스 , VNext : 다다음 시퀀스, HPrev : 이전 시퀀스 , VPrev : 2단계 전 시퀀스
            contours = contours.HNext;
            for (int i = 0; i < contours.Total; i++)
            {
                // 배열 형식을 사용하여 불러오되, 점 밸류를 포함하셔야합니다.
                Cv.DrawCircle(con, contours[i].Value, 3, CvColor.Red);
            }


            // 전체 모두 그리기
            while( contours != null )
            {
                for (int i = 0; i < contours.Total; i++)
                {
                    // 배열 형식을 사용하여 불러오되, 점 밸류를 포함하셔야합니다.
                    Cv.DrawCircle(con, contours[i].Value, 3, CvColor.Red);
                }
                contours = contours.HNext;
            }

            return con;
        }

        public IplImage FindContour2(IplImage src)
        {

            con = new IplImage(src.Size, BitDepth.U8, 3);

            Cv.Copy(src, con);
            bin = this.Binary(src, 150);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;

            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);

            // 드로우 컨투어 사용해보기
            // < 결과 이미지, 윤곽선 저장 공간, 외곽윤곽의 색상, 내곽윤곽의 색상, 최대 레벨, 두께, 선형 타입>
            // 최대 레벨이 0일 경우 지정된 윤곽선만 그리며 최대 레벨이 1일 경우 윤곽선과 중첩된 모든 윤곽선을 그립니다.
            // 최대 레벨이 2일 경우 윤곽선과 중첩된 윤곽선, 중첩에 중첩된 윤곽선을 그리게 됩니다.
            // 선형 타입은 (AntiAlias) 윤곽을 그렸을 때 계단 현상을 최소화하는 역할을 합니다.
            Cv.DrawContours(con, contours, CvColor.Yellow, CvColor.Red, 1, 4, LineType.AntiAlias);

            // 검출된 데이터를 모두 사용했으므로 스토리지와 Contours 메모리를 해제할 수 있습니다.
            // 현재 클래스에서 변수 형식으로 선언하여 따로 메모리를 해제할 필요가 없지만 메인폼이나 클래스 내보에서도 코드가 길어질 때 메모리를 지속적으로 해제해 주셔야합니다.

            // 메모리 저장소의 메모리 해제
            Cv.ReleaseMemStorage(Storage);

            // 시퀀스 메모리 비우기
            Cv.ClearSeq(contours);

            return con;
        }

        public IplImage ScannerContour(IplImage src)
        {

            con = new IplImage(src.Size, BitDepth.U8, 3);

            Cv.Copy(src, con);
            bin = this.Binary(src, 150);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;

            // 스캐너의 경우 스캐너 변수를 생성해야합니다.
            // 인수 순서는 findcontours와 비슷합니다.
            // < 이진화 이미지, 메모리 저장소, 자료구조의 크기, 검색방법, 근사화 방법 >
            // 단, 여기서 윤곽선 저장 공간은 할당하지 않습니다. 스캐너는 하나하나씩 오브젝트를 검출하는 방법이기 때문입니다.
            CvContourScanner scanner = Cv.StartFindContours(bin, Storage, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);
            // FindContour는 한번에 모든 윤곽선을 그려주었지만, 스캐너 방법은 하나하나씩 검출하기 때문에 찾은 윤곽선마다 그려주어야 합니다.

            // 그리는법 1. While문
            // 컨투어스를 다음 컨투어로 검출하게 실행시킴 FindNextContour는 이미지에서 순차적으로 컨투어를 검사합니다.
            // 이미지에서 더 이상 검사할 컨투어가 없는 경우 널 값을 반환합니다.
            while ((contours = Cv.FindNextContour(scanner)) != null)
            {
                // 윤곽선의 검출점의 위치가 1,1일경우 뛰어넘기
                if (contours[0].Value == new CvPoint(1, 1)) continue;
                Cv.DrawContours(con, contours, CvColor.Yellow, CvColor.Red, 1, 4, LineType.AntiAlias);
            }
            // while문이 종료되었다면 윤곽선이 더 이상 검출할 것이 남아있지 않으므로 윤곽선 검출을 종료합니다.
            Cv.EndFindContours(scanner); // 스캐너 탐색 중단

            // Find Contour와 다른점은 Contour 변수 안에 들어있는 양이 다릅니다.
            // Find Contour는 전체 이미지의 모든 윤곽선에 대한 정보가 담겨 있고 Scanner는 가장 최초로 검출된 윤곽선의 정보만 담겨있습니다.
            // 즉 Find Contour는 모든 윤곽선이며 Scanner는 한개 오브젝트의 윤곽선입니다.

            return con;
        }

        public IplImage ScannerContour2(IplImage src)
        {

            con = new IplImage(src.Size, BitDepth.U8, 3);

            Cv.Copy(src, con);
            bin = this.Binary(src, 150);

            CvMemStorage Storage = new CvMemStorage();

            CvContourScanner scanner = Cv.StartFindContours(bin, Storage, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);


            // 그리는법 2. Foreach문
            foreach(CvSeq<CvPoint> contours in scanner)
            {
                if (contours[0].Value == new CvPoint(1, 1)) continue;
                con.DrawContours(contours, CvColor.Yellow, CvColor.Red, 1, 4, LineType.AntiAlias);
            }

            return con;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (con != null) Cv.ReleaseImage(con);
        }
    }
}


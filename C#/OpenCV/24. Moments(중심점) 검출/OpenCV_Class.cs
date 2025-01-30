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
        IplImage apcon;
        IplImage mom;

        public IplImage Binary(IplImage src, int threshold)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, threshold, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage ApproxPoly(IplImage src)
        {
            apcon = new IplImage(src.Size, BitDepth.U8, 3);

            Cv.Copy(src, apcon);
            bin = this.Binary(src, 200);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);
            CvSeq<CvPoint> apcon_seq = Cv.ApproxPoly(contours, CvContour.SizeOf, Storage, ApproxPolyMethod.DP, 3, true);

            for (CvSeq<CvPoint> c = apcon_seq; c != null; c = c.HNext)
            {
                if(c.Total > 4)
                {
                    for(int i = 0; i<c.Total; i++)
                    {
                        CvPoint conpt = new CvPoint(c[i].Value.X, c[i].Value.Y);
                        CvPoint? p = Cv.GetSeqElem(c, i);
                        conpt.X = p.Value.X;
                        conpt.Y = p.Value.Y;

                        Cv.DrawCircle(apcon, conpt, 3, CvColor.Black);

                    }
                }
            }

            return apcon;

        }


        public IplImage CenterPoint(IplImage src)
        {
            mom = new IplImage(src.Size, BitDepth.U8, 3);

            Cv.Copy(src, mom);
            bin = this.Binary(src, 200);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);
            CvSeq<CvPoint> apcon_seq = Cv.ApproxPoly(contours, CvContour.SizeOf, Storage, ApproxPolyMethod.DP, 3, true);


            // 모멘트 함수를 사용하고 무게 중심점을 검출하기 위해 해당 값을 저장할 변수 생성
            // 모멘트 변수, 중심점 x, 중심점 y
            CvMoments moments;
            int cX = 0, cY = 0;

            for (CvSeq<CvPoint> c = apcon_seq; c != null; c = c.HNext)
            {
                if (c.Total > 4)
                {
                    // 모멘트 함수 사용
                    // < 시퀀스 , 모멘트 저장 공간, 이진화 조건 >
                    // 이진화 조건은 이진화 메소드처럼 픽셀이 0값이 아닌 경우 모두 1로 계산하여 처리하게 됩니다.
                    Cv.Moments(c, out moments, true);

                    // cx와 cy 중심점 좌표 계산
                    // 무게 중심 공식 사용
                    // Moment는 더블형이므로 int 형으로 변경하기위해
                    cX = Convert.ToInt32(moments.M10 / moments.M00);
                    cY = Convert.ToInt32(moments.M01 / moments.M00);

                    Cv.DrawCircle(mom, new CvPoint(cX, cY), 5, CvColor.Red, -1);
                }
            }

            return mom;

        }


        // 모멘트 함수의경우 다양한 모멘트들을 계산해 줍니다.
        // 역학적인 풀이가 필요한 경우 해당 모멘트들을 추가적으로 계산할 필요 없이 주어진 공식에 대입하여 풀이하시면 됩니다.
        // 또한 변수를 인트형이나 더블형처럼 일치시켜주셔야 합니다.
        // 역학적인 풀이가 필요하지 않는 경우 무게중심점 공식만 숙지하시면 여러 검출 결과에서 유용하게 사용하실 수 있습니다.
        // 검출된 오브젝트의 무게중심점으로 새롭게 계산이나 검출을 시작할 수 있으며 또는 검출된 무게중심점에서 검출 완료 등을 표시하는 방법으로 응용할 수 있습니다.
        // 무게 중심점은 많은 계산식에서 사용되는 중요한 요소입니다. 무게중심점 계산 공식을 숙지하시길 바랍니다.


        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문을 추가
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (apcon != null) Cv.ReleaseImage(apcon);
            if (mom != null) Cv.ReleaseImage(mom);
        }
    }
}


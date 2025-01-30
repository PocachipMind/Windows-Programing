using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage bin;
        IplImage apcon;

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

            // ApproxPoly 함수를 사용하기 위해 이전 강좌의 FindContours
            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);

            // ApproxPoly 함수 . 해당 함수는 시퀀스 안에 포인트들이 저장되어 있는 변수로 값을 반환해줍니다.
            // < 검출된 컨투어, 자료구조의 크기, 메모리 저장소, 근사 방법, 근사 정확도, 시퀀스 결정 >
            CvSeq<CvPoint> apcon_seq = Cv.ApproxPoly(contours, CvContour.SizeOf, Storage, ApproxPolyMethod.DP, 3, true);
            // 근사 방법은 Douglas-Peucker 방식만 가능함으로 dp를 적용하며 근사 정확도는 3, 시퀀스 결정은 true
            // apcon_seq에는 시퀀스 형태로 근사화된 코너점들이 포함되어 있습니다.

            // 이중 포문을 이용하여 첫번째 for 문에는 시퀀스 단계를 너미고 두번째 for문에는 해당 시퀀스에서 포인트들을 확인합니다.
            for (CvSeq<CvPoint> c = apcon_seq; c != null; c = c.HNext)
            {
                // 검출된 코너점들이 4개 이상일때만 검출하도록 설정
                // c의 갯수가 4개 이상일때만 실행
                if(c.Total > 4)
                {
                    // c의 갯수만큼 for 문 반복
                    for(int i = 0; i<c.Total; i++)
                    {
                        // 시퀀스 안에 포인트를 얻기위한 변수
                        CvPoint conpt = new CvPoint(c[i].Value.X, c[i].Value.Y);
                        // 인덱스를 사용하여 시퀀스 요소에 대한 포인트 반환 함수
                        CvPoint? p = Cv.GetSeqElem(c, i); // < 시퀀스, 해당 시퀀스의 인덱스 > 
                        // 즉 c 시퀀스 안에 담겨있는 i 번째 코너점을 불러온다.
                        conpt.X = p.Value.X;
                        conpt.Y = p.Value.Y;

                        Cv.DrawCircle(apcon, conpt, 3, CvColor.Black);

                    }
                }
            }

            return apcon;

        }

        // ApproxPoly 함수는 곡선을 근사화하여 코너점으로 간주하는 함수입니다.ApproxPoly 함수에서는 근사정확도 인수의 결정이 가장 중요합니다.
        // 코너점들을 검출하기 위해 사용되는 FindConturs 함수의 인수는 검색 방법과 근사 방법을 중요하게 사용합니다.
        // 그러므로 FindConturs 함수에서 중요하게 사용되는 이진화 이미지의 정확도가 가장 중요합니다.
        // 즉 어떤 함수를 사용하던 최초 계산에 사용될 검출 이미지의 형성이 가장 중요합니다.
        // 그레이 스케일이나 이진화, 무폴로지 연산 등을 사용하여 가장 검출하기 좋은 형태로 만드는 것이 중요합니다.
        // 노이즈가 많고 이미지가 깔끔하지 않으면 FindConturs에서 컨투어를 제대로 검출하지 못하게 됩니다.
        // ApproxPoly는 컨투어를 통해 근사화하기 때문에 코너점들까지 제대로 검출되지 않습니다.
        // 가장 최초의 계산 이미지의 설정이 제대로 되어야 검출이 정확히 될 수 있습니다.

        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문을 추가
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (apcon != null) Cv.ReleaseImage(apcon);
        }
    }
}


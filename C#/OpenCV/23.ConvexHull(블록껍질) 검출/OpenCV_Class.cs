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
        IplImage convex;

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

        public IplImage ConvexHull(IplImage src)
        {
            convex = new IplImage(src.Size, BitDepth.U8, 3);

            Cv.Copy(src, convex);
            bin = this.Binary(src, 200);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);
            CvSeq<CvPoint> apcon_seq = Cv.ApproxPoly(contours, CvContour.SizeOf, Storage, ApproxPolyMethod.DP, 3, true);

            for (CvSeq<CvPoint> c = apcon_seq; c != null; c = c.HNext)
            {
                if (c.Total > 4)
                {
                    // ConvexHull 함수는 Cv Point 배열 형식을 사용하여 최외곽의 코너점들을 판단하기 위한 데이터로 사용합니다.
                    CvPoint[] conpt = new CvPoint[c.Total]; // 배열의 크기는 코너점들의 개수와 동일.
                    for (int i = 0; i < c.Total; i++)
                    {
                        CvPoint? p = Cv.GetSeqElem(c, i);

                        conpt[i] = new CvPoint(p.Value.X, p.Value.Y); // conpt 배열의 점들을 모두 저장시키는 방법

                    }

                    CvPoint[] hull; // 이 변수는 Convex Hull 함수를 통하여 검출된 최외곽의 코너점들만 분류하여 담아줍니다.

                    Cv.ConvexHull2(conpt, out hull, ConvexHullOrientation.Clockwise); // < 코너점들의 배열, 최외곽점의 저장 공간, 회전방향 >
                    // 최외곽 저장공간은 아웃키워드를 포함하며 시계방향으로 검출하도록 하겠습니다.

                    // 최초 시작점을 할당하기전 링큐 네임스페이스 추가
                    // 마지막 점을 가장 최초의 시작점으로 할당하기 위해 PTO 변수를 새롭게 생성.
                    CvPoint pt0 = hull.Last();

                    // foreach 반복문을 사용하여 hull 변수에 있는 모든 값을 pt에 담아 반복시키도록 합니다.
                    foreach(CvPoint pt in hull)
                    {
                        // 가장 최초점인 마지막점과 시작점을 연결
                        Cv.DrawLine(convex, pt0, pt, CvColor.Green, 2);
                        // pt0 변수에 현재의 시작점을 할당하게 합니다.
                        pt0 = pt;
                        // 이렇게 할 경우 첫번째 반복 실행에는 마지막 점과 시작점이 이어지며 두번째 반복실행에는 시작점과 두번째 점이 연결됩니다.
                        // 마지막 반복 실행에는 마지막 이전점과 마지막 점이 연결되어 시계방향, 반시계 방향 상관없이 이어줄 수 있습니다.
                    }

                }
            }

            return convex;

        }

        // convex은 다른 함수들을 기본적으로 진행이 된 후 사용되는 함수입니다.
        // 그러므로 convex을 손쉽게 사용하기 위해선 다른 함수들의 사용법과 CvPoint 배열 형식으로 지점들을 담는 방법을 익히셔야합니다.


        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문을 추가
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (apcon != null) Cv.ReleaseImage(apcon);
            if (convex != null) Cv.ReleaseImage(convex);
        }
    }
}


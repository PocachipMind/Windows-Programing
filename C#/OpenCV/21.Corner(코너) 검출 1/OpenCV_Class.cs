using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage gray;
        IplImage corner;

        // 그레이 스케일 메소드
        public IplImage GrayScale(IplImage src)
        {
            
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage GoodFeaturesToTrack(IplImage src)
        {

            corner = new IplImage(src.Size, BitDepth.U8, 3);

            //모폴로지 연산과 동일하게 코너를 검출하는 메소드에서도 잠깐 저장할 이미지 공간이 필요.
            IplImage eigImage = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage tempImage = new IplImage(src.Size, BitDepth.U8, 1);

            // 코너필드의 원본을 복사, 그레이필드에 그레이 스케일 적용
            Cv.Copy(src, corner);
            gray = this.GrayScale(src);

            // 코너점을 저장할 공간과 최대 코너 제한 개수를 설정.
            // 코너점을 저장할 공간
            CvPoint2D32f[] corners;
            // 코너 최대 개수 설정 : 이 값보다 더 많은 개수가 검출될 경우 코너의 강도가 좋은 순으로 반환
            int cornerCount = 150;

            // 코너 검출 함수 < 계산 이미지, 첫 번째 임시 이미지, 두 번째 임시 이미지, 코너 저장 공간, 최대 코너의 개수, 퀄리티 레벨, 최소 거리 >
            // 코너 저장 공간은 아웃 키워드를 포함해야하며 최대 코너 개수는 레퍼런스 키워드 사용해야함.
            Cv.GoodFeaturesToTrack(gray, eigImage, tempImage, out corners, ref cornerCount, 0.01, 5);
            // 아웃 키워드를 사용하여 검출된 코너들이 배열에 저장되며 레퍼런스 키워드를 사용해서 검출된 코너점들을 다시 저장합니다.
            // 이 함수를 거치게 되면 배열엔 코너점들이 담기게 되고 코너 카운트 변수에는 검출된 코너의 개수로 덮어져 저장됩니다.


            // < 계산 이미지, 코너 저장 공간, 최대 코너점 개수, 검출 영역, 검출 제외 영역, 정밀화 반복 작업 >
            Cv.FindCornerSubPix(gray, corners, cornerCount, new CvSize(10, 10), new CvSize(-1, -1), new CvTermCriteria(100, 0.01));
            // 검출 영역에 x에 10, y에 10을 넣어 넓은 범위로 검색을 해보도록 합니다. 즉 21x21의 영역으로 검색합니다.
            // 검출 제외 영역이 없게 설정합니다. 또한 정밀화 반복작업은 총 100회 또는 정확도가 0.01에 도달할 경우 종료하게 됩니다.


            // For 문을 이용하여 코너 카운트의 개수만큼 반복하며 검출된 코너점에 원을 그려 보도록합니다.
            for(int i = 0; i<cornerCount; i++)
            {
                // 검출된 좌표는 코너스 배열에 담겨있습니다.
                Cv.DrawCircle(corner, corners[i], 3, CvColor.Black, 2);

            }

            return corner;

        }

        public IplImage GoodFeaturesToTrack2(IplImage src)
        {
            // 해당 함수에서 Harris Corner를 적용해봅니다.
            corner = new IplImage(src.Size, BitDepth.U8, 3);

            IplImage eigImage = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage tempImage = new IplImage(src.Size, BitDepth.U8, 1);

            Cv.Copy(src, corner);
            gray = this.GrayScale(src);

            CvPoint2D32f[] corners;
            int cornerCount = 150;
            // 관심 영역을 null로 사용하여 모든 곳에서 검출, 블록사이즈는 3으로 설정 : 블록사이즈가 너무 클경우 검출이 제대로 되지 않습니다.
            // 해리스 방법을 사용할 예정이므로 True로 사용하고 K값은 0.01로 사용합니다.
            Cv.GoodFeaturesToTrack(gray, eigImage, tempImage, out corners, ref cornerCount, 0.01, 5, null, 3, true, 0.01);



            for (int i = 0; i < cornerCount; i++)
            {
                Cv.DrawCircle(corner, corners[i], 3, CvColor.Black, 2);
            }

            return corner;

        }

        public IplImage HarrisCorner(IplImage src)
        {
            // 해리스 코너 함수는 float32 정밀도를 사용해서 결과를 반환합니다.
            corner = new IplImage(src.Size, BitDepth.F32, 1);

            gray = this.GrayScale(src);

            // 해리스 코너 함수는 추가적으로 선언해야하는 변수가 없습니다.

            // < 원본 이미지, 결과 이미지, 블록 사이즈, 커널 크기, k 사이즈 >
            // 블록 크기는 3으로 사용하며 커널 크기도 3 k 사이즈는 0.01 사용
            Cv.CornerHarris(gray, corner, 3, ApertureSize.Size3, 0.01);

            return corner;
        }

        // Good Features To Track의 해리스 코너법은 CVPoint 형식으로 위치를 반환하며 헤리스 함수는 이미지를 반환합니다. 
        // 오브젝트 등을 검출하는 함수에 있어서 정확한 위치가 필요하다면 Good Features To Track 방법을 사용하며
        // 이미지를 통해 바로 처리하는 경우에는 해리스 함수를 사용하시면 됩니다.
        // 또한 Good Features To Track 함수를 사용할 때 퀄리티 레벨과 최소 거리를 적절하게 설정해 주어야 하며 FindCornerSubPix를 사용하여 코너점을 재 조정할 때 검출 영역과 정밀화 반복 작업의 인수 또한 적절한 값을 사용해야합니다.
        // 해리스 코너를 이용할때는 블록 사이즈와 케이스 사이즈에 많은 영향을 받으므로 너무 작지도 너무 크지도 않은 값을 사용하셔야 합니다.

        public void Dispose()// 사용이 끝난 필드의 메모리 해제 구문을 추가
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (corner != null) Cv.ReleaseImage(corner);
        }
    }
}


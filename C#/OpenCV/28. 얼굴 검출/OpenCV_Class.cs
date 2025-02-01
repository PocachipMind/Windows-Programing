using System;
using OpenCvSharp;


namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage gray;
        IplImage haarface;

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage FaceDetection(IplImage src)
        {
            haarface = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.Copy(src, haarface);
            gray = this.GrayScale(src);

            // 그레이 스케일 이미지에 대비를 맞춰주기 위해서 히스토그램을 평활화 하도록 하겠습니다.
            // 히스토그램의 높이를 일정하게 만들어 대비를 일정하게 맞춥니다.
            Cv.EqualizeHist(gray, gray);

            // 얼굴 검출 함수를 사용하기 전에 상수인 Scale Factor와 minNeighbors 상수를 설정합니다.
            double scaleFactor = 1.139;
            int minNeighbors = 1;

            // xml파일을 포함할 Haar Classifier 캐스케이드 형식의 변수 선언
            CvHaarClassifierCascade cascade = CvHaarClassifierCascade.FromFile("../../haarcascade_frontalface_alt.xml");// 저장된 경로에 xml 파일을 포함

            // 다음으로 메모리 저장소 생성
            CvMemStorage storage = new CvMemStorage();

            // 얼굴 검출 함수 사용
            // 얼굴 검출 함수 또한 시퀀스 형태의 에버리지 컴프 형식을 저장합니다.
            CvSeq<CvAvgComp> faces = Cv.HaarDetectObjects(gray, cascade, storage, scaleFactor, minNeighbors, HaarDetectionType.ScaleImage, new CvSize(90, 90), new CvSize(0, 0)); 
            // 얼굴 검출 함수 적용 < 검출용 이미지, 캐스케이드, 메모리 저장소, 스케일 팩터, 민네이버스, 검출 방법, 최소 크기, 최대 크기>
            // 검출 방법은 스케일 이미지를 사용하며 최소 크기는 90,90보다 크며 최대 크기는 고려하지 않도록 합니다.

            // for 문을 이용하여 검출된 얼굴의 개수만큼 반복합니다.
            for ( int i=0 ; i < faces.Total ; i++ )
            {
                CvRect r = faces[i].Value.Rect;

                // 중심점 좌표 설정 PPT에서 본 공식 적용. 정수형을 사용하므로 Cv.Round를 사용하여 반올림의 정수형으로 맞춰줌.
                int cX = Cv.Round(r.X + r.Width * 0.5);
                int cY = Cv.Round(r.Y + r.Height * 0.5);
                // 반지름 계산
                int radius = Cv.Round((r.Width + r.Height) * 0.25);

                // 검출된 얼굴 좌표위에 그리기
                Cv.DrawCircle(haarface, new CvPoint(cX, cY), radius, CvColor.Black, 3);

            }
            return haarface;

        }


        // 얼굴 검출 함수는 상수로 인하여 검출의 정확도에 큰 영향을 미칩니다.
        // 그러므로 스케일 팩터의 값을 적절하게 주는 것이 가장 중요한 요소입니다.
        // XML 파일을 불러와 정해진 패턴을 찾는 형식이므로 검출용 이미지에서 얼굴의 특징을 살리는 것 또한 검출의 정확도에 큰 영향을 미칩니다.
        // 조금 더 정확도를 높이는 방법으로는 검출용 이미지에서 리사이즈 함수 등을 적용해 검출용 이미지에서 얼굴의 형태를 더 찾기 쉽게 만드는 방법입니다.
        // 이미지의 크기를 키워 검색을 시도한다면 얼굴의 특징이 더 부각되기 때문에 얼굴 특징이 제대로 검출되지 않았던 작은 얼굴도 검출 할 수 있습니다.



        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
            if (haarface != null) Cv.ReleaseImage(haarface);
        }
    }
}


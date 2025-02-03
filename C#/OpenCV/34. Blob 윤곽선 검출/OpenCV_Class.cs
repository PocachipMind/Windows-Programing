using System;
using OpenCvSharp;
using System.Collections.Generic;
// 이 네임스페이스는 목록, 배열, 테이블, 사전 등에 대한 개체의 제너릭 콜렉션을 정의하는 함수가 포함되어 있습니다.
// 이 네임스페이스에 포함되어 있는 Key Value Pair 함수를 사용하여 블롭에서 키 값을 호출해 Value 값을 불러와 적용합니다.

using OpenCvSharp.Blob;
// 해당 네임스페이스에 블롭 함수 포함되어있음.


namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        IplImage bin;
        IplImage blob;
        IplImage blobcontour;

        public IplImage Binary(IplImage src, int threshold)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, threshold, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage BlobImage(IplImage src)
        {
            blob = new IplImage(src.Size, BitDepth.U8, 3);
            bin = this.Binary(src, 50);

            // 라벨링을 실행하기 위해 블롭 생성자를 생성합니다.
            CvBlobs blobs = new CvBlobs();
            blobs.Label(bin); // < 검출할 이미지 >

            // 이진화 이미지를 기반으로 라벨링을 실행합니다.
            // 여기서 검출용 이미지에 노이즈가 많다면 노이즈 또한 라벨링을 진행하기 때문에 노이즈를 최소화하는 것이 라벨링에서 가장 중요합니다.
            // 이제 라벨링된 이미지를 렌더링하여 라벨링 결과를 표시하도록 하겠습니다.
            // 이 단계는 필수적인 단계가 아니며 라벨링이 진행된 결과를 시각적으로 확인하기 위해 사용합니다. 
            blobs.RenderBlobs(src, blob); // < 원본 , 결과 >

            // foreach문을 사용하여 검출된 결과를 반복하여 표시.
            foreach(KeyValuePair<int,CvBlob> item in blobs) // 앞서 선언한 컬렉션 제너링 네임스페이스에 포함되어있는 키밸류페어 사용.
            {
                CvBlob b = item.Value; // b 변수에서 각각의 라벨링 넘버에 따른 정보가 담겨있습니다.

                // 라벨링 번호 표시
                // 라벨링 번호는 b.label에 담겨있으며 중심점은 b.centroid에 담겨있습니다.
                Cv.PutText(blob, Convert.ToString(b.Label), b.Centroid, new CvFont(FontFace.HersheyComplex, 1, 1), CvColor.Red);
                // 이외에도 에어리어에는 면적, 앵글에는 각도, 렉트에는 사각형 정보, 컨투어 점 스타팅 포인트에는 라벨링의 시작 위치 등이 담겨있습니다.
            }
            return blob;
        }

        public IplImage BlobContourImage(IplImage src)
        {
            blobcontour = new IplImage(src.Size, BitDepth.U8, 3);
            bin = this.Binary(src, 50);

            CvBlobs blobs = new CvBlobs();
            blobs.Label(bin);

            foreach (KeyValuePair<int, CvBlob> item in blobs)
            {
                CvBlob b = item.Value;

                // b 값에서 컨투어 값을 불러오도록 하겠습니다. cv컨투어 체인 형식의 cc변수를 생성하고 
                // 블록 컨투어를 담습니다.
                CvContourChainCode cc = b.Contour;
                // 해당 변수를 렌더링하여 결과 확인
                cc.Render(blobcontour);

                // 윤곽선들을 다각화하기 위해서 CvContourPolygon 형식의 ex_polygon 선언하고
                // 블롭 컨투어인 cc변수를 convertToPolygon으로 이용하여 다각형으로 변환합니다.
                CvContourPolygon ex_polygon = cc.ConvertToPolygon();

                // foreach 문을 사용하여 ex 폴리곤 변수들의 값을 cvPoint 형식으로 p 값에 담습니다.
                foreach(CvPoint p in ex_polygon)
                {
                    // 드로우 서클을 이용하여 블럭 컨투어 필드의 윤곽점 지점들을 파란색으로 그려보도록 하겠습니다.
                    Cv.DrawCircle(blobcontour, p, 1, CvColor.Blue, -1);
                }


                // 내부 윤곽은 for문 두번 중첩해야합니다.
                for (int i = 0; i < b.InternalContours.Count; i++)
                {
                    CvContourPolygon in_polygon = b.InternalContours[i].ConvertToPolygon(); // 내부 윤곽선 다각화
                    foreach(CvPoint p in in_polygon)
                    {
                        // 내부 윤곽은 빨간색 원으로 그려보겠습니다.
                        Cv.DrawCircle(blobcontour, p, 1, CvColor.Red, -1);
                    }
                }

            }
            return blobcontour;
        }

        // 이전 강좌에서 배운 컨투어의 원리와 동일하지만 라벨링이 적용된 이미지를 통하여 검출한다는 것을 알 수 있습니다.
        // 내부 윤곽을 검출하는 순서를 확인해 본다면 라벨링된 개수만큼 반복하고 라벨링 된 오브젝트 안에서 다시 내부 윤곽의 개수를 확인한 뒤 해당 내부 윤곽을 다각화하는 방식입니다.
        // 다양한 변수들이 사용되고 변수의 인자들이 복잡합니다. 또한 반복문이 여러번 중첩되어서 혼동이 오기 쉽습니다.
        // 각각의 변수의 인자들이 어떤 역할을 하며 어떤 방식으로 반복문이 중첩되는지 이해하신다면 쉽게 사용하실 수 있습니다.


        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (blob != null) Cv.ReleaseImage(blob);
            if (blobcontour != null) Cv.ReleaseImage(blobcontour);
        }
    }
}


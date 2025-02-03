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

        // 라벨링은 검출용 이미지를 생성할 때 오브젝트의 형태를 최대한 유지하면서 노이즈를 제거하는 것이 가장 중요합니다.
        // 노이즈가 많이 발생한다면 노이즈 또한 라벨링이 됩니다. 
        // 노이즈를 최소화해서도 완전히 노이즈를 제거할 수 없을 땐 라벨링의 면적을 의미하는 에어리어의 값을 비교하여 일정값 이하일 경우 사용하지 않게 하여 면적의 값이 높은 라벨링만 남기는 방법 등으로 활용할 수 있습니다.
        // 중심점을 비롯하여 등고선의 각도, 라벨링의 시작 위치 등 다양한 정보를 담고 있으므로 유용하게 활용할 수 있는 함수입니다.


        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (blob != null) Cv.ReleaseImage(blob);
        }
    }
}


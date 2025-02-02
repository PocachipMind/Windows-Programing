using System;
using OpenCvSharp;

// 형식변환을 이용하기 위해선 기본적으로 포함되어야하는 참조가 있습니다.
// 1강에서 OpenCV를 설치할 때 사용하였던 참조관리자를 통해 DLL하나를 추가해야합니다.
// PresentationCore DLL 추가 : 해당 DLL은 Bitmap 소스를 사용할 수 있게 해줌.

// 비트맵 확장자를 포함하고있는 시스템 드로잉 추가
using System.Drawing;
// 형식 변환을 포함하고 있는 OepnCVSharp.Extension 추가 
using OpenCvSharp.Extensions;


namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable // 영상처리에서는 메모리관리가 중요하므로 해당 상속으로 관리되지않은 메모리를 해제할 수 있습니다.
    {
        IplImage ipl;

        public IplImage ToBitmap(IplImage src)
        {
            ipl = new IplImage(src.Size, BitDepth.U8, 3);

            // src 이미지를 비트맵 변환
            Bitmap bitmap = src.ToBitmap();

            // 비트맵으로 변환했으므로 그래픽스 적용. C# 에서 사용하는 그래픽스 적용법과 동일합니다.
            Graphics grp = Graphics.FromImage(bitmap);

            // 그래픽스에 빨간색 원그려봅니다.
            grp.DrawEllipse(new Pen(Color.Red, 10), 10, 10, 200, 200);

            // 비트맵을 다시 IPL 형식으로 변환
            ipl = bitmap.ToIplImage();

            return ipl;

        }

        // 인수를 담는 방법으로 사용
        public IplImage ToBitmap2(IplImage src)
        {
            ipl = new IplImage(src.Size, BitDepth.U8, 3);

            // 비트맵 컨버터 사용
            // Bitmap bitmap = src.ToBitmap();
            Bitmap bitmap = BitmapConverter.ToBitmap(src);

            // 비트맵으로 변환했으므로 그래픽스 적용. C# 에서 사용하는 그래픽스 적용법과 동일합니다.
            Graphics grp = Graphics.FromImage(bitmap);

            // 그래픽스에 빨간색 원그려봅니다.
            grp.DrawEllipse(new Pen(Color.Red, 10), 10, 10, 200, 200);

            //ipl = bitmap.ToIplImage();
            ipl = BitmapConverter.ToIplImage(bitmap);

            return ipl;

        }

        // 참조와 네임스페이스만 추가되어 있다면 매우 간단하게 형식을 변환할 수 있습니다.
        // 비트맵 확장자를 사용하기 위해서는 드로잉 네임스페이스를 추가해야 합니다.
        // 물론 비트맵 확장자를 사용하지 않고 바로 To Bitmap을 사용하여 변환에 출력하셔도됩니다.
        // 하지만 이미지를 출력하지 않고 가상공간에서 데이터를 처리하거나 변수 형태로 저장할 경우 비트맵 확장자를 사용하셔야 합니다.
        // 각각 형식이 다르므로 형식에 맞게 변환할 때 사용하시면 됩니다.

        public void Dispose()
        {
            if (ipl != null) Cv.ReleaseImage(ipl);
        }
    }
}


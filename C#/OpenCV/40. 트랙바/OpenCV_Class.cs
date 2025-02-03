using System;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        IplImage bin;

        public IplImage Binary(IplImage src, int threshold)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.BgrToGray);
            Cv.Threshold(bin, bin, threshold, 255, ThresholdType.Binary);

            return bin;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
        }
    }
}

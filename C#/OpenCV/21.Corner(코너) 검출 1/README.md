# 코너 검출 1

> 영상이나 이미지에서 모서리(코너)를 검출하기 위해 사용합니다.
- Good Feature to Track 방법
- Harris Corner Detector 방법

> 그레이스케일 이미지 또는 이진화 이미지를 계산이미지로 사용
- 단일 채널 이미지를 사용하여 계산
- new IplImage(src.Size, BitDepth.U8, 1);

## Good Feature to Track 방법

> 퀄리티 레벨 ( qualityLevel )

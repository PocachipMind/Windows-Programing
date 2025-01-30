# 코너 검출 1

> 영상이나 이미지에서 모서리(코너)를 검출하기 위해 사용합니다.
- Good Feature to Track 방법
- Harris Corner Detector 방법

> 그레이스케일 이미지 또는 이진화 이미지를 계산이미지로 사용
- 단일 채널 이미지를 사용하여 계산
- new IplImage(src.Size, BitDepth.U8, 1);

## Good Feature to Track 방법

> 퀄리티 레벨 ( qualityLevel )
- 가장 좋은 코너 측정 값에 퀄리티 레벨 수치를 곱한 값 보다 낮은 값이면 그 코너들은 무시
- 즉 가장 우수한 코너점을 기준으로 잡아 다른 코너점들이 코너가 맞는지를 판단합니다.

> 최소 거리 ( minDistance )
- 검출된 코너들의 최소거리
- 최소 거리 이상의 값만 검출
- 검출된 코너가 측정값이 높더라도 너무 가까이 있으면 하나의 코너로 볼 수가 있습니다. 여기서 최소거리를 두어 어느정도 거리가 벌어져 있어야 코너로 간주합니다.

### 퀄리티 레벨과 최소 거리 둘 다 실수형(Double)을 사용

## Find Corner SubPix 함수
> Find Corner SubPix 함수
- 검출된 코너들의 정확한 위치를 찾기 위해 반복
Find Corner SubPix 함수는 총 3개의 인수에 의해 검출이 결정됩니다.
- win : 검출 영역
- zeroZone : 검출 제외 영역
- Criteria : 코너 정밀화 반복 작업

![image](https://github.com/user-attachments/assets/484bc07c-fc22-4082-a838-9f2f3563a825)

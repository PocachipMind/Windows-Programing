# 결과 저장
결과저장은 OpenCV를 통하여 영상처리한 결과물을 파일로 내보내어 확인할 수 있습니다.

- 함수나 메소드가 적용된 결과 이미지 또는 동영상 저장

메인폼을 캡쳐하거나 화면 녹화하는 방법은 정확한 결과를 저장할 수 없습니다.

OpenCV에 내장된 함수를 이용하여 지정된 크기로 저장할 수 있으며 동영상의 경우 프레임 수를 설정하여 기록할 수 있습니다.

### 이미지 저장
1. 단일 캡쳐 : 저장과 동일한 역할 ( 버튼을 통하여 캡쳐할 경우 마지막 클릭으로 해당 결과가 저장됩니다. )
2. 다중 캡쳐 : 다른 이름으로 저장과 동일한 역할 ( 버튼을 통하여 캡쳐할 경우 연속해서 누른다면 모두 저장됩니다. )

### 동영상 저장
1. 단일 녹화 : 저장과 동일한 역할 ( 버튼을 통하여 캡쳐할 경우 마지막 클릭으로 해당 결과가 저장됩니다. )
2. 다중 녹화 : 다른 이름으로 저장과 동일한 역할 ( 버튼을 통하여 캡쳐할 경우 연속해서 누른다면 모두 저장됩니다. )

여기서 다중으로 저장하는 방법은 저장하는 파일 명이 겹치지 않도록 현재 시간을 제목으로 사용하는 방법

연속적으로 클릭하였다 해도 그때의 시간은 동일할 수 없으므로 모두 저장하게 됩니다

동영상의 경우 프레임이 존재하므로 타이머 컨트롤을 이용하여 동영상을 저장합니다.

타이머 컨트롤을 사용하지 않을 경우 While 문과 같은 반복문을 이용하여 저장할 수 있습니다.

![image](https://github.com/user-attachments/assets/3922aaaa-31ba-44cd-9ae6-da33e3d67d4d)

## 이미지/동영상 저장 함수 인수

- 이미지
  1) 경로 + 파일이름 + 확장자
  2) 저장할 이미지
     
![image](https://github.com/user-attachments/assets/4fd8d0c6-110e-41e7-9490-7c5f24b234d0)


## 코드
코드내에서 타이머가 실행될 때마다 그동안의 프레임을 저장하게 되는데 저장하는 간격이 넓을수록 프레임이 한 번에 저장되어 영상 속도가 빨라집니다.

# 원 검출
원검출은 영상이나 이미지에서 원을 찾기위해서 사용합니다.
- 직선 검출과 동일하게 단일 채널 이미지에서 HoughCirrcles 함수를 사용해 원 검출
1) 그레이 스케일 검출
2) 블러 적용
3) 허프 서클 적용
이진화 처리 이외에도 그레이스케일이나 블러를 적용하여 검출 가능.

## 허프 서클
허프 변환 알고리즘을 이용하여 원을 검출하게 되는데 허프 변환 특징점은 파라미터를 공간으로 웹핑하여 검출합니다.

원의 방정식 : ( x - a )² + ( y - b )² = r² 

a = x 의 중심점 , b = y의 중심점 
- 파라미터는 ( a, b, r )
- 원의 중심점은 ( a, b )

위 방정식에서 원의 중심점은 a, b 이며 파라미터는 a,b,r이 됩니다.

1) 2차원 공간 (x,y)에서 3차원 공간 ( a,b,r )로 변환합니다.
2) 각 가장자리에 대하여 ( a,b,r )을 계산하고 3차원 히스토그램으로 원을 판단합니다.

 차원 히스토그램에서 도수가 높은 a,b,r을 선택하게 됩니다.

이 방법은 이미지에서 가장 긴 변의 길이가 n이라면 n³ 바이트의 메모리를 필요로합니다.

그렇기 때문에 메모리의 문제 해결을 위해 2차원 방식을 사용합니다. 이 방식을 이용해 원을 검출하게 됩니다.
![image](https://github.com/user-attachments/assets/0b376a73-286a-4db3-97ae-e515487b0dbc)



메모리 문제를 해결하기 위해서 2단계로 나누어 계산합니다.

1) 첫 번째 가장자리에서 그라디언트 방법을 이용하여 원의 중심점을 ( a,b )에 대한 2차원 히스토그램으로 선정합니다.
2) 선정된 중심점 ( a,b )와 가장자리의 좌표를 원의 방정식을 대입해 반지름 r를 1차원 히스토그램으로 판단합니다.

즉 히스토그램에 대하여 필요한 메모리가 줄어들게 되어 이미지에서 가장 긴 변의 길이가 n이라면 n² + n 바이트의 메모리가 필요하게 됩니다.

## 허프 서클 함수의 주요 파라미터
- 계산 방법 : Gradient 방법만 지원. 허프서클을 사용할 때 계산 방법의 인수는 그라디언트로 고정적입니다.
  
- dp : 입력된 이미지의 최대크기에 대한 축소율
  
  1 > 입력된 이미지와 동일한 크기
  
  2 > 입력된 이미지의 절반 크기
  
  일반적으로 1을 입력하여 입력된 이미지와 동일한 크기로 계산합니다.
  
- 최소 거리 : 검출된 원과 원 사이의 최소한의 거리

  이 거리보다 더 멀리 있어야 원으로 검출합니다.

- Edge 임계값 : Canny Edge의 상위 임계값

  하위 임계값은 상위 임계값의 절반으로 자동 할당

  캐니 엣지는 주요한 인수로 임계값을 2개 사용하는데 여기서 하위 임계값은 상위 임계값의 절반으로 자동 할당됩니다.

- 중심 임계값 : 1차원 히스토그램의 임계값

- 최소 반지름 : 검출된 원의 최소 반지름

  0 = 최소 반지름 고려하지 않음

- 최대 반지름 : 검출된 원의 최대 반지름

  0 = 최대 반지름 고려하지 않음


  

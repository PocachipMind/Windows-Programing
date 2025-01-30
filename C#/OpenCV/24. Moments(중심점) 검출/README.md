# Moments(중심점) 검출

중심점 검출, 즉 무게 중심.

## 중심점 검출
영상이나 이미지에서 중심점을 찾기 위해서 사용합니다.

- 점들이 포함되어있는 배열에서 Moments 함수를 사용하여 중심점을 검출

- ApproxPoly을 응용하여 사용.

코너점들을 모두 계산하여 중심점을 찾아줌으로 비대칭한 도형이나 모양이 특이한 경우에도 더 효과적으로 중심점을 검출해줍니다.


![image](https://github.com/user-attachments/assets/984f0eeb-1ece-472d-9072-1902cb4ea1d8)


모멘트 함수는 총 3가지의 모멘트를 얻을 수 있습니다.

1. 공간 모멘트, 2. 중심 모멘트, 3. 정규화된 중심 모멘트

![image](https://github.com/user-attachments/assets/2930dd01-8fb7-4e54-85ba-c7644a27e22c)

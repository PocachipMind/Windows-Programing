# 블록껍질 검출
외곽점 검출, 즉 블록껍질 검출.

## 외곽점 검출
외곽점 검출은 영상이나 이미지에서 최외곽점들을 검출하기 위해서 사용합니다.

- 점들이 포함 되어있는 배열에서 ConvexHull2 함수를 사용해 최외곽점들을 검출

1) FindContours 을 사용하여 컨투어(윤곽선) 검출
2) ApproxPoly 을 사용하여 코너점 검출
3) ConvexHull2 을 사용하여 최외곽점 검출

![image](https://github.com/user-attachments/assets/a6e97bd4-5570-40d9-b465-b1fe6a58f7eb)

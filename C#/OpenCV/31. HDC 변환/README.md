# HDC 변환

OpenCVSharp에서 지원하는 Cv.PutText()는 산세리프 글꼴과 scale로 글자 크기 제어

단점

1) 영문 이외의 언어는 지원하지 않음. 특수문자 또한 지원되지 않음.
2) 특정 글꼴 사용 불가. 글꼴 설정시 폰트페이스에 담겨있는 산세리프 글꼴과 세리플 글꼴만을 선택적으로 사용이 가능합니다.
3) 글자 크기 설정의 어려움. 글자의 크기를 설정할 때 h스케일과 v스케일의 값을 사용하여 글자의 크기를 설정합니다. 스케일 값이므로 직관적으로 크기를 설정하는 것이 매우 어렵습니다.
4) 문자의 부착 위치가 좌측 하단이며 글자의 크기에 따라 표시되는 위치가 계속 변화합니다. 만약 0,0 위치인 좌측 상단에 텍스트를 삽입시 이미지 밖에 글자가 형성되어 이미지에는 보이지 않습니다. 즉 좌측 상단 모서리에 글자를 표시하는 것은 어려운 일입니다.


Cv.PutText()를 이용하여 "안녕하세요. ABC"를 입력할 경우. 문자 한 글자마다 "??" 처리.

이 문제점들을 이전 강좌에서 배운 형식 변환이나 hdc 변환을 이용하여 해결할 수 있습니다.

## HDC 변환

- HDC : Handle to Device Context 의 약자이며
- C# Graphics와 관련된 장치 컨텍스트에 대한 핸들을 가져와 작업
간단하게 C# 픽쳐박스에 문자열이나 도형을 그릴 때 사용하는 방법을 가져와 이용하게 됩니다.

그래픽스의 장점
1) C#에서 지원하는 모든 언어 사용 가능. 한국어, 중국어, 일본어 등 다국적 언어 사용 가능, 특수문자 사용 가능
2) 모든 글꼴 사용 가능. 설치되어있는 글꼴은 모두 사용 가능.
3) 글자 크기 설정이 간편 ( 워드나 한글 등에서 글꼴의 크기를 설정할때 사용하는 pt 형식으로 크기 설정 )
4) 문자의 위치가 좌측 상단이며 글자 크기에 따라 위치가 변하지 않음.

# WeatherApp

WPF를 통해 WeatherApp를 구현합니다.

- MVVM 패턴에 맞게 설계, API 사용, Command / 데이터바인딩 / Converter 구현 방법 학습

- ![image](https://github.com/user-attachments/assets/b7c30b95-4695-4314-a20b-225077621384)

# Converter

1. KelvinToCelsiusConverter 기능
   - 켈빈 온도를 섭씨(Celsius) 온도로 변환하여 표시하기 위한 Value converter
   - 섭씨 = 켈빈 - 273.15
---------------------------------
- IValueConverter를 구현한 KelvinToCelsiusConverter 클래스 구현
-  WeatherWindow의 온도 표시 부분에 구현한 Converter를 사용하여 온도 값이 변환되어 화면에 표시되도록 구현
  - ℃는 특수문자를 스트링으로 입력하여 구현 가능함
  - ConvertBack에는 특별한 로직 없이 빈 문자열을 반환하도록 함

![image](https://github.com/user-attachments/assets/bedfc7eb-e700-4c00-93c6-42dfcf17b2c0)


# Custom TextBlock과 의존속성
- TempColorTextBlock 추가하고 기능 구현하기
  - 온도에 따라 텍스트 색상을 달리하는 기능 있는 TempColorTextBlock을 TextBlock을 상속하여 구현.
    1. TempProperty 의존속성을 구현하여 바인딩이 가능하도록 하고, 해당속성이 바뀌면 Foreground 색상이 바뀌는 로직을 콜백함수로 등록.
       - 섭씨 20도를 기준으로, 이보다 온도가 높을 시 빨간색, 낮을 시 파란색으로텍스트를 표시.
    2. 기존 온도를 보여주는 TextBlock 대신, 새로 구현한TempColorTextBlock을 WeatherWindow에서 사용하고, 구현한의존속성과 Text property를 바인딩하여 예제 프로그램과 동일하게동작하도록 구현.
   
  ![image](https://github.com/user-attachments/assets/633e9a19-0193-443d-a545-1b1c7cbcbc18)


# 커맨드를 사용한 기능 구현
- 상단 TextBox를 사용한 도시 추가 기능 구현
  - London, Paris, Jeonju, Seoul이 ListView를 통해 표시되고 있다.
  - Refresh 버튼 아래 TextBox에 사용자가 도시를 입력하고 Refresh 버튼을 누르면, 아래와 같은 동작이 일어나도록 구현.
    1. TextBox가 비어있는 경우, 가장 최근에 정보를 얻어온 도시의 날씨 정보를 갱신하여 표시
    2. TextBox에 이미 List(VM.Cites)에 있는것과 같은 이름의 도시가 입력되었을 경우, 해당 도시의 정보를 Update하고 표시
    3. TextBox에 List에 없는 도시 이름이 입력되었을 경우(ex, Toronto), 그 도시를 List에 추가하고, 해당 도시의 정보를 Update하고 표시
       - 단, 도시 이름 잘못 입력 등 케이스에 대한 예외 처리는 없음
         
![image](https://github.com/user-attachments/assets/d724ebe9-611f-41f9-80d5-b4956e204e76)


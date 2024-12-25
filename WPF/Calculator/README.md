# Calculator

WPF를 통해 Calculator를 구현합니다.

- Resorce를 통한 효율적인 디자인 코드 구현법, 콘텐츠 컨트롤, 레이아웃 구성법 학습.

![image](https://github.com/user-attachments/assets/3283da07-448e-46bc-ba98-f6c6452a9365)

![image](https://github.com/user-attachments/assets/d9dcf382-a323-4700-8706-b7b6f524b499)

# Excutables 1. 버튼
1. Del 버튼
   - ResultLabel의 텍스트를 뒤에서부터 하나씩 지울 수 있는 기능
   - 텍스트가 한 글자일때 누르면, 0으로 초기화

2. Sqrt 버튼
   - ResultLabel의 텍스트의 제곱근을 계산하여 보여주는 버튼
   - ResultLabel의 텍스트가 음수라면 동작하지 않음

3. X^2 버튼
   - ResultLabel의 텍스트의 제곱을 계산하여 보여주는 버튼

4. 1/x 버튼
   - ResultLabel의 텍스트의 역수를 계산하여 보여주는 버튼

![image](https://github.com/user-attachments/assets/c4fb11f4-8ccf-4c16-96c6-b43d08d08b17)

# Excutables 2. 계산 History 및 계산식 창

1. 계산 History창(빨간색)
   - ListView를 사용
   - “=“ 버튼으로 계산을 수행하면, 계산 내용을 표시

2. 계산식 창(파란색)
   - ResultLabel 위에, 입력 정보를 옅은 회색으로 표시
   - 연산자가 눌리면, lastNumber와 현재 입력된 연산자를 표시
     
![image](https://github.com/user-attachments/assets/012a6676-16de-41f7-9c1e-7cd3cb322a04)

# Excutables 3. 연속 계산 기능

1. 연산자가 2개 이상 사용되는 계산이 가능하도록 프로그램 구현
   - 계산 History 및 계산식 창도 이에 맞게 구현
   - 중간 계산 결과를 화면에 보여주지 않음
     
![image](https://github.com/user-attachments/assets/64022d6a-035e-46c5-bb3b-bb883eab1e61)



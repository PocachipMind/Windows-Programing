# Winfrom
Winform을 활용한 동기, 비동기 파일 전송을 학습합니다.

![image](https://github.com/user-attachments/assets/6efea542-c50b-4e70-a3be-47bbf0c45102)

Source 를 통해 전송할 파일을 선택하고 Target 을 통해 어디로 전송할지 위치를 지정합니다.


Async Copy와 Sync Copy 버튼을 통해 전송하며, 중간에 Cancel 버튼을 눌러 비동기,동기간의 반응차이를 관찰합니다.

테스트는 동영상이나 큰 그림같이 시간이 오래걸리는것이 관찰하기 유리합니다.



Sync Copy 버튼의 경우 파일을 복사하고 있을때 Cancle 버튼을 눌러도 반응을 거의 못하는 반면, Async Copy 버튼은 잘 반응합니다.

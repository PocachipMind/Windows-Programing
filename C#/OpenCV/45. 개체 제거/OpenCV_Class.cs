using System;
using OpenCvSharp;

namespace OpenCV_Practice
{
    internal class OpenCV_Class : IDisposable
    {
        // 마스크를 적용하는 방법으로 이전 강좌에서 배운 마우스 콜백 함수를 이용하여 마스크를 생성하도록 하겠습니다.
        IplImage inpaint;

        public IplImage InpaintImage(IplImage src)
        {
            inpaint = new IplImage(src.Size, BitDepth.U8, 3);
            
            // 계산 이미지로 사용할 Paint 변수를 생성하고 원본 이미지를 복제합니다.
            IplImage paint = src.Clone();

            // 마스크 변수 생성, 채널은 1로 설정
            IplImage mask = new IplImage(src.Size, BitDepth.U8, 1);

            // 이제 윈도우를 생성하여 이 윈도우에서 마스크를 직접 표시하여 생성하도록 하겠습니다.
            // 그림판이나 포토샾에서 브러쉬 도구처럼 윈도우 창위에 그려보도록 하겠습니다.
            CvWindow win_Paint = new CvWindow("Paint", WindowMode.AutoSize, paint);

            // 마우스의 이전 좌표를 저장할 prevPt를 생성하고 초기 위치를 -1,-1로 사용하도록 합니다.
            CvPoint prevPt = new CvPoint(-1, -1);

            // 이제 이전 강좌에서 배운 마우스 콜백 함수를 적용하도록 하겠습니다.
            // 여기서 new 키워드가 아닌 delegate를 사용하여 바로 적용합니다.
            // 매개변수의 순서는 동일하게 < 마우스 이벤트, x좌표, y좌표, 플래그 > 입니다.
            win_Paint.OnMouseCallback += delegate (MouseEvent eve, int x, int y, MouseEvent flag)
            {
                // 중괄호 구문으로 코드를 구성함으로 마지막에 세미콜론을 포함합니다.
                
                // 브러쉬 효과를 구현하기 위해서 마우스의 좌측 버튼이 클릭되었을 때
                if(eve == MouseEvent.LButtonDown)
                {
                    // 이전 마우스의 지점을 현재 XY 좌표로 갱신합니다.
                    prevPt = new CvPoint(x, y);
                }
                else if (eve == MouseEvent.LButtonUp || (flag & MouseEvent.FlagLButton) == 0) // else if 구문으로 마우스 좌측 버튼을 떼거나 마우스 왼쪽 버튼을 누르고 드래그하지 않으면 
                {
                    // 이전 마우스 포인터를 다시 -1, -1로 초기화합니다.
                    prevPt = new CvPoint(-1, -1);
                }
                // 다시 else if 구문으로 마우스를 움직이면서 마우스 좌측 버튼을 누르고 드래그하는 상태라면 드로우 라인을 통하여 브러쉬 효과를 구현합니다.
                else if ( eve == MouseEvent.MouseMove && (flag & MouseEvent.FlagLButton) != 0)
                {
                    // 현재 위치를 기억할 pt 변수를 생성하고 현재 위치를 사용합니다.
                    CvPoint pt = new CvPoint(x, y);

                    // DrawLine을 사용하여 마스크와 페인트에 표시하도록 하겠습니다.
                    Cv.DrawLine(mask, prevPt, pt, CvColor.White, 5, LineType.AntiAlias, 0); // 색상은 하얀색, 크기는 5, Anti Alias를 적용하여 부드럽게 합니다.
                    
                    // 마스크는 계산을 위해 사용하며 페인트 위에 그리는 것은 우리가 눈으로 보고 마스크를 편하게 생성하기 위함입니다.
                    Cv.DrawLine(paint, prevPt, pt, CvColor.White, 5, LineType.AntiAlias, 0);

                    // 이제 이전 지점을 현재 위치로 갱신합니다.
                    prevPt = pt;
                    // 마지막으로 윈도우 창에 페인트 변수를 표시하도록 하겠습니다.
                    win_Paint.ShowImage(paint);

                    // 코드를 모두 구성하기 전에 마우스 콜백 이벤트를 확인하고 진행하겠습니다.
                }
            };

            // 이번에는 윈도우 창에서 KeyEvent를 적용해 보겠습니다.
            // KeyEvent란 윈도우 창에서 특정 키를 눌렀을 경우 해당 이벤트가 실행됩니다.
            bool repeat = true;
            while (repeat)
            {
                // 스위치 구믄을 이용하여 키 이벤트 사용해보기
                // CvWindow.WaitKey()를 사용하여 어떤 키가 입력되는지 판단합니다.
                switch(CvWindow.WaitKey())
                {
                    // 먼저 영문 r 키가 입력될 경우 이미지를 처음 상태로 초기화하는 이벤트로 사용
                    case 'r':
                        mask.SetZero(); // 마스크값 초기화
                        Cv.Copy(src, paint); // paint 변수를 src로 변경
                        win_Paint.ShowImage(paint);// 그후 표시되는 이미지 다시 띄움
                        break;
                    case '\r': // 엔터키가 입력되었을 때
                        CvWindow win_Inpaint = new CvWindow("Inpainted", WindowMode.AutoSize);// 새로운 윈도우 창을 생성하여 이곳의 결과를 표시하도록 하겠습니다.
                        // 이제 개체 제거 함수인 inpaint 함수를 사용해봅니다.
                        Cv.Inpaint(paint, mask, inpaint, 3, InpaintMethod.NS); // Cv.Inpaint이며 인수의 순서는 < 계산 이미지, 마스크, 결과 이미지, 반지름, 알고리즘 > 입니다. 
                        // 반지름은 3으로 알고리즘은 나비의 스토크스 방식을 사용하겠습니다.
                        win_Inpaint.ShowImage(inpaint);// 이후 결과용 윈도우 창에 띄웁니다.
                        break;
                    case (char)27: // ESC 키를 눌렀을때 메인 폼으로 돌아가도록. ESC는 ASCII 값으로 27을 의미합니다.
                        CvWindow.DestroyAllWindows(); // 모든 윈도우창 닫기
                        repeat = false;
                        break;

                }
            }

            // 결과 이미지를 반환하여 최종적으로 적용된이미지를 메인폼으로 반환할 예정이므로 인페인트를 반환값으로 사용합니다
            return inpaint;

        }


        // 이번 강좌에서는 인페인팅 함수와 키 이벤트에 대해 배웠습니다.
        // 인페인트 함수는 마스크를 얼마나 정확하게 제거할 부분에 적용하느냐가 가장 중요합니다.
        // 앞서 배운 강좌들을 응용하여 불필요한 부분을 검출하여 마스크로 사용하거나 마우스 이벤트와 키이벤트를 활용하여 사용자들이 직접 개체를 제거할 수 있습니다.
        // 키는 ASCII 코드 값을 이용하며 키보드의 Shift나 Insert 키 등도 활용할 수 있습니다. 



        public void Dispose()
        {
            if (inpaint != null) Cv.ReleaseImage(inpaint);
        }
    }
}

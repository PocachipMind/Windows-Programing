# 윤곽선 검출

윤곽선 검출은 영상이나 이미지에서 윤곽선, 즉 컨투어를 검출하기위해 사용합니다.

이진화된 이미지에서 검출을 실행하며 오브젝트에서 내곽과 외곽의 윤곽선을 검출.

- 영상이나 이미지에서 윤곽선을 검출하기 위해서 사용
- 이진화 이미지를 이용하여 검출
- 내곽과 외곽의 윤곽선 검출
- 이미지의 최외곽의 내무 윤곽까지 검출
- 윤곽선 검출 함수는 코너 검출 함수에도 사용

## 검색 방법 ( ContourRetrieval.* )

- ContourRetrieval.CComp : 모든 윤곽선을 검색하여 두 레벨의 계층 구조로 구성합니다. 최상위 레벨은 구성 요소의 외곽(외부) 경계이고, 두 번째 레벨은 내곽(홀)의 경계입니다.

- ContourRetrieval.External : 외곽 윤곽선만 검출합니다.

- ContourRetrieval.List : 모든 윤곽선을 검출하여 list에 넣습니다.

- ContourRetrieval.Tree : 모든 윤곽선을 검출하여 Tree계층 구조로 만듭니다.

### 가장 많이 사용되는 방법은 List 사용 방법. 특별한 경우가 아니라면 List 방법을 사용하시면 됩니다.

## 근사화 방법 ( ContourChain.* )

- ContourChain.ApproxNone : 윤곽점들의 모든 점을 반환합니다.

- ContourChain.ApproxSimple : 윤곽점들 단순화 수평, 수직 및 대각선 요소를 압축하고 끝점만 남겨 둡니다.

- ContourChain.Code : 프리먼 체인 코드에서의 윤곽선으로 적용합니다.

- ContourChain.ApproxTC89KCOS, ContourChain.ApproxTC89L1 : Teh-Chin 알고리즘 적용합니다.

- ContourChain.LinkRuns : 하나의 수평 세그먼트를 연결하여 완전히 다른 윤곽선 검색 알고리즘을 사용합니다.

### 가장 많이 사용되는 방법은 Approx 방법

### 윤곽선 검출함수는 크게 두가지가 있습니다. FindContour, ContourScanner

# UnityWebRequestPractice

## 문제

- Google 이미지 검색에서 나온 이미지들을 긁어와서 화면에 생생해야 한다.
- 유니티로 검색창을 만들고 검색창에서 단어를 입력 받아 해당 이미지만을 표시할 것
- 단어 입력 후 검색을 하면 이미지가 화면 아래에서 위로 제 각각의 거리에서 위로 올라가도록 할 것

## 요구사항 분석
- InputField를 통해 원하는 단어를 입력 받음
- 입력 받은 단어와 주소를 합침
- 위 주소에서 이미지들을 요청
- 요청한 이미지들을 다운로드
- 다운로드한 이미지들를 토대로 Image Sprite 생성
- 이미지 객체를 생성(Imagte Sprite, random position) 
- 일정 속도로 위로 상승

## 클래스 식별
- ImageWebRequest
   - InputField에 입력된 단어로 Crawling을 실행 및 이미지 생성
   - 원하는 주소에서 Crawling을 실행하는 최종 인테페이스 제공
- ImageHandler
   - 매 프레임 마다 일정 속도로 하늘로 상승

## Sequence 설계
- 이미지 이름 입력
   - InputField -> *입력* -> ImageName
- 검색 버튼 클릭
   - Button -> *click* -> ImageWebRequest
   - ImageWebRequest -> *Request* -> ImageWebRequest
   - ImageWebRequest -> *GetImageURLs* -> ImageWebRequest
   - ImageWebRequest -> *RequestImage* -> ImageWebRequest
   - ImageWebRequest -> *SpawnImage* -> ImageWebRequest

# UnityWebRequestPractice

## 문제

- https://search.naver.com/search.naver?where=image&sm=tab_jum&query=\
  위 주소 뒤에 검색하고 싶은 단어를 추가해서 브라우저 주소창에 넣으면 해당 단어와 일치하는 이미지가 나옴
- 유니티로 검색창을 만들고 검색창에서 단어를 입력 받아 위 주소의 결과 중 이미지만을 표시할 것
- 단어 입력 후 검색을 하면 이미지가 화면 아래에서 위로 제 각각의 거리에서 위로 올라가도록 할 것

## 요구사항 분석
1. InputField를 통해 원하는 단어를 입력 받음
2. 입력 받은 단어와 주소를 합침
3. 위 주소에서 이미지들을 요청
4. 요청한 이미지들을 다운로드
5. 다운로드한 이미지들를 토대로 Image Sprite 생성
6. 이미지 객체를 생성(Imagte Sprite, random position) 
7. 일정 속도로 위로 상승
8. 일정 시간 후 파괴

## 클래스 식별
- Crawling Manager
   - Keyword를 통해 원하는 정보를 Crawling
   - 원하는 주소에서 Crawling을 실행하는 최종 인테페이스 제공
   - Crawling을 통해 img scr URL 정보들을 저장
   - img scr url에서 sprite 생성해서 List로 저장
   - Crawling을 통해 얻은 데이터들을 보관하는 속성 값 제공
- Image Create Manager
   - Crawling Manager에 저장된 이미지 데이터들을 가지고 와서 랜덤으로 이미지들을 생성
- Image Instance(Prefab)
   - Image 컴포넌트를 가지고 있음
   - 생성자를 통해 Image 컴포넌트의 sprite 초기화
   - 매 프레임 마다 일정 속도로 하늘로 상승
   - 일정 시간이 흐른 후 자동 제거

## 절차 설계(Process Design)
- 이미지 이름을 입력하고 검색 버튼 클릭
- Crawling을 실행
- Crawling Manager에 저장된 이미지 데이터들을 가지고 와서 랜덤으로 이미지들을 생성

## 사용자 인터페이스 설계
- 이미지 이름 입력
   - InputField -> *입력* -> ImageName
- 검색 버튼 클릭
   - Button -> *click* -> ImageCreateManager                     // 버튼 클릭
   - ImageCreateManager -> *CrawlingDate* -> CrawlingManager     // 어떤 종류의 Crawling 확인
   - CrawlingManager -> *CrawlingImage* -> CrawlingManager       // Image Crawling 호출
   - CrawlingManager -> *SaveSrc* -> CrawlingManager             // HTML에서 scr url 저장
   - CrawlingManager -> *SaveSprite* -> CrawlingManager          // scr url에서 sprite 생성 후 저장
   - CrawlingManager -> *LoadSprite* -> ImageCreateManager       // CrawlingManager에 저장된 sprite를 Load
   - ImageCreateManager -> *CreateImageInstane* -> ImageInstance // ImageInstance를 생성
   - ImageInstance -> *ImageInstance* -> ImageInstance           // ImageInstance 생성할 때 초기화
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

## 각 요구사항을 토대로 설계(Collaboration Design)
1. InputField를 통해 원하는 단어를 입력 받음
   - InputField -> `텍스트 입력` -> Text
2. 입력 받은 단어와 주소를 합침
   - Text -> `+` -> ImageController
3. 위 주소에서 이미지들을 요청
   - SendButton -> `OnRequest` -> ImageController 
   - SendButton -> `RequestImage` -> ImageController 
4. 요청한 이미지들을 다운로드
   - ImageController -> `DownloadImage` -> ImageController
5. 다운로드한 이미지를 토대로 Imgae Sprite 생성
   - ImageController -> `Sprite.Create` -> Sprite
6. 이미지 객체를 생성
   - Sprite -> `ImageObject` -> ImageController
   - ImageController -> `Instantiate` -> ImageObject
7. 일정 속도로 위로 상승
   - ImageObject -> `Update` -> ImageObject
   - ImageObject -> `RiseImage` -> ImageObject
8. 일정 시간 후 파괴
   - ImageObject -> `Update` -> ImageObject
   - ImageObject -> `SelfDestroy` -> ImageObject

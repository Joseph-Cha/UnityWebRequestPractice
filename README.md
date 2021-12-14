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

## 인사이트
### 1. UnityWebRequest를 구현하는 다양한 방법들에 대한 이해

1. Coroutine과 Callback 방식을 통해 WebRequest를 구현
   ``` C#
   private IEnumerator RequestByCoroutine(string url, Action<UnityWebRequest> callBack)
   {
      UnityWebRequest uwr = UnityWebRequest.Get(url);
      yield return uwr.SendWebRequest();

      while (!uwr.isDone)
         yield return null;
      
      if (uwr.isDone)
      {
         callBack.Invoke(uwr);
      }
   }
   ```
2. async, await을 통해 WebRequest 구현
   ``` C#
   private async void RequestByAsync(string url)
   {
      UnityWebRequest uwr = UnityWebRequest.Get(url);
      UnityWebRequestAsyncOperation ao = uwr.SendWebRequest();
      await ao;
      if (ao.isDone)
      {
         // 이후 작업 진행
      }
   }
   ```
   단, `UnityWebRequestAsyncOperation`을 await으로 받기 위해서 아래 소스가 필요
   ``` C#
   using System;
   using UnityEngine.Networking;
   using System.Runtime.CompilerServices;
   using UnityEngine;

   public struct UnityWebRequestAwaiter : INotifyCompletion
   {
      private UnityWebRequestAsyncOperation asyncOp;
      private Action continuation;

      public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
      {
         this.asyncOp = asyncOp;
         continuation = null;
      }

      public bool IsCompleted { get { return asyncOp.isDone; } }

      public void GetResult() { }

      public void OnCompleted(Action continuation)
      {
         this.continuation = continuation;
         asyncOp.completed += OnRequestCompleted;
      }

      private void OnRequestCompleted(AsyncOperation obj)
      {
         continuation?.Invoke();
      }
   }

   public static class ExtensionMethods
   {
      public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
      {
         return new UnityWebRequestAwaiter(asyncOp);
      }
   }
   ```
3. `AsyncOperation`의 completed callback 방식
   ``` C#
   public void RequestByAsyncOperation(string name)
   {
      string requestUrl = string.Format(Url, imageName);
      UnityWebRequest uwr = UnityWebRequest.Get(requestUrl);
      uwr.SendWebRequest().completed += ao => 
      {
         if (uwr.isDone)
         {
               // 이후 작업
         }
         uwr.Dispose();
      };
   }
   ```

### 2. AsyncOperation은 어떻게 비동기로 작업을 처리해줄까?

![image](https://user-images.githubusercontent.com/75019048/145919443-ac45cbf4-125a-44fa-98a5-ade36f9e4750.png)

AsyncOperation.completed가 invoke 될 때 Call Stack을 보면 Main Thread에서 해당 작업을 처리하는 것을 볼 수 있다.

즉, AsyncOperation은 싱글 쓰레드 비동기로 해당 작업을 처리하는 것을 알 수 있다.

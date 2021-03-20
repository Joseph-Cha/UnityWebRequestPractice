using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
 using Newtonsoft.Json;
using System;

public class ImageController : MonoBehaviour
{
    [SerializeField]
    private InputField InputField = null;

    [SerializeField]
    private Image image = null;
    private string _imageName
    {
        get
        {
            return InputField.text; 
        }
    }

    // private string _serverURL
    // {
    //     get
    //     {
    //         return "https://www.google.com/search?q=%EA%B0%95%EC%9D%B4%EC%A7%80&source=lnms&tbm=isch&sa=X&ved=2ahUKEwiXifTovL7vAhWEHHAKHTIDBCIQ_AUoAXoECAQQAw&biw=1792&bih=975";
    //     }
    // }
    // private string _serverURL2
    // {
    //     get
    //     {
    //         return "&source=lnms&tbm=isch&sa=X&ved=2ahUKEwiXifTovL7vAhWEHHAKHTIDBCIQ_AUoAXoECAQQAw&biw=1792&bih=975";
    //     }
    // }
    
    // private void Start()
    // {   
        
    //    // new ImageObject(image);
    // }

    public void OnRequest()
    {
        StartCoroutine(RequestImage());
    }

    
    private IEnumerator RequestImage()
    {
        // string requestURL = _serverURL + _imageName + _serverURL2;  
        // using (UnityWebRequest request = UnityWebRequest.Get(requestURL))
        // {
        //     if(!request.isNetworkError && !request.isHttpError)
        //     {
        //         yield return request.SendWebRequest();
        //         string[] pages = requestURL.Split('/');
        //         int page = pages.Length - 1;
        //         if(request.isDone)
        //         {
        //             Debug.Log(pages[page] + ":\nReceived: " + request.downloadHandler.text);                    
        //         }
        //     }
        // }
        
        string requestURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSi9kL1x8wgF0xKfd3dHbSHYCrZ-uMDuCxsyL5ss6UdXMM2UvzBk9A7J2EF4Uo&amp;s";

        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(requestURL))
        {
            if(!request.isNetworkError && !request.isHttpError)
            {
                yield return request.SendWebRequest();
                if(request.isDone)
                {
                    Debug.Log($"request.result : {request.result}");
                    DownloadImage(request);
                }
            }
        }
    }

    private void DownloadImage(UnityWebRequest request)
    {
        // 이미지 다운로드 
        Texture2D texture = null;
        try
        {
            texture = DownloadHandlerTexture.GetContent(request);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }


        // 다운로드한 이미지를 토대로 이미지 Sprite 생성
        Sprite sprite = null;
        try
        {
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));   
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        image.sprite = sprite;
        image.SetNativeSize();
    }
}

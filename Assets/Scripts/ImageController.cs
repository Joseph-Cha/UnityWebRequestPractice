using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
 using Newtonsoft.Json;


public class ImageController : MonoBehaviour
{
    [SerializeField]
    private InputField InputField;

    private string _imageName
    {
        get
        {
            return InputField.text; 
        }
    }

    private string _serverURL
    {
        get
        {
            return "https://search.naver.com/search.naver?where=image&sm=tab_jum&query=";
        }
    }

    public void OnRequest()
    {
        StartCoroutine(RequestImage());
    }

    private IEnumerator RequestImage()
    {
        string requestURL = _serverURL + _imageName;  

        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(requestURL))
        {
            if(!request.isNetworkError && !request.isHttpError)
            {
                yield return request.SendWebRequest();
                DownloadImage(request);
            }
        }
    }

    private void DownloadImage(UnityWebRequest request)
    {
        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        
    }




}

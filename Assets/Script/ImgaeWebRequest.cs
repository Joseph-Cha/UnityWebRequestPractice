using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImgaeWebRequest : MonoBehaviour
{
    public Transform Root;
    public string requestUrl { get; private set; } = "https://www.google.com/search?q=train&source=lnms&tbm=isch&sa=X&ved=2ahUKEwiOu72Votb0AhUisVYBHQRzB6UQ_AUoAXoECAEQAw&cshid=1639037441866438&biw=1812&bih=978&dpr=1";
    public void Request(string name)
    {
        // requestUrl += name; 
        UnityWebRequest uwr = UnityWebRequest.Get(requestUrl);
        uwr.SendWebRequest().completed += ao => 
        {
            if (uwr.isDone)
            {
                string html = uwr.downloadHandler.text; 
                var urls = GetImageURLs(html);
                
                foreach (string url in urls)
                {
                    if (url.Contains("https"))
                    {
                        RequestImage(url);
                    }
                }
            }
            uwr.Dispose();
        };
    }

    private IEnumerable<string> GetImageURLs(string html)
    {
        HtmlDocument htmlDoc = new HtmlDocument();
        List<string> urls = new List<string>();
        htmlDoc.LoadHtml(html);
        var nodes = htmlDoc.DocumentNode.Descendants("img");
        
        foreach (var node in nodes)
        {
            urls.Add(node.Attributes["src"].Value);
        }

        return urls;
    }

    private void RequestImage(string url)
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        uwr.SendWebRequest().completed += ao =>
        {
            if (ao.isDone)
            {
                Texture image = ((DownloadHandlerTexture)uwr.downloadHandler).texture;
                SpawnImage(image);
            }
            uwr.Dispose();
        };
    }

    private void SpawnImage(Texture image)
    {
        GameObject go = Manager.Resource.Instantiate("image", Root);
        RawImage rawImage = go.GetComponent<RawImage>();
        rawImage.texture = image;
        rawImage.SetNativeSize();
    }
}
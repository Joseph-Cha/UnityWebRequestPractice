using System.Collections.Generic;
using HtmlAgilityPack;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ImageWebRequest : MonoBehaviour
{
    public Transform Root;
    public TMP_InputField InputField;
    private const string Url = "https://www.google.com/search?q={0}&source=lnms&tbm=isch&sa=X&ved=2ahUKEwiOu72Votb0AhUisVYBHQRzB6UQ_AUoAXoECAEQAw&cshid=1639037441866438&biw=1812&bih=978&dpr=1";
    private string imageName => InputField.text;

    public void Request()
    {
        string requestUrl = string.Format(Url, imageName);
        UnityWebRequest uwr = UnityWebRequest.Get(requestUrl);
        uwr.SendWebRequest().completed += ao => 
        {
            if (ao.isDone)
            {
                string html = uwr.downloadHandler.text; 
                IEnumerable<string> urls = GetImageUrls(html);

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

    private IEnumerable<string> GetImageUrls(string html)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        IEnumerable<HtmlNode> nodes = htmlDoc.DocumentNode.Descendants("img");

        return nodes.Select(n => n.Attributes["src"].Value);
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
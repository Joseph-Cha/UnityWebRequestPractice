using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataTpye
{
    Image
}
public class CrawlingManager : MonoBehaviour
{
    public void CrawlingDate(string keyword, DataTpye dataType)
    {
        switch (dataType)
        {
            case DataTpye.Image:
                CrawlingImage(keyword);
                break;
            default:
                Debug.Log("There is no DataTpye. You can only Crawl Date which is Image");
                break;
        }
    }

    private void CrawlingImage(string keyword)
    {

    }
    private void SaveSrc()
    {

    }
    private void SaveSprite()
    {

    }

}

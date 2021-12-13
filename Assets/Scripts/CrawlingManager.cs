using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    Image
}
public class CrawlingManager
{
    public static void CrawlingDate(string keyword, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Image:
                CrawlingImage(keyword);
                break;
            default:
                Debug.Log("There is no DataTpye. You can only Crawl Date which is Image");
                break;
        }
    }

    private static void CrawlingImage(string keyword)
    {
        
    }

    private void SaveSrc()
    {

    }

    private void SaveSprite()
    {

    }

}

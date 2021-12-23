using System;
using System.Collections.Generic;
using System.Xml;

public class StockDataController
{
    private IList<StockData> StockDatas = new List<StockData>();

    public void ParseStockData(XmlNode node)
    {
        string data = node.Attributes["data"].Value;
        CreateStockData(data);
    }

    private void CreateStockData(string data)
    {
        string[] stockInfos = data.Split('|');
        DateTime date = DateTime.ParseExact(stockInfos[0], "yyyymmdd", null);
        int startPrice = int.Parse(stockInfos[1]);
        int maxPrice = int.Parse(stockInfos[2]);
        int minPrice = int.Parse(stockInfos[3]);
        int endPrice = int.Parse(stockInfos[4]);
        int volume = int.Parse(stockInfos[5]);

        StockData stockData = new StockData(date, startPrice, maxPrice, minPrice, endPrice, volume);

        AddStockData(stockData);
    }

    private void AddStockData(StockData data)
    {
        if (StockDatas.Contains(data) == false)
        {
            StockDatas.Add(data);
        }
    }

    public IList<StockData> GetStockDatas() => StockDatas;

    public int Count() => StockDatas.Count;
    
    public void RemoveStockDatas()
    {
        if (StockDatas != null)
        {
            StockDatas.Clear();
        }
    }
}
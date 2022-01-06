using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

public class StockProgram : MonoBehaviour
{
    public Setting Setting;
    public WindowGraph WindowGraph;
    private StockDataController stockDataController = new StockDataController();
 
    public void Request()
    {
        string url = string.Format(
            "https://fchart.stock.naver.com/sise.nhn?symbol={0}&timeframe=day&count={1}&requestType=0", 
            Setting.StockCode, Setting.ShowCount);

        UnityWebRequest uwr = UnityWebRequest.Get(url);
        uwr.SendWebRequest().completed += ao => 
        {
            if (ao.isDone)
            {
                string data = uwr.downloadHandler.text;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNode parentNode = doc.SelectSingleNode("protocol/chartdata");

                foreach (XmlNode node in parentNode.ChildNodes)
                {
                    stockDataController.ParseStockData(node);
                }

                Calculate();
            }
            uwr.Dispose();
        };
    }

    public void Calculate()
    {
        MyAccount myAccount = new MyAccount(Setting.Balance);
        IList<StockData> stocks = stockDataController.GetStockDatas();

        // 구매 조건
        // 전날 최고가 > 오늘 시작가 + (전날 최고가 - 전날 최저가) * 0.5 
        // => 오늘 시작가 +  (전날 최고가 - 전날 최저가) * 0.5 + 100으로 매수
        for (int i = 1; i < stockDataController.Count() - 1; i++)
        {
            int today = i;
            int yesterday = i - 1;
            int tomorrow = i + 1;

            if (stocks[today].MaxPrice > stocks[today].StartPrice + stocks[yesterday].Range * Setting.k)
            {
                int buyPrice = (int)(stocks[today].StartPrice + stocks[yesterday].Range * Setting.k + 100);
                myAccount.BuyStock(buyPrice);
            }

            myAccount.SellStock(stocks[tomorrow].StartPrice);
            WindowGraph.ShowGraph(myAccount.GetRevenue() / 1000, stocks[tomorrow].Date);
            Debug.Log($"{stocks[tomorrow].Date.ToString("yyyy/mm/dd")} 수익: {string.Format("{0:#,0}", myAccount.GetRevenue())}원");        }

        stockDataController.RemoveStockDatas();
    }
}
public class StockData
{
    public StockData(System.DateTime date, int startPrice, int maxPrice, int minPrice, int endPrice, int volume)
    {
        Date = date;
        StartPrice = startPrice;
        MaxPrice = maxPrice;
        MinPrice = minPrice;
        EndPrice = endPrice;
        Volume = volume;
    }

    public System.DateTime Date;
    public int StartPrice;
    public int MaxPrice;
    public int MinPrice;
    public int EndPrice;
    public int Volume;
    public int Range => MaxPrice - MinPrice;
}
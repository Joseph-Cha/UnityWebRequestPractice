using System;
using UnityEngine;

public class MyStock
{
    public int Price;
    public int Count;
    public int Value => Price * Count;
}

public class MyAccount
{
    public MyAccount(int balance = 10_000_000) => this.balance = balance;

    private int balance;
    private MyStock myStock;

    public void BuyStock(int price)
    {
        int count = GetQuantity(price);
        myStock = new MyStock() { Price = price, Count = count };
        balance -= myStock.Value;
    }

    public void SellStock(int price)
    {
        if (myStock != null)
        {
            balance += myStock.Count * price;
            myStock = null;
        }
    }

    public int GetRevenue() => balance - 10_000_000;

    private int GetQuantity(int price) => balance / price;
}
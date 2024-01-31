class Item : ITax
{
    public string Name { get; }
    public decimal Price { get; }
    public bool IsImported { get; }
    public bool IsExempt { get; }
    public int Quantity { get; set; }
    public decimal SalesTax { get; private set; }
    public decimal FinalPrice { get; private set; }

    public Item(string name, decimal price, bool isImported, bool isExempt)
    {
        Name = name;
        Price = price;
        IsImported = isImported;
        IsExempt = isExempt;
        Quantity = 1;
    }

    public void CalculateTax()
    {
        decimal taxRate = 0.0m;

        if (!IsExempt)
            taxRate += 0.1m;

        if (IsImported)
            taxRate += 0.05m;

        SalesTax = Math.Ceiling((Price * taxRate) / 0.05m) * 0.05m;

        FinalPrice = (Price + SalesTax) * Quantity;
    }
}
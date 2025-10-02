using System;

namespace CleanArchitecture.Domain.Constants;

public class DomainConstants
{
    public const string DefaultCurrency = "USD";
    public const int MaxProductNameLength = 200;
    public const int MaxDescriptionLength = 1000;
    public const int MinStockQuantity = 0;
    public const int MaxStockQuantity = 999999;
}

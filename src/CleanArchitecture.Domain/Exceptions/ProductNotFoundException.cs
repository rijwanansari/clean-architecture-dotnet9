using System;

namespace CleanArchitecture.Domain.Exceptions;

public class ProductNotFoundException : DomainException
{
    public ProductNotFoundException(Guid productId) 
        : base($"Product with ID {productId} was not found") { }
}
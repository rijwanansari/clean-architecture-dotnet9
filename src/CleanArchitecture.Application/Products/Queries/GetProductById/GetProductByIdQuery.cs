using System;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Products.Queries.GetProducts;
using MediatR;

namespace CleanArchitecture.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;

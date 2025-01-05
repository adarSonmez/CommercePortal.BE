﻿using AutoMapper;
using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Application.Common.Responses;
using CommercePortal.Application.Dtos.Files;
using CommercePortal.Domain.Common;
using MediatR;

namespace CommercePortal.Application.Features.ProductImageFiles.Queries.GetProductImageById;

/// <summary>
/// Handles the <see cref="GetProductImageByIdQueryRequest"/>.
/// </summary>
public class GetProductImageByIdQueryHandler : IRequestHandler<GetProductImageByIdQueryRequest, SingleResponse<ProductImageFileDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;

    public GetProductImageByIdQueryHandler(IMapper mapper, IProductImageFileReadRepository productImageFileReadRepository)
    {
        _mapper = mapper;
        _productImageFileReadRepository = productImageFileReadRepository;
    }

    public async Task<SingleResponse<ProductImageFileDto>> Handle(GetProductImageByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ProductImageFileDto>();
        try
        {
            var product = await _productImageFileReadRepository.GetByIdAsync(request.Id);
            BusinessRules.Run(("PIF916690", BusinessRules.CheckEntityNull(product)));

            response.SetData(_mapper.Map<ProductImageFileDto>(product));
        }
        catch (Exception ex)
        {
            response.AddError("PIF593212", ex.Message);
        }

        return response;
    }
}
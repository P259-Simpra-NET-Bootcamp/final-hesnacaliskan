﻿using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<ProductsController> _logger;
        readonly IProductService _productService;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger, IProductService productService)
        {
            _mediator = mediator;
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<GetAllProductQueryResponse>> Get()
        {
            return await _mediator.Send(new GetAllProductQueryRequest());

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

    }
}

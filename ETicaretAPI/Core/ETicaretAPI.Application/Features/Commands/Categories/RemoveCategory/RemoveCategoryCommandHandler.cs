﻿using MediatR;
using ETicaretAPI.Application.Repositories.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Categories.RemoveCategory
{
    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommandRequest, RemoveCategoryCommandResponse>
    {
        readonly ICategoryWriteRepository _categoryWriteRepository;

        public RemoveCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<RemoveCategoryCommandResponse> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _categoryWriteRepository.RemoveAsync(request.Id);
            await _categoryWriteRepository.SaveAsync();
            return new();
        }
    }
}

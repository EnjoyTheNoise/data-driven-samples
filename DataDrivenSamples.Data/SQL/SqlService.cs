using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataDrivenSamples.Data.Shared.Dtos.Create;
using DataDrivenSamples.Data.Shared.Dtos.Delete;
using DataDrivenSamples.Data.Shared.Dtos.Get;
using DataDrivenSamples.Data.Shared.Dtos.Update;
using DataDrivenSamples.Data.Shared.Models;
using DataDrivenSamples.Data.SQL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DataDrivenSamples.Data.SQL
{
    public class SqlService : ISqlService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SqlService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IList<Item>> GetAll()
        {
            return await _unitOfWork.ItemRepository.GetAll().ToListAsync();
        }

        public async Task<Item> GetItem(GetItemRequestDto dto)
        {
            return await Get(dto.Id);
        }

        public async Task<Item> GetItem(string id)
        {
            return await Get(id);
        }

        public async Task<Item> CreateItem(CreateItemRequestDto dto)
        {
            var item = _mapper.Map<Item>(dto);

            await _unitOfWork.ItemRepository.AddAsync(item);
            await _unitOfWork.SaveAsync();

            return item;
        }

        public async Task<Item> UpdateItem(UpdateItemRequestDto dto)
        {
            var item = await GetItem(dto.Id);
            if (item.Value != dto.Value)
            {
                item.Value = dto.Value;
            }
            _unitOfWork.ItemRepository.Update(item);
            await _unitOfWork.SaveAsync();

            return item;
        }

        public async Task DeleteItem(DeleteItemRequestDto dto)
        {
            await Delete(dto.Id);
        }

        public async Task DeleteItem(string id)
        {
            await Delete(id);
        }

        private async Task Delete(string id)
        {
            var item = await GetItem(id);

            if (item != null)
            {
                _unitOfWork.ItemRepository.Delete(item);
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task<Item> Get(string id)
        {
            return await _unitOfWork.ItemRepository.Get(item => item.Id == id).SingleOrDefaultAsync();
        }
    }
}

using AutoMapper;
using EBANX.Assignment.Application.Models;
using EBANX.Assignment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBANX.Assignment.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AspnetRunDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class AspnetRunDtoMapper : Profile
    {
        public AspnetRunDtoMapper()
        {
            CreateMap<Deposit, DepositModel>().ReverseMap();

            CreateMap<Withdraw, WithdrawModel>().ReverseMap();

            CreateMap<Transfer, TransferModel>().ReverseMap();
        }
    }
}

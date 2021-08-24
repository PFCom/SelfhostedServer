using AutoMapper;

namespace PFCom.Selfhosted.Lib.AutoMapping.Impl
{
    public class ObjectMapper<TData, TDto> : IObjectMapper<TData, TDto>
    {
        private Mapper _mapper { get; set; }

        public ObjectMapper()
        {
        }
        
        public TDto Map(TData data)
        {
            this._mapper ??= new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TData, TDto>()));

            return this._mapper.Map<TDto>(data);
        }
    }
}

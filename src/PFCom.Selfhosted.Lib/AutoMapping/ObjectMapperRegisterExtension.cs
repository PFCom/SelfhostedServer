using System;
using System.Collections.Generic;
using PFCom.Selfhosted.Lib.AutoMapping.Impl;

namespace PFCom.Selfhosted.Lib.AutoMapping
{
    public static class ObjectMapperRegisterExtension
    {
        public static ICollection<TDto> MapTo<TData, TDto>(this IList<TData> list, Func<ICollection<TDto>> collectionFactory = null)
        {
            collectionFactory ??= () => new List<TDto>(list.Count);
            
            ICollection<TDto> res = collectionFactory();
            IObjectMapper<TData, TDto> mapper = new ObjectMapper<TData, TDto>();

            foreach (TData data in list)
            {
                res.Add(mapper.Map(data));
            }

            return res;
        }
    }
}

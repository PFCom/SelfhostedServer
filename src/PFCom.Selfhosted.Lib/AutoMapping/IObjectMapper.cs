namespace PFCom.Selfhosted.Lib.AutoMapping
{
    public interface IObjectMapper<TData, TDto>
    {
        public TDto Map(TData data);
    }
}

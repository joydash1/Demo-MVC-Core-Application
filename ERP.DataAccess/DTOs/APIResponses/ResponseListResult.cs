namespace ERP.DataAccess.Domains
{
    public class ResponseListResult<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
namespace Contact.API.Model
{
    public class RequestModel
    {
        public RequestModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

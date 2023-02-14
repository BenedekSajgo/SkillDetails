namespace BLL.Models
{
    public class BaseResult<T>
    {
        public List<string> Errors { get; set; } = new();
        public bool IsSuccesfull => !Errors.Any();
        public T Data { get; set; }
    }
}

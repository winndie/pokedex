namespace Pokedex.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }

        public ServiceResult(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        public ServiceResult()
        {
            IsSuccess = false;
        }
    }
}


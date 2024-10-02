namespace LaxmiSunriseBank.Services.Models
{
    /// <summary>
    /// Common API Response Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonAPIResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public T ResponseData { get; set; } = default!;
        public string? ErrorMessage { get; set; }
        public bool IsTimedOut { get; set; }
        public string? Response { get; set; }
    }
}

namespace AshidaERPNext.Models.Ajax
{
    public class ServerResponseDatatable
    {
        public dynamic? Data { get; set; }
        public string? Error { get; set; }
        public dynamic? FieldErrors { get; set; }
        public dynamic? Cancelled { get; set; }
    }
}

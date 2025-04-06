namespace AshidaERPNext.Models.Ajax
{
    public class ServerResponse
    {
        public bool Success { get; set; }
        public bool Exception { get; set; }
        public bool SessionExpired { get; set; }
        public bool AccessDenied { get; set; }
        public object Response { get; set; }
        public ServerMessage Message { get; set; }

        public ServerResponse()
        {
            Success = true;
            Exception = false;
            SessionExpired = false;
            AccessDenied = false;
            Response = new { };
            Message = new ServerMessage();
        }
    }
}

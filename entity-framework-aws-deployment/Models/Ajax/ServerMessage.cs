namespace AshidaERPNext.Models.Ajax
{
    public class ServerMessage
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public ServerMessage()
        {
            Type = "";
            Value = "";
        }
    }
}

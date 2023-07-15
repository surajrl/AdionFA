namespace AdionFA.TransferObject.Base
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }

        private string _message;
        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public string EntityId { get; set; }
        public object Enity { get; set; }
    }
}

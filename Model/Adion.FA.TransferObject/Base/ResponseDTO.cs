﻿using Adion.FA.Infrastructure.I18n.Enums;

namespace Adion.FA.TransferObject.Base
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;

                if (MessageResource == 0)
                {

                }
            }
        }

        private int _messageResource;
        public int MessageResource
        {
            get => _messageResource;
            set
            {
                _messageResource = value;

                if (Message == null)
                {
                    Message = ((MessageResourceEnum)value).GetMessage() ?? string.Empty;
                }
            }
        }

        public int EntityTypeId { get; set; }
        public string EntityId { get; set; }
        public object Enity { get; set; }
    }
}

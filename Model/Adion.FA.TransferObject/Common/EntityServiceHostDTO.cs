﻿using Adion.FA.TransferObject.Base;

namespace Adion.FA.TransferObject.Common
{
    public class EntityServiceHostDTO : EntityBaseDTO
    {
        public int EntityServiceHostId { get; set; }

        public int EntityTypeId { get; set; }
        public EntityTypeDTO EntityType { get; set; }

        public int EntityId { get; set; }

        public long ProcessId { get; set; }
    }
}

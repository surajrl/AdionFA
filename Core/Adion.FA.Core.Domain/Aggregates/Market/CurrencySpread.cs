﻿using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Market
{
    [Table(nameof(CurrencySpread))]
    public class CurrencySpread : ReferenceDataBaseExtended
    {
        [Key]
        public int CurrencySpreadId { get; set; }
    }
}

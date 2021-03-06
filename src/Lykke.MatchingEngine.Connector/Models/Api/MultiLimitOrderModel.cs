﻿using System;
using System.Collections.Generic;

namespace Lykke.MatchingEngine.Connector.Models.Api
{
    /// <summary>
    /// Multilimit order model.
    /// </summary>
    public class MultiLimitOrderModel
    {
        public string Id { get; set; }

        public string ClientId { get; set; }

        public string AssetPairId { get; set; }

        public bool CancelPreviousOrders { get; set; }

        public CancelMode CancelMode { get; set; }

        public IReadOnlyList<MultiOrderItemModel> Orders { get; set; } = Array.Empty<MultiOrderItemModel>();
    }
}
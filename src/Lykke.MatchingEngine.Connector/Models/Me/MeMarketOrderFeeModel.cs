﻿using Lykke.MatchingEngine.Connector.Models.Common;
using ProtoBuf;

namespace Lykke.MatchingEngine.Connector.Models.Me
{
    [ProtoContract]
    public class MeMarketOrderFeeModel
    {
        [ProtoMember(1, IsRequired = true)]
        public int Type { get; set; }
        [ProtoMember(2, IsRequired = false)]
        public double Size { get; set; }
        [ProtoMember(3, IsRequired = false)]
        public string SourceClientId { get; set; }
        [ProtoMember(4, IsRequired = false)]
        public string TargetClientId { get; set; }
        [ProtoMember(5, IsRequired = false)]
        public int SizeType { get; set; }
        [ProtoMember(6, IsRequired = false)]
        public string[] AssetId { get; set; }

        public static MeMarketOrderFeeModel Create(
            FeeType type,
            double size,
            string sourceClientId,
            string targetClientid,
            FeeSizeType sizeType,
            string[] assetIds)
        {
            return new MeMarketOrderFeeModel
            {
                Type = (int)type,
                Size = size,
                SourceClientId = sourceClientId,
                TargetClientId = targetClientid,
                SizeType = (int)sizeType,
                AssetId = assetIds,
            };
        }
    }
}

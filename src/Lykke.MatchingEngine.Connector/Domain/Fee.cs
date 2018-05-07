﻿using Lykke.MatchingEngine.Connector.Abstractions.Models;
using System;
using Common;
using Lykke.MatchingEngine.Connector.Models;

namespace Lykke.MatchingEngine.Connector.Domain
{
    //todo: Normally this logic has to be implemented inside FeeCalculator service

    internal class Fee
    {
        public FeeType Type { get; }

        public double Size { get; }

        public string SourceClientId { get; }

        public string TargetClientId { get; }

        public FeeSizeType SizeType { get; }

        public Fee(FeeType type, double size, string sourceClientId, string targetClientId, FeeSizeType sizeType)
        {
            switch (sizeType)
            {
                case FeeSizeType.ABSOLUTE:
                case FeeSizeType.PERCENTAGE:
                    break;
                default:
                    throw new Exception("Unknown fee size type");
            }

            Type = type;
            Size = size;
            SourceClientId = sourceClientId;
            TargetClientId = targetClientId;
            SizeType = sizeType;
        }

        public double CalculateAmountWithFee(double sourceAmount, int accuracy)
        {
            if (Type == FeeType.EXTERNAL_FEE)
            {
                return sourceAmount;
            }
            else
            {
                var amount = 0d;
                switch (SizeType)
                {
                    case FeeSizeType.ABSOLUTE:
                        amount = sourceAmount > 0 ? sourceAmount + Size : sourceAmount - Size;
                        break;
                    case FeeSizeType.PERCENTAGE:
                        amount = Math.Round(Math.Abs(sourceAmount) * (1 + Size), 15);
                        break;
                }
                return amount.TruncateDecimalPlaces(accuracy, true);
            }
        }

        public static Fee GenerateCashInOutFee(double amount, int accuracy, string clientId, double feeSize = 0d,
            FeeSizeType feeSizeType = FeeSizeType.ABSOLUTE)
        {
            var feeAbsolute = 0d;

            if (feeSize > 0)
            {
                switch (feeSizeType)
                {
                    case FeeSizeType.ABSOLUTE:
                        feeAbsolute = feeSize;
                        break;
                    case FeeSizeType.PERCENTAGE:
                        feeAbsolute = Math.Round(Math.Abs(amount) * feeSize, 15);
                        break;
                }

                feeAbsolute = feeAbsolute.TruncateDecimalPlaces(accuracy, true);
            }

            if (Math.Abs(feeAbsolute) > 0)
                return new Fee(feeSize.GetFeeType(), feeAbsolute, null, clientId, FeeSizeType.ABSOLUTE);

            return null;
        }

        public static Fee GenerateTransferFee(double amount, int accuracy, string feeClientId, double feeSizePercentage)
        {
            var feeAbsolute = Math.Round(amount * feeSizePercentage, 15).TruncateDecimalPlaces(accuracy, true);

            if (Math.Abs(feeAbsolute) > double.Epsilon)
                return new Fee(FeeType.CLIENT_FEE, feeAbsolute, null, feeClientId, FeeSizeType.ABSOLUTE);

            return null;
        }
    }
}
﻿using NinjaTrader.Custom.AddOns.OrderFlowBot.DataBar;

namespace NinjaTrader.Custom.AddOns.OrderFlowBot.Strategies
{
    // This strategy is designed for trading smaller price ranges aiming to capitalize on mean reversion.
    // Trade the edges with smaller targets on lower volatility times.
    public class RangeRebound : StrategyBase
    {
        public override string Name { get; set; }
        public override Direction ValidStrategyDirection { get; set; }

        public RangeRebound(OrderFlowBotState orderFlowBotState, OrderFlowBotDataBars dataBars, string name)
        : base(orderFlowBotState, dataBars, name)
        {
        }

        public override void CheckStrategy()
        {
            if (IsValidLongDirection() && ValidStrategyDirection == Direction.Flat)
            {
                CheckLong();
            }

            if (IsValidShortDirection() && ValidStrategyDirection == Direction.Flat)
            {
                CheckShort();
            }
        }

        public override void CheckLong()
        {
            if (IsBullishBar() && IsOpenAboveTriggerStrikePrice() && IsValidMinDelta() && IsValidWithinTriggerStrikePriceRange())
            {
                ValidStrategyDirection = Direction.Long;
            }
        }

        public override void CheckShort()
        {
            if (IsBearishBar() && IsOpenBelowTriggerStrikePrice() && IsValidMaxDelta() && IsValidWithinTriggerStrikePriceRange())
            {
                ValidStrategyDirection = Direction.Short;
            }
        }

        private bool IsOpenAboveTriggerStrikePrice()
        {
            if (orderFlowBotState.TriggerStrikePrice == 0)
            {
                return true;
            }

            return dataBars.Bar.Prices.Open > orderFlowBotState.TriggerStrikePrice;
        }

        private bool IsOpenBelowTriggerStrikePrice()
        {
            if (orderFlowBotState.TriggerStrikePrice == 0)
            {
                return true;
            }

            return dataBars.Bar.Prices.Open < orderFlowBotState.TriggerStrikePrice;
        }

        private bool IsValidMinDelta()
        {
            return dataBars.Bar.Deltas.MinDelta > -50 && dataBars.Bar.Deltas.MaxDelta > 50;
        }

        private bool IsValidMaxDelta()
        {
            return dataBars.Bar.Deltas.MaxDelta < 50 && dataBars.Bar.Deltas.MinDelta < -50;
        }

        private bool IsValidWithinTriggerStrikePriceRange()
        {
            if (orderFlowBotState.TriggerStrikePrice == 0)
            {
                return true;
            }

            return orderFlowBotState.TriggerStrikePrice - dataBars.Bar.Prices.Close <= 2;
        }

        private bool IsBullishBar()
        {
            return dataBars.Bar.BarType == BarType.Bullish;
        }

        private bool IsBearishBar()
        {
            return dataBars.Bar.BarType == BarType.Bearish;
        }
    }
}

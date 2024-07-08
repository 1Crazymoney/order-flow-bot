﻿using NinjaTrader.Custom.AddOns.OrderFlowBot.DataBar;
using NinjaTrader.Custom.AddOns.OrderFlowBot.DataBar.Dependencies;
using NinjaTrader.NinjaScript.Indicators;

namespace NinjaTrader.NinjaScript.Strategies
{
    // Set custom values that needs to access NinjaScript methods since it gives issues when extending elsewhere
    public partial class OrderFlowBot : Strategy
    {
        private OrderFlowDataBarBase GetOrderFlowDataBarBase(int barsAgo, OrderFlowCumulativeDelta cumulativeDelta)
        {
            NinjaTrader.NinjaScript.BarsTypes.VolumetricBarsType volumetricBar = Bars.BarsSeries.BarsType as NinjaTrader.NinjaScript.BarsTypes.VolumetricBarsType;

            CumulativeDeltaBar cumulativeDeltaBar = new CumulativeDeltaBar
            {
                Open = cumulativeDelta.DeltaOpen[barsAgo],
                Close = cumulativeDelta.DeltaClose[barsAgo],
                High = cumulativeDelta.DeltaHigh[barsAgo],
                Low = cumulativeDelta.DeltaLow[barsAgo],
            };

            OrderFlowDataBarBase baseBar = new OrderFlowDataBarBase
            {
                VolumetricBar = volumetricBar,
                BarsAgo = barsAgo,
                Time = ToTime(Time[barsAgo]),
                CurrentBar = CurrentBar,
                High = High[barsAgo],
                Low = Low[barsAgo],
                Open = Open[barsAgo],
                Close = Close[barsAgo],
                CumulativeDeltaBar = cumulativeDeltaBar,
            };

            return baseBar;
        }
    }
}

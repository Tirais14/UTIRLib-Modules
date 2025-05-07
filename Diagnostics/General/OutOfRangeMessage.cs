namespace UTIRLib.Diagnostics
{
    public class OutOfRangeMessage : ConsoleMessage
    {
        public enum LimiterType
        {
            /// <summary>
            /// Mark limiter value as max bound
            /// </summary>
            Max,
            /// <summary>
            /// Mark limiter value as min bound
            /// </summary>
            Min
        }

        public OutOfRangeMessage(object limiterValue, LimiterType limiterType) :
            base($"Out of Range. Bounds: {limiterType} = {limiterValue}", CallStackFramesOffsetConstructor)
        { }
        public OutOfRangeMessage(object param, string paramName, object limiterValue,
            LimiterType limiterType) :
            base($"Out of Range. Value: {param}, bounds: {limiterType} = {limiterValue}",
                    CallStackFramesOffsetConstructor)
        { }
    }
}

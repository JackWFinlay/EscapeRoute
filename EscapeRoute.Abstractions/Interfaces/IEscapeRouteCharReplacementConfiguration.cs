

using EscapeRoute.Abstractions.Enums;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouteCharReplacementConfiguration
    {
        public TabBehavior TabBehavior { get; set; }
        public SingleQuoteBehavior SingleQuoteBehavior { get; set; }
        public BackspaceBehavior BackspaceBehavior { get; set; }
        public FormFeedBehavior FormFeedBehavior { get; set; }
        public BackslashBehavior BackslashBehavior { get; set; }
        public NewLineBehavior NewLineBehavior { get; set; }
        public DoubleQuoteBehavior DoubleQuoteBehavior { get; set; }
        public CarriageReturnBehavior CarriageReturnBehavior { get; set; }
        public UnicodeNullBehavior UnicodeNullBehavior { get; set; }
        
        public UnicodeBehavior UnicodeBehavior { get; set; }
        public UnicodeSurrogateBehavior UnicodeSurrogateBehavior { get; set; }
        
        public IEscapeRouteEscapeHandler<TabBehavior> TabEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<BackspaceBehavior> BackspaceEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<FormFeedBehavior> FormFeedEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<BackslashBehavior> BackslashEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<DoubleQuoteBehavior> DoubleQuoteEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<SingleQuoteBehavior> SingleQuoteEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<NewLineBehavior> NewLineEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<CarriageReturnBehavior> CarriageReturnEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<UnicodeNullBehavior> UnicodeNullEscapeHandler { get; set; }

        public IEscapeRouteEscapeFunctionHandler<UnicodeBehavior> UnicodeEscapeHandler { get; set; }
        public IEscapeRouteEscapeFunctionHandler<UnicodeSurrogateBehavior> UnicodeSurrogateEscapeHandler { get; set; }
    }
}
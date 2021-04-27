using EscapeRoute.Abstractions.Enums;

namespace EscapeRoute.SpanEngine.Abstractions.Interfaces
{
    public interface IEscapeRouteConfiguration
    {
        public TabBehavior TabBehavior { get; set; }
        public SingleQuoteBehavior SingleQuoteBehavior { get; set; }
        public BackspaceBehavior BackspaceBehavior { get; set; }
        public FormFeedBehavior FormFeedBehavior { get; set; }
        public BackslashBehavior BackslashBehavior { get; set; }
        public NewLineType NewLineType { get; set; }
        public DoubleQuoteBehavior DoubleQuoteBehavior { get; set; }
        public CarriageReturnBehavior CarriageReturnBehavior { get; set; }
        
        public UnicodeBehavior UnicodeBehavior { get; set; }
        
        public IEscapeRouteEscapeHandler<TabBehavior> TabEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<BackspaceBehavior> BackspaceEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<FormFeedBehavior> FormFeedEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<BackslashBehavior> BackslashEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<DoubleQuoteBehavior> DoubleQuoteEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<SingleQuoteBehavior> SingleQuoteEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<NewLineType> NewLineEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<CarriageReturnBehavior> CarriageReturnEscapeHandler { get; set; }
        public IEscapeRouteEscapeHandler<UnicodeBehavior> UnicodeEscapeHandler { get; set; }
        public IReplacementEngine ReplacementEngine { get; set; }
    }
}
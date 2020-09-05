using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.BehaviorHandlers;

namespace EscapeRoute
{
    public class EscapeRouteConfiguration
    {
        private const TabBehavior _defaultTabBehavior = TabBehavior.Strip;
        private const BackspaceBehavior _defaultBackspaceBehavior = BackspaceBehavior.Strip;
        private const TrimBehavior _defaultTrimBehavior = TrimBehavior.Both;
        private const FormFeedBehavior _defaultFormFeedBehavior = FormFeedBehavior.Strip;
        private const BackslashBehavior _defaultBackslashBehavior = BackslashBehavior.Escape;
        private const UnicodeBehavior _defaultUnicodeBehavior = UnicodeBehavior.Escape;
        private const NewLineType _defaultNewLineType = NewLineType.Space;
        private const DoubleQuoteBehavior _defaultDoubleQuoteBehavior = DoubleQuoteBehavior.Double;

        private readonly Lazy<BackslashBehaviorHandler> _defaultBackslashBehaviorHandler = new Lazy<BackslashBehaviorHandler>();
        private readonly Lazy<BackspaceBehaviorHandler> _defaultBackspaceBehaviorHandler = new Lazy<BackspaceBehaviorHandler>();
        private readonly Lazy<FormFeedBehaviorHandler> _defaultFormFeedBehaviorHandler = new Lazy<FormFeedBehaviorHandler>();
        private readonly Lazy<TabBehaviorHandler> _defaultTabBehaviorHandler = new Lazy<TabBehaviorHandler>();
        private readonly Lazy<UnicodeBehaviorHandler> _defaultUnicodeBehaviorHandler = new Lazy<UnicodeBehaviorHandler>();
        private readonly Lazy<TrimBehaviorHandler> _defaultTrimBehaviorHandler = new Lazy<TrimBehaviorHandler>();
        private readonly Lazy<DoubleQuoteBehaviorHandler> _defaultDoubleQuoteBehaviorHandler = new Lazy<DoubleQuoteBehaviorHandler>();

        private TabBehavior? _tabBehavior;
        private BackspaceBehavior? _backspaceBehavior;
        private TrimBehavior? _trimBehavior;
        private FormFeedBehavior? _formFeedBehavior;
        private BackslashBehavior? _backslashBehavior;
        private UnicodeBehavior? _unicodeBehavior;
        private NewLineType? _newLineType;
        private DoubleQuoteBehavior? _doubleQuoteBehavior;

        private IEscapeRouteBehaviorHandler<BackslashBehavior> _backslashBehaviorHandler;
        private IEscapeRouteBehaviorHandler<BackspaceBehavior> _backspaceBehaviorHandler;
        private IEscapeRouteBehaviorHandler<FormFeedBehavior> _formFeedBehaviorHandler;
        private IEscapeRouteBehaviorHandler<TabBehavior> _tabBehaviorHandler;
        private IEscapeRouteBehaviorHandler<UnicodeBehavior> _unicodeBehaviorHandler;
        private IEscapeRouteBehaviorHandler<TrimBehavior> _trimBehaviorHandler;
        private IEscapeRouteBehaviorHandler<DoubleQuoteBehavior> _doubleQuoteBehaviorHandler;
        

        /// <summary>
        /// Gets or sets how tab \t characters are handled.
        /// </summary>
        public TabBehavior TabBehavior
        {
            get => _tabBehavior ?? _defaultTabBehavior;
            set => _tabBehavior = value;
        }

        /// <summary>
        /// Gets or sets the handler for handling tab \t characters.
        /// </summary>
        public IEscapeRouteBehaviorHandler<TabBehavior> TabBehaviorHandler
        {
            get => _tabBehaviorHandler ?? _defaultTabBehaviorHandler.Value;
            set => _tabBehaviorHandler = value;
        }

        /// <summary>
        /// Gets or sets how backspace \b characters are handled.
        /// </summary>
        /// <value>Backspace behaviour</value>
        public BackspaceBehavior BackspaceBehavior
        {
            get => _backspaceBehavior ?? _defaultBackspaceBehavior;
            set => _backspaceBehavior = value;
        }

        /// <summary>
        /// Gets or sets the handler for how backspace \b characters are handled.
        /// </summary>
        public IEscapeRouteBehaviorHandler<BackspaceBehavior> BackspaceBehaviorHandler
        {
            get => _backspaceBehaviorHandler ?? _defaultBackspaceBehaviorHandler.Value;
            set => _backspaceBehaviorHandler = value;
        }

        /// <summary>
        /// Gets or sets how line trimming is handled.
        /// </summary>
        public TrimBehavior TrimBehavior
        {
            get => _trimBehavior ?? _defaultTrimBehavior;
            set => _trimBehavior = value;
        }

        /// <summary>
        /// Gets or sets the handler for handling trimming of lines.
        /// </summary>
        public IEscapeRouteBehaviorHandler<TrimBehavior> TrimBehaviorHandler
        {
            get => _trimBehaviorHandler ?? _defaultTrimBehaviorHandler.Value;
            set => _trimBehaviorHandler = value;
        }

        /// <summary>
        /// Gets or sets how form feed \f characters are handled.
        /// </summary>
        public FormFeedBehavior FormFeedBehavior {
            get => _formFeedBehavior ?? _defaultFormFeedBehavior;
            set => _formFeedBehavior = value;
        }

        /// <summary>
        /// Gets or sets the handler for handling form feed \f characters.
        /// </summary>
        public IEscapeRouteBehaviorHandler<FormFeedBehavior> FormFeedBehaviorHandler
        {
            get => _formFeedBehaviorHandler ?? _defaultFormFeedBehaviorHandler.Value;
            set => _formFeedBehaviorHandler = value;
        }

        /// <summary>
        /// Gets or sets how backslash \\ characters are handled.
        /// </summary>
        public BackslashBehavior BackslashBehavior
        {
            get => _backslashBehavior ?? _defaultBackslashBehavior;
            set => _backslashBehavior = value;
        }

        /// <summary>
        /// Gets or sets the handler for handling backslash // characters.
        /// </summary>
        public IEscapeRouteBehaviorHandler<BackslashBehavior> BackslashBehaviorHandler
        {
            get => _backslashBehaviorHandler ?? _defaultBackslashBehaviorHandler.Value;
            set => _backslashBehaviorHandler = value;
        }

        /// <summary>
        /// Gets or sets how Unicode characters are handled.
        /// </summary>
        public UnicodeBehavior UnicodeBehavior 
        {
            get => _unicodeBehavior ?? _defaultUnicodeBehavior;
            set => _unicodeBehavior = value;
        }

        /// <summary>
        /// Gets or sets the handler for handling non-ASCII characters.
        /// </summary>
        public IEscapeRouteBehaviorHandler<UnicodeBehavior> UnicodeBehaviorHandler
        {
            get => _unicodeBehaviorHandler ?? _defaultUnicodeBehaviorHandler.Value;
            set => _unicodeBehaviorHandler = value;
        }

        /// <summary>
        /// Gets or sets the new line type for the output.
        /// </summary>
        public NewLineType NewLineType
        {
            get => _newLineType ?? _defaultNewLineType;
            set => _newLineType = value;
        }

        /// <summary>
        /// Gets or sets the double quote behavior.
        /// </summary>
        public DoubleQuoteBehavior DoubleQuoteBehavior
        {
            get => _doubleQuoteBehavior ?? _defaultDoubleQuoteBehavior;
            set => _doubleQuoteBehavior = value;
        }

        /// <summary>
        /// Gets or sets the behavior handler for double quote characters.
        /// </summary>
        public IEscapeRouteBehaviorHandler<DoubleQuoteBehavior> DoubleQuoteBehaviorHandler
        {
            get => _doubleQuoteBehaviorHandler ?? _defaultDoubleQuoteBehaviorHandler.Value;
            set => _doubleQuoteBehaviorHandler = value;
        }
    }
}
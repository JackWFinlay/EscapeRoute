using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.BehaviorHandlers;

namespace EscapeRoute
{
    public class EscapeRouteConfiguration
    {
        private const TabBehavior _defaultTabBehavior = TabBehavior.Strip;
        private const NewLineBehavior _defaultNewLineBehavior = NewLineBehavior.Strip;
        private const CarriageReturnBehavior _defaultCarriageReturnBehavior = CarriageReturnBehavior.Strip;
        private const BackspaceBehavior _defaultBackspaceBehavior = BackspaceBehavior.Strip;
        private const TrimBehavior _defaultTrimBehavior = TrimBehavior.Both;
        private const FormFeedBehavior _defaultFormFeedBehavior = FormFeedBehavior.Strip;
        private const BackslashBehavior _defaultBackslashBehavior = BackslashBehavior.Escape;
        private const UnicodeBehavior _defaultUnicodeBehavior = UnicodeBehavior.Escape;
        private const NewLineType _defaultNewLineType = NewLineType.None;
        
        private readonly Lazy<BackslashBehaviorHandler> _defaultBackslashBehaviorHandler = new Lazy<BackslashBehaviorHandler>();
        private readonly Lazy<BackspaceBehaviorHandler> _defaultBackspaceBehaviorHandler = new Lazy<BackspaceBehaviorHandler>();
        private readonly Lazy<CarriageReturnBehaviorHandler> _defaultCarriageReturnBehaviorHandler = new Lazy<CarriageReturnBehaviorHandler>();
        private readonly Lazy<FormFeedBehaviorHandler> _defaultFormFeedBehaviorHandler = new Lazy<FormFeedBehaviorHandler>();
        private readonly Lazy<NewLineBehaviorHandler> _defaultNewLineBehaviorhandler = new Lazy<NewLineBehaviorHandler>();
        // private readonly Lazy<BackslashBehaviorHandler> _defaultBackslashBehaviorHandler = new Lazy<BackslashBehaviorHandler>();
        // private readonly Lazy<BackslashBehaviorHandler> _defaultBackslashBehaviorHandler = new Lazy<BackslashBehaviorHandler>();
        // private readonly Lazy<BackslashBehaviorHandler> _defaultBackslashBehaviorHandler = new Lazy<BackslashBehaviorHandler>();

        private TabBehavior? _tabBehavior;
        private NewLineBehavior? _newLineBehavior;
        private CarriageReturnBehavior? _carriageReturnBehavior;
        private BackspaceBehavior? _backspaceBehavior;
        private TrimBehavior? _trimBehavior;
        private FormFeedBehavior? _formFeedBehavior;
        private BackslashBehavior? _backslashBehavior;
        private UnicodeBehavior? _unicodeBehavior;
        private NewLineType? _newLineType;

        private IEscapeRouteBehaviorHandler<BackslashBehavior> _backslashBehaviorHandler;
        private IEscapeRouteBehaviorHandler<BackspaceBehavior> _backspaceBehaviorHandler;
        private IEscapeRouteBehaviorHandler<CarriageReturnBehavior> _carriageReturnBehaviorHandler;
        private IEscapeRouteBehaviorHandler<FormFeedBehavior> _formFeedBehaviorHandler;
        private IEscapeRouteBehaviorHandler<NewLineBehavior> _newLineBehaviorHandler;


        /// <summary>
        /// Gets or sets how tab \t characters are handled.
        /// </summary>
        public TabBehavior TabBehavior
        {
            get => _tabBehavior ?? _defaultTabBehavior;
            set => _tabBehavior = value;
        }

        /// <summary>
        /// Gets or sets how new line \n characters are handled.
        /// </summary>
        /// <value>New line behaviour</value>
        public NewLineBehavior NewLineBehavior
        {
            get => _newLineBehavior ?? _defaultNewLineBehavior;
            set => _newLineBehavior = value;
        }

        /// <summary>
        /// Gets or sets the handler for handling new line \n characters.
        /// </summary>
        public IEscapeRouteBehaviorHandler<NewLineBehavior> NewLineBehaviorHandler
        {
            get => _newLineBehaviorHandler ?? _defaultNewLineBehaviorhandler.Value;
            set => _newLineBehaviorHandler = value;
        }

        /// <summary>
        /// Gets or sets how carriage return \r characters are handled.
        /// </summary>
        public CarriageReturnBehavior CarriageReturnBehavior
        {
            get => _carriageReturnBehavior ?? _defaultCarriageReturnBehavior;
            set => _carriageReturnBehavior = value;
        }

        /// <summary>
        /// Gets or sets the handler for handling carriage return \r characters.
        /// </summary>
        public IEscapeRouteBehaviorHandler<CarriageReturnBehavior> CarriageReturnBehaviorHandler
        {
            get => _carriageReturnBehaviorHandler ?? _defaultCarriageReturnBehaviorHandler.Value;
            set => _carriageReturnBehaviorHandler = value;
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
        /// Gets or sets the new line type for the output.
        /// </summary>
        public NewLineType NewLineType
        {
            get => _newLineType ?? _defaultNewLineType;
            set => _newLineType = value;
        }
    }
}
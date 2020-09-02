using System;
using EscapeRoute.Abstractions.Enums;
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
        
        private readonly Lazy<BackslashBehaviorHandler> _defaultBackslashBehaviorHandler = new Lazy<BackslashBehaviorHandler>();
        private readonly Lazy<BackspaceBehaviorHandler> _defaultBackspaceBehaviorHandler = new Lazy<BackspaceBehaviorHandler>();
        private readonly Lazy<CarriageReturnBehaviorHandler> _defaultCarriageReturnBehaviorHandler = new Lazy<CarriageReturnBehaviorHandler>();
        // private readonly Lazy<BackslashBehaviorHandler> _defaultBackslashBehaviorHandler = new Lazy<BackslashBehaviorHandler>();
        // private readonly Lazy<BackslashBehaviorHandler> _defaultBackslashBehaviorHandler = new Lazy<BackslashBehaviorHandler>();
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

        private BackslashBehaviorHandler _backslashBehaviorHandler;
        private BackspaceBehaviorHandler _backspaceBehaviorHandler;
        private CarriageReturnBehaviorHandler _carriageReturnBehaviorHandler;

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
        /// Gets or sets how carriage return \r characters are handled.
        /// </summary>
        public CarriageReturnBehavior CarriageReturnBehavior
        {
            get => _carriageReturnBehavior ?? _defaultCarriageReturnBehavior;
            set => _carriageReturnBehavior = value;
        }

        public CarriageReturnBehaviorHandler CarriageReturnBehaviorHandler
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
        public BackspaceBehaviorHandler BackspaceBehaviorHandler
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
        public BackslashBehaviorHandler BackslashBehaviorHandler
        {
            get => _backslashBehaviorHandler ?? _defaultBackslashBehaviorHandler.Value;
            set => _backslashBehaviorHandler = value;
        }

        /// <summary>
        /// Gets or sets how Unicode characters are handled.
        /// </summary>
        public UnicodeBehavior UnicodeBehavior {
            get => _unicodeBehavior ?? _defaultUnicodeBehavior;
            set => _unicodeBehavior = value;
        }
    }
}
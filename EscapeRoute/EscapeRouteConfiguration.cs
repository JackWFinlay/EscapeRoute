using System;
using System.Collections;
using System.Collections.Generic;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.BehaviorHandlers;
using EscapeRoute.EscapeHandlers;
using EscapeRoute.ReplacementEngines;

namespace EscapeRoute
{
    public class EscapeRouteConfiguration : IEscapeRouteConfiguration
    {
        private const TabBehavior _defaultTabBehavior = TabBehavior.Strip;
        private const BackspaceBehavior _defaultBackspaceBehavior = BackspaceBehavior.Strip;
        private const TrimBehavior _defaultTrimBehavior = TrimBehavior.Both;
        private const FormFeedBehavior _defaultFormFeedBehavior = FormFeedBehavior.Strip;
        private const BackslashBehavior _defaultBackslashBehavior = BackslashBehavior.Escape;
        private const UnicodeBehavior _defaultUnicodeBehavior = UnicodeBehavior.Escape;
        private const NewLineType _defaultNewLineType = NewLineType.Space;
        private const DoubleQuoteBehavior _defaultDoubleQuoteBehavior = DoubleQuoteBehavior.Double;
        private const SingleQuoteBehavior _defaultSingleQuoteBehavior = SingleQuoteBehavior.Single;

        private readonly Lazy<BackslashEscapeHandler> _defaultBackslashEscapeHandler = new Lazy<BackslashEscapeHandler>();
        private readonly Lazy<BackspaceEscapeHandler> _defaultBackspaceEscapeHandler = new Lazy<BackspaceEscapeHandler>();
        private readonly Lazy<FormFeedEscapeHandler> _defaultFormFeedEscapeHandler = new Lazy<FormFeedEscapeHandler>();
        private readonly Lazy<TabEscapeHandler> _defaultTabEscapeHandler = new Lazy<TabEscapeHandler>();
        private readonly Lazy<DoubleQuoteEscapeHandler> _defaultDoubleQuoteEscapeHandler = new Lazy<DoubleQuoteEscapeHandler>();
        private readonly Lazy<SingleQuoteEscapeHandler> _defaultSingleQuoteEscapeHandler = new Lazy<SingleQuoteEscapeHandler>();
        private readonly Lazy<UnicodeBehaviorHandler> _defaultUnicodeBehaviorHandler = new Lazy<UnicodeBehaviorHandler>();
        private readonly Lazy<TrimBehaviorHandler> _defaultTrimBehaviorHandler = new Lazy<TrimBehaviorHandler>();

        private readonly Lazy<SpanReplacementEngine> _defaultReplacementEngine = new Lazy<SpanReplacementEngine>();

        private TabBehavior? _tabBehavior;
        private BackspaceBehavior? _backspaceBehavior;
        private TrimBehavior? _trimBehavior;
        private FormFeedBehavior? _formFeedBehavior;
        private BackslashBehavior? _backslashBehavior;
        private UnicodeBehavior? _unicodeBehavior;
        private NewLineType? _newLineType;
        private DoubleQuoteBehavior? _doubleQuoteBehavior;
        private SingleQuoteBehavior? _singleQuoteBehavior;

        private IList<IEscapeRouteCustomBehaviorHandler> _customBehaviorHandlers;

        private IEscapeRouteEscapeHandler<BackslashBehavior> _backslashEscapeHandler;
        private IEscapeRouteEscapeHandler<BackspaceBehavior> _backspaceEscapeHandler;
        private IEscapeRouteEscapeHandler<FormFeedBehavior> _formFeedEscapeHandler;
        private IEscapeRouteEscapeHandler<TabBehavior> _tabEscapeHandler;
        private IEscapeRouteEscapeHandler<DoubleQuoteBehavior> _doubleQuoteEscapeHandler;
        private IEscapeRouteEscapeHandler<SingleQuoteBehavior> _singleQuoteEscapeHandler;
        private IEscapeRouteBehaviorHandler<TrimBehavior> _trimBehaviorHandler;
        private IEscapeRouteBehaviorHandler<UnicodeBehavior> _unicodeBehaviorHandler;

        private IReplacementEngine _replacementEngine;

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
        public IEscapeRouteEscapeHandler<TabBehavior> TabEscapeHandler
        {
            get => _tabEscapeHandler ?? _defaultTabEscapeHandler.Value;
            set => _tabEscapeHandler = value;
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
        public IEscapeRouteEscapeHandler<BackspaceBehavior> BackspaceEscapeHandler
        {
            get => _backspaceEscapeHandler ?? _defaultBackspaceEscapeHandler.Value;
            set => _backspaceEscapeHandler = value;
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
        public IEscapeRouteEscapeHandler<FormFeedBehavior> FormFeedEscapeHandler
        {
            get => _formFeedEscapeHandler ?? _defaultFormFeedEscapeHandler.Value;
            set => _formFeedEscapeHandler = value;
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
        public IEscapeRouteEscapeHandler<BackslashBehavior> BackslashEscapeHandler
        {
            get => _backslashEscapeHandler ?? _defaultBackslashEscapeHandler.Value;
            set => _backslashEscapeHandler = value;
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
        public IEscapeRouteEscapeHandler<DoubleQuoteBehavior> DoubleQuoteEscapeHandler
        {
            get => _doubleQuoteEscapeHandler ?? _defaultDoubleQuoteEscapeHandler.Value;
            set => _doubleQuoteEscapeHandler = value;
        }
        
        /// <summary>
        /// Gets or sets the single quote behavior.
        /// </summary>
        public SingleQuoteBehavior SingleQuoteBehavior
        {
            get => _singleQuoteBehavior ?? _defaultSingleQuoteBehavior;
            set => _singleQuoteBehavior = value;
        }

        /// <summary>
        /// Gets or sets the behavior handler for single quote characters.
        /// </summary>
        public IEscapeRouteEscapeHandler<SingleQuoteBehavior> SingleQuoteEscapeHandler
        {
            get => _singleQuoteEscapeHandler ?? _defaultSingleQuoteEscapeHandler.Value;
            set => _singleQuoteEscapeHandler = value;
        }

        /// <summary>
        /// Gets or sets custom behavior handlers to be run after default handlers.
        /// </summary>
        public IList<IEscapeRouteCustomBehaviorHandler> CustomBehaviorHandlers
        {
            get => _customBehaviorHandlers ?? new List<IEscapeRouteCustomBehaviorHandler>();
            set => _customBehaviorHandlers = value;
        }

        /// <summary>
        /// Gets or sets the replacement engine for handling text replacements.
        /// </summary>
        public IReplacementEngine ReplacementEngine
        {
            get => _replacementEngine ?? _defaultReplacementEngine.Value;
            set => _replacementEngine = value;
        }
    }
}
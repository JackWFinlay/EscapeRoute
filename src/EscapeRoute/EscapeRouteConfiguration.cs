namespace JackWFinlay.EscapeRoute
{
    public class EscapeRouteConfiguration
    {
        internal const TabBehaviour DefaultTabBehaviour = TabBehaviour.Strip;
        internal const NewLineBehaviour DefaultNewLineBehaviour = NewLineBehaviour.Strip;
        internal const CarriageReturnBehaviour DefaultCarriageReturnBehaviour = CarriageReturnBehaviour.Strip;
        internal const BackspaceBehaviour DefaultBackspaceBehaviour = BackspaceBehaviour.Strip;
        internal const TrimBehaviour DefaultTrimBehaviour = TrimBehaviour.Both;

        private TabBehaviour? _tabBehaviour;
        private NewLineBehaviour? _newLineBehaviour;
        private CarriageReturnBehaviour? _carriageReturnBehaviour;
        private BackspaceBehaviour? _backspaceBehaviour;
        private TrimBehaviour? _trimBehaviour;

        /// <summary>
        /// Gets or sets how tab \t characters are handled.
        /// </summary>
        /// <value>Tab behaviour</value>
        public TabBehaviour TabBehaviour
        {
            get => _tabBehaviour ?? DefaultTabBehaviour;
            set => _tabBehaviour = value;
        }

        /// <summary>
        /// Gets or sets how new line \n characters are handled.
        /// </summary>
        /// <value>New line behaviour</value>
        public NewLineBehaviour NewLineBehaviour
        {
            get => _newLineBehaviour ?? DefaultNewLineBehaviour;
            set => _newLineBehaviour = value;
        }

        /// <summary>
        /// Gets or sets how carraiage return \r characters are handled.
        /// </summary>
        /// <value>Carriage return behaviour</value>
        public CarriageReturnBehaviour CarriageReturnBehaviour
        {
            get => _carriageReturnBehaviour ?? DefaultCarriageReturnBehaviour;
            set => _carriageReturnBehaviour = value;
        }

        /// <summary>
        /// Gets or sets how backspace \b characters are handled.
        /// </summary>
        /// <value>Backspace behaviour</value>
        public BackspaceBehaviour BackspaceBehaviour
        {
            get => _backspaceBehaviour ?? DefaultBackspaceBehaviour;
            set => _backspaceBehaviour = value;
        }

        /// <summary>
        /// Gets or sets how line trimming is handled.
        /// </summary>
        /// <value>Line trim behaviour</value>
        public TrimBehaviour TrimBehaviour
        {
            get => _trimBehaviour ?? DefaultTrimBehaviour;
            set => _trimBehaviour = value;
        }
    }
}
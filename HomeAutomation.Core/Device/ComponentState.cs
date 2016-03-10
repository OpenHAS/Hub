namespace HomeAutomation.Core
{
    /// <summary>
    /// Contains the state of a component.
    /// </summary>
    public class ComponentState
    {
        /// <summary>
        /// Name of the component. eg. LM35 (a temperature sensor)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The string representation of this component.
        /// </summary>
        public string Value { get; set; }
    }
}

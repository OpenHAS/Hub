namespace HomeAutomation.Core
{
    /// <summary>
    /// The IHomeController interface is implemented by every controller participating in the system.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Initializes the controller, givint the option to access the <see cref="ControllerHost"/>.
        /// </summary>
        /// <param name="controllerHost"></param>
        void Initialize(ControllerHost controllerHost);

        /// <summary>
        /// The controller should return its updated diagnostics object.
        /// </summary>
        object GetUpdatedDiagnostics();
    }
}

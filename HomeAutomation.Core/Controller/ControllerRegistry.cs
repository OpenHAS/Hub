using HomeAutomation.Core.Controller;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Maintains a registry of the controllers registered to the system.
    /// </summary>
    public class ControllerRegistry
    {
        public event EventHandler<IController> ControllerAdded;

        private IController[] controllerList = new IController[0];

        /// <summary>
        /// Registers a controller to the system.
        /// </summary>
        /// <param name="controller"></param>
        public void RegisterController(IController controller)
        {
            List<IController> newList = new List<IController>(controllerList);

            newList.Add(controller);

            // make sure we give the controller the option to take hold on the controller host.
            controller.Initialize(HomeAutomationSystem.ControllerHost);

            controllerList = newList.ToArray();

            ControllerAdded?.Invoke(this, controller);
        }

        /// <summary>
        /// Returns the currently registered controller list, which can be used to loop through the list, as it will never change, but 
        /// rather a new instance will be returned on change.
        /// </summary>
        public IController[] GetControllers()
        {
            return controllerList;
        }
    }
}

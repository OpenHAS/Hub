using System;
using System.Collections.Generic;

namespace HomeAutomation.Core.Service
{
    /// <summary>
    /// SerivceRegistry is the main point in the application to turn to for services.
    /// </summary>
    public class ServiceRegistry
    {
        private Dictionary<Type, IService> serviceRegistry;

        public ServiceRegistry()
        {
            serviceRegistry = new Dictionary<Type, IService>();
        }

        /// <summary>
        /// Registers a service into the system.
        /// </summary>
        /// <param name="serviceInstance"></param>
        public void RegisterService(IService serviceInstance)
        {
            RegisterService(serviceInstance.GetType(), serviceInstance);
        }

        /// <summary>
        /// Registers a service into the system.
        /// </summary>
        /// <param name="serviceInstance"></param>
        public void RegisterService(Type serviceType, IService serviceInstance)
        {
            serviceRegistry.Add(serviceType, serviceInstance);
        }

        public IService GetServiceByType(Type serviceType)
        {
            return serviceRegistry.ContainsKey(serviceType) ? serviceRegistry[serviceType] : null;
        }
    }
}

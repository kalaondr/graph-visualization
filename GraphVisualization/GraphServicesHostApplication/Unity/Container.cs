using Microsoft.Practices.Unity;

namespace GraphServicesHostApplication.Unity
{
    /// <summary>
    /// Class providing acces to Unity container instance
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// The Unity container instance
        /// </summary>
        public static IUnityContainer Instance { get; } = new UnityContainer();
    }
}
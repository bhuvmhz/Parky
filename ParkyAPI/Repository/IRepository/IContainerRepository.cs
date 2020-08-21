using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface IContainerRepository
    {
        ICollection<Container> GetContainers();
        Container GetContainer(int containerId);
        bool ContainerExists(string containerName);
        bool ContainerExists(int containerId);
        bool CreateContainer(Container container);
        bool UpdateContainer(Container container);
        bool DeleteContainer(Container container);
        bool Save();
    }
}

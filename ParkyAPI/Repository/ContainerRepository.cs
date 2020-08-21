using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Extensions;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository 
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly ApplicationDbContext _db;

        public ContainerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ContainerExists(int containerId)
        {
            return _db.Containers.Any(c => c.ContainerId == containerId);
        }

        public bool ContainerExists(string containerName)
        {
            return _db.Containers.Any(c => c.ContainerName.TrimAndLower() == containerName.TrimAndLower());
        }

        public bool CreateContainer(Container container)
        {
            _db.Containers.Add(container);
            return Save();
        }

        public bool DeleteContainer(Container container)
        {
            _db.Containers.Remove(container);
            return Save();
        }

        public Container GetContainer(int containerId)
        {
            return _db.Containers.FirstOrDefault(x => x.ContainerId == containerId);
        }

        public ICollection<Container> GetContainers()
        {
            return _db.Containers.OrderBy(c => c.ContainerName).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateContainer(Container container)
        {
            _db.Containers.Update(container);
            return Save();
        }
    }
}

using System;
using System.Collections.Generic;

namespace TheProject
{
    public sealed class EmployeeProvider
    {
        private readonly ISource source;

        public EmployeeProvider(ISource source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public Employee Get(int id)
        {
            return source.GetById(id);
        }

        public IEnumerable<Employee> Find(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return source.FindByName(name);
        }

        public void Delete(int id)
        {
            source.Delete(id);
        }
    }

    public sealed class Employee
    {
        public string Name { get; }
        public int Id { get; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public interface ISource
    {
        void Delete(int id);
        IEnumerable<Employee> FindByName(string name);
        Employee GetById(int id);
    }
}

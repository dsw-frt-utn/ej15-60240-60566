using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public string Name { get; init; }
        public Guid? SpecialityId { get; set; }
        public string Description { get; init; }

        private Speciality()
        {
        }
        public Speciality(String name, String description,
            Guid? id=null): base(id)
        {
            Name = name;
            Description = description;
        }
    }
}

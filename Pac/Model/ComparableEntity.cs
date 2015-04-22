using System;
using Ocon.Entity;

namespace Pac.Model
{
    public abstract class ComparableEntity : IEntity, IComparable<ComparableEntity>
    {
        public int CompareTo(ComparableEntity other)
        {
            return Id.CompareTo(other.Id);
        }

        public Guid Id { get; set; }
        public Guid WidgetId { get; set; }
    }
}
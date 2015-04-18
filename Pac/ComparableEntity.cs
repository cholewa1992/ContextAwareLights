using System;
using Ocon.Entity;

namespace HueLightClient
{
	public abstract class ComparableEntity : IEntity, IComparable<ComparableEntity>
	{
		public Guid Id { get; set; }
		public Guid WidgetId { get; set; }
		public int CompareTo(ComparableEntity other)
		{
			return Id.CompareTo(other.Id);
		}
	}
}


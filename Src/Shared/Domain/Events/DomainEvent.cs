namespace UserService.Shared.Domain.Events
{
    public class DomainEvent
    {
        private static readonly Dictionary<string, List<Action<IDomainEvent>>> handlersMap = new();
        private static readonly List<AggregateRoot> markedAggregates = new();

        private static AggregateRoot? FindMarkedAggregateByID(UniqueIdentity id)
        {
            return markedAggregates.Find(aggregate => aggregate.Id.Equals(id));
        }

        public static void MarkAggregateForDispatch(AggregateRoot aggregate)
        {
            if (FindMarkedAggregateByID(aggregate.Id) == null)
            {
                markedAggregates.Add(aggregate);
            }
        }

        private static void Dispatch(IDomainEvent @event)
        {
            string eventClassName = @event.GetType().Name;

            if (handlersMap.ContainsKey(eventClassName))
            {
                var handlers = handlersMap[eventClassName];
                foreach (var handler in handlers)
                {
                    handler(@event);
                }
            }
        }

        private static void DispatchAggregateEvents(AggregateRoot aggregate)
        {
            foreach (var @event in aggregate.GetDomainEvents())
            {
                Dispatch(@event);
            }
        }

        public static void DispatchEventsForAggregate(UniqueIdentity id)
        {
            var aggregate = FindMarkedAggregateByID(id);
            if (aggregate != null)
            {
                DispatchAggregateEvents(aggregate);
                aggregate.ClearEvents();
                RemoveAggregateFromMarkedDispatchList(aggregate);
            }
        }

        private static void RemoveAggregateFromMarkedDispatchList(AggregateRoot aggregate)
        {
            markedAggregates.RemoveAll(a => a.Equals(aggregate));
        }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            string eventClassName = typeof(T).Name;

            if (!handlersMap.ContainsKey(eventClassName))
            {
                handlersMap[eventClassName] = new List<Action<IDomainEvent>>();
            }

            handlersMap[eventClassName].Add(@event => callback((T)@event));
        }

        public static void ClearHandlers()
        {
            handlersMap.Clear();
        }

        public static void ClearMarkedAggregates()
        {
            markedAggregates.Clear();
        }
    }
}
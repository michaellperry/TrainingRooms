using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using UpdateControls.Correspondence.Strategy;
using System;
using System.IO;

/**
/ For use with http://graphviz.org/
digraph "TrainingRooms.Model"
{
    rankdir=BT
    Room -> Venue
    Room__name -> Room
    Room__name -> Room__name [label="  *"]
    Group -> Venue
    Group__name -> Group
    Group__name -> Group__name [label="  *"]
    Schedule -> Room
    Schedule -> Day
    Event -> Schedule
    Event -> Group
}
**/

namespace TrainingRooms.Model
{
    public partial class Individual : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Individual newFact = new Individual(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._anonymousId = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Individual fact = (Individual)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._anonymousId);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Individual.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Individual.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Individual", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Individual GetUnloadedInstance()
        {
            return new Individual((FactMemento)null) { IsLoaded = false };
        }

        public static Individual GetNullInstance()
        {
            return new Individual((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Individual> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Individual)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results

        // Business constructor
        public Individual(
            string anonymousId
            )
        {
            InitializeResults();
            _anonymousId = anonymousId;
        }

        // Hydration constructor
        private Individual(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Venue : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Venue newFact = new Venue(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Venue fact = (Venue)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Venue.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Venue.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Venue", 2);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Venue GetUnloadedInstance()
        {
            return new Venue((FactMemento)null) { IsLoaded = false };
        }

        public static Venue GetNullInstance()
        {
            return new Venue((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Venue> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Venue)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries
        private static Query _cacheQueryRooms;

        public static Query GetQueryRooms()
		{
            if (_cacheQueryRooms == null)
            {
			    _cacheQueryRooms = new Query()
		    		.JoinSuccessors(Room.GetRoleVenue())
                ;
            }
            return _cacheQueryRooms;
		}

        // Predicates

        // Predecessors

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Room> _rooms;

        // Business constructor
        public Venue(
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
        }

        // Hydration constructor
        private Venue(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _rooms = new Result<Room>(this, GetQueryRooms(), Room.GetUnloadedInstance, Room.GetNullInstance);
        }

        // Predecessor access

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public Result<Room> Rooms
        {
            get { return _rooms; }
        }

        // Mutable property access

    }
    
    public partial class Day : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Day newFact = new Day(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._when = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Day fact = (Day)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._when);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Day.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Day.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Day", 56);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Day GetUnloadedInstance()
        {
            return new Day((FactMemento)null) { IsLoaded = false };
        }

        public static Day GetNullInstance()
        {
            return new Day((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Day> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Day)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private DateTime _when;

        // Results

        // Business constructor
        public Day(
            DateTime when
            )
        {
            InitializeResults();
            _when = when;
        }

        // Hydration constructor
        private Day(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public DateTime When
        {
            get { return _when; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Room : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Room newFact = new Room(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Room fact = (Room)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Room.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Room.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Room", -1516598966);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Room GetUnloadedInstance()
        {
            return new Room((FactMemento)null) { IsLoaded = false };
        }

        public static Room GetNullInstance()
        {
            return new Room((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Room> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Room)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleVenue;
        public static Role GetRoleVenue()
        {
            if (_cacheRoleVenue == null)
            {
                _cacheRoleVenue = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "venue",
			        Venue._correspondenceFactType,
			        false));
            }
            return _cacheRoleVenue;
        }

        // Queries
        private static Query _cacheQueryName;

        public static Query GetQueryName()
		{
            if (_cacheQueryName == null)
            {
			    _cacheQueryName = new Query()
    				.JoinSuccessors(Room__name.GetRoleRoom(), Condition.WhereIsEmpty(Room__name.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryName;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Venue> _venue;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Room__name> _name;

        // Business constructor
        public Room(
            Venue venue
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _venue = new PredecessorObj<Venue>(this, GetRoleVenue(), venue);
        }

        // Hydration constructor
        private Room(FactMemento memento)
        {
            InitializeResults();
            _venue = new PredecessorObj<Venue>(this, GetRoleVenue(), memento, Venue.GetUnloadedInstance, Venue.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Room__name>(this, GetQueryName(), Room__name.GetUnloadedInstance, Room__name.GetNullInstance);
        }

        // Predecessor access
        public Venue Venue
        {
            get { return IsNull ? Venue.GetNullInstance() : _venue.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<Room__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _name.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Room__name(this, _name, value.Value));
                    }
                });
			}
        }

    }
    
    public partial class Room__name : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Room__name newFact = new Room__name(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Room__name fact = (Room__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Room__name.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Room__name.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Room__name", -968433032);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Room__name GetUnloadedInstance()
        {
            return new Room__name((FactMemento)null) { IsLoaded = false };
        }

        public static Room__name GetNullInstance()
        {
            return new Room__name((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Room__name> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Room__name)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleRoom;
        public static Role GetRoleRoom()
        {
            if (_cacheRoleRoom == null)
            {
                _cacheRoleRoom = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "room",
			        Room._correspondenceFactType,
			        false));
            }
            return _cacheRoleRoom;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Room__name._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Room__name.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Room> _room;
        private PredecessorList<Room__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Room__name(
            Room room
            ,IEnumerable<Room__name> prior
            ,string value
            )
        {
            InitializeResults();
            _room = new PredecessorObj<Room>(this, GetRoleRoom(), room);
            _prior = new PredecessorList<Room__name>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Room__name(FactMemento memento)
        {
            InitializeResults();
            _room = new PredecessorObj<Room>(this, GetRoleRoom(), memento, Room.GetUnloadedInstance, Room.GetNullInstance);
            _prior = new PredecessorList<Room__name>(this, GetRolePrior(), memento, Room__name.GetUnloadedInstance, Room__name.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Room Room
        {
            get { return IsNull ? Room.GetNullInstance() : _room.Fact; }
        }
        public PredecessorList<Room__name> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Group : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Group newFact = new Group(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Group fact = (Group)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Group.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Group.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Group", -1516598966);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Group GetUnloadedInstance()
        {
            return new Group((FactMemento)null) { IsLoaded = false };
        }

        public static Group GetNullInstance()
        {
            return new Group((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Group> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Group)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleVenue;
        public static Role GetRoleVenue()
        {
            if (_cacheRoleVenue == null)
            {
                _cacheRoleVenue = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "venue",
			        Venue._correspondenceFactType,
			        false));
            }
            return _cacheRoleVenue;
        }

        // Queries
        private static Query _cacheQueryName;

        public static Query GetQueryName()
		{
            if (_cacheQueryName == null)
            {
			    _cacheQueryName = new Query()
    				.JoinSuccessors(Group__name.GetRoleGroup(), Condition.WhereIsEmpty(Group__name.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryName;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Venue> _venue;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Group__name> _name;

        // Business constructor
        public Group(
            Venue venue
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _venue = new PredecessorObj<Venue>(this, GetRoleVenue(), venue);
        }

        // Hydration constructor
        private Group(FactMemento memento)
        {
            InitializeResults();
            _venue = new PredecessorObj<Venue>(this, GetRoleVenue(), memento, Venue.GetUnloadedInstance, Venue.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Group__name>(this, GetQueryName(), Group__name.GetUnloadedInstance, Group__name.GetNullInstance);
        }

        // Predecessor access
        public Venue Venue
        {
            get { return IsNull ? Venue.GetNullInstance() : _venue.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<Group__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _name.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Group__name(this, _name, value.Value));
                    }
                });
			}
        }

    }
    
    public partial class Group__name : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Group__name newFact = new Group__name(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Group__name fact = (Group__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Group__name.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Group__name.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Group__name", -1825605400);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Group__name GetUnloadedInstance()
        {
            return new Group__name((FactMemento)null) { IsLoaded = false };
        }

        public static Group__name GetNullInstance()
        {
            return new Group__name((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Group__name> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Group__name)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleGroup;
        public static Role GetRoleGroup()
        {
            if (_cacheRoleGroup == null)
            {
                _cacheRoleGroup = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "group",
			        Group._correspondenceFactType,
			        false));
            }
            return _cacheRoleGroup;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Group__name._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Group__name.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Group> _group;
        private PredecessorList<Group__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Group__name(
            Group group
            ,IEnumerable<Group__name> prior
            ,string value
            )
        {
            InitializeResults();
            _group = new PredecessorObj<Group>(this, GetRoleGroup(), group);
            _prior = new PredecessorList<Group__name>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Group__name(FactMemento memento)
        {
            InitializeResults();
            _group = new PredecessorObj<Group>(this, GetRoleGroup(), memento, Group.GetUnloadedInstance, Group.GetNullInstance);
            _prior = new PredecessorList<Group__name>(this, GetRolePrior(), memento, Group__name.GetUnloadedInstance, Group__name.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Group Group
        {
            get { return IsNull ? Group.GetNullInstance() : _group.Fact; }
        }
        public PredecessorList<Group__name> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Schedule : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Schedule newFact = new Schedule(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Schedule fact = (Schedule)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Schedule.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Schedule.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Schedule", 531036552);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Schedule GetUnloadedInstance()
        {
            return new Schedule((FactMemento)null) { IsLoaded = false };
        }

        public static Schedule GetNullInstance()
        {
            return new Schedule((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Schedule> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Schedule)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleRoom;
        public static Role GetRoleRoom()
        {
            if (_cacheRoleRoom == null)
            {
                _cacheRoleRoom = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "room",
			        Room._correspondenceFactType,
			        false));
            }
            return _cacheRoleRoom;
        }
        private static Role _cacheRoleDay;
        public static Role GetRoleDay()
        {
            if (_cacheRoleDay == null)
            {
                _cacheRoleDay = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "day",
			        Day._correspondenceFactType,
			        false));
            }
            return _cacheRoleDay;
        }

        // Queries
        private static Query _cacheQueryEvents;

        public static Query GetQueryEvents()
		{
            if (_cacheQueryEvents == null)
            {
			    _cacheQueryEvents = new Query()
		    		.JoinSuccessors(Event.GetRoleSchedule())
                ;
            }
            return _cacheQueryEvents;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Room> _room;
        private PredecessorObj<Day> _day;

        // Fields

        // Results
        private Result<Event> _events;

        // Business constructor
        public Schedule(
            Room room
            ,Day day
            )
        {
            InitializeResults();
            _room = new PredecessorObj<Room>(this, GetRoleRoom(), room);
            _day = new PredecessorObj<Day>(this, GetRoleDay(), day);
        }

        // Hydration constructor
        private Schedule(FactMemento memento)
        {
            InitializeResults();
            _room = new PredecessorObj<Room>(this, GetRoleRoom(), memento, Room.GetUnloadedInstance, Room.GetNullInstance);
            _day = new PredecessorObj<Day>(this, GetRoleDay(), memento, Day.GetUnloadedInstance, Day.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _events = new Result<Event>(this, GetQueryEvents(), Event.GetUnloadedInstance, Event.GetNullInstance);
        }

        // Predecessor access
        public Room Room
        {
            get { return IsNull ? Room.GetNullInstance() : _room.Fact; }
        }
        public Day Day
        {
            get { return IsNull ? Day.GetNullInstance() : _day.Fact; }
        }

        // Field access

        // Query result access
        public Result<Event> Events
        {
            get { return _events; }
        }

        // Mutable property access

    }
    
    public partial class Event : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Event newFact = new Event(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Event fact = (Event)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Event.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Event.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"TrainingRooms.Model.Event", 1015337858);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Event GetUnloadedInstance()
        {
            return new Event((FactMemento)null) { IsLoaded = false };
        }

        public static Event GetNullInstance()
        {
            return new Event((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Event> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Event)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleSchedule;
        public static Role GetRoleSchedule()
        {
            if (_cacheRoleSchedule == null)
            {
                _cacheRoleSchedule = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "schedule",
			        Schedule._correspondenceFactType,
			        false));
            }
            return _cacheRoleSchedule;
        }
        private static Role _cacheRoleGroup;
        public static Role GetRoleGroup()
        {
            if (_cacheRoleGroup == null)
            {
                _cacheRoleGroup = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "group",
			        Group._correspondenceFactType,
			        false));
            }
            return _cacheRoleGroup;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Schedule> _schedule;
        private PredecessorObj<Group> _group;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public Event(
            Schedule schedule
            ,Group group
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _schedule = new PredecessorObj<Schedule>(this, GetRoleSchedule(), schedule);
            _group = new PredecessorObj<Group>(this, GetRoleGroup(), group);
        }

        // Hydration constructor
        private Event(FactMemento memento)
        {
            InitializeResults();
            _schedule = new PredecessorObj<Schedule>(this, GetRoleSchedule(), memento, Schedule.GetUnloadedInstance, Schedule.GetNullInstance);
            _group = new PredecessorObj<Group>(this, GetRoleGroup(), memento, Group.GetUnloadedInstance, Group.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Schedule Schedule
        {
            get { return IsNull ? Schedule.GetNullInstance() : _schedule.Fact; }
        }
        public Group Group
        {
            get { return IsNull ? Group.GetNullInstance() : _group.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    

	public class CorrespondenceModel : ICorrespondenceModel
	{
		public void RegisterAllFactTypes(Community community, IDictionary<Type, IFieldSerializer> fieldSerializerByType)
		{
			community.AddType(
				Individual._correspondenceFactType,
				new Individual.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Individual._correspondenceFactType }));
			community.AddType(
				Venue._correspondenceFactType,
				new Venue.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Venue._correspondenceFactType }));
			community.AddQuery(
				Venue._correspondenceFactType,
				Venue.GetQueryRooms().QueryDefinition);
			community.AddType(
				Day._correspondenceFactType,
				new Day.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Day._correspondenceFactType }));
			community.AddType(
				Room._correspondenceFactType,
				new Room.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Room._correspondenceFactType }));
			community.AddQuery(
				Room._correspondenceFactType,
				Room.GetQueryName().QueryDefinition);
			community.AddType(
				Room__name._correspondenceFactType,
				new Room__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Room__name._correspondenceFactType }));
			community.AddQuery(
				Room__name._correspondenceFactType,
				Room__name.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Group._correspondenceFactType,
				new Group.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Group._correspondenceFactType }));
			community.AddQuery(
				Group._correspondenceFactType,
				Group.GetQueryName().QueryDefinition);
			community.AddType(
				Group__name._correspondenceFactType,
				new Group__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Group__name._correspondenceFactType }));
			community.AddQuery(
				Group__name._correspondenceFactType,
				Group__name.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Schedule._correspondenceFactType,
				new Schedule.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Schedule._correspondenceFactType }));
			community.AddQuery(
				Schedule._correspondenceFactType,
				Schedule.GetQueryEvents().QueryDefinition);
			community.AddType(
				Event._correspondenceFactType,
				new Event.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Event._correspondenceFactType }));
		}
	}
}

using System;
using System.Collections;
using System.Diagnostics;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// Singleton Object Pool that holds user created objects.
	/// </summary>
	public class ObjectPool
	{
        public class ObjectAddedEventArgs : EventArgs
        {
            public Object instance;

            public ObjectAddedEventArgs(Object instance)
            {
                this.instance = instance;
            }
        }

		public delegate void ObjectAddedDelegate(Object sender, ObjectAddedEventArgs e);

		public static event ObjectAddedDelegate ObjectAdded = delegate { };

		public static bool Add(Object instance)	
		{
			Debug.Assert(instance != null);
			if (pool.Contains(instance)) return false;
			pool.Add(instance);
			ObjectAdded(null, new ObjectAddedEventArgs(instance));
			return true;
		}

		internal static ArrayList pool = new ArrayList();
	}
}

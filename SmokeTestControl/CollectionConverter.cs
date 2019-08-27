using System;
using System.Collections;
using System.Collections.Generic;

namespace net.wesleysteiner
{
	namespace utility
	{
		/// <summary>
		/// Utility class to convert an ICollection of any type into an array of another type.
		/// </summary>
		public class CollectionConverter<source_type, target_type>
		{
			public delegate target_type TypeConverter(source_type source);

			public static target_type[] ToArray(ICollection collection, TypeConverter converter)
			{
				List<target_type> list = new List<target_type>(collection.Count);
				foreach (source_type source in collection)
				{
					list.Add(converter(source));
				}
				return list.ToArray();
			}
		}
	}
}

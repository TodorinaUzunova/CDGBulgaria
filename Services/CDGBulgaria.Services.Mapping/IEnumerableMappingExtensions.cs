using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace CDGBulgaria.Services.Mapping
{
	public static class IEnumerableMappingExtensions
	{
		public static IEnumerable<TDestination> To<TDestination>(this IEnumerable source)
		{
			if (source==null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			foreach (var src in source)
			{
				yield return AutoMapper.Mapper.Map<TDestination>(src);
			}
		
		}
	}
}

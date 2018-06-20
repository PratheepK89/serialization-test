using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BayatGames.SaveGamePro.Utilities
{

	/// <summary>
	/// String utils.
	/// </summary>
	public static class StringUtils
	{

		/// <summary>
		/// Converts string capitialization from Title Case to camel Case.
		/// </summary>
		/// <returns>The camel case.</returns>
		/// <param name="titleCase">Title case.</param>
		public static string ToCamelCase ( string titleCase )
		{
			return char.ToLowerInvariant ( titleCase [ 0 ] ) + titleCase.Substring ( 1 );
		}
		
	}

}
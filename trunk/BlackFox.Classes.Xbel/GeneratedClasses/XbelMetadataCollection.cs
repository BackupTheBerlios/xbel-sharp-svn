/*
 * Date: 08/07/2004
 * Time: 22:18
 */

using System;
using System.Collections;

namespace BlackFox.Classes.Xbel
{
	/// <summary>
	///     <para>
	///       A collection that stores <see cref='.XbelMetadata'/> objects.
	///    </para>
	/// </summary>
	/// <seealso cref='.XbelMetadataCollection'/>
	[Serializable()]
	public class XbelMetadataCollection : CollectionBase {
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='.XbelMetadataCollection'/>.
		///    </para>
		/// </summary>
		public XbelMetadataCollection()
		{
		}
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='.XbelMetadataCollection'/> based on another <see cref='.XbelMetadataCollection'/>.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///       A <see cref='.XbelMetadataCollection'/> from which the contents are copied
		/// </param>
		public XbelMetadataCollection(XbelMetadataCollection val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref='.XbelMetadataCollection'/> containing any array of <see cref='.XbelMetadata'/> objects.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///       A array of <see cref='.XbelMetadata'/> objects with which to intialize the collection
		/// </param>
		public XbelMetadataCollection(XbelMetadata[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// <para>Represents the entry at the specified index of the <see cref='.XbelMetadata'/>.</para>
		/// </summary>
		/// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
		/// <value>
		///    <para> The entry at the specified index of the collection.</para>
		/// </value>
		/// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> is outside the valid range of indexes for the collection.</exception>
		public XbelMetadata this[int index] {
			get {
				return ((XbelMetadata)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		///    <para>Adds a <see cref='.XbelMetadata'/> with the specified value to the 
		///    <see cref='.XbelMetadataCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='.XbelMetadata'/> to add.</param>
		/// <returns>
		///    <para>The index at which the new element was inserted.</para>
		/// </returns>
		/// <seealso cref='.XbelMetadataCollection.AddRange'/>
		public int Add(XbelMetadata val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		/// <para>Copies the elements of an array to the end of the <see cref='.XbelMetadataCollection'/>.</para>
		/// </summary>
		/// <param name='value'>
		///    An array of type <see cref='.XbelMetadata'/> containing the objects to add to the collection.
		/// </param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <seealso cref='.XbelMetadataCollection.Add'/>
		public void AddRange(XbelMetadata[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		///     <para>
		///       Adds the contents of another <see cref='.XbelMetadataCollection'/> to the end of the collection.
		///    </para>
		/// </summary>
		/// <param name='value'>
		///    A <see cref='.XbelMetadataCollection'/> containing the objects to add to the collection.
		/// </param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <seealso cref='.XbelMetadataCollection.Add'/>
		public void AddRange(XbelMetadataCollection val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// <para>Gets a value indicating whether the 
		///    <see cref='.XbelMetadataCollection'/> contains the specified <see cref='.XbelMetadata'/>.</para>
		/// </summary>
		/// <param name='value'>The <see cref='.XbelMetadata'/> to locate.</param>
		/// <returns>
		/// <para><see langword='true'/> if the <see cref='.XbelMetadata'/> is contained in the collection; 
		///   otherwise, <see langword='false'/>.</para>
		/// </returns>
		/// <seealso cref='.XbelMetadataCollection.IndexOf'/>
		public bool Contains(XbelMetadata val)
		{
			return List.Contains(val);
		}
		
		/// <summary>
		/// <para>Copies the <see cref='.XbelMetadataCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
		///    specified index.</para>
		/// </summary>
		/// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='.XbelMetadataCollection'/> .</para></param>
		/// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
		/// <returns>
		///   <para>None.</para>
		/// </returns>
		/// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='.XbelMetadataCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
		/// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
		/// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
		/// <seealso cref='System.Array'/>
		public void CopyTo(XbelMetadata[] array, int index)
		{
			List.CopyTo(array, index);
		}
		
		/// <summary>
		///    <para>Returns the index of a <see cref='.XbelMetadata'/> in 
		///       the <see cref='.XbelMetadataCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='.XbelMetadata'/> to locate.</param>
		/// <returns>
		/// <para>The index of the <see cref='.XbelMetadata'/> of <paramref name='value'/> in the 
		/// <see cref='.XbelMetadataCollection'/>, if found; otherwise, -1.</para>
		/// </returns>
		/// <seealso cref='.XbelMetadataCollection.Contains'/>
		public int IndexOf(XbelMetadata val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		/// <para>Inserts a <see cref='.XbelMetadata'/> into the <see cref='.XbelMetadataCollection'/> at the specified index.</para>
		/// </summary>
		/// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
		/// <param name=' value'>The <see cref='.XbelMetadata'/> to insert.</param>
		/// <returns><para>None.</para></returns>
		/// <seealso cref='.XbelMetadataCollection.Add'/>
		public void Insert(int index, XbelMetadata val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		///    <para>Returns an enumerator that can iterate through 
		///       the <see cref='.XbelMetadataCollection'/> .</para>
		/// </summary>
		/// <returns><para>None.</para></returns>
		/// <seealso cref='System.Collections.IEnumerator'/>
		public new XbelMetadataEnumerator GetEnumerator()
		{
			return new XbelMetadataEnumerator(this);
		}
		
		/// <summary>
		///    <para> Removes a specific <see cref='.XbelMetadata'/> from the 
		///    <see cref='.XbelMetadataCollection'/> .</para>
		/// </summary>
		/// <param name='value'>The <see cref='.XbelMetadata'/> to remove from the <see cref='.XbelMetadataCollection'/> .</param>
		/// <returns><para>None.</para></returns>
		/// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
		public void Remove(XbelMetadata val)
		{
			List.Remove(val);
		}
		
		public class XbelMetadataEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			public XbelMetadataEnumerator(XbelMetadataCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			public XbelMetadata Current {
				get {
					return ((XbelMetadata)(baseEnumerator.Current));
				}
			}
			
			object IEnumerator.Current {
				get {
					return baseEnumerator.Current;
				}
			}
			
			public bool MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			
			bool IEnumerator.MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			
			public void Reset()
			{
				baseEnumerator.Reset();
			}
			
			void IEnumerator.Reset()
			{
				baseEnumerator.Reset();
			}
		}
	}
}

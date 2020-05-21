﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace BizHawk.BizInvoke
{
	/// <summary>
	/// create interop delegates and function pointers for a particular calling convention
	/// </summary>
	public interface ICallingConventionAdapter
	{
		IntPtr GetFunctionPointerForDelegate(Delegate d);
		IntPtr GetArrivalFunctionPointer(IntPtr p, ParameterInfo pp, object lifetime);

		Delegate GetDelegateForFunctionPointer(IntPtr p, Type delegateType);
		IntPtr GetDepartureFunctionPointer(IntPtr p, ParameterInfo pp, object lifetime);
	}

	public static class CallingConventionAdapterExtensions
	{
		public static T GetDelegateForFunctionPointer<T>(this ICallingConventionAdapter a, IntPtr p)
			where T : class
		{
			return (T)(object)a.GetDelegateForFunctionPointer(p, typeof(T));
		}
	}

	public class ParameterInfo
	{
		public Type ReturnType { get; }
		public IReadOnlyList<Type> ParameterTypes { get; }

		public ParameterInfo(Type returnType, IEnumerable<Type> parameterTypes)
		{
			ReturnType = returnType;
			ParameterTypes = parameterTypes.ToList().AsReadOnly();
		}

		/// <exception cref="InvalidOperationException"><paramref name="delegateType"/> does not inherit <see cref="Delegate"/></exception>
		public ParameterInfo(Type delegateType)
		{
			if (!typeof(Delegate).IsAssignableFrom(delegateType))
				throw new InvalidOperationException("Must be a delegate type!");
			var invoke = delegateType.GetMethod("Invoke");
			ReturnType = invoke.ReturnType;
			ParameterTypes = invoke.GetParameters().Select(p => p.ParameterType).ToList().AsReadOnly();
		}
	}

	public static class CallingConventionAdapters
	{
		private class NativeConvention : ICallingConventionAdapter
		{
			public IntPtr GetArrivalFunctionPointer(IntPtr p, ParameterInfo pp, object lifetime)
			{
				return p;
			}

			public Delegate GetDelegateForFunctionPointer(IntPtr p, Type delegateType)
			{
				return Marshal.GetDelegateForFunctionPointer(p, delegateType);
			}

			public IntPtr GetDepartureFunctionPointer(IntPtr p, ParameterInfo pp, object lifetime)
			{
				return p;
			}

			public IntPtr GetFunctionPointerForDelegate(Delegate d)
			{
				return Marshal.GetFunctionPointerForDelegate(d);
			}
		}

		/// <summary>
		/// native (pass-through) calling convention
		/// </summary>
		public static ICallingConventionAdapter Native { get; } = new NativeConvention();

		/// <summary>
		/// convention appropriate for waterbox guests
		/// </summary>
		public static ICallingConventionAdapter Waterbox { get; } =
#if true
			new NativeConvention();
#else
			new SysVHostMsGuest();
#endif

		private class SysVHostMsGuest : ICallingConventionAdapter
		{
			private const ulong Placeholder = 0xdeadbeeffeedface;
			private const byte Padding = 0x06;
			private const int BlockSize = 256;
			private static readonly byte[][] Depart =
			{
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x20, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x30, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x8b, 0x55, 0xf8, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x48, 0x89, 0xd1, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x30, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x40, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x89, 0x55, 0xe8, 0x48, 0x8b, 0x75, 0xe8, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x49, 0x89, 0xf0, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x40, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x89, 0x55, 0xe8, 0x48, 0x89, 0x4d, 0xe0, 0x48, 0x8b, 0x7d, 0xe0, 0x48, 0x8b, 0x75, 0xe8, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x49, 0x89, 0xf9, 0x49, 0x89, 0xf0, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x60, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x89, 0x55, 0xe8, 0x48, 0x89, 0x4d, 0xe0, 0x4c, 0x89, 0x45, 0xd8, 0x48, 0x8b, 0x7d, 0xe0, 0x48, 0x8b, 0x75, 0xe8, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0x8b, 0x45, 0xd8, 0x48, 0x89, 0x44, 0x24, 0x20, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x49, 0x89, 0xf9, 0x49, 0x89, 0xf0, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x60, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x89, 0x55, 0xe8, 0x48, 0x89, 0x4d, 0xe0, 0x4c, 0x89, 0x45, 0xd8, 0x4c, 0x89, 0x4d, 0xd0, 0x48, 0x8b, 0x7d, 0xe0, 0x48, 0x8b, 0x75, 0xe8, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0x8b, 0x45, 0xd0, 0x48, 0x89, 0x44, 0x24, 0x28, 0x48, 0x8b, 0x45, 0xd8, 0x48, 0x89, 0x44, 0x24, 0x20, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x49, 0x89, 0xf9, 0x49, 0x89, 0xf0, 0xff, 0xd0, 0xc9, 0xc3, },
			};
			private static readonly byte[][] Arrive =
			{
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x20, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x30, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x8b, 0x55, 0xf8, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x48, 0x89, 0xd1, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x30, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x40, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x89, 0x55, 0xe8, 0x48, 0x8b, 0x75, 0xe8, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x49, 0x89, 0xf0, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x40, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x89, 0x55, 0xe8, 0x48, 0x89, 0x4d, 0xe0, 0x48, 0x8b, 0x7d, 0xe0, 0x48, 0x8b, 0x75, 0xe8, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x49, 0x89, 0xf9, 0x49, 0x89, 0xf0, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x60, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x89, 0x55, 0xe8, 0x48, 0x89, 0x4d, 0xe0, 0x4c, 0x89, 0x45, 0xd8, 0x48, 0x8b, 0x7d, 0xe0, 0x48, 0x8b, 0x75, 0xe8, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0x8b, 0x45, 0xd8, 0x48, 0x89, 0x44, 0x24, 0x20, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x49, 0x89, 0xf9, 0x49, 0x89, 0xf0, 0xff, 0xd0, 0xc9, 0xc3, },
				new byte[] { 0x55, 0x48, 0x89, 0xe5, 0x48, 0x83, 0xec, 0x60, 0x48, 0x89, 0x7d, 0xf8, 0x48, 0x89, 0x75, 0xf0, 0x48, 0x89, 0x55, 0xe8, 0x48, 0x89, 0x4d, 0xe0, 0x4c, 0x89, 0x45, 0xd8, 0x4c, 0x89, 0x4d, 0xd0, 0x48, 0x8b, 0x7d, 0xe0, 0x48, 0x8b, 0x75, 0xe8, 0x48, 0x8b, 0x55, 0xf0, 0x48, 0x8b, 0x4d, 0xf8, 0x48, 0x8b, 0x45, 0xd0, 0x48, 0x89, 0x44, 0x24, 0x28, 0x48, 0x8b, 0x45, 0xd8, 0x48, 0x89, 0x44, 0x24, 0x20, 0x48, 0xb8, 0xce, 0xfa, 0xed, 0xfe, 0xef, 0xbe, 0xad, 0xde, 0x49, 0x89, 0xf9, 0x49, 0x89, 0xf0, 0xff, 0xd0, 0xc9, 0xc3, },
			};

			private static readonly int[] DepartPlaceholderIndices;
			private static readonly int[] ArrivePlaceholderIndices;

			private static int FindPlaceholderIndex(byte[] data)
			{
				return Enumerable.Range(0, data.Length - 7)
					.Single(i => BitConverter.ToUInt64(data, i) == Placeholder);
			}

			static SysVHostMsGuest()
			{
				DepartPlaceholderIndices = Depart.Select(FindPlaceholderIndex).ToArray();
				ArrivePlaceholderIndices = Arrive.Select(FindPlaceholderIndex).ToArray();
				if (Depart.Any(b => b.Length > BlockSize) || Arrive.Any(b => b.Length > BlockSize))
					throw new InvalidOperationException();
			}

			private readonly MemoryBlock _memory;
			private readonly object _sync = new object();
			private readonly WeakReference[] _refs;

			public SysVHostMsGuest()
			{
				int size = 4 * 1024 * 1024;
				_memory = MemoryBlock.Create((ulong)size);
				_memory.Activate();
				_refs = new WeakReference[size / BlockSize];
			}

			private int FindFreeIndex()
			{
				for (int i = 0; i < _refs.Length; i++)
				{
					if (_refs[i] == null || !_refs[i].IsAlive)
						return i;
				}
				throw new InvalidOperationException("Out of Thunk memory");
			}

			private int FindUsedIndex(object lifetime)
			{
				for (int i = 0; i < _refs.Length; i++)
				{
					if (_refs[i]?.Target == lifetime)
						return i;
				}
				return -1;
			}

			private static void VerifyParameter(Type type)
			{
				if (type == typeof(float) || type == typeof(double))
					throw new NotSupportedException("floating point not supported");
				if (type == typeof(void) || type.IsPrimitive)
					return;
				if (type.IsByRef || type.IsClass)
					return;
				throw new NotSupportedException("Unknown type.  Possibly supported?");
			}

			private static int VerifyDelegateSignature(ParameterInfo pp)
			{
				VerifyParameter(pp.ReturnType);
				foreach (var ppp in pp.ParameterTypes)
					VerifyParameter(ppp);
				return pp.ParameterTypes.Count;
			}

			private void WriteThunk(byte[] data, int placeholderIndex, IntPtr p, int index)
			{
				_memory.Protect(_memory.Start, _memory.Size, MemoryBlock.Protection.RW);
				var ss = _memory.GetStream(_memory.Start + (ulong)index * BlockSize, BlockSize, true);
				ss.Write(data, 0, data.Length);
				for (int i = data.Length; i < BlockSize; i++)
					ss.WriteByte(Padding);
				ss.Position = placeholderIndex;
				var bw = new BinaryWriter(ss);
				bw.Write((long)p);
				_memory.Protect(_memory.Start, _memory.Size, MemoryBlock.Protection.RX);
			}

			private IntPtr GetThunkAddress(int index)
			{
				return Z.US(_memory.Start + (ulong)index * BlockSize);
			}

			private void SetLifetime(int index, object lifetime)
			{
				if (_refs[index] == null)
					_refs[index] = new WeakReference(lifetime);
				else
					_refs[index].Target = lifetime;
			}

			public IntPtr GetFunctionPointerForDelegate(Delegate d)
			{
				// for this call only, the expectation is that it can be called multiple times
				// on the same delegate and not leak extra memory, so the result has to be cached
				lock (_sync)
				{
					var index = FindUsedIndex(d);
					if (index != -1)
					{
						return GetThunkAddress(index);
					}
					else
					{
						return GetArrivalFunctionPointer(
							Marshal.GetFunctionPointerForDelegate(d), new ParameterInfo(d.GetType()), d);
					}
				}
			}

			public IntPtr GetArrivalFunctionPointer(IntPtr p, ParameterInfo pp, object lifetime)
			{
				lock (_sync)
				{
					var index = FindFreeIndex();
					var count = VerifyDelegateSignature(pp);
					WriteThunk(Arrive[count], ArrivePlaceholderIndices[count], p, index);
					SetLifetime(index, lifetime);
					return GetThunkAddress(index);
				}
			}

			public Delegate GetDelegateForFunctionPointer(IntPtr p, Type delegateType)
			{
				lock (_sync)
				{
					var index = FindFreeIndex();
					var count = VerifyDelegateSignature(new ParameterInfo(delegateType));
					WriteThunk(Depart[count], DepartPlaceholderIndices[count], p, index);
					var ret = Marshal.GetDelegateForFunctionPointer(GetThunkAddress(index), delegateType);
					SetLifetime(index, ret);
					return ret;
				}
			}

			public IntPtr GetDepartureFunctionPointer(IntPtr p, ParameterInfo pp, object lifetime)
			{
				lock (_sync)
				{
					var index = FindFreeIndex();
					var count = VerifyDelegateSignature(pp);
					WriteThunk(Depart[count], DepartPlaceholderIndices[count], p, index);
					SetLifetime(index, lifetime);
					return GetThunkAddress(index);
				}
			}

		}
	}
}

using System;
using System.Linq;
using System.Reflection;

namespace StructureMap.AutoMocking.NSubstitute
{
	public class NSubstituteFactory
	{
		private readonly MethodInfo mockMethod;

		public NSubstituteFactory()
		{
			Assembly nSubstitute = Assembly.Load("NSubstitute");
			mockMethod = nSubstitute.GetType("NSubstitute.Substitute").GetMethods().First(x => x.Name == "For" &&
																						  x.GetGenericArguments().Length == 1 &&
																						  x.IsGenericMethod &&
																						  x.IsStatic);
			if (mockMethod == null)
				throw new InvalidOperationException("Unable to find Type NSubstitute.Substitute.For<T> in assembly " + nSubstitute.Location);
		}

		public object CreateMock(Type type)
		{
			var genericMockMethod = mockMethod.MakeGenericMethod(type);
			var mockedtype = genericMockMethod.Invoke(null, new object[] { new object[] { } });
			return mockedtype;
		}

		public object CreateMockThatCallsBase(Type type, object[] args)
		{
			var genericMockMethod = mockMethod.MakeGenericMethod(type);
			var mockedtype = genericMockMethod.Invoke(null, new object[] { args });
			return mockedtype;
		}
	}
}
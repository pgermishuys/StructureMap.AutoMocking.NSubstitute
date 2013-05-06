using System;

namespace StructureMap.AutoMocking.NSubstitute
{
	public class NSubstituteServiceLocator : ServiceLocator
	{
		private readonly NSubstituteFactory _mocks = new NSubstituteFactory();

		public T PartialMock<T>(params object[] args) where T : class
		{
			return (T)_mocks.CreateMockThatCallsBase(typeof(T), args);
		}

		public object Service(Type serviceType)
		{
			return _mocks.CreateMock(serviceType);
		}

		public T Service<T>() where T : class
		{
			return (T)_mocks.CreateMock(typeof(T));
		}
	}
}
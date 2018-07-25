using System;
using Simbad.Platform.Core.Dependencies;
using Xunit;

namespace Simbad.Platform.Core.Tests
{
    public class SimpleParameterlessCtorServiceResolverTests
    {
        private readonly SimpleParameterlessCtorServiceResolver _target;
        
        public SimpleParameterlessCtorServiceResolverTests()
        {
            _target = new SimpleParameterlessCtorServiceResolver();
        }
        
        [Fact]
        public void Should_CreateInstance_When_CorrectTypesUsed()
        {
            var service = _target.Resolve<ITestService>(typeof(TestService));
            
            Assert.NotNull(service);
        }
        
        [Fact]
        public void ShouldNot_CreateInstance_When_ImplementationType_IsNotDerivedFrom_AbstractionType()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _target.Resolve<ITestService>(typeof(AnotherService)));
            
            Assert.Contains($"type is not derived from <{typeof(ITestService)}>", ex.Message);
        }
        
        [Fact]
        public void ShouldNot_CreateInstance_When_ImplementationType_IsNotAClass()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _target.Resolve<object>(typeof(int)));
            
            Assert.Contains("type is not a class", ex.Message);
        }
        
        [Fact]
        public void ShouldNot_CreateInstance_When_ImplementationType_IsAnAbstractClass()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _target.Resolve<ITestService>(typeof(AbstractTestService)));
            
            Assert.Contains("type is an abstract class", ex.Message);
        }        
        
        [Fact]
        public void ShouldNot_CreateInstance_When_ImplementationType_HasNoParameterlessCtor()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _target.Resolve<ITestService>(typeof(ServiceWithoutParameterlessCtor)));
            
            Assert.Contains("type has no parameterless ctor", ex.Message);
        }        

        private interface ITestService
        {
        }

        private class TestService : ITestService
        {
        }

        private abstract class AbstractTestService : ITestService
        {
        }

        private class AnotherService
        {
        }

        private class ServiceWithoutParameterlessCtor : ITestService
        {
            private readonly int _field;
            
            public ServiceWithoutParameterlessCtor(int field)
            {
                _field = field;
            }
        }
    }
}
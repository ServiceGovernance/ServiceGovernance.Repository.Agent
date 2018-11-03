using FluentAssertions;
using NUnit.Framework;
using ServiceGovernance.Repository.Agent.Configuration;
using System;

namespace ServiceGovernance.Repository.Agent.Tests
{
    [TestFixture]
    public class RepositoryAgentOptionsTests
    {
        protected RepositoryAgentOptions _options;

        [SetUp]
        public void Setup()
        {
            _options = new RepositoryAgentOptions();
        }

        public class ValidateMethod : RepositoryAgentOptionsTests
        {
            [Test]
            public void Should_Throw_Exception_If_No_Repository_Is_Defined()
            {
                _options.Repository = null;

                Action action = () => _options.Validate();
                action.Should().ThrowExactly<ConfigurationException>().Where(e => e.ConfigurationName == "Repository");
            }

            [Test]
            public void Should_Throw_Exception_If_No_ServiceIdentifier_Is_Defined()
            {
                _options.Repository = new Uri("http://test.com");
                _options.ServiceIdentifier = null;

                Action action = () => _options.Validate();
                action.Should().ThrowExactly<ConfigurationException>().Where(e => e.ConfigurationName == "ServiceIdentifier");
            }

            [Test]
            public void Should_Throw_Exception_If_ServiceIdentifier_Is_Empty()
            {
                _options.Repository = new Uri("http://test.com");
                _options.ServiceIdentifier = "";

                Action action = () => _options.Validate();
                action.Should().ThrowExactly<ConfigurationException>().Where(e => e.ConfigurationName == "ServiceIdentifier");
            }

            [Test]
            public void Should_Not_Throw_Exception_If_Required_Values_Filled()
            {
                _options.Repository = new Uri("http://test.com");
                _options.ServiceIdentifier = "myapi";

                Action action = () => _options.Validate();
                action.Should().NotThrow();
            }
        }
    }
}

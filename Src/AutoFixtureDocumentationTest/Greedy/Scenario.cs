﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Ploeh.AutoFixtureDocumentationTest.Greedy
{
    public class Scenario
    {
        [Fact]
        public void BastardIsCreatedWithModestConstructorByDefault()
        {
            var fixture = new Fixture();
            var b = fixture.CreateAnonymous<Bastard>();
            Assert.IsAssignableFrom<DefaultFoo>(b.Foo);
        }

        [Fact]
        public void FooIsDefaultEvenWhenFooIsFrozen()
        {
            var fixture = new Fixture();
            fixture.Register<IFoo>(
                fixture.CreateAnonymous<DummyFoo>);
            var b = fixture.CreateAnonymous<Bastard>();
            Assert.IsAssignableFrom<DefaultFoo>(b.Foo);
        }

        [Fact]
        public void FooIsDummyWithSpecializedCustomization()
        {
            var fixture = new Fixture();
            fixture.Customize<Bastard>(c => c.FromFactory(
                new ConstructorInvoker(
                    new GreedyConstructorQuery())));
            fixture.Register<IFoo>(
                fixture.CreateAnonymous<DummyFoo>);
            var b = fixture.CreateAnonymous<Bastard>();
            Assert.IsAssignableFrom<DummyFoo>(b.Foo);
        }

        [Fact]
        public void FooIsDummyWithGeneralCustomization()
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(
                new ConstructorInvoker(
                    new GreedyConstructorQuery()));
            fixture.Register<IFoo>(
                fixture.CreateAnonymous<DummyFoo>);
            var b = fixture.CreateAnonymous<Bastard>();
            Assert.IsAssignableFrom<DummyFoo>(b.Foo);
        }
    }
}
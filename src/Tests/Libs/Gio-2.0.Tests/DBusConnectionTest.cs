﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gio.Tests
{
    [TestClass, TestCategory("SystemTest")]
    public class DBusConnectionTest : Test
    {
        [TestMethod]
        public void GetSessionBusShouldNotBeNull()
        {
            var obj = DBusConnection.Get(BusType.Session);
            obj.Should().NotBeNull();
        }
    }
}

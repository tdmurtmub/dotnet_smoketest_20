using System;
using System.Reflection;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
class ObjectPoolTestFixture
{
	[SetUp]
	public void SetUp()
	{
		ObjectPool.pool.Clear();
	}

	[Test]
	public void AddingInstanceReturnsTrueIfObjectWasAdded()
	{
		Assert.IsTrue(ObjectPool.Add("anything"));
	}

	[Test]
	public void AddingInstanceReturnsFalseIfObjectNotAdded()
	{
		string same_object = "something else";
		ObjectPool.Add(same_object);
		Assert.IsFalse(ObjectPool.Add(same_object));
	}

	[Test]
	public void AddingInstanceFiresEvent()
	{
		Object instance_received = null;
        ObjectPool.ObjectAdded += delegate(Object sender, ObjectPool.ObjectAddedEventArgs e) { instance_received = e.instance; };
		string source_object = "something";
		ObjectPool.Add(source_object);
		Assert.AreSame(source_object, instance_received);
        ObjectPool.ObjectAdded -= delegate(Object sender, ObjectPool.ObjectAddedEventArgs e) { };
	}

	[Test]
	public void AddingInstanceOnlyFiresIfActuallyAdded()
	{
		bool was_fired = false;
        ObjectPool.ObjectAdded += delegate(Object sender, ObjectPool.ObjectAddedEventArgs e) { was_fired = true; };
		string same_object = "something";
		ObjectPool.Add(same_object);
		was_fired = false;
		ObjectPool.Add(same_object);
		Assert.IsFalse(was_fired);
        ObjectPool.ObjectAdded -= delegate(Object sender, ObjectPool.ObjectAddedEventArgs e) { };
	}
}
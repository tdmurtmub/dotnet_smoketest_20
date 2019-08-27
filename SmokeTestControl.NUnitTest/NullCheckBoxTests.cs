using System;
using System.Diagnostics;
using System.Windows.Forms;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
class NullCheckBoxTestFixture
{
	[Test]
	public void Construction ()
	{
		Label target = new Label();
		Button button = new Button();
		NullCheckBox boxNotChecked = new NullCheckBox(target, button, false);
		Assert.IsFalse(boxNotChecked.Enabled);
		Assert.IsFalse(boxNotChecked.Checked);
		NullCheckBox boxChecked = new NullCheckBox(target, button, true);
		Assert.IsTrue(boxChecked.Enabled);
		Assert.IsTrue(boxChecked.Checked);
	}

	[Test]
	public void CheckBoxOffDisablesTargetControls()
	{
		Label input_control = new Label();
		Button type_button = new Button();
		NullCheckBox box = new NullCheckBox(input_control, type_button, true);
		box.Checked = false;
		box.OnClicked(null, null);
		Assert.IsFalse(input_control.Enabled);
		Assert.IsFalse(type_button.Enabled);
	}

	class AnySupportedArgumentType { }

	class AnyNonSupportedArgumentType<T> { }

	[Test]
	public void CheckBoxOnEnablesTypeButtonBasedOnType ()
	{
		Label input_control = new Label();
		Button type_button = new Button();
		NullCheckBox box = new NullCheckBox(input_control, type_button, true);
		box.Checked = true;
		type_button.Tag = typeof(AnySupportedArgumentType);
		box.OnClicked(null, null);
		Assert.IsTrue(type_button.Enabled);
		type_button.Tag = typeof(AnyNonSupportedArgumentType<int>);
		box.OnClicked(null, null);
		Assert.IsFalse(type_button.Enabled);
	}

	[Test]
	public void CheckBoxOnDisablesInputControlIfNoInstanceAvailable ()
	{
		Label input_control = new Label();
		Button type_button = new Button();
		NullCheckBox box = new NullCheckBox(input_control, type_button, true);
		box.Checked = true;
		input_control.Tag = null;
		type_button.Tag = typeof(AnySupportedArgumentType);
		box.OnClicked(null, null);
		Assert.IsFalse(input_control.Enabled);
	}

	[Test]
	public void CheckBoxOnEnablesInputControlForEditableTypes()
	{
		Label input_control = new Label();
		Button type_button = new Button();
		NullCheckBox box = new NullCheckBox(input_control, type_button, true);
		box.Checked = true;
		input_control.Tag = null;
		type_button.Tag = typeof(Int32);
		box.OnClicked(null, null);
		Assert.IsTrue(input_control.Enabled);
	}
}

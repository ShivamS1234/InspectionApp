using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Behavior
{
  public class PasswordValidationBehavior : Behavior<Entry>
  {
    const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{5,}$";


    protected override void OnAttachedTo(Entry bindable)
    {
      bindable.TextChanged += HandleTextChanged;
      base.OnAttachedTo(bindable);
    }

    void HandleTextChanged(object sender, TextChangedEventArgs e)
    {
      if (!String.IsNullOrEmpty(e.NewTextValue))
      {
        bool IsValid = false;
        IsValid = (Regex.IsMatch(e.NewTextValue, passwordRegex));
        ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
      }
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
      bindable.TextChanged -= HandleTextChanged;
      base.OnDetachingFrom(bindable);
    }
  }
}

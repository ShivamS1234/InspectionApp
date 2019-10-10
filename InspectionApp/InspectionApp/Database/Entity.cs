using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace InspectionApp.Database
{
  public class Entity : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    [PrimaryKey]
    public int? Id { get; set; }

    private DateTime _createdAt;

    public DateTime CreatedAt
    {
      get { return _createdAt; }
      set
      {
        if (value.Equals(_createdAt))
        {
          return;
        }
        _createdAt = value;
        OnPropertyChanged();
      }
    }

    private DateTime _modifiedAt;

    public DateTime ModifiedAt
    {
      get { return _modifiedAt; }
      set
      {
        if (value.Equals(_modifiedAt))
        {
          return;
        }
        _modifiedAt = value;
        OnPropertyChanged();
      }
    }

    private bool _deleted;

    public bool Deleted
    {
      get { return _deleted; }
      set
      {
        if (value.Equals(_deleted))
        {
          return;
        }
        _deleted = value;
        OnPropertyChanged();
      }
    }

    private bool _updated;

    public bool Updated
    {
      get { return _updated; }
      set
      {
        if (value.Equals(_updated))
        {
          return;
        }
        _updated = value;
        OnPropertyChanged();
      }
    }

    private bool _NewEntry;

    public bool NewEntry
    {
      get { return _NewEntry; }
      set
      {
        if (value.Equals(_NewEntry))
        {
          return;
        }
        _NewEntry = value;
        OnPropertyChanged();
      }
    }


    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Entity()
    {
      CreatedAt = DateTime.Now;
      ModifiedAt = DateTime.Now;
    }
  }
}

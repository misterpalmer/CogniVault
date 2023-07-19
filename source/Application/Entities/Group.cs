using CogniVault.Application.Entities;
namespace CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;
using System;
using System.Collections.Generic;

public class Group : IGroup
{
    public Guid Id { get; }
    public GroupName Name { get; set; }
    public ICollection<IUser> Users { get; }

    public Group(GroupName name)
    {
        Name = name;
        Users = new List<IUser>();
    }

    public void AddUser(IUser user)
    {
        Users.Add(user);
    }

    public void RemoveUser(IUser user)
    {
        Users.Remove(user);
    }

    public void Rename(GroupName newName)
    {
        Name = newName;
    }
}

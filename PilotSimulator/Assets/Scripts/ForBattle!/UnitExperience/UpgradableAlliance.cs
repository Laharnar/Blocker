using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableAlliance:MonoBehaviour
{

    [SerializeField] List<UpgradableUser> existingUsers = new List<UpgradableUser>();

    public void Add(UpgradableUser upgradableUser)
    {
        if(!existingUsers.Contains(upgradableUser))
            existingUsers.Add(upgradableUser);
    }

    internal UpgradableUser GetUser(int user)
    {
        return existingUsers[user];
    }
}

using UnityEngine;


public class ShotGun : Weapon
{
    public void changeFireMode()
    {
        if (fireModes > 0 && ammo > 0)
        {
            currentFireMode++;

            if (currentFireMode >= fireModes)
                currentFireMode = 0;

            if (currentFireMode == 0)
            {
                
                reloadCooldown = 2;
                projAmount = 8;
            }
            else
            {
                reloadCooldown = 4;
                clipSize = 1;
                projAmount = 32;
                if (clip >= 2)
                {
                    clip = 1;
                }

            }

        }
    }
}

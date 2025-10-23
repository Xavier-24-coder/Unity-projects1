using UnityEngine;


public class ShotGun : Weapon
{
    public void changeFireMode()
    {
        if (fireModes > 0)
        {
            currentFireMode++;

            if (currentFireMode >= fireModes)
                currentFireMode = 0;

            if (currentFireMode == 0)
            {

                projAmount = 8;
            }
            else
            {
                clipSize = 1;
                projAmount = 24;
                if (clip >= 2)
                {
                    clip = 1;
                }

            }

        }
    }
}

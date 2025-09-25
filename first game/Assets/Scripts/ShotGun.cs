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
                clipSize = 2;
                projAmount = 20;
            }
                

            else
            {
                clipSize = 1;
                projAmount = 40;
                if(clip >=2)
                {
                    clip = 1;
                }
                
            }

        }
    }
}

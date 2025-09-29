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
                
                projAmount = 16;
                if(clip >=2)
                {
                    clip = 1;
                }
                
            }

        }
    }
}

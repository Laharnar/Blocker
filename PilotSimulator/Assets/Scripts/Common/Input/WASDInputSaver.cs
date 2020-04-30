using UnityEngine;

public class WASDInputSaver:MonoBehaviour {

    public FloatVar horizontal;
    public FloatVar vertical;
    public bool raw = false;
    public bool keepSquared = true;
    public bool instantStart = false;
    public bool instantStop = false;
    Vector2 lastInput;
    private void Update()
    {
        if (keepSquared)
        {

            Vector2 v;
            if(raw)
                v = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            else v = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (instantStop)
            {
                if ((lastInput.x > 0 && v.x < lastInput.x )||
                    (lastInput.x < 0 && v.x > lastInput.x))
                {
                    v.x = 0;
                }

                if ((lastInput.y > 0 && v.y < lastInput.y) ||
                    (lastInput.y < 0 && v.y > lastInput.y))
                {
                    v.y = 0;
                }
            }
            v.Normalize();
            horizontal.Value = v.x;
            vertical.Value = v.y;

            lastInput = v;
        }
        else
        {
            horizontal.Value = Input.GetAxis("Horizontal");
            vertical.Value = Input.GetAxis("Vertical");
        }
    }
}

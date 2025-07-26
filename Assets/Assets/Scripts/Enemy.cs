using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool isExtreme = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  



    public void SetIsExtreme(bool isExtreme)
    {
        this.isExtreme = isExtreme;
    }
    public bool GetIsExtreme()
    {
        return isExtreme;
    }
}

using UnityEngine;

public class BowView : MonoBehaviour
{
    private Animator anim;
    private StringBone stringBone;
    private GameObject stringBoneOriginalParent;
    private Vector3 stringBoneOriginalEulerAngles;
    private Vector3 stringBoneOriginalPosition;

    [SerializeField]
    private float arrowReleaseStrength;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        stringBone = gameObject.GetComponentInChildren<StringBone>();

        stringBoneOriginalParent = stringBone.transform.parent.gameObject;
        stringBoneOriginalEulerAngles = stringBone.transform.localEulerAngles;
        stringBoneOriginalPosition = stringBone.transform.localPosition;
    }

    public void DrawBow()
    {
        anim.SetTrigger("DrawBow");
    }

    public void ReleaseArrow(ArrowView arrow)
    {
        arrow.Shoot(arrowReleaseStrength);
        anim.SetTrigger("ReleaseArrow");
    }

    public void CancelDraw()
    {
        anim.SetTrigger("ReleaseArrow");
    }

    public void ParentStringBoneToObject(GameObject parentObj)
    {
        stringBone.transform.SetParent(parentObj.transform);
        stringBone.transform.localEulerAngles = stringBoneOriginalEulerAngles;
    }

    public void ResetStringBoneParent()
    {
        stringBone.transform.SetParent(stringBoneOriginalParent.transform);
        stringBone.transform.localEulerAngles = stringBoneOriginalEulerAngles;
        stringBone.transform.localPosition = stringBoneOriginalPosition;
    }
}

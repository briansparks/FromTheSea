public class NameLabelView : AbstractLabelView
{
    protected override float CULLING_DISTANCE => Constants.LABEL_CULLING_DISTANCE;

    // Update is called once per frame
    void Update()
    {
        if (Label.gameObject.activeInHierarchy && !IsDisabled)
        {
            RotateLabelToFacePlayer();
        }
    }
}

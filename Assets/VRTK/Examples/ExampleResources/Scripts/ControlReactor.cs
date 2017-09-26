namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEventHelper;

    public class ControlReactor : MonoBehaviour
    {
        public Adder Adder;
        public TextMesh dispalyTextMesh;

        private float currentPreviewNumber;

        private VRTK_Control_UnityEvents controlEvents;
        private VRTK_InteractableObject_UnityEvents interactableObjEvents;

        private void Awake() {
            Adder = transform.root.GetComponentInChildren<Adder>();
            print("adder added");
        }

        private void Start()
        {
            controlEvents = GetComponent<VRTK_Control_UnityEvents>();
            if (controlEvents == null)
            {
                controlEvents = gameObject.AddComponent<VRTK_Control_UnityEvents>();
            }
            controlEvents.OnValueChanged.AddListener(HandleLivePreviewChange);


            interactableObjEvents = GetComponent<VRTK_InteractableObject_UnityEvents>();
            if (interactableObjEvents == null) {
                interactableObjEvents = gameObject.AddComponent<VRTK_InteractableObject_UnityEvents>();
            }
            
            interactableObjEvents.OnUngrab.AddListener(HandleReleaseAdd);

        }


        //on CHANGE, display current value for this slider to help user gauge
        private void HandleLivePreviewChange(object sender, Control3DEventArgs e)
        {
            currentPreviewNumber = e.value;
            dispalyTextMesh.text = currentPreviewNumber.ToString();
            //dispalyTextMesh.text = currentPreviewNumber.ToString() + "(" + e.normalizedValue.ToString() + "%)";

        }

        //on RELEASE, Add to Adder
        private void HandleReleaseAdd(object interactingObject, InteractableObjectEventArgs e) 
            {
            Adder.Add(currentPreviewNumber);
            print("adder used");
        }
    }
}
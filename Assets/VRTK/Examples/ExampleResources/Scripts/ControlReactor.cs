namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEventHelper;
    using System.Collections;

    public class ControlReactor : MonoBehaviour
    {
        public Adder Adder;
        public TextMesh dispalyTextMesh;
        private MeshRenderer textMeshRenderer;

        private float currentPreviewNumber;
        private bool previewShowing = false;
        private Color textStartColor;

        private VRTK_Control_UnityEvents controlEvents;
        private VRTK_InteractableObject_UnityEvents interactableObjEvents;
        private VRTK_Slider slider;

        private void Awake() {
            Adder = transform.root.GetComponentInChildren<Adder>();

            slider = GetComponent<VRTK_Slider>();
            textMeshRenderer = dispalyTextMesh.GetComponent<MeshRenderer>();
            textStartColor = textMeshRenderer.material.color;
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

        IEnumerator HidePreview() {
            yield return new WaitForSecondsRealtime(0.05f);
            //dispalyTextMesh.text = "";
            StartCoroutine(LerpColorAway());
        }

        IEnumerator LerpColorAway() {
            float ElapsedTime = 0.0f;
            float TotalTime = 0.3f;
            while (ElapsedTime < TotalTime) {
                ElapsedTime += Time.deltaTime;
                textMeshRenderer.material.color = new Color(textStartColor.r, textStartColor.g, textStartColor.b, (1 - (ElapsedTime / TotalTime)));
                yield return null;
            }
        }


        //on CHANGE, display current value for this slider to help user gauge
        private void HandleLivePreviewChange(object sender, Control3DEventArgs e)
        {
            textMeshRenderer.material.color = textStartColor;
            currentPreviewNumber = e.value;
            if(currentPreviewNumber > 0) {  
                dispalyTextMesh.text = "+" + currentPreviewNumber.ToString(); //add a plus sign for positive nums
            } else if (currentPreviewNumber < 0) {
                dispalyTextMesh.text = currentPreviewNumber.ToString();
            } else {
                dispalyTextMesh.text = " " + currentPreviewNumber.ToString();
                StartCoroutine(HidePreview());
            }

        }

        //on RELEASE, Add to Adder
        private void HandleReleaseAdd(object interactingObject, InteractableObjectEventArgs e) 
        {
            StartCoroutine(PopNumToConfirm());
            Adder.Add(currentPreviewNumber);
        }

        IEnumerator PopNumToConfirm() {
            dispalyTextMesh.fontSize = 30;
            yield return new WaitForSecondsRealtime(0.2f);
            dispalyTextMesh.fontSize = 15;
        }

       
    }
}
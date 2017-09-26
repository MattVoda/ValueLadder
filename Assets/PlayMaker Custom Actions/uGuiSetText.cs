// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the text value of a UGui Text component. With reset on exit option ")]
	public class uGuiSetText : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		//[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UGui Text component.")]
		public FsmString text;
		
		[Tooltip("The former text of the UGui Text component.")]
		public FsmString StoredOldText;
		
		
		
		[Tooltip("Reset the Text when exiting this state.")]
		public bool resetOnExit;

		public bool everyFrame;
		
		[Tooltip("Bypass button to drive the action by bool")]
		public FsmBool doThisAction = true;

		private uUI.Text _text;
		

		public override void Reset()
		{
			text = null;
			StoredOldText = null;
			resetOnExit = false;
			everyFrame = false;
			doThisAction = true;
		}
		
		public override void OnEnter()
		{
	
			if (doThisAction.Value == false)
			{
				Finish();
			}
			else
			{
				GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
				if (_go!=null)
				{
					_text = _go.GetComponent<uUI.Text>();
				}
				DoGetOldTextValue();
	
				DoSetTextValue();
				
				if (!everyFrame)
					Finish();
			}
			
		}
		
		
		public override void OnUpdate()
		{
			DoSetTextValue();
		}
		
		
		
		public override void OnExit()
		{
			if (doThisAction.Value)
			{
				if (resetOnExit)
				{
					DoSetOldTextValue();
				}
			}
		
		}
		

		
		void DoSetTextValue()
		{
			
			
			if (_text!=null)
			{
				_text.text = text.Value;
			}
		}
		
		void DoGetOldTextValue()
		{
			
			if (_text!=null)
			{
			StoredOldText.Value = _text.text;
			}
		}
		
		void DoSetOldTextValue()
		{
			if (_text!=null)
			{
			_text.text = StoredOldText.Value;
			}
		}
		
	}
}
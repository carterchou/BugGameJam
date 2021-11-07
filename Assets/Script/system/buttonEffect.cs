using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonEffect : MonoBehaviour
{
	public pop_item pop;
	public bool needSFX = true;
    public Action callback;

    public void press_SE()
    {
		if(needSFX == true) {
			pop_item.btnType type = pop_item.btnType.other;
			if (pop != null) {
				type = pop.GetBtnType();
			}
			switch (type) {
				case pop_item.btnType.ok:
					AudioManager.PlaySE("ok");
					break;
				case pop_item.btnType.yes_no:
					AudioManager.PlaySE("ok");
					break;
			}
		}
        
    }
   
    public void close_SE() {
		if (needSFX == true) {
			pop_item.btnType type = pop_item.btnType.other;
			if (pop != null) {
				type = pop.GetBtnType();
			}
			switch (type) {
				case pop_item.btnType.yes_no:
					AudioManager.PlaySE("cancel");
					break;
			}
		}
    }
}

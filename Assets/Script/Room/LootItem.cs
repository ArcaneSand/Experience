using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Item item;
    public Image image;
    // Start is called before the first frame update
    private float waitTime = 0.5f;
    private void Awake() {
        image = GetComponent<Image>();
    }

    public void InitializeItem(Item item){
        this.item = item;
        int RV = Random.Range(0,item.sprite.Length);
        image.sprite = item.sprite[RV];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HoverManager.OnMouseLoseFocus();
    }

    private void ShowMessage(){
        HoverManager.OnMouseHover(item.ToolTip(),Input.mousePosition);
    }

    private IEnumerator StartTimer(){
        yield return new WaitForSeconds(waitTime);
        ShowMessage();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        item.Click();
        if(item.GetType()== typeof(Herb)){
            Destroy(gameObject);
        }
    }
}

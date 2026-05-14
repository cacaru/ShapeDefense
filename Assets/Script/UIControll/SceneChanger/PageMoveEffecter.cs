using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class PageMoveEffecter : SceneSingleton<PageMoveEffecter>
{
    [SerializeField] private RectTransform effect_rect;
    [SerializeField] private RectTransform gate_rect;

    public bool End = false;

    public void EffectShow() {
        var tmp_rect = effect_rect.rect;
        var tmp_gate = gate_rect.rect;
        StartCoroutine(EffectShowOn(tmp_rect, tmp_gate));
    }

    IEnumerator EffectShowOn(Rect rect, Rect gate) {
        while (true) {
            if(rect.width <= 0) {
                break;
            }
            rect.width -= 130;
            rect.height -= 260;
            effect_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.width);
            effect_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.height);
            yield return wff;
        }

        StartCoroutine(GateOn(gate));
    }

    IEnumerator GateOn(Rect gate) {
        while (true) {
            if (gate.width >= 150000) {
                break;
            }
            gate.width += 10000;
            gate.height += 10000;
            gate_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, gate.width);
            gate_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gate.height);
            yield return wff;
        }
        End = true;
    }
}

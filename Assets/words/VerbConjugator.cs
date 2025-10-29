using System;
using Unity.VisualScripting;
using UnityEngine;

public static class VerbConjugator
{
    private static VerbData verbToHave;
    private static VerbData verbToBe;
    private static VerbData verbWill;
    
    public static string Conjugate(VerbData conjugateThis, Tense contextTense, Pronoun contextPronoun)
    {
        return contextTense switch
        {
            (Tense.present_simple) => $"{conjugateThis.present[Convert.ToInt32(contextPronoun)]}",
            (Tense.present_continuous) =>
                $"{verbToBe.present[Convert.ToInt32(contextPronoun)]} {conjugateThis.presentParticiple}",
            (Tense.present_perfect) =>
                $"{verbToHave.present[Convert.ToInt32(contextPronoun)]} {conjugateThis.pastParticiple}",
            (Tense.present_perfect_continuous) =>
                $"{verbToHave.present[Convert.ToInt32(contextPronoun)]} {verbToBe.pastParticiple} {conjugateThis.presentParticiple}",
            (Tense.past_simple) => $"{conjugateThis.preterite[Convert.ToInt32(contextPronoun)]}",
            (Tense.past_continuous) =>
                $"{verbToBe.preterite[Convert.ToInt32(contextPronoun)]} {conjugateThis.presentParticiple}",
            (Tense.past_perfect) =>
                $"{verbToHave.preterite[Convert.ToInt32(contextPronoun)]} {conjugateThis.pastParticiple}",
            (Tense.past_perfect_continuous) =>
                $"{verbToHave.preterite[Convert.ToInt32(contextPronoun)]} {verbToBe.pastParticiple} {conjugateThis.presentParticiple}",
            (Tense.future_simple) => $"{verbWill.imperative} {conjugateThis.imperative}",
            (Tense.future_continuous) =>
                $"{verbWill.imperative} {verbToBe.imperative} {conjugateThis.presentParticiple}",
            (Tense.future_perfect) => $"{verbWill.imperative} {verbToHave.imperative} {conjugateThis.pastParticiple}",
            (Tense.future_perfect_continuous) =>
                $"{verbWill.imperative} {verbToHave.imperative} {verbToBe.pastParticiple} {conjugateThis.presentParticiple}",
            _ => ""
        };
    }

    public static void SetAuxiliaryVerbs(VerbData toHave, VerbData toBe, VerbData will)
    {
        verbToHave = toHave;
        verbToBe = toBe;
        verbWill = will;
    }
}
